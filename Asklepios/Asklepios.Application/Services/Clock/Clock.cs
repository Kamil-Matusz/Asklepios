namespace Asklepios.Application.Services.Clock;

public class Clock : IClock
{
    public DateTime CurrentDate() => DateTime.UtcNow;
}