using Asklepios.Core.Entities.Examinations;
using Asklepios.Core.ValueObjects;
using Asklepios.Infrastructure.DAL.PostgreSQL;

namespace Asklepios.Infrastructure.DAL.Seeders.Examinations;

public class ExaminationsSeeder : IOrderedSeeder
{
    public int Order => 6;
    public async Task SeedAsync(AsklepiosDbContext dbContext)
    {
        var examinations = dbContext.Examinations.AsQueryable();
        if (!examinations.Any())
        {
            var newExaminations = new List<Examination>()
            {
                new Examination(1, "Morfologia Krwi", ExamCategory.Lab()),
                new Examination(2, "Badanie Hormonalne", ExamCategory.Lab()),
                new Examination(3, "Testy wątrobowe", ExamCategory.Lab()),
                new Examination(4, "RTG klatki piersiowej", ExamCategory.Picture()),
                new Examination(5, "USG jamy brzusznej", ExamCategory.Picture()),
                new Examination(6, "Tomografia", ExamCategory.Picture()),
                new Examination(7, "Rezonans magentyczny", ExamCategory.Picture()),
                new Examination(8, "Endoskopia", ExamCategory.Picture()),
                new Examination(9, "EKG", ExamCategory.Func()),
                new Examination(10, "Biopsja", ExamCategory.Func()),
                new Examination(11, "Test HIV", ExamCategory.Func()),
                new Examination(12, "Test na COVID-19", ExamCategory.Lab()),
                new Examination(13, "Posiew krwi", ExamCategory.Lab()),
                new Examination(14, "Badanie poziomu glukozy", ExamCategory.Lab()),
                new Examination(15, "Mammografia", ExamCategory.Picture()),
                new Examination(16, "RTG kończyn", ExamCategory.Picture()),
                new Examination(17, "USG tarczycy", ExamCategory.Picture()),
                new Examination(18, "Kolonoskopia", ExamCategory.Picture()),
                new Examination(19, "Spirometria", ExamCategory.Func()),
                new Examination(20, "Holter EKG", ExamCategory.Func()),
                new Examination(21, "Badanie neurologiczne", ExamCategory.Func()),
                new Examination(22, "Badanie dna oka", ExamCategory.Func()),
                new Examination(23, "Test tolerancji glukozy (OGTT)", ExamCategory.Lab()),
                new Examination(24, "Test na alergie pokarmowe", ExamCategory.Lab()),
                new Examination(25, "Gastroskopia", ExamCategory.Picture()),
                new Examination(26, "USG piersi", ExamCategory.Picture()),
                new Examination(27, "Audiometria", ExamCategory.Func()),
                new Examination(28, "Test wysiłkowy", ExamCategory.Func()),
                new Examination(29, "EEG", ExamCategory.Func()),
                new Examination(30, "Pomiar ciśnienia tętniczego 24h", ExamCategory.Func())
            };
            
            dbContext.Examinations.AddRange(newExaminations);
            await dbContext.SaveChangesAsync();
        }
    }
}