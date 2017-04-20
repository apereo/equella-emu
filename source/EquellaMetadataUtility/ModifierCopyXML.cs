using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace EquellaMetadataUtility
{
    public class ModifierCopyXML
    {
        public const string ModifierType = "Copy XML";
        public bool Enabled;
        public string sourceNode;
        public int modifierPosition = 0;
        public string targetXpath;
        public bool createNode = false;

        private XmlElement root = null;
        private Utils utils = new Utils();

        public ModifierCopyXML()
        {
            sourceNode = "";
            targetXpath = "";
            Enabled = true;
        }

        public ModifierCopyXML(string inititalSourceNode, string initialTargetXpath)
        {
            sourceNode = inititalSourceNode;
            targetXpath = initialTargetXpath;
            Enabled = true;
        }

        public void setRoot(XmlElement rootParam)
        {
            root = rootParam;
        }

        public string validate()
        {
            if (sourceNode == "")
            {
                throw new System.InvalidOperationException("Source Node cannot be left empty");
            }
            if (targetXpath == "")
            {
                throw new System.InvalidOperationException("Target XPath cannot be left empty");
            }

            // form test XmlDoc
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<xml><item></item></xml>");
            XmlElement root = (XmlElement)xmlDoc.SelectSingleNode("/*");
            XmlNodeList matchingSourceNodes;

            matchingSourceNodes = root.SelectNodes(sourceNode);
            matchingSourceNodes = root.SelectNodes(targetXpath);

            return "Modifier is correctly formatted.";
        }

        // clone a node and all it's descendents
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

            // get matching source nodes
            XmlNodeList matchingSourceNodes = root.SelectNodes(sourceNode);

            // if required, create elements that don't exist in the target xpath
            if ((matchingSourceNodes.Count > 0) && createNode)
            {
                utils.createXpath(xmlDoc, root, targetXpath);
            }

            foreach (XmlNode matchingSourceNode in matchingSourceNodes)
            //if (matchingSourceNodes.Count > 0)
            {
                XmlElement sourceElement = (XmlElement)matchingSourceNode;

                XmlNodeList matchingTargetNodes = root.SelectNodes(targetXpath);

                // iterate through target nodes, clone source node and append to each target node
                XmlElement matchingElement;
                foreach (XmlNode matchingTargetNode in matchingTargetNodes)
                {
                    XmlNode clonedNode = sourceElement.Clone();
                    matchingElement = (XmlElement)matchingTargetNode;
                    matchingElement.AppendChild(clonedNode);
                }
            }
        }
    }
}
