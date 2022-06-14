using System;

namespace Utils
{
    static class Tools
    {
        public static string StripExtension(string fileName)
        {
            return fileName.Substring(0,fileName.IndexOf("."));
        }
    }
}
