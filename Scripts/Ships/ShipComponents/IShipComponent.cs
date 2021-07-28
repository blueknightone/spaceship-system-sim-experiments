using System.Collections.Generic;
using BlueKnightOne.Ships.ShipResources;

namespace BlueKnightOne.Ships.ShipComponents
{
    public interface IShipComponent
    {
        /// <summary>
        ///     Called to initialize a component back to default values.
        /// </summary>
        void Initialize();

        void OnActivateComponent();
        void OnDeactivateComponent();
        void OnDestroyComponent();
    }
}