using Godot;

namespace ArchitectsInVoid.Audio;

public class FmodVCA : FmodGdAPI
{
    public FmodVCA(Variant ev) : base(ev)
    {
        
    }
    
    #region Properties
    public float Volume
    {
        get => (float)GetProperty("volume");
        set => SetProperty("volume", value);
    }
    #endregion
    #region Methods
    public string GetGuid()
    {
        return (string)FmodCall("get_guid");
    }

    public string GetPath()
    {
        return (string)FmodCall("get_path");
    }

    public bool IsValid()
    {
        return (bool)FmodCall("is_valid");
    }
    #endregion
}