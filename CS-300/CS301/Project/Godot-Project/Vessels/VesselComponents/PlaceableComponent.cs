using System.IO;
using System.Text.Json.Serialization;
using ArchitectsInVoid.Helper;
using ArchitectsInVoid.JsonDataConversion;
using ArchitectsInVoid.WorldData;
using Godot;
using FileAccess = Godot.FileAccess;

using sf = Godot.ResourceSaver.SaverFlags;

namespace ArchitectsInVoid.VesselComponent;

public enum PlaceableComponentType
{
    FixedScale,
    DynamicScale
}

public enum PlaceableComponentResult
{
    Success,
    ErrorAddToVessel,
    ErrorCreateNewVessel,
    ErrorPositionOrScale,
}
/// <summary>
/// Base class for all objects that can be attached to vessels.
/// </summary>
[Tool]
public partial class PlaceableComponent : CollisionShape3D
{
    public virtual PlaceableComponentType ComponentType { get; set; }
    
    [Export] protected double Density;

    [Export] public Image _Thumbnail;

    [Export] private string _thumbnailName = "thumb.res";

    public virtual Component Type { get; set; } = Component.None;

    public Data SaveData { get; set; }

    public Vessel Vessel;
    private readonly sf _flags =
        sf.ReplaceSubresourcePaths & 
        sf.Compress;
    
    public enum Component
    {
        None,
        Armour,
        Cockpit,
        Thruster
    }

    public class Data
    {
        [JsonInclude][JsonPropertyName("Type")] public Component JComponent { get; set; }
        [JsonInclude] public JVector3 Position { get; set; }
        [JsonInclude] public JVector3 Scale { get; set; }
        [JsonInclude] public JBasis Basis { get; set; }
        

        public Data()
        {
            JComponent = Component.None;
            Position = null;
            Scale = null;
            Basis = null;
        }

        public Data(Component type, JVector3 position, JVector3 scale, JBasis basis)
        {
            JComponent = type;
            Position = position;
            Scale = scale;
            Basis = basis;
        }
    }

    public override void _Ready()
    {
        SaveData = new Data(); // get data in memory
    }


    #region Component Selection Data

    public enum Category
    {
        Other,
        Armour,
        Controls,
        Movement,
        Power,
        Storage,
        Utility,
        Defense,
        Production,
        Temperature,
        Doors,
        Aesthetics
    }

    [ExportGroup("Component Selection Data")]
    [Export] public string _cmpSlcData_Title;
    [Export] public string _cmpSlcData_Desc;
    [Export] public Category _category;
    [ExportSubgroup("Extra Properties")]
    [Export] public string _cmpSlcData_infoTitle0;
    [Export] public string _cmpSlcData_infoDesc0;
    [Export] public string _cmpSlcData_infoTitle1;
    [Export] public string _cmpSlcData_infoDesc1;
    [Export] public string _cmpSlcData_infoTitle2;
    [Export] public string _cmpSlcData_infoDesc2;
    [Export] public string _cmpSlcData_infoTitle3;
    [Export] public string _cmpSlcData_infoDesc3;
    [Export] public string _cmpSlcData_infoTitle4;
    [Export] public string _cmpSlcData_infoDesc4;
    [Export] public string _cmpSlcData_infoTitle5;
    [Export] public string _cmpSlcData_infoDesc5;
    [Export] public string _cmpSlcData_infoTitle6;
    [Export] public string _cmpSlcData_infoDesc6;
    [Export] public string _cmpSlcData_infoTitle7;
    [Export] public string _cmpSlcData_infoDesc7;
    [Export] public string _cmpSlcData_infoTitle8;
    [Export] public string _cmpSlcData_infoDesc8;
    [Export] public string _cmpSlcData_infoTitle9;
    [Export] public string _cmpSlcData_infoDesc9;

    #endregion


    /***************NEW VESSEL***************/
    #region NewVessel
    // Scaled
    public virtual Vessel PlaceRVN(Vector3 position, Vector3 scale, Basis rotation)
    {
        GD.Print($"Placing new vessel with the following data: \nPosition : {position}\nScale : {scale}\nBasis : {rotation}");

        SaveData = new Data(Type, position, scale, rotation);
        Scale = scale;
        var vessel = VesselData._VesselData.CreateVessel(position);
        if (vessel == null)
        {
            GD.PushError("FAILED TO CREATE A NEW VESSEL");
            return null;
        }

        
        var vesselRB = vessel.RigidBody;
        var componentData = vessel.ComponentData;
        vesselRB.AddChild(this);
        vesselRB.Mass += Density * Scale.LengthSquared();
        vesselRB.Transform = vesselRB.Transform with { Basis = rotation };
        Vessel = vessel;
        return vessel;
    }

    // Scaled
    public virtual PlaceableComponentResult Place(Vector3 position, Vector3 scale, Basis rotation)
    {
        Scale = scale;
        return AddToNewVessel(position, rotation);
    }
    
    // Not scaled
    public virtual PlaceableComponentResult Place(Vector3 position, Basis rotation)
    {
        return AddToNewVessel(position, rotation);
    }

    protected PlaceableComponentResult AddToNewVessel(Vector3 position, Basis rotation)
    {
        GD.Print($"Adding to new vessel with the following data: \nPosition : {position}\nScale : {Scale}\nBasis : {rotation}");
        SaveData = new Data(Type, position, Scale, rotation);
        var vessel = VesselData._VesselData.CreateVessel(position);
        if (vessel == null) return PlaceableComponentResult.ErrorCreateNewVessel;
        
        
        var vesselRB = vessel.RigidBody;
        var componentData = vessel.ComponentData;
        vesselRB.AddChild(this);
        vesselRB.Mass += Density * Scale.LengthSquared();
        vesselRB.Transform = vesselRB.Transform with { Basis = rotation };
        Vessel = vessel;
        return PlaceableComponentResult.Success;
        
    }
    #endregion
    /*************EXISTING VESSEL*************/
    #region ExistingVessel
    // Scaled
    public virtual PlaceableComponentResult Place(Vector3 position, Vector3 scale, Basis rotation, Vessel vessel)
    {
        if (vessel == null)
        {
            GD.Print("Making new vessel");
            return Place(position, scale, rotation);
        }
        GD.Print("Adding to existing vessel");
        return AddToVessel(vessel, position, scale, rotation);
    }

    // Not scaled
    public virtual PlaceableComponentResult Place(Vector3 position, Vessel vessel, Basis rotation)
    {
        return AddToVessel(vessel, position, Vector3.One, rotation);
    }

    public Vessel PlaceRV(Vector3 position, Vector3 scale, Basis rotation, Vessel vessel)
    {
        GD.Print($"Placing with existing vessel with the following data: \nPosition : {position}\nScale : {scale}\nBasis : {rotation}");

        SaveData = new Data(Type, position, scale, rotation);
        GD.Print($"Exising scale is {scale}");

        var vesselRb = vessel.RigidBody;

        //var componentData = vessel.ComponentData;
        Transform = Transform with { Basis = vesselRb.Transform.Basis.Inverse() * rotation };
        vesselRb.AddChild(this);
        Position = position * vesselRb.Transform;

        Scale = scale;

        vesselRb.Mass += Density * Scale.LengthSquared();
        Vessel = vessel;
        return vessel;
    }

    protected PlaceableComponentResult AddToVessel(Vessel vessel, Vector3 position, Vector3 scale, Basis rotation)
    {
        GD.Print($"Adding to existing vessel with the following data: \nPosition : {position}\nScale : {scale}\nBasis : {rotation}");
        SaveData = new Data(Type, position, scale, rotation);
        
        //vessel.AddComponent(this);

        var vesselRb = vessel.RigidBody;

        //var componentData = vessel.ComponentData;
        Transform = Transform with { Basis = vesselRb.Transform.Basis.Inverse() * rotation };
        vesselRb.AddChild(this);
        Position = position * vesselRb.Transform;

        Scale = scale;

        vesselRb.Mass += Density * Scale.LengthSquared();
        Vessel = vessel;
        return PlaceableComponentResult.Success;
    }
    #endregion

    public override void _Process(double delta)
    {
        PackedScene scene = new PackedScene();
        // scene
        Type = Component.None;
    }


    public void RecieveThumbnail(string path, Texture2D preview, Texture2D thumb, Variant userData)
    {
        
        // Gets the actual raw image type which means it can be converted to binary
        var newThumbnail = preview.GetImage();
        // Gets the containing folder of the component scene
        path = SceneUtility.GetSceneDirectory(path)  + "/" + _thumbnailName;
        GD.Print("Creating new thumbnail at " + path);
        if (_Thumbnail != null)
        {
            _Thumbnail.ResourcePath = null;
        }
        _Thumbnail = null;
        ResourceSaver.Save(newThumbnail, path, _flags);
        newThumbnail.ResourcePath = path;
        _Thumbnail = newThumbnail;
    }


    public override void _Notification(int what)
    {
        if (!Engine.IsEditorHint()) return;

        if (what == NotificationEditorPreSave)
        {
            EditorPreSave();
        }
        else if (what == NotificationEditorPostSave)
        {
            EditorPostSave();
        }

    }

    protected virtual void EditorPreSave()
    {
        
    }

    protected virtual void EditorPostSave()
    {
        GenerateThumbnail();
    }
    
    protected virtual void GenerateThumbnail()
    {
        //scenepreviewextractor.GetPreview(SceneFilePath, this, "RecieveThumbnail", 0);
    }
}