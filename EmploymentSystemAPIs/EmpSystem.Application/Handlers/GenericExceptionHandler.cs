namespace EmpSystem.Application.Handlers
{
    public static class GenericExceptionHandler
    {
        public static T Handle<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
