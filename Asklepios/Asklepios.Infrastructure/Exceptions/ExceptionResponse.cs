using System.Net;

namespace Asklepios.Infrastructure.Exceptions;

public record ExceptionResponse(object Response, HttpStatusCode StatusCode);