/*IShipSystem.cs (c) 2021
Author: Justin Abbott (lastmilegames@gmail.com)
Desc: Represents a electrical power storage device.
Created:  2021-07-28T18:28:31.536Z
Modified: 2021-07-28T21:15:28.626Z
*/

using System.Collections.Generic;
using BlueKnightOne.Ships.ShipComponents;

namespace BlueKnightOne.Ships.ShipSystems
{
    public interface IShipSystem
    {
        // Properties
        List<IShipComponent> connectedComponents { get; }

        // Methods
        void DistributeResource(string resourceGUID, float amount);
    }
}