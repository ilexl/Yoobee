using ArchitectsInVoid.Debug;
using ArchitectsInVoid.Interactables;
using ArchitectsInVoid.Player;
using Godot;

namespace ArchitectsInVoid.Vessels.VesselComponents.Cockpit;

public partial class CockpitBehavior : Node
{
    [Export] private InteractableObject _interactor;
    [Export] public Cockpit PhysicalCockpit;


    private PlayerController _mountedPlayer;
    public override void _Ready()
    {
        _interactor.Interacted += BreakInteraction;
    }
    
    private void BreakInteraction(PlayerController player, InteractableObject.InteractionType type)
    {
        var position = PhysicalCockpit.GlobalPosition;
        var basis = PhysicalCockpit.Transform.Basis;
        var globalBasis = PhysicalCockpit.GlobalTransform.Basis;
        
        var halfScale =  globalBasis.Orthonormalized() * (basis.Scale / 2) ;
        
        switch (type)
        {
            case InteractableObject.InteractionType.LookedAt:
                DebugDraw.Box(position - halfScale, position + halfScale, globalBasis.Orthonormalized(), Colors.Aqua, drawOnTop:true);
                break;
            case InteractableObject.InteractionType.UseAction:
                MountPlayer(player);
                break;
        }
    }


    private void MountPlayer(PlayerController newPlayer)
    {
        if (_mountedPlayer != null) return;
        _mountedPlayer = newPlayer;
        newPlayer.MountToCockpit(this);
    }

    public override void _Process(double delta)
    {
        
    }
}