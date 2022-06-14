// See https://aka.ms/new-console-template for more information
using Utils;
class Program
{
    private static string LCLFileName;
    static void Main(string[] args)
    {
        //if (args.Length == 0) return;
        //LCLFileName = args[0];
        LCLFileName = "E:\\GitHub\\Logisim-Connection-Language\\Test\\04.lcl";
        ParseLCL();
        constructRelativePosition();
        constructCirc();
    }

    private static void ParseLCL()
    {

    }

    private static void constructRelativePosition()
    {

    }

    private static void constructCirc()
    {
        Circ.CircStream file = new Circ.CircStream(Tools.StripExtension(LCLFileName)+".circ");
        file.IO(false, 2, 1);
        file.IO(false, 2, 3);
        file.Wire(true, 2, 1, 10);
        file.Wire(true, 2, 3, 10);
        file.AND(15, 2);
        file.Wire(true, 15, 2, 1);
        file.IO(true, 16, 2);
        file.Close();
    }
}


