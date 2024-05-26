
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

orderProcessor.Process(new Order(), ShippingLabelProduced, OrderProcessed);

//orderProcessor.Process(new Order(), (order) => { Console.WriteLine($"Shipping label produced for order {order.OrderNumber}."); return true; }, (order) => { Console.WriteLine($"Shipping label produced for order {order.OrderNumber}."); return true; });

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

bool OrderProcessed(Order order)
{
    Console.WriteLine($"Order {order.OrderNumber} processed.");
    return true;
}