using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

namespace ArchitectsInVoid.WorldData;

[Tool]
public partial class Data : Node
{
    [Export] string _name;
    [Export] string _gameVersion;
    [Export] string _lastSaved;
    [Export] WorldDataManager wmData;
    const string VALID_FILE_STRING = "THIS IS A VALID FILE :)";
    public override void _Ready()
    {
        if (Engine.IsEditorHint()) { return; } // do NOT run when not in game
        if (wmData == null)
        {
            wmData = (WorldDataManager)GetParent().FindChild("World", recursive: false);
            if (wmData == null)
            {
                GD.PushError("Data: world data manager not found...");
            }
        }
    }

    public void Load(string name)
    {
        GD.Print("Data: loading...");
        DirAccess.MakeDirRecursiveAbsolute(GetSavePath());
        _name = name;
        if (!FileAccess.FileExists($"{GetSavePath()}{_name}.dat"))
        {
            GD.PushWarning($"Data: load failed, no such file name exists {_name}.dat");
            return;
        }
        var file = FileAccess.Open($"{GetSavePath()}{_name}.dat", FileAccess.ModeFlags.Read);
        _gameVersion = file.GetVar().AsString();
        if(_gameVersion != GetGameVerstion())
        {
            GD.PushWarning($"Data: {name} is old version {_gameVersion} | current game version {GetGameVerstion()}");
            GD.PushWarning("Data: TODO implement a confirm prompt for loading different version...");
        }
        GD.Print(file.GetVar().AsString());

        wmData._Load(file);

        _lastSaved = file.GetVar().AsString();

        file.Close(); // must be called or else creates a bunch of .tmp files
    }


    public void DeleteSave(string name)
    {
        GD.Print($"Data: DELETING {name}.dat");
        DirAccess.MakeDirRecursiveAbsolute(GetSavePath());
        _name = name;
        if (!FileAccess.FileExists($"{GetSavePath()}{_name}.dat"))
        {
            GD.PushWarning($"Data: check failed, no such file name exists {_name}.dat");
            return;
        }
        DirAccess.RemoveAbsolute($"{GetSavePath()}{_name}.dat");
        GD.Print($"Data: {name}.dat DELETED");
    }


    public string GetLastSavedFromFile(string filename)
    {
        DirAccess.MakeDirRecursiveAbsolute(GetSavePath());
        if (FileAccess.FileExists($"{GetSavePath()}{filename}.dat"))
        {
            var file = FileAccess.Open($"{GetSavePath()}{filename}.dat", FileAccess.ModeFlags.Read);
            _ = file.GetVar().AsString();
            if (file.GetVar().AsString() != VALID_FILE_STRING)
            {
                GD.PushError($"Data: INVALID FILE {filename}.dat");
            }
            wmData._DiscardLoadPast(file);
            string dateTime = file.GetVar().AsString();
            file.Close();
            return dateTime;
        }
        GD.PushError($"Data: NO FILE FOUND {filename}.dat");
        return "";
    }

    public void NewGame(string worldName)
    {
        _name = worldName;
        GD.Print("Data: creating new game save...");
        DirAccess.MakeDirRecursiveAbsolute(GetSavePath());
        if (FileAccess.FileExists($"{GetSavePath()}{_name}.dat"))
        {
            GD.PushWarning("Overwriting file here. Need to implement a confirm pop up...");
        }
        var file = FileAccess.Open($"{GetSavePath()}{_name}.dat", FileAccess.ModeFlags.Write);
        _gameVersion = GetGameVerstion();
        file.StoreVar(_gameVersion);
        file.StoreVar(VALID_FILE_STRING);

        wmData.NewGame(file);

        file.StoreVar(GetDateTime());
        file.Close();
    }

    string GetDateTime()
    {
        var now = DateTime.Now;
        string date = $"{now.Day}-{now.Month}-{now.Year}";
        string time = $"{now.ToString("HH")}:{now.ToString("mm")}:{now.ToString("ss")}";
        GD.Print($"{date} {time}");
        return $"{date} {time}";
    }

    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        System.Array.Reverse(charArray);
        return new string(charArray);
    }

    public List<string> RetrieveAllValidSavesAsList()
    {
        var files = DirAccess.GetFilesAt(GetSavePath());
        var fileList = new List<string>();
        foreach (var fileName in files)
        {
            string f = fileName;
            f = Reverse(f);
            f = f.Remove(0, 4);
            f = Reverse(f);
            bool isValid = IsValidFile(f);
            if (isValid)
            {
                fileList.Add(f);
            }
        }

        return fileList;
    }

    public bool IsValidFile(string fileName)
    {
        DirAccess.MakeDirRecursiveAbsolute(GetSavePath());
        if (!FileAccess.FileExists($"{GetSavePath()}{fileName}.dat"))
        {
            GD.PushWarning($"Data: valid checked failed but only because there is no such file name as {fileName}.dat");
            return false;
        }
        var file = FileAccess.Open($"{GetSavePath()}{fileName}.dat", FileAccess.ModeFlags.Read);
        GD.Print($"File {GetSavePath()}{fileName}.dat exists == {FileAccess.FileExists($"{GetSavePath()}{fileName}.dat")}");
        if (file == null)
        {
            GD.PushWarning($"Data: tried loading a file that no exist>>> {fileName}.dat");
            return false;
        }
        _ = file.GetVar().AsString();
        
        if(file.GetVar().AsString() == VALID_FILE_STRING)
        {
            file.Close();
            return true;
        }
        file.Close();
        return false;
    }

    public void Save(string name)
    {
        GD.Print("Data: saving...");
        DirAccess.MakeDirRecursiveAbsolute(GetSavePath());
        _name = name;
        _gameVersion = GetGameVerstion();

        var file = FileAccess.Open($"{GetSavePath()}{_name}.dat", FileAccess.ModeFlags.Write);
        file.StoreVar(_gameVersion);
        file.StoreVar(VALID_FILE_STRING);

        wmData._Save(file);

        _lastSaved = GetDateTime();
        file.StoreVar(_lastSaved);
        file.Close(); // must be called or else creates a bunch of .tmp files
    }

    public void QuickSave()
    {
        Save(_name);
    }

    string GetSavePath()
    {
        return "user://" + "saves/";
    }

    string GetGameVerstion()
    {
        return ProjectSettings.GetSettingWithOverride("application/config/version").AsString();
    }

    void ClearInspector()
    {
        Clear();
    }

    public void Clear()
    {
        _name = "";
        _gameVersion = "";
        wmData._Clear();
    }

    void SaveFromInspector()
    {
        Save(_name);
    }

    void LoadFromInspector()
    {
        Load(_name);
    }
    public Godot.Collections.Array AddInspectorButtons()
    {
        var buttons = new Godot.Collections.Array();

        var btnRefresh = new Dictionary
            {
                { "name", "Refresh" },
                { "icon", GD.Load("res://Testing/InspectorButtons/icon.svg") },
                { "pressed", Callable.From(_Ready) }
            };
        buttons.Add(btnRefresh);

        var btnLoad = new Dictionary
            {
                { "name", "Load" },
                { "icon", GD.Load("res://Testing/InspectorButtons/icon.svg") },
                { "pressed", Callable.From(LoadFromInspector) }
            };
        buttons.Add(btnLoad);

        var btnSave = new Dictionary
            {
                { "name", "Save" },
                { "icon", GD.Load("res://Testing/InspectorButtons/icon.svg") },
                { "pressed", Callable.From(SaveFromInspector) }
            };
        buttons.Add(btnSave);

        var btnClear = new Dictionary
            {
                { "name", "Clear" },
                { "icon", GD.Load("res://Testing/InspectorButtons/icon.svg") },
                { "pressed", Callable.From(ClearInspector) }
            };
        buttons.Add(btnClear);

        return buttons;
    }

}

// 
/* currently the save data is structure like so:
 * ----------------------------------------------------------
 * filename.dat     -> filename
 * 
 * CHECKS ---------------------------------------------------
 * string           -> game-version
 * string           -> valid file :)
 * ----------------------------------------------------------
 * 
 * WORLD DATA -----------------------------------------------
 *      PLAYER DATA *****************************************
 *      int         -> players amount
 *      TBC player data goes here (not implemented yet)
 * 
 *      SHIP DATA *******************************************
 *      int         -> ships amount
 *      TBC ship data goes here (not implemented yet)
 *      
 *      FURHTER DATA CAN BE ADDED HERE***********************
 * ----------------------------------------------------------
 * 
 * OTHER ----------------------------------------------------
 * string           -> play time
 * TBC mod stuff
 * 
 */
