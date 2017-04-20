using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace EquellaMetadataUtility
{
    public class ModifierRenameNode
    {
        public const string ModifierType = "Rename Node";
        public bool Enabled;
        public string currentXpath;
        public int modifierPosition = 0;
        public string renamedNode;

        private XmlElement root = null;

        public ModifierRenameNode()
        {
            currentXpath = "";
            renamedNode = "";
            Enabled = true;
        }

        public ModifierRenameNode(string inititalCurrentXpath, string initialRenamedXpath)
        {
            currentXpath = inititalCurrentXpath;
            renamedNode = initialRenamedXpath;
            Enabled = true;
        }

        public void setRoot(XmlElement rootParam)
        {
            root = rootParam;
        }

        public string validate()
        {
            if (currentXpath == "")
            {
                throw new System.InvalidOperationException("xpath cannot be left empty");
            }
            if (renamedNode == "")
            {
                throw new System.InvalidOperationException("new name cannot be left empty");
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<xml><item></item></xml>");


            XmlNodeList matchingNodes = xmlDoc.SelectNodes(currentXpath);

            XmlElement testElement = xmlDoc.CreateElement(renamedNode);

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

            string attributeName = "";
            // check if xpath is referring to an attribute
            if (currentXpath.Contains("/@"))
            {
                attributeName = currentXpath.Substring(currentXpath.LastIndexOf("/@") + 2);
                currentXpath = currentXpath.Substring(0, currentXpath.LastIndexOf("/@"));
            }

            XmlNodeList matchingNodes = root.SelectNodes(currentXpath);

            foreach (XmlNode matchingNode in matchingNodes)
            {
                XmlElement matchingElement = (XmlElement)matchingNode;
                if (attributeName == "")
                {

                    XmlNode newNode = replaceNode(matchingElement, renamedNode, xmlDoc);
                    XmlNode parentNode = matchingElement.ParentNode;
                    parentNode.ReplaceChild(newNode, matchingElement);
                }
                else
                {
                    if (matchingElement.HasAttribute(attributeName))
                    {
                        if (renamedNode != attributeName)
                        {
                            // throw an exception if trying to rename attribute to an existing attribute
                            if (matchingElement.HasAttribute(renamedNode)) throw new System.InvalidOperationException("new name cannot be left empty");
                            string value = matchingElement.GetAttribute(attributeName);
                            matchingElement.SetAttribute(renamedNode, value);
                            matchingElement.RemoveAttribute(attributeName);
                        }
                    }
                }
            }
        }
        private XmlNode replaceNode(XmlNode oldNode, string newNodeName, XmlDocument xmlDoc)
        {
            if (oldNode.NodeType == XmlNodeType.Element)
            {
                XmlElement oldElement = (XmlElement)oldNode;
                XmlElement newElement = xmlDoc.CreateElement(newNodeName);
                foreach (XmlNode xmlNode in oldNode.ChildNodes)
                {
                    newElement.AppendChild(xmlNode.Clone());
                }
                foreach (XmlAttribute xmlAttribute in oldElement.Attributes)
                {
                    newElement.SetAttributeNode((XmlAttribute)xmlAttribute.Clone());
                }
                return newElement;
            }
            else
            {
                return null;
            }
        }
    }
}
