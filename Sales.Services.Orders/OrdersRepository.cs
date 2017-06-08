namespace Sales.services.Orders
{
    using System.Collections.Generic;
    using System.Linq;

    public class OrdersRepository:IOrdersRepository
    {
        List<Order> ordersList = new List<Order>();
        public Order ReadByOrdernumber(string orderNumber)
        {
            return this.Read().FirstOrDefault(o => o.OrderNumber == orderNumber);
        }

        public IQueryable<Order> Read()
        {
            return ordersList.AsQueryable();
        }
    }
}