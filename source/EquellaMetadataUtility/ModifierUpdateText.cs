using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;


namespace EquellaMetadataUtility
{
    public class ModifierUpdateText
    {
        public const string ModifierType = "Update Text";
        public bool Enabled;
        public string xpath;
        public string updateText;
        public bool createNode = false;
        public int modifierPosition = 0;

        private XmlElement root = null;
        private Utils utils = new Utils();

        public ModifierUpdateText()
        {
            xpath = "";
            updateText = "";
            Enabled = true;
        }

        public ModifierUpdateText(string inititalXpath, string initialUpdateText)
        {
            xpath = inititalXpath;
            updateText = initialUpdateText;
            Enabled = true;
        }

        public void setRoot(XmlElement rootParam)
        {
            root = rootParam;
        }

        public string validate()
        {
            if(xpath == "")
            {
                throw new System.InvalidOperationException("xpath cannot be left empty");
            }

            // check if processed xpath is valid on an empty item-type document
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<xml><item></item></xml>");
            XmlNodeList matchingNodes = xmlDoc.SelectNodes(xpath);

            return "Modifier is correctly formatted.";
        }

        // update text in any nodes at xpath in xmlDoc with updateText
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

            // if required, create elements and attributes that don't exist in the xpath
            if (createNode)
            {
                utils.createXpath(xmlDoc, root, xpath);
            }

            // set attribute or append/update a text node

            // check if xpath is referring to an attribute
            string[] xpathParts = xpath.Split(new String[] { "/" }, StringSplitOptions.None);
            if (xpathParts[xpathParts.Length - 1].StartsWith("@"))
            {
                string attributeName = xpathParts[xpathParts.Length - 1].Substring(1, xpathParts[xpathParts.Length - 1].Length - 1);
                string parentXpath = "";
                for (int i = 0; i < xpathParts.Length - 1; i++)
                {
                    parentXpath += xpathParts[i] + "/";
                }

                // trim last slash
                if (parentXpath.EndsWith("/")) parentXpath = parentXpath.Substring(0, parentXpath.Length - 1);

                // prepend slash if orignal xpath starts with one
                if (xpath.StartsWith("/")) parentXpath = "/" + parentXpath;

                // query on slash if no mathing nodes found
                if (parentXpath == "") parentXpath = ".";

                XmlNodeList matchingNodes = root.SelectNodes(parentXpath);
                foreach (XmlNode matchingNode in matchingNodes)
                {
                    ((XmlElement)matchingNode).SetAttribute(attributeName, updateText);
                }
            }
            else
            {
                XmlNodeList matchingNodes = root.SelectNodes(xpath);
                foreach (XmlNode matchingNode in matchingNodes)
                {
                    XmlElement matchingElement = (XmlElement)matchingNode;
                    XmlNode matchingElementTextNode = utils.GetTextNode(matchingElement);

                    // either update existing text node or append a new one
                    if (matchingElementTextNode != null)
                    {
                        matchingElementTextNode.Value = updateText;
                    }
                    else
                    {
                        matchingElement.AppendChild(xmlDoc.CreateTextNode(updateText));
                    }
                }
            }
        }
    }
}
