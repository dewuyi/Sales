using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sales.services.Orders
{
    public interface IOrdersWriteRepository
    {
        void Save(Order order);
    }
}