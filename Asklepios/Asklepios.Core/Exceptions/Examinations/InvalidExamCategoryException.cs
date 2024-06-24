namespace Asklepios.Core.Exceptions.Examinations;

public class InvalidExamCategoryException : CustomException
{
    public string ExamCategory { get; }

    public InvalidExamCategoryException(string examCategory) : base($"Exam category: '{examCategory}' is invalid.")
    {
        ExamCategory = examCategory;
    }
}