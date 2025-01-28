using System;
using System.Collections.Generic;
using System.Diagnostics;
using ArchitectsInVoid.Audio;
using ArchitectsInVoid.Debug;
using ArchitectsInVoid.Debug.Meshes;
using ArchitectsInVoid.Interactables;
using ArchitectsInVoid.UI;
using ArchitectsInVoid.Vessels.VesselComponents.Cockpit;
using Godot;
using Godot.Collections;

namespace ArchitectsInVoid.Player;

public partial class PlayerController : Node
{
    private enum RotationControlMode
    {
        HeadOnly,
        HeadAndBody,
        Locked
    }
    
    private Basis _freeHeadMovement;
    private RotationControlMode _rotationMode = RotationControlMode.HeadAndBody;
    
    #region Config
    private const float Acceleration = 25.0f;
    private const float MouseSensitivity = 0.05f;
    private const float RollSensitivity = 50.0f;

    private const double MinPitch = -50 * (Math.PI / 180.0);
    private const double MaxPitch = 50 * (Math.PI / 180.0);
    private const double MinYaw = -70 * (Math.PI / 180.0);
    private const double MaxYaw = 70 * (Math.PI / 180.0);
    private const double MinRoll = -20 * (Math.PI / 180.0);
    private const double MaxRoll = 20 * (Math.PI / 180.0);
    
    [Export] private Vector3 _useDistance;
    
    private Vector3 _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity_vector").As<Vector3>() *
                               ProjectSettings.GetSetting("physics/3d/default_gravity").As<float>();
    #endregion

    #region Nodes
    public RigidBody3D Body;
    public Node3D Head;
    private Node3D _headPosition;
    private Camera3D _camera;
    private CockpitBehavior _mountedCockpit;
    #endregion
    
    public bool Dampeners = true;
    
    public bool Jetpack
    {
        get
        {
            // TODO: Fuel and power
            bool hasFuel = true; 
            bool hasPower = true;

            return hasFuel && hasPower && _rotationMode == RotationControlMode.HeadAndBody;
        }
        set
        {
            // TODO: Fuel and power
            bool hasFuel = true; 
            bool hasPower = true;

            _rotationMode = hasFuel && hasPower && value ? RotationControlMode.HeadAndBody : RotationControlMode.HeadOnly;
        }
    }
    
    public override void _Ready()
    {
        var ev = FmodServer.CreateEventInstance("event:/BarneyFromBlackMesa");
        ev.Start();
        // Assign our components
        Body = GetNode<RigidBody3D>("Body");
        Head = GetNode<Node3D>("Head");
        _headPosition = Body.GetNode<Node3D>("HeadPosition");
        _camera = Head.GetNode<Camera3D>("Camera");
        
        Head.Transform = _headPosition.Transform;
        DebugDraw.Line(Body.Position, duration: 50);
    }
    public override void _PhysicsProcess(double delta)
    {
        var moveVector = new Vector3(0, 0, 0);
        if (Pause.Singleton.IsPaused is not true)
        {
            Body.Freeze = false;
            if(Input.MouseMode != Input.MouseModeEnum.Visible)
            {
                moveVector = ProcessKeys(delta);
                HighlightObjectsUnderCrosshair();
            }
            HeadMovement();
            BodyRotation();

            if (Jetpack) JetpackProcess(moveVector, delta);
            else NoJetpackProcess(delta);
            if (_mountedCockpit != null)
            {
                Body.GlobalPosition = _mountedCockpit.PhysicalCockpit.GlobalPosition;
                Head.GlobalRotation = _mountedCockpit.PhysicalCockpit.GlobalRotation;
            }
        }
        else
        {
            Body.Freeze = true;
        }

        
    }

    #region Input
    public override void _Input(InputEvent @event)
    {
        if (Input.MouseMode == Input.MouseModeEnum.Visible) { return; }

        if (@event is InputEventMouseMotion mouseEvent)
        {
            ProcessMouseInput(mouseEvent);
        }
    }


    
    private void ProcessMouseInput(InputEventMouseMotion movement)
    {
        double azimuthInput = Mathf.DegToRad(-movement.Relative.X * MouseSensitivity);
        double elevationInput = Mathf.DegToRad(-movement.Relative.Y * MouseSensitivity);
        switch (_rotationMode)
        {
            case RotationControlMode.HeadOnly:
                    
                _freeHeadMovement = _freeHeadMovement.Rotated(Vector3.Up, azimuthInput);
                _freeHeadMovement = _freeHeadMovement.Rotated(_freeHeadMovement * Vector3.Right, elevationInput);
                break;
            case RotationControlMode.HeadAndBody:
                    
                Head.RotateObjectLocal(Vector3.Up, azimuthInput);
                Head.RotateObjectLocal(Vector3.Right, elevationInput);
                break;
            case RotationControlMode.Locked:
                break;
        }

    }
    private Vector3 ProcessKeys(double delta)
    {
        var inputRoll = Input.GetAxis("roll_left", "roll_right");
        Head.RotateObjectLocal(Vector3.Forward, Mathf.DegToRad(inputRoll * RollSensitivity * (float)delta));

        if (Input.IsActionJustPressed("interactions_use_or_interact")) Use();
        if (Input.IsActionJustPressed("toggle_dampeners")) Dampeners = !Dampeners;
        if (Input.IsActionJustPressed("toggle_jetpack"))
        {
            _freeHeadMovement = new Basis(Body.Quaternion.Inverse() * Head.Quaternion);
            Jetpack = !Jetpack;
        }

        
        var inputLeftRight = Input.GetAxis("move_left", "move_right");
        var inputUpDown = Input.GetAxis("move_down", "move_up");
        var inputForwardBackward = Input.GetAxis("move_forward", "move_backward");
        return new Vector3(inputLeftRight, inputUpDown, inputForwardBackward);
    }
    #endregion
        
    #region NonTranslatingMovement
    private void HeadMovement()
    {
        // Setting head position
        Head.Position = Body.Position + Body.Transform.Basis * _headPosition.Position;
        
        // Clamping head angle
        Vector3 angles = _freeHeadMovement.GetEuler();
        angles.X = Math.Clamp(angles.X, MinPitch, MaxPitch);
        angles.Y = Math.Clamp(angles.Y, MinYaw, MaxYaw);
        angles.Z = Math.Clamp(angles.Z, MinRoll, MaxRoll);
        _freeHeadMovement = Basis.FromEuler(angles);
        
        
    }
    private void BodyRotation()
    {
        // Rotating body
        switch (_rotationMode)
        {
            case RotationControlMode.HeadOnly:
                break;
            case RotationControlMode.HeadAndBody:
                
                var desiredAngularChangeY = -((Head.Basis * Vector3.Forward) * Body.Basis).X;
                var desiredAngularChangeX = ((Head.Basis * Vector3.Forward) * Body.Basis).Y;
                var desiredAngularChangeZ = -((Head.Basis * Vector3.Up) * Body.Basis).X;
                Body.AngularVelocity = Body.Transform.Basis * new Vector3(desiredAngularChangeX, desiredAngularChangeY, desiredAngularChangeZ) *
                                       10.0f;
                break;
            case RotationControlMode.Locked:
                break;
        }
    }
    #endregion
        
    #region Jetpack
    private void JetpackProcess(Vector3 input, double delta)
    {

        if (Dampeners)
        {
            // TODO: Make this relative to another body
            var dampenVector = ((Head.Basis * input).Normalized() * 2 - Body.LinearVelocity.Normalized()).Normalized();
            if (Math.Abs(input.X) + Math.Abs(input.Y) + Math.Abs(input.Z) == 0) dampenVector = -Body.LinearVelocity.Normalized();

            var acceleration = dampenVector * Acceleration;
            acceleration -= _gravity;
            double accelerationLength = acceleration.Length();
            if (accelerationLength > 25)
            {
                acceleration /= accelerationLength;
                acceleration *= 25;
            };
            Body.LinearVelocity += acceleration * delta;
            
        }
        else
        {
            Body.LinearVelocity += Head.Basis * input.Normalized() * Acceleration * delta;
        }
        
    }
    private void NoJetpackProcess(double delta)
    {
        Head.Basis = Body.Basis * _freeHeadMovement;
    }
    #endregion
        
    #region Interaction
    private Dictionary InteractRay()
    {
        var spaceState = Body.GetWorld3D().DirectSpaceState;
        var query = PhysicsRayQueryParameters3D.Create(Head.GlobalPosition, Head.GlobalPosition - Head.GlobalBasis * _useDistance, 4);
        var result = spaceState.IntersectRay(query);
        return result;
    }
    private void Use()
    {
        var result = InteractRay();
        
        if (!result.TryGetValue("collider", out var value)) return;
        
        if ((Node3D)value is InteractableObject interactor)
        {
            interactor.Interacted(this, InteractableObject.InteractionType.UseAction);
        }
    }

    private void HighlightObjectsUnderCrosshair()
    {
        var result = InteractRay();

        if (!result.TryGetValue("collider", out var value)) return;
        
        if ((Node3D)value is InteractableObject interactor)
        {
            interactor.Interacted(this, InteractableObject.InteractionType.LookedAt);
        }
    }
    #endregion

    public void MountToCockpit(CockpitBehavior cockpitBehavior)
    {
        _mountedCockpit = cockpitBehavior;
        Body.CollisionLayer = 0;
        Body.CollisionMask = 0;
        Body.Position = _mountedCockpit.PhysicalCockpit.GlobalPosition;
        Body.LinearVelocity = Vector3.Zero;
        Head.GlobalRotation = _mountedCockpit.PhysicalCockpit.GlobalRotation;
        _rotationMode = RotationControlMode.Locked;
    }
}