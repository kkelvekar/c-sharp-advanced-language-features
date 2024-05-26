using WarehouseManagementSystem.Domain;

namespace WarehouseManagementSystem.Business
{
    public class OrderProcessor
    {
        public Action<Order>? OnOrderInitialized { get;  set; }

        public event EventHandler<OrderProcessEventArgs>? OnOrderCreated;
        
        public void Process(Order order, Func<Order, bool> shippingLabelProducedEvent)
        {
            // Run some code..

            Initialize(order);

            if (shippingLabelProducedEvent?.Invoke(order) ?? false)
            {
                OnOrderProcessed(new() { Order = order });
            }
            
        }

        // Why this method ? Relax it is just a standard pattern to follow
        protected virtual void OnOrderProcessed(OrderProcessEventArgs e)
        {
            OnOrderCreated?.Invoke(this, e);
        }

        private void Initialize(Order order)
        {
            ArgumentNullException.ThrowIfNull(order, nameof(order));

            OnOrderInitialized?.Invoke(order);
            order.IsReadyForShipment = true;
        }
    }

    
}