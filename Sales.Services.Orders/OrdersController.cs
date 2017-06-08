
    using System;
    using System.Web.Http;
    using System.Collections.Generic;
    using System.Linq;

    namespace Sales.services.Orders
    {
        using System;
        using System.Web.Http;

        public class OrdersController : ApiController
        {
            private IOrdersRepository ordersRepository;
            private IOrdersWriteRepository ordersWriteRepository;
            private IOrderValidation orderValidation;

            public OrdersController(IOrdersRepository ordersRepository, IOrdersWriteRepository ordersWriteRepository, IOrderValidation orderValidation)
            {
                this.ordersRepository = ordersRepository;
                this.ordersWriteRepository = ordersWriteRepository;
                this.orderValidation = orderValidation;
            }

            // POST /api/orders?orderNo={orderNo}&customerId={customerId}&orderDate={orderDate}
            [HttpPost]
            [Route("api/orders")]
            public IHttpActionResult Save([FromUri] string orderNo, [FromUri] string customerId, [FromUri] DateTime orderDate,
              [FromBody] OrderItem[] orderItems)
            {
                if (String.IsNullOrWhiteSpace(orderNo))
                {
                    return BadRequest();
                }

                if (String.IsNullOrWhiteSpace(customerId))
                {
                    return BadRequest();
                }

                if (!this.orderValidation.OrderNumberIsValid(orderNo))
                {
                    return BadRequest();
                }

                var now = DateTime.Now;

                if (orderDate.Subtract(orderDate.TimeOfDay) >= now.Subtract(now.TimeOfDay))
                {
                    return BadRequest();
                }

                var newOrder = this.CreateOrder(orderNo, customerId, orderDate, orderItems);

                return Created($"api/orders/{orderNo}", newOrder);
            }

            public Order CreateOrder(string orderNo, string customerId, DateTime orderDate, OrderItem[] orderItems)
            {
                IOrderService orderService = new OrderService(this.ordersWriteRepository);
                return orderService.Create(orderNo, customerId, orderDate, orderItems);
            }

            public IHttpActionResult ReadOrder(string orderNo)
            {
                if (!this.orderValidation.OrderNumberIsValid(orderNo))
                {
                    return NotFound();
                }

                Order order = this.ordersRepository.ReadByOrdernumber(orderNo);
                if (order == null)
                {
                    return NotFound();
                }

                return Created($"api/orders/{orderNo}", order);
            }


        }
    }
