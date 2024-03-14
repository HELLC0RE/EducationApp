using Npgsql.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Windows.Forms;
using System.Xml.Linq;
using Npgsql;
using iText.Layout;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.pdf.draw;
using Chunk = iTextSharp.text.Chunk;

namespace EducationApp
{
    public class Generator
    {
        public void GenerateTXT(string fileName, DealInfo dealInfo)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = fileName;
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";

            string filePath;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = saveFileDialog.FileName;
            }
            else
            {
                return;
            }

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine("                                                      Договор оказания образовательных услуг");
                sw.WriteLine($"Г. Москва 20 сентября 2023 года");
                sw.WriteLine($"ООО «Sound Production», в лице генерального директора Левшина Сергея Сергеевича," +
                    $" действующего на основании Устава общества, именуемого в дальнейшем Исполнитель, с одной стороны");
                sw.WriteLine("И");
                string phoneNumber = dealInfo.phone_number;
                sw.WriteLine($"{dealInfo.lastname + " " + dealInfo.firstname + " " + dealInfo.midlename}, " +
                    $"{dealInfo.birthday.ToShortDateString()} года рождения, " +
                    $"проживающий по адресу: {dealInfo.address}, " +
                    $"паспорт: серия {dealInfo.passport_data.Split(' ')[0]}, номер {dealInfo.passport_data.Split(' ')[1]}, " +
                    $"выданный отделом УФМС России по Тюменской области в городе Тюмень " +
                    $"{dealInfo.birthday.ToShortDateString()}, " +
                    $"номер телефона: {$"{phoneNumber}"}, именуемый в дальнейшем Заказчик, с другой стороны");
                sw.WriteLine("заключили настоящий договор о нижеследующем");
                sw.WriteLine("Предмет");
                sw.WriteLine("В соответствии с настоящим соглашением Исполнитель в лице ООО «Образование» " +
                    $"обязуется оказать Заказчику в лице {dealInfo.lastname + " " + dealInfo.firstname + " " + dealInfo.midlename}, " +
                    "за оговоренную договором плату, следующие образовательные услуги:" + 
                    dealInfo.educationPrograms);
                sw.WriteLine("Заключительные положения");
                sw.WriteLine("  ● Настоящий договор составлен в двух экземплярах. Один экземпляр передается Заказчику, другой передается Исполнителю.");
                sw.WriteLine("  ● По всем моментам, которые не оговорены в настоящем соглашении, стороны руководствуются действующим законодательством Российской Федерации.");
                sw.WriteLine();
                sw.WriteLine("Подпись директора");
                sw.WriteLine("Подпись плательщика");
            }
        }
        
        public void GeneratePDF(string fileName, DealInfo dealInfo)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = fileName;
            saveFileDialog.Filter = "PDF файлы (*.pdf)|*.pdf";
            string filePath;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = saveFileDialog.FileName;
            }
            else
            {
                return;
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                iTextSharp.text.Document document = new iTextSharp.text.Document();
                BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 12);
                iTextSharp.text.Font boldFont = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.BOLD);
                using (PdfWriter writer = PdfWriter.GetInstance(document, fs))
                {
                    document.Open();
                    document.Add(new Paragraph("                                            Договор оказания образовательных услуг", boldFont));
                    document.Add(new Paragraph($"Г. Москва 20 сентября 2023 года", font));
                    document.Add(new Paragraph($"ООО «Sound Production», в лице генерального директора Левшина Сергея Сергеевича," +
                        $" действующего на основании Устава общества, именуемого в дальнейшем Исполнитель, с одной стороны", font));
                    document.Add(new Paragraph("И", font));
                    string phoneNumber = dealInfo.phone_number;
                    document.Add(new Paragraph($"{dealInfo.lastname + " " + dealInfo.firstname + " " + dealInfo.midlename}, " +
                        $"{dealInfo.birthday.ToShortDateString()} года рождения, " +
                        $"проживающий по адресу: {dealInfo.address}, " +
                        $"паспорт: серия {dealInfo.passport_data.Split(' ')[0]}, номер {dealInfo.passport_data.Split(' ')[1]}, " +
                        $"выданный отделом УФМС России по Тюменской области в городе Тюмень " +
                        $"{dealInfo.birthday.ToShortDateString()}, " +
                        $"номер телефона: {$"{phoneNumber}"}, именуемый в дальнейшем Заказчик, с другой стороны", font));
                    document.Add(new Paragraph("заключили настоящий договор о нижеследующем", font));
                    document.Add(new Paragraph("Предмет", boldFont));
                    document.Add(new Paragraph("В соответствии с настоящим соглашением Исполнитель в лице ООО «Центр образования» " +
                        $"обязуется оказать Заказчику в лице {dealInfo.lastname + " " + dealInfo.firstname + " " + dealInfo.midlename}, " +
                        "за оговоренную договором плату, следующие образовательные услуги:", font));
                        document.Add(new Paragraph("    ● " + dealInfo.educationPrograms, font));
                    document.Add(new Paragraph("Заключительные положения", boldFont));
                    document.Add(new Paragraph("    ● Настоящий договор составлен в двух экземплярах. Один экземпляр передается Заказчику, другой передается Исполнителю.", font));
                    document.Add(new Paragraph("    ● По всем моментам, которые не оговорены в настоящем соглашении, стороны руководствуются действующим законодательством Российской Федерации.", font));
                    document.Add(new Paragraph(""));
                    document.Add(new Paragraph("Подпись директора", boldFont));
                    document.Add(new Paragraph("Подпись плательщика", boldFont));
                    document.Close();
                }
            }
        }
        
        public void GeneratePdfPaymentList(string fileName, DealInfo dealInfo)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = fileName;
            saveFileDialog.Filter = "PDF файлы (*.pdf)|*.pdf";

            string filePath;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = saveFileDialog.FileName;
            }
            else
            {
                return;
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                iTextSharp.text.Document document = new iTextSharp.text.Document();
                BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font bigfont = new iTextSharp.text.Font(baseFont, 12);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 8);
                iTextSharp.text.Font smallFont = new iTextSharp.text.Font(baseFont, 5);
                iTextSharp.text.Font boldFont = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.BOLD);
                using (PdfWriter writer = PdfWriter.GetInstance(document, fs))
                {
                    document.Open();
                    PdfPTable table = new PdfPTable(2);
                    table.SetWidths(new float[] { 50, 120 });
                    PdfPCell leftCell = new PdfPCell();
                    Paragraph name = new Paragraph("Квитанция", bigfont);
                    name.Alignment = Element.ALIGN_CENTER;
                    name.SpacingBefore = 5f;
                    leftCell.AddElement(name);
                    string imagePath = "qr.jpg";
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagePath);
                    leftCell.AddElement(image);
                    table.AddCell(leftCell);
                    PdfPCell rightCell = new PdfPCell();
                    LineSeparator lineSeparator = new LineSeparator(0.0F, 101.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1);
                    Paragraph line = new Paragraph(new Chunk(lineSeparator));
                    line.SetLeading(0.5F, 0.5F);
                    Paragraph formname = new Paragraph("Форма №ПД-4", smallFont);
                    formname.Alignment = Element.ALIGN_CENTER;
                    rightCell.AddElement(formname);
                    Paragraph consumer = new Paragraph("ООО «Sound Production»", font);
                    consumer.Alignment = Element.ALIGN_CENTER;
                    rightCell.AddElement(consumer);
                    rightCell.AddElement(line);
                    Paragraph counsumernameDesc = new Paragraph("(наименование получателя платежа)", smallFont);
                    counsumernameDesc.Alignment = Element.ALIGN_CENTER;
                    rightCell.AddElement(counsumernameDesc);
                    rightCell.AddElement(new Paragraph("    ИНН 060834123664 КПП 255505341          30101810400000000886", font));
                    rightCell.AddElement(line);
                    rightCell.AddElement(new Paragraph("        (инн получателя платежа)                                        (номер счёта получателя платежа)", smallFont));
                    Paragraph bank = new Paragraph("БИК 964004621 (ОТДЕЛЕНИЕ БАНКА РОССИИ//УФК по Воронежской области г. Воронеж", font);
                    bank.Alignment = Element.ALIGN_CENTER;
                    rightCell.AddElement(bank);
                    rightCell.AddElement(line);
                    Paragraph banknameDesc = new Paragraph("(наименование банка получателя платежа)", smallFont);
                    banknameDesc.Alignment = Element.ALIGN_CENTER;
                    rightCell.AddElement(banknameDesc);
                    rightCell.AddElement(new Paragraph($"Договор:№{dealInfo.id}", font));
                    rightCell.AddElement(line);
                    rightCell.AddElement(new Paragraph($"ФИО обучающегося: " +
                        $"{dealInfo.lastname + " " + dealInfo.firstname + " " + dealInfo.midlename}", font));
                    rightCell.AddElement(line);
                    rightCell.AddElement(new Paragraph("Назначение: Оплата за курсы", font));
                    rightCell.AddElement(line);
                    rightCell.AddElement(new Paragraph($"ФИО плательщика: " +
                        $"{dealInfo.lastname + " " + dealInfo.firstname + " " + dealInfo.midlename}", font));
                    rightCell.AddElement(line);
                    rightCell.AddElement(new Paragraph($"Адрес плательщика: {dealInfo.address}", font));
                    rightCell.AddElement(line);
                    rightCell.AddElement(new Paragraph($"КБК: 18210441010983012110", font));
                    rightCell.AddElement(line);
                    rightCell.AddElement(new Paragraph($"ОКТМО: 12345678", font));
                    rightCell.AddElement(line);
                    rightCell.AddElement(new Paragraph($"Сумма: {dealInfo.total_cost} рублей", font));
                    rightCell.AddElement(line);
                    rightCell.AddElement(new Paragraph("С условиями приёма указанной в платёжном документе суммы, в т.ч. " +
                       "с суммой взимаемой платы за услуги банка ознакомлен и согласен.        Подпись плательщика ________________\\", smallFont));

                    table.AddCell(rightCell);

                    document.Add(table);

                    document.Close();
                }
            }
        }   
    }
}
