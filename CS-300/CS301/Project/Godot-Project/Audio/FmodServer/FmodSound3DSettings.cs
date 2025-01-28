using Godot;

namespace ArchitectsInVoid.Audio;

public class FmodSound3DSettings : FmodGdAPI
{
    public FmodSound3DSettings(Variant ev) : base(ev)
    {
    }
    
    #region Properties
    public float DistanceFactor
    {
        get => (float)GetProperty("distance_factor");
        set => SetProperty("distance_factor", value);
    }
    public float DopperScale
    {
        get => (float)GetProperty("dopper_scale");
        set => SetProperty("dopper_scale", value);
    }
    public float RolloffScale
    {
        get => (float)GetProperty("rolloff_scale");
        set => SetProperty("rolloff_scale", value);
    }
    #endregion
}