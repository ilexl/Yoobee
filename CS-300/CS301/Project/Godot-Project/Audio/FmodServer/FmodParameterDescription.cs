using Godot;

namespace ArchitectsInVoid.Audio;

public class FmodParameterDescription : FmodGdAPI
{
    public FmodParameterDescription(Variant ev) : base(ev)
    {
    }
    #region Methods
    public float GetDefaultValue()
    {
        return (float)FmodCall("get_default_value");
    }

    public int GetId()
    {
        return (int)FmodCall("get_id");
    }

    public float GetMaximum()
    {
        return (float)FmodCall("get_maximum");
    }

    public float GetMinimum()
    {
        return (float)FmodCall("get_minimum");
    }

    public string GetName()
    {
        return (string)FmodCall("get_name");
    }

    public bool IsAutomatic()
    {
        return (bool)FmodCall("is_automatic");
    }

    public bool IsDiscrete()
    {
        return (bool)FmodCall("is_discrete");
    }

    public bool IsGlobal()
    {
        return (bool)FmodCall("is_global");
    }

    public bool IsLabeled()
    {
        return (bool)FmodCall("is_labeled");
    }

    public bool IsReadOnly()
    {
        return (bool)FmodCall("is_read_only");
    }
    
    #endregion
}