using Utils;
class Program
{
    private static string? LCLFileName; //The LCL file name is used globally.
    private static List<Structure.Node>? Inputs;
    private static List<Structure.Component>? Outputs;
    private static List<Structure.Component>? ANDs;
    private static List<Structure.Component>? ORs;
    private static List<Structure.Component>? XORs;
    private static List<Structure.Component>? NOTs;
    static void Main(string[] args)
    {
        //if (args.Length == 0) return; //Exits if not given enough arguments
        //LCLFileName = args[0]; //Read the first argument as the file name.
        LCLFileName = "E:\\GitHub\\Logisim-Connection-Language\\Test\\04.lcl"; //For debugging

        //Parse the LCL.
        bool parseSuccess=ParseLCL();
        if (!parseSuccess) return; //Exits if its unable to parse the file.

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
        Inputs = new List<Structure.Node>();
        ANDs = new List<Structure.Component>();
        ORs = new List<Structure.Component>();
        XORs = new List<Structure.Component>();
        NOTs = new List<Structure.Component>();
        Outputs = new List<Structure.Component>();

        Structure.Node.InitializeNodes();
        Structure.Component.InitializeComponents();

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

                                Structure.ComponentType firstIdentifier;
                                Structure.ComponentType secondIdentifier;

                                if (Tools.stringEndWith(line.Substring(0, firstNumberStart), "input"))
                                {
                                    firstIdentifier = (Structure.ComponentType)(-1);
                                }
                                else if (Tools.stringEndWith(line.Substring(0, firstNumberStart), "and"))
                                {
                                    firstIdentifier = Structure.ComponentType.AND;
                                }
                                else if (Tools.stringEndWith(line.Substring(0, firstNumberStart), "xor"))
                                {
                                    firstIdentifier = Structure.ComponentType.XOR;
                                }
                                else if (Tools.stringEndWith(line.Substring(0, firstNumberStart), "or"))
                                {
                                    firstIdentifier = Structure.ComponentType.OR;
                                }
                                else if (Tools.stringEndWith(line.Substring(0, firstNumberStart), "not"))
                                {
                                    firstIdentifier = Structure.ComponentType.NOT;
                                }
                                else
                                {
                                    Console.WriteLine("Syntax Error: Line " + curLine + ", invalid identifier.");
                                    return false;
                                }

                                if (Tools.stringEndWith(line.Substring(0, thirdNumberStart), "and"))
                                {
                                    secondIdentifier = Structure.ComponentType.AND;
                                }
                                else if (Tools.stringEndWith(line.Substring(0, thirdNumberStart), "xor"))
                                {
                                    secondIdentifier = Structure.ComponentType.XOR;
                                }
                                else if (Tools.stringEndWith(line.Substring(0, thirdNumberStart), "or"))
                                {
                                    secondIdentifier = Structure.ComponentType.OR;
                                }
                                else if (Tools.stringEndWith(line.Substring(0, thirdNumberStart), "not"))
                                {
                                    secondIdentifier = Structure.ComponentType.NOT;
                                }
                                else if (Tools.stringEndWith(line.Substring(0, thirdNumberStart), "output"))
                                {
                                    secondIdentifier = Structure.ComponentType.Output;
                                }
                                else
                                {
                                    Console.WriteLine("Syntax Error: Line " + curLine + ", invalid identifier.");
                                    return false;
                                }
                                Structure.Component secondComponent;
                                List<Structure.Component> secondComponentTypeList = componentTypeToList(secondIdentifier);
                                if (secondComponentTypeList.Count <= thirdNumber)
                                {
                                    for (int i = secondComponentTypeList.Count; i < thirdNumber; i++)
                                    {
                                        secondComponentTypeList.Add(new Structure.Component(secondIdentifier));
                                    }
                                    secondComponent = new Structure.Component(secondIdentifier);
                                    secondComponentTypeList.Add(secondComponent);
                                }
                                else
                                {
                                    secondComponent = secondComponentTypeList[thirdNumber];
                                }
                                if (firstIdentifier != (Structure.ComponentType)(-1))
                                {
                                    Structure.Component firstComponent;
                                    List<Structure.Component> firstComponentTypeList = componentTypeToList(firstIdentifier);
                                    if (firstComponentTypeList.Count <= firstNumber)
                                    {
                                        for(int i = firstComponentTypeList.Count; i < firstNumber; i++)
                                        {
                                            firstComponentTypeList.Add(new Structure.Component(firstIdentifier));
                                        }
                                        firstComponent = new Structure.Component(firstIdentifier);
                                        firstComponentTypeList.Add(firstComponent);
                                    }
                                    else
                                    {
                                        firstComponent = firstComponentTypeList[firstNumber];
                                    }

                                    firstComponent.Output.AddOutput(secondComponent);
                                }
                                else
                                {
                                    Structure.Node inputNode;
                                    if (Inputs.Count <= firstNumber)
                                    {
                                        for (int i = Inputs.Count; i < firstNumber; i++)
                                        {
                                            Inputs.Add(new Structure.Node(null));
                                        }
                                        inputNode = new Structure.Node(null);
                                        Inputs.Add(inputNode);
                                    }
                                    else
                                    {
                                        inputNode = Inputs[firstNumber];
                                    }

                                    inputNode.AddOutput(secondComponent);
                                }
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

    private static List<Structure.Component> componentTypeToList(Structure.ComponentType type)
    {
        List<Structure.Component>? list = type switch
        {
             Structure.ComponentType.Output => Outputs,
             Structure.ComponentType.AND => ANDs,
             Structure.ComponentType.OR => ORs,
             Structure.ComponentType.XOR => XORs,
             Structure.ComponentType.NOT => NOTs,
             _ => null,
        };
        if (list == null) throw new ArgumentException("Error: Type not defined.");
        return list;

    }

    private static bool constructCirc()
    {
        try
        {
            Circ.CircStream file = new Circ.CircStream(Tools.StripExtension(LCLFileName) + ".circ", Inputs.Count+1, 0);
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

