using Utils;
class Program
{
    private static string LCLFileName; //The LCL file name is used globally.
    static void Main(string[] args)
    {
        //if (args.Length == 0) return; //Exits if not given enough arguments
        //LCLFileName = args[0]; //Read the first argument as the file name.
        LCLFileName = "E:\\GitHub\\Logisim-Connection-Language\\Test\\04.lcl"; //For debugging

        //Parse the LCL.
        bool parseSuccess=ParseLCL();
        if (!parseSuccess) return; //Exits if its unable to parse the file.

        constructRelativePosition();
        constructCirc();
    }
    /// <summary>
    /// Parses the LCL source code.
    /// </summary>
    /// <returns>
    /// Returns false if an error occurred, vice versa.
    /// </returns>
    private static bool ParseLCL()
    {
        bool finishedImporting = false; //Indicates whether the importing is done.
        int curLine = 1; //Indicates the current line index.
        try //Catches the exception caused by "StreamReader" and the other exceptions.
        {
            using (StreamReader sr = new StreamReader(LCLFileName)) //Uses StreamReader to Read
            {
                while (!sr.EndOfStream) //Keep on reading until the end of file.
                {
                    try //Catches the exception caused by "ReadLine".
                    {
                        string line = sr.ReadLine(); //Deal with one line once.
                        if (line.StartsWith("CNCT")) //Detects the CNCT statement (See LCL format for more).
                        {
                            finishedImporting = true; //IMPT always come before CNCT.

                            //Find the first number.
                            int firstNumberStart = line.IndexOf("["); //Find the first "["
                            if (Tools.CheckSyntaxError(firstNumberStart, curLine, "[")) return false; //Log a error and stop the program if couldn't find it.
                            int firstNumberEnd = line.IndexOf("]", firstNumberStart); //Find the first "]" starting from the first "[" 
                            if (Tools.CheckSyntaxError(firstNumberEnd, curLine, "]")) return false; //Same as above.

                            //Find the second number.
                            int secondNumberStart = line.IndexOf("[", firstNumberEnd); //Find the second "[" starting from the first "]"
                            if (Tools.CheckSyntaxError(secondNumberStart, curLine, "[")) return false; //Same as above.
                            int secondNumberEnd = line.IndexOf("]", secondNumberStart); //Find the second "]" starting from the second "[" 
                            if (Tools.CheckSyntaxError(secondNumberEnd, curLine, "]")) return false; //Same as above.

                            //Find the third number.
                            int thirdNumberStart = line.IndexOf("[", secondNumberEnd); //Find the third "[" starting from the second "]"
                            if (Tools.CheckSyntaxError(thirdNumberStart, curLine, "[")) return false; //Same as above.
                            int thirdNumberEnd = line.IndexOf("]", thirdNumberStart); //Find the third "]" starting from the third "[" 
                            if (Tools.CheckSyntaxError(thirdNumberEnd, curLine, "]")) return false; //Same as above.

                            //Find the fourth number.
                            int fourthNumberStart = line.IndexOf("[", thirdNumberEnd); //Find the fourth "[" starting from the third "]"
                            if (Tools.CheckSyntaxError(fourthNumberStart, curLine, "[")) return false; //Same as above.
                            int fourthNumberEnd = line.IndexOf("]", fourthNumberStart); //Find the fourth "]" starting from the fourth "[" 
                            if (Tools.CheckSyntaxError(fourthNumberEnd, curLine, "]")) return false; //Same as above.


                            try //Catches the exception caused by "int.Parse"
                            {
                                //Parse numbers
                                int firstNumber = int.Parse(line.Substring(firstNumberStart + 1, firstNumberEnd - firstNumberStart));
                                int secondNumber = int.Parse(line.Substring(secondNumberStart + 1, secondNumberEnd - secondNumberStart));
                                int thirdNumber = int.Parse(line.Substring(thirdNumberStart + 1, thirdNumberEnd - thirdNumberStart));
                                int fourthNumber = int.Parse(line.Substring(fourthNumberStart + 1, fourthNumberEnd - fourthNumberStart));


                            }
                            catch (ArgumentNullException err)
                            {
                                //Didn't find the parameter.
                                Console.WriteLine("Syntax Error: Line " + curLine + ", expected an parameter.\n" + err.Message);
                                return false;
                            }
                            catch (FormatException err)
                            {
                                //Invalid parameter.
                                Console.WriteLine("Syntax Error: Line " + curLine + ", invalid parameter.\n" + err.Message);
                                return false;
                            }
                            catch (OverflowException err)
                            {
                                //Parameter out of range.
                                Console.WriteLine("Syntax Error: Line " + curLine + ", parameter out of range.\n" + err.Message);
                                return false;
                            }
                        }
                        curLine++;
                    }
                    catch(IOException err)
                    {
                        Console.WriteLine("Error: IOError occurred while reading the source file");
                        Console.Write(err.Message);
                        return false;
                    }
                    catch(OutOfMemoryException err)
                    {
                        Console.WriteLine("Error: Out of memory while reading the source file");
                        Console.Write(err.Message);
                        return false;
                    }
                }
            }
            return true;
        }
        catch (IOException err)
        {
            Console.WriteLine("Error: IOError occurred while opening the source file");
            Console.Write(err.Message);
            return false;
        }
        catch (Exception err)
        {
            Console.WriteLine("Error: Error occurred while accessing the source file");
            Console.Write(err.Message);
            return false;
        }
    }

    private static void constructRelativePosition()
    {

    }

    private static bool constructCirc()
    {
        try
        {
            Circ.CircStream file = new Circ.CircStream(Tools.StripExtension(LCLFileName) + ".circ");
            file.IO(false, 2, 1);
            file.IO(false, 2, 3);
            file.Wire(true, 2, 1, 10);
            file.Wire(true, 2, 3, 10);
            file.AND(15, 2);
            file.Wire(true, 15, 2, 1);
            file.IO(true, 16, 2);
            file.Close();
            return true;
        }
        catch (Exception err)
        {
            Console.WriteLine("Error: Error occurred while accessing the circ file");
            Console.Write(err.Message);
            return false;
        }
    }
}

