using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Autocad Related Imports*/
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;

namespace Khal_Deafting
{

    public class DrawKhal

    {

        

    public Tuple<Document,Database,Editor> GetDocumentReference()

        {
           
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;


            return new Tuple<Document, Database, Editor>(doc, db, edt);
        
        }

    }
    public class Driver
    {
        [CommandMethod ("DRAW_KHAL")]
        public void DrawMyKhal () 
        {
            DrawKhal mykhal = new DrawKhal();
            Tuple <Document, Database, Editor> myreferences = mykhal.GetDocumentReference();
            Document doc = myreferences.Item1;
            Database db= myreferences.Item2;
            Editor edt = myreferences.Item3;
            edt.WriteMessage("Sucessfully Initiated Khal drawing......");



        }
    
    
    }
}
