using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;

namespace Sales.services.Orders
{
    [TestFixture]
    public class OrderValidationTests
    {
        [Test]
        public void ValidateOrder_AllNumbers_ReturnsFalse()
        {
            OrderValidation orderValidation = new OrderValidation();
            Boolean result = orderValidation.OrderNumberIsValid("1250-2315");
            Assert.AreEqual(result, false);
        }

        [Test]
        public void ValidateOrder_SevenCharacters_ReturnsFalse()
        {
            OrderValidation orderValidation = new OrderValidation();
            Boolean result = orderValidation.OrderNumberIsValid("YN50-231");
            Assert.AreEqual(result, false);
        }

        [Test]
        public void ValidateOrder_Returns_True()
        {
            OrderValidation orderValidation = new OrderValidation();
            Boolean result = orderValidation.OrderNumberIsValid("YN50-2315");
            Assert.AreEqual(result, true);
        }

    }
}