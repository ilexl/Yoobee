using System;
using System.Diagnostics;
using ArchitectsInVoid.Debug;
using ArchitectsInVoid.VesselComponent;
using Godot;

namespace ArchitectsInVoid.Player.ComponentCreation;

// Keeps track of the current state of the component placing
internal enum ComponentPlacerState
{
    Idle,
    Placing,
}

public partial class ComponentCreator : Node
{
    #region Config

    [ExportCategory("Placement")] 
    [Export] private bool _gridSnap = true;
    [Export] private double _gridSize = 0.1; // 10 centimeters
        [ExportGroup("Sensitivity")]
            [Export] private double _placementDistanceSensitivity = 1.0;
            [Export] private double _placementRotationSensitivity = 1.0;
        [ExportGroup("Distance")]
            [Export] private double _minPlacementDistance = 1;
            [Export] private double _maxPlacementDistance = 30;
            [Export] private double _maxTruncationThreshold = 5.0;
            [Export] private double _minTruncationThreshold = 1.5;
        [ExportGroup("Color")]
            [Export] private Color _maxTruncationColor = Colors.Crimson;
            [Export] private Color _minTruncationColor = Colors.DarkGreen;
            [Export] private Color _noTruncationColor = Colors.CornflowerBlue;
    [ExportCategory("Assigned objects")]
        [Export] private Node3D _head;
        [Export] private RigidBody3D _body;
    
    #endregion
    
    // Placement cursor
    private double _desiredPlacementDistance = 10.0; // The distance the player has selected
    private double _truncatedPlacementDistance; // The distance after truncation (raycast into existing component)
    private double _storedPlacementDistance; // The stored distance after initiating a placement to be returned to after ending the placement
    
    private Vector3 _truncatedPlacementPosition;

    private Vector3 _placementNormal;
    
    private Vector3 _cursorStart;
    private Vector3 _cursorEnd;
    private Cursor _cursor;

    private Vessel _targetedVessel;
    private RigidBody3D _targetedCollider;
    private CollisionShape3D _targetedShape;
    private Window _root;

    private bool _canPlace;

    public static ComponentCreator Singleton;
    
    
    // Component selection
    private PackedScene _selectedComponentScene;
    
    public PackedScene SelectedComponentScene // Useful for setting the label in addition to the value
    {
        get => _selectedComponentScene;
        set
        {
            _selectedComponentScene = value;
            _cursor.SetLabelName(value);
        } 
    }

    private ComponentPlacerState _state = ComponentPlacerState.Idle;
    
    
    public override void _Ready()
    {
        if (Engine.IsEditorHint()) { return; } // do NOT run when not in game
        // Cursor
        var cursorScene = (PackedScene)ResourceLoader.Load("res://Player/ComponentCreation/Cursor.tscn");
        var cursorObject = cursorScene.Instantiate();
        _cursor = cursorObject as Cursor;

        Singleton = this;

        CallDeferred("add_child", _cursor);

        _root = GetTree().Root;
        _canPlace = true;
    }

    

    public override void _PhysicsProcess(double delta)
    {
        if (Input.MouseMode == Input.MouseModeEnum.Visible) 
        {
            if (_canPlace)
            {
                _canPlace = false;
                GD.Print("Can NOT place component now...");
            }
        }
        // Store if player inputs have been taken this frame
        // This should probably be in PhysicsProcess
        float placeAction = Input.GetActionStrength("place_component");

        if (_canPlace is false)
        {
            _cursor.Visible = false;
            _state = ComponentPlacerState.Idle;
            if (placeAction == 0.0 && Input.MouseMode == Input.MouseModeEnum.Captured)
            {
                _canPlace = true;
                GD.Print("Can NOW place component again...");
            }
        }
        else
        {

            float placementDistanceControl = (Input.IsActionJustPressed("increase_placement_distance") ? 1 : 0) + (Input.IsActionJustPressed("decrease_placement_distance") ? -1 : 0);

            // Show the cursor and handle placement distance if the player has a component selected on their hotbar
            SetPlaceRotation(delta);
            if (SelectedComponentScene is not null)
            {
                _cursor.Visible = true;
                SetPlaceDistance(placementDistanceControl);
            }
            else
            {
                _cursor.Visible = false;
            }

            switch (_state)
            {
                // The player isn't already placing something
                case ComponentPlacerState.Idle:
                    // Set the starting position of the cursor and move on to the next state if the player has depressed the place button
                    if (placeAction == 1.0 && SelectedComponentScene is not null)
                    {
                        StartPlace();

                    }
                    // Set the whole cursor to be at the cursor position
                    else
                    {
                        Idle();
                    }

                    break;
                // The player has started placing something TODO: cancellation
                case ComponentPlacerState.Placing:
                    PlacingVisuals();
                    if (placeAction == 0.0) FinishPlace();
                    break;
            }
        }   
    }
    
    private void SetPlaceDistance(float value)
    {
        double change = _placementDistanceSensitivity * value;
        var spaceState = _head.GetWorld3D().DirectSpaceState;

        _desiredPlacementDistance = Math.Clamp(_desiredPlacementDistance + change, _minPlacementDistance, _maxPlacementDistance);
        
        var query = PhysicsRayQueryParameters3D.Create(_head.Position, CalculateCursorPosition(), 2);
        var result = spaceState.IntersectRay(query);
        if (result.Count > 0)
        {
            _truncatedPlacementPosition = (Vector3)result["position"];
            _truncatedPlacementDistance = (_truncatedPlacementPosition - _head.Position).Length();

            if (_state == ComponentPlacerState.Idle)
            {
                _targetedCollider = (RigidBody3D)result["collider"];
                _targetedVessel = (Vessel)_targetedCollider.GetParent();
            }

            _targetedShape = (CollisionShape3D)_targetedCollider.GetChild(1 + (int)result["shape"]);
            _cursor.Basis = _targetedCollider.Transform.Basis * _targetedShape.Transform.Basis;
            _cursor.Scale = Vector3.One; // Reset the scale since the basis also includes it
            
            SetPlaceColor();
            SnapPlacementToGrid(_targetedShape, result);
            
        }
        else
        {
            
            _truncatedPlacementPosition = Vector3.Zero;
            _truncatedPlacementDistance = _desiredPlacementDistance;
            if (_state == ComponentPlacerState.Idle) _targetedVessel = null;
            _cursor.SetColor(_noTruncationColor);
        }
    }


    private void SetPlaceColor()
    {
        double colorVal = (_desiredPlacementDistance - _truncatedPlacementDistance - _minTruncationThreshold) / (_maxTruncationThreshold - _minTruncationThreshold);
        _cursor.SetColor(_minTruncationColor.Lerp(_maxTruncationColor, Math.Clamp(colorVal, 0, 1)));
    }

    private void SnapPlacementToGrid(CollisionShape3D targetedShape, Godot.Collections.Dictionary result)
    {
        (Vector3 widthV, Vector3 heightV, Vector3 depthV) = GetFaceVectors(_targetedCollider.Transform * targetedShape.Transform, (Vector3)result["normal"] );
        if (_gridSnap)
        {
            //TODO: transform position to local of the cube and snap to the face, then transform back to world
            //PROBLEM: Floating point errors
            //Tomorrow me can deal with this
            Transform3D transformOrtho =  targetedShape.Transform.Orthonormalized() * _targetedCollider.Transform.Orthonormalized();

            Vector3 targetRelativeHitPosition = _truncatedPlacementPosition * transformOrtho;
            
            targetRelativeHitPosition = (targetRelativeHitPosition + targetedShape.Scale).Snapped(_gridSize) - targetedShape.Scale;
            _truncatedPlacementPosition = transformOrtho * targetRelativeHitPosition;
        }
        
        double widthLength = widthV.Length();
        double heightLength = heightV.Length();
        
        Vector3 faceCenter = targetedShape.GlobalPosition + depthV / 2;
        DebugDraw.Grid(faceCenter,  widthV / widthLength, heightV / heightLength, countUp: widthLength / 0.2, countRight: heightLength / 0.2);

        _placementNormal = (Vector3)result["normal"];
    }
    
    /// <summary>
    /// Handles the rotation of the cursor and any mid placement components based on head orientation.
    /// </summary>
    /// <param name="delta">Time since last frame</param>
    private void SetPlaceRotation(double delta)
    {
        double pitch = Input.GetAxis("component_rotate_pitch_down", "component_rotate_pitch_up") * _placementRotationSensitivity * delta;
        double yaw = Input.GetAxis("component_rotate_yaw_left", "component_rotate_yaw_right") * _placementRotationSensitivity * delta;
        double roll = Input.GetAxis("component_rotate_roll_right", "component_rotate_roll_left") * _placementRotationSensitivity * delta;
        Basis headBasis = _head.Transform.Basis;
        Basis xBasis = new Basis(headBasis.X, pitch);
        Basis yBasis = new Basis(headBasis.Y, yaw);
        Basis zBasis = new Basis(headBasis.Z, roll);
        Basis combined = xBasis * yBasis * zBasis * _cursor.Transform.Basis;
        combined = combined.Orthonormalized();
        _cursor.Transform = _cursor.Transform with { Basis = combined };
    }


    /// <summary>
    /// Handles the visuals of placing
    /// </summary>
    private void PlacingVisuals()
    {
        _cursorEnd = CalculateCursorPosition();
        if (_gridSnap)
        {
            // Transform3D transformOrtho =  _cursor.Transform.Orthonormalized();
            //
            // Vector3 targetRelativeStartPosition = _cursorStart * transformOrtho;
            // Vector3 targetRelativeEndPosition = _cursorEnd * transformOrtho;
            // targetRelativeEndPosition = targetRelativeEndPosition.Snapped(_gridSize);
            // _cursorEnd = transformOrtho * targetRelativeEndPosition;
            //
        }
        
        
        
        
        
        // This is a sign that this whole thing needs a refactor. This shouldn't be here.
        var component = _selectedComponentScene.Instantiate() as PlaceableComponent;
        if (component.ComponentType == PlaceableComponentType.FixedScale)
        {
            Vector3 previewScale = component.Scale;

            Vector3 previewOffset = _cursor.Transform.Basis * previewScale;

            
            Vector3 normalOffset = GetOffsetFromSurface(component);
            
            _cursor.SetCornerPositions(_cursorStart - previewOffset / 2 + normalOffset, _cursorStart + previewOffset / 2 + normalOffset);
        }
        else
        {
            _cursor.SetCornerPositions(_cursorStart, _cursorEnd);
        }
        
        
        // _cursor.SetScale(scale);
        _cursor.SetLabelVisible(true);
    }

    private void StartPlace()
    {
        _state = ComponentPlacerState.Placing;
        _cursorStart = _truncatedPlacementPosition == Vector3.Zero ? CalculateCursorPosition() : _truncatedPlacementPosition;

        
        // Store the placement distance to stop the node from getting constantly closer to the player
        _storedPlacementDistance = _desiredPlacementDistance;
        // Set the place distance to start at the current truncated distance to make thins more intuitive
        _desiredPlacementDistance = _truncatedPlacementDistance;
    }

    private void Idle()
    {
        // Set the position to the truncated position if it exists, or just go the full distance
        Vector3 position = _truncatedPlacementPosition == Vector3.Zero ? CalculateCursorPosition() : _truncatedPlacementPosition;
        _cursor.Position = position;
        _cursor.SetScale(Vector3.Zero);

        
        // This is a sign that this whole thing needs a refactor. This shouldn't be here.
        
        if (_selectedComponentScene?.Instantiate() is PlaceableComponent { ComponentType: PlaceableComponentType.FixedScale } component)
        {
            Vector3 previewScale = component.Scale;
            Vector3 previewOffset = _cursor.Transform.Basis * previewScale;
            Vector3 normalOffset = GetOffsetFromSurface(component);
            
            _cursor.SetCornerPositions(position - previewOffset / 2 + normalOffset, position + previewOffset / 2 + normalOffset);
        }
        else
        {
            _cursor.SetCornerPositions(position, position);
        }
        
    }
    

    /// <summary>
    /// Cleans up everything when finishing placement
    /// </summary>
    private void FinishPlace()
    {
        var placeableComponent = _selectedComponentScene.Instantiate() as PlaceableComponent;
        // Resolve the stored distance
        _desiredPlacementDistance = _storedPlacementDistance;
        _state = ComponentPlacerState.Idle;
        _cursor.SetLabelVisible(false);
        if (_selectedComponentScene is null) return;
        
        
        var scale = _cursor.GetComponentScale();
        if (scale.X * scale.Y * scale.Z == 0) return;
        var position = _cursorStart.Lerp(_cursorEnd, 0.5);
        if (placeableComponent.ComponentType == PlaceableComponentType.FixedScale)
        {
            
            position += GetOffsetFromSurface(placeableComponent);
        }
        
        var rotation = _cursor.Transform.Basis;
        
        placeableComponent.Place(position, scale, rotation.Orthonormalized(), _targetedVessel);
    }

    public Vessel PlaceFromData(Vector3 position, Vector3 scale, Basis basis, PlaceableComponent.Component type, Vessel vessel)
    {
        PackedScene typeScene = GetPackedSceneForType(type);
        GD.Print($"Component being placed is {typeScene.ResourcePath}");
        if(typeScene == null) return vessel;
        var placeableComponent = typeScene.Instantiate() as PlaceableComponent;
        if(vessel == null)
        {
            GD.Print("Creating new vessel with component");
            vessel = placeableComponent.PlaceRVN(position, scale, basis);
            return vessel;
        }
        else
        {
            GD.Print("Adding component to existing vessel");
            vessel = placeableComponent.PlaceRV(position, scale, basis, vessel);
            return vessel;
        }
    }

    private PackedScene GetPackedSceneForType(PlaceableComponent.Component type)
    {
        switch (type)
        {
            case PlaceableComponent.Component.Armour:
                return GD.Load("res://Vessels/VesselComponents/Armour/Armour.tscn") as PackedScene;
            case PlaceableComponent.Component.Cockpit:
                return GD.Load("res://Vessels/VesselComponents/Cockpit/Cockpit.tscn") as PackedScene;
            case PlaceableComponent.Component.Thruster:
                return GD.Load("res://Vessels/VesselComponents/Thruster/Thruster.tscn") as PackedScene;
            case PlaceableComponent.Component.None:
            default:
                GD.PushWarning("ComponentCreator: component type not defined - not creating a component...");
                return null;

        }
    }

    private Vector3 CalculateCursorPosition()
    {
        return _head.Position - _head.Transform.Basis.Z * _desiredPlacementDistance;
    }


    // TODO: Put this somewhere more useful
    public (Vector3 widthVector, Vector3 heightVector, Vector3 depthVector) GetFaceVectors(Transform3D colliderTransform, Vector3 collisionNormal)
    {
        // Get the basis (local axes) of the collider's transform
        Basis basis = colliderTransform.Basis;

        // Find the axis in the basis that is closest to the normal (the one aligned with the face normal)
        Vector3 axisX = basis * Vector3.Right;
        Vector3 axisY = basis * Vector3.Up;
        Vector3 axisZ = basis * Vector3.Forward;

        // Determine which axis is closest to the collision normal
        double dotX = collisionNormal.Dot(axisX);
        double dotY = collisionNormal.Dot(axisY);
        double dotZ = collisionNormal.Dot(axisZ);
        
        double dotAX = Mathf.Abs(dotX);
        double dotAY = Mathf.Abs(dotY);
        double dotAZ = Mathf.Abs(dotZ);

        // Based on the largest dot product, the normal is aligned with that axis, and the others are width and height
        if (dotAX > dotAY && dotAX > dotAZ)
        {
            // The normal is aligned with the local X axis, so Y and Z are width and height
            return (axisY, axisZ, axisX * Math.Sign(dotX));
        }
        if (dotAY > dotAX && dotAY > dotAZ)
        {
            // The normal is aligned with the local Y axis, so X and Z are width and height
            return (axisX, axisZ, axisY * Math.Sign(dotY));
        }
        
        // The normal is aligned with the local Z axis, so X and Y are width and height
        return (axisX, axisY, axisZ * Math.Sign(dotZ));
        
    }


    private Vector3 GetOffsetFromSurface(PlaceableComponent component)  
    {
        Vector3 widthXNormal = _cursor.Transform.Basis * component.Transform.Basis.X;
        Vector3 widthYNormal = _cursor.Transform.Basis * component.Transform.Basis.Y;
        Vector3 widthZNormal = _cursor.Transform.Basis * component.Transform.Basis.Z;

        double dotX = Math.Abs(widthXNormal.Dot(_placementNormal));
        double dotY = Math.Abs(widthYNormal.Dot(_placementNormal));
        double dotZ = Math.Abs(widthZNormal.Dot(_placementNormal));

        double maxDot = Math.MaxMagnitude(Math.MaxMagnitude(dotX, dotY), dotZ);
            
        return maxDot * _placementNormal / 2;
    }

}