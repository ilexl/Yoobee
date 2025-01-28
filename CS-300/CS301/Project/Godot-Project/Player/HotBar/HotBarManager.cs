using System;
using System.Collections.Generic;
using ArchitectsInVoid.VesselComponent;
using Godot;

namespace ArchitectsInVoid.Player.HotBar;

public partial class HotBarManager : Node
{
	// Hotbar array made accessible to everything
	[Export] public int CurrentHotbarIndex;
	public PackedScene[][] HotBars;
	
	
	// Reference to the component creator for use with any scenes that inherit from PlaceableComponent
	private ComponentCreation.ComponentCreator _componentCreator;
	private PackedScene _selectedScene;
	public int HotBarIndex;

	public static HotBarManager Singleton;

	private List<Texture2D> _hotbarTextures = new();
	public delegate void HotbarSlotChanged(int index);
	public static HotbarSlotChanged HotbarSlotChangedEvent;
	
	public delegate void HotbarTextureChanged(Image texture, int index);
	public static HotbarTextureChanged HotbarTextureChangedEvent;
	
	public override void _Ready()
	{
		HotBars = new PackedScene[10][] 
		{
        new PackedScene[10],
        new PackedScene[10],
        new PackedScene[10],
        new PackedScene[10],
        new PackedScene[10],
        new PackedScene[10],
        new PackedScene[10],
        new PackedScene[10],
        new PackedScene[10],
        new PackedScene[10]
        };
		CurrentHotbarIndex = 1;

        if (Engine.IsEditorHint()) { return; } // do NOT run when not in game
        _componentCreator = GetNode<ComponentCreation.ComponentCreator>("../ComponentCreator");
		Singleton = this;
    }


	private bool _hotbarIconsNeedUpdating = true;
	public override void _PhysicsProcess(double delta)
	{
		if (_hotbarIconsNeedUpdating)
		{
			for (var index = 0; index < 10; index++)
			{
				var scene = HotBars[CurrentHotbarIndex][index];
				if (scene == null)
				{
                    HotbarTextureChangedEvent(null, index);
					continue;
                }
				PlaceableComponent comp = (PlaceableComponent)scene.Instantiate();
				if (comp == null)
				{
                    HotbarTextureChangedEvent(null, index);
                    continue;
                }
				HotbarTextureChangedEvent(comp._Thumbnail, index);
			}

			_hotbarIconsNeedUpdating = false;
			//GD.Print("Hotbar icons updated");
		}


		for (var i = 0; i < 10; i++) // Assuming "hotbar_0" to "hotbar_9"
		{
			if (Input.IsActionJustPressed($"hotbar_{i}"))
			{
				HotbarSlotChangedEvent(i);
				_selectedScene = HotBars[CurrentHotbarIndex][i];
				HotBarIndex = i;

				if (_selectedScene == null) // Hotbar slot is empty
				{
					GD.Print("Hotbar slot is empty");
					_componentCreator.SelectedComponentScene = null;
					return;
				}

				var instance = _selectedScene.Instantiate();
				_componentCreator.SelectedComponentScene = instance is PlaceableComponent ? _selectedScene : null;
				instance.Free(); // Immediately delete instance from memory as it is no longer required
								 // Because of this, it is important that hotbar scenes have nothing in their constructor

			}
		}
	}

    public void SetSlot(int slot, PackedScene component)
    {
		GD.Print("Setting slot");
		GD.Print(slot);
		HotBars[CurrentHotbarIndex][slot] = component;
		_hotbarIconsNeedUpdating = true;
    }

    internal void SelectHotbar(int hotbar)
    {
		CurrentHotbarIndex = hotbar;
        _hotbarIconsNeedUpdating = true;
    }
}