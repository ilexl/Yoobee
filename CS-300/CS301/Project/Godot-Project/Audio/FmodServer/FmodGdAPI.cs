using Godot;

namespace ArchitectsInVoid.Audio;

public abstract class FmodGdAPI
{
    #region Config
    private const string WrapperPath = "res://Audio/FmodServer/generic_fmod_wrapper.gd";
    private const string SetEvent = "re";
    private const string SetFunction = "rf";
    private const string SendArgument = "rx";
    private const string CallFunction = "fc";
    private const string WrapperGetProperty = "gp";
    private const string WrapperSetProperty = "sp";
    #endregion
    #region Object Initialization
    public Variant Object;
    public FmodGdAPI(Variant @object)
    {
        Object = @object;
    }
    #endregion
    #region Wrapper singleton
    private static Script _wrapper;
    private static Script Wrapper => _wrapper ??= (Script)GD.Load(WrapperPath);
    #endregion
    #region Wrapper interfacers
    protected Variant FmodCall(string functionName, params Variant[] args)
    {
        Wrapper.Call(SetEvent, Object);
        Wrapper.Call(SetFunction, functionName);
        foreach (var arg in args)
        {
            Wrapper.Call(SendArgument, arg);
        }

        return Wrapper.Call(CallFunction);
    }

    protected Variant GetProperty(string propertyName)
    {
        Wrapper.Call(SetEvent, Object);
        return Wrapper.Call(WrapperGetProperty, propertyName);
    }

    protected void SetProperty(string propertyName, Variant value)
    {
        Wrapper.Call(SetEvent, Object);
        Wrapper.Call(WrapperSetProperty, propertyName, value);
    }
    #endregion
}