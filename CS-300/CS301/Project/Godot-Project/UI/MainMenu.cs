using Godot;
using Godot.Collections;

namespace ArchitectsInVoid.UI;

[Tool]
public partial class MainMenu : Node
{
    [Export] private TextureButton _resumeBtn, _newBtn, _loadBtn, _optionsBtn, _exitBtn;
    [Export] private Window _winSettings, _winWorldManager;
    [Export] private WindowManager _wm;

    /// <summary>
    /// Called when the node enters the scene tree for the first time.
    /// </summary>
    public override void _Ready()
    {
        if (Engine.IsEditorHint()) { return; } // do NOT run when not in game

        #region Error OR Null Checks

        if (_wm == null)
        {
            _wm = (WindowManager)GetParent().GetParent();
            if (_wm == null)
            {
                GD.PushError("MainMenu: missing WindowManger...");
                return;
            }
        }
        if (_winSettings == null || _winWorldManager == null)
        {
            _winSettings = (Window)_wm.FindChild("Settings", false);
            _winWorldManager = (Window)_wm.FindChild("WorldManager", false);
            if (_winSettings == null || _winWorldManager == null)
            {
                GD.PushError("MainMenu: missing windows...");
                return;
            }
        }
        if (_resumeBtn == null || _newBtn == null || _loadBtn == null || _optionsBtn == null || _exitBtn == null)
        {
            _resumeBtn = (TextureButton)FindChild("Resume");
            _newBtn = (TextureButton)FindChild("New");
            _loadBtn = (TextureButton)FindChild("Load");
            _optionsBtn = (TextureButton)FindChild("Options");
            _exitBtn = (TextureButton)FindChild("Exit");
            if (_resumeBtn == null || _newBtn == null || _loadBtn == null || _optionsBtn == null || _exitBtn == null)
            {
                GD.PushError("MainMenu: missing buttons...");
                return;
            }
        }

        #endregion
        #region Connect Buttons

        if (!_resumeBtn.IsConnected(BaseButton.SignalName.ButtonDown, Callable.From(ResumeGame)))
        {
            _resumeBtn.Connect(BaseButton.SignalName.ButtonDown, Callable.From(ResumeGame));
        }
        if (!_newBtn.IsConnected(BaseButton.SignalName.ButtonDown, Callable.From(NewGame)))
        {
            _newBtn.Connect(BaseButton.SignalName.ButtonDown, Callable.From(NewGame));
        }
        if (!_loadBtn.IsConnected(BaseButton.SignalName.ButtonDown, Callable.From(LoadGame)))
        {
            _loadBtn.Connect(BaseButton.SignalName.ButtonDown, Callable.From(LoadGame));
        }
        if (!_optionsBtn.IsConnected(BaseButton.SignalName.ButtonDown, Callable.From(Options)))
        {
            _optionsBtn.Connect(BaseButton.SignalName.ButtonDown, Callable.From(Options));
        }
        if (!_exitBtn.IsConnected(BaseButton.SignalName.ButtonDown, Callable.From(Exit)))
        {
            _exitBtn.Connect(BaseButton.SignalName.ButtonDown, Callable.From(Exit));
        }

        #endregion
    }

    /// <summary>
    /// Button for resume game
    /// <br/>Currently this button is disabled and needs TODO: implementation
    /// </summary>
    private void ResumeGame()
    {
        GD.Print("MainMenu: Resume Game");
        // TODO: implement resume game functionality
    }

    /// <summary>
    /// Button for new game
    /// </summary>
    private void NewGame()
    {
        GD.Print("MainMenu: New Game");
        _wm.ShowWindow(_winWorldManager);
        foreach (var n in _winWorldManager.GetChildren())
        {
            var w = (WorldManager)n;
            if (w != null)
            {
                w.CallNew();
                break;
            }
        }
    }

    /// <summary>
    /// Button for load game
    /// </summary>
    private void LoadGame()
    {
        GD.Print("MainMenu: Load Game");
        _wm.ShowWindow(_winWorldManager);
        foreach (var n in _winWorldManager.GetChildren())
        {
            var w = (WorldManager)n;
            if (w != null)
            {
                w.CallLoad();
                break;
            }
        }
    }

    /// <summary>
    /// Button for options
    /// </summary>
    private void Options()
    {
        GD.Print("MainMenu: Options");
        _wm.ShowWindow(_winSettings);
        ((UIManager)_wm.GetParent()).SettingsManager.LoadSettings();
    }

    /// <summary>
    /// Manages the X / Close button the window has
    /// </summary>
    public override void _Notification(int what)
    {
        if (what == NotificationWMCloseRequest) Exit();
    }

    /// <summary>
    /// Button for exit game
    /// </summary>
    private void Exit()
    {
        GD.Print("INFO: Exit");
        GetTree().Quit(0);
    }

    /// <summary>
    /// Used for inspector buttons plugin
    /// </summary>
    public Godot.Collections.Array AddInspectorButtons()
    {
        var buttons = new Godot.Collections.Array();

        var reload = new Dictionary
        {
            { "name", "Reload" },
            { "icon", GD.Load("res://Testing/InspectorButtons/icon.svg") },
            {
                "pressed", Callable.From(_Ready)
            }
        };
        buttons.Add(reload);


        return buttons;
    }
}