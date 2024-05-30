using Asklepios.Core.Exceptions.Examinations;

namespace Asklepios.Core.ValueObjects;

public sealed record ExamCategory
{
    public static IEnumerable<string> ExamCategories { get; } = new[] { "Laboratoryjne", "Obrazowe","Funkcjonalne"};
    
    public string Value { get; }

    public ExamCategory(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 30)
        {
            throw new InvalidExamCategoryException(value);
        }

        if (!ExamCategories.Contains(value))
        {
            throw new InvalidExamCategoryException(value);
        }

        Value = value;
    }
    
    public static ExamCategory Lab() => new("Laboratoryjne");
    public static ExamCategory Picture() => new("Obrazowe");
    public static ExamCategory Func() => new("Funkcjonalne");
    
    public static implicit operator ExamCategory(string value) => new ExamCategory(value);

    public static implicit operator string(ExamCategory value) => value?.Value;

    public override string ToString() => Value;
}