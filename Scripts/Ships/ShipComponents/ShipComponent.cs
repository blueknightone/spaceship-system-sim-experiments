
using System.Collections.Generic;
using BlueKnightOne.Ships.ShipResources;
using BlueKnightOne.Ships.ShipSystems;
using BlueKnightOne.Utilities;
using Godot;

namespace BlueKnightOne.Ships.ShipComponents
{
    public class ShipComponent : Node, IShipComponent
    {
        #region Godot Signals

        [Signal] public delegate void ComponentActivated();
        [Signal] public delegate void ComponentDeactivated();
        [Signal] public delegate void ComponentDestroyed();

        #endregion

        #region Public Properties

        /// <summary>
        ///     Component is active when the ShipComponentState.Active flag is on and Inoperable flags are off.
        /// </summary>
        public bool IsActive => (CurrentState & ShipComponentState.Active ) != 0
                                && (CurrentState & ShipComponentState.Inoperable) == 0;

        public ShipComponentState CurrentState { get; private set; }

        #endregion

        #region Insepctor Variables

        [Export] 
        private ShipComponentState startingState = ShipComponentState.None;

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

        private ComponentWearTimer componentWearTimer;

        #endregion

        #region Godot Lifecycle Events

        public override void _Ready()
        {
            if (startingState == ShipComponentState.None)
            {
                GD.PushError($"{Name} starting state cannot be \"None\".");
            }
            else
            {
                CurrentState = startingState;
            }
            
            componentWearTimer = GetChild<ComponentWearTimer>(0);
            if (componentWearTimer is null)
            {
                GD.PushError($"{Name} must have a ComponentWearTimer.");
            }
        }

        #endregion

        #region Public Methods

        public bool CheckForResourceAvailable(ShipConsumableResource resource, float amountRequested = 0f)
        {
            float totalAmountAvaialable = 0f;

            foreach (ResourceStorageBuffer buffer in internalStorageBuffers)
            {
                if (buffer.AcceptedResource != resource) continue;
                
                totalAmountAvaialable += buffer.CurrentAmountStored;

                // Stop iterating through buffers if we found enough.
                if (totalAmountAvaialable > amountRequested) return true;
            }

            return false;
        }

        public float AddResourceToInternalStorage(ShipConsumableResource resource, float amountRequested)
        {
            if (incomingResources.Contains(resource))
            {
                return SendToInternalStorage(resource, amountRequested);
            }

            return amountRequested;
        }

        private float SendToInternalStorage(ShipConsumableResource resource, float amountRequested)
        {
            float amountRemaining = amountRequested;
            
            foreach (ResourceStorageBuffer buffer in internalStorageBuffers)
            {
                if (buffer.AcceptedResource != resource) continue;

                amountRemaining = buffer.AddToBuffer(amountRemaining);

                if (amountRemaining.Approximately(0f)) return 0f;
            }

            return amountRemaining;
        }

        public float GetResourceFromInternalStorage(ShipConsumableResource resource, float amountRequested)
        {
            if (outgoingResources.Contains(resource))
            {
                return RetrieveFromInternalStorage(resource, amountRequested);
            }

            return amountRequested;
        }

        private float RetrieveFromInternalStorage(ShipConsumableResource resource, float amountRequested)
        {
            float requestedAmountRemaining = amountRequested;
            float amountRetrieved = 0f;

            foreach (ResourceStorageBuffer buffer in internalStorageBuffers)
            {
                if (buffer.AcceptedResource != resource) continue;

                amountRetrieved += buffer.RemoveFromBuffer(requestedAmountRemaining);

                // If we're close enough to the requested amount, quit out early.
                if (amountRetrieved.Approximately(amountRequested)) return amountRetrieved;
            }

            return amountRetrieved;
        }

        public void ProcessResources()
        {
            throw new System.NotImplementedException();
        }

        public void ActivateComponent()
        {
            // TODO: Check can activate.

            if ((CurrentState & ShipComponentState.Inactive) != 0)
            {
                // Unset the inactive bit
                CurrentState &= ~ShipComponentState.Inactive;
            }
            //Set the active bit
            CurrentState |= ShipComponentState.Active;

            componentWearTimer.Start();
        }

        public void DeactivateComponent()
        {
            if ((CurrentState & ShipComponentState.Active) != 0)
            {
                // Unset the Active state bit
                CurrentState &= ~ShipComponentState.Active;
            }
            //Set the Inactive state bit.
            CurrentState |= ShipComponentState.Inactive;

            componentWearTimer.Stop();
        }

        public void ToggleComponent()
        {
            // If the current state is Inactive, activate the component.
            if ((CurrentState & ShipComponentState.Inactive) != 0)
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