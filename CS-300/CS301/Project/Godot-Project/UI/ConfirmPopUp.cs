using Godot;
using System;

/// <summary>
/// Pop up managed by the pop up manager
/// <br/>requires the user to confirm with a callback if confirmed
/// </summary>
[Tool]
public partial class ConfirmPopUp : Node
{
    [Export] RichTextLabel _title;
    [Export] TextureButton _buttonClose;
    [Export] TextureButton _buttonConfirm;
    [Export] Callable call;
    public void Setup(string message, Callable confirmBind)
	{
        if (!_buttonConfirm.IsConnected(BaseButton.SignalName.ButtonDown, confirmBind))
        {
            _buttonConfirm.Connect(BaseButton.SignalName.ButtonDown, confirmBind);
        }
        if (!_buttonConfirm.IsConnected(BaseButton.SignalName.ButtonDown, Callable.From(Close)))
        {
            _buttonConfirm.Connect(BaseButton.SignalName.ButtonDown, Callable.From(Close));
        }
        _title.Text = message;
        if(_buttonClose == null) { return; }
        if (!_buttonClose.IsConnected(BaseButton.SignalName.ButtonDown, Callable.From(Close)))
        {
            _buttonClose.Connect(BaseButton.SignalName.ButtonDown, Callable.From(Close));
        }
    }

    void Close()
    {
        GetParent().RemoveChild(this);
    }
}
