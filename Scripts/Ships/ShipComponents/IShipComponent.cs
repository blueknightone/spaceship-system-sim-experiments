/*ShipBatteryComponent.cs (c) 2021
Author: Justin Abbott (lastmilegames@gmail.com)
Desc: Represents a electrical power storage device.
Created:  2021-07-28T18:28:31.536Z
Modified: 2021-07-28T21:14:21.979Z
*/
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