using Godot;
using System;

public partial class AccessableInventoryTitle : Node
{
    [Export] TextureButton _btn;
    [Export] RichTextLabel _label;
    Callable _call;
    Inventory _inventory;

    public void Setup(Callable call, Inventory inventory)
    {
        _btn.Connect(BaseButton.SignalName.ButtonUp, Callable.From(Toggle));
        _label.Text = inventory.InventoryName;
        _call = call;
        _inventory = inventory;
    }

    private void Toggle()
    {
        GD.Print("AccessableInventoryTitle: toggle pressed");
        _call.Call(this);
    }

    public void UntoggleFromWindow()
    {
        _btn.SetPressedNoSignal(false);
    }

    public bool ButtonState()
    {
        return _btn.ButtonPressed;
    }

    public Inventory GetInventory()
    {
        return _inventory;
    }

}
