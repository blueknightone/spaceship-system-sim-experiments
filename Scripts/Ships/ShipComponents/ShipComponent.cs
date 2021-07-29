
using System.Collections.Generic;
using BlueKnightOne.Ships.ShipResources;
using BlueKnightOne.Ships.ShipSystems;
using Godot;

namespace BlueKnightOne.Ships.ShipComponents
{
    public class ShipComponent : Node, IShipComponent
    {
        #region Public Properties
        public bool IsActive => throw new System.NotImplementedException();

        #endregion

        #region Insepctor Variables

        [Export] private ShipComponentState startingState = ShipComponentState.None;

        /// <summary>
        ///     A list of <code>ShipConsumableResource</code>s that can be sent to this component
        /// </summary>
        [Export] private List<ShipConsumableResource> incomingResources;

        /// <summary>
        ///     A list of <code>ShipConsumableResource</code>s that this component sends out
        /// </summary>
        [Export] private List<ShipConsumableResource> outgoingResources;

        /// <summary>
        ///     A list of internal buffers (tubes, tanks, wires, etc.)
        /// </summary>
        [Export] private List<ResourceStorageBuffer> internalStorageBuffers;

        [Export] private ComponentEfficiency componentEfficiency;

        [Export] private NodePath componentWearNodePath;

        #endregion

        #region Member Variables

        /// <summary>
        ///     References the <code>IShipSystem</code> that the component is attached to.
        /// </summary>
        private IShipSystem parentSystem;

        private ShipComponentState currentState;

        private Dictionary<ShipConsumableResource, float> internalStorage;

        private ComponentWear componentWearTimer;

        #endregion

        #region Godot Lifecycle Events

        public override void _Ready()
        {
            currentState = startingState;
            componentWearTimer = GetNode<ComponentWear>(componentWearNodePath);
        }

        #endregion

        #region Public Methods

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

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public void ProcessResources()
        {
            throw new System.NotImplementedException();
        }

        public void SetParentSystem(IShipSystem parentSystem)
        {
            this.parentSystem = parentSystem;
        }

        public void ActivateComponent()
        {
            if ((currentState & ShipComponentState.Inactive) != 0)
            {
                // Unset the inactive bit
                currentState &= ~ShipComponentState.Inactive;
            }
            //Set the active bit
            currentState |= ShipComponentState.Active;
        }

        public void DeactivateComponent()
        {
            if ((currentState & ShipComponentState.Active) != 0)
            {
                // Unset the Active state bit
                currentState &= ~ShipComponentState.Active;
            }
            //Set the Inactive state bit.
            currentState |= ShipComponentState.Inactive;
        }

        public void ToggleComponent()
        {
            // If the current state is Inactive, activate the component.
            if ((currentState & ShipComponentState.Inactive) != 0)
            {
                ActivateComponent();
            }
            else
            {
                DeactivateComponent();
            }
        }

        #endregion

        #region PrivateMethods

        #endregion
    }
}