/*
The MIT License (MIT)

Copyright(c) 2022, Jim Sculley

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
using SolidWorks.Interop.sldworks;
using SolidWorksTools;
using SolidWorks.Interop.swcommands;
using SolidWorks.Interop.swconst;
using SolidWorks.Interop.swpublished;
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
        SldWorks SwEventPtr = null;
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
            SwEventPtr = (SldWorks)swApp;
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
                List<ConfigInfo> configInfoList = getConfigInfo();
                MaterialSearchDialog d = new MaterialSearchDialog(materialDatabaseDescriptors, configInfoList);
                DialogResult dr = d.ShowDialog();
                switch (dr)
                {
                    case DialogResult.Ignore: //the user clicked 'Open Library'
                        return 0;
                    case DialogResult.Cancel: //the user clicked 'Cancel'
                        return 1;
                    case DialogResult.OK: //the user clicked 'Apply'
                        applyMaterial(d.SelectedMaterial, configInfoList);
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


        private List<ConfigInfo> getConfigInfo()
        {
            List<ConfigInfo> configInfoList = new List<ConfigInfo>();
            List<ModelDoc2> componentDocs = getTargetModelDocuments();
            foreach (ModelDoc2 mDoc in componentDocs)
            {
                List<string> configNames = buildConfigNames(mDoc);
                ConfigInfo configInfo = new ConfigInfo(mDoc, Target.CURRENT, configNames);
                configInfoList.Add(configInfo);
            }
            return configInfoList;
        }
        /// <summary>
        /// Create a collection of config names with spaces padding the front
        /// of derived configurations to give an indented tree look to the name list
        /// </summary>
        /// <returns>an indented list of configuration names</returns>
        private List<string> buildConfigNames(ModelDoc2 mDoc)
        {
            //ModelDoc2 mDoc = swApp.ActiveDoc as ModelDoc2;
            object[] configNamesObjArray = mDoc.GetConfigurationNames();
            List<string> configNames = new List<string>();
            foreach (object obj in configNamesObjArray)
            {
                string nextName = obj as string;
                Configuration nextConfig = mDoc.GetConfigurationByName(nextName);
                //Recursively pad the front of any derived config names
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
        /// Apply a material in one or more configurations.  If the current document is an assembly, 
        /// apply the material to the selected part(s) or selected bodies of the selected part(s)
        /// </summary>
        /// <param name="material">the material to be applied</param>
        /// <param name="configInfo">the configruation where the material should be applied</param>
        /// <returns><b>true</b> if the material was applied successfully, <b>false</b> otherwise</returns>
        private void applyMaterial(MaterialSearchResult material, List<ConfigInfo> configInfoList)
        {
            List<ModelDoc2> targetModelDocs = getTargetModelDocuments();
            ModelDoc2 mDoc = swApp.ActiveDoc as ModelDoc2;
            SelectionMgr selMgr = mDoc.SelectionManager;
            foreach (ModelDoc2 nextModelDoc in targetModelDocs)
            {
                string selTypeString = Enum.GetName(typeof(swSelectType_e), selMgr.GetSelectedObjectType3(1, -1));
                if (selMgr.GetSelectedObjectCount2(-1) == 0)
                {
                    //No bodies or components are selected, so this must be a part document.  Apply material to entire part.
                    applyMaterialToPart(nextModelDoc, material, configInfoList[0]);
                    return;
                }
                //Apply material to selected bodies
                for (int i = 1; i <= selMgr.GetSelectedObjectCount2(-1); i++)
                {
                    int selType = selMgr.GetSelectedObjectType3(i, -1);
                    Body2 selBody = null;
                    switch (selType)
                    {
                        case (int)swSelectType_e.swSelSOLIDBODIES:
                        case (int)swSelectType_e.swSelBODYFEATURES:
                            selBody = selMgr.GetSelectedObject6(i, -1) as Body2;
                            if (!applyMaterialToBody(selBody, material, configInfoList[0]))
                            {
                                swApp.SendMsgToUser2(
                                    "Failed to apply material to body" + selBody.Name,
                                    (int)swMessageBoxIcon_e.swMbStop,
                                    (int)swMessageBoxIcon_e.swMbStop);
                            }
                            break;
                        case (int)swSelectType_e.swSelCOMPONENTS:
                            Component2 comp = selMgr.GetSelectedObject6(i, -1) as Component2;
                            ModelDoc2 compDoc = comp.GetModelDoc2() as ModelDoc2;
                            if (compDoc == nextModelDoc)
                            {
                                applyMaterialToPart(compDoc, material, configInfoList[0]);
                            }
                            //selBody = selMgr.GetSelectedObject6(i, -1) as Body2;
                            //mDoc.Extension.SelectByID2(
                            //    selBody.Name, 
                            //    "SOLIDBODY", 
                            //    0, 0, 0, //X,Y,Z
                            //    true, //append
                            //    -1, //mark
                            //    null, //callout
                            //    (int)swSelectOption_e.swSelectOptionDefault);
                            //if (!applyMaterialToBody(selBody, material, configInfo))
                            //{
                            //    swApp.SendMsgToUser2(
                            //        "Failed to apply material to component" + comp.Name,
                            //        (int)swMessageBoxIcon_e.swMbStop,
                            //        (int)swMessageBoxIcon_e.swMbStop);
                            //}
                            break;
                        case (int)swSelectType_e.swSelBROWSERITEM:
                            comp = selMgr.GetSelectedObjectsComponent3(i, -1) as Component2;
                            if (comp == null) //this is a part
                            {
                                applyMaterialToPart(mDoc, material, configInfoList[0]);
                            }
                            else
                            {
                                compDoc = comp.GetModelDoc2() as ModelDoc2;
                                if (compDoc == nextModelDoc)
                                {
                                    applyMaterialToPart(compDoc, material, configInfoList[0]);
                                }
                            }
                            break;
                    }
                }
            }
            if (mDoc.GetType() == (int)swDocumentTypes_e.swDocASSEMBLY)
            {
                mDoc.ForceRebuild3(false);
            }
        }

        private bool applyMaterialToPart(ModelDoc2 partModelDoc, MaterialSearchResult material, ConfigInfo configInfo)
        {
            PartDoc partDoc = partModelDoc as PartDoc;
            switch (configInfo.AppliesTo)
            {
                case Target.CURRENT:
                    Configuration currentConfig = partModelDoc.GetActiveConfiguration();
                    partDoc.SetMaterialPropertyName2(currentConfig.Name, material.Library, material.MaterialName);
                    break;
                case Target.ALL:
                    object[] configNamesObjArray = partModelDoc.GetConfigurationNames();
                    foreach (object nextNameObj in configNamesObjArray)
                    {
                        string nextConfig = nextNameObj as string;
                        partDoc.SetMaterialPropertyName2(nextConfig.TrimStart(), material.Library, material.MaterialName);
                    }
                    break;
                case Target.SELECTED:
                    foreach (string nextConfig in configInfo.SelectedConfigs)
                    {
                        partDoc.SetMaterialPropertyName2(nextConfig.TrimStart(), material.Library, material.MaterialName);
                    }
                    break;
            }
            return true;
        }

        private bool applyMaterialToBody(IBody2 body, MaterialSearchResult material, ConfigInfo configInfo)
        {
            //int result = 0;
            //switch (configInfo.AppliesTo)
            //{
            //    case Target.CURRENT:
            //        Configuration currentConfig = partModelDoc.GetActiveConfiguration();

            //        result = body.SetMaterialProperty(currentConfig.Name, material.Library, material.MaterialName);



            //        string dbName = ""; //DEBUG
            //        string name = body.GetMaterialPropertyName("", out dbName); //DEBUG
            //        break;
            //    case Target.ALL:
            //        object[] configNamesObjArray = partModelDoc.GetConfigurationNames();
            //        foreach (object nextNameObj in configNamesObjArray)
            //        {
            //            string nextConfig = nextNameObj as string;
            //            result = body.SetMaterialProperty(nextConfig.TrimStart(), material.Library, material.MaterialName);
            //        }
            //        break;
            //    case Target.SELECTED:
            //        foreach (string nextConfig in configInfo.SelectedConfigs)
            //        {
            //            result = body.SetMaterialProperty(nextConfig.TrimStart(), material.Library, material.MaterialName);
            //        }
            //        break;
            //}
            //return false;
            return true;
        }
        /// <summary>
        /// Materials can be applied to bodies or entire parts.  The material can be changed from within the part
        /// document, an assembly containing the part at some level or a drawing with a view containing the part.
        /// This method will determine the appropriate <see cref="ModelDoc2"/> object(s) where the material should 
        /// be applied
        /// </summary>
        /// <returns>a list of <see cref="ModelDoc2"/> objects</returns>
        private List<ModelDoc2> getTargetModelDocuments()
        {
            List<ModelDoc2> targetModelDocs = new List<ModelDoc2>();
            ModelDoc2 mDoc = swApp.ActiveDoc as ModelDoc2;
            SelectionMgr selMgr = mDoc.SelectionManager;
            swDocumentTypes_e docType = (swDocumentTypes_e)Enum.ToObject(typeof(swDocumentTypes_e), mDoc.GetType());
            switch (docType)
            {
                case swDocumentTypes_e.swDocPART:
                    targetModelDocs.Add(mDoc);
                    break;
                case swDocumentTypes_e.swDocASSEMBLY:
                    int selCount = selMgr.GetSelectedObjectCount2(-1);
                    for (int i = 1; i <= selCount; i++)
                    {
                        Component2 selComp = selMgr.GetSelectedObjectsComponent4(i, -1) as Component2;
                        ModelDoc2 selCompModelDoc = selComp.GetModelDoc2();
                        if (selComp != null && !targetModelDocs.Contains(selCompModelDoc))
                        {
                            targetModelDocs.Add(selCompModelDoc);
                        }
                    }
                    break;
                case swDocumentTypes_e.swDocDRAWING:
                    break;
                default:
                    break;
            }
            return targetModelDocs;
        }
        #endregion
    }

}
