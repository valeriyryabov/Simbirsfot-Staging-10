namespace SimbirsfotStaging10.BLL.Infrastructure
{
    public static class UrlMakeUtils
    {
        public const string HttpsPrefix = "https://";
        public static string UriParamsFromTuppleArr((string, string)[] keyVals)
        {
            string res = "";
            foreach (var keyVal in keyVals)
                res += keyVal.Item1 + "=" + keyVal.Item2 + "&";
            return res.Substring(0, res.Length - 1);
        }

    }
}
