using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;


namespace EquellaMetadataUtility
{
    public class ModifierReplaceText
    {
        public const string ModifierType = "Replace Text";
        public bool Enabled;
        public string xpath;
        public string findText;
        public string replaceWithText;
        public int modifierPosition = 0;

        private XmlElement root = null;
        private Utils utils = new Utils();

        public ModifierReplaceText()
        {
            xpath = "";
            findText = "";
            replaceWithText = "";
            Enabled = true;
        }

        public ModifierReplaceText(string inititalXpath, string initialfindText, string initialReplaceWithText)
        {
            xpath = inititalXpath;
            findText = initialfindText;
            replaceWithText = initialReplaceWithText;
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

        // update text in any nodes at xpath in xmlDoc with updateText
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
                // check if attribute and modify attribute value instead
                if (xpath.Contains('@'))
                {
                    // replace attribute text
                    matchingNode.Value = matchingNode.Value.Replace(findText, replaceWithText);
                }
                else
                {
                    XmlElement matchingElement = (XmlElement)matchingNode;
                    XmlNode matchingElementTextNode = utils.GetTextNode(matchingElement);
                    if (matchingElementTextNode != null)
                    {
                        // replace element text
                        matchingElementTextNode.Value = matchingElementTextNode.Value.Replace(findText, replaceWithText);
                    }
                }
            }
        }
    }
}
