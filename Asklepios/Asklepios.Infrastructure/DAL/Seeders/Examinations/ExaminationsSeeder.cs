using Asklepios.Core.Entities.Examinations;
using Asklepios.Core.ValueObjects;
using Asklepios.Infrastructure.DAL.PostgreSQL;

namespace Asklepios.Infrastructure.DAL.Seeders.Examinations;

public class ExaminationsSeeder : IOrderedSeeder
{
    public int Order => 6;
    public async Task SeedAsync(AsklepiosDbContext dbContext)
    {
        var examinations = dbContext.Examinations.ToList();
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
                new Examination(11, "Test HIV", ExamCategory.Func())
            };
            
            dbContext.Examinations.AddRange(newExaminations);
            await dbContext.SaveChangesAsync();
        }
    }
}