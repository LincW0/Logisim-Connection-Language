using System;

namespace Utils
{
    /// <summary>
    /// <para>This is a static class filled with handy tools.</para>
    /// <para>
    /// Created date: 2022/6/14
    /// Created time: 16:48
    /// Author: ItsHealingSpell
    /// </para>
    /// </summary>
    static class Tools
    {
        /// <summary>
        /// Strip the extension of a file name.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>The striped file name.</returns>
        public static string StripExtension(string fileName)
        {
            return fileName.Substring(0,fileName.IndexOf("."));
        }
        /// <summary>
        /// Prints a error onscreen if a number equals -1, formatted like: "Syntax Error: Line xx, Expected xxx"
        /// This is just a way to simplify the code.
        /// </summary>
        /// <param name="num">The number that needed checking</param>
        /// <param name="ln">The "Line" Information</param>
        /// <param name="expected">The "Expected" Information</param>
        /// <returns>
        /// Returns true if the number equals -1, vice versa.
        /// </returns>
        public static bool CheckSyntaxError(int num, int ln, string expected)
        {
            if(num == -1)
            {
                Console.WriteLine("Syntax Error: Line " + ln + ", expected '" + expected + "'.");
                return true;
            }
            return false;
        }
    }
}
