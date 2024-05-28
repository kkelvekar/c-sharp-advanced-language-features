namespace WarehouseManagementSystem.Domain
{
    public class Order
    {
        public Guid OrderNumber { get; init; }
        public ShippingProvider ShippingProvider { get; init; }
        public int Total { get; }
        public bool IsReadyForShipment { get; set; } = true;
        public IEnumerable<Item> LineItems { get; set; }

        public Order()
        {
            OrderNumber = Guid.NewGuid();
        }

        // This is a Deconstruct method, it is a new feature in C# 7.0, it is used to deconstruct an object into multiple variables
        public void Deconstruct(out Guid orderNumber, out ShippingProvider shippingProvider, out int total, out bool isReadyForShipment, out IEnumerable<Item> lineItems)
        {
            orderNumber = OrderNumber;
            shippingProvider = ShippingProvider;
            total = Total;
            isReadyForShipment = IsReadyForShipment;
            lineItems = LineItems;
        }


    }

    public class ProcessedOrder : Order { }

    public class Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }
        public decimal Weight { get; set; }
    }
}