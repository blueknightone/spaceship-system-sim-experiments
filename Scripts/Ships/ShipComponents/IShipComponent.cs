/*ShipBatteryComponent.cs (c) 2021
Author: Justin Abbott (lastmilegames@gmail.com)
Desc: Represents a electrical power storage device.
Created:  2021-07-28T18:28:31.536Z
Modified: 2021-07-28T21:14:21.979Z
*/
using BlueKnightOne.Ships.ShipResources;
using BlueKnightOne.Ships.ShipSystems;
namespace BlueKnightOne.Ships.ShipComponents
{
    public interface IShipComponent
    {
        #region Methods
        void Initialize(IShipSystem parentSystem);
        void ProcessResources();
        void SetParentSystem(IShipSystem parentSystem);
        float GetResourceFromInternalStorage(ShipConsumableResource resource);
        void AddResourceToInternalStorage(ShipConsumableResource resource, float amount);
        float CheckForResourceAvailable(ShipConsumableResource resource, float amountRequested = 0f);

        #endregion
    }
}