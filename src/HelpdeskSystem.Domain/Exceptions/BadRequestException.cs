namespace HelpdeskSystem.Domain.Exceptions;

public sealed class BadRequestException(string message) : Exception(message);