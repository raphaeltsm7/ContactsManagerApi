using ContactsManagerWeb.Models;

namespace ContactsManagerWeb.Extensions
{
    static class APIResponseExtension
    {
        public static bool IsValid(this APIResponse response)
        {
            return (response != null && response.IsSuccess);
        }
    }
}