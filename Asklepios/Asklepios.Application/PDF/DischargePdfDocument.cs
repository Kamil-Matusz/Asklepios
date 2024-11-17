using Asklepios.Core.DTO.Patients;
using QuestPDF.Fluent;
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
            page.Margin(40);

            page.Content().Column(column =>
            {
                column.Spacing(20);
                
                column.Item().AlignCenter().Text("Wypis pacjenta ze szpitala")
                    .FontSize(20)
                    .Bold();
                
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(2);
                        columns.RelativeColumn(4);
                    });

                    table.Cell().Text("Imię i nazwisko:").FontSize(14).Bold();
                    table.Cell().Text($"{_details.PatientName} {_details.PatientSurname}").FontSize(14);
                    
                    column.Spacing(10);
                    
                    table.Cell().Text("PESEL:").FontSize(14).Bold();
                    table.Cell().Text(_details.PeselNumber).FontSize(14);
                    
                    column.Spacing(10);

                    table.Cell().Text("Adres:").FontSize(14).Bold();
                    table.Cell().Text(_details.Address).FontSize(14);

                    table.Cell().Text("Data wypisu:").FontSize(14).Bold();
                    table.Cell().Text($"{_details.Date:yyyy-MM-dd}").FontSize(14);

                    table.Cell().Text("Lekarz:").FontSize(14).Bold();
                    table.Cell().Text($"{_details.DoctorName} {_details.DoctorSurname}").FontSize(14);
                });
                
                column.Item().Text("Powód wypisu:")
                    .FontSize(14)
                    .Bold()
                    .Underline();

                column.Item().Text(_details.DischargeReasson).FontSize(12);
                
                column.Item().Text("Podsumowanie:")
                    .FontSize(14)
                    .Bold()
                    .Underline();

                column.Item().Text(_details.Summary).FontSize(12);
                
                column.Item().Row(row =>
                {
                    row.RelativeColumn().Column(inner =>
                    {
                        inner.Spacing(10);
                        inner.Item().Text("Podpis lekarza:").FontSize(14).Bold();
                        inner.Item().Text("_____________________").FontSize(12);
                    });

                    row.RelativeColumn().Column(inner =>
                    {
                        inner.Spacing(10);
                        inner.Item().Text("Miejsce na pieczątkę:").FontSize(14).Bold();
                        inner.Item().Text("_____________________").FontSize(12);
                    });
                });
            });
            
            page.Footer().AlignCenter().Text("Wygenerowano automatycznie przez system Asklepios.");
        });
    }
}