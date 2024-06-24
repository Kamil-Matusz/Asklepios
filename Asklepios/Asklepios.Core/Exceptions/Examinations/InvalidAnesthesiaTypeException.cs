namespace Asklepios.Core.Exceptions.Examinations;

public class InvalidAnesthesiaTypeException : CustomException
{
    public string AnesthesiaType { get; }

    public InvalidAnesthesiaTypeException(string anesthesiaType) : base($" '{anesthesiaType}' is invalid.")
    {
        AnesthesiaType = anesthesiaType;
    }
}