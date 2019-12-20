using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using NUnit.Framework;
using VuelingService;

namespace VuelingService.UnitTest
{

    [TestFixture]
    class ConverterTest
    {
        /*
         */
        [Test]
        public void GetExistingRate_RateFound_RateResult()
        {
            Converter converter = new Converter();
            string fr = "EUR";
            string to = "CAD";
            List<XElement> list = new List<XElement>();
            list.Add(GetElement(@"<rate from = ""EUR""  to = ""CAD"" rate = ""0.72"" />"));


            decimal result = converter.GetExistingRate(fr, to, list);

            Assert.IsTrue(result == (decimal)0.72);
        }


        [Test]
        public void GetExistingRate_RateNofound_RateResult()
        {
            Converter converter = new Converter();
            string fr = "EUR";
            string to = "CAD";
            List<XElement> list = new List<XElement>();
            list.Add(GetElement(@"<rate from = ""EUR""  to = ""USD"" rate = ""0.72"" />"));


            decimal result = converter.GetExistingRate(fr, to, list);

            Assert.IsTrue(result == 0);
        }
        [Test]
        public void GetExistingRate_EmptyList_RateResult()
        {
            Converter converter = new Converter();
            string fr = "EUR";
            string to = "CAD";
            List<XElement> list = new List<XElement>();



            decimal result = converter.GetExistingRate(fr, to, list);

            Assert.IsTrue(result == 0);
        }

        [Test]
        public void HasToBeConverted_RateFound_TheResultIsFalse()
        {
            Converter converter = new Converter();
            string fr = "EUR";
            string to = "CAD";
            List<XElement> list = new List<XElement>();
            list.Add(GetElement(@"<rate from = ""EUR""  to = ""CAD"" rate = ""0.72"" />"));


            bool result = converter.HasToBeConverted(fr, to, list);

            Assert.IsTrue(result==false);
        }

        [Test]
        public void HasToBeConverted_RateNotFound_TheResultIsTrue()
        {
            Converter converter = new Converter();
            string fr = "EUR";
            string to = "CAD";
            List<XElement> list = new List<XElement>();



            bool result = converter.HasToBeConverted(fr, to, list);

            Assert.IsTrue(result == true);
        }


        [Test]
        public void GetConvertedRate_RateNotFound_TheResultIsADecimalValue()
        {
            Converter converter = new Converter();
            string fr = "EUR";
            string to = "USD";
            List<XElement> list = new List<XElement>();
            list.Add(GetElement(@"<rate from = ""EUR""  to = ""CAD"" rate = ""0.72"" />"));
            list.Add(GetElement(@"<rate from = ""CAD""  to = ""USD"" rate = ""0.72"" />"));



            decimal result = converter.GetConvertedRate(fr, to, list);

            Assert.IsTrue(result == (decimal)0.5184);
        }

        [Test]
        public void GetConvertedRate_RateFound_TheResultIsZero()
        {
            Converter converter = new Converter();
            string fr = "KSH";
            string to = "USD";
            List<XElement> list = new List<XElement>();
            list.Add(GetElement(@"<rate from = ""EUR""  to = ""CAD"" rate = ""0.72"" />"));
            list.Add(GetElement(@"<rate from = ""CAD""  to = ""USD"" rate = ""0.72"" />"));



            decimal result = converter.GetConvertedRate(fr, to, list);

            Assert.IsTrue(result == 0);
        }

        [Test]
        public void Converting_RateFoundDirectly_TheResultToBeCalculated()
        {
            Converter converter = new Converter();
            string fr = "CAD";
            string to = "USD";
            List<XElement> list = new List<XElement>();
            list.Add(GetElement(@"<rate from = ""EUR""  to = ""CAD"" rate = ""0.52"" />"));
            list.Add(GetElement(@"<rate from = ""CAD""  to = ""USD"" rate = ""0.72"" />"));



            decimal result = converter.Converting(fr, to, list, 10);

            Assert.IsTrue(result == (decimal)7.2);
        }

        [Test]
        public void Converting_RateFoundButNotDirectly_TheResultToBeCalculated()
        {
            Converter converter = new Converter();
            string fr = "EUR";
            string to = "USD";
            List<XElement> list = new List<XElement>();
            list.Add(GetElement(@"<rate from = ""EUR""  to = ""CAD"" rate = ""0.52"" />"));
            list.Add(GetElement(@"<rate from = ""CAD""  to = ""USD"" rate = ""0.72"" />"));



            decimal result = converter.Converting(fr, to, list, 10);

            Assert.IsTrue(result == (decimal)3.744);
        }

        [Test]
        public void Converting_RateNotFound_TheResultToBeCalculated()
        {
            Converter converter = new Converter();
            string fr = "KSH";
            string to = "USD";
            List<XElement> list = new List<XElement>();
            list.Add(GetElement(@"<rate from = ""EUR""  to = ""CAD"" rate = ""0.72"" />"));
            list.Add(GetElement(@"<rate from = ""CAD""  to = ""USD"" rate = ""0.72"" />"));



            decimal result = converter.Converting(fr, to, list, 10);

            Assert.IsTrue(result == 0);
        }


        private XElement GetElement(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            return XElement.Parse(doc.DocumentElement.OuterXml);
        }

    }
}
