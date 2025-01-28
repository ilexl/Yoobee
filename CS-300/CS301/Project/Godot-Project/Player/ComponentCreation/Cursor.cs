using Godot;
using System.Collections.Generic;
using ArchitectsInVoid.Debug;

namespace ArchitectsInVoid.Player.ComponentCreation;

public partial class Cursor : Node3D
{
	// No reason to change this, just magic numbers are bad
	private const int EdgeCount = 4;
	
	
	// Meshes
	private Dictionary<char, List<MeshInstance3D>> _edges;
	private MeshInstance3D _startCorner;
	private MeshInstance3D _endCorner;
	
	// Text
	private Label3D _label;

	private bool _loaded = false;
	public override void _Ready()
	{
		if (Engine.IsEditorHint()) { return; } // do NOT run when not in game
		// Initialize edge meshes
		_edges = new Dictionary<char, List<MeshInstance3D>>()
		{
			{ 'X', new List<MeshInstance3D>(EdgeCount) },
			{ 'Y', new List<MeshInstance3D>(EdgeCount) },
			{ 'Z', new List<MeshInstance3D>(EdgeCount) }
		};
		foreach (var edgeGroup in _edges)
		{
			for (int i = 0; i < EdgeCount; i++)
			{
				var meshInstance = new MeshInstance3D();
				meshInstance.Mesh = new BoxMesh();
				meshInstance.Scale = new Vector3(0.1f, 0.1f, 0.1f);
				AddChild(meshInstance);
				edgeGroup.Value.Add(meshInstance);
			}
		}
		// Initialize corner meshes
		_startCorner = new MeshInstance3D();
		_startCorner.Mesh = new SphereMesh();
		_startCorner.Scale = new Vector3(0.1f, 0.1f, 0.1f);
		AddChild(_startCorner);
		_endCorner = new MeshInstance3D();
		_endCorner.Mesh = new SphereMesh();
		_endCorner.Scale = new Vector3(0.1f, 0.1f, 0.1f);
		AddChild(_endCorner);
		
		// assign label
		_label = GetNode<Label3D>("Object Text");
		_label.Visible = false;
		_loaded = true;
	}

	/// <summary>
	/// Sets the text of the label
	/// </summary>
	/// <param name="scene"></param>
	public void SetLabelName(PackedScene scene)
	{
		if (scene == null)
		{
			_label.Text = "No component selected!";
			return;
		}

		_label.Text = scene.ResourcePath;

	}
	/// <summary>
	/// Sets the scale of the whole cursor while keeping the width of the wireframe constant
	/// </summary>
	/// <param name="scale"></param>
	public new void SetScale(Vector3 scale)
	{
		if (!IsNodeReady()) return;
		var xEdges = _edges['X'];
		var yEdges = _edges['Y'];
		var zEdges = _edges['Z'];

		ScaleEdgeSet(xEdges, new Vector3(scale.X, 0.1, 0.1), new Vector3(0, scale.Y, scale.Z));
		ScaleEdgeSet(yEdges, new Vector3(0.1, scale.Y, 0.1), new Vector3(scale.X, 0, scale.Z));
		ScaleEdgeSet(zEdges, new Vector3(0.1, 0.1, scale.Z), new Vector3(scale.X, scale.Y, 0));
	}
	/// <summary>
	/// Scales a given edge set by a position and scale.
	/// Expects 2/3 values in scale to be 0.1 with the remaining one being the actual scale.
	/// Expects the corresponding value in pos to be 0 and the other two to be scales.
	/// EG, scale = Vector3(10, 0.1, 0.1), pos = Vector3(0, 10, 10) 
	/// </summary>
	/// <param name="edges"></param>
	/// <param name="scale"></param>
	/// <param name="pos"></param>
	private void ScaleEdgeSet(List<MeshInstance3D> edges, Vector3 scale, Vector3 pos)
	{
		for (int i = 0; i < 4; i++)
		{
			edges[i].Scale = scale;
		}

		
		Vector3 max = pos / 2;
		Vector3 min = -max;

		// Sets the position of each edge mesh such that the points would resemble a square when viewed from any cardinal direction
		edges[0].Position = max;
		edges[1].Position = new Vector3(min.X, max.Y, min.Z);
		edges[2].Position = new Vector3(max.X, min.Y, min.Z);
		edges[3].Position = new Vector3(min.X, min.Y, max.Z);
	}
	/// <summary>
	/// Takes the rotation of the cursor and sets the position and scale accordingly to meet the desired position
	/// </summary>
	/// <param name="start"></param>
	/// <param name="end"></param>
	public void SetCornerPositions(Vector3 start, Vector3 end)
	{
		if (!IsNodeReady()) return;
		// WORLD coords
		Position = start.Lerp(end, 0.5);;

		Vector3 localStart = start * Transform; // Note to self: Vector * Transform converts to local space while Transform * Vector converts to world space
		Vector3 localEnd = end * Transform;
		_startCorner.Position = localStart;
		_endCorner.Position = localEnd;
		//SetScale(localStart - localEnd);
		DebugDraw.Box(start, end, Transform.Basis);
	}

	public Vector3 GetComponentScale()
	{
		GD.Print(_startCorner.Position - _endCorner.Position);
		return _startCorner.Position - _endCorner.Position;
	}
	/// <summary>
	/// Sets the label visibility
	/// </summary>
	/// <param name="visible"></param>
	public void SetLabelVisible(bool visible)
	{
		_label.Visible = visible;
	}

	StandardMaterial3D _material = new()
	{
		ShadingMode = BaseMaterial3D.ShadingModeEnum.Unshaded,
		VertexColorUseAsAlbedo = true,
		Transparency = BaseMaterial3D.TransparencyEnum.Alpha,
		CullMode = BaseMaterial3D.CullModeEnum.Disabled,
	};
	public void SetColor(Color color)
	{
		_material.AlbedoColor = color;
		_startCorner.MaterialOverride = _material;
		_endCorner.MaterialOverride = _material;
	}


}