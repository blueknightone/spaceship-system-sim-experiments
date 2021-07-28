/*ShipConsumableResource.cs (c) 2021
Author: Justin Abbott (lastmilegames@gmail.com)
Desc: Represents a electrical power storage device.
Created:  2021-07-28T18:28:31.536Z
Modified: 2021-07-28T21:40:34.496Z
*/

using System;
using BlueKnightOne.Utilities;
using Godot;

namespace BlueKnightOne.Ships.ShipResources
{
    public class ShipConsumableResource : Resource
    {

        public string GUID { get; private set; }

        [Export]
        public string DisplayName { get; private set; }

        [Export(PropertyHint.MultilineText)]
        public string Description { get; private set; }

        [ExportFlagsEnum(typeof(ShipConsumableType))]
        private short consumableType;

        public ShipConsumableType ConsumableType
        {
            get => (ShipConsumableType)consumableType;
            private set => consumableType = (short)value;
        }

        public ShipConsumableResource()
        {
            if (string.IsNullOrEmpty(GUID))
            {
                GUID = Guid.NewGuid().ToString();
            }

            DisplayName = "Resource";
            ConsumableType = ShipConsumableType.None;
            Description = "";
        }
    }
}