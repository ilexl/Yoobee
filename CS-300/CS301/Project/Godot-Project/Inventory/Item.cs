using Godot;
using System;
using System.Text.Json.Serialization;

namespace ArchitectsInVoid.Inventory;

/// <summary>
/// Items for use in an inventory as a data form
/// </summary>
public partial class Item : Node
{
    Type _type;
    Measurement _measurement;
    int _amount;
    int _maxAmount;

    public string ShortHandAmount()
    {
        if(_type == Type.None)
        {
            return "";
        }

        int shorthandCount = 0;
        string amountStr = _amount.ToString();
        while(amountStr.Length >= 4)
        {
            shorthandCount++;
            amountStr = amountStr.Remove(-1); // remove first number
            amountStr = amountStr.Remove(-1); // remove second number
            // check if length is 2 if so we add a dot and use that
            if(amountStr.Length == 2)
            {
                amountStr = $"{amountStr[0]}.{amountStr[1]}";
                break;
            }
            else
            {
                amountStr = amountStr.Remove(-1); // remove third number if longer than 2
            }
            return AddMeasurement($"{amountStr}{TypeShortHand[shorthandCount]}");
        }

        return AddMeasurement(amountStr);
    }

    public int GetCurrentAmount()
    {
        return _amount;
    }

    private string AddMeasurement(string num)
    {
        switch (_measurement)
        {
            case Measurement.Mass:
                return $"{num} kg";
            case Measurement.Volume:
                return $"{num} L";
            case Measurement.Percentage:
                return $"{num} %";
            case Measurement.Pressure:
                return $"{num} bar";
            case Measurement.Amount:
            default:
                return num;
        }
    }

    public int GetFreeAmount()
    {
        return _maxAmount - _amount;
    }

    public Texture2D GetIcon()
    {
        return GetItemDataTexture(_type);
    }

    public Type GetCurrentItem()
    {
        return _type;
    }

    internal void ChangeAmount(int change)
    {
        _amount += change;
    }

    public Item() : this(Type.None) { }
    public Item(Type type) : this(type, GetItemDataMaxAmount(type)) { }
    public Item(Type type, int amount)
    {
        _type = type;
        _measurement = GetItemDataMeasurement(type);
        _maxAmount = GetItemDataMaxAmount(type);
        _amount = amount;
    }
}

public partial class Item : Node
{
    public static int GetItemDataMaxAmount(Type type)
    {
        return ALL_ITEMS[(int)type].MaxAmount;
    }
    private static Measurement GetItemDataMeasurement(Type type)
    {
        return ALL_ITEMS[(int)type].Measurement;
    }
    public static Texture2D GetItemDataTexture(Type type)
    {
        string name = Enum.GetName(typeof(Type), type);
        return GD.Load($"res://UI/Item-Icons/{name}.png") as Texture2D;
    }

    internal struct ItemData
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Type? _type { get; set; }
        int _maxAmount;
        public int MaxAmount { get => _maxAmount; }
        Measurement _measurement;
        public Measurement Measurement { get => _measurement; }


        public ItemData(Type t, int max, Measurement m)
        {
            _type = t;
            _maxAmount = max;
            _measurement = m;
        }

    }
    internal enum Measurement: int
    {
        Amount = 0,
        Mass = 1,
        Volume = 2,
        Pressure = 3,
        Percentage = 4
    }

    private static string[] TypeShortHand =
    {
        "K",
        "M",
        "B" // 32 bit int max is 2,147,483,647 aka 2.1 billion ish
    };
    public enum Type : int
    {
        None = 0,
        IronPlate = 1,
        CopperPlate = 2,

        Count = 3, // this has to be the length this enum aka the last value
    }
    internal static readonly ItemData[] ALL_ITEMS = new ItemData[(int)Type.Count]
    {
        new ItemData(Type.None,             0,          Measurement.Amount),
        new ItemData(Type.IronPlate,        100,        Measurement.Amount),
        new ItemData(Type.CopperPlate,      100,        Measurement.Amount)
    };
}

internal class StringEnumConverter
{
}