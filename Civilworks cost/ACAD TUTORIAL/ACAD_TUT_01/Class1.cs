using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
namespace ACAD_TUT_01
{
    public class Class1
    {
        [CommandMethod("cl")]
        public static void CreateCircle()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            //start transection
            using (Transaction trans = db.TransactionManager.StartTransaction())
            { 
            
            
            }
        
        }
    }
}
