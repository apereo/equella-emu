using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace EquellaMetadataUtility
{
    public class ModifierRemoveNode
    {
        public const string ModifierType = "Remove Node";
        public bool Enabled;
        public string xpath;
        public int modifierPosition = 0;

        private XmlElement root = null;

        public ModifierRemoveNode()
        {
            xpath = "";
            Enabled = true;
        }

        public ModifierRemoveNode(string inititalXpath)
        {
            xpath = inititalXpath;
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
                throw new System.InvalidOperationException("xpath cannot be left empty");
            }

            // check if xpath is valid on an empty item-stype document
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<xml><item></item></xml>");

            XmlNodeList matchingNodes = xmlDoc.SelectNodes(xpath);

            return "Modifier is correctly formatted.";

        }

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

            XmlNodeList matchingNodes = root.SelectNodes(xpath);

            foreach (XmlNode matchingNode in matchingNodes)
            {
                if (matchingNode.NodeType == XmlNodeType.Attribute)
                    ((XmlAttribute)matchingNode).OwnerElement.RemoveAttribute(matchingNode.Name);
                else
                    matchingNode.ParentNode.RemoveChild(matchingNode);
            }
        }
    }
}
