using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Xml.Serialization;


namespace EquellaMetadataUtility
{
    public class ModifierScript
    {
        public const string ModifierType = "Script";
        public bool Enabled;
        public string ScriptText;
        public int modifierPosition = 0;
        public bool runOnlyOnce = false;
        public bool valid = true;

        private XmlElement root = null;
        private ScriptEngine scriptEngine;
        private bool compiled = false;

        public ModifierScript()
        {
            ScriptText = "";
            Enabled = true;
        }

        public ModifierScript(string initialXSLTtext)
        {
            ScriptText = initialXSLTtext;
            Enabled = true;
        }

        public void setScriptEngine(ScriptEngine scriptEngineParam)
        {
            scriptEngine = scriptEngineParam;
        }

        public void createVsaEngine()
        {
            scriptEngine.createVsaEngine();
        }

        public void setRoot(XmlElement rootParam)
        {
            root = rootParam;
        }

        public void addObject(string objectName, Object scriptObject)
        {
            scriptEngine.addObject(objectName, scriptObject);
        }

        public void setCompiledState(bool compiledParam)
        {
            compiled = compiledParam;
        }

        public bool getCompiledState()
        {
            return compiled;
        }

        public void validate()
        {
            // assign script to script engine
            scriptEngine.setScript(ScriptText);
            scriptEngine.addObject("xml", new XMLWrapper());
            // compile script
            if (!scriptEngine.compile())
            {
                string error = scriptEngine.formattedLastError();
                throw new System.InvalidOperationException(scriptEngine.formattedLastError());
            }
        }

        // run script
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

        // run script
        public void transform(XmlDocument xmlDoc)
        {
            // assign script to script engine
            scriptEngine.setScript(ScriptText);

            // compile and run script
            if (!compiled)
            {
                if (!scriptEngine.compile())
                {
                    valid = false;
                    string error = scriptEngine.formattedLastError();
                    throw new System.InvalidOperationException(scriptEngine.formattedLastError());
                }
                compiled = true;
            }
            try
            {
                if (!scriptEngine.run())
                {
                    throw new System.InvalidOperationException("Script modifier " + modifierPosition + ", " + scriptEngine.formattedLastError());
                }
            }
            catch (Exception err)
            {
                scriptEngine.vsaEngine.Reset();
                throw err;
            }
        }
    }
}
