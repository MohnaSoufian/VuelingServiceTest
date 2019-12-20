using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;

namespace VuelingService
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    
    
    public class WebService1 : System.Web.Services.WebService
    {
        // mock : datos falsos  crear producto con divisas y hacer test
        // caso divisa no exista, divisa se puede calcular


        // Get all rates method
        //**********************
        [WebMethod]
        public List<string> GetAllRates()
        {
            ClassHandler handlerObject = new ClassHandler();
            return handlerObject.FirstFunction();

        }

        // Get all transaction method
        //*************************
        [WebMethod]
        public List<String> GetAllTransactions()
        {
            ClassHandler handlerObject = new ClassHandler();
            return handlerObject.SecondFunction();
        }

        // Get all product transaction in Euro 
        // we use an object of handlerObject
        [WebMethod]
        public List<String> GetTransactionsInEuro( string sku)
        {
            ClassHandler handlerObject = new ClassHandler();
            return handlerObject.ThirdFunction(sku);
        }

    }
}