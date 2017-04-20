using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace EquellaMetadataUtility
{
    class Utils
    {
        public Utils()
        {
        }

        public XmlNode GetTextNode(XmlElement xmlElement)
        {
            foreach (XmlNode childNode in xmlElement)
            {
                if (childNode.NodeType == XmlNodeType.Text)
                {
                    return childNode;
                }
            }
            return null;
        }

        public void createXpath(XmlDocument xmlDoc, XmlElement root, string processedXpath)
        {
            string candidateXpath = "";
            //ArrayList newXpathNodes = new ArrayList();
            string slashedXpath = processedXpath;
            if (!slashedXpath.StartsWith("/"))
            {
                slashedXpath = "/" + slashedXpath;
            }
            string[] xpathNodes = slashedXpath.Split(new String[] { "/" }, StringSplitOptions.None);

            // traverse backward along xpath until find an element that exists
            for (int i = xpathNodes.Length; i > 0; i--)
            {
                candidateXpath = "";
                int j;
                for (j = 0; j < i; j++)
                {
                    if (xpathNodes[j] != "") candidateXpath += xpathNodes[j] + "/";
                }
                // trim last slash
                if (candidateXpath.EndsWith("/")) candidateXpath = candidateXpath.Substring(0, candidateXpath.Length - 1);

                // prepend slash if orignal xpath starts with one
                if (processedXpath.StartsWith("/")) candidateXpath = "/" + candidateXpath;

                // query on slash if no mathing nodes found
                if (candidateXpath == "") candidateXpath = ".";

                XmlNodeList candidateNodes = root.SelectNodes(candidateXpath);

                // check if there are existing elements at this point in the xpath
                if (candidateNodes.Count > 0)
                {
                    // create remaining nodes for each matching candidate node
                    foreach (XmlNode candidateNode in candidateNodes)
                    {
                        XmlNode parentNewNode = candidateNode;
                        for (int k = j; k < xpathNodes.Length; k++)
                        {
                            // if attribute add and break
                            if (xpathNodes[k].Substring(0, 1) == "@")
                            {
                                parentNewNode.Attributes.Append(xmlDoc.CreateAttribute(xpathNodes[k].Substring(1)));
                                break;
                            }
                            // add element
                            try
                            {
                                XmlNode newNode = xmlDoc.CreateElement(xpathNodes[k]);
                                parentNewNode.AppendChild(newNode);
                                parentNewNode = newNode;
                            }
                            catch (Exception err)
                            {
                                throw new System.InvalidOperationException("Cannot create element " + processedXpath);
                            }
                        }
                    }
                    break;
                }
            }
        }

        public void stripNode(XmlNode node, bool recurse)
        {
            ArrayList nodesToRemove = new ArrayList();
            bool nodeToBeStripped = false;
            foreach (XmlNode childNode in node.ChildNodes)
            {
                // list empty text nodes (to remove if any should be)
                if ((childNode.NodeType == XmlNodeType.Whitespace) || (childNode.NodeType == XmlNodeType.Text && childNode.Value.Trim() == ""))
                    nodesToRemove.Add(childNode);

                // only remove empty text nodes if not a leaf node (i.e. a child element exists)
                if (!nodeToBeStripped && (childNode.NodeType == XmlNodeType.Element))
                    nodeToBeStripped = true;
            }

            // remove flagged text nodes
            if (nodeToBeStripped)
                foreach (XmlNode childNode in nodesToRemove)
                    node.RemoveChild(childNode);

            // recurse if specified
            if (recurse)
                foreach (XmlNode childNode in node.ChildNodes)
                    if (childNode.NodeType == XmlNodeType.Element)
                        stripNode(childNode, true);
        }

    }
}
