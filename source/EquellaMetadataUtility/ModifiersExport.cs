using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace EquellaMetadataUtility
{
    [XmlRoot("emu_modifiers")]
    [XmlInclude(typeof(ModifierUpdateText))]
    [XmlInclude(typeof(ModifierAddXML))]
    [XmlInclude(typeof(ModifierRenameNode))]
    [XmlInclude(typeof(ModifierRemoveNode))]
    [XmlInclude(typeof(ModifierCopyXML))]
    [XmlInclude(typeof(ModifierReplaceText))]
    [XmlInclude(typeof(ModifierXSLT))]
    [XmlInclude(typeof(ModifierScript))]
    public class ModifiersExport
    {
        // modifiers
        [XmlElement("modifier_list")]
        public ModifierList modifierList;

        // container classes to generate container node in xml
        public class ModifierList
        {
            [XmlElement("modifier")]
            public ArrayList list;
        }


        public ModifiersExport()
        {
            modifierList = new ModifierList();
        }
    }
}
