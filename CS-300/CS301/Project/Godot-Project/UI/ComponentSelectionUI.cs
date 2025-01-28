using ArchitectsInVoid.Player.HotBar;
using ArchitectsInVoid.UI;
using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class ComponentSelectionUI : Control
{
    #region Variables
    public static ComponentSelectionUI Singleton;
    [Export] Control _blockSelectionMenu;
    [Export] GetPackedScenes _gps;
    [Export] VBoxContainer _containerVertical;
    [Export] PackedScene _blankComponentSelection;
    [ExportGroup("ComponentInfoList")]
    [Export] Control _componentInfo0;
    [Export] Control _componentInfo1;
    [Export] Control _componentInfo2;
    [Export] Control _componentInfo3;
    [Export] Control _componentInfo4;
    [Export] Control _componentInfo5;
    [Export] Control _componentInfo6;
    [Export] Control _componentInfo7;
    [Export] Control _componentInfo8;
    [Export] Control _componentInfo9;
    List<Control> _componentInfos;
    [ExportGroup("ComponentInfoInformation")]
    [Export] RichTextLabel _componentInfoTitle;
    [Export] RichTextLabel _componentInfoDesc;
    [Export] TextureRect _componentTexture;

    #endregion

    public override void _Ready()
    {
        Singleton = this;
        MenuShown(false);
        _componentInfos = new List<Control>()
        {
            _componentInfo0,
            _componentInfo1,
            _componentInfo2,
            _componentInfo3,
            _componentInfo4,
            _componentInfo5,
            _componentInfo6,
            _componentInfo7,
            _componentInfo8,
            _componentInfo9
        };
        ShowComponentInfo(null);

        List<PackedScene> packedScenes = _gps.GetAll();
        GenerateSlots(packedScenes);
    }

    public struct ComponentInfo
    {
        [Export]public string Title;
        [Export]public string Info;
    }

    void ShowComponentInfo(List<ComponentInfo> componentInfos)
    {
        int length = 0;
        if (componentInfos != null)
        {
            length = componentInfos.Count;
            if (length > 10)
            {
                // if bigger than 10 then there are too many
                GD.PushError("ComponentSelectionUI: index too large for array...");
                return;
            }
            for (int i = 0; i < length; i++)
            {
                RichTextLabel title = _componentInfos[i].FindChild("Title") as RichTextLabel;
                RichTextLabel info = _componentInfos[i].FindChild("Info") as RichTextLabel;
                title.Text = componentInfos[i].Title;
                info.Text = componentInfos[i].Info;
                GD.Print($"ComponentSelectionUI: component info line {i} is \"{componentInfos[i].Title}\" \"{componentInfos[i].Info}\"");
            }
        }
        
        for(int i = length; i < 10; i++)
        {
            RichTextLabel title = _componentInfos[i].FindChild("Title") as RichTextLabel;
            RichTextLabel info = _componentInfos[i].FindChild("Info") as RichTextLabel;
            title.Text = "";
            info.Text = "";
            GD.Print($"ComponentSelectionUI: component info line {i} is \"\" \"\"");
        }
    }

    public void MenuShown(bool shown)
    {
        _blockSelectionMenu.Visible = shown;
    }
    public bool IsCurrentlyShown()
    {
        return _blockSelectionMenu.Visible;
    }

    public override void _Input(InputEvent @event)
    {
        if (GameManager.Singleton.CurrentGameState == GameManager.GameState.MainMenu)
        {
            return;
        }
        if (Pause.Singleton.IsPaused == true)
        {
            return;
        }
        if (@event.IsActionPressed("component_config_menu"))
        {
            GD.Print("ComponentSelectionUI: config menu button pressed");
            MenuShown(!_blockSelectionMenu.Visible);
            if (Input.MouseMode == Input.MouseModeEnum.Captured)
            {
                Input.MouseMode = Input.MouseModeEnum.Visible;
            }
            else
            {
                Input.MouseMode = Input.MouseModeEnum.Captured;
            }
        }
    }

    public void GenerateSlots(List<PackedScene> packedScenes)
    {
        foreach(var child in _containerVertical.GetChildren())
        {
            child.QueueFree();
        }

        double verticalSlotsRaw = (double)packedScenes.Count / 12d;
        double verticalSlots = System.Math.Round(verticalSlotsRaw);
        if (verticalSlotsRaw.ToString().Contains(".") && verticalSlots < verticalSlotsRaw)
        {
            verticalSlots++;
        }

        for (int i = 0; i < verticalSlots; i++)
        {
            HBoxContainer containerH = new HBoxContainer();
            for (int j = 0; j < 12; j++)
            {
                Node c = _blankComponentSelection.Instantiate();
                containerH.AddChild(c);
                ComponentSelect cs = c as ComponentSelect;
                
                if (i * 12 + j < packedScenes.Count)
                {
                    cs.Setup(packedScenes[i * 12 + j]);
                }
                else
                {
                    cs.Setup(null);
                }
            }
            containerH.AddThemeConstantOverride("separation", -2);
            _containerVertical.AddChild(containerH);
        }
    }

    internal void CheckIfDroppedOnHotbar(ComponentSelect componentSelect)
    {
        UIManager _uiManger = GetParent().GetParent() as UIManager;
        
        if(_uiManger == null)
        {
            GD.PushError("ComponentSelectionUI: no ui manager found...");
            return;
        }
        
        var HUD = _uiManger.HudMenu;
        int slot = HUD.CheckIfMouseOver();
        if(slot == -1 || slot == 10) { return; }

        HotBarManager.Singleton.SetSlot(slot, componentSelect.Component);
    }
}
