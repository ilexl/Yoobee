using Godot;
using Godot.Collections;

namespace ArchitectsInVoid.Audio;

public class FmodBank : FmodGdAPI
{
    public FmodBank(Variant ev) : base(ev)
    {
    }
    
    #region Methods
    public int GetVcaCount()
    {
        return (int)FmodCall("get_vca_count");
    }

    public int GetBusCount()
    {
        return (int)FmodCall("get_bus_count");
    }
    public Array GetBusList()
    {
        return (Array)FmodCall("get_bus_list");
    }
    public Array GetDescriptionList()
    {
        return (Array)FmodCall("get_description_list");
    }
    public int GetEventDescriptionCount()
    {
        return (int)FmodCall("get_event_description_count");
    }
    public string GetGodotResPath()
    {
        return (string)FmodCall("get_godot_res_path");
    }
    public string GetGuid()
    {
        return (string)FmodCall("get_guid");
    }
    public int GetLoadingState()
    {
        return (int)FmodCall("get_loading_state");
    }
    public string GetPath()
    {
        return (string)FmodCall("get_path");
    }
    public int GetStringCount()
    {
        return (int)FmodCall("get_string_count");
    }
    public Array GetVcaList()
    {
        return (Array)FmodCall("get_vca_list");
    }
    public bool IsValid()
    {
        return (bool)FmodCall("is_valid");
    }
    #endregion
}