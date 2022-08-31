using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Path = System.IO.Path;
using Text = iText.Layout.Element.Text;

namespace Migros
{
    public static class Printer
    {
        public static void Print(Cari cari, List<Siparis> siparisler)
        {
            var now = DateTime.Now;
            string fileName = "Migros-" + DateTime.Now.ToString("dd.MM.yyyy HH.mm.ss") + " " + cari.CariNo.ToString(Globals.settings.cariNoFormat);

            string fullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName + ".pdf");

            int num = 0;
            while (File.Exists(fullPath))
                fullPath = Path.Combine(fullPath, fileName + "(" + ++num + ").pdf");

            using (FileStream fs = new FileStream(fullPath, FileMode.Create))
            using (PdfWriter pdfwriter = new PdfWriter(fs))
            using (PdfDocument pdfDocument = new PdfDocument(pdfwriter))
            using (Document document = new Document(pdfDocument, PageSize.A4))
            {
                document.SetMargins(20, 20, 20, 20);
                PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA, "CP1254");
                PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD, "CP1254");
                document.Add(new Paragraph(new Text("Cari No: ").SetFont(bold)).Add(new Text(cari.CariNo.ToString(Globals.settings.cariNoFormat)).SetFont(font)));
                document.Add(new Paragraph(new Text("İsim: ").SetFont(bold)).Add(new Text(cari.Isim).SetFont(font)));
                document.Add(new Paragraph(new Text("Kart No: ").SetFont(bold)).Add(new Text(cari.KartNo).SetFont(font)));
                Table table = new Table(5).UseAllAvailableWidth().SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                table.AddHeaderCell(new Cell().Add(new Paragraph("Sipariş No.")).SetFont(bold));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Puan")).SetFont(bold));
                table.AddHeaderCell(new Cell().Add(new Paragraph("TL")).SetFont(bold));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Kullanılan")).SetFont(bold));
                table.AddHeaderCell(new Cell().Add(new Paragraph("İşlem Tarihi")).SetFont(bold));
                foreach (var item in siparisler)
                {
                    table.AddCell(new Cell().Add(new Paragraph(item.SipNo.ToString(Globals.settings.siparisNoFormat))).SetFont(font));
                    table.AddCell(new Cell().Add(new Paragraph(item.Puan.ToString())).SetFont(font));
                    table.AddCell(new Cell().Add(new Paragraph(item.TL.ToString(Globals.settings.tlFormat))).SetFont(font));
                    table.AddCell(new Cell().Add(new Paragraph(item.Kullanilan.ToString(Globals.settings.tlFormat))).SetFont(font));
                    table.AddCell(new Cell().Add(new Paragraph(item.IslemTarihi.ToString(Globals.settings.tarihFormat))).SetFont(font));
                }

                document.Add(table);

                decimal toplamPuan = siparisler.Sum(f => f.Puan),
                    toplamTL = siparisler.Sum(f => f.TL),
                    toplamKullanilan = siparisler.Sum(f => f.Kullanilan),
                    kalan = toplamTL - toplamKullanilan;

                document.Add(new Paragraph(new Text("Toplam Puan: ").SetFont(bold)).Add(new Text(toplamPuan.ToString()).SetFont(font)));
                document.Add(new Paragraph(new Text("Toplam TL: ").SetFont(bold)).Add(new Text(toplamTL.ToString(Globals.settings.tlFormat)).SetFont(font)));
                document.Add(new Paragraph(new Text("Toplam Kullanılan: ").SetFont(bold)).Add(new Text(toplamKullanilan.ToString(Globals.settings.tlFormat)).SetFont(font)));
                document.Add(new Paragraph(new Text("Kalan: ").SetFont(bold)).Add(new Text(kalan.ToString(Globals.settings.tlFormat)).SetFont(font)));
            }

            Process.Start(fullPath);
        }
    }
}