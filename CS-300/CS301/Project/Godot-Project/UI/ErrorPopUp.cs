using Godot;
using System;

/// <summary>
/// Pop up managed by the pop up manager
/// <br/>displays an error on the side of the screen
/// <br/>user must close it. No call back
/// </summary>
[Tool]
public partial class ErrorPopUp : Node
{
	[Export] RichTextLabel _title;
	[Export] RichTextLabel _message;
	[Export] TextureButton _button;
    public void Setup(string title, string message)
	{
        if (!_button.IsConnected(BaseButton.SignalName.ButtonDown, Callable.From(Close)))
        {
            _button.Connect(BaseButton.SignalName.ButtonDown, Callable.From(Close));
        }
		_title.Text = title;
        _message.Text = message;
    }

	void Close()
	{
		GetParent().RemoveChild(this);
	}
}
