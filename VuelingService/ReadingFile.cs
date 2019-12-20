using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;

namespace VuelingService
{
    public class ReadingFile
    {

        // In this method we read from an input URL a XML file using XmlReader Object, then we add the XElement in IEnumerable
        public IEnumerable<XElement> SimpleStreamAxis(string inputUrl,
                                              string elementName)
        {
            using (XmlReader reader = XmlReader.Create(inputUrl))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == elementName)
                        {
                            XElement el = XElement.ReadFrom(reader) as XElement;
                            if (el != null)
                            {
                                yield return el;
                            }
                        }
                    }
                }
            }
        }

        // In this method we read a XML file and change it to a list of XElement
        public List<XElement> XmlFileToList(string inputUrl, string balise)

        {

            IEnumerable<XElement> xBids =SimpleStreamAxis(inputUrl, balise);
            return ((from el in xBids select el).ToList());

        }

    }
}