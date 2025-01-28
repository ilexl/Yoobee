using Godot;
using System;

public partial class InfoBoxDisplay : Control
{
    [Export] string _title;
    [Export] string _info;
    [Export] RichTextLabel _titleLabel;
    [Export] RichTextLabel _infoLabel;

    public void SetTitle(string title)
    {
        _title = title;
    }

    public void SetInfo(string info)
    {
        _info = info;
    }

    void RefreshLabels()
    {
        _infoLabel.Text = _info; 
        _titleLabel.Text = _title; 
    }

    public override void _Ready()
    {
        if(_titleLabel == null || _infoLabel == null)
        {
            _titleLabel = FindChild("InfoBoxTitle") as RichTextLabel;
            _infoLabel = FindChild("InfoBoxInfo") as RichTextLabel;
            if (_titleLabel == null || _infoLabel == null)
            {
                GD.PushError("InfoBoxDisplay: missing labels...");
            }
        }
        RefreshLabels();
    }



}
