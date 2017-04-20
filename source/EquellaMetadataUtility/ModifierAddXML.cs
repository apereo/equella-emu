using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace EquellaMetadataUtility
{
    public class ModifierAddXML
    {
        public const string ModifierType = "Add XML";
        public bool Enabled;
        public string xpath;
        public string addXML;
        public bool createNode = false;
        public int modifierPosition = 0;

        private XmlElement root = null;
        private Utils utils = new Utils();

        public ModifierAddXML()
        {
            xpath = "";
            addXML = "";
            Enabled = true;
        }

        public ModifierAddXML(string inititalXpath, string initialAddXML)
        {
            xpath = inititalXpath;
            addXML = initialAddXML;
            Enabled = true;
        }

        public void setRoot(XmlElement rootParam)
        {
            root = rootParam;
        }

        public string validate()
        {
            if (xpath == "")
            {
                throw new System.InvalidOperationException("XPath cannot be left empty");
            }
            if (addXML == "")
            {
                throw new System.InvalidOperationException("XML to add cannot be left empty");
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<xml><item></item></xml>");


            // check if xpath is valid on an empty item-stype document
            XmlNodeList matchingNodes = xmlDoc.SelectNodes(xpath);

            // validate AddXML text
            XmlDocumentFragment xfrag = xmlDoc.CreateDocumentFragment();
            xfrag.InnerXml = addXML;
            XmlElement matchingElement = (XmlElement)xmlDoc.SelectNodes("/xml/item")[0];
            matchingElement.AppendChild(xfrag);
 
            return "Modifier is correctly formatted.";
        }

        // append subtree to all nodes at xpath with addXML
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

        public void transform(XmlDocument xmlDoc)
        {
            // if no root set then root element as document root
            if (root == null)
            {
                root = (XmlElement)xmlDoc.SelectSingleNode("/*");
            }

            // if required, create elements that don't exist in the target xpath
            if (createNode)
            {
                utils.createXpath(xmlDoc, root, xpath);
            }

            // get matching elements
            XmlNodeList matchingNodes = root.SelectNodes(xpath);

            foreach (XmlNode matchingNode in matchingNodes)
            {
                XmlElement matchingElement = (XmlElement)matchingNode;

                // create AddXML fragment
                XmlDocumentFragment xfrag = xmlDoc.CreateDocumentFragment();
                xfrag.InnerXml = addXML;

                // strip fragment (with recursion)
                foreach (XmlNode childNode in xfrag.ChildNodes)
                    if (childNode.NodeType == XmlNodeType.Element)
                        utils.stripNode(childNode, true);
                matchingElement.AppendChild(xfrag);

                // strip parent element (no recursion)
                utils.stripNode(matchingElement, false);
            }
        }
    }
}
