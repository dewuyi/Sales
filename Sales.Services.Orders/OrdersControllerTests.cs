using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using System.Web.Http;
using System.Web.Http.Results;
using NSubstitute;


namespace Sales.services.Orders
{
    [TestFixture]
    public class OrdersControllerTests
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
        public void SaveOrderNoCustomerId_Returns_BadRequest()
        {
            OrdersController ordersController = new OrdersController(this.ordersRepository, this.ordersWriteRepository, this.orderValidation);
            Order newOrder = new Order
            {
                OrderNumber = "YN00-0000",
                CustomerId = "Customer1",
                OrderDate = new DateTime(2016, 06, 15),
                OrderItems = new[]
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
        }
            };


            IHttpActionResult actionResult = ordersController.Save(newOrder.OrderNumber, "", DateTime.Now, newOrder.OrderItems);
            Assert.IsTrue(actionResult.GetType() == typeof(BadRequestResult));
        }

        [Test]
        public void SaveOrderInvalidOrderNumber_Returns_BadRequest()
        {
            OrdersController ordersController = new OrdersController(this.ordersRepository, this.ordersWriteRepository, this.orderValidation);
            Order newOrder = new Order
            {
                OrderNumber = "111",
                CustomerId = "Customer1",
                OrderDate = new DateTime(2016, 06, 15),
                OrderItems = new[]
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
        }
            };
            IHttpActionResult actionResult = ordersController.Save(newOrder.OrderNumber, newOrder.CustomerId,
              new DateTime(2016, 06, 23), newOrder.OrderItems);
            Assert.IsTrue(actionResult.GetType() == typeof(BadRequestResult));
        }

        [Test]
        public void SaveOrder_Returns_CreatedNegotiatedContent()
        {
            OrdersController ordersController = new OrdersController(this.ordersRepository, this.ordersWriteRepository, this.orderValidation);
            Order newOrder = new Order
            {
                OrderNumber = "YN50-2315",
                CustomerId = "Customer1",
                OrderDate = new DateTime(2016, 06, 15),
                OrderItems = new[]
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
        }
            };
            IHttpActionResult actionResult = ordersController.Save(newOrder.OrderNumber, newOrder.CustomerId,
              new DateTime(2016, 06, 23), newOrder.OrderItems);
            Assert.IsTrue(actionResult.GetType() == typeof(CreatedNegotiatedContentResult<Order>));
        }

        [Test]
        public void ReadOrder_Returns_NotFound()
        {
            OrdersController ordersController = new OrdersController(this.ordersRepository, this.ordersWriteRepository, this.orderValidation);
            this.ordersRepository.Read().Returns(new List<Order>
      {
        new Order
        {
          OrderNumber = "YN50-2315",
          CustomerId = "Customer1",
          OrderDate = new DateTime(2016, 06, 15),
          OrderItems = new[]
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
          }
        },
        new Order
        {
          OrderNumber = "YN88-2005",
          CustomerId = "Customer1",
          OrderDate = new DateTime(2016, 04, 02),
          OrderItems = new[]
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
          }
        }
      }.AsQueryable());

            IHttpActionResult actionResult = ordersController.ReadOrder("YN88-4859");
            Assert.IsTrue(actionResult.GetType() == typeof(NotFoundResult));
        }


    }
}