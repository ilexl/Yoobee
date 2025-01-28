using Godot;
using System;
using System.Threading.Tasks;

/// <summary>
/// Class manages the visuals to behave like TextureButton except uses 
/// custom plugin for SVG textures that scale properly.
/// <br/><br/>MUST be attached to a TextureButton
/// </summary>
public partial class SvgButton : TextureButton
{
    [Export] Node2D _normal;
    [Export] Node2D _pressed;
    [Export] Node2D _hover;
    [Export] Node2D _disabled;
    [Export] RichTextLabel _optionalLabel;

    TextureButton tBtn;
    State _currentState;

    enum State
    {
        normal,
        pressed,
        hover,
        disabled
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (Engine.IsEditorHint()) { return; } // do NOT run when not in game 
        tBtn = this;
        ShowCorrectImage(GetState());
        RefreshState();
    }

    public override void _Process(double delta)
    {
        if (!this.Visible) { return; }
        RefreshState();
    }

    void RefreshState()
    {
        ShowCorrectImage(GetState());
    }

    State GetState()
    {
        if (tBtn.Disabled) { return State.disabled; } // if disabled - do nothing else
        if (tBtn.ButtonPressed) { return State.pressed; } // if pressed use this
        if (tBtn.IsHovered()) { return State.hover; } // last check is hover
        return State.normal; // if no other state - button is normal
    }

    void ShowCorrectImage(State state)
    {
        if(_currentState == state) { return; }

        _normal.Visible = false;
        _pressed.Visible = false;
        _hover.Visible = false;
        _disabled.Visible = false;

        _currentState = state;

        if (_optionalLabel == null)
        {
            switch (state)
            {
                case State.normal:
                    _normal.Visible = true;
                    break;
                case State.pressed:
                    _pressed.Visible = true;
                    break;
                case State.hover:
                    _hover.Visible = true;
                    break;
                case State.disabled:
                    _disabled.Visible = true;
                    break;
            }
            return;
        }
        else
        {
            if (this.ToggleMode)
            {
                // normal is off
                // pressed is on
                // no hover
                switch (state)
                {
                    case State.normal:
                        _normal.Visible = true;
                        if (_optionalLabel != null)
                        {
                            _optionalLabel.RemoveThemeColorOverride("default_color");
                            _optionalLabel.AddThemeColorOverride("default_color", new Color(0.5f, 0.5f, 0.5f));
                        }
                        break;
                    case State.pressed:
                        _pressed.Visible = true;
                        if (_optionalLabel != null)
                        {
                            _optionalLabel.RemoveThemeColorOverride("default_color");
                            _optionalLabel.AddThemeColorOverride("default_color", new Color(1f, 1f, 1f));
                        }
                        break;
                    default:
                        if (tBtn.ButtonPressed)
                        {
                            _pressed.Visible = true;
                            if (_optionalLabel != null)
                            {
                                _optionalLabel.RemoveThemeColorOverride("default_color");
                                _optionalLabel.AddThemeColorOverride("default_color", new Color(1f, 1f, 1f));
                            }
                        }
                        else
                        {
                            _normal.Visible = true;
                            if (_optionalLabel != null)
                            {
                                _optionalLabel.RemoveThemeColorOverride("default_color");
                                _optionalLabel.AddThemeColorOverride("default_color", new Color(0.5f, 0.5f, 0.5f));
                            }
                        }
                        break;
                }


                return;
            }
            else
            {
                switch (state)
                {
                    case State.normal:
                        _normal.Visible = true;
                        if (_optionalLabel != null)
                        {
                            _optionalLabel.RemoveThemeColorOverride("default_color");
                            _optionalLabel.AddThemeColorOverride("default_color", new Color(1f, 1f, 1f));
                        }
                        break;
                    case State.pressed:
                        _pressed.Visible = true;
                        break;
                    case State.hover:
                        _hover.Visible = true;
                        break;
                    case State.disabled:
                        _disabled.Visible = true;
                        if (_optionalLabel != null)
                        {
                            _optionalLabel.RemoveThemeColorOverride("default_color");
                            _optionalLabel.AddThemeColorOverride("default_color", new Color(0.5f, 0.5f, 0.5f));
                        }
                        break;
                }
                return;
            }
        }

        

    }
}
