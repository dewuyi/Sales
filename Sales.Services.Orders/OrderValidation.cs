using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace Sales.services.Orders
{
    public class OrderValidation:IOrderValidation
    {
        public Boolean OrderNumberIsValid(string orderNo)
        {
            if (orderNo.Length != 9)
            {
                return false;
            }
            Regex rgx = new Regex(@"^[Y][N]\d{2}(-\d{4})$");
            return rgx.IsMatch(orderNo);
        }
    }
}