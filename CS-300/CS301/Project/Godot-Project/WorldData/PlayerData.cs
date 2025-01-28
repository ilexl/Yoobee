using Godot;
using System;
using ArchitectsInVoid.Player;

[Tool]



public partial class PlayerData : Node
{
    [Export] private PackedScene _playerPrefab;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        if (Engine.IsEditorHint()) { return; } // do NOT run when not in game
        if (_playerPrefab == null)
        {
            _playerPrefab = (PackedScene)GD.Load("res://Scenes/BlankPlayer.tscn");
            if (_playerPrefab == null)
            {
                GD.PushError("PlayerData: No Packed Scene found for playerBlank...");
                return;
            }
        }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void _Load(FileAccess file)
    {
        // get amount of players
        int playerAmount = file.GetVar().AsInt32();
        for (int i = 0; i < playerAmount; i++)
        {
            PlayerController player = (PlayerController)_playerPrefab.Instantiate();
            AddChild(player);
            
            Vector3 position = file.GetVar().AsVector3();
            Vector3 rotation = file.GetVar().AsVector3();
            Vector3 linearVelocity = file.GetVar().AsVector3();
            Vector3 angularVelocity = file.GetVar().AsVector3();
            Vector3 headRotation = file.GetVar().AsVector3();
            bool dampeners = file.GetVar().AsBool();
            bool jetpack = file.GetVar().AsBool();
            
            player.Body.Position = position;
            player.Body.Rotation = rotation;
            player.Body.LinearVelocity = linearVelocity;
            player.Body.AngularVelocity = angularVelocity;
            player.Head.Rotation = headRotation;
            player.Dampeners = dampeners;
            player.Jetpack = jetpack;
            
            
        }
        if (playerAmount == 0)
        {
            var player = _playerPrefab.Instantiate();
            AddChild(player);
        }
    }

    public void _DiscardLoadPast(FileAccess file)
    {
        // get amount of players
        int playerAmount = file.GetVar().AsInt32();
        for (int i = 0; i < playerAmount; i++)
        {
            _ = file.GetVar().AsVector3();
            _ = file.GetVar().AsVector3();
            _ = file.GetVar().AsVector3();
            _ = file.GetVar().AsVector3();
            _ = file.GetVar().AsVector3();
            _ = file.GetVar().AsBool();
            _ = file.GetVar().AsBool();
            
        }
    }

    public void _Save(FileAccess file)
    {
        var players = GetChildren();

        file.StoreVar(players.Count);
        
        foreach (var player in players)
        {
            PlayerController playerController = player as PlayerController;
            Vector3 position = playerController.Body.Position;
            Vector3 rotation = playerController.Body.Rotation;
            Vector3 linearVelocity = playerController.Body.LinearVelocity;
            Vector3 angularVelocity = playerController.Body.AngularVelocity;
            Vector3 headRotation = playerController.Head.Rotation;
            bool dampeners = playerController.Dampeners;
            bool jetpack = playerController.Jetpack;
            
            file.StoreVar(position);
            file.StoreVar(rotation);
            file.StoreVar(linearVelocity);
            file.StoreVar(angularVelocity);
            file.StoreVar(headRotation);
            file.StoreVar(dampeners);
            file.StoreVar(jetpack);
        }
        
    }

    internal void _NewGame(FileAccess file)
    {
        file.StoreVar(0);
    }
}
