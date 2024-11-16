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

            page.Header().AlignCenter().Text("Wypis Pacjenta").FontSize(20).Bold();

            page.Content().Column(column =>
            {
                column.Spacing(10);

                column.Item().Text($"Imię i nazwisko pacjenta: {_details.PatientName} {_details.PatientSurname}").FontSize(12);
                column.Item().Text($"PESEL: {_details.PeselNumber}").FontSize(12);
                column.Item().Text($"Data wypisu: {_details.Date:yyyy-MM-dd}").FontSize(12);
                column.Item().Text($"Powód wypisu: {_details.DischargeReasson}").FontSize(12);
                column.Item().Text($"Uwagi: {_details.Summary}").FontSize(12);
                column.Item().Text($"Lekarz prowadzący: {_details.DoctorName} {_details.DoctorSurname}").FontSize(12);
            });

            page.Footer().AlignCenter().Text("Wygenerowano automatycznie przez system Asklepios.");
        });
    }
}