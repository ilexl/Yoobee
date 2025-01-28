using Godot;

namespace ArchitectsInVoid.UI.UIElements;

/// <summary>
/// World save title is used as a button for the world manager
/// </summary>
public partial class WorldSaveTitle : Control
{
    [Export] private TextureButton _btn;
    [Export] public string Date;
    [Export] public string Title;
    [Export] private RichTextLabel _worldName, _saveDate;
    private WorldManager _worldManager;

    /// <summary>
    /// Called when the Node is first loaded into the scene
    /// </summary>
    public override void _Ready()
    {
        if (Engine.IsEditorHint()) { return; } // do NOT run when not in game
        // error checking
        if (_worldName == null || _saveDate == null)
        {
            _worldName = (RichTextLabel)FindChild("WorldName");
            _saveDate = (RichTextLabel)FindChild("SaveDate");
            if (_worldName == null || _saveDate == null)
            {
                GD.PushError("WorldSaveTitle: missing text labels...");
                return;
            }
        }

        if (_btn == null)
        {
            _btn = (TextureButton)FindChild("TextureButton");
            if (_btn == null) GD.PushError("WorldSaveTitle: missing button...");
        }
    }

    /// <summary>
    /// binds the signal to the world manager
    /// </summary>
    public void BindButtonToManager(WorldManager manager)
    {
        _worldManager = manager;
        _btn.Connect(BaseButton.SignalName.ButtonDown, Callable.From(ButtonPressed));
    }

    /// <summary>
    /// returns the state/pressed of this button
    /// </summary>
    public bool GetButtonState()
    {
        return !_btn.ButtonPressed;
    }

    /// <summary>
    /// Alerts the world manager this button was clicked
    /// </summary>
    private void ButtonPressed()
    {
        _worldManager.ListedWorldClicked(this);
    }

    /// <summary>
    /// Updates the text of this world title and the date the world was last saved
    /// </summary>
    public void UpdateWorldSaveTitle(string title, string date)
    {
        Title = title;
        Date = date;

        if (_worldName == null || _saveDate == null)
        {
            GD.PushError("WorldSaveTitle: missing text labels...");
            return;
        }

        _worldName.Text = Title;
        _saveDate.Text = Date;
    }
}