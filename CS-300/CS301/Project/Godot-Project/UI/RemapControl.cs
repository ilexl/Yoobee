using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// This manages a remappable control
/// </summary>
[Tool]
public partial class RemapControl : Node
{
    #region Variables

    [Export] public string InputMapName;
	[Export] public string DisplayName;
    [Export] TextureButton _primaryRemapBtn, _secondaryRemapBtn;
	[Export] RichTextLabel _title, _primaryRemapTxt, _secondaryRemapTxt;
    InputEvent _primaryIE, _secondaryIE;
    string _strprimaryIE, _strsecondaryIE;
    UIManager _uim;
    InfoPopUp _infoPopUp;
    bool _remapPrimary, _remapSecondary;

    #endregion

    /// <summary>
    /// Called when the node is first loaded in the scene tree
    /// </summary>
    public override void _Ready()
    {
        if (Engine.IsEditorHint()) { return; } // do NOT run when not in game

        _remapPrimary = false;
        _remapSecondary = false;
        if (!_primaryRemapBtn.IsConnected(BaseButton.SignalName.ButtonDown, Callable.From(RemapPrimary)))
        {
            _primaryRemapBtn.Connect(BaseButton.SignalName.ButtonDown, Callable.From(RemapPrimary));
        }
        if (!_secondaryRemapBtn.IsConnected(BaseButton.SignalName.ButtonDown, Callable.From(RemapSecondary)))
        {
            _secondaryRemapBtn.Connect(BaseButton.SignalName.ButtonDown, Callable.From(RemapSecondary));
        }
    }

    /// <summary>
    /// Button for remapping the primary input for THIS remappable control
    /// </summary>
    void RemapPrimary()
    {
        if(Input.IsMouseButtonPressed(MouseButton.Right))
        {
            _primaryIE = null;
            SaveCurrent();
            DisplayCurrent();
            return;
        }
        _remapPrimary = true;
        _infoPopUp = _uim.PopUpManager.DisplayInfoPopUpNC("Press any key/button...");
    }

    /// <summary>
    /// Button for remapping the secondary input for THIS remappable control
    /// </summary>
    void RemapSecondary()
    {
        if (Input.IsMouseButtonPressed(MouseButton.Right))
        {
            _secondaryIE = null;
            SaveCurrent();
            DisplayCurrent();
            return;
        }
        _remapSecondary = true;
        _infoPopUp = _uim.PopUpManager.DisplayInfoPopUpNC("Press any key/button...");
    }

    /// <summary>
    /// Custom version to recieve, identify and map input
    /// </summary>
    /// <param name="event"></param>
    public override void _Input(InputEvent @event)
    {
        bool reset = false;
        // if remapping either primary or secondary
        if (_remapPrimary || _remapSecondary)
        {
            // if the event is an input (keyboard)
            if (@event is InputEventKey eventKey)
            {
                // if the input was actually pressed
                if (eventKey.Pressed)
                {
                    // if remapping primary
                    if (_remapPrimary)
                    {
                        if(InputEventToString(eventKey) == InputEventToString(_secondaryIE))
                        {
                            _secondaryIE = null;
                        }
                        _primaryIE = eventKey;
                        
                    }

                    // if remapping secondary
                    if (_remapSecondary)
                    {
                        if (InputEventToString(eventKey) == InputEventToString(_primaryIE))
                        {
                            _primaryIE = null;
                        }
                        _secondaryIE = eventKey;    
                    }

                    reset = true;

                    
                }
            }
            // if the event is an input (mouse button)
            if (@event is InputEventMouseButton eventMouse)
            {
                // if the input was actually pressed
                if (eventMouse.Pressed)
                {
                    // if remapping primary
                    if (_remapPrimary)
                    {
                        if (InputEventToString(eventMouse) == InputEventToString(_secondaryIE))
                        {
                            _secondaryIE = null;
                        }
                        _primaryIE = eventMouse;
                        
                    }
                    
                    // if remapping secondary
                    if (_remapSecondary)
                    {
                        if (InputEventToString(eventMouse) == InputEventToString(_primaryIE))
                        {
                            _primaryIE = null;
                        }
                        _secondaryIE = eventMouse;
                    }

                    reset = true;
                }
            }
            // if the event is an input (joypad button)
            if (@event is InputEventJoypadButton eventJoy)
            {
                // if the input was actually pressed
                if (eventJoy.Pressed)
                {
                    // if remapping primary
                    if (_remapPrimary)
                    {
                        if (InputEventToString(eventJoy) == InputEventToString(_secondaryIE))
                        {
                            _secondaryIE = null;
                        }
                        _primaryIE = eventJoy;

                    }

                    // if remapping secondary
                    if (_remapSecondary)
                    {
                        if (InputEventToString(eventJoy) == InputEventToString(_primaryIE))
                        {
                            _primaryIE = null;
                        }
                        _secondaryIE = eventJoy;

                    }

                    reset = true;
                }

            }

            // reset if any control was changed
            if (reset)
            {
                // reset everything else
                _infoPopUp.Close();
                _infoPopUp = null;
                _remapPrimary = false;
                _remapSecondary = false;
                SaveCurrent();
                DisplayCurrent();
            }
        }
    }

    /// <summary>
    /// Displays the current control primary and secondary input names
    /// </summary>
    public void DisplayCurrent()
	{
        if (DisplayName == "" || DisplayName == null) { DisplayName = InputMapName; }
        _title.Text = DisplayName;

        _strprimaryIE = InputEventToString(_primaryIE);
        _strsecondaryIE = InputEventToString(_secondaryIE);

        GD.Print($"RemapControl: {InputMapName} PRIMARY  : {_strprimaryIE}");
        _primaryRemapTxt.Text = _strprimaryIE;

        GD.Print($"RemapControl: {InputMapName} SECONDARY: {_strsecondaryIE}");
        _secondaryRemapTxt.Text = _strsecondaryIE;
    }

    /// <summary>
    /// Loads a control from settings if set by user or from Godot Prefs if valid in InputMap
    /// </summary>
    public void LoadControlData()
    {
        int isValid = InputMap.GetActions().IndexOf(InputMapName);
        if (isValid == -1)
        {
            GD.PushError($"RemapControl: {InputMapName} is not a valid Action in the InputMap...");
            return; // not a valid input - dont continue
        }

        // check if settings exists
        bool settingsFound = TryGetSettings();
        if (!settingsFound) // not found
        {
            GD.PushError("RemapControl: cant find settings...");
            return; // not saved - dont continue
        }

        // check control has saved data from settings
        bool controlHasSavedData = false;
        if (_uim.SettingsManager.SavedControls != null)
        {
            foreach (var c in _uim.SettingsManager.SavedControls)
            {
                if (c.InputMapName == InputMapName)
                {
                    controlHasSavedData = true;
                    InputMap.ActionEraseEvents(InputMapName);
                    _primaryIE = c.inputEventPrimary;
                    _secondaryIE = c.inputEventSecondary;
                    if(_primaryIE != null)
                    {
                        InputMap.ActionAddEvent(InputMapName, _primaryIE); // sets input here
                    }
                    if(_secondaryIE != null)
                    {
                        InputMap.ActionAddEvent(InputMapName, _secondaryIE); // sets input here
                    }
                }
            }
        }


        // if no settings load default here
        ForceMaxInputs(InputMapName); // forces max of 2 input actions for primary | secondary

        if (controlHasSavedData)
        {
            // use values from settings
            _strprimaryIE = InputEventToString(_primaryIE);
            _strsecondaryIE = InputEventToString(_secondaryIE);
        }
        else
        {
            // load values from default inputmap
            _primaryIE = null;
            _secondaryIE = null;

            Array<InputEvent> e = InputMap.ActionGetEvents(InputMapName);

            foreach (InputEvent ev in e)
            {
                if (ev is InputEventKey || ev is InputEventMouseButton)
                {
                    if (_primaryIE == null)
                    {
                        _primaryIE = ev;
                        _strprimaryIE = InputEventToString(ev);
                    }
                    else if (_secondaryIE == null)
                    {
                        _secondaryIE = ev;
                        _strsecondaryIE = InputEventToString(ev);
                    }
                    else
                    {
                        GD.PushError("RemapControl: forced inputs includes more than 2 inputs...");
                        return;
                    }
                }
                if (ev is InputEventJoypadButton)
                {
                    GD.PushError("RemapControl: forced inputs includes a joypad control (NOT IMPLEMENTED)...");
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Saves the currnt control to settings
    /// </summary>
    public void SaveCurrent()
    {
        DisplayCurrent(); // show current value

        if (_uim.SettingsManager.SavedControls == null)
        {
            // if list is blank i.e no controls save before.
            // make a new one - no checks required
            _uim.SettingsManager.SavedControls = new List<ArchitectsInVoid.Settings.Settings.Control>();
            var c = new ArchitectsInVoid.Settings.Settings.Control();
            c.InputMapName = InputMapName;
            c.inputEventPrimary = _primaryIE;
            c.inputEventSecondary = _secondaryIE;
            _uim.SettingsManager.SavedControls.Add(c);
        }
        else
        {
            // if list found then check if control is in the list and update accordingly
            bool found = false;
            foreach(var c in _uim.SettingsManager.SavedControls)
            {
                if(c.InputMapName == InputMapName)
                {
                    found = true;
                    _uim.SettingsManager.SavedControls.Remove(c);
                    var cn = new ArchitectsInVoid.Settings.Settings.Control();
                    cn.InputMapName = InputMapName;
                    cn.inputEventPrimary = _primaryIE;
                    cn.inputEventSecondary = _secondaryIE;
                    _uim.SettingsManager.SavedControls.Add(cn);
                    break;
                }
            }
            if (!found)
            {
                var cn = new ArchitectsInVoid.Settings.Settings.Control();
                cn.InputMapName = InputMapName;
                cn.inputEventPrimary = _primaryIE;
                cn.inputEventSecondary = _secondaryIE;
                _uim.SettingsManager.SavedControls.Add(cn);
            }
        }
        LoadControlData();
    }

    /// <summary>
    /// Forces a max input amount per input name
    /// <br/>DEFAULT: 2 due to primary and secondary. 
    /// <br/>Any more will add undefined behaviour as the user wont know it is a control...
    /// </summary>
    private static void ForceMaxInputs(string inputMapName)
    {
        Array<InputEvent> e = InputMap.ActionGetEvents(inputMapName);
        int amount = 0;
        foreach(InputEvent ev in e)
        {
            if (ev is InputEventKey iek || ev is InputEventMouseButton)
            {
                amount++;
                if(amount > 2)
                {
                    InputMap.ActionEraseEvent(inputMapName, ev);
                }
            }
            if (ev is InputEventJoypadButton)
            {
                GD.PushWarning("Controller Support Not FULLY Implemented Yet...");
                amount++;
                if (amount > 2)
                {
                    InputMap.ActionEraseEvent(inputMapName, ev);
                }
            }
        }
    }

    /// <summary>
    /// Returns a string of the input that a user can understand
    /// </summary>
    private static string InputEventToString(InputEvent ev)
    {
        if(ev == null)
        {
            return "None";
        }
        if (ev is InputEventKey iek)
        {
            return iek.AsTextPhysicalKeycode();

        }
        if (ev is InputEventMouseButton iemb)
        {
            return $"Mouse {iemb.ButtonIndex}";
        }
        if (ev is InputEventJoypadButton)
        {
            // TODO: implement naming for controllers...
            GD.PushError("RemapControl: forced inputs includes a joypad control (NOT IMPLEMENTED)...");
            return "NOT IMPLEMENTED";
        }
        GD.PushError("RemapControl: ie to string no valid type found...");
        return "INVALID";
    }

    /// <summary>
    /// Tries to get Settings from the UIManager
    /// </summary>
    bool TryGetSettings()
    {
        var target = GameManager.Singleton.FindChild("UI");
        var uim = (UIManager)target;
        if(uim != null)
        {
            _uim = uim;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Resets the input to the defaults made in project settings input map
    /// </summary>
    internal void Reset()
    {
        //GD.Print("RemapControl: reset called");
        InputMap.ActionEraseEvents(InputMapName);
        var test = ProjectSettings.GetSetting($"input/{InputMapName}");
        //GD.Print(test);
        Array<InputEvent> e = (Array<InputEvent>)test.AsGodotDictionary().GetValueOrDefault("events");

        _primaryIE = null;
        _secondaryIE = null;

        foreach (InputEvent ev in e)
        {
            if (ev is InputEventKey || ev is InputEventMouseButton)
            {
                if (_primaryIE == null)
                {
                    _primaryIE = ev;
                    _strprimaryIE = InputEventToString(ev);
                }
                else if (_secondaryIE == null)
                {
                    _secondaryIE = ev;
                    _strsecondaryIE = InputEventToString(ev);
                }
                else
                {
                    GD.PushError("RemapControl: reset inputs includes more than 2 inputs...");
                    return;
                }
            }
            if (ev is InputEventJoypadButton)
            {
                GD.PushError("RemapControl: reset inputs includes a joypad control (NOT IMPLEMENTED)...");
                return;
            }
        }
        if (_primaryIE != null)
        {
            InputMap.ActionAddEvent(InputMapName, _primaryIE);
        }
        if (_secondaryIE != null)
        {
            InputMap.ActionAddEvent(InputMapName, _secondaryIE);
        }
    }
}
