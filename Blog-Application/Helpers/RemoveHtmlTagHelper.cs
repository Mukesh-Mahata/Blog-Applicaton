using System.Text.RegularExpressions;
using System.Net;

namespace Blog_Application.Helpers
{
    public static class RemoveHtmlTagHelper
    {
        public static string RemoveHtmlTags(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // Remove HTML tags (non-greedy) then decode HTML entities
            var withoutTags = Regex.Replace(input, "<.*?>", string.Empty);
            return WebUtility.HtmlDecode(withoutTags);
        }
    }
} 
