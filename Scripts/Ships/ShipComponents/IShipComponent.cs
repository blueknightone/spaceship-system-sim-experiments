/*ShipBatteryComponent.cs (c) 2021
Author: Justin Abbott (lastmilegames@gmail.com)
Desc: Represents a electrical power storage device.
Created:  2021-07-28T18:28:31.536Z
Modified: 2021-07-29T23:36:15.023Z
*/
using BlueKnightOne.Ships.ShipResources;
using BlueKnightOne.Ships.ShipSystems;

namespace BlueKnightOne.Ships.ShipComponents
{
    public interface IShipComponent
    {
        #region Properties
        
        bool IsActive { get; }

        #endregion

        #region Methods

        void ActivateComponent();
        void DeactivateComponent();
        void ToggleComponent();
        void ProcessResources();
        float AddResourceToInternalStorage(ShipConsumableResource resource, float amounRequestedt);
        bool CheckForResourceAvailable(ShipConsumableResource resource, float amountRequested = 0f);
        float GetResourceFromInternalStorage(ShipConsumableResource resource, float amountRequested);

        #endregion
    }
}