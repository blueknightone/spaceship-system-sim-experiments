using BlueKnightOne.Ships.ShipSystems;
using Godot;

namespace BlueKnightOne.Ships.ShipComponents
{
    public abstract class BaseShipComponent : Node, IShipComponent
    {

        #region Godot Signals
        
        [Signal] public delegate void ComponentActivated();
        [Signal] public delegate void ComponentDeactivated();
        [Signal] public delegate void ComponentDestroyed();

        #endregion

        #region Instance Variables
        private IShipSystem connectedSystem;

        #endregion

        #region Godot Events

        public override void _Ready()
        {
            GetConnectedSystem();
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
            // Simply 
        }
        
        #endregion

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

    }
}