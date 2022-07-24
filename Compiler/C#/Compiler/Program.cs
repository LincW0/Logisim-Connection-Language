using Utils;
using System.Collections.Generic;
using System.Linq;
class Program
{
    private static List<Structure.InputComponent>? Inputs;
    private static List<Structure.OutputComponent>? Outputs;
    private static List<Structure.ANDComponent>? ANDs;
    private static List<Structure.ORComponent>? ORs;
    private static List<Structure.XORComponent>? XORs;
    private static List<Structure.NOTComponent>? NOTs;
    static void Main(string[] args)
    {
        //if (args.Length == 0) return; //Exits if not given enough arguments
        //string? LCLFileName = args[0]; //Read the first argument as the file name.
        string? LCLFileName = "E:\\GitHub\\Logisim-Connection-Language\\Test\\04.lcl"; //For debugging

        //Parse the LCL.
        Initialize();
        bool parseSuccess=ParseLCL(LCLFileName);
        if (!parseSuccess) return; //Exits if its unable to parse the file.

        constructCirc(LCLFileName);
    }
    private static void Initialize()
    {
        ANDs = new List<Structure.ANDComponent>();
        ORs = new List<Structure.ORComponent>();
        XORs = new List<Structure.XORComponent>();
        NOTs = new List<Structure.NOTComponent>();
        Inputs = new List<Structure.InputComponent>();
        Outputs = new List<Structure.OutputComponent>();

        Structure.Node.InitializeNodes();
        Structure.Component.InitializeComponents();
    }
    public enum ComponentType
    {
        Input,
        Output,
        AND,
        OR,
        XOR,
        NOT,
    }
    /// <summary>
    /// Parses the LCL source code.
    /// </summary>
    /// <returns>
    /// Returns false if an error occurred, vice versa.
    /// </returns>
    private static bool ParseLCL(string fileName)
    {
        bool finishedImporting = false; //Indicates whether the importing is done.
        int curLine = 1; //Indicates the current line index.
        Dictionary<string, string> imports = new Dictionary<string, string>();
        Dictionary<string, IONum> importIOs = new Dictionary<string, IONum>();
        Dictionary<string, List<Structure.Node>> importInputs = new Dictionary<string, List<Structure.Node>>();
        Dictionary<string, List<Structure.Component>> importOutputs = new Dictionary<string, List<Structure.Component>>();
        try //Catches the exception caused by "StreamReader" and the other exceptions.
        {
            using (StreamReader sr = new StreamReader(fileName)) //Uses StreamReader to Read
            {
                while (!sr.EndOfStream) //Keep on reading until the end of file.
                {
                    try //Catches the exception caused by "ReadLine".
                    {
                        string line = sr.ReadLine(); //Deal with one line once.
                        if (line == null) break;
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
                                int firstNumber = int.Parse(line.Substring(firstNumberStart + 1, firstNumberEnd - firstNumberStart - 1));
                                int secondNumber = int.Parse(line.Substring(secondNumberStart + 1, secondNumberEnd - secondNumberStart - 1));
                                int thirdNumber = int.Parse(line.Substring(thirdNumberStart + 1, thirdNumberEnd - thirdNumberStart - 1));
                                int fourthNumber = int.Parse(line.Substring(fourthNumberStart + 1, fourthNumberEnd - fourthNumberStart - 1));

                                List<Structure.Component>? firstIdentifierList;
                                List<Structure.Component>? secondIdentifierList;



                                string? importInputFile = null;
                                string? importOutputFile = null;

                                if (Tools.stringEndWith(line.Substring(0, firstNumberStart), "input"))
                                {
                                    firstIdentifierList = ((IEnumerable<Structure.Component>)Inputs).ToList();
                                }
                                else if (Tools.stringEndWith(line.Substring(0, firstNumberStart), "and"))
                                {
                                    firstIdentifierList = ((IEnumerable<Structure.Component>)ANDs).ToList();
                                }
                                else if (Tools.stringEndWith(line.Substring(0, firstNumberStart), "xor"))
                                {
                                    firstIdentifierList = ((IEnumerable<Structure.Component>)XORs).ToList();
                                }
                                else if (Tools.stringEndWith(line.Substring(0, firstNumberStart), "or"))
                                {
                                    firstIdentifierList = ((IEnumerable<Structure.Component>)ORs).ToList();
                                }
                                else if (Tools.stringEndWith(line.Substring(0, firstNumberStart), "not"))
                                {
                                    firstIdentifierList = ((IEnumerable<Structure.Component>)NOTs).ToList();
                                }
                                else
                                {
                                    try
                                    {
                                        importInputFile = imports[Tools.GetCNCTStringFirstKeyword(line)];
                                        firstIdentifierList = null;
                                    }
                                    catch (KeyNotFoundException)
                                    {
                                        Console.WriteLine("Syntax Error: Line " + curLine + ", invalid identifier.");
                                        return false;
                                    }
                                }
                                if (Tools.stringEndWith(line.Substring(0, thirdNumberStart), "and"))
                                {
                                    secondIdentifierList = ((IEnumerable<Structure.Component>)ANDs).ToList();
                                }
                                else if (Tools.stringEndWith(line.Substring(0, thirdNumberStart), "xor"))
                                {
                                    secondIdentifierList = ((IEnumerable<Structure.Component>)XORs).ToList();
                                }
                                else if (Tools.stringEndWith(line.Substring(0, thirdNumberStart), "or"))
                                {
                                    secondIdentifierList = ((IEnumerable<Structure.Component>)ORs).ToList();
                                }
                                else if (Tools.stringEndWith(line.Substring(0, thirdNumberStart), "not"))
                                {
                                    secondIdentifierList = ((IEnumerable<Structure.Component>)NOTs).ToList();
                                }
                                else if (Tools.stringEndWith(line.Substring(0, thirdNumberStart), "output"))
                                {
                                    secondIdentifierList = ((IEnumerable<Structure.Component>)Outputs).ToList();
                                }
                                else
                                {
                                    try
                                    {
                                        importOutputFile = imports[Tools.GetCNCTStringLastKeyword(line)];
                                        secondIdentifierList = null;
                                    }
                                    catch(KeyNotFoundException)
                                    {
                                        Console.WriteLine("Syntax Error: Line " + curLine + ", invalid identifier.");
                                        return false;
                                    }
                                }
                                //firstIdentifierList.
                                Structure.Component firstComponent;
                                Structure.Component secondComponent;
                                if (firstIdentifierList != null && secondIdentifierList!=null)
                                {
                                    if (secondIdentifierList.Count <= thirdNumber)
                                    {
                                        for (int i = secondIdentifierList.Count; i <= thirdNumber; i++)
                                        {
                                            if (secondIdentifierList.GetType().GetGenericArguments()[0] == typeof(Structure.ANDComponent))
                                            {
                                                secondIdentifierList.Add(new Structure.ANDComponent());
                                            }
                                            else if (secondIdentifierList.GetType().GetGenericArguments()[0] == typeof(Structure.ORComponent))
                                            {
                                                secondIdentifierList.Add(new Structure.ORComponent());
                                            }
                                            else if (secondIdentifierList.GetType().GetGenericArguments()[0] == typeof(Structure.XORComponent))
                                            {
                                                secondIdentifierList.Add(new Structure.XORComponent());
                                            }
                                            else if (secondIdentifierList.GetType().GetGenericArguments()[0] == typeof(Structure.NOTComponent))
                                            {
                                                secondIdentifierList.Add(new Structure.NOTComponent());
                                            }
                                            else if (secondIdentifierList.GetType().GetGenericArguments()[0] == typeof(Structure.InputComponent))
                                            {
                                                secondIdentifierList.Add(new Structure.InputComponent());
                                            }
                                            else if (secondIdentifierList.GetType().GetGenericArguments()[0] == typeof(Structure.OutputComponent))
                                            {
                                                secondIdentifierList.Add(new Structure.OutputComponent());
                                            }
                                        }
                                        secondComponent = secondIdentifierList.Last();
                                    }
                                    else
                                    {
                                        secondComponent = secondIdentifierList[thirdNumber];
                                    }

                                    if (firstIdentifierList.Count <= firstNumber)
                                    {
                                        for (int i = firstIdentifierList.Count; i <= firstNumber; i++)
                                        {
                                            if (firstIdentifierList.GetType().GetGenericArguments()[0] == typeof(Structure.ANDComponent))
                                            {
                                                firstIdentifierList.Add(new Structure.ANDComponent());
                                            }
                                            else if (firstIdentifierList.GetType().GetGenericArguments()[0] == typeof(Structure.ORComponent))
                                            {
                                                firstIdentifierList.Add(new Structure.ORComponent());
                                            }
                                            else if (firstIdentifierList.GetType().GetGenericArguments()[0] == typeof(Structure.XORComponent))
                                            {
                                                firstIdentifierList.Add(new Structure.XORComponent());
                                            }
                                            else if (firstIdentifierList.GetType().GetGenericArguments()[0] == typeof(Structure.NOTComponent))
                                            {
                                                firstIdentifierList.Add(new Structure.NOTComponent());
                                            }
                                            else if (firstIdentifierList.GetType().GetGenericArguments()[0] == typeof(Structure.InputComponent))
                                            {
                                                firstIdentifierList.Add(new Structure.InputComponent());
                                            }
                                            else if (firstIdentifierList.GetType().GetGenericArguments()[0] == typeof(Structure.OutputComponent))
                                            {
                                                firstIdentifierList.Add(new Structure.OutputComponent());
                                            }
                                        }
                                        firstComponent = firstIdentifierList.Last();
                                    }
                                    else
                                    {
                                        firstComponent = firstIdentifierList[thirdNumber];
                                    }

                                    firstComponent.Output.AddOutput(secondComponent);
                                }
                                
                            }
                            catch (ArgumentNullException err)
                            {
                                //Didn't find the parameter.
                                Console.WriteLine("Syntax Error: File "+ fileName + ", Line " + curLine + ", expected an parameter.\n" + err.Message);
                                return false;
                            }
                            catch (FormatException err)
                            {
                                //Invalid parameter.
                                Console.WriteLine("Syntax Error: File " + fileName + ", Line " + curLine + ", invalid parameter.\n" + err.Message);
                                return false;
                            }
                            catch (OverflowException err)
                            {
                                //Parameter out of range.
                                Console.WriteLine("Syntax Error: File " + fileName + ", Line " + curLine + ", parameter out of range.\n" + err.Message);
                                return false;
                            }
                        }
                        else if(line.StartsWith("IMPT"))
                        {
                            if(finishedImporting)
                            {
                                Console.WriteLine("Syntax Error: File " + fileName + ", Line " + curLine + ", must import before using \"CNCT\" keyword.");
                                return false;
                            }
                            string importIdentifier = Tools.GetIMPTStringLastKeyword(line);
                            string importFileName = line.Substring(line.IndexOf("\""), line.LastIndexOf("\"") - line.IndexOf("\"") + 1);
                            if(!File.Exists(importFileName))
                            {
                                Console.WriteLine("Error: Import file \"" + importFileName + "\" doesn't exist.");
                            }
                            imports.Add(importIdentifier, importFileName);
                            IONum? IOnum = IOCount(importFileName);
                            if(IOnum == null)
                            {
                                return false;
                            }
                            importIOs.Add(importIdentifier, (IONum) IOnum);
                            List<Structure.Node> inputsList = new List<Structure.Node>();
                            importInputs.Add(importIdentifier, inputsList);
                            List<Structure.Component> outputsList = new List<Structure.Component>();
                            importOutputs.Add(importIdentifier, outputsList);
                        }
                        curLine++;
                    }
                    catch(IOException err)
                    {
                        Console.WriteLine("Error: IOError occurred while reading file: " + fileName);
                        Console.Write(err.Message);
                        return false;
                    }
                    catch(OutOfMemoryException err)
                    {
                        Console.WriteLine("Error: Out of memory while reading file: " + fileName);
                        Console.Write(err.Message);
                        return false;
                    }
                }
            }
            return true;
        }
        catch (IOException err)
        {
            Console.WriteLine("Error: IOError occurred while opening file: " + fileName);
            Console.Write(err.Message);
            return false;
        }
        catch (Exception err)
        {
            Console.WriteLine("Error: Error occurred while accessing file: " + fileName);
            Console.Write(err.Message);
            return false;
        }
    }

    struct IONum
    {
        public int NumberOfOutput;
        public int NumberOfInput;
    }

    private static IONum? IOCount(string fileName)
    {
        int curLine = 1; //Indicates the current line index.
        IONum IOnum = new IONum()
        {
            NumberOfInput = 0,
            NumberOfOutput = 0
        };
        try //Catches the exception caused by "StreamReader" and the other exceptions.
        {
            using (StreamReader sr = new StreamReader(fileName)) //Uses StreamReader to Read
            {
                while (!sr.EndOfStream) //Keep on reading until the end of file.
                {
                    try //Catches the exception caused by "ReadLine".
                    {
                        string line = sr.ReadLine(); //Deal with one line once.
                        if (line == null) break;
                        if (line.StartsWith("CNCT")) //Detects the CNCT statement (See LCL format for more).
                        {
                            //Find the first number.
                            int firstNumberStart = line.IndexOf("["); //Find the first "["
                            if (Tools.CheckSyntaxError(firstNumberStart, curLine, "[")) return null; //Log a error and stop the program if couldn't find it.
                            int firstNumberEnd = line.IndexOf("]", firstNumberStart); //Find the first "]" starting from the first "[" 
                            if (Tools.CheckSyntaxError(firstNumberEnd, curLine, "]")) return null; //Same as above.

                            //Find the third number.
                            int thirdNumberStart = line.IndexOf("[", line.IndexOf(",")); //Find the third "[" starting from the second "]"
                            if (Tools.CheckSyntaxError(thirdNumberStart, curLine, "[")) return null; //Same as above.
                            int thirdNumberEnd = line.IndexOf("]", thirdNumberStart); //Find the third "]" starting from the third "[" 
                            if (Tools.CheckSyntaxError(thirdNumberEnd, curLine, "]")) return null; //Same as above.

                            try //Catches the exception caused by "int.Parse"
                            {
                                //Parse numbers
                                int firstNumber = int.Parse(line.Substring(firstNumberStart + 1, firstNumberEnd - firstNumberStart - 1));
                                int thirdNumber = int.Parse(line.Substring(thirdNumberStart + 1, thirdNumberEnd - thirdNumberStart - 1));

                                if (Tools.stringEndWith(line.Substring(0, firstNumberStart), "input"))
                                {
                                    if(IOnum.NumberOfInput <= firstNumber)
                                    {
                                        IOnum.NumberOfInput = firstNumber + 1;
                                    }
                                }

                                if (Tools.stringEndWith(line.Substring(0, thirdNumberStart), "output"))
                                {
                                    if (IOnum.NumberOfOutput <= thirdNumber)
                                    {
                                        IOnum.NumberOfOutput = thirdNumber + 1;
                                    }
                                }
                            }
                            catch (ArgumentNullException err)
                            {
                                //Didn't find the parameter.
                                Console.WriteLine("Syntax Error: File " + fileName + ", Line " + curLine + ", expected an parameter.\n" + err.Message);
                                return null;
                            }
                            catch (FormatException err)
                            {
                                //Invalid parameter.
                                Console.WriteLine("Syntax Error: File " + fileName + ", Line " + curLine + ", invalid parameter.\n" + err.Message);
                                return null;
                            }
                            catch (OverflowException err)
                            {
                                //Parameter out of range.
                                Console.WriteLine("Syntax Error: File " + fileName + ", Line " + curLine + ", parameter out of range.\n" + err.Message);
                                return null;
                            }
                        }
                        curLine++;
                    }
                    catch (IOException err)
                    {
                        Console.WriteLine("Error: IOError occurred while reading file: " + fileName);
                        Console.Write(err.Message);
                        return null;
                    }
                    catch (OutOfMemoryException err)
                    {
                        Console.WriteLine("Error: Out of memory while reading file: " + fileName);
                        Console.Write(err.Message);
                        return null;
                    }
                }
            }
            return IOnum;
        }
        catch (IOException err)
        {
            Console.WriteLine("Error: IOError occurred while opening file: " + fileName);
            Console.Write(err.Message);
            return null;
        }
        catch (Exception err)
        {
            Console.WriteLine("Error: Error occurred while accessing file: " + fileName);
            Console.Write(err.Message);
            return null;
        }
    }

    private static bool constructCirc(string fileName)
    {
        try
        {
            Circ.CircStream file = new Circ.CircStream(Tools.StripExtension(fileName) + ".circ", Inputs.Count+1, 0);
            int InputIndex = 0;
            foreach (Structure.Node Input in Inputs)
            {
                file.xOffset = Inputs.Count + 1;
                Input.WriteSelf(file);
                file.xOffset = 0;
                file.IO(false, 2, 2 * InputIndex + 1);
                file.WireHorizontally(2 * InputIndex + 1, 2, Inputs.Count + 2 - InputIndex);
                file.WireVertically(Inputs.Count + 2 - InputIndex, 2 * InputIndex + 1, Input.Y);
                file.Wire(true, Inputs.Count + 2 - InputIndex, Input.Y, InputIndex+1);
                InputIndex++;
            }
            file.xOffset = Inputs.Count + 1;
            foreach (Structure.Component AND in ANDs)
            {
                AND.WriteSelf(file);
            }
            foreach (Structure.Component OR in ORs)
            {
                OR.WriteSelf(file);
            }
            foreach (Structure.Component XOR in XORs)
            {
                XOR.WriteSelf(file);
            }
            foreach (Structure.Component NOT in NOTs)
            {
                NOT.WriteSelf(file);
            }
            int OutputIndex = 0;
            foreach (Structure.Component Output in Outputs)
            {
                file.IO(true, Structure.Component.MaxX + Outputs.Count + 2, 2 * OutputIndex + 1);
                file.WireHorizontally(2 * OutputIndex + 1, Structure.Component.MaxX + OutputIndex + 2, Structure.Component.MaxX + Outputs.Count + 2);
                file.WireVertically(Structure.Component.MaxX + OutputIndex + 2, 2 * OutputIndex + 1, Output.inputs[0].Y);
                file.Wire(true, Structure.Component.MaxX + 1, Output.inputs[0].Y, OutputIndex + 1);
                OutputIndex++;
            }
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

