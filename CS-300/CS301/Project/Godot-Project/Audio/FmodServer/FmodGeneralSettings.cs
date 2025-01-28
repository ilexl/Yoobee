using Godot;

namespace ArchitectsInVoid.Audio;

public class FmodGeneralSettings : FmodGdAPI
{
    public FmodGeneralSettings(Variant ev) : base(ev)
    {
    }
    
    #region Properties
    public string BanksPath
    {
        get => (string)GetProperty("banks_path");
        set => SetProperty("banks_path", value);
    }
    public int ChannelCount
    {
        get => (int)GetProperty("channel_count");
        set => SetProperty("banks_path", value);
    }
    public int DefaultListenerCount
    {
        get => (int)GetProperty("default_listener_count");
        set => SetProperty("default_listener_count", value);
    }
    public bool IsLiveUpdateEnabled
    {
        get => (bool)GetProperty("is_live_update_enabled");
        set => SetProperty("is_live_update_enabled", value);
    }
    public bool IsMemoryTrackingEnabled
    {
        get => (bool)GetProperty("is_memory_tracking_enabled");
        set => SetProperty("is_memory_tracking_enabled", value);
    }
    public bool ShouldLoadByName
    {
        get => (bool)GetProperty("should_load_by_name");
        set => SetProperty("should_load_by_name", value);
    }
    #endregion
}