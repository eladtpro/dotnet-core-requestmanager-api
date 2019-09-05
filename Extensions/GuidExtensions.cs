using System;
namespace RequestManager.Extensions
{
    public static class GuidExtensions
    {
        const string KEY_FORMAT = "N";

        public static string Format(this Guid key)
        {
            return key.ToString(KEY_FORMAT);
        }
    }
}
