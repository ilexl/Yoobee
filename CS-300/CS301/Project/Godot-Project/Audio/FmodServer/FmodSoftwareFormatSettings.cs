using Godot;

namespace ArchitectsInVoid.Audio;

public class FmodSoftwareFormatSettings : FmodGdAPI
{
    private FmodSoftwareFormatSettings(Variant ev) : base(ev)
    {
    }

    #region Properties
    public int RawSpeakersCount
    {
        get => (int)GetProperty("raw_speakers_count");
        set => SetProperty("raw_speakers_count", value);
    }
    public int SampleRate
    {
        get => (int)GetProperty("sample_rate");
        set => SetProperty("sample_rate", value);
    }
    public int SampleMode
    {
        get => (int)GetProperty("sample_mode");
        set => SetProperty("sample_mode", value);
    }
    #endregion
}