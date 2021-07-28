/*ShipBatteryComponent.cs (c) 2021
Author: Justin Abbott (lastmilegames@gmail.com)
Desc: Represents a electrical power storage device.
Created:  2021-07-28T18:28:31.536Z
Modified: 2021-07-28T21:14:36.991Z
*/

using System;

namespace BlueKnightOne.Ships.ShipComponents
{
    [Flags]
    public enum ShipComponentType : short
    {
        None = 0,
        ResourceStorage = 1 << 0,
        ResourceConsumer = 1 << 1,
        ResourceProducer = 1 << 2
    }
}