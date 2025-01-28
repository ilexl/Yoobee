using Godot;
using Godot.Collections;
using System;

namespace ArchitectsInVoid.WorldData;

[Tool]
public partial class WorldDataManager : Node
{
	[Export] PlayerData _dataPlayer;
	[Export] VesselData _dataVessel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        if (Engine.IsEditorHint()) { return; } // do NOT run when not in game
        if (_dataPlayer == null)
        {
            _dataPlayer = (PlayerData)FindChild("Players", recursive: false);
            if (_dataPlayer == null)
            {
                GD.PushError("WorldDataManager: missing instances of data...");
            }
        }
        if(_dataVessel == null)
        {
            _dataVessel = (VesselData)FindChild("Ships", recursive: false);
            if (_dataVessel == null)
            {
                GD.PushError("WorldDataManager: missing instances of data...");
            }
        }
    }

    public Godot.Collections.Array AddInspectorButtons()
    {
        var buttons = new Godot.Collections.Array();

        var btnRefresh = new Dictionary
            {
                { "name", "Refresh" },
                { "icon", GD.Load("res://Testing/InspectorButtons/icon.svg") },
                { "pressed", Callable.From(_Ready) }
            };
        buttons.Add(btnRefresh);

        return buttons;
    }

    // add data managers below
    // data managers MUST be in correct/same ORDER
    // current order goes:
    //      player data
    //      ship data
    //      ...

    public void _Load(FileAccess file)
	{
        _Clear(); // clear the current objects and make room for the new

        // load directly from file ---------------------------------------------

        _dataPlayer._Load(file);
        _dataVessel._Load(file);
    }
	public void _Save(FileAccess file)
	{
        _dataPlayer._Save(file);
        _dataVessel._Save(file);
    }

    public void _DiscardLoadPast(FileAccess file)
    {
        _dataPlayer._DiscardLoadPast(file);
        _dataVessel._DiscardLoadPast(file);
    }

    // WARNING - CALLING THIS WILL UNLOAD ALL THE DATA IN GAME. DO NOT CALL THIS :)
    public void _Clear()
	{
        GD.Print("WorldDataManager: clearing all data currently loaded...");
        foreach(var child in _dataPlayer.GetChildren())
        {
            _dataPlayer.RemoveChild(child);
        }
        foreach (var child in _dataVessel.GetChildren())
        {
            _dataVessel.RemoveChild(child);
        }
    }

    internal void NewGame(FileAccess file)
    {
        _dataPlayer._NewGame(file);
        _dataVessel._NewGame(file);
    }
}
