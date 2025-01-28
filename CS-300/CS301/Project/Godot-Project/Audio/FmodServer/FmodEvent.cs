using Godot;

namespace ArchitectsInVoid.Audio;

public class FmodEvent : FmodGdAPI
{
    public FmodEvent(Variant ev) : base(ev)
    {
    }

    #region Properties
    
    /// <summary>
    /// TESTING NEEDED. Assumed to be a layer that can be used for masking sounds for certain listeners, useful for multiplayer. Or portal shenanigans.
    /// </summary>
    public int ListenerMask
    {
        get => (int)GetProperty("listener_mask");
        set => SetProperty("listener_mask", value);
    }
    /// <summary>
    /// TESTING NEEDED. Gets if event is paused. Might set it too, not ure.
    /// </summary>
    public bool Paused
    {
        get => (bool)GetProperty("paused");
        set => SetProperty("paused", value);
    }
    /// <summary>
    /// Gets or sets the master pitch on this event.
    /// </summary>
    public float Pitch
    {
        get => (float)GetProperty("pitch");
        set => SetProperty("pitch", value);
    }
    /// <summary>
    /// TESTING NEEDED. Definitely gets the position on the track in milliseconds, might set it too.
    /// </summary>
    public int Position
    {
        get => (int)GetProperty("position");
        set => SetProperty("position", value);
    }
    /// <summary>
    /// TESTING NEEDED. Gets the Transform2D of this event. Might set it too.
    /// </summary>
    public Transform2D Transform2D
    {
        get => (Transform2D)GetProperty("transform_2d");
        set => SetProperty("transform_2d", value);
    }
    /// <summary>
    /// TESTING NEEDED. Gets the Transform3D of this event. Might set it too.
    /// </summary>
    public Transform3D Transform3D
    {
        get => (Transform3D)GetProperty("transform_3d");
        set => SetProperty("transform_3d", value);
    }
    /// <summary>
    /// TESTING NEEDED. Gets or sets the master volume of this event.
    /// </summary>
    public float Volume
    {
        get => (float)GetProperty("volume");
        set => SetProperty("volume", value);
    }
    #endregion

    #region Methods
    /// <summary>
    /// TESTING NEEDED. Completely unsure.
    /// </summary>
    public void EventKeyOff()
    {
        FmodCall("event_key_off");
    }
    /// <summary>
    /// Gets the current value of a parameter by ID.
    /// </summary>
    /// <param name="id">The ID of the parameter.</param>
    /// <returns>The parameter value.</returns>
    public float GetParameterById(int id)
    {
        return (float)FmodCall("get_parameter_by_id", id);
    }
    /// <summary>
    /// Gets the current value of a parameter by name.
    /// </summary>
    /// <param name="name">The name of the parameter.</param>
    /// <returns>The parameter value.</returns>
    public float GetParameterByName(string name)
    {
        return (float)FmodCall("get_parameter_by_name", name);
    }
    /// <summary>
    /// Gets the playback state of the event. Presumedly should be casted to an enum.
    /// </summary>
    /// <returns>The playback state.</returns>
    public int GetPlaybackState()
    {
        return (int)FmodCall("get_playback_state");
    }
    /// <summary>
    /// No clue.
    /// </summary>
    /// <returns>Please test</returns>
    public string GetProgrammerCallbackSoundKey()
    {
        return (string)FmodCall("get_programmer_callback_sound_key");
    }
    /// <summary>
    /// Gets the reverb level... by index? not sure
    /// </summary>
    /// <param name="index">The index!!!</param>
    /// <returns>The reverb level?</returns>
    public float GetReverbLevel(int index)
    {
        return (float)FmodCall("get_reverb_level", index);
    }
    /// <summary>
    /// Not sure, presumedly checks if the sound is valid?
    /// </summary>
    /// <returns>true if valid</returns>
    public bool IsValid()
    {
        return (bool)FmodCall("is_valid");
    }
    /// <summary>
    /// Not sure
    /// </summary>
    /// <returns>True if virtual?</returns>
    public bool IsVirtual()
    {
        return (bool)FmodCall("is_virtual");
    }
    /// <summary>
    /// Completely destroys the sound I think?
    /// </summary>
    public void Release()
    {
        FmodCall("release");
    }
    /// <summary>
    /// Sets the 3D attributes (position, rotation, skew? Not sure if this has an effect on sound).
    /// </summary>
    /// <param name="transform">The Transform3D to copy from.</param>
    public void Set3dAttributes(Transform3D transform)
    {
        FmodCall("set_3d_attributes", transform);
    }
    /// <summary>
    /// Not sure? Needs testing.
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="callBackMask"></param>
    public void SetCallback(Callable callback, int callBackMask)
    {
        FmodCall("set_callback", callback, callBackMask);
    }
    /// <summary>
    /// Sets the value of a parameter for this event by ID. Should not be used on global parameters.
    /// </summary>
    /// <param name="parameterId">The ID of the parameter to set.</param>
    /// <param name="value">The value to set the parameter to.</param>
    public void SetParameterById(int parameterId, float value)
    {
        FmodCall("set_parameter_by_id", parameterId, value);
    }
    /// <summary>
    /// Unsure how this differs from SetParameterById. Needs testing.
    /// </summary>
    /// <param name="parameterId"></param>
    /// <param name="label"></param>
    /// <param name="ignoreSeekSpeed"></param>
    public void SetParameterByIdWithLabel(int parameterId, string label, bool ignoreSeekSpeed)
    {
        FmodCall("set_parameter_by_id_with_label", parameterId, label, ignoreSeekSpeed);
    }
    /// <summary>
    /// Sets the value of a parameter for this event by name. Should not be used on global parameters.
    /// </summary>
    /// <param name="parameterName">The name of the parameter to set.</param>
    /// <param name="value">The value to set the parameter to.</param>
    public void SetParameterByName(string parameterName, float value)
    {
        FmodCall("set_parameter_by_name", parameterName, value);
    }
    /// <summary>
    /// Unsure how this differs from SetParameterByName.
    /// </summary>
    /// <param name="parameterName"></param>
    /// <param name="label"></param>
    /// <param name="ignoreSeekSpeed"></param>
    public void SetParameterByNameWithLabel(string parameterName, string label, bool ignoreSeekSpeed)
    {
        FmodCall("set_parameter_by_name_with_label", parameterName, label, ignoreSeekSpeed);
    }
    /// <summary>
    /// ????????????????? wtf is this - Will
    /// </summary>
    /// <param name="pProgrammersCallbackSoundKey"></param>
    public void SetProgrammerCallback(string pProgrammersCallbackSoundKey)
    {
        FmodCall("set_programmer_callback", pProgrammersCallbackSoundKey);
    }
    /// <summary>
    /// Sets the master reverb level of the event... by index? Not sure
    /// </summary>
    /// <param name="index"></param>
    /// <param name="level"></param>
    public void SetReverbLevel(int index, float level)
    {
        FmodCall("set_reverb_level", index, level);
    }
    /// <summary>
    /// Starts the timeline on the event.
    /// </summary>
    public void Start()
    {
        FmodCall("start");
    }
    /// <summary>
    /// Stops the timeline on the event.
    /// </summary>
    /// <param name="stopMode">The stop mode. 0 allows fadeout. 1 is immediate. Should be an enum</param>
    public void Stop(int stopMode)
    {
        FmodCall("stop", stopMode);
    }
    #endregion
}