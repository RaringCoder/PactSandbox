using PactNet.Matchers;

namespace PactSandbox.ClientTests
{
    public static class Matchers
    {
        public static IMatcher Url(string example, string url)
        {
            var regex = url
                .Replace("/", @"\/") // Escape forward slashes in the Ruby-compatible regex.
                .Replace("{number}", @"\d+"); // Any digits one or more times.

            return Match.Regex(example, regex);
        }
    }
}
