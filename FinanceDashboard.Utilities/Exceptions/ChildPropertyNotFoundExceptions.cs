namespace FinanceDashboard.Utilities.Exceptions
{
    public class ChildPropertyNotFoundExceptions : Exception
    {
        public ChildPropertyNotFoundExceptions(string child, string message) : base(child) { }
    }
}
