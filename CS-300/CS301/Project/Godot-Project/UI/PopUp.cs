using Godot;
using Godot.Collections;
using System;

/// <summary>
/// Manages the pop ups for the game
/// </summary>
[Tool]
public partial class PopUp : Control
{
    #region Variables

    [Export] Node _holder;
    [Export] PackedScene _errorPopUp;
	[Export] PackedScene _confirmPopUp;
	[Export] PackedScene _confirmPopUpCD;
    [Export] PackedScene _infoPopUp;
    [Export] PackedScene _infoPopUpNC;
    [Export] PackedScene _infoPopUpCB;
    [Export] PackedScene _inputPopUp;
    public string LastInput;

    #endregion

    /// <summary>
    /// Called when the node is first loaded into the scene tree
    /// </summary>
    public override void _Ready()
    {
        if (Engine.IsEditorHint()) { return; } // do NOT run when not in game


        Control c = (Control)GetNode(this.GetPath()); // always show this when running the game instead
        c.Show(); // shows it duhhh
    }

    /// <summary>
    /// Test function if pop ups wish to test their binding
    /// </summary>
    void TestCall()
    {
        GD.Print("PopUp: Test Works");
    }
    
    #region Pop Ups

    /// <summary>
    /// Displays a pop up
    /// <br/>This one is an error (user has to close)
    /// </summary>
    public void DisplayError(string title, string message)
	{
        var inst = _errorPopUp.Instantiate();
        _holder.AddChild(inst);
		ErrorPopUp epu = (ErrorPopUp)inst;
        epu.Setup(title, message);
    }

    /// <summary>
    /// Displays a pop up
    /// <br/>This one is a confirm prompt (user has to close) + callbacks
    /// </summary>
	public void DisplayConfirmPopUp(string message, Callable confirmBind)
	{
        var inst = _confirmPopUp.Instantiate();
        _holder.AddChild(inst);
        ConfirmPopUp cpu = (ConfirmPopUp)inst;
        cpu.Setup(message, confirmBind);
    }

    /// <summary>
    /// Displays a pop up
    /// <br/>This one is a confirm prompt with a countdown (user has to close) + callbacks
    /// </summary>
    public void DisplayConfirmPopUpCD(string message, double timerStart, Callable confirmBind, Callable cancelBind)
    {
        var inst = _confirmPopUpCD.Instantiate();
        _holder.AddChild(inst);
        ConfirmPopUpCD cpu = (ConfirmPopUpCD)inst;
        cpu.Setup(message, timerStart, confirmBind, cancelBind);
    }

    /// <summary>
    /// Displays a pop up
    /// <br/>This one is a info prompt (user has to close)
    /// </summary>
    public void DisplayInfoPopUp(string message)
    {
        var inst = _infoPopUp.Instantiate();
        _holder.AddChild(inst);
        InfoPopUp ipu = (InfoPopUp)inst;
        ipu.Setup(message);
    }

    /// <summary>
    /// Displays a pop up.
    /// <br/>This one is an info prompt with a callback
    /// </summary>
    public void DisplayInfoPopUp(string message, Callable acknowledgeBind)
    {
        var inst = _infoPopUpCB.Instantiate();
        _holder.AddChild(inst);
        ConfirmPopUp ipu = (ConfirmPopUp)inst;
        ipu.Setup(message, acknowledgeBind);
    }

    /// <summary>
    /// Displays a pop up
    /// <br/>This one is a info prompt (user cannot close) + return value for managing it directly
    /// </summary>
    public InfoPopUp DisplayInfoPopUpNC(string message)
    {
        var inst = _infoPopUpNC.Instantiate();
        _holder.AddChild(inst);
        InfoPopUp ipu = (InfoPopUp)inst;
        ipu.Setup(message);
        return ipu;
    }

    /// <summary>
    /// Displays a pop up
    /// <br/>This one is a input prompt (user has to close) + callbacks
    /// </summary>
    public void DisplayInputPopUp(string message, Callable confirmBind)
    {
        var inst = _inputPopUp.Instantiate();
        _holder.AddChild(inst);
        InputPopUp ipu = (InputPopUp)inst;
        ipu.Setup(message, confirmBind);
    }

    #endregion
}
