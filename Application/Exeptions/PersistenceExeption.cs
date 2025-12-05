namespace BibliotecaDigital.Application.Exeptions;

public class PersistenceExeption(string message, Exception innerException) : Exception(message, innerException)
{
}