using ArchitectsInVoid.Inventory;
using Godot;
using System;

public partial class InventorySlot : Control
{
    [Export] TextureRect _slotIcon;
    [Export] TextureButton _button;
    [Export] RichTextLabel _amountText;
    [Export] ItemSlotSlider _slider;
    Item _item;
    bool _rightDown = false;

    public override void _Ready()
    {
        _button.Connect(BaseButton.SignalName.ButtonDown, Callable.From(Pressed));
        _button.Connect(BaseButton.SignalName.ButtonUp, Callable.From(Pressed));
    }

    void Pressed()
    {
        if (Input.IsMouseButtonPressed(MouseButton.Left))
        {
            InventoryManager.Singleton.SlotActivated(this);
            GD.Print("InventorySlot: left click down");
            return;
        }
        else if (Input.IsMouseButtonPressed(MouseButton.Right))
        {
            // right mouse button down
            GD.Print("InventorySlot: right click down");
            if(_item.GetCurrentItem() == Item.Type.None) { return; }
            _rightDown = true;
            _slider.SetStart(GetViewport().GetMousePosition(), _item.GetCurrentAmount());
            _slider.Show();
            return;
        }
        else if(_rightDown)
        {
            GD.Print("InventorySlot: right click up");
            _rightDown = false;

            int amount = _slider.GetSliderAmount();
            _slider.Hide();
            if(amount > 0)
            {
                InventoryManager.Singleton.SlotActivated(this, amount);
            }

            // right mouse button up
            return;
        }
    }

    public void SetItem(Item item)
    {
        _item = item;
        Refresh();
    }

    public void Refresh()
    {
        _amountText.Text = _item.ShortHandAmount();
        _slotIcon.Texture = _item.GetIcon();
    }

    public Item GetItem()
    {
        return _item;
    } 

}
