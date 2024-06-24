namespace Asklepios.Core.Exceptions.Examinations;

public class ExamResultNotFoundExaction : CustomException
{
    public Guid Id { get; set; }
    
    public ExamResultNotFoundExaction(Guid id) : base($"Exam result with ID: '{id}' was not found.")
    {
        Id = id;
    }
}