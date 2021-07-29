
using System.Collections.Generic;
using BlueKnightOne.Ships.ShipResources;
using BlueKnightOne.Ships.ShipSystems;
using BlueKnightOne.Utilities;
using Godot;

namespace BlueKnightOne.Ships.ShipComponents
{
    public abstract class BaseShipComponent : Node, IShipComponent
    {
        #region Insepctor Variables
        /// <summary>
        ///     A list of <code>ShipConsumableResource</code>s that can be sent to this component
        /// </summary>
        [Export] private List<ShipConsumableResource> incomingResources;

        /// <summary>
        ///     A list of <code>ShipConsumableResource</code>s that this component sends out
        /// </summary>
        [Export] private List<ShipConsumableResource> outgoingResources;

        /// <summary>
        ///     A list 
        /// </summary>
        [Export] private List<float> incoming;

        #endregion

        #region Member Variables

        /// <summary>
        ///     References the <code>IShipSystem</code> that the component is attached to.
        /// </summary>
        private IShipSystem parentSystem;

        private Dictionary<ShipConsumableResource, float> internalStorage;

        public void AddResourceToInternalStorage(ShipConsumableResource resource, float amount)
        {
            throw new System.NotImplementedException();
        }

        public float CheckForResourceAvailable(ShipConsumableResource resource, float amountRequested = 0)
        {
            throw new System.NotImplementedException();
        }

        public float GetResourceFromInternalStorage(ShipConsumableResource resource)
        {
            throw new System.NotImplementedException();
        }

        public void Initialize(IShipSystem parentSystem)
        {
            throw new System.NotImplementedException();
        }

        public void ProcessResources()
        {
            throw new System.NotImplementedException();
        }

        public void SetParentSystem(IShipSystem parentSystem)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}