using ArchitectsInVoid.VesselComponent;
using Godot;
using System;

[Tool]
public partial class ComponentSelect : TextureButton
{
    [Export] public PackedScene Component;
    [Export] TextureRect _componentRect;
    [Export] TextureButton _componentButton;
    [Export] Control _highlightBtn;
    public void Setup(PackedScene component)
    {
        Component = component;
        if(Component != null )
        {
            var temp = component.Instantiate();
            var pc = temp as PlaceableComponent;
            if( pc != null )
            {
                _componentRect.Texture = ImageTexture.CreateFromImage(pc._Thumbnail);
            }
        }

        if(!_componentButton.IsConnected(BaseButton.SignalName.ButtonDown, Callable.From(BtnPressed)))
        {
            _componentButton.Connect(BaseButton.SignalName.ButtonDown, Callable.From(BtnPressed));
        }
        if (!_componentButton.IsConnected(BaseButton.SignalName.ButtonUp, Callable.From(BtnPressed)))
        {
            _componentButton.Connect(BaseButton.SignalName.ButtonUp, Callable.From(BtnPressed));
        }
        _highlightBtn.Hide();
    }

    void BtnPressed()
    {
        GD.Print("ComponentSelect: test");

        if (_componentButton.ButtonPressed)
        {
            GD.Print("ComponentSelect: button down");
            _highlightBtn.Show();
            Input.SetCustomMouseCursor(_componentRect.Texture);
        }
        else
        {
            GD.Print("ComponentSelect: button up");
            _highlightBtn.Hide();
            Input.SetCustomMouseCursor(null);
            ComponentSelectionUI.Singleton.CheckIfDroppedOnHotbar(this);
        }
    }
}
