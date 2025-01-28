using Godot;
using System;

/// <summary>
/// Pop up managed by the pop up manager
/// <br/>requires the user to confirm or cancel
/// <br/>has a countdown DEFAULT is 99999
/// <br/>has callbacks for both confirm and cancel
/// </summary>
[Tool]
public partial class ConfirmPopUpCD : Node
{
    [Export] RichTextLabel _title;
    [Export] TextureButton _buttonClose;
    [Export] TextureButton _buttonConfirm;
    [Export] Callable cancelled;
    [Export] double timeLeft = 99999;
    [Export] string _message;
    public void Setup(string message, double timerStart, Callable confirmBind, Callable cancelledBind)
	{
        if (!_buttonClose.IsConnected(BaseButton.SignalName.ButtonDown, Callable.From(Close)))
        {
            _buttonClose.Connect(BaseButton.SignalName.ButtonDown, Callable.From(Close));
        }
        if (!_buttonConfirm.IsConnected(BaseButton.SignalName.ButtonDown, confirmBind))
        {
            _buttonConfirm.Connect(BaseButton.SignalName.ButtonDown, confirmBind);
        }
        cancelled = cancelledBind;
        if (!_buttonConfirm.IsConnected(BaseButton.SignalName.ButtonDown, Callable.From(CloseNC)))
        {
            _buttonConfirm.Connect(BaseButton.SignalName.ButtonDown, Callable.From(CloseNC));
        }
        _message = message;
        timeLeft = timerStart;
        _title.Text = _message + " " + Math.Round(timeLeft, 2).ToString();
    }

    public override void _Process(double delta)
    {
        timeLeft -= delta;
        _title.Text = _message + " " + Math.Round(timeLeft, 2).ToString();
        if (timeLeft < 0)
        {
            Close();
        }
    }

    void Close()
    {
        GetParent().RemoveChild(this);
        cancelled.Call();
    }

    void CloseNC()
    {
        GetParent().RemoveChild(this);
    }
}
