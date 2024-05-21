using Asklepios.Core.Exceptions.Examinations;

namespace Asklepios.Core.ValueObjects;

public sealed record AnesthesiaType
{
    public static IEnumerable<string> Anesthesia { get; } = new[] { "Ogólne", "Miejscowe","Regionalne", "Rdzeniowe"};
    
    public string Value { get; }

    public AnesthesiaType(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 30)
        {
            throw new InvalidAnesthesiaTypeException(value);
        }

        if (!Anesthesia.Contains(value))
        {
            throw new InvalidAnesthesiaTypeException(value);
        }

        Value = value;
    }
    
    public static AnesthesiaType General() => new("Ogólne");
    public static AnesthesiaType Local() => new("Miejscowe");
    public static AnesthesiaType Regional() => new("Regionalne");
    public static AnesthesiaType Spinal() => new("Rdzeniowe");
    
    public static implicit operator AnesthesiaType(string value) => new AnesthesiaType(value);

    public static implicit operator string(AnesthesiaType value) => value?.Value;

    public override string ToString() => Value;
}