using Godot;
using GodotPlugins.Game;
using System;

/// <summary>
/// Pop up managed by the pop up manager
/// <br/>requires the user input text
/// <br/>made specifically for new world input
/// <br/>has callback for both confirm
/// </summary>
[Tool]
public partial class InputPopUp : Node
{
    [Export] RichTextLabel _title;
    [Export] TextEdit input;
    [Export] TextureButton _buttonClose;
    [Export] TextureButton _buttonConfirm;
    [Export] Callable call;
    public void Setup(string message, Callable confirmBind)
    {
        if (!_buttonClose.IsConnected(BaseButton.SignalName.ButtonDown, Callable.From(Close)))
        {
            _buttonClose.Connect(BaseButton.SignalName.ButtonDown, Callable.From(Close));
        }
        if (!_buttonConfirm.IsConnected(BaseButton.SignalName.ButtonDown, Callable.From(SetInput)))
        {
            _buttonConfirm.Connect(BaseButton.SignalName.ButtonDown, Callable.From(SetInput));
        }
        if (!_buttonConfirm.IsConnected(BaseButton.SignalName.ButtonDown, confirmBind))
        {
            _buttonConfirm.Connect(BaseButton.SignalName.ButtonDown, confirmBind);
        }
        if (!_buttonConfirm.IsConnected(BaseButton.SignalName.ButtonDown, Callable.From(CloseCheck)))
        {
            _buttonConfirm.Connect(BaseButton.SignalName.ButtonDown, Callable.From(CloseCheck));
        }
        _title.Text = message;
    }


    void SetInput()
    {
        ((PopUp)GetParent().GetParent()).LastInput = input.Text;
    }
    void CloseCheck()
    {
        if (input.Text.Length > 0)
        {
            GetParent().RemoveChild(this);
        }
    }
    void Close()
    {
        GetParent().RemoveChild(this);
    }
}
