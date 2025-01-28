using Godot;

namespace ArchitectsInVoid.Audio;

public class FmodBus : FmodGdAPI
{
    public FmodBus(Variant ev) : base(ev)
    {
    }
    
    #region Properties
    public bool Mute
    {
        get => (bool)GetProperty("mute");
        set => SetProperty("mute", value);
    }
    public bool Paused
    {
        get => (bool)GetProperty("paused");
        set => SetProperty("paused", value);
    }
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

    public void StopAllEvents()
    {
        FmodCall("stop_all_events");
    }
    #endregion
}