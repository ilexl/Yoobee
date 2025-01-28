using Godot;
using Godot.Collections;
using Godot.NativeInterop;

namespace ArchitectsInVoid.Audio;

public class FmodEventDescription : FmodGdAPI
{
    public FmodEventDescription(Variant ev) : base(ev)
    {
    }
    #region Methods
    public string GetGuid()
    {
        return (string)FmodCall("get_guid");
    }

    public int GetInstanceCount()
    {
        return (int)FmodCall("get_instance_count");
    }

    public Array GetInstanceList()
    {
        return (Array)FmodCall("get_instance_list");
    }

    public int GetLength()
    {
        return (int)FmodCall("get_length");
    }

    public FmodParameterDescription GetParameterById(int eventPathIdPair)
    {
        var gdParamDesc = FmodCall("get_parameter_by_id", eventPathIdPair);
        return new FmodParameterDescription(gdParamDesc);
    }

    public FmodParameterDescription GetParameterByIndex(int index)
    {
        var gdParamDesc = FmodCall("get_parameter_by_index", index);
        return new FmodParameterDescription(gdParamDesc);
    }

    public FmodParameterDescription GetParameterByName(string name)
    {
        var gdParamDesc = FmodCall("get_parameter_by_name", name);
        return new FmodParameterDescription(gdParamDesc);
    }

    public int GetParameterCount()
    {
        return (int)FmodCall("get_parameter_count");
    }

    public string GetParameterLabelById(int id, int index)
    {
        return (string)FmodCall("get_parameter_label_by_id", id, index);
    }
    public string GetParameterLabelByIndex(int index, int id)
    {
        return (string)FmodCall("get_parameter_label_by_index", index, id);
    }
    public string GetParameterLabelByName(string name, int index)
    {
        return (string)FmodCall("get_parameter_label_by_name", name, index);
    }

    public Variant GetParameterLabelsById(int id)
    {
        return FmodCall("get_parameter_labels_by_id", id);
    }

    public Variant GetParameterLabelsByIndex(int index)
    {
        return FmodCall("get_parameter_labels_by_index", index);
    }

    public Variant GetParameterLabelsByName(string name)
    {
        return FmodCall("get_parameter_labels_by_name", name);
    }

    public Array GetParameters()
    {
        return (Array)FmodCall("get_parameters");
    }

    public string GetPath()
    {
        return (string)FmodCall("get_path");
    }

    public int GetSampleLoadingState()
    {
        return (int)FmodCall("get_sample_loading_state");
    }

    public float GetSoundSize()
    {
        return (float)FmodCall("get_sound_size");
    }

    public Dictionary GetUserProperty(string name)
    {
        return (Dictionary)FmodCall("get_user_property", name);
    }

    public int GetUserPropertyCount()
    {
        return (int)FmodCall("get_user_property_count");
    }

    public bool HasSustainPoint()
    {
        return (bool)FmodCall("has_sustain_point");
    }

    public bool Is3D()
    {
        return (bool)FmodCall("is_3d");
    }

    public bool IsOneShot()
    {
        return (bool)FmodCall("is_one_shot");
    }

    public bool IsSnapshot()
    {
        return (bool)FmodCall("is_snapshot");
    }

    public bool IsStream()
    {
        return (bool)FmodCall("is_stream");
    }

    public bool IsValid()
    {
        return (bool)FmodCall("is_valid");
    }

    public void LoadSampleData()
    {
        FmodCall("load_sample_data");
    }

    public void ReleaseAllInstances()
    {
        FmodCall("release_all_instances");
    }

    public void UnloadSampleData()
    {
        FmodCall("unload_sample_data");
    }

    public Dictionary UserPropertyByIndex(int index)
    {
        return (Dictionary)FmodCall("user_property_by_index", index);
    }
    #endregion
}