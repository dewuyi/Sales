using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using NSubstitute;

namespace Sales.services.Orders
{
    [TestFixture]
    public class OrderServiceTests
    {
        private IOrdersRepository ordersRepository;
        private IOrdersWriteRepository ordersWriteRepository;
        private IOrderValidation orderValidation;

        [SetUp]
        public void SetUp()
        {
            this.ordersRepository = Substitute.For<IOrdersRepository>();
            this.ordersWriteRepository = Substitute.For<IOrdersWriteRepository>();
            this.orderValidation = new OrderValidation();
        }
        [Test]
        public void CreateOrder_Returns_Order()
        {
            IOrderService orderService = new OrderService(this.ordersWriteRepository);
            OrderItem[] orderItems =
             {
          new OrderItem
          {
            ItemName = "Book",
            Amount = 20
          },
          new OrderItem
          {
            ItemName = "Card",
            Amount = 10
          },
          new OrderItem
          {
            ItemName = "Pen",
            Amount = 5
          }
        };

          Order order =  orderService.Create("YN59-0483", "Customer4", new DateTime(2014, 10, 2), orderItems);
            Order newOrder = new Order
            {
                CustomerId = "Customer4",
                OrderDate = new DateTime(2014, 10, 2),
                OrderNumber = "YN59-0483",
                OrderItems = orderItems
            };
            Assert.AreEqual(order.CustomerId, newOrder.CustomerId);
            Assert.AreEqual(order.OrderNumber, newOrder.OrderNumber);
            Assert.AreEqual(order.OrderDate, newOrder.OrderDate);
            Assert.AreEqual(order.OrderItems, newOrder.OrderItems);
        }
    }
}