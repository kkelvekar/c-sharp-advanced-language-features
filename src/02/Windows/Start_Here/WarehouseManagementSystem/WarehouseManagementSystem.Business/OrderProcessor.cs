using WarehouseManagementSystem.Domain;

namespace WarehouseManagementSystem.Business
{
    public class OrderProcessor
    {
        public delegate void OrderInitializedEventHandler(Order order);
        public delegate bool ShippingLabelProducedEventHandler(Order order);
        public delegate bool OrderProcessedEventHandler(Order order);

        public OrderInitializedEventHandler? OnOrderInitialized { get;  set; }
        
        public void Process(Order order, ShippingLabelProducedEventHandler shippingLabelProducedEvent, OrderProcessedEventHandler onCompleteEvent)
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