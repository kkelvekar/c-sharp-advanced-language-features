﻿namespace WarehouseManagementSystem.Domain
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

        public IEnumerable<Order> GetNewOrders()
        {
            return new List<Order>
            {
                new Order
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
                },
                new Order
                {
                    ShippingProvider = new SwedishPostalServiceShippingProvider
                    {
                        Name = "British Postal Service",
                        FreightCost = 300,
                        DeliverNextDay = true
                    },
                    LineItems = new List<Item>
                    {
                        new Item { Name = "Item 1", Price = 500, InStock = true },
                        new Item { Name = "Item 2", Price = 700, InStock = true }
                    }
                }
            };
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
    }
}