using System;

/// <summary>
///     Flags for resource types
/// </summary>
[Flags]
public enum ShipResourceType : short
{

    // I used left bit shifts so it is easier to tell which bit gets flipped. - BlueKnightOne
    None = 0,
    Fuel = 1 << 0,
    Coolant = 1 << 1,
    Atmosphere = 1 << 2,
    Electricity = 1 << 3,
    Heat = 1 << 4,
    Water = 1 << 5
}