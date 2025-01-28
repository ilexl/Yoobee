using ArchitectsInVoid.Inventory;
using ArchitectsInVoid.UI;
using Godot;
using System.Collections.Generic;

[Tool]
public partial class InventoryManager : Node
{
    [Export] PackedScene _inventoryUIWindow;
    [Export] Control _inventoryWindowParent;
    [Export] Control _inventoriesList;
    [Export] PackedScene _AccessableInventoryTitle;
    [Export] Control _inventoriesListParent;
    [Export] bool _inventoryListShown;
    List<Inventory> _accessableInventories;
    InventorySlot _activeSlot;
    Item _currentItemInCursor;

    public static InventoryManager Singleton;

    public override void _Ready()
    {
        if (Engine.IsEditorHint()) { return; } // do NOT run when not in game
        Singleton = this;
        _activeSlot = null;
        _inventoriesList.Hide();
        _inventoryListShown = false;
        GetAccessableInventories();
        ClearWindows();
        CreateWindowsForAccessableInventories();
    }

    void TryShowInventoryList()
    {
        GD.Print("InventoryManager: trying to show list of inventories");
        if (GameManager.Singleton.CurrentGameState == GameManager.GameState.MainMenu)
        {
            GD.Print("InventoryManager: cant show list while in main menu");
            return;
        }
        _inventoryListShown = true;
        _inventoriesList.Show();
        UpdateAccessableInventoryList();
        GD.Print("InventoryManager: list of inventories now being shown");

    }

    private void UpdateAccessableInventoryList()
    {
        foreach(var child in _inventoriesListParent.GetChildren())
        {
            child.QueueFree(); // remove list
        }
        GetAccessableInventories();
        foreach(var inventory in _accessableInventories)
        {
            var title = _AccessableInventoryTitle.Instantiate();
            AccessableInventoryTitle ait = title as AccessableInventoryTitle;
            if(ait == null)
            {
                GD.PushError("InventoryManager: AccessableInventoryTitle was null...");
                return;
            }
            ait.Setup(Callable.From((AccessableInventoryTitle aitExternal) => ToggleInventoryFromList(aitExternal)), inventory);
            _inventoriesListParent.AddChild(title);
            
        }
    }

    public void CloseInventoryFromWindow(InventoryWindow iw)
    {
        foreach (var child in _inventoriesListParent.GetChildren())
        {
            AccessableInventoryTitle ait = child as AccessableInventoryTitle;
            if(ait.GetInventory().InventoryName == iw.Inventory.InventoryName)
            {
                ait.UntoggleFromWindow();
            }
        }
        if (_activeSlot != null)
        {
            _activeSlot.Refresh();
        }
    }

    public void ToggleInventoryFromList(AccessableInventoryTitle ait)
    {
        bool show = ait.ButtonState();
        if (show)
        {
            GD.Print($"InventoryManager: showing inventory for {ait.GetInventory().InventoryName}");
            var inventoryWindow = GetInventoryWindow(ait);
            inventoryWindow.Shown(true);
        }
        else
        {
            GD.Print($"InventoryManager: hiding inventory for {ait.GetInventory().InventoryName}");
            var inventoryWindow = GetInventoryWindow(ait);
            inventoryWindow.Shown(false);
        }

    }

    private InventoryWindow CreateInventoryWindow(Inventory inventory)
    {
        var newWindow = _inventoryUIWindow.Instantiate();
        var inventoryWindow = newWindow as InventoryWindow;
        inventoryWindow.Inventory = inventory;
        inventoryWindow.CallClose = Callable.From((InventoryWindow iw) => CloseInventoryFromWindow(iw));
        _inventoryWindowParent.AddChild(newWindow);
        return inventoryWindow;
    }

    private void CreateWindowsForAccessableInventories()
    {
        foreach(var inventory in _accessableInventories)
        {
            InventoryWindow inventoryWindow = null;
            foreach (var window in _inventoryWindowParent.GetChildren())
            {
                InventoryWindow iWindow = window as InventoryWindow;
                if (iWindow.Inventory.InventoryName == inventory.InventoryName)
                {
                    GD.Print("InventoryManager: window found");
                    inventoryWindow = iWindow;
                }
            }
            if (inventoryWindow == null)
            {
                GD.Print("InventoryManager: no window found - creating a new one");
                inventoryWindow = CreateInventoryWindow(inventory);
                inventoryWindow.Shown(false);
            }
        }
        
    }

    private InventoryWindow GetInventoryWindow(AccessableInventoryTitle ait)
    {
        InventoryWindow inventoryWindow = null;
        foreach (var window in _inventoryWindowParent.GetChildren())
        {
            InventoryWindow iWindow = window as InventoryWindow;
            if (iWindow.Inventory.InventoryName == ait.GetInventory().InventoryName)
            {
                GD.Print("InventoryManager: window found");
                inventoryWindow = iWindow;
            }
        }
        if (inventoryWindow == null)
        {
            GD.Print("InventoryManager: no window found - creating a new one");
            inventoryWindow = CreateInventoryWindow(ait.GetInventory());
        }
        return inventoryWindow;
    }


    private void GetAccessableInventories()
    {
        _accessableInventories = new List<Inventory>();
        GD.Print("InventoryManager: temporary inventories in memory...");

        var test1 = new Inventory();
        var test2 = new Inventory();

        List<Item> items1 = new List<Item>
        {
            new Item(Item.Type.IronPlate), new Item(Item.Type.IronPlate), new Item(Item.Type.IronPlate),
            new Item(Item.Type.IronPlate), new Item(Item.Type.IronPlate), new Item(Item.Type.IronPlate),
            new Item(), new Item(), new Item(), new Item(), new Item(), new Item(), new Item(),
            new Item(), new Item(), new Item(), new Item(), new Item(), new Item(), new Item()
        };
        List<Item> items2 = new List<Item>
        {
            new Item(Item.Type.CopperPlate), new Item(Item.Type.CopperPlate), new Item(Item.Type.CopperPlate),
            new Item(Item.Type.CopperPlate), new Item(Item.Type.CopperPlate), new Item(Item.Type.CopperPlate),
            new Item(), new Item(), new Item(), new Item(), new Item(), new Item(), new Item(),
            new Item(), new Item(), new Item(), new Item(), new Item(), new Item(), new Item()
        };

        test1.Setup("Test Inventory 1", items1);
        test2.Setup("Test Inventory 2", items2);

        _accessableInventories.Add(test1);
        _accessableInventories.Add(test2);
    }

    public void ClearWindows()
    {
        foreach (var window in _inventoryWindowParent.GetChildren())
        {
            window.QueueFree();
        }
    }

    public void CancelItemInCursor()
    {
        GD.Print("InventoryManager: cursor hidden - putting back item into previous slot");
        if (_activeSlot != null)
        {
            if (_activeSlot.GetItem().GetCurrentItem() == Item.Type.None)
            {
                _activeSlot.SetItem(_currentItemInCursor);
            }
            else
            {
                _activeSlot.GetItem().ChangeAmount(_currentItemInCursor.GetCurrentAmount());
            }
            _activeSlot.Refresh();
            _activeSlot = null;
        }
        _currentItemInCursor = null;
        UpdateInventories();
        ResetCursor();

    }

    public void HideInventoryList()
    {
        _inventoriesList.Hide();
        _inventoryListShown = false;
        GD.Print("InventoryManager: list of inventories now being hidden");
    }

    public override void _Input(InputEvent @event)
    {
        if (GameManager.Singleton.CurrentGameState == GameManager.GameState.MainMenu)
        {
            return;
        }
        if(Pause.Singleton.IsPaused == true)
        {
            return;
        }
        if (@event.IsActionPressed("interactions_inventory"))
        {
            if (_inventoryListShown)
            {
                HideInventoryList();
            }
            else
            {
                TryShowInventoryList();
            }
        }
    }

    public void SlotActivated(InventorySlot inventorySlot)
    {
        if(_activeSlot == null)
        {
            if (inventorySlot.GetItem().GetCurrentItem() == Item.Type.None)
            {
                // no point in activating a blank slot if it will do nothing
                ResetCursor();
                return;
            }
            // take item from slot
            _activeSlot = inventorySlot;
            _currentItemInCursor = inventorySlot.GetItem();
            inventorySlot.SetItem(new Item()); // set slot as blank
            SetCursorToImage(_currentItemInCursor);
        }
        else
        {
            // temp item in cursor (hold)
            var temp = _currentItemInCursor;

            // take item from slot
            if (inventorySlot.GetItem().GetCurrentItem() == _currentItemInCursor.GetCurrentItem())
            {
                // try place some or all of cursor item into slot
                if(inventorySlot.GetItem().GetFreeAmount() >= _currentItemInCursor.GetCurrentAmount())
                {
                    // enough free room to clear cursor
                    inventorySlot.GetItem().ChangeAmount(_currentItemInCursor.GetCurrentAmount());
                    _activeSlot = null;
                    _currentItemInCursor = null;
                    ResetCursor();
                }
                else
                {
                    // not enough free room but will still drop off what we can
                    int freeRoom = inventorySlot.GetItem().GetFreeAmount();
                    inventorySlot.GetItem().ChangeAmount(freeRoom);
                    _currentItemInCursor.ChangeAmount(-freeRoom);
                }
            }
            else
            {
                if (inventorySlot.GetItem().GetCurrentItem() == Item.Type.None)
                {
                    _activeSlot = null;
                    _currentItemInCursor = null;
                    ResetCursor();
                }
                else
                {
                    _activeSlot = inventorySlot;
                    _currentItemInCursor = inventorySlot.GetItem();
                    SetCursorToImage(_currentItemInCursor);
                }
                inventorySlot.SetItem(temp); // set slot as temp/what was held in the mouse
            }
        }
        UpdateInventories();
        inventorySlot.Refresh();
        if(_activeSlot != null)
        {
            _activeSlot.Refresh();
        }
    }

    public void SlotActivated(InventorySlot inventorySlot, int amount)
    {
        if (amount == inventorySlot.GetItem().GetCurrentAmount())
        {
            SlotActivated(inventorySlot);
            return; // if full amount - use normal function
        }
        if (_activeSlot == null)
        {
            if (inventorySlot.GetItem().GetCurrentItem() == Item.Type.None)
            {
                // no point in activating a blank slot if it will do nothing
                ResetCursor();
                return;
            }
            // take item from slot
            _activeSlot = inventorySlot;
            _currentItemInCursor = new Item(inventorySlot.GetItem().GetCurrentItem(), amount);
            inventorySlot.GetItem().ChangeAmount(-amount);
            SetCursorToImage(_currentItemInCursor);
        }
        UpdateInventories();
        inventorySlot.Refresh();
        if (_activeSlot != null)
        {
            _activeSlot.Refresh();
        }
    }

    void UpdateInventories()
    {
        foreach(var child in _inventoryWindowParent.GetChildren())
        {
            var inventoryWindow = child as InventoryWindow;
            inventoryWindow.UpdateInventoryFromSlots();
        }
    }

    void ResetCursor()
    {
        GD.Print("InventoryManager: reset cursor");
        Input.SetCustomMouseCursor(null);
    }

    void SetCursorToImage(Item item)
    {
        GD.Print("InventoryManager: custom cursor");
        Input.SetCustomMouseCursor(Item.GetItemDataTexture(item.GetCurrentItem()));
    }

    internal bool IsAnyWindowActive()
    {
        foreach(var child in _inventoryWindowParent.GetChildren())
        {
            var window = child as InventoryWindow;
            if (window.IsVisible())
            {
                return true;
            }
        }
        return false;
    }
}
