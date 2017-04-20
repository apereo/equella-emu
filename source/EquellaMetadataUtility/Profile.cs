using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;


namespace EquellaMetadataUtility
{
    [XmlRoot("emu_profile")]
    [XmlInclude(typeof(ModifierUpdateText))]
    [XmlInclude(typeof(ModifierAddXML))]
    [XmlInclude(typeof(ModifierRenameNode))]
    [XmlInclude(typeof(ModifierRemoveNode))]
    [XmlInclude(typeof(ModifierCopyXML))]
    [XmlInclude(typeof(ModifierReplaceText))]
    [XmlInclude(typeof(ModifierXSLT))]
    [XmlInclude(typeof(ModifierScript))]
    public class Profile
    {
        // connection settings
        [XmlElement("institution_url")]
        public String institutionUrl;

        [XmlElement("username")]
        public String username;

        [XmlElement("password")]
        public String password;

        [XmlElement("shared_secret_login")]
        public bool sharedSecretLogin;

        [XmlElement("use_proxy")]
        public bool useProxy;

        [XmlElement("proxy_address")]
        public String proxyAddress;

        [XmlElement("proxy_username")]
        public String proxyUsername;

        [XmlElement("proxy_password")]
        public String proxyPassword;

        [XmlElement("ss_username")]
        public String ssUsername;

        [XmlElement("shared_secret_id")]
        public String sharedSecretID;

        [XmlElement("shared_secret")]
        public String sharedSecret;

        [XmlElement("log_file_location")]
        public String logFileLocation;

        // query settings
        [XmlElement("where_statement")]
        public String whereStatement;

        [XmlElement("freetext_query")]
        public String freetextQuery;

        [XmlElement("item_uuid")]
        public String itemUuid;

        [XmlElement("item_version")]
        public String itemVersion;

        [XmlElement("all_collections")]
        public bool allCollections;

        [XmlElement("collections_list")]
        public CollectionsList collectionsList;

        [XmlElement("columns_include_name")]
        public bool columnsIncludeName;

        [XmlElement("columns_include_itemid")]
        public bool columnsIncludeItemID;

        [XmlElement("columns_include_itemversion")]
        public bool columnsIncludeItemVersion;

        [XmlElement("columns_list")]
        public ColumnsList columnsList;

        [XmlElement("selected_columns_list")]
        public SelectedColumnsList selectedColumnsList;

        [XmlElement("delimiter")]
        public String delimiter;

        [XmlElement("sort_order")]
        public String sortOrder;

        [XmlElement("reverse_sort")]
        public bool reverseSort;

        [XmlElement("include_nonlive")]
        public bool includeNonLive;

        [XmlElement("limit")]
        public String limit;

        [XmlElement("csv_filename")]
        public String csvFilename;

        [XmlElement("load_csv_with_profile")]
        public bool loadCsvWithProfile;

        // update settings
        [XmlElement("modifier_list")]
        public ModifierList modifierList;

        [XmlElement("update_all_results")]
        public bool updateAllResults;

        [XmlElement("update_first_few_only")]
        public bool updateFirstFewOnly;

        [XmlElement("update_first_few_number")]
        public int updateFirstFewNumber;

        [XmlElement("uncheck_processed_results")]
        public bool uncheckProcessedResults;

        [XmlElement("logOutputSize")]
        public int logOutputSize;

        [XmlElement("create_log_file")]
        public bool createLogFile;

        [XmlElement("clear_log_on_update")]
        public bool clearLogOnUpdate;

        [XmlElement("copy_item_files_to_staging")]
        public bool copyItemFilesToStaging;

        // container classes to generate container node in xml
        public class ModifierList
        {
            [XmlElement("modifier")]
            public ArrayList list;
        }

        public class CollectionsList
        {
            [XmlElement("collection")]
            public ArrayList list = new ArrayList();
        }

        public class ColumnsList
        {
            [XmlElement("column")]
            public ArrayList list = new ArrayList();
        }

        public class SelectedColumnsList
        {
            [XmlElement("selected")]
            public ArrayList list = new ArrayList();
        }

        public Profile()
        {
            modifierList = new ModifierList();
            collectionsList = new CollectionsList();
            columnsList = new ColumnsList();
            selectedColumnsList = new SelectedColumnsList();
        }
    }
}
