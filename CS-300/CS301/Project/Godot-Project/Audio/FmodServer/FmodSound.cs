using Godot;

namespace ArchitectsInVoid.Audio;

public class FmodSound : FmodGdAPI
{
    public FmodSound(Variant ev) : base(ev)
    {
    }
    
    #region Properties
    public float Pitch
    {
        get => (int)GetProperty("pitch");
        set => SetProperty("pitch", value);
    }
    public float Volume
    {
        get => (int)GetProperty("volume");
        set => SetProperty("volume", value);
    }
    #endregion
    
    #region Methods
    public bool IsPlaying()
    {
        return (bool)FmodCall("is_playing");
    }

    public bool IsValid()
    {
        return (bool)FmodCall("is_valid");
    }

    public void Play()
    {
        FmodCall("play");
    }

    public void Release()
    {
        FmodCall("release");
    }

    public void SetPaused(bool paused)
    {
        FmodCall("set_paused", paused);
    }

    public void Stop()
    {
        FmodCall("stop");
    }
    #endregion
}
