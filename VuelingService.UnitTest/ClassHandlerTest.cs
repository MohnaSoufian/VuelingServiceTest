using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;

namespace VuelingService.UnitTest
{
    [TestFixture]
    public class ClassHandlerTest
    {
        [Test]
        public void GetAllrates_XMLFileFound_ResultReturnList()
        {
            ClassHandler handler = new ClassHandler();

            List<string> list = handler.GetAllRates("http://quiet-stone-2094.herokuapp.com/rates.xml", "rate");

            Assert.IsNotEmpty(list);
        }

        //[Test]
        //public void SecondFunction()
        //{
        //    Assert.AreEqual(1, 0);
        //}
    }

}
