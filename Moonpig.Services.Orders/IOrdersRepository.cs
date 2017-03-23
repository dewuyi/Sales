namespace Moonpig.Services.Orders
{
    public interface IOrdersRepository
    {
        void Save(Order order);
    }
}