using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
namespace ACAD_TUT_01
{
    public class Class1
    {
        [CommandMethod("Hello")]
        public static void HelloFromCSharp()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;
            edt.WriteMessage("I am  really exicted to see this working!");
            //start transection
            
        
        }
    }
}
