using ArchitectsInVoid.Settings;
using ArchitectsInVoid.UI;
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manager for anything UI related
/// <br/>Contains: MainMenu, SettingsMenu, Settings, WorldManager, LoadingScreen, HUD, Pause, PopUp, WindowManager
/// </summary>
[Tool]
public partial class UIManager : Node
{
    #region Variables

    [Export] public MainMenu MainMenu;
	[Export] public SettingsMenu SettingsMenu;
	[Export] public WorldManager WorldMenu;
	[Export] public LoadingScreen LoadingMenu;
	[Export] public HUD HudMenu;
	[Export] public Pause PauseMenu;
    [Export] public PopUp PopUpManager;
	[Export] public WindowManager UIWindowManager;
    [Export] public InventoryManager UIInventoryManager;
    [Export] public ComponentSelectionUI ComponentSelection;
    [Export] public Settings SettingsManager;

    #endregion

    /// <summary>
    /// Called when the node enters the scene tree for the first time.
    /// </summary>
    public override void _Ready()
	{
        if (Engine.IsEditorHint()) { return; } // do NOT run when not in game
        _ = _GetDependents();
    }

    public override void _Notification(int what)
    {
        
        if (what == NotificationEditorPreSave)
        {
            GD.Print("UIManager: Unloading all SVGTextures");
            foreach (var child in Helper.SearchRecursive<Sprite2D>(this))
            {
                child.Texture = null;
            }
        }
        if (what == NotificationEditorPostSave)
        {
            GD.Print("UIManager: Reloading all SVGTextures");
            foreach (var child in Helper.SearchRecursive<Sprite2D>(this))
            {
                child.Call("_update_texture");
            }
            
        }
    }

    /// <summary>
    /// Ensures all managed nodes and scripts have been loaded and referenced
    /// <br/>Errors if something is missing...
    /// </summary>
    bool _GetDependents()
    {
        if (UIWindowManager == null)
        {
            UIWindowManager = (WindowManager)FindChild("WindowManager", recursive: false);

            if (UIWindowManager == null)
            {
                GD.PushError("UIManager: window manager not found...");
                return false;
            }
        }
        if (MainMenu == null)
        {
            MainMenu = (MainMenu)UIWindowManager.FindChild("MainMenuScript");

            if (MainMenu == null)
            {
                GD.PushError("UIManager: main menu not found...");
                return false;
            }
        }
        if (SettingsMenu == null)
        {
            SettingsMenu = (SettingsMenu)UIWindowManager.FindChild("SettingsManager");

            if (SettingsMenu == null)
            {
                GD.PushError("UIManager: settings menu not found...");
                return false;
            }
        }  
        if (WorldMenu == null)
        {
            WorldMenu = (WorldManager)UIWindowManager.FindChild("WorldManagerButtons");

            if (WorldMenu == null)
            {
                GD.PushError("UIManager: world manager not found...");
                return false;
            }
        }
        if (LoadingMenu == null)
        {
            LoadingMenu = (LoadingScreen)UIWindowManager.FindChild("LoadingScreenLogic");

            if (LoadingMenu == null)
            {
                GD.PushError("UIManager: loading screen not found...");
                return false;
            }
        }
        if (HudMenu == null)
        {
            HudMenu = (HUD)UIWindowManager.FindChild("HUDButtons");


            if (HudMenu == null)
            {
                GD.PushError("UIManager: HUD not found...");
                return false;
            }
        }
        if (PauseMenu == null)
        {
            PauseMenu = UIWindowManager.FindChild("PauseMenuButtons") as Pause;

            if (PauseMenu == null)
            {
                GD.PushError("UIManager: pause menu not found...");
                return false;
            }
        }
        if (PopUpManager == null)
        {
            PopUpManager = (PopUp)UIWindowManager.FindChild("PopUp");

            if (PopUpManager == null)
            {
                GD.PushError("UIManager: popup not found...");
                return false;
            }
        }
        if (UIInventoryManager == null)
        {
            UIInventoryManager = FindChild("Inventories") as InventoryManager;
            if (UIInventoryManager == null)
            {
                GD.PushError("UIManager: inventories not found...");
                return false;
            }
        }
        if (ComponentSelection == null)
        {
            ComponentSelection = FindChild("ComponentSelection") as ComponentSelectionUI;
            if (ComponentSelection == null)
            {
                GD.PushError("UIManager: component selection not found...");
                return false;
            }
        }
        if (SettingsManager == null)
        {
            SettingsManager = (Settings)this.GetParent().FindChild("Settings", false);
            if (SettingsManager == null)
            {
                GD.Print("UIManager: settings not found... likely called from UI not Game...");
                return true;
            }
            return true;
        }
        return true;
    }

    /// <summary>
    /// Calls _Ready to all scripts managed by this
    /// </summary>
    void _RefreshAllUIElements()
    {
        GD.Print("UIManager: getting required dependants");
        bool success = _GetDependents();

        if(UIWindowManager !=  null)
        {
            UIWindowManager.ManualRefresh();
        }

        if (success)
        {
            GD.Print("UIManager: required dependants being refreshed");

            MainMenu._Ready();
            SettingsMenu._Ready();
            WorldMenu._Ready();
            LoadingMenu._Ready();
            HudMenu._Ready();
            PauseMenu._Ready();
            ComponentSelection._Ready();
        }
    }

    /// <summary>
    /// Used for the Inspector Buttons plugin
    /// </summary>
    public Godot.Collections.Array AddInspectorButtons()
    {
        var buttons = new Godot.Collections.Array();

        var btnRefresh = new Dictionary
            {
                { "name", "Refresh All" },
                { "icon", GD.Load("res://Testing/InspectorButtons/icon.svg") },
                { "pressed", Callable.From(_RefreshAllUIElements) }
            };
        buttons.Add(btnRefresh);

        return buttons;
    }
}
