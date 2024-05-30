namespace Asklepios.Core.Exceptions.Examinations;

public class ExaminationNotFoundException : CustomException
{
    public int Id { get; set; }
    
    public ExaminationNotFoundException(int id) : base($"Examination with ID: '{id}' was not found.")
    {
        Id = id;
    }
}