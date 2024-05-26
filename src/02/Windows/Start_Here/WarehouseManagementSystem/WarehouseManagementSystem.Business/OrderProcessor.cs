using WarehouseManagementSystem.Domain;

namespace WarehouseManagementSystem.Business
{
    public class OrderProcessor
    {
        public Action<Order>? OnOrderInitialized { get;  set; }
        
        public void Process(Order order, Func<Order, bool> shippingLabelProducedEvent, Func<Order, bool> onCompleteEvent)
        {
            // Run some code..

            Initialize(order);

            if (shippingLabelProducedEvent?.Invoke(order) ?? false)
            {

                onCompleteEvent?.Invoke(order);
            }
            
        }

        private void Initialize(Order order)
        {
            ArgumentNullException.ThrowIfNull(order, nameof(order));

            order.IsReadyForShipment = true;
            OnOrderInitialized?.Invoke(order);
        }
    }
}