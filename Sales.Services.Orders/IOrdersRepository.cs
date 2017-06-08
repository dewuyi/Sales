namespace Sales.services.Orders
{
    using System.Linq;
    public interface IOrdersRepository
    {
        Order ReadByOrdernumber(string orderNumber);
        IQueryable<Order> Read();
    }
}