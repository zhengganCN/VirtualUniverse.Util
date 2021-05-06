using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.IO;

namespace VirtualUniverse.Export.iTextSharp
{
    public class Class1
    {
        public void S()
        {
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(new FileStream("/myfiles/hello.pdf", FileMode.Create, FileAccess.Write)));
            Document document = new Document(pdfDocument);
            Table
            String line = "Hello! Welcome to iTextPdf";
            document.Add(new Paragraph(line));
            document.Close();
            Console.WriteLine("Awesome PDF just got created.");
        }
    }
}
