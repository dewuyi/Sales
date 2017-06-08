using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sales.services.Orders
{
    public class OrderService : IOrderService
    {
        private IOrdersWriteRepository ordersWriteRepository;
        public OrderService(IOrdersWriteRepository ordersWriteRepository)
        {
            this.ordersWriteRepository = ordersWriteRepository;
        }
        public Order Create(string orderNo, string customerId, DateTime orderDate, OrderItem[] orderItems)
        {
            var newOrder = new Order
            {
                CustomerId = customerId,
                OrderNumber = orderNo,
                OrderDate = orderDate,
                OrderItems = orderItems
            };
            foreach (OrderItem orderItem in newOrder.OrderItems)
            {
                newOrder.TotalAmount += orderItem.Amount;
                orderItem.ParentOrder = newOrder;
            }
            this.ordersWriteRepository.Save(newOrder);
            return newOrder;
        }
    }
}