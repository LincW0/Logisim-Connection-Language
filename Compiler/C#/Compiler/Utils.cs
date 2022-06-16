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
            int dotIndex = fileName.IndexOf("."); //Find the dot.
            return fileName.Substring(0,dotIndex); //Return everything before it.
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
            if(num == -1) //Check the condition
            {
                Console.WriteLine("Syntax Error: Line " + ln + ", expected '" + expected + "'."); //Formatted
                return true;
            }
            return false;
        }
        /// <summary>
        /// Check if a string ends with another certain string.
        /// </summary>
        /// <param name="from">The string thats needs checking.</param>
        /// <param name="endWith">What should the string end with?</param>
        /// <returns>Returns true if the string does end with that string, vice versa.</returns>
        public static bool stringEndWith(string from,string endWith)
        {
            int firstStringLength = from.Length; //Get the length.
            int secondStringLength = endWith.Length;
            int j = 0; //Initialize the loop.
            for(int i = firstStringLength - secondStringLength; i < firstStringLength; i++)
            {
                // "i" indicates the index in the "from" string
                // "j" indicates the index in the "endWith" string
                if (from[i] != endWith[j])
                {
                    return false; //If there is a character that doesn't matches, return false.
                }
                j++;
            }
            return true; //Return true if everything matches.
        }
    }
}
