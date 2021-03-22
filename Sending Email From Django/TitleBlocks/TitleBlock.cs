using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

namespace TitleBlocks
{
    public class TitleBlock
    {
        [CommandMethod("DTB")]
        public void DrawTitleBlock()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor edt = doc.Editor;

            PromptKeywordOptions pko = new PromptKeywordOptions("\nSelect Title Block Paper Size: ");
            pko.Keywords.Add("A4 (297mm x 210mm)");
            pko.Keywords.Add("A3 (420mm x 297mm)");
            pko.Keywords.Add("A2 (594mm x 420mm)");
            pko.Keywords.Add("A1 (841mm x 594mm)");
            pko.Keywords.Add("A0 (1189mm x 841mm)");
            pko.AllowNone = true;
            pko.AppendKeywordsToMessage = true;

            // Get the result
            PromptResult pr = edt.GetKeywords(pko);
            string paperSize = pr.StringResult.ToUpper();

            // Instantiate a TBlock class
            TBlock tb = new TBlock();

            PromptStringOptions pso = new PromptStringOptions("\nEnter Project Name: ");
            pso.AllowSpaces = true;
            pr = edt.GetString(pso);
            string projectName = pr.StringResult;
            if (projectName != "")
            {
                tb.ProjectName = projectName;
            }                

            pso = new PromptStringOptions("\nEnter Drawing Title: ");
            pso.AllowSpaces = true;
            pr = edt.GetString(pso);
            string drawingTitle = pr.StringResult;
            if (drawingTitle != "")
            {
                tb.DrawingTitle = drawingTitle;
            }
                
            pso = new PromptStringOptions("\nEnter Location: ");
            pso.AllowSpaces = true;
            pr = edt.GetString(pso);
            string location = pr.StringResult;
            if (location != "")
            {
                tb.Address = location;
            }
            
            pso = new PromptStringOptions("\nEnter Project Number: ");
            pso.AllowSpaces = false;
            pr = edt.GetString(pso);
            string projectNo = pr.StringResult;
            if (projectNo != "")
            {
                tb.ProjectNo = projectNo;
            }
            
            pso = new PromptStringOptions("\nEnter Project z : ");
            pso.AllowSpaces = false;
            pr = edt.GetString(pso);
            string projectDate = pr.StringResult;
            if (projectDate != "")
            {
                tb.DrawingDate = projectDate;
            }
            
            pso = new PromptStringOptions("\nEnter Drawing Scale: ");
            pso.AllowSpaces = false;
            pr = edt.GetString(pso);
            string drawingScale = pr.StringResult;
            if (drawingScale != "")
            {
                tb.DrawingScale = drawingScale;
            }
            
            pso = new PromptStringOptions("\nEnter Revision Number: ");
            pso.AllowSpaces = false;
            pr = edt.GetString(pso);
            string revNo = pr.StringResult;
            if (revNo != "")
            {
                tb.RevisionNo = revNo;
            }            

            pso = new PromptStringOptions("\nEnter Drawing Number: ");
            pso.AllowSpaces = false;
            pr = edt.GetString(pso);
            string drawingNo = pr.StringResult;
            if (drawingNo != "")
            {
                tb.DrawingNo = drawingNo;
            }
            
            // Handle the option selected
            switch (paperSize)
            {
                case "A4":
                    // Instantiate A4 size and call DrawTitleBlock
                    A4TitleBlock a4 = new A4TitleBlock();                    
                    tb.DrawTitleBlock(a4.Height, a4.Width);
                    break;
                case "A3":
                    // Instantiate A3 size and call DrawTitleBlock                    
                    A3TitleBlock a3 = new A3TitleBlock();
                    tb.DrawTitleBlock(a3.Height, a3.Width);
                    break;
                case "A2":
                    // Instantiate A2 size and call DrawTitleBlock
                    A2TitleBlock a2 = new A2TitleBlock();
                    tb.DrawTitleBlock(a2.Height, a2.Width);
                    break;
                case "A1":
                    // Instantiate A1 size and call DrawTitleBlock
                    A1TitleBlock a1 = new A1TitleBlock();
                    tb.DrawTitleBlock(a1.Height, a1.Width);
                    break;
                case "A0":
                    // Instantiate A0 size and call DrawTitleBlock
                    A0TitleBlock a0 = new A0TitleBlock();
                    tb.DrawTitleBlock(a0.Height, a0.Width);
                    break;
            }
        }
    }
}
