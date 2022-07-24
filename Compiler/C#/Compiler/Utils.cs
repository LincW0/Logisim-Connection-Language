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
            if ((from[firstStringLength - secondStringLength - 1] != ((char) 32)) && (from[firstStringLength - secondStringLength -1] != '\t')) return false;
            return true; //Return true if everything matches.
        }

        public static string GetIMPTStringLastKeyword(string line)
        {
            int keywordEnd = -1;
            int keywordStart = -1;
            for(int i = line.Length - 1; i >= 0; i--)
            {
                if ((line[i] != ';') && (line[i] != '\t') && (line[i] != ((char) 32)))
                {
                    keywordEnd = i;
                    break;
                }
            }
            if(keywordEnd == -1)
            {
                throw new FormatException("Error: Irregular line.");
            }
            for(int i = keywordEnd; i >= 0; i--)
            {
                if ((line[i] == ',') || (line[i] == ((char) 32)))
                {
                    keywordStart = i + 1;
                    break;
                }
            }
            return line.Substring(keywordStart, keywordEnd - keywordStart + 1);
        }

        public static string GetCNCTStringLastKeyword(string from)
        {
            int firstStringLength = from.Length;
            int keywordStart = -1;
            int keywordEnd;
            int indexOfComma = from.IndexOf(",");
            if (indexOfComma == -1)
            {
                throw new FormatException("Error: Irregular line.");
            }
            keywordEnd = from.Substring(indexOfComma).IndexOf("[");
            for (int i = indexOfComma + 1; i < from.Length; i++)
            {
                if ((from[i] != ((char)32)) && (from[i] == '\t'))
                {
                    keywordStart = i;
                    break;
                }
            }
            return from.Substring(keywordStart, keywordEnd - keywordStart);
        }

        public static string? GetCNCTStringFirstKeyword(string from)
        {
            int firstStringLength = from.Length;
            int keywordStart = -1;
            int keywordEnd = from.IndexOf(',');
            for (int i = 0; i < firstStringLength; i++)
            {
                if ((from[i] == ((char)32)) || (from[i] == '\t'))
                {
                    keywordStart = i + 1;
                    break;
                }
            }
            if (keywordStart == -1) return null;
            string keyword = from.Substring(keywordStart, keywordEnd - keywordStart);
            return keyword;
        }

        /*public static KeyValuePair<string, string>? findStringStartKeyValue(string from, Dictionary<string, string> dictionary)
        {
            int firstStringLength = from.Length;
            int keywordStart = -1;
            int keywordEnd = -1;
            for (int i = 0; i < firstStringLength; i++)
            {
                if ((from[i] == ((char)32)) || (from[i] == '\t'))
                {
                    keywordStart = i + 1;
                    break;
                }
            }
            if (keywordStart == -1) return null;
            for (int i = keywordStart + 1; i < firstStringLength; i++)
            {
                if ((from[i] == ((char)32)) || (from[i] == '\t'))
                {
                    keywordEnd = i;
                    break;
                }
            }
            if (keywordEnd == -1) return null;
            string keyword = from.Substring(keywordStart, keywordEnd - keywordStart);
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
                if (String.Compare(from, keyword) == 0)
                {
                    return keyValuePair;
                }
            }
            return null;
        }*/
    }
}
