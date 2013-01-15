namespace System.Net
{
    internal static class HttpUtility
    {
        public static string HtmlEncode(string value)
        {
            return WebUtility.HtmlEncode(value);
        }

        public static string HtmlDecode(string value)
        {
            return WebUtility.HtmlDecode(value);
        }

        public static string UrlEncode(string str)
        {
            return WebUtility.UrlEncode(str);
        }

        public static string UrlDecode(string str)
        {
            return WebUtility.UrlDecode(str);
        }

        public static byte[] UrlEncodeToBytes(byte[] bytes, int offset, int count)
        {
            return WebUtility.UrlEncodeToBytes(bytes, offset, count);
        }

        public static byte[] UrlDecodeToBytes(byte[] bytes, int offset, int count)
        {
            return WebUtility.UrlDecodeToBytes(bytes, offset, count);
        }
    }
}