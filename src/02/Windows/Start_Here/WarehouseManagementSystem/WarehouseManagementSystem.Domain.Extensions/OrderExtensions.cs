using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Domain.Extensions
{
    public static class OrderExtensions
    {
        public static List<Order> GetNewOrders(this List<Order> orders)
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
                    ShippingProvider = new BritishPostalServiceShippingProvider
                    {
                        Name = "British Postal Service",
                        FreightCost = 300,
                        DeliverNextDay = true,
                        VAT = 0.2m
                    },
                    LineItems = new List<Item>
                    {
                        new Item { Name = "Item 1", Price = 500, InStock = true },
                        new Item { Name = "Item 2", Price = 700, InStock = true }
                    }
                }
            };
        }
    }
}
