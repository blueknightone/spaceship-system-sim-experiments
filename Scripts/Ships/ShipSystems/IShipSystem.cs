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