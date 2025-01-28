using ArchitectsInVoid.Inventory;
using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Inventory for use in player, vessels blocks, etc...
/// </summary>
public partial class Inventory : Node
{
    [Export] string _inventoryName;
    private List<Item> _items;
    public string InventoryName { get => _inventoryName; }
    public List<Item> Items { get => _items; }

    public void Setup(string inventoryName, List<Item> items)
    {
        _inventoryName = inventoryName;
        _items = items; 
    }
}
