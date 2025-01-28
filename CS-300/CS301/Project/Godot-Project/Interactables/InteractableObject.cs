using ArchitectsInVoid.Player;
using Godot;

namespace ArchitectsInVoid.Interactables;

/// <summary>
/// Script for making physical objects glow and pop up a prompt showing it object can be interacted with
/// </summary>
public partial class InteractableObject : StaticBody3D
{
    public enum InteractionType
    {
        None,
        UseAction,
        LookedAt
    }
    
    public delegate void DInteracted(PlayerController player, InteractionType type);
    public DInteracted Interacted;
}