using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Autocad Related Imports*/
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
namespace Khal_Deafting
{
    public struct AutocadReference 
    {
        Document doc;
        Database db;
        Editor ed;
    }
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
        public static void Main(string [] args) 
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
