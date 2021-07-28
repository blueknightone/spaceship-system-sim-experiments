using System;

namespace BlueKnightOne.Ships.ShipResources
{
    [Serializable]
    public abstract class BaseShipResource
    {
        public string GUID { get; private set; }

        public string DisplayName { get; set; } = "Resource";

        public ShipResourceType resourceType { get; set; } = ShipResourceType.None;

        public BaseShipResource()
        {
            if (string.IsNullOrEmpty(GUID))
            {
                GUID = Guid.NewGuid().ToString();
            }
        }
    }
}