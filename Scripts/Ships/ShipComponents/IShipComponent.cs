/*ShipBatteryComponent.cs (c) 2021
Author: Justin Abbott (lastmilegames@gmail.com)
Desc: Represents a electrical power storage device.
Created:  2021-07-28T18:28:31.536Z
Modified: 2021-07-29T23:36:15.023Z
*/
using BlueKnightOne.Ships.ShipResources;
using BlueKnightOne.Ships.ShipSystems;
using Godot;

namespace BlueKnightOne.Ships.ShipComponents
{
    public interface IShipComponent
    {
        #region Properties
        
        bool IsActive { get; }
        ShipComponentState CurrentState { get; }

        #endregion

        #region Methods

        float AddResource(ShipConsumableResource resource, float amounRequestedt);
        float GetResource(ShipConsumableResource resource, float amountRequested);
        bool CheckResourceAvailability(ShipConsumableResource resource, float amountRequested = 0f);

        void SetComponentState(ShipComponentState nextState, bool overwriteState);
        void Activate();
        void Deactivate();
        void Toggle();
        void Damage(float wearAmount);
        void Destroy();
        void Remove();
        void Install();
        void ToggleOverclock();

        #endregion
    }
}