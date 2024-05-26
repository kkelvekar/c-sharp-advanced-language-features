
using WarehouseManagementSystem.Business;
using WarehouseManagementSystem.Domain;

var orderProcessor = new OrderProcessor();


orderProcessor.OnOrderInitialized += order =>
{
    order = new Order
    {
        ShippingProvider = new SwedishPostalServiceShippingProvider
        {
            Name = "Swedish Postal Service",
            FreightCost = 100,
            DeliverNextDay = true
        },
        LineItems = new List<Item>
        {
            new Item { Name = "Item 1", Price = 100, InStock = true },
            new Item { Name = "Item 2", Price = 200, InStock = true }
        }
    };

    Console.WriteLine($"Order {order.OrderNumber} initialized.");
};

orderProcessor.OnOrderInitialized += OrderInitialized; // += is used to add a methods to the delegate basically chaning the delegate to a multicast delegate

orderProcessor.OnOrderInitialized += order => order.IsReadyForShipment = true;

// Order of assigning the event handlers matters, that is before calling the Process method
orderProcessor.OnOrderCreated += (sender, e) => Console.WriteLine($"Order 1 {e.Order.OrderNumber} processed.");
orderProcessor.OnOrderCreated += (sender, e) => Console.WriteLine($"Order 2 {e.Order.OrderNumber} processed.");

orderProcessor.Process(new Order(), ShippingLabelProduced);


void OrderInitialized(Order order)
{
    order = new Order
    {
        ShippingProvider = new SwedishPostalServiceShippingProvider
        {
            Name = "British Postal Service",
            FreightCost = 100,
            DeliverNextDay = true
        },
        LineItems = new List<Item>
        {
            new Item { Name = "Item 3", Price = 100, InStock = true },
            new Item { Name = "Item 4", Price = 200, InStock = true }
        }
    };

    Console.WriteLine($"Order {order.OrderNumber} initialized.");
}

bool ShippingLabelProduced(Order order)
{
    Console.WriteLine($"Shipping label produced for order {order.OrderNumber}.");
    return true;
}
