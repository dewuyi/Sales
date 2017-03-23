namespace Moonpig.Services.Orders
{
    using System;
    
    public class Order
    {
        public string OrderNumber { get; set; }

        public string CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderItem[] OrderItems { get; set; }

        public Decimal TotalAmount { get; set; }
    }

    public class OrderItem
    {
        public Order ParentOrder { get; set; }

        public string ItemName { get; set; }

        public decimal Amount { get; set; }
    }
}