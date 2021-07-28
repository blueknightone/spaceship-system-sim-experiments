
using BlueKnightOne.Ships.ShipResources;
using BlueKnightOne.Ships.ShipSystems;
using BlueKnightOne.Utilities;
using Godot;

namespace BlueKnightOne.Ships.ShipComponents
{
    public abstract class BaseShipComponent : Node, IShipComponent
    {

        #region Godot Signals

        [Signal] public delegate void ComponentActivated();
        [Signal] public delegate void ComponentDeactivated();
        [Signal] public delegate void ComponentDestroyed();
        [Signal] public delegate void ResourceConsumed(ShipConsumableResource resource, float amount);

        #endregion

        #region Inspector Variables
        [ExportFlagsEnum(typeof(ShipComponentType))] private short componentType;
        [Export] private Resource acceptedResources;

        #endregion

        #region Instance Variables
        private IShipSystem connectedSystem;

        public ShipComponentType ComponentType 
        {
            get => (ShipComponentType)componentType;
            set => componentType = (short)value;
        }

        #endregion

        #region Godot Events

        public override void _Ready()
        {
            GetConnectedSystem();
            Initialize();
        }

        private void GetConnectedSystem()
        {
            connectedSystem = GetParent<IShipSystem>();

            if (connectedSystem is null)
            {
                GD.PushError($"{Name} must be the child of an IShipSystem.");
                return;
            }
        }

        public override void _Process(float delta)
        {
            
        }

        #endregion

        #region Public Methods
        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public void OnActivateComponent()
        {
            throw new System.NotImplementedException();
        }

        public void OnDeactivateComponent()
        {
            throw new System.NotImplementedException();
        }

        public void OnDestroyComponent()
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Private Methods
            
        #endregion
    }
}