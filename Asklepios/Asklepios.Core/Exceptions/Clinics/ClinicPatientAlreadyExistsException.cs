namespace Asklepios.Core.Exceptions.Clinics;

public class ClinicPatientAlreadyExistsException : CustomException
{
    public string PeselNumber { get; set; }
    
    public ClinicPatientAlreadyExistsException(string peselNumber) : base($"Patient with pesel: '{peselNumber}' exist.")
    {
        PeselNumber = peselNumber;
    }
}