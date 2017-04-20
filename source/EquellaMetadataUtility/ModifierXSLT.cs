using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Xml.Serialization;


namespace EquellaMetadataUtility
{
    public class ModifierXSLT
    {
        public const string ModifierType = "XSLT";
        public bool Enabled;
        public string XSLTtext;

        private XmlElement root = null;

        public ModifierXSLT()
        {
            XSLTtext = "";
            Enabled = true;
        }

        public ModifierXSLT(string initialXSLTtext)
        {
            XSLTtext = initialXSLTtext;
            Enabled = true;
        }

        public void setRoot(XmlElement rootParam)
        {
            root = rootParam;
        }

        public void validate()
        {
            if (XSLTtext.Trim() == "")
            {
                throw new System.InvalidOperationException("XSLT cannot be left empty");
            }

            // check if transform is valid on an empty item-stype document
            XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
            xslCompiledTransform.Load(new XmlTextReader(new StringReader(XSLTtext)));
            
            XmlReader inputXmlReader = XmlReader.Create(new StringReader("<xml><item></item></xml>"));

            StringWriter sw = new StringWriter();
            XmlWriter outputXmlWriter = XmlWriter.Create(sw);

            xslCompiledTransform.Transform(inputXmlReader, outputXmlWriter);

        }

        // transform with XSLT
        public string transform(string inputXML)
        {
            string newXMLString;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(inputXML);

            //transform XML
            transform(xmlDoc);

            StringWriter sw = new StringWriter();
            XmlTextWriter xw = new XmlTextWriter(sw);
            xmlDoc.WriteTo(xw);
            newXMLString = sw.ToString();

            return newXMLString;
        }

        // apply XSLT to inputXML
        public void transform(XmlDocument xmlDoc)
        {
            // if no root set then root element as document root
            if (root == null)
            {
                root = (XmlElement)xmlDoc.SelectSingleNode("/*");
            }

            StringWriter sw = new StringWriter();
            XmlWriter xw = new XmlTextWriter(sw);
            xmlDoc.WriteTo(xw);
            string inputXMLstring = sw.ToString();
            
            XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
            xslCompiledTransform.Load(new XmlTextReader(new StringReader(XSLTtext)));

            XmlReader inputXmlReader = XmlReader.Create(new StringReader(inputXMLstring));

            StringWriter outputStringWriter = new StringWriter();
            XmlWriter outputXmlWriter = XmlWriter.Create(outputStringWriter);

            xslCompiledTransform.Transform(inputXmlReader, outputXmlWriter);

            string newXMLstring = outputStringWriter.ToString();
            xmlDoc.LoadXml(newXMLstring);
        }

    }
}
