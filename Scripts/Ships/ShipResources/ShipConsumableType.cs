/*ShipBatteryComponent.cs (c) 2021
Author: Justin Abbott (lastmilegames@gmail.com)
Desc: Represents a electrical power storage device.
Created:  2021-07-28T18:28:31.536Z
Modified: 2021-07-28T21:14:49.968Z
*/

using System;

/// <summary>
///     Flags for resource types
/// </summary>
[Flags]
public enum ShipConsumableType : short
{

    // I used left bit shifts so it is easier to tell which bit gets flipped. - BlueKnightOne
    None = 0,
    Fuel = 1 << 0,
    Coolant = 1 << 1,
    Atmosphere = 1 << 2,
    Electricity = 1 << 3,
    Heat = 1 << 4,
    Water = 1 << 5,
    Thrust = 1 << 6
}