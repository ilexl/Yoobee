using Godot;
using System.Collections.Generic;

namespace ArchitectsInVoid.Settings;

/// <summary>
/// Settings loaded / saved / applied for the game
/// </summary>
[Tool]
public partial class Settings : Node
{
    #region Variables

    [ExportGroup("General")]
    [Export] public Language GameLanguage;

    // controls
    public List<Control> SavedControls;

    [ExportGroup("Audio")]
    [Export] public double MasterVolume;
    [Export] public double EffectsVolume;
    [Export] public double MusicVolume;
    [Export] public double DialougeVolume;
    [Export] public Language SpokenLanguage;
    [Export] public bool Subtitles;
    [Export] public Language SubtitlesLanguage;
    [Export] public SpeakerMode SpeakerSMode;
    [Export] Node _fmodSpecificScript;

    [ExportGroup("Display")]
    [Export] public Vector2I Resolution;
    [Export] public double RefreshRate;
    [Export] public DisplayMode DisplaySMode;
    [Export] public bool VSync;

    #endregion

    #region Defined Types

    /// <summary>
    /// Resolutions supported by us. Specified up to 4K
    /// </summary>
    public Vector2I[] SUPPORTED_RESOLUTIONS = { new Vector2I(1024, 768),
                                                new Vector2I(1280, 720),
                                                new Vector2I(1280, 800),
                                                new Vector2I(1280, 1024),
                                                new Vector2I(1366, 768),
                                                new Vector2I(1600, 900),
                                                new Vector2I(1680, 1050),
                                                new Vector2I(1440, 900),
                                                new Vector2I(1920, 1080),
                                                new Vector2I(2560, 1440),
                                                new Vector2I(3840, 2160)};
 
    /// <summary>
    /// Languages supported by the game
    /// <br/>NOTE: currently only supports ENGLISH but has others for demo purposes
    /// <br/>TODO: add support for other languages
    /// </summary>
    public enum Language
    {
        English = 0, // currently only support english - others are for demo purposes 
        Mandarin = 1,
        Spanish = 2,
        Hindi = 3,
        Russian = 4,
        Japanese = 5,
        Korean = 6,
        French = 7,
        German = 8,
        Italian = 9
    };

    /// <summary>
    /// Speaker Mode for Fmod defined in settings
    /// <br/>TODO: fix Fmod ignoring the setting...
    /// </summary>
    public enum SpeakerMode
    {
        DEFAULT = 0,
        RAW = 1,
        MONO = 2,
        STEREO = 3,
        QUAD = 4,
        SURROUND = 5,
        FIVEPOINTONE = 6,
        SEVENPOINTONE = 7,
        SEVENPOINTONEPOINTFOUR = 8
    };

    /// <summary>
    /// A remappable control which can be triggered by a primary or secondary trigger
    /// </summary>
    public struct Control
    {
        public string InputMapName;
        public InputEvent inputEventPrimary;
        public InputEvent inputEventSecondary;
    }

    /// <summary>
    /// Display types fullscreen windowed etc...
    /// </summary>
    public enum DisplayMode
    {
        Fullscreen_Exclusive = 0,
        Fullscreen_Borderless = 1,
        Windowed = 2
    }

    #endregion

    #region Main Functions

    /// <summary>
    /// _Ready runs when the node is first being loaded into the scene tree
    /// </summary>
    public override void _Ready()
    {
        if (Engine.IsEditorHint()) { return; } // do NOT run when not in game
        LoadSettings(); // load from disk when loading for the first time
        ApplyCurrentSettings(); // apply settings loaded from disk
    }

    /// <summary>
    /// Calls the sub settings apply functions
    /// </summary>
    public void ApplyCurrentSettings()
    {
        ApplyScreenSettings();
        ApplySoundSettings();
    }

    #endregion

    #region Public String Converters

    /// <summary>
    /// Returns a string version of the Language Enum
    /// </summary>
    public string LanguageToString(Language language)
    {
        switch (language)
        {
            case Language.English:
                return "English";
            case Language.Mandarin:
                return "Mandarin";
            case Language.Spanish:
                return "Spanish";
            case Language.Hindi:
                return "Hindi";
            case Language.Russian:
                return "Russian";
            case Language.Japanese:
                return "Japanese";
            case Language.Korean:
                return "Korean";
            case Language.French:
                return "French";
            case Language.German:
                return "German";
            case Language.Italian:
                return "Italian";
            default:
                GD.Print("Settings: language invalid - likely null data");
                return "NULL";
        }
    }

    /// <summary>
    /// Returns a string version of the speaker mode suitable for a user to understand
    /// </summary>
    public string SpeakerModeToDisplayString(SpeakerMode speakerMode)
    {
        switch (speakerMode)
        {
            case SpeakerMode.DEFAULT:
                return "Default";
            case SpeakerMode.RAW:
                return "Raw";
            case SpeakerMode.MONO:
                return "Mono";
            case SpeakerMode.STEREO:
                return "Stereo";
            case SpeakerMode.QUAD:
                return "Quad";
            case SpeakerMode.SURROUND:
                return "Surround";
            case SpeakerMode.FIVEPOINTONE:
                return "5.1";
            case SpeakerMode.SEVENPOINTONE:
                return "7.1";
            case SpeakerMode.SEVENPOINTONEPOINTFOUR:
                return "7.1.4";
            default:
                GD.PushError("Settings: Not a valid SpeakerMode");
                return "ERROR";
        }
    }

    /// <summary>
    /// Returns a string version of display mode suitable for a user to read
    /// </summary>
    public string DisplayModeToString(DisplayMode mode)
    {
        switch (mode)
        {
            case DisplayMode.Fullscreen_Exclusive:
                {
                    return "Fullscreen (Exclusive)";
                }
            case DisplayMode.Fullscreen_Borderless:
                {
                    return "Fullscreen (Borderless)";
                }
            case DisplayMode.Windowed:
                {
                    return "         Windowed       "; // blank space is due to control not set up correctly
                }
            default:
                {
                    GD.Print("Settings: display mode invalid - likely null data");
                    return "NULL";
                }
        }
    }

    #endregion

    #region FileStuff

    /// <summary>
    /// returns a constant string path to the save location in a valid format
    /// <br/>NOTE: did not like combining them or using a variable hence this function :)
    /// </summary>
    string GetSavePath()
    {
        return "user://" + "application/";
    }

    /// <summary>
    /// Saves all settings using all the local variables of Settings class
    /// </summary>
    public void SaveSettings()
    {
        GD.Print("Settings: saving to file...");
        DirAccess.MakeDirRecursiveAbsolute(GetSavePath());
        string _name = "settings";
        var file = FileAccess.Open($"{GetSavePath()}{_name}.dat", FileAccess.ModeFlags.Write);

        // file .StoreVar
        // save file stuff here

        // Audio
        file.StoreVar("Audio");
        file.StoreVar(MasterVolume);
        file.StoreVar(EffectsVolume);
        file.StoreVar(MusicVolume);
        file.StoreVar(DialougeVolume);
        file.StoreVar((int)SpokenLanguage);
        file.StoreVar(Subtitles);
        file.StoreVar((int)SubtitlesLanguage);
        file.StoreVar((int)SpeakerSMode);

        // Display
        file.StoreVar("Display");
        file.StoreVar(Resolution);
        file.StoreVar(RefreshRate);
        file.StoreVar((int)DisplaySMode);
        file.StoreVar(VSync);

        // General
        file.StoreVar("General");
        file.StoreVar((int)GameLanguage);

        file.Close(); // must be called or else creates a bunch of .tmp files

        SaveControls(); // controls are stored in a seperate file
    }

    /// <summary>
    /// Saves the controls managed by settings
    /// <br/>Called by the main save function
    /// </summary>
    private void SaveControls()
    {
        GD.Print("Settings: saving controls to file...");
        DirAccess.MakeDirRecursiveAbsolute(GetSavePath());
        string _name = "controls";
        var file = FileAccess.Open($"{GetSavePath()}{_name}.dat", FileAccess.ModeFlags.Write);
        int amount = SavedControls.Count;
        file.StoreVar(amount); // store the amount to allow reading a loop amount
        foreach (var control in SavedControls)
        {
            file.StoreVar(control.InputMapName);
            SaveKey(file, control.inputEventPrimary);
            SaveKey(file, control.inputEventSecondary);
        }
        file.Close();
    }

    /// <summary>
    /// Keys are stored differently due to Variant issues
    /// <br/> type -1 == null input aka blank
    /// <br/> type  0 == mouse button
    /// <br/> type  1 == joypad button
    /// <br/> type  2 == keyboard
    /// </summary>
    /// <param name="file">controls file</param>
    /// <param name="ie">the input event to save</param>
    void SaveKey(FileAccess file, InputEvent ie)
    {
        if (ie == null)
        {
            file.StoreVar(-1); // type -1 == no input / blank control
        }
        else if (ie is InputEventMouseButton iemb)
        {
            file.StoreVar(0); // type 0 == mouse button input
            file.StoreVar(ie.Device);
            file.StoreVar((long)iemb.ButtonIndex);
        }
        else if (ie is InputEventJoypadButton iejb)
        {
            file.StoreVar(1); // type 1 == joypad button input
            file.StoreVar(ie.Device);
            file.StoreVar((long)iejb.ButtonIndex);

        }
        else if (ie is InputEventKey iek)
        {
            file.StoreVar(2); // type 2 == keyboard input
            file.StoreVar(ie.Device);
            file.StoreVar((long)iek.Keycode);
            file.StoreVar((long)iek.PhysicalKeycode);
            file.StoreVar((long)iek.Unicode);
        }
        else
        {
            file.StoreVar(-1); // not known is default  
            GD.PushError("Settings: invalid InputEvent type...");
        }
    }

    /// <summary>
    /// Keys are loaded differently due to Variant issues.
    /// <br/>Creates a InputEvent suitable for remappable controls
    /// <br/> type -1 == null input aka blank
    /// <br/> type  0 == mouse button
    /// <br/> type  1 == joypad button
    /// <br/> type  2 == keyboard
    /// </summary>
    /// <param name="file">controls file</param>
    InputEvent LoadKey(FileAccess file)
    {
        int type = file.GetVar().AsInt32();
        InputEvent output = null; // blank input
        if (type == -1)
        {
            return output; // return early and prevent further checks
        }
        if (type == 0) // InputEventMouseButton
        {
            var o = new InputEventMouseButton();
            o.Device = file.GetVar().AsInt32();
            o.ButtonIndex = (MouseButton)file.GetVar().AsInt64();
            output = o;
        }
        if (type == 1) // InputEventJoypadButton
        {
            var o = new InputEventJoypadButton();
            o.Device = file.GetVar().AsInt32();
            o.ButtonIndex = (JoyButton)file.GetVar().AsInt64();
            output = o;

        }
        if (type == 2) // InputEventKey
        {
            var o = new InputEventKey();
            o.Device = file.GetVar().AsInt32();
            o.Keycode = (Key)file.GetVar().AsInt64();
            o.PhysicalKeycode = (Key)file.GetVar().AsInt64();
            o.Unicode = file.GetVar().AsInt64();
            output = o;

        }
        return output;
    }

    /// <summary>
    /// Default settings defined in this function loaded to local variables and saved (overwriting the file)
    /// </summary>
    public void DefaultSettings()
    {
        GD.Print("Settings: setting default settings");

        GameLanguage = Language.English;

        MasterVolume = 100d;
        EffectsVolume = 100d;
        MusicVolume = 100d;
        DialougeVolume = 100d;
        SpokenLanguage = Language.English;
        Subtitles = true;
        SubtitlesLanguage = Language.English;
        SpeakerSMode = SpeakerMode.STEREO;

        Resolution = SUPPORTED_RESOLUTIONS[1];
        RefreshRate = 60;
        DisplaySMode = DisplayMode.Windowed;
        VSync = false;

        GD.Print("Settings: default settings set");

        DefaultControls(); // default controls defined by godot preferences
        SaveSettings();
    }

    /// <summary>
    /// Default controls loaded from godot project - file will be blank with no changes to default controls
    /// </summary>
    private void DefaultControls()
    {
        GD.Print("Settings: default controls to file...");
        DirAccess.MakeDirRecursiveAbsolute(GetSavePath());
        SavedControls = new();
        string _name = "controls";
        var file = FileAccess.Open($"{GetSavePath()}{_name}.dat", FileAccess.ModeFlags.Write);
        int zero = 0;
        file.StoreVar(zero); // store 0 to show there are no saved controls only default set in godot
        file.Close();
    }

    /// <summary>
    /// Load settings from file
    /// </summary>
    public void LoadSettings()
    {
        GD.Print("Settings: loading settings from file...");
        DirAccess.MakeDirRecursiveAbsolute(GetSavePath());
        string _name = "settings";
        if (!FileAccess.FileExists($"{GetSavePath()}{_name}.dat"))
        {
            GD.Print("Settings: no file exists... creating one with default settings");
            DefaultSettings(); // load default settings and save to file
            return; // no need to continue loading as default will load
        }
        var file = FileAccess.Open($"{GetSavePath()}{_name}.dat", FileAccess.ModeFlags.Read);

        // file .GetVar() .AsSomething()
        // load file stuff here

        // Audio
        if (file.GetVar().AsString() != "Audio")
        {
            file.Close();
            GD.PushError("Settings: loaded settings failed... file incorrect...");
            DefaultSettings();
            return;
        }
        MasterVolume = file.GetVar().AsDouble();
        EffectsVolume = file.GetVar().AsDouble();
        MusicVolume = file.GetVar().AsDouble();
        DialougeVolume = file.GetVar().AsDouble();
        SpokenLanguage = (Language)file.GetVar().AsInt32();
        Subtitles = file.GetVar().AsBool();
        SubtitlesLanguage = (Language)file.GetVar().AsInt32();
        SpeakerSMode = (SpeakerMode)file.GetVar().AsInt32();

        // Display
        if (file.GetVar().AsString() != "Display")
        {
            file.Close();
            GD.PushError("Settings: loaded settings failed... file incorrect...");
            DefaultSettings();
            return;
        }
        Resolution = file.GetVar().AsVector2I();
        RefreshRate = file.GetVar().AsInt32();
        DisplaySMode = (DisplayMode)file.GetVar().AsInt32();
        VSync = file.GetVar().AsBool();

        // General
        if (file.GetVar().AsString() != "General")
        {
            file.Close();
            GD.PushError("Settings: loaded settings failed... file incorrect...");
            DefaultSettings();
            return;
        }
        GameLanguage = (Language)file.GetVar().AsInt32();

        file.Close(); // must be called or else creates a bunch of .tmp files

        LoadControls(); // controls loaded seperately
    }

    /// <summary>
    /// Load controls from file
    /// </summary>
    private void LoadControls()
    {
        GD.Print("Settings: loading controls from file...");
        DirAccess.MakeDirRecursiveAbsolute(GetSavePath());
        string _name = "controls";
        if (!FileAccess.FileExists($"{GetSavePath()}{_name}.dat"))
        {
            GD.Print("Settings: no file exists... creating one with default settings");
            DefaultControls(); // load default settings and save to file
            return; // no need to continue loading as default will load
        }
        var file = FileAccess.Open($"{GetSavePath()}{_name}.dat", FileAccess.ModeFlags.Read);

        SavedControls = new List<Control>();

        // file .GetVar() .AsSomething()
        // load file stuff here

        // get amount as int
        int amount = file.GetVar().AsInt32();

        // loop amount
        for (; amount > 0; amount--)
        {
            Control c = new Control();
            c.InputMapName = file.GetVar().AsString();
            c.inputEventPrimary = LoadKey(file);
            c.inputEventSecondary = LoadKey(file);
            SavedControls.Add(c);
        }

        file.Close(); // must be called or else creates a bunch of .tmp files
    }
    
    #endregion

    #region DisplayMode
    
    /// <summary>
    /// Applies the display mode to the window
    /// </summary>
    void SetDisplayMode(DisplayMode mode)
    {
        switch (mode)
        {
            case DisplayMode.Fullscreen_Exclusive:
                {
                    GD.Print("Settings: fullscreen exclusive applied");
                    DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
                    DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, false);
                    break;
                }
            case DisplayMode.Fullscreen_Borderless:
                {
                    GD.Print("Settings: fullscreen borderless applied");
                    DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
                    DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, true);
                    break;
                }
            case DisplayMode.Windowed:
                {
                    GD.Print("Settings: windowed applied");
                    DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
                    DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, false);
                    break;
                }
            default:
                {
                    GD.PushError("Settings: no such display mode | it could also be null?");
                    break;
                }
        }
    }

    /// <summary>
    /// Applies VSync on/off to the window
    /// </summary>
    void SetVSync(bool sync)
    {
        if (sync)
        {
            DisplayServer.WindowSetVsyncMode(DisplayServer.VSyncMode.Enabled);
        }
        else
        {
            DisplayServer.WindowSetVsyncMode(DisplayServer.VSyncMode.Disabled);
        }
    }

    /// <summary>
    /// Applies the screen settings to the game window
    /// </summary>
    void ApplyScreenSettings()
    {
        if (Engine.IsEditorHint()) { return; } // do NOT apply settings if run by editor (changes the actual editor window??)
        DisplayServer.ScreenSetKeepOn(true);
        SetDisplayMode(DisplaySMode);
        SetVSync(VSync);
        DisplayServer.WindowSetSize(Resolution);
        Engine.Singleton.MaxFps = (int)System.Math.Round(RefreshRate, 0, System.MidpointRounding.AwayFromZero);
        Engine.Singleton.PhysicsJitterFix = 0;
        if (DisplaySMode == DisplayMode.Windowed)
        {
            // gets the center of the MAIN screen and puts the window in the center
            Window mainWindow = GetWindow();
            mainWindow.MoveToCenter();
            GD.Print($"Settings: Centered window to PRIMARY DISPLAY");
        }
    }

    #endregion

    #region AudioMode
    
    /// <summary>
    /// Applies the sounds settings to the game
    /// </summary>
    void ApplySoundSettings()
    {
        // TODO: fix implementation of speakermode
        //_fmodSpecificScript.Call("SetSpeakerMode", (int)SpeakerSMode);
    }

    #endregion
}