using System;
using System.IO;

namespace Circ
{
    class CircStream
    {
        private string fileName;
        private StreamWriter file;
        public CircStream(string f)
        {
            fileName = f;
            file = new StreamWriter(f,false);
            file.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>\n<project source=\"2.7.1\" version=\"1.0\">\nThis file is intended to be loaded by Logisim (http://www.cburch.com/logisim/).\n<lib desc=\"#Wiring\" name=\"0\"/>\n  <lib desc=\"#Gates\" name=\"1\"/>\n  <lib desc=\"#Plexers\" name=\"2\"/>\n  <lib desc=\"#Arithmetic\" name=\"3\"/>\n  <lib desc=\"#Memory\" name=\"4\"/>\n  <lib desc=\"#I/O\" name=\"5\"/>\n  <lib desc=\"#Base\" name=\"6\">\n    <tool name=\"Text Tool\">\n      <a name=\"text\" val=\"\"/>\n      <a name=\"font\" val=\"SansSerif plain 12\"/>\n      <a name=\"halign\" val=\"center\"/>\n      <a name=\"valign\" val=\"base\"/>\n    </tool>\n  </lib>\n  <main name=\"main\"/>\n  <options>\n    <a name=\"gateUndefined\" val=\"ignore\"/>\n    <a name=\"simlimit\" val=\"1000\"/>\n    <a name=\"simrand\" val=\"0\"/>\n  </options>\n  <mappings>\n    <tool lib=\"6\" map=\"Button2\" name=\"Menu Tool\"/>\n    <tool lib=\"6\" map=\"Button3\" name=\"Menu Tool\"/>\n    <tool lib=\"6\" map=\"Ctrl Button1\" name=\"Menu Tool\"/>\n  </mappings>\n  <toolbar>\n    <tool lib=\"6\" name=\"Poke Tool\"/>\n    <tool lib=\"6\" name=\"Edit Tool\"/>\n    <tool lib=\"6\" name=\"Text Tool\">\n      <a name=\"text\" val=\"\"/>\n      <a name=\"font\" val=\"SansSerif plain 12\"/>\n      <a name=\"halign\" val=\"center\"/>\n      <a name=\"valign\" val=\"base\"/>\n    </tool>\n    <sep/>\n    <tool lib=\"0\" name=\"Pin\">\n      <a name=\"tristate\" val=\"false\"/>\n    </tool>\n    <tool lib=\"0\" name=\"Pin\">\n      <a name=\"facing\" val=\"west\"/>\n      <a name=\"output\" val=\"true\"/>\n      <a name=\"labelloc\" val=\"east\"/>\n    </tool>\n    <tool lib=\"1\" name=\"NOT Gate\"/>\n    <tool lib=\"1\" name=\"AND Gate\"/>\n    <tool lib=\"1\" name=\"OR Gate\"/>\n  </toolbar>\n  <circuit name=\"main\">\n    <a name=\"circuit\" val=\"main\"/>\n    <a name=\"clabel\" val=\"\"/>\n    <a name=\"clabelup\" val=\"east\"/>\n    <a name=\"clabelfont\" val=\"SansSerif plain 12\"/>\n");
        }

        public void Wire(bool horizontal, int x, int y, int length)
        {
            if (horizontal)
            {
                file.WriteLine("    <wire from=\"(" + x * 10 + "," + y * 10 + ")\" to=\"(" + (x * 10 + length * 10) + "," + y * 10 + ")\"/>");
            }
            else
            {
                file.WriteLine("    <wire from=\"(" + x * 10 + "," + y * 10 + ")\" to=\"(" + x * 10 + "," + (y * 10 + length * 10) + ")\"/>");
            }
        }

        public void IO(bool output, int x, int y)
        {
            file.WriteLine("    <comp lib=\"0\" loc=\"(" + x * 10 + "," + y * 10 + ")\" name=\"Pin\">");
            file.WriteLine("      <a name=\"output\" val=\"" + (output ? "true" : "false") + "\"/>");
            file.WriteLine("      <a name=\"facing\" val=\"" + (output ? "west" : "east") + "\"/>");
            file.WriteLine("      <a name=\"tristate\" val=\"false\"/>");
            file.WriteLine("    </comp>");
        }

        public void NOT(int x, int y)
        {
            file.WriteLine("    <comp lib=\"1\" loc=\"(" + x * 10 + "," + y * 10 + ")\" name=\"NOT Gate\">");
            file.WriteLine("      <a name=\"size\" val=\"20\"/>");
            file.WriteLine("    </comp>");
        }

        public void AND(int x, int y)
        {
            file.WriteLine("    <comp lib=\"1\" loc=\"(" + x * 10 + "," + y * 10 + ")\" name=\"AND Gate\">");
            file.WriteLine("      <a name=\"size\" val=\"30\"/>");
            file.WriteLine("      <a name=\"inputs\" val=\"2\"/>");
            file.WriteLine("    </comp>");
        }

        public void XOR(int x, int y)
        {
            file.WriteLine("    <comp lib=\"1\" loc=\"(" + x * 10 + "," + y * 10 + ")\" name=\"XOR Gate\">");
            file.WriteLine("      <a name=\"size\" val=\"30\"/>");
            file.WriteLine("      <a name=\"inputs\" val=\"2\"/>");
            file.WriteLine("    </comp>");
        }

        public void OR(int x, int y)
        {
            file.WriteLine("    <comp lib=\"1\" loc=\"(" + x * 10 + "," + y * 10 + ")\" name=\"OR Gate\">");
            file.WriteLine("      <a name=\"size\" val=\"30\"/>");
            file.WriteLine("      <a name=\"inputs\" val=\"2\"/>");
            file.WriteLine("    </comp>");
        }

        public void Close()
        {
            file.Write("  </circuit>\n</project>");
            file.Close();
        }

        //ublic void 
    }
}
