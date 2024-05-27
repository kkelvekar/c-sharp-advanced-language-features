using WarehouseManagementSystem.Domain;

namespace WarehouseManagementSystem.Business
{
    public class OrderProcessor
    {
        public Action<Order>? OnOrderInitialized { get;  set; }

        public event EventHandler<OrderProcessEventArgs>? OnOrderCreated;
        
        public void Process(Order order, Func<Order, bool> shippingLabelProducedEvent)
        { 

            Initialize(order);

            if (shippingLabelProducedEvent?.Invoke(order) ?? false)
            {
                OnOrderProcessed(new() { Order = order });
            }
            
        }

        public void Process(Order order)
        {
            Console.WriteLine("Processing order strong type");
        }

        public void Process(object order)
        {
          Console.WriteLine("Processing order object");
        }

        // This method is just to show the power of Anonymous types in C# 9, ideally this should be in a method and should be pass as a parameter nor it should be returning anything
        public void PrintOrderSummeries(IEnumerable<Order> orders)
        {
            var summeries = orders.Select(order => new
            {
                order.OrderNumber,
                TotalCount = order.LineItems.Count(),
                TotalPrice = order.LineItems.Sum(item => item.Price)
            });

            foreach (var summary in summeries)
            {
                Console.WriteLine($"Order {summary.OrderNumber} has {summary.TotalCount} items with total price of {summary.TotalPrice}");
            }

            var totalPricesWithTax = summeries.Sum(summary => (summary.TotalPrice * 1.15m));
            Console.WriteLine($"Total price of all orders with tax is {totalPricesWithTax}");

            var firstOrder = summeries.First(); 
            var copy = firstOrder with { TotalCount = 10 };
            Console.WriteLine($"First order has {firstOrder.TotalCount} items, copy has {copy.TotalCount} items");

            // copy.TotalCount = 20; // This will not compile because the copy or og anonymous type is immutable or reado only
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