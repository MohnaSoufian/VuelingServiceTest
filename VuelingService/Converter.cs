using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace VuelingService
{
    public class Converter
    {
        /*In this method, we check in a list of XElement if a rate from one giving devise to an other exist or not.
         * if it exists we return the rate value, if not we return 0.
         */
        public decimal GetExistingRate(string fr, string to, List<XElement> rates)
        {
            var rateList = (from el in rates
                            where (string)el.Attribute("from") == fr && (string)el.Attribute("to") == to
                            select (decimal)el.Attribute("rate"));

            ExeptionLog.Save(" GetExistingRate method  result" + rateList.SingleOrDefault());

            return (rateList != null && rateList.Count() != 0) ? rateList.SingleOrDefault() : 0;
        }


        /* in this method, we check if a rate of two giving devises exist or not*/
        public bool HasToBeConverted(string fr, string to, List<XElement> rates)
        {
            bool result = false;
            decimal rate = GetExistingRate(fr, to, rates);
            if (rate == 0)
                result = true;
            return result;
        }
        // GetConverterRate method returns 0 if we couldn't calculate the rate
        // this method will be used two times: exchanging parameters.
        public decimal GetConvertedRate(string fr, string to, List<XElement> rates)
        {
            decimal result = 0;
            ReadingFile Reader = new ReadingFile();
            var posibleList = (from el in rates
                               where (string)el.Attribute("from") == fr
                               select el).ToList();
            ExeptionLog.Save("methodo GetConvertedRate  posibleList.Count() is:  " + posibleList.Count());
            if (posibleList.Count() != 0 && posibleList != null)
            {
                foreach (XElement e in posibleList)
                {
                    string temporaryTo = e.Attribute("to") != null ? e.Attribute("to").Value : "";
                    decimal temporaryRate = decimal.Parse(e.Attribute("rate").Value);
                    var temporary = (from el in rates
                                     where (string)el.Attribute("from") == temporaryTo && (string)el.Attribute("to") == to
                                     select (decimal)el.Attribute("rate"));
                    decimal temporaryRate2 = temporary != null ? temporary.SingleOrDefault() : 0;
                    if (temporaryRate2 != 0)
                    {
                        result = temporaryRate * temporaryRate2;
                        ExeptionLog.Save(" the result of GetConvertedRate When temporaryRate2 !=0  = " + result);
                        break;
                    }

                }

            }


            return result;

        }
        // Converting method, giving an amount in a certain devide, we convert it to an other devise 
        public decimal Converting(string fr, string to, List<XElement> rates, decimal amount)
        {
            decimal rate;
            decimal amountEuro;

            if (HasToBeConverted(fr, to, rates))
            {

                rate = GetConvertedRate(fr, to, rates);
                amountEuro = amount * rate;
                ExeptionLog.Save("method Converting when HasToBeConverted amountEuro is:  " + amountEuro);
            }
            else
            {
                rate = GetExistingRate(fr, to, rates);
                amountEuro = amount * rate;
                ExeptionLog.Save("method Converting when not HasToBeConverted amountEuro is:  " + amountEuro);
            }
            return amountEuro;

        }

    }
}