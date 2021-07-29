/*ShipComponentState.cs (c) 2021
Author: Justin Abbott (lastmilegames@gmail.com)
Desc: An enum indicated the current state of a ship component.
Created:  2021-07-29T17:57:24.120Z
Modified: 2021-07-29T18:44:12.491Z
*/

using System;

namespace BlueKnightOne.Ships.ShipComponents
{
    [Flags]
    public enum ShipComponentState
    {
        None = 0,
        Inactive = 1 << 0,
        Active = 1 << 1,
        Worn = 1 << 2,
        Damaged = 1 << 3,
        ReducedFunction = Active | Worn,
        SeverelyReducedFunction = Active | Damaged,
        Destroyed = 1 << 4,
        Missing = 1 << 5,
        Disabled = 1 << 6,
        Overclock = 1 << 7,
        Inoperable = Destroyed | Missing | Disabled
    }
}