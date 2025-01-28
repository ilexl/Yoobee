using Godot;

namespace ArchitectsInVoid.Audio;

public class FmodPerformanceData : FmodGdAPI
{
    public FmodPerformanceData(Variant ev) : base(ev)
    {
    }
    
    #region Properties
    public float Convolution1
    {
        get => (float)GetProperty("convolution1");
        set => SetProperty("convolution1", value);
    }
    public float Convolution2
    {
        get => (float)GetProperty("convolution2");
        set => SetProperty("convolution2", value);
    }
    public int CurrentlyAllocated
    {
        get => (int)GetProperty("currently_allocated");
        set => SetProperty("currently_allocated", value);
    }
    public float Dsp
    {
        get => (float)GetProperty("dsp");
        set => SetProperty("dsp", value);
    }
    public float Geometry
    {
        get => (float)GetProperty("geometry");
        set => SetProperty("geometry", value);
    }
    public int MaxAllocated
    {
        get => (int)GetProperty("max_allocated");
        set => SetProperty("max_allocated", value);
    }

    public int OtherBytesRead
    {
        get => (int)GetProperty("other_bytes_read");
        set => SetProperty("other_bytes_read", value);
    }
    public int SampleBytesRead
    {
        get => (int)GetProperty("sample_bytes_read");
        set => SetProperty("sample_bytes_read", value);
    }
    public float Stream
    {
        get => (float)GetProperty("stream");
        set => SetProperty("stream", value);
    }
    public int StreamBytesRead
    {
        get => (int)GetProperty("stream_bytes_read");
        set => SetProperty("stream_bytes_read", value);
    }
    public float Studio
    {
        get => (float)GetProperty("studio");
        set => SetProperty("studio", value);
    }
    public float Update
    {
        get => (float)GetProperty("update");
        set => SetProperty("update", value);
    }
    #endregion
}