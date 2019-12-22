using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace VuelingService
{
    public class ClassHandler
    {

        // The first method does the follwing actions:
        // - Calls an object reader to read the XML file and change it to a list of XElement
        // - We iterate over the list and we save the different attribute in a list of strings
        // - We handle the expections if an error occurs when loading the XML file and save the error in a log file

        public List<string> GetAllRates(string url , string element)
        {
            List<string> list = new List<string>();
            ReadingFile Reader = new ReadingFile();
            try
            {
                var rates = Reader.XmlFileToList(url, element);
                foreach (XElement e in rates)
                {
                    list.Add("rate is: " + (e.Attribute("rate") != null ? e.Attribute("rate").Value : "")
                        + " from = " + (e.Attribute("from") != null ? e.Attribute("from").Value : "")
                        + " to =  " + (e.Attribute("to") != null ? e.Attribute("to").Value : ""));
                }
                ExeptionLog.Save("first method runs perfectly");
            }
            catch (Exception e)
            {
                ExeptionLog.Save(this, e);
                list.Add("Information not available, try again later!");

            }
            
            return list;
        }

        // The second method does the follwing actions (basically the same actions that the first method does):
        // - Calls an object reader to read the XML file and change it to a list of XElement
        // - We iterate over the list and we save the different attribute in a list of strings
        // - We handle the expections if an error occurs when loading the XML file and save the error in a log file

        public List<String> SecondFunction()
        {
            List<string> list = new List<string>();
            ReadingFile Reader = new ReadingFile();
            try
            {

                var transactions = Reader.XmlFileToList("http://quiet-stone-2094.herokuapp.com/transactions.xml", "transaction");
                foreach (XElement e in transactions)
                {
                    list.Add("sku is: " + (e.Attribute("sku") != null ? e.Attribute("sku").Value : "")
                        + " amount = " + (e.Attribute("amount") != null ? e.Attribute("amount").Value : "")
                        + " currency =  " + (e.Attribute("currency") != null ? e.Attribute("currency").Value : ""));

                }
                ExeptionLog.Save("first method runs perfectly");
            }
            catch (Exception e)
            {
                ExeptionLog.Save(this, e);
                list.Add("Information not available, try again later!");
            }


            return list;
        }

         
        /* The third method calculate the amount in euro of all transaction that belongs to a specific product
         * So, we also read and change the XML file to a list of XElement, then 
         * we select the XElement having a specific 'SKU' attribute.
         * Now that we have a list of XElement with the same SKU attribute, we check if the 'currency' attribute
         * is in EURO, if it is We add the amount to the counting 
         * if it's not,  we convert the amount to EURO the we add it to the counting result.
         * Also we catch the exception if an error occures when reading the xml file and  report the error in a log file
             */
        public List<String> ThirdFunction(string sku)
        {
            decimal result = 0;
            List<string> list = new List<string>();
            ReadingFile Reader = new ReadingFile();
            Converter converter = new Converter();
            try
            {
                var transaction = Reader.XmlFileToList("http://quiet-stone-2094.herokuapp.com/transactions.xml", "transaction");
                var transactionSku = (from el in transaction where (string)el.Attribute("sku") == sku select el).ToList();

                if (transactionSku.Count() != 0)
                {

                    foreach (XElement e in transactionSku)
                    {
                        if ((string)e.Attribute("currency") != "EUR")
                        {
                            decimal elementAmount = converter.Converting((string)e.Attribute("currency"), "EUR", transactionSku, decimal.Parse(e.Attribute("amount").Value));
                            result += elementAmount;
                            list.Add("sku is: " + sku + " amount = " + elementAmount + " currency = EUR  ");
                        }
                        else
                        {
                            result += decimal.Parse(e.Attribute("amount").Value);
                            list.Add("sku is: " + sku + " amount = " + decimal.Parse(e.Attribute("amount").Value) + " currency = EUR  ");
                        }

                    }
                    list.Add(" TOTAL AMOUNT IS  " + result);

                }
                else
                {
                    list.Add(" transaction not found");
                }


            }
            catch (Exception e)
            {
                ExeptionLog.Save(this, e);
                list.Add(" Information not found, try again later!");
            }
            


            return list;

        }
    }
}