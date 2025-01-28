using ArchitectsInVoid.WorldData;
using Godot;

namespace ArchitectsInVoid.UI;

/// <summary>
/// Manages the pause menu and pause state of the game
/// </summary>
[Tool]
public partial class Pause : Node
{
    #region Variables

    [Export] TextureButton _resumeBtn, _saveGameBtn, _loadGameBtn, _mainMenuBtn, _desktopBtn;
    public static Pause Singleton => _singleton;
    private static Pause _singleton;
    public bool IsPaused => _isPaused;
    private bool _isPaused;
    private bool _gameSavedWhilePaused;
    private bool _readyForPauseInput;

    [Export] private UIManager _UIManager;

    #endregion

    /// <summary>
    /// Called when the node enters the scene tree for the first time.
    /// </summary>
    public override void _Ready()
    {
        if (Engine.IsEditorHint()) { return; } // do NOT run when not in game

        #region Singleton Setter

        if (_singleton != null)
        {
            GD.Print("Pause: singleton already exists.");
        }
        _singleton = this;

        #endregion
        #region Error or Null Checks

        if (_resumeBtn == null || _saveGameBtn == null || _loadGameBtn == null || _mainMenuBtn == null || _desktopBtn == null)
        {
            GD.Print("Pause: missing texture buttons...");
            return;
        }
        if(_UIManager == null)
        {
            _UIManager = GetParent().GetParent().GetParent() as UIManager;
            if (_UIManager == null)
            {
                GD.Print("Pause: ui manager not found...");
            }
        }

        #endregion
        #region Connect Buttons

        if (!_resumeBtn.IsConnected(BaseButton.SignalName.ButtonDown, Callable.From(ResumeGame)))
        {
            _resumeBtn.Connect(BaseButton.SignalName.ButtonDown, Callable.From(ResumeGame));
        }
        if (!_saveGameBtn.IsConnected(BaseButton.SignalName.ButtonDown, Callable.From(SaveGame)))
        {
            _saveGameBtn.Connect(BaseButton.SignalName.ButtonDown, Callable.From(SaveGame));
        }
        if (!_loadGameBtn.IsConnected(BaseButton.SignalName.ButtonDown, Callable.From(LoadGame)))
        {
            _loadGameBtn.Connect(BaseButton.SignalName.ButtonDown, Callable.From(LoadGame));
        }
        if (!_mainMenuBtn.IsConnected(BaseButton.SignalName.ButtonDown, Callable.From(MainMenu)))
        {
            _mainMenuBtn.Connect(BaseButton.SignalName.ButtonDown, Callable.From(MainMenu));
        }
        if (!_desktopBtn.IsConnected(BaseButton.SignalName.ButtonDown, Callable.From(Desktop)))
        {
            _desktopBtn.Connect(BaseButton.SignalName.ButtonDown, Callable.From(Desktop));
        }

        #endregion

        _gameSavedWhilePaused = false;
        _readyForPauseInput = true;
    }

    /// <summary>
    /// Manages input for pausing and unpausing the game
    /// </summary>
    public override void _Input(InputEvent @event)
    {
        // get the game state and if in game check if pause button pressed
        if(GameManager.Singleton.CurrentGameState == GameManager.GameState.InGame)
        {
            // readyForPauseInput is here because PAUSE BUTTON CAN RUN MULTIPLE FUCKING TIMES
            //                                    Seriously... Dont ask......................
            if (@event.IsActionPressed("miscellaneous_pause") && _readyForPauseInput)
            {
                _readyForPauseInput = false; // stop duplicate call
                GD.Print(@event as InputEventAction);
                GD.Print("Pause: pause input received...");

                if (_isPaused)
                {
                    GD.Print("Pause: game is currently paused so unpausing it now");
                    SetPause(false);
                }
                else
                {
                    GD.Print("Pause: game is currently unpaused so pausing it now");
                    SetPause(true);
                }
                _readyForPauseInput = true; // allow further calls now that we are done
            }

            // switch show / hide cursor
            if (@event.IsActionPressed("miscellaneous_show_cursor"))
            {
                if(Input.MouseMode == Input.MouseModeEnum.Captured)
                {
                    Input.MouseMode = Input.MouseModeEnum.Visible;
                }
                else
                {
                    if (CheckIfCursorForced()) { return; }
                    Input.MouseMode = Input.MouseModeEnum.Captured;
                    _UIManager.UIInventoryManager.CancelItemInCursor();
                    // Cancel item being in cursor
                }
            }
        }
        
    }

    public bool CheckIfCursorForced()
    {
        if (IsPaused) { return true; }// cursor forced in pause menu
        if (ComponentSelectionUI.Singleton.IsCurrentlyShown()) { return true; }// cursor forced in component menu

        return false; // cursor is not forced after all these checks
    }

    public void GameSavedTrigger()
    {
        _gameSavedWhilePaused = true;
    }

    /// <summary>
    /// Sets the pause state
    /// </summary>
    public void SetPause(bool pause)
    {
        _gameSavedWhilePaused = false;
        _isPaused = pause;
        if(IsPaused)
        {
            ((UIManager)GameManager.Singleton.FindChild("UI")).UIWindowManager.ShowWindow("PauseMenu");
            GD.Print("Pause: game has been paused");
            // This probably shouldn't be here
            Input.MouseMode = Input.MouseModeEnum.Visible;
        }
        else
        {
            ((UIManager)GameManager.Singleton.FindChild("UI")).UIWindowManager.ShowWindow("HUD");
            GD.Print("Pause: game has been unpaused");
            if (CheckIfCursorForced()) { return; }
            Input.MouseMode = Input.MouseModeEnum.Captured;
        }
    }

    /// <summary>
    /// Button for resume game
    /// </summary>
    private void ResumeGame()
    {
        GD.Print("Pause: resume btn pressed");
        SetPause(false);
    }

    /// <summary>
    /// Button for save game
    /// </summary>
    private void SaveGame()
    {
        GD.Print("Pause: save btn pressed");
        _UIManager.WorldMenu.DataInstance.QuickSave();
        _UIManager.PopUpManager.DisplayInfoPopUp("Successfully Saved!", Callable.From(ResumeGame));
    }

    /// <summary>
    /// Button for load game
    /// </summary>
    private void LoadGame()
    {
        GD.Print("Pause: saveload btn pressed");
        _UIManager.UIWindowManager.ShowOnly("WorldManager");
        _UIManager.WorldMenu.CallLoad();
    }

    /// <summary>
    /// Button for main menu
    /// </summary>
    private void MainMenu()
    {
        GD.Print("Pause: main menu btn pressed");
        if (_gameSavedWhilePaused)
        {
            MainMenuConfirmed(); // go straight to main menu
        }
        else
        {
            // confirm with user is this what they want
            // we dont need an unsaved game here :3
            ((UIManager)GameManager.Singleton.FindChild("UI")).PopUpManager.DisplayConfirmPopUp("Exit without saving?", Callable.From(MainMenuConfirmed));
        }
    }

    /// <summary>
    /// Goes straight to the main menu
    /// </summary>
    public void MainMenuConfirmed()
    {
        _UIManager.UIWindowManager.ShowWindow("MainMenu");
        HideAllGameRelatedUI();
        GameManager.Singleton.SetGameState(GameManager.GameState.MainMenu);
        ((Data)GameManager.Singleton.FindChild("Data")).Clear();
        _UIManager.UIInventoryManager.HideInventoryList();
    }

    void HideAllGameRelatedUI()
    {
        // hide inventories componentselection
        _UIManager.ComponentSelection.MenuShown(false);
        _UIManager.UIInventoryManager.HideInventoryList();
        _UIManager.UIInventoryManager.ClearWindows();
    }

    /// <summary>
    /// Exits the game to desktop
    /// </summary>
    private void Exit()
    {
        GD.Print("INFO: Exit");
        GetTree().Quit();
    }

    /// <summary>
    /// Button for quitting to desktop
    /// </summary>
    private void Desktop()
    {
        GD.Print("Pause: desktop btn pressed");
        if (_gameSavedWhilePaused)
        {
            Exit(); // exit if there was a save
        }
        else
        {
            // if no save then make sure you confirm with user
            // dont need a lost save here :3
            ((UIManager)GameManager.Singleton.FindChild("UI")).PopUpManager.DisplayConfirmPopUp("Are you sure you want to exit without saving?", Callable.From(Exit));
        }
    }
}