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
    public class DrawKhal
    {
    public Document GetDocument()

        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            return doc;
        
        }

    }
    public class Driver
    { 
    
    
    
    }
}
