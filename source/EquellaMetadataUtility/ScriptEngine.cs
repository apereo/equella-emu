using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Vsa;
using Microsoft.JScript;
using Microsoft.JScript.Vsa;

namespace EquellaMetadataUtility
{
    public class ScriptEngine : IVsaSite, IDisposable
    {
        public VsaEngine vsaEngine;
        public bool VSAEngineCreated = false;
        public Hashtable globalItemLookupTable = new Hashtable();
        //public Hashtable scriptVariables = new Hashtable();
        public EvalResult evalResult = new EvalResult();
        public string lastCompileError;
        public string lastCompileErrorLineText;
        public int lastCompileErrorLineNumber;
        public int lastCompileErrorColNumber;
        protected bool _disposed;
        const string evalScriptPrefix = "scrEvalResult.result = ";
        

        // inititalize engine
        public void createVsaEngine()
        {
            vsaEngine = new VsaEngine();
            vsaEngine.RootMoniker = "ScriptGlobals://JScript";
            vsaEngine.Site = this;
            vsaEngine.InitNew();
            vsaEngine.RootNamespace = "emu";

            // add system references
            IVsaReferenceItem refItem = (IVsaReferenceItem)vsaEngine.Items.CreateItem("mscorlib", VsaItemType.Reference, VsaItemFlag.None);
            refItem.AssemblyName = "mscorlib.dll";
            refItem = (IVsaReferenceItem)vsaEngine.Items.CreateItem("system", VsaItemType.Reference, VsaItemFlag.None);
            refItem.AssemblyName = "System.dll";


            //// add propertybag for script variables
            //IVsaGlobalItem item = (IVsaGlobalItem)vsaEngine.Items.CreateItem("oldvars", VsaItemType.AppGlobal, VsaItemFlag.None);
            //item.TypeString = "System.Collections.HashTable";
            //globalItemLookupTable["oldvars"] = scriptVariables;

            // add object for eval results
            IVsaGlobalItem item = (IVsaGlobalItem)vsaEngine.Items.CreateItem("scrEvalResult", VsaItemType.AppGlobal, VsaItemFlag.None);
            item.TypeString = evalResult.GetType().FullName;
            globalItemLookupTable["scrEvalResult"] = evalResult;

            vsaEngine.GenerateDebugInfo = false;

            VSAEngineCreated = true;
        }

        public void closeVsaEngine()
        {
            vsaEngine.Close();
        }

        // remove an object from the engine if it exists
        public void removeObject(string name)
        {
            foreach (VsaItem currentItem in vsaEngine.Items)
            {
                if (currentItem.Name == name)
                {
                    vsaEngine.Items.Remove(name);
                    break;
                }
            }
        }

        // add a global object to the engine
        public void addObject(string name, object Object)
        {
            globalItemLookupTable[name] = Object;
            removeObject(name);
            IVsaGlobalItem item = (IVsaGlobalItem)vsaEngine.Items.CreateItem(name, VsaItemType.AppGlobal, VsaItemFlag.None);
            item.TypeString = Object.GetType().FullName;
        }

        // specify script
        public void setScript(string script)
        {
            //vsaEngine.s
            foreach (VsaItem currentItem in vsaEngine.Items)
            {
                if (currentItem.Name == "code")
                {
                    vsaEngine.Items.Remove("code");
                    break;
                }
            }

            IVsaCodeItem codeItem = (IVsaCodeItem)vsaEngine.Items.CreateItem("code", VsaItemType.Code, VsaItemFlag.None);
            codeItem.SourceText = script;
        }

        // compile the script
        public bool compile()
        {
            lastCompileError = "";
            if (vsaEngine.Compile())
            {
                return true;
            }
            return false;
        }

        // execute the script
        public bool run()
        {
            lastCompileError = "";
            //if (vsaEngine.Compile())
            if (true)
            {
                vsaEngine.Run();
                vsaEngine.Reset();
                return true;
            }
            return false;
        }

        // evaluate a statement
        public object eval(string script, out int error, bool compileOnly)
        {
            // form scrEvalResult.result evaluation script
            setScript(evalScriptPrefix + script + ";");

            // run script and return scrEvalResult.result
            lastCompileError = "";
            if (!compileOnly)
            {
                if (run())
                {
                    error = 0;
                    return evalResult.result;
                }
            }
            else
            {
                if (compile())
                {
                    error = 0;
                    return script;
                }
            }
            error = 1;
            return null;

        }

        //// process a scripted template(tags are of the form <% statement() %>)
        //public string evalTemplate(string template, out int error)
        //{
        //    bool tagsPresent;

        //    return evalTemplate(template, out error, false, out tagsPresent);
        //}


        //public string evalTemplate(string template, out int error, bool compileOnly, out bool tagsPresent)
        //{
        //    error = 0;
        //    string evalScript;
        //    object evalResult;
        //    string processedTemplate = template;
        //    int startpos = 0;
        //    int endpos = 0;
        //    string tag = "";
        //    ArrayList tags = new ArrayList();
        //    int tagNumber = -1;

        //    tagsPresent = false;

        //    // parse template and replace tags with evals (first pass to get tag positions for errors, second pass to process tags)
        //    for (int pass = 0; pass < 2; pass++)
        //    {
        //        for (startpos = 0; startpos < processedTemplate.Length - 1; startpos++)
        //        {
        //            // test for opening tag
        //            if ((processedTemplate[startpos] == '<') && (processedTemplate[startpos + 1] == '%'))
        //            {
        //                for (endpos = startpos; endpos < processedTemplate.Length - 1; endpos++)
        //                {
        //                    // test for closing tag
        //                    if ((processedTemplate[endpos] == '%') && (processedTemplate[endpos + 1] == '>'))
        //                    {
        //                        if (pass == 0)
        //                        {
        //                            tags.Add(startpos);
        //                        }
        //                        else
        //                        {
        //                            tagNumber++;

        //                            // extract eval script from tag
        //                            evalScript = processedTemplate.Substring(startpos + 2, endpos - startpos - 2);

        //                            tag = processedTemplate.Substring(startpos, endpos - startpos + 2);

        //                            // evaluate evalScript
        //                            evalResult = eval(evalScript, out error, compileOnly);

        //                            if (error == 0)
        //                            {

        //                                // replace tag with evaluation
        //                                processedTemplate = processedTemplate.Substring(0, startpos) + evalResult + processedTemplate.Substring(endpos + 2);
        //                            }

        //                        }
        //                        break;
        //                    }
        //                    if (error != 0) break;
        //                }
        //            }
        //            if (error != 0) break;
        //        }
        //    }
        //    if (error == 0)
        //    {
        //        // return successfully processed template
        //        if (tags.Count > 0)
        //        {
        //            tagsPresent = true;
        //        }
        //        return processedTemplate;
        //    }
        //    else
        //    {
        //        // calculate error details

        //        lastCompileErrorLineText = tag;
        //        string textBefore = template.Substring(0, (int)tags[tagNumber] - 1);
        //        string[] linesBefore = textBefore.Split('\n');

        //        lastCompileErrorLineNumber = linesBefore.Length;

        //        lastCompileErrorColNumber = (int)tags[tagNumber] - textBefore.LastIndexOf('\n');

        //        if (tags.Count > 0)
        //        {
        //            tagsPresent = true;
        //        }
        //        return "";
        //    }
        //}

        // return a formatted error inclduing line number, column number, source code and error description
        public string formattedLastError()
        {
            string formatedError = "Line " + lastCompileErrorLineNumber + ", Column " + lastCompileErrorColNumber;
            formatedError = formatedError + "\r\n    " + lastCompileErrorLineText;
            formatedError = formatedError + "\r\n" + lastCompileError;

            return formatedError;
        }

        #region IVsaSite Members
        public object GetGlobalInstance(string name)
        {
            return globalItemLookupTable[name];
        }
        public void GetCompiledState(out byte[] pe, out byte[] dbgInfo)
        {
            pe = null; dbgInfo = null;
        }
        public object GetEventSourceInstance(string itemName, string sourceName)
        {
            return null;
        }
        public void Notify(string notify, object info) { }
        public bool OnCompilerError(IVsaError error)
        {
            lastCompileError = error.Description;
            lastCompileErrorLineText = error.LineText;
            lastCompileErrorLineNumber = error.Line;
            lastCompileErrorColNumber = error.StartColumn;

            lastCompileErrorLineText = lastCompileErrorLineText.Split('\n')[lastCompileErrorLineNumber - 1];

            return false;
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (vsaEngine != null)
                    vsaEngine.Close();

                _disposed = true;
            }
        }

        #endregion
    }
    
    public class EvalResult
    {
        public object result;
    }

    public class Logger
    {
        public MainForm loggingForm;
        public MainForm.logOutputTarget loggingFormLogTarget;
        public void log(string eventText)
        {
            loggingForm.logEvent(eventText, false, "green", loggingFormLogTarget);
        }
        public void log(string eventText, string eventColor)
        {
            loggingForm.logEvent(eventText, false, eventColor, loggingFormLogTarget);
        }
        public void log(string eventText, string eventColor, MainForm.logOutputTarget logTarget)
        {
            // log event whilst ensuring not attempting to log to file when loggingForm is not
            if ((logTarget == MainForm.logOutputTarget.displayAndFile) && (loggingFormLogTarget == MainForm.logOutputTarget.displayOnly))
            {
                // cannot log to file so degrade to display only
                logTarget = MainForm.logOutputTarget.displayOnly;
            }
            if (!(logTarget == MainForm.logOutputTarget.fileOnly) || !(loggingFormLogTarget == MainForm.logOutputTarget.displayOnly))
            {
                loggingForm.logEvent(eventText, false, eventColor, logTarget);
            }
        }
    }

    // class for script engine to control modifier flow
    public class ModifiersControl
    {
        public bool skipNext = false;
        public bool skipRemaining = false;
        public bool cancelSave = false;
    }

    public class XMLWrapper
    {
        public XmlDocument dom;
        public XmlElement root;

        private ModifierUpdateText modifierUpdateText = new ModifierUpdateText();
        private ModifierAddXML modifierAddXML = new ModifierAddXML();
        private ModifierRenameNode modifierRenameNode = new ModifierRenameNode();
        private ModifierRemoveNode modifierRemoveNode = new ModifierRemoveNode();
        private ModifierCopyXML modifierCopyXML = new ModifierCopyXML();
        private ModifierReplaceText modifierReplaceText = new ModifierReplaceText();
        private ModifierXSLT modifierXSLT = new ModifierXSLT();

        // Constructors
        public XMLWrapper()
        {
            dom = new XmlDocument();
            root = (XmlElement)dom.SelectSingleNode("/*");
        }

        public XMLWrapper(string xmlString)
        {
            dom = new XmlDocument();
            dom.LoadXml(xmlString);
            root = (XmlElement)dom.SelectSingleNode("/*");
        }

        public XMLWrapper(XmlElement rootElement)
        {
            dom = rootElement.OwnerDocument;
            root = rootElement;
        }

        // return an ArrayList of text values from nodes matching the xpath
        public ArrayList getNodes(string xpath)
        {
            ArrayList results = new ArrayList();
            XmlNode textNode;

            XmlNodeList matchingNodes = (XmlNodeList)root.SelectNodes(xpath);
            foreach (XmlNode matchingNode in matchingNodes)
            {
                if (matchingNode.NodeType == XmlNodeType.Element)
                {
                    textNode = GetTextNode((XmlElement)matchingNode);
                    if (textNode != null)
                    {
                        results.Add(textNode.Value);
                    }
                }
                if (matchingNode.NodeType == XmlNodeType.Attribute)
                {
                    results.Add(((XmlAttribute)matchingNode).Value);
                }
            }
            return results;
        }

        public ArrayList getSubtrees(string xpath)
        {
            ArrayList results = new ArrayList();

            XmlNodeList matchingNodes = (XmlNodeList)root.SelectNodes(xpath);
            foreach (XmlNode matchingNode in matchingNodes)
            {
                if (matchingNode.NodeType == XmlNodeType.Element)
                {
                    results.Add(new XMLWrapper((XmlElement)matchingNode));
                }
                if (matchingNode.NodeType == XmlNodeType.Attribute)
                {
                    results.Add(((XmlAttribute)matchingNode).Value);
                }
            }
            return results;
        }

        // return an XMLWrapper object from the matching xpath (returns null if not found)
        public XMLWrapper getSubtree(string xpath)
        {
            ArrayList results = getSubtrees(xpath);
            if (results.Count > 0)
            {
                return (XMLWrapper)(results[0]);
            }
            else
                return null;
        }

        // return the text value of the element at the matching xpath (returns empty string if not found)
        public string getNode(string xpath)
        {
            ArrayList results = getNodes(xpath);
            if (results.Count > 0)
            {
                return (string)results[0];
            }
            else
                return "";
        }

        // wrapper function for ModifierUpdateText
        public void updateText(string xpath, string updateText, bool createNode)
        {
            modifierUpdateText.xpath = xpath;
            modifierUpdateText.updateText = updateText;
            modifierUpdateText.createNode = createNode;
            modifierUpdateText.setRoot(root);

            modifierUpdateText.transform(dom);
        }

        // wrapper function for ModifierAddXml
        public void addXml(string xpath, string addXML, bool createNode)
        {
            modifierAddXML.xpath = xpath;
            modifierAddXML.addXML = addXML;
            modifierAddXML.createNode = createNode;
            modifierAddXML.setRoot(root);

            modifierAddXML.transform(dom);
        }

        // wrapper function for ModifierRenameNode
        public void renameNode(string currentXpath, string renamedNode)
        {
            modifierRenameNode.currentXpath = currentXpath;
            modifierRenameNode.renamedNode = renamedNode;
            modifierRenameNode.setRoot(root);

            modifierRenameNode.transform(dom);
        }

        // wrapper function for ModifierRemoveNode
        public void removeXml(string xpath)
        {
            modifierRemoveNode.xpath = xpath;
            modifierRemoveNode.setRoot(root);

            modifierRemoveNode.transform(dom);
        }

        // wrapper function for ModifierCopyXML
        public void copyXml(string sourceNode, string targetXpath, bool createNode)
        {
            modifierCopyXML.sourceNode = sourceNode;
            modifierCopyXML.targetXpath = targetXpath;
            modifierCopyXML.createNode = createNode;
            modifierCopyXML.setRoot(root);

            modifierCopyXML.transform(dom);
        }

        // wrapper function for ModifierReplaceText
        public void replaceText(string xpath, string findText, string replaceWithText)
        {
            modifierReplaceText.xpath = xpath;
            modifierReplaceText.findText = findText;
            modifierReplaceText.replaceWithText = replaceWithText;
            modifierReplaceText.setRoot(root);

            modifierReplaceText.transform(dom);
        }

        // wrapper function for ModifierXSLT
        public void runXslt(string xslt)
        {
            modifierXSLT.XSLTtext = xslt;
            modifierXSLT.setRoot(root);

            modifierXSLT.transform(dom);
            root = (XmlElement)dom.SelectSingleNode("/*");
        }

        // return string representation of dom
        public string asString()
        {
            return asString(false);
        }

        // return formatted string representation of dom
        public string asFormattedString()
        {
            return asString(true);
        }

        private string asString(bool formatted)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter xw = new XmlTextWriter(sw);
            if (formatted)
            {
                xw.Formatting = Formatting.Indented;
            }
            root.WriteTo(xw);
            return sw.ToString();
        }

        private XmlNode GetTextNode(XmlElement xmlElement)
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

    }
}
