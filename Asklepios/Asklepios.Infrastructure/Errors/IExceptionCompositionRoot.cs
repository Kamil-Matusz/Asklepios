using Asklepios.Infrastructure.Exceptions;

namespace Asklepios.Infrastructure.Errors;

internal interface IExceptionCompositionRoot
{
    ExceptionResponse Map(Exception exception);
}