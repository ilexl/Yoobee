using Godot;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// A mananger for keeping track of the games current state
/// </summary>
public partial class GameManager : Node
{
    private static GameManager _singleton;
    public static GameManager Singleton => _singleton;

    public GameState CurrentGameState => _currentGameState;
    private GameState _currentGameState;

    public enum GameState
    {
        MainMenu = 0,
        InGame = 1
    };

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        
        
        
        
        
        
        
        
        if (Engine.IsEditorHint()) { return; } // do NOT run when not in game
        if (_singleton != null)
        {
            GD.PushError("GameManager: singleton already exists.");
        }
        _singleton = this;
        SetGameState(GameState.MainMenu);
    }

    public void SetGameState(GameState state)
    {
        _currentGameState = state;
        switch (_currentGameState)
        {
            case GameState.MainMenu:
                {
                    GD.Print("GameManager: now in main menu");
                    break;
                }
            case GameState.InGame:
                {
                    GD.Print("GameManager: now in game");
                    break;
                }
            default:
                {
                    GD.PushError("GameManager: no such GameState...");
                    break;
                }
        }
    }
}
