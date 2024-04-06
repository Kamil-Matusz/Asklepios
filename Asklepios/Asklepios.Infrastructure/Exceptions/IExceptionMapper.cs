namespace Asklepios.Infrastructure.Exceptions;

public interface IExceptionMapper
{
    ExceptionResponse Map(Exception exception);
}