using Godot;
using System;

public partial class ItemSlotSlider : HSlider
{
    Vector2 _mouseStartPos;

    public void SetStart(Vector2 mousePos, int maxValue)
    {
        _mouseStartPos = mousePos;
        MaxValue = maxValue;
    }

    public int GetSliderAmount()
    {
        return (int)Value;
    }

    public override void _Process(double delta)
    {
        if (!Visible) { return; }
        double mouseDistance = Mathf.Clamp(GetViewport().GetMousePosition().X - _mouseStartPos.X, 0d, GetRect().Size.X);
        Value = mouseDistance / GetRect().Size.X * MaxValue;

    }
}
