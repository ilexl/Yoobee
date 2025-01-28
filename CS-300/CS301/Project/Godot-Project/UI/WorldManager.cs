using System;
using System.Collections.Generic;
using System.Linq;
using ArchitectsInVoid.UI.UIElements;
using ArchitectsInVoid.WorldData;
using Godot;
using Godot.Collections;

namespace ArchitectsInVoid.UI;

/// <summary>
/// Lists and manages the saved worlds
/// </summary>
[Tool]
public partial class WorldManager : Node
{
    #region Variables

    [Export] private TextureButton _cancelBtn, _loadBtn, _newGameBtn, _deleteBtn, _saveBtn;
    private List<WorldSaveTitle> _currentlySelected;
    [Export] private int _testAmount;
    [Export] private bool _testWorldList;
    [Export] private Window _winMainMenu, _winHud;
    [Export] private WindowManager _wmMain;
    [Export] private Node _worldListHolder;
    [Export] private PackedScene _worldSaveListScene;
    [Export] UIManager _UIManager;
    [Export] public Data DataInstance;
    [Export] Control _mmBackground; // main menu background image

    #endregion

    /// <summary>
    /// Called when the node enters the scene tree for the first time.
    /// </summary>
    public override void _Ready()
    {
        if (Engine.IsEditorHint()) { return; } // do NOT run when not in game

        #region Error OR Null Checks

        if (_wmMain == null)
        {
            _wmMain = (WindowManager)GetParent().GetParent();
            if (_wmMain == null)
            {
                GD.PushError("World Manager: missing WindowManger/s...");
                return;
            }
        }
        if (_winMainMenu == null || _winHud == null)
        {
            _winMainMenu = (Window)_wmMain.FindChild("MainMenu", false);
            _winHud = (Window)_wmMain.FindChild("HUD", false);
            if (_winMainMenu == null || _winHud == null)
            {
                GD.PushError("WorldManager: missing windows...");
                return;
            }
        }
        if (_worldSaveListScene == null)
        {
            _worldSaveListScene = (PackedScene)GD.Load("res://UI/UIElements/world_save.tscn");
            if (_worldSaveListScene == null)
            {
                GD.PushError("WorldManager: No Packed Scene found for worldSaveListScene...");
                return;
            }
        }
        if (_worldListHolder == null)
        {
            _worldListHolder = GetParent().FindChild("WorldListHolder");
            if (_worldListHolder == null)
            {
                GD.PushError("WorldManager: No node found for worldListHolder...");
                return;
            }
        }
        if (_cancelBtn == null || _loadBtn == null || _newGameBtn == null)
        {
            _cancelBtn = (TextureButton)GetParent().FindChild("CancelBtn");
            _loadBtn = (TextureButton)GetParent().FindChild("LoadBtn");
            _newGameBtn = (TextureButton)GetParent().FindChild("NewGame");
            if (_cancelBtn == null || _loadBtn == null || _newGameBtn == null)
            {
                GD.PushError("WorldManager: missing buttons...");
                return;
            }
        }

        if(_deleteBtn == null || _saveBtn == null)
        {
            _deleteBtn = GetParent().FindChild("Trash-Btn") as TextureButton;
            _saveBtn = GetParent().FindChild("SaveBtn") as TextureButton;
            if (_deleteBtn == null || _saveBtn == null)
            {
                GD.PushError("WorldManager: no node found for trash/save btns...");
                return;
            }
        }
        if(_mmBackground == null)
        {
            _mmBackground = GetParent().FindChild("Background-WM") as Control;

            if (_mmBackground == null)
            {
                GD.PushError("WorldManager: no node found for background...");
                return;
            }
        }
        
        #endregion
        #region Connect buttons

        if (!_cancelBtn.IsConnected(BaseButton.SignalName.ButtonUp, Callable.From(Cancel)))
        {
            _cancelBtn.Connect(BaseButton.SignalName.ButtonUp, Callable.From(Cancel));
        }
        if (!_loadBtn.IsConnected(BaseButton.SignalName.ButtonUp, Callable.From(LoadSelectedWorld)))
        {
            _loadBtn.Connect(BaseButton.SignalName.ButtonUp, Callable.From(LoadSelectedWorld));
        }
        if (!_newGameBtn.IsConnected(BaseButton.SignalName.ButtonUp, Callable.From(NewGameBtnCall)))
        {
            _newGameBtn.Connect(BaseButton.SignalName.ButtonUp, Callable.From(NewGameBtnCall));
        }
        if (!_deleteBtn.IsConnected(BaseButton.SignalName.ButtonUp, Callable.From(DeleteSelectedWorlds)))
        {
            _deleteBtn.Connect(BaseButton.SignalName.ButtonUp, Callable.From(DeleteSelectedWorlds));
        }
        if (!_saveBtn.IsConnected(BaseButton.SignalName.ButtonUp, Callable.From(SaveSelectedOverwrite)))
        {
            _saveBtn.Connect(BaseButton.SignalName.ButtonUp, Callable.From(SaveSelectedOverwrite));
        }

        #endregion

        _currentlySelected = new List<WorldSaveTitle>();
        _loadBtn.Disabled = true;
        _saveBtn.Disabled = true;
        _deleteBtn.Disabled = true;
        _UIManager = ((UIManager)_wmMain.GetParent());
        DataInstance = (Data)_UIManager.GetParent().FindChild("Data");
    }

    /// <summary>
    /// Calls new game from the world manager
    /// </summary>
    private void NewGameBtnCall()
    {
        GD.Print("WorldManager: new game btn pressed");
        if(GameManager.Singleton.CurrentGameState == GameManager.GameState.MainMenu)
        {
            _UIManager.PopUpManager.DisplayInputPopUp("Enter new world name:", Callable.From(NewWorldConfirmed));
            // continue as normal in new world
        }
        else
        {
            // otherwise we need to create a new save for the current game...
            _UIManager.PopUpManager.DisplayInputPopUp("Save as:", Callable.From(SaveAs));
        }
    }
    
    void SaveAs()
    {
        string input = _UIManager.PopUpManager.LastInput;
        if (input.Length == 0)
        {
            _UIManager.PopUpManager.DisplayError("Error: input", "Input cannot be blank... Try filling out the input :)");
            return;
        }

        DataInstance.Save(input);
        RefreshSaves();
        _UIManager.PopUpManager.DisplayInfoPopUp("Successfully Saved!");
        _UIManager.PauseMenu.GameSavedTrigger();
    }

    void SaveSelectedOverwrite()
    {
        _UIManager.PopUpManager.DisplayConfirmPopUp("Are you sure?\nThis will overwrite the save data...", Callable.From(SaveSelectedOverwriteConfirmed));
    }

    void SaveSelectedOverwriteConfirmed()
    {
        GD.Print("WorldManager: overwriting save for selected world");
        DataInstance.Save(_currentlySelected.First().Title);
        _UIManager.PopUpManager.DisplayInfoPopUp("Saved successfully...");
        _UIManager.PauseMenu.GameSavedTrigger();
    }

    /// <summary>
    /// Called when the button (cancel) is pressed
    /// </summary>
    private void Cancel()
    {
        GD.Print("Settings: Back Button Pressed");
        if (GameManager.Singleton.CurrentGameState == GameManager.GameState.MainMenu)
        {
            _wmMain.ShowWindow(_winMainMenu);
        }
        else
        {
            _wmMain.HideOnly("WorldManager");
        }
        
    }

    /// <summary>
    /// load called and refreshes saves
    /// </summary>
    public void CallLoad()
    {
        GD.Print("WorldManager: load called");
        _currentlySelected = new List<WorldSaveTitle>();
        _loadBtn.Disabled = true;
        _mmBackground.Visible = GameManager.Singleton.CurrentGameState == GameManager.GameState.MainMenu;

        if (_testWorldList) Test();
        else RefreshSaves();
    }

    /// <summary>
    /// Removes all current worlds from list and replaces with with new ones that are current
    /// </summary>
    private void RefreshSaves()
    {
        foreach (var n in _worldListHolder.GetChildren()) _worldListHolder.RemoveChild(n);
        foreach (string worldName in DataInstance.RetrieveAllValidSavesAsList())
        {
            var inst = _worldSaveListScene.Instantiate();
            _worldListHolder.AddChild(inst);
            var wst = (WorldSaveTitle)inst;
            var title = worldName;
            var date = DataInstance.GetLastSavedFromFile(worldName);
            wst.UpdateWorldSaveTitle(title, date);
            wst.BindButtonToManager(this);
        }
    }

    /// <summary>
    /// If test is enabled - displayed a specified amounf of test worlds
    /// </summary>
    private void Test()
    {
        foreach (var n in _worldListHolder.GetChildren()) _worldListHolder.RemoveChild(n);
        for (var i = 0; i < _testAmount; i++)
        {
            var inst = _worldSaveListScene.Instantiate();
            _worldListHolder.AddChild(inst);
            var wst = (WorldSaveTitle)inst;
            var title = "Lorem ipsum dolar " + i;
            var r = new Random();
            var date = $"{r.Next(0, 10)}{r.Next(0, 10)}-{r.Next(0, 10)}{r.Next(0, 10)}-{r.Next(0, 10)}{r.Next(0, 10)}{r.Next(0, 10)}{r.Next(0, 10)} {r.Next(0, 10)}{r.Next(0, 10)}:{r.Next(0, 10)}{r.Next(0, 10)}:{r.Next(0, 10)}{r.Next(0, 10)}";
            wst.UpdateWorldSaveTitle(title, date);
            wst.BindButtonToManager(this);
        }
    }

    /// <summary>
    /// Main menu new called and refreshes saves
    /// </summary>
    public void CallNew()
    {
        _UIManager.PopUpManager.DisplayInputPopUp("Enter new world name:", Callable.From(NewWorldConfirmed));
        CallLoad();
    }

    void DeleteSelectedWorlds()
    {
        GD.Print("WorldManager: delete btn pressed");
        _UIManager.PopUpManager.DisplayConfirmPopUp("Are you sure you want to delete \nthese worlds. It cannot be undone...", Callable.From(DeleteSelectedWorldsConfirmed));
    }

    void DeleteSelectedWorldsConfirmed()
    {
        GD.Print("WorldManager: delete selected worlds confirmed");
        foreach(WorldSaveTitle wst in _currentlySelected)
        {
            DeleteWorld(wst);
        }
        RefreshSaves();
        _currentlySelected = new List<WorldSaveTitle>();
        ListedWorldClicked(null);
    }

    void DeleteWorld(WorldSaveTitle wst)
    {
        DataInstance.DeleteSave(wst.Title);
    }

    /// <summary>
    /// Call back for when the input for a new world is confirmed
    /// </summary>
    public void NewWorldConfirmed()
    {
        string input = _UIManager.PopUpManager.LastInput;
        if (input.Length == 0)
        {
            _UIManager.PopUpManager.DisplayError("Error: input", "Input cannot be blank... Try filling out the input :)");
            return;
        }

        DataInstance.NewGame(input);
        RefreshSaves();
    }

    /// <summary>
    /// Loads the selected world from data and brings user to game
    /// </summary>
    private void LoadSelectedWorld()
    {
        if (GameManager.Singleton.CurrentGameState == GameManager.GameState.MainMenu)
        {
            LoadSelectedWorldConfirmed();
        }
        else
        {
            _UIManager.PopUpManager.DisplayConfirmPopUp("Are you sure?\nAny Unsaved Progress will be lost...", Callable.From(LoadSelectedWorldConfirmed));
        }
        
    }

    void LoadSelectedWorldConfirmed()
    {
        GD.Print("WorldManager: loading selected world");
        GameManager.Singleton.SetGameState(GameManager.GameState.InGame);
        _wmMain.ShowWindow(_winHud);
        DataInstance.Load(_currentlySelected.First().Title);
        _UIManager.PauseMenu.SetPause(false);
    }

    /// <summary>
    /// Adds or removes a world from currently selected worlds
    /// </summary>
    public void ListedWorldClicked(WorldSaveTitle wst)
    {
        if(wst != null)
        {
            var currentState = wst.GetButtonState();
            GD.Print($"ListedWorldClicked: {wst.Title} received with state {currentState}");
            if (currentState == false) _currentlySelected.Remove(wst);
            if (currentState) _currentlySelected.Add(wst);
        }

        if (_currentlySelected.Count > 0)
        {
            _deleteBtn.Disabled = false;
        }
        else
        {
            _deleteBtn.Disabled=true;
        }


        if (_currentlySelected.Count == 0 || _currentlySelected.Count > 1)
        {
            _loadBtn.Disabled = true;
            _saveBtn.Disabled = true;
        }
        else if (_currentlySelected.Count == 1)
        {
            _loadBtn.Disabled = false;
            if (GameManager.Singleton.CurrentGameState == GameManager.GameState.MainMenu)
            {
                _saveBtn.Disabled = true; // cant save in main menu
            }
            else
            {
                _saveBtn.Disabled = false; // can save while in game
            }
        }
        else 
        {
            GD.PushError("WorldManger: invalid amount in list...");
        }
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