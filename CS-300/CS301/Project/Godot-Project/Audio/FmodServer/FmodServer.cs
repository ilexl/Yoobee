using System;
using Godot;
using Godot.Collections;
using Array = Godot.Collections.Array;

namespace ArchitectsInVoid.Audio;

/// <summary>
/// A GDscript to C# wrapper for FMOD bindings
/// </summary>
public static class FmodServer
{
    #region Config
    private const string WrapperPath = "res://Audio/FmodServer/fmod_server_wrapper.gd";
    private const string ReceiveFunction = "rf";
    private const string ReceiveArgument = "rx";
    private const string FinalizeCall = "fc";
    #endregion
    
    #region InstancingWrapper
    
    private static Script _wrapper;
    private static Script Wrapper => _wrapper ??= (Script)GD.Load(WrapperPath);

    #endregion
    #region FunctionCallWrapper
    private static Variant FmodCall(string functionName, params Variant[] args)
    {
        Wrapper.Call(ReceiveFunction, functionName);
        foreach (var arg in args)
        {
            Wrapper.Call(ReceiveArgument, arg);
        }
        return Wrapper.Call(FinalizeCall);
    }
    #endregion


    public static void AddListener(int index, Variant gameObject)
    {
        FmodCall("add_listener", index, gameObject);
    }

    public static bool BanksStillLoading()
    {
        return (bool)FmodCall("banks_still_loading");
    }

    public static bool CheckBusGuid(string guid)
    {
        return (bool)FmodCall("check_bus_guid", guid);
    }
    public static bool CheckBusPath(string busPath)
    {
        return (bool)FmodCall("check_bus_path", busPath);
    }

    public static bool CheckEventGuid(string guid)
    {
        return (bool)FmodCall("check_event_guid", guid);
    }

    public static bool CheckEventPath(string eventPath)
    {
        return (bool)FmodCall("check_event_path", eventPath);
    }

    public static bool CheckVcaGuid(string guid)
    {
        return (bool)FmodCall("check_vca_guid", guid);
    }

    public static bool CheckVcaPath(string vcaPath)
    {
        return (bool)FmodCall("check_vca_path", vcaPath);
    }
    public static FmodEvent CreateEventInstance(string eventPath)
    {
        var gdEvent = FmodCall("create_event_instance", eventPath);
        return new FmodEvent(gdEvent);
    }

    public static FmodEvent CreateEventInstanceFromDescription(FmodEventDescription description)
    {
        var gdEvent = FmodCall("create_event_instance_from_description", description.Object);
        return new FmodEvent(gdEvent);
    }

    public static FmodEvent CreateEventInstanceWithGuid(string guid)
    {
        var gdEvent = FmodCall("create_event_instance_with_guid", guid);
        return new FmodEvent(gdEvent);
    }

    public static FmodSound CreateSoundInstance(string path)
    {
        var gdEvent = FmodCall("create_sound_instance", path);
        return new FmodSound(gdEvent);
    }

    public static Array GetAllBanks()
    {
        return (Array)FmodCall("get_all_banks");
    }

    public static Array GetAllBuses()
    {
        return (Array)FmodCall("get_all_buses");
    }

    public static Array GetAllEventDescriptions()
    {
        return (Array)FmodCall("get_all_event_descriptions");
    }

    public static Array GetAllVca()
    {
        return (Array)FmodCall("get_all_vca");
    }

    public static Array GetAvailableDrivers()
    {
        return (Array)FmodCall("get_available_drivers");
    }

    public static FmodBus GetBus(string busPath)
    {
        var gdBus = FmodCall("get_bus", busPath);
        return new FmodBus(gdBus);
    }

    public static FmodBus GetBusFromGuid(string guid)
    {
        var gdBus = FmodCall("get_bus_from_guid", guid);
        return new FmodBus(gdBus);
    }

    public static int GetDriver()
    {
        return (int)FmodCall("get_driver");
    }

    public static FmodEventDescription GetEvent(string eventPath)
    {
        var gdEventDescription = FmodCall("get_event", eventPath);
        return new FmodEventDescription(gdEventDescription);
    }

    public static FmodEventDescription GetEventFromGuid(string guid)
    {
        var gdEventDescription = FmodCall("get_event_from_guid", guid);
        return new FmodEventDescription(gdEventDescription);
    }

    public static float GetGlobalParameterById(int id)
    {
        return (int)FmodCall("get_global_parameter_by_id", id);
    }

    public static float GetGlobalParameterByName(string name)
    {
        return (float)FmodCall("get_global_parameter_by_name", name);
    }

    public static Dictionary GetGlobalParameterDescById(int id)
    {
        return (Dictionary)FmodCall("get_global_parameter_desc_by_id", id);
    }

    public static Dictionary GetGlobalParameterDescByName(string name)
    {
        return (Dictionary)FmodCall("get_global_parameter_desc_by_id", name);
    }

    public static int GetGlobalParameterDescCount()
    {
        return (int)FmodCall("get_global_parameter_desc_count");
    }

    public static Array GetGlobalParameterDescList()
    {
        return (Array)FmodCall("get_global_parameter_desc_list");
    }

    public static Vector2 GetListener2dVelocity(int index)
    {
        return (Vector2)FmodCall("get_listener_2d_velocity", index);
    }

    public static Vector3 GetListener3DVelocity(int index)
    {
        return (Vector3)FmodCall("get_listener_3d_velocity", index);
    }

    public static bool GetListenerLock(int index)
    {
        return (bool)FmodCall("get_listener_lock", index);
    }

    public static int GetListenerNumber()
    {
        return (int)FmodCall("get_listener_number");
    }

    public static Transform2D GetListenerTransform2D(int index)
    {
        return (Transform2D)FmodCall("get_listener_transform_2d", index);
    }

    public static Transform3D GetListenerTransform3D(int index)
    {
        return (Transform3D)FmodCall("get_listener_transform_3d", index);
    }

    public static float GetListenerWeight(int index)
    {
        return (float)FmodCall("get_listener_weight", index);
    }

    public static Object GetObjectAttachedToListener(int index)
    {
        return FmodCall("get_object_attached_to_listener", index);
    }

    public static FmodPerformanceData GetPerformanceData()
    {
        var gdPerformanceData = FmodCall("get_performance_data");
        return new FmodPerformanceData(gdPerformanceData);
    }

    public static int GetSystemDspBufferLength()
    {
        return (int)FmodCall("get_system_dsp_buffer_length");
    }

    public static FmodDspSettings GetSystemDspBufferSettings()
    {
        var gdDspSettings = FmodCall("get_system_dsp_buffer_settings");
        return new FmodDspSettings(gdDspSettings);
    }

    public static int GetSystemDspNumBuffers()
    {
        return (int)FmodCall("get_system_dsp_num_buffers");
    }

    public static FmodVCA GetVca(string vcaPath)
    {
        var gdVca = FmodCall("get_vca", vcaPath);
        return new FmodVCA(gdVca);
    }

    public static FmodVCA GetVcaFromGuid(string guid)
    {
        var gdVca = FmodCall("get_vca_from_guid", guid);
        return new FmodVCA(gdVca);
    }

    public static void Init(FmodGeneralSettings settings)
    {
        FmodCall("init", settings.Object);
    }

    public static FmodBank LoadBank(string pathToBank, int flags)
    {
        var gdBank = FmodCall("load_bank", pathToBank, flags);
        return new FmodBank(gdBank);
    }

    public static FmodFile LoadFileAsMusic(string path)
    {
        var gdFile = FmodCall("load_file_as_music", path);
        return new FmodFile(gdFile);
    }

    public static FmodFile LoadFileAsSound(string path)
    {
        var gdFile = FmodCall("load_file_as_sound", path);
        return new FmodFile(gdFile);
    }

    public static void MuteAllEvents()
    {
        FmodCall("mute_all_events");
    }

    public static void PauseAllEvents()
    {
        FmodCall("pause_all_events");
    }

    public static void PlayOneShot(string eventName)
    {
        FmodCall("play_one_shot", eventName);
    }

    public static void PlayOneShotAttached(string eventName, Node gameObject)
    {
        FmodCall("play_one_shot_attached", eventName, gameObject);
    }

    public static void PlayOneShotAttachedWithParams(string eventName, Node gameObject, Dictionary parameters)
    {
        FmodCall("play_one_shot_attached_with_params", eventName, gameObject, parameters);
    }

    public static void PlayOneShotUsingEventDescription(FmodEventDescription description)
    {
        FmodCall("play_one_shot_using_event_description", description.Object);
    }
    public static void PlayOneShotUsingEventDescriptionAttached(FmodEventDescription description, Node gameObject)
    {
        FmodCall("play_one_shot_using_event_description_attached", description.Object, gameObject);
    }
    
    public static void PlayOneShotUsingEventDescriptionWithParams(FmodEventDescription description, Dictionary parameters)
    {
        FmodCall("play_one_shot_using_event_description_with_params", description.Object, parameters);
    }
    public static void PlayOneShotUsingEventDescriptionAttachedWithParams(FmodEventDescription description, Node gameObject, Dictionary parameters)
    {
        FmodCall("play_one_shot_using_event_description_attached_with_params", description.Object, gameObject,
            parameters);
    }

    public static void PlayOneShotUsingGuid(string guid)
    {
        FmodCall("play_one_shot_using_guid", guid);
    }

    public static void PlayOneShotUsingGuidAttached(string guid, Node gameObject)
    {
        FmodCall("play_one_shot_using_guid_attached", guid, gameObject);
    }

    public static void PlayOneShotUsingGuidWithParams(string guid, Dictionary parameters)
    {
        FmodCall("play_one_shot_using_guid_with_params", guid, parameters);
    }

    public static void PlayOneShotUsingGuidAttachedWithParams(string guid, Node gameObject, Dictionary parameters)
    {
        FmodCall("play_one_shot_using_guid_attached_with_params", guid, gameObject, parameters);
    }

    public static void RemoveListener(int index)
    {
        FmodCall("remove_listener", index);
    }

    public static void SetDriver(int id)
    {
        FmodCall("set_driver", id);
    }

    public static void SetGlobalParameterById(int parameterId, float value)
    {
        FmodCall("set_global_parameter_by_id", parameterId, value);
    }

    public static void SetGlobalParameterByIdWithLabel(int parameterId, string label)
    {
        FmodCall("set_global_parameter_by_id_with_label", parameterId, label);
    }

    public static void SetGlobalParameterByName(string parameterName, float value)
    {
        FmodCall("set_global_parameter_by_name", parameterName, value);
    }

    public static void SetGlobalParameterByNameWithLabel(string parameterName, string label)
    {
        FmodCall("set_global_parameter_by_name_with_label", parameterName, label);
    }

    public static void SetListenerLock(int index, bool isLocked)
    {
        FmodCall("set_listener_lock", index, isLocked);
    }

    public static void SetListenerNumber(int listenerNumber)
    {
        FmodCall("set_listener_number", listenerNumber);
    }

    public static void SetListenerTransform2D(int index, Transform2D transform)
    {
        FmodCall("set_listener_transform_2d", index, transform);
    }

    public static void SetListenerTransform3D(int index, Transform3D transform)
    {
        FmodCall("set_listener_transform_3d", index, transform);
    }

    public static void SetListenerWeight(int index, float weight)
    {
        FmodCall("set_listener_weight", index, weight);
    }

    public static void SetSoftwareFormat(FmodSoftwareFormatSettings pSettings)
    {
        FmodCall("set_software_format", pSettings.Object);
    }

    public static void SetSound3DSettings(FmodSound3DSettings pSettings)
    {
        FmodCall("set_sound_3d_settings", pSettings.Object);
    }

    public static void SetSystemDspBufferSize(FmodDspSettings dspSettings)
    {
        FmodCall("set_system_dsp_buffer_size", dspSettings.Object);
    }

    public static void Shutdown()
    {
        FmodCall("shutdown");
    }

    public static void UnloadBank(string pathToBank)
    {
        FmodCall("unload_bank", pathToBank);
    }

    public static void UnloadFile(string pathToFile)
    {
        FmodCall("unload_file", pathToFile);
    }

    public static void UnmuteAllEvents()
    {
        FmodCall("unmute_all_events");
    }

    public static void UnpauseAllEvents()
    {
        FmodCall("unpause_all_events");
    }

    public static void Update()
    {
        FmodCall("update");
    }

    public static void WaitForAllLoads()
    {
        FmodCall("wait_for_all_loads");
    }
}