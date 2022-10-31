namespace ICD.Exceptions.EntityExceptions;
public class EntityCouldNotFoundException : Exception
{
    private const string message = "Entity could not found";
    public EntityCouldNotFoundException() : base(message)
    {

    }
}
