using SldWorks;
using SolidWorksTools;
using SwCommands;
using SwConst;
using SWPublished;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Linq;
namespace org.duckdns.buttercup.MaterialSearch
{
    /// <summary>
    /// SOLIDWORKS add-in to expand the material selection dialog to allow searching by description as well as name
    /// </summary>
    [Guid("7d9a0851-0c53-4a7b-91ab-6ff1992391b4"), ComVisible(true)]
    [SwAddin(
        Description = "Search materials by name or description",
        Title = "Material Search",
        LoadAtStartup = true
        )]
    public class MaterialSearchAddin : ISwAddin
    {
        #region Private Variables
        private ISldWorks swApp = null;
        private int addinID = 0;
        private List<MaterialDatabaseDescriptor> materialDatabaseDescriptors;
        #endregion

        #region Event Handler Variables
        SldWorks.SldWorks SwEventPtr = null;
        #endregion

        #region Boilerplate SolidWorks Registration Code
        [ComRegisterFunctionAttribute]
        public static void RegisterFunction(Type t)
        {
            #region Get Custom Attribute: SwAddinAttribute
            SwAddinAttribute SWattr = null;
            Type type = typeof(MaterialSearchAddin);

            foreach (System.Attribute attr in type.GetCustomAttributes(false))
            {
                if (attr is SwAddinAttribute)
                {
                    SWattr = attr as SwAddinAttribute;
                    break;
                }
            }

            #endregion

            try
            {
                Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;
                Microsoft.Win32.RegistryKey hkcu = Microsoft.Win32.Registry.CurrentUser;

                string keyname = "SOFTWARE\\SolidWorks\\Addins\\{" + t.GUID.ToString() + "}";
                Microsoft.Win32.RegistryKey addinkey = hklm.CreateSubKey(keyname);
                addinkey.SetValue(null, 0);

                addinkey.SetValue("Description", SWattr.Description);
                addinkey.SetValue("Title", SWattr.Title);

                keyname = "Software\\SolidWorks\\AddInsStartup\\{" + t.GUID.ToString() + "}";
                addinkey = hkcu.CreateSubKey(keyname);
                addinkey.SetValue(null, Convert.ToInt32(SWattr.LoadAtStartup), Microsoft.Win32.RegistryValueKind.DWord);
            }
            catch (System.NullReferenceException nl)
            {
                Console.WriteLine("There was a problem registering this dll: SWattr is null. \n\"" + nl.Message + "\"");
                System.Windows.Forms.MessageBox.Show("There was a problem registering this dll: SWattr is null.\n\"" + nl.Message + "\"");
            }

            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);

                System.Windows.Forms.MessageBox.Show("There was a problem registering the function: \n\"" + e.Message + "\"");
            }
        }

        [ComUnregisterFunctionAttribute]
        public static void UnregisterFunction(Type t)
        {
            try
            {
                Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;
                Microsoft.Win32.RegistryKey hkcu = Microsoft.Win32.Registry.CurrentUser;

                string keyname = "SOFTWARE\\SolidWorks\\Addins\\{" + t.GUID.ToString() + "}";
                hklm.DeleteSubKey(keyname);

                keyname = "Software\\SolidWorks\\AddInsStartup\\{" + t.GUID.ToString() + "}";
                hkcu.DeleteSubKey(keyname);
            }
            catch (System.NullReferenceException nl)
            {
                Console.WriteLine("There was a problem unregistering this dll: " + nl.Message);
                System.Windows.Forms.MessageBox.Show("There was a problem unregistering this dll: \n\"" + nl.Message + "\"");
            }
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show("There was a problem unregistering this dll: \n\"" + e.Message + "\"");
            }
        }

        #endregion

        #region Boilerplate ISwAddin Implementation
        public MaterialSearchAddin()
        {
        }

        public bool ConnectToSW(object ThisSW, int cookie)
        {
            swApp = (ISldWorks)ThisSW;
            addinID = cookie;

            //Setup callbacks
            swApp.SetAddinCallbackInfo(0, this, addinID);

           
            #region Setup the Event Handlers
            SwEventPtr = (SldWorks.SldWorks)swApp;
            AttachEventHandlers();
            #endregion

            return true;
        }

        public bool DisconnectFromSW()
        {
            DetachEventHandlers();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(swApp);
            swApp = null;
            //The addin _must_ call GC.Collect() here in order to retrieve all managed code pointers 
            GC.Collect();
            GC.WaitForPendingFinalizers();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return true;
        }
        #endregion
       
        #region Bolierplate Event Handler Setup
        public bool AttachEventHandlers()
        {
            try
            {
                SwEventPtr.CommandOpenPreNotify += new DSldWorksEvents_CommandOpenPreNotifyEventHandler(CommandOpenPreNotify);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool DetachEventHandlers()
        {
            try
            {
                SwEventPtr.CommandOpenPreNotify -= new DSldWorksEvents_CommandOpenPreNotifyEventHandler(CommandOpenPreNotify);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Called when the CommandOpenPreNotify event is fired
        /// </summary>
        /// <param name="command">the command code for the event</param>
        /// <param name="userCommand">third party command ID</param>
        /// <returns><b>0</b> if the user clicks the Open Library button, <b>1</b> otherwise</returns>
        int CommandOpenPreNotify(int command, int userCommand)
        {
            if (command == (int)swCommands_e.swCommands_EditMaterial)
            {
                initMaterialDatabases();
                List<string> configNames = buildConfigNames();
                ConfigInfo configInfo = new ConfigInfo(Target.CURRENT, configNames);
                MaterialSearchDialog d = new MaterialSearchDialog(materialDatabaseDescriptors, configInfo);
                DialogResult dr = d.ShowDialog();
                switch (dr)
                {
                    case DialogResult.Ignore: //the user clicked 'Open Library'
                        return 0;
                    case DialogResult.Cancel: //the user clicked 'Cancel'
                        return 1;
                    case DialogResult.OK: //the user clicked 'Apply'
                        applyMaterial(d.SelectedMaterial, configInfo);
                        return 1;
                }
            }
            return 0;
        }

        #endregion

        #region Private Implementation
        /// <summary>
        /// Initialize the material database descriptors for each material database that SW knows about
        /// </summary>
        private void initMaterialDatabases()
        {
            string[] matlDatabaseNameArray = swApp.GetMaterialDatabases() as string[];
            string schemaPath = swApp.GetMaterialSchemaPathName();
            materialDatabaseDescriptors = new List<MaterialDatabaseDescriptor>(swApp.GetMaterialDatabaseCount());
            foreach (string nextName in matlDatabaseNameArray)
            {
                string databaseFilename = Directory.GetFiles(Path.GetDirectoryName(nextName), Path.GetFileName(nextName)).FirstOrDefault();
                string databaseName = Path.GetFileNameWithoutExtension(databaseFilename);
                materialDatabaseDescriptors.Add(new MaterialDatabaseDescriptor(databaseName, XElement.Load(nextName)));
            }
        }

        /// <summary>
        /// Create a collection of config names with spaces padding the front
        /// of derived configurations to give an indented tree look to the name list
        /// </summary>
        /// <returns>an indented list of configuration names</returns>
        private List<string> buildConfigNames()
        {
            ModelDoc2 mDoc = swApp.ActiveDoc as ModelDoc2;
            object[] configNamesObjArray = mDoc.GetConfigurationNames();
            List<string> configNames = new List<string>();
            foreach (object obj in configNamesObjArray)
            {
                string nextName = obj as string;
                Configuration nextConfig = mDoc.GetConfigurationByName(nextName);
                //Recursively pad the front of and deerived config names
                while (nextConfig != null && nextConfig.IsDerived())
                {
                    nextName = "    " + nextName;
                    nextConfig = nextConfig.GetParent();
                }
                configNames.Add(nextName as string);
            }
            return configNames;
        }
        /// <summary>
        /// Apply a material to the entire model or to selected bodies in one or more configurations
        /// </summary>
        /// <param name="material">the material to be applied</param>
        /// <param name="configInfo">the configruation where the material should be applied</param>
        private void applyMaterial(MaterialSearchResult material, ConfigInfo configInfo)
        {
            ModelDoc2 mDoc = swApp.ActiveDoc as ModelDoc2;
            PartDoc pDoc = swApp.ActiveDoc as PartDoc;
            SelectionMgr selMgr = mDoc.SelectionManager;
            if (selMgr.GetSelectedObjectCount2(1) == 0)
            {
                //Apply material to model
                switch (configInfo.AppliesTo)
                {
                    case Target.CURRENT:
                        Configuration currentConfig = mDoc.GetActiveConfiguration();
                        pDoc.SetMaterialPropertyName2(currentConfig.Name, material.Library, material.MaterialName);
                        break;
                    case Target.ALL:
                        object[] configNamesObjArray = mDoc.GetConfigurationNames();
                        foreach (object nextNameObj in configNamesObjArray)
                        {
                            string nextConfig = nextNameObj as string;
                            pDoc.SetMaterialPropertyName2(nextConfig.TrimStart(), material.Library, material.MaterialName);
                        }
                        break;
                    case Target.SELECTED:
                        foreach (string nextConfig in configInfo.SelectedConfigs)
                        {
                            pDoc.SetMaterialPropertyName2(nextConfig.TrimStart(), material.Library, material.MaterialName);
                        }
                        break;
                }
            }
            else
            {
                //Apply material to selected bodies
                for (int i = 0; i < selMgr.GetSelectedObjectCount2(1); i++)
                {
                    if (selMgr.GetSelectedObjectType3(i, 0) != (int)swSelectType_e.swSelBODYFEATURES)
                    {
                        continue;
                    }
                    Body2 selBody = selMgr.GetSelectedObject6(i, 0) as Body2;

                    switch (configInfo.AppliesTo)
                    {

                        case Target.CURRENT:
                            Configuration currentConfig = mDoc.GetActiveConfiguration();
                            selBody.SetMaterialProperty(currentConfig.Name, material.Library, material.MaterialName);
                            break;
                        case Target.ALL:
                            object[] configNamesObjArray = mDoc.GetConfigurationNames();
                            foreach (object nextNameObj in configNamesObjArray)
                            {
                                string nextConfig = nextNameObj as string;
                                selBody.SetMaterialProperty(nextConfig.TrimStart(), material.Library, material.MaterialName);
                            }
                            break;
                        case Target.SELECTED:
                            foreach (string nextConfig in configInfo.SelectedConfigs)
                            {
                                selBody.SetMaterialProperty(nextConfig.TrimStart(), material.Library, material.MaterialName);
                            }
                            break;
                    }
                }
            }
        }
        #endregion
    }

}
