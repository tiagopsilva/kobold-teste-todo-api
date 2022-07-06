namespace Kobold.TodoApp.Api.Extensions
{
    public static class StringExtensions
    {
        public static bool IsPresent(this string value)
        {
            return value != null && value.Trim() != string.Empty;
        }
    }
}
