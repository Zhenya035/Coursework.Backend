namespace Coursework.Domain.Exceptions;

public class AlreadyAddedException(string message) : Exception($"{message} is already in the database");