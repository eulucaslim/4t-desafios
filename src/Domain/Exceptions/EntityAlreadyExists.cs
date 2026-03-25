namespace Domain.Exceptions;

public class EntityAlreadyExists(string message) : Exception(message);
