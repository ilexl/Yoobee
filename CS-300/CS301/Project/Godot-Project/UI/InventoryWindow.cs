using Godot;
using System;
using System.Collections.Generic;
using System.Net;

public partial class InventoryWindow : Node
{
    [Export] int _slotsAmount;
    [Export] PackedScene _inventorySlotScene;
    [Export] Window _window;
    [Export] VBoxContainer _containerVertical;
    Inventory _inventory;
    List<InventorySlot> _slots;
    private Callable _callClose;
    public Callable CallClose
    {
        set => _callClose = value;
    }
    public Inventory Inventory
    {
        get => _inventory;
        set
        {
            _inventory = value;
            _slots = new List<InventorySlot>();
            _slotsAmount = _inventory.Items.Count;
            RecalculateSlots();
            _window.Title = _inventory.InventoryName;
        }
    }

    public bool IsVisible()
    {
        return _window.Visible;
    }

    public override void _Ready()
    {
        _slots = new List<InventorySlot>();
        _window.SizeChanged += RecalculateSlots;
        RecalculateSlots();
        Shown(false);
    }

    public void Shown(bool shown)
    {
        _window.Visible = shown;
    }

    

    void RecalculateSlots()
    {
        foreach(var child in _containerVertical.GetChildren())
        {
            child.QueueFree();
        }
        
        double horizontalSlotsRaw = ((double)(_window.Size.X - 8)) / 100;
        int horizontalSlots = (int)System.Math.Round(horizontalSlotsRaw, MidpointRounding.ToZero);
        if (horizontalSlots != horizontalSlotsRaw)
        {
            Vector2I windowSize = _window.Size;
            windowSize.X = 8 + (horizontalSlots * 100);
            _window.Size = windowSize;
            return; // this function will be call again bc anyway...
        }

        double verticalSlotsRaw = ((double)_slotsAmount) / ((double)horizontalSlots);
        double verticalSlots = System.Math.Round(verticalSlotsRaw);
        if (verticalSlotsRaw.ToString().Contains(".") && verticalSlots < verticalSlotsRaw)
        {
            verticalSlots++;
        }

        _slots.Clear();
        int slotCounter = 0;
        for(int i = 0 ; i < verticalSlots; i++)
        {
            HBoxContainer containerH = new HBoxContainer();
            for(int j = 0 ; j < horizontalSlots; j++)
            {
                if (slotCounter < _slotsAmount)
                {
                    var slot = _inventorySlotScene.Instantiate();
                    containerH.AddChild(slot);
                    InventorySlot s = slot as InventorySlot;
                    s.SetItem(_inventory.Items[slotCounter]);
                    _slots.Add(s);
                    slotCounter++;
                }
            }
            containerH.AddThemeConstantOverride("separation", -2);
            _containerVertical.AddChild(containerH);
        }

    }


    public void CallCloseFW()
    {
        _callClose.Call(this);
    }

    public void UpdateInventoryFromSlots()
    {
        for(int i = 0 ; i < _slots.Count; i++)
        {
            _inventory.Items[i] = _slots[i].GetItem();
        }
    }
}
