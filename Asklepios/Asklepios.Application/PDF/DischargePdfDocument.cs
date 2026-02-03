using Asklepios.Core.DTO.Patients;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Asklepios.Application.PDF;

public class DischargePdfDocument : IDocument
{
    private readonly DischargeItemDto _details;

    public DischargePdfDocument(DischargeItemDto details)
    {
        _details = details;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(50);
            page.Size(PageSizes.A4);

            page.Header().BorderBottom(1).BorderColor("#004d40").PaddingBottom(10).Row(row =>
            {
                row.RelativeItem().AlignLeft().Text("System Asklepios")
                    .FontSize(10)
                    .FontColor("#004d40");
                
                row.RelativeItem().AlignRight().Text($"Data: {DateTime.Now:dd.MM.yyyy}")
                    .FontSize(10)
                    .FontColor("#666666");
            });

            page.Content().PaddingVertical(20).Column(column =>
            {
                column.Spacing(15);
                
                // Tytuł dokumentu
                column.Item().AlignCenter().Text("WYPIS PACJENTA ZE SZPITALA")
                    .FontSize(22)
                    .Bold()
                    .FontColor("#004d40");
                
                column.Item().PaddingTop(5).LineHorizontal(2).LineColor("#004d40");
                
                // Sekcja: Dane pacjenta
                column.Item().PaddingTop(15).Text("Dane pacjenta")
                    .FontSize(16)
                    .Bold()
                    .FontColor("#00695c");
                
                column.Item().PaddingTop(10).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(3);
                        columns.RelativeColumn(7);
                    });

                    // Styling dla wszystkich komórek
                    table.Cell().Border(1).BorderColor("#cccccc")
                        .Padding(10).Background("#f5f5f5")
                        .Text("Imię i nazwisko:").FontSize(12).Bold();
                    table.Cell().Border(1).BorderColor("#cccccc")
                        .Padding(10)
                        .Text($"{_details.PatientName} {_details.PatientSurname}").FontSize(12);
                    
                    table.Cell().Border(1).BorderColor("#cccccc")
                        .Padding(10).Background("#f5f5f5")
                        .Text("PESEL:").FontSize(12).Bold();
                    table.Cell().Border(1).BorderColor("#cccccc")
                        .Padding(10)
                        .Text(_details.PeselNumber).FontSize(12);

                    table.Cell().Border(1).BorderColor("#cccccc")
                        .Padding(10).Background("#f5f5f5")
                        .Text("Adres:").FontSize(12).Bold();
                    table.Cell().Border(1).BorderColor("#cccccc")
                        .Padding(10)
                        .Text(_details.Address).FontSize(12);

                    table.Cell().Border(1).BorderColor("#cccccc")
                        .Padding(10).Background("#f5f5f5")
                        .Text("Data wypisu:").FontSize(12).Bold();
                    table.Cell().Border(1).BorderColor("#cccccc")
                        .Padding(10)
                        .Text($"{_details.Date:dd.MM.yyyy}").FontSize(12);

                    table.Cell().Border(1).BorderColor("#cccccc")
                        .Padding(10).Background("#f5f5f5")
                        .Text("Lekarz prowadzący:").FontSize(12).Bold();
                    table.Cell().Border(1).BorderColor("#cccccc")
                        .Padding(10)
                        .Text($"{_details.DoctorName} {_details.DoctorSurname}").FontSize(12);
                });
                
                // Sekcja: Powód wypisu
                column.Item().PaddingTop(20).Text("Powód wypisu")
                    .FontSize(16)
                    .Bold()
                    .FontColor("#00695c");
                
                column.Item().PaddingTop(10).Border(1).BorderColor("#cccccc")
                    .Padding(15).Background("#fafafa")
                    .Text(_details.DischargeReasson)
                    .FontSize(12)
                    .LineHeight(1.5f);
                
                // Sekcja: Podsumowanie
                column.Item().PaddingTop(20).Text("Podsumowanie")
                    .FontSize(16)
                    .Bold()
                    .FontColor("#00695c");

                column.Item().PaddingTop(10).Border(1).BorderColor("#cccccc")
                    .Padding(15).Background("#fafafa")
                    .Text(_details.Summary)
                    .FontSize(12)
                    .LineHeight(1.5f);
                
                // Sekcja: Podpisy
                column.Item().PaddingTop(40).Row(row =>
                {
                    row.RelativeItem().Column(leftColumn =>
                    {
                        leftColumn.Item().Text("Podpis lekarza:")
                            .FontSize(11)
                            .FontColor("#666666");
                        leftColumn.Item().PaddingTop(30).LineHorizontal(1).LineColor("#333333");
                        leftColumn.Item().AlignCenter().PaddingTop(5).Text($"{_details.DoctorName} {_details.DoctorSurname}")
                            .FontSize(10)
                            .FontColor("#666666");
                    });

                    row.ConstantItem(50);

                    row.RelativeItem().Column(rightColumn =>
                    {
                        rightColumn.Item().Text("Pieczątka:")
                            .FontSize(11)
                            .FontColor("#666666");
                        rightColumn.Item().PaddingTop(15).Height(50).Border(1).BorderColor("#999999");
                    });
                });
            });
            
            page.Footer().AlignCenter().Text(text =>
            {
                text.Span("Wygenerowano automatycznie przez system ").FontSize(9).FontColor("#999999");
                text.Span("Asklepios").FontSize(9).Bold().FontColor("#004d40");
                text.Span($" | {DateTime.Now:dd.MM.yyyy HH:mm}").FontSize(9).FontColor("#999999");
            });
        });
    }
}