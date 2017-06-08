using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.services.Orders
{
    public interface IOrderService
    {
        Order Create(string orderNo, string customerId, DateTime orderDate, OrderItem[] orderItems);
    }
}
