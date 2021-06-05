using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Autocad Related Imports*/
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Colors;
/*Exel File Related Import*/
using OfficeOpenXml;

namespace Khal_Deafting
{
   public class DrawingEnvironment
    {

        public void createLayers()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                try
                {
                    LinetypeTable acLineTypTable;

                    acLineTypTable = trans.GetObject(db.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;
                    
                    string sLineTypeName;

                    LayerTable mylayers;
                    mylayers = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                    LayerTableRecord mylayer;
                    /*Adding Layer for Design Level*/
                    string layerName = "Design_Bed_Level for Xsection";
                    if (mylayers.Has(layerName) == false)
                    {

                        mylayer = new LayerTableRecord();
                        sLineTypeName= "DASHED";
                        if (acLineTypTable.Has(sLineTypeName) == false)
                        {
                            db.LoadLineTypeFile(sLineTypeName, "acad.lin");

                        }
                        mylayer.Name = layerName;
                        mylayer.LinetypeObjectId = acLineTypTable[ sLineTypeName];
                        mylayer.LineWeight = LineWeight.LineWeight009;
                        mylayers.UpgradeOpen();
                        mylayers.Add(mylayer);
                        trans.AddNewlyCreatedDBObject(mylayer, true);

                    }

                    /*Adding Layers for CL of Xsection */
                    layerName = "CL of Xsection";
                    if (mylayers.Has(layerName) == false)
                    {

                        mylayer = new LayerTableRecord();
                        sLineTypeName = "CENTER";
                        if (acLineTypTable.Has(sLineTypeName) == false)
                        {
                            db.LoadLineTypeFile(sLineTypeName, "acad.lin");

                        }
                        mylayer.Name = layerName;
                        mylayer.LinetypeObjectId = acLineTypTable[sLineTypeName];
                        mylayer.LineWeight = LineWeight.LineWeight009;
                        mylayers.UpgradeOpen();
                        mylayers.Add(mylayer);
                        trans.AddNewlyCreatedDBObject(mylayer, true);

                    }
                    /*Adding Layer for Existing Bed Profile */
                    layerName = "Existing_Bed_Level for Xsection";
                    if (mylayers.Has(layerName) == false)
                    {

                        mylayer = new LayerTableRecord();
                        sLineTypeName = "CENTER";
                        if (acLineTypTable.Has(sLineTypeName) == false)
                        {
                            db.LoadLineTypeFile(sLineTypeName, "acad.lin");

                        }
                        mylayer.Name = layerName;
                       // mylayer.LinetypeObjectId = acLineTypTable[sLineTypeName];
                        mylayer.LineWeight = LineWeight.LineWeight009;
                        mylayers.UpgradeOpen();
                        mylayers.Add(mylayer);
                        trans.AddNewlyCreatedDBObject(mylayer, true);

                    }
                    /*Adding Layer for Excavation*/
                    layerName = "Excavation Area";
                    if (mylayers.Has(layerName) == false)
                    {

                        mylayer = new LayerTableRecord();
                        sLineTypeName = "CENTER";
                        if (acLineTypTable.Has(sLineTypeName) == false)
                        {
                            db.LoadLineTypeFile(sLineTypeName, "acad.lin");

                        }
                        mylayer.Name = layerName;
                        // mylayer.LinetypeObjectId = acLineTypTable[sLineTypeName];
                        mylayer.LineWeight = LineWeight.LineWeight009;
                        mylayers.UpgradeOpen();
                        mylayers.Add(mylayer);
                        trans.AddNewlyCreatedDBObject(mylayer, true);

                    }
                    /*Adding Layer for Existing Table */
                    layerName = "Cross Section Data Table";
                    if (mylayers.Has(layerName) == false)
                        {

                            mylayer = new LayerTableRecord();
                            sLineTypeName = "CENTER";
                            if (acLineTypTable.Has(sLineTypeName) == false)
                            {
                                db.LoadLineTypeFile(sLineTypeName, "acad.lin");

                            }
                            mylayer.Name = layerName;
                            // mylayer.LinetypeObjectId = acLineTypTable[sLineTypeName];
                            mylayer.LineWeight = LineWeight.LineWeight009;
                            mylayers.UpgradeOpen();
                            mylayers.Add(mylayer);
                            trans.AddNewlyCreatedDBObject(mylayer, true);

                        }

                    /*Adding Layer for Existing Table */
                    layerName = "Cross Section Labels";
                    if (mylayers.Has(layerName) == false)
                    {

                        mylayer = new LayerTableRecord();
                        sLineTypeName = "CENTER";
                        if (acLineTypTable.Has(sLineTypeName) == false)
                        {
                            db.LoadLineTypeFile(sLineTypeName, "acad.lin");

                        }
                        mylayer.Name = layerName;
                        // mylayer.LinetypeObjectId = acLineTypTable[sLineTypeName];
                        mylayer.LineWeight = LineWeight.LineWeight009;
                        mylayers.UpgradeOpen();
                        mylayers.Add(mylayer);
                        trans.AddNewlyCreatedDBObject(mylayer, true);

                    }
                    /*Adding Layer for Design Long Section */
                    layerName = "Design Bed Level Long Section";
                    if (mylayers.Has(layerName) == false)
                    {

                        mylayer = new LayerTableRecord();
                        sLineTypeName = "ACAD_ISO14W100";
                        if (acLineTypTable.Has(sLineTypeName) == false)
                        {
                            db.LoadLineTypeFile(sLineTypeName, "acad.lin");

                        }
                        mylayer.Name = layerName;
                        mylayer.LinetypeObjectId = acLineTypTable[sLineTypeName];
                        mylayer.LineWeight = LineWeight.LineWeight009;
                        mylayers.UpgradeOpen();
                        mylayers.Add(mylayer);
                        trans.AddNewlyCreatedDBObject(mylayer, true);

                    }
                    /*Adding Layer for Existing Long Section */
                    layerName = "Existing Bed Level for Long Section";
                    if (mylayers.Has(layerName) == false)
                    {

                        mylayer = new LayerTableRecord();
                        sLineTypeName = "DASHDOT2";
                        if (acLineTypTable.Has(sLineTypeName) == false)
                        {
                            db.LoadLineTypeFile(sLineTypeName, "acad.lin");

                        }
                        mylayer.Name = layerName;
                        mylayer.LinetypeObjectId = acLineTypTable[sLineTypeName];
                        mylayer.LineWeight = LineWeight.LineWeight009;
                        mylayers.UpgradeOpen();
                        mylayers.Add(mylayer);
                        trans.AddNewlyCreatedDBObject(mylayer, true);

                    }
                    /*Adding Layer for Existing Left Bank */
                    layerName = "Existing Left Bank of Long section";
                    if (mylayers.Has(layerName) == false)
                    {

                        mylayer = new LayerTableRecord();
                        sLineTypeName = "DASHED2";
                        if (acLineTypTable.Has(sLineTypeName) == false)
                        {
                            db.LoadLineTypeFile(sLineTypeName, "acad.lin");

                        }
                        mylayer.Name = layerName;
                        mylayer.LinetypeObjectId = acLineTypTable[sLineTypeName];
                        mylayer.LineWeight = LineWeight.LineWeight009;
                        mylayers.UpgradeOpen();
                        mylayers.Add(mylayer);
                        trans.AddNewlyCreatedDBObject(mylayer, true);

                    }
                    /*Adding Layer for Existing Right Bank */
                    layerName = "Existing Right Bank of Long Section";
                    if (mylayers.Has(layerName) == false)
                    {

                        mylayer = new LayerTableRecord();
                        sLineTypeName = "ACAD_ISO15W100";
                        if (acLineTypTable.Has(sLineTypeName) == false)
                        {
                            db.LoadLineTypeFile(sLineTypeName, "acad.lin");

                        }
                        mylayer.Name = layerName;
                        mylayer.LinetypeObjectId = acLineTypTable[sLineTypeName];
                        mylayer.LineWeight = LineWeight.LineWeight009;
                        mylayers.UpgradeOpen();
                        mylayers.Add(mylayer);
                        trans.AddNewlyCreatedDBObject(mylayer, true);

                    }
                    /*Adding Layer for Existing Table */
                    layerName = "Long Section Data Table";
                    if (mylayers.Has(layerName) == false)
                    {

                        mylayer = new LayerTableRecord();
                        sLineTypeName = "CENTER";
                        if (acLineTypTable.Has(sLineTypeName) == false)
                        {
                            db.LoadLineTypeFile(sLineTypeName, "acad.lin");

                        }
                        mylayer.Name = layerName;
                        // mylayer.LinetypeObjectId = acLineTypTable[sLineTypeName];
                        mylayer.LineWeight = LineWeight.LineWeight009;
                        mylayers.UpgradeOpen();
                        mylayers.Add(mylayer);
                        trans.AddNewlyCreatedDBObject(mylayer, true);

                    }
                    /*Adding Layer for Long Section Label */
                    layerName = "Long Section Labels";
                    if (mylayers.Has(layerName) == false)
                    {

                        mylayer = new LayerTableRecord();
                        sLineTypeName = "CENTER";
                        if (acLineTypTable.Has(sLineTypeName) == false)
                        {
                            db.LoadLineTypeFile(sLineTypeName, "acad.lin");

                        }
                        mylayer.Name = layerName;
                        // mylayer.LinetypeObjectId = acLineTypTable[sLineTypeName];
                        mylayer.LineWeight = LineWeight.LineWeight009;
                        mylayers.UpgradeOpen();
                        mylayers.Add(mylayer);
                        trans.AddNewlyCreatedDBObject(mylayer, true);

                    }
                    /*Adding Layer for Long Section Label */
                    layerName = "Long Section Border";
                    if (mylayers.Has(layerName) == false)
                    {

                        mylayer = new LayerTableRecord();
                        sLineTypeName = "CENTER";
                        if (acLineTypTable.Has(sLineTypeName) == false)
                        {
                            db.LoadLineTypeFile(sLineTypeName, "acad.lin");

                        }
                        mylayer.Name = layerName;
                        // mylayer.LinetypeObjectId = acLineTypTable[sLineTypeName];
                        mylayer.LineWeight = LineWeight.LineWeight009;
                        mylayers.UpgradeOpen();
                        mylayers.Add(mylayer);
                        trans.AddNewlyCreatedDBObject(mylayer, true);

                    }
                    /*Adding Layer for Croos Data Borde */
                    layerName = "Cross section Data Border";
                    if (mylayers.Has(layerName) == false)
                        {

                            mylayer = new LayerTableRecord();
                            sLineTypeName = "CENTER";
                            if (acLineTypTable.Has(sLineTypeName) == false)
                            {
                                db.LoadLineTypeFile(sLineTypeName, "acad.lin");

                            }
                            mylayer.Name = layerName;
                            // mylayer.LinetypeObjectId = acLineTypTable[sLineTypeName];
                            mylayer.LineWeight = LineWeight.LineWeight009;
                            mylayers.UpgradeOpen();
                            mylayers.Add(mylayer);
                            trans.AddNewlyCreatedDBObject(mylayer, true);

                        }


                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("\n" + ex.Source);
                    
                }
                trans.Commit();
            }




        }

    }
}
