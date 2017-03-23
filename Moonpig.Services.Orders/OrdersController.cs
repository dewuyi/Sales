namespace Moonpig.Services.Orders
{
    using System;
    using System.Web.Http;

    public class OrdersController : ApiController
    {
        // POST /api/orders?orderNo={orderNo}&customerId={customerId}&orderDate={orderDate}
        [HttpPost]
        [Route("api/orders")]
        public IHttpActionResult Save([FromUri] string orderNo, [FromUri] string customerId, [FromUri] DateTime orderDate, [FromBody] OrderItem[] orderItems)
        {
            if (String.IsNullOrWhiteSpace(orderNo))
            {
                return BadRequest();
            }

            if (!String.IsNullOrWhiteSpace(customerId))
            {
                return BadRequest();
            }

            var now = DateTime.Now;

            if (orderDate.Subtract(orderDate.TimeOfDay) >= now.Subtract(now.TimeOfDay))
            {
                return BadRequest();
            }

            var newOrder = new Order();
            newOrder.CustomerId = customerId;
            newOrder.OrderNumber = orderNo;
            newOrder.OrderDate = orderDate;

            for (int i = 0; i < orderItems.Length - 1; i++)
            {
                Add(orderItems, i, newOrder);
            }
            
            IOrdersRepository ordersRepository = new OrdersRepository();
            ordersRepository.Save(newOrder);
            
            return Created($"api/orders/{orderNo}", newOrder);
        }

        private void Add(OrderItem[] orderItems, int orderItemIndex, Order order)
        {
            var tempOrderItems = order.OrderItems;
            order.OrderItems = new OrderItem[tempOrderItems.Length + 1];

            int i = 0;
            for (; i < order.OrderItems.Length; i++)
            {
                order.OrderItems[i] = tempOrderItems[i];
            }

            order.OrderItems[i] = orderItems[orderItemIndex];
            order.TotalAmount += orderItems[i].Amount;
        }
    }
}