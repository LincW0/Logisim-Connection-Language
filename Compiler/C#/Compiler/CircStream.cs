using System;
using System.IO;

namespace Circ
{
    /// <summary>
    /// <para>This is a file stream customized for .circ files.</para>
    /// <para>Allows you to use .circ format with ease.</para>
    /// <para>Always remenber to call the Close function before ending the program.</para>
    /// <para>
    /// Created date: 2022/6/14
    /// Created time: 16:43
    /// Author: ItsHealingSpell
    /// </para>
    /// </summary>
    class CircStream
    {
        private string fileName; //The circ file name.
        private StreamWriter file; //The StreamWriter object for writing the file.
        /// <summary>
        /// The constructor of CircStream.
        /// </summary>
        /// <param name="f">The file name.</param>
        /// <exception cref="Exception">The exception occurs when unable to open the file.</exception>
        public CircStream(string f)
        {
            fileName = f;
            try
            {
                file = new StreamWriter(f, false); //Overwrites the file.
            }
            catch(Exception err) //This should cover a lot of errors, but I'm too lazy, so I put "Exception".
            {
                throw new Exception("IOError when opening the file\n",err); //Mark that this error has been caught
            }

            //The circ format requires this.
            file.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>\n<project source=\"2.7.1\" version=\"1.0\">\nThis file is intended to be loaded by Logisim (http://www.cburch.com/logisim/).\n<lib desc=\"#Wiring\" name=\"0\"/>\n  <lib desc=\"#Gates\" name=\"1\"/>\n  <lib desc=\"#Plexers\" name=\"2\"/>\n  <lib desc=\"#Arithmetic\" name=\"3\"/>\n  <lib desc=\"#Memory\" name=\"4\"/>\n  <lib desc=\"#I/O\" name=\"5\"/>\n  <lib desc=\"#Base\" name=\"6\">\n    <tool name=\"Text Tool\">\n      <a name=\"text\" val=\"\"/>\n      <a name=\"font\" val=\"SansSerif plain 12\"/>\n      <a name=\"halign\" val=\"center\"/>\n      <a name=\"valign\" val=\"base\"/>\n    </tool>\n  </lib>\n  <main name=\"main\"/>\n  <options>\n    <a name=\"gateUndefined\" val=\"ignore\"/>\n    <a name=\"simlimit\" val=\"1000\"/>\n    <a name=\"simrand\" val=\"0\"/>\n  </options>\n  <mappings>\n    <tool lib=\"6\" map=\"Button2\" name=\"Menu Tool\"/>\n    <tool lib=\"6\" map=\"Button3\" name=\"Menu Tool\"/>\n    <tool lib=\"6\" map=\"Ctrl Button1\" name=\"Menu Tool\"/>\n  </mappings>\n  <toolbar>\n    <tool lib=\"6\" name=\"Poke Tool\"/>\n    <tool lib=\"6\" name=\"Edit Tool\"/>\n    <tool lib=\"6\" name=\"Text Tool\">\n      <a name=\"text\" val=\"\"/>\n      <a name=\"font\" val=\"SansSerif plain 12\"/>\n      <a name=\"halign\" val=\"center\"/>\n      <a name=\"valign\" val=\"base\"/>\n    </tool>\n    <sep/>\n    <tool lib=\"0\" name=\"Pin\">\n      <a name=\"tristate\" val=\"false\"/>\n    </tool>\n    <tool lib=\"0\" name=\"Pin\">\n      <a name=\"facing\" val=\"west\"/>\n      <a name=\"output\" val=\"true\"/>\n      <a name=\"labelloc\" val=\"east\"/>\n    </tool>\n    <tool lib=\"1\" name=\"NOT Gate\"/>\n    <tool lib=\"1\" name=\"AND Gate\"/>\n    <tool lib=\"1\" name=\"OR Gate\"/>\n  </toolbar>\n  <circuit name=\"main\">\n    <a name=\"circuit\" val=\"main\"/>\n    <a name=\"clabel\" val=\"\"/>\n    <a name=\"clabelup\" val=\"east\"/>\n    <a name=\"clabelfont\" val=\"SansSerif plain 12\"/>\n");
        }
        /// <summary>
        /// Wires two points.
        /// </summary>
        /// <param name="horizontal">
        /// <para>True if the wire is to be placed horizontally.</para>
        /// <para>False if the wire is to be placed vertically.</para>
        /// </param>
        /// <param name="x">The x coordinate of the starting point.</param>
        /// <param name="y">The y coordinate of the starting point.</param>
        /// <param name="length">The length of the wire.</param>
        public void Wire(bool horizontal, int x, int y, int length)
        {
            if (horizontal) //Split the two conditions
            {
                //This is coded according to the circ format.
                file.WriteLine("    <wire from=\"(" + x * 10 + "," + y * 10 + ")\" to=\"(" + (x * 10 + length * 10) + "," + y * 10 + ")\"/>");
            }
            else
            {
                //This is coded according to the circ format.
                file.WriteLine("    <wire from=\"(" + x * 10 + "," + y * 10 + ")\" to=\"(" + x * 10 + "," + (y * 10 + length * 10) + ")\"/>");
            }
        }
        /// <summary>
        /// <para>Place a Input/Output component.</para>
        /// </summary>
        /// <param name="output">
        /// <para>True if you are placing a output component.</para>
        /// <para>False if you are placing a input component.</para>
        /// </param>
        /// <param name="x">The x coordinate of the point that connects wires.</param>
        /// <param name="y">The y coordinate of the point that connects wires.</param>
        public void IO(bool output, int x, int y)
        {
            //This is coded according to the circ format.
            file.WriteLine("    <comp lib=\"0\" loc=\"(" + x * 10 + "," + y * 10 + ")\" name=\"Pin\">");
            file.WriteLine("      <a name=\"output\" val=\"" + (output ? "true" : "false") + "\"/>");
            file.WriteLine("      <a name=\"facing\" val=\"" + (output ? "west" : "east") + "\"/>");
            file.WriteLine("      <a name=\"tristate\" val=\"false\"/>");
            file.WriteLine("    </comp>");
        }
        /// <summary>
        /// Place a NOT Gate.
        /// See the circ format for more.
        /// </summary>
        /// <param name="x">
        /// <para>The x coordinate of the point that connects output wires.</para>
        /// <para>Please note that the x input coordinate of the gate is the param "x" minus 2</para>
        /// </param>
        /// <param name="y">
        /// <para>The y coordinate of the point that connects output wires.</para>
        /// <para>Please note that the y input coordinate of the gate is the same as the param "y"</para>
        /// </param>
        public void NOT(int x, int y)
        {
            file.WriteLine("    <comp lib=\"1\" loc=\"(" + x * 10 + "," + y * 10 + ")\" name=\"NOT Gate\">");
            file.WriteLine("      <a name=\"size\" val=\"20\"/>");
            file.WriteLine("    </comp>");
        }
        /// <summary>
        /// Place a AND Gate.
        /// See the circ format for more.
        /// </summary>
        /// <param name="x">
        /// <para>The x coordinate of the point that connects output wires.</para>
        /// <para>Please note that the x input coordinate of the gate is the param "x" minus 3</para>
        /// </param>
        /// <param name="y">
        /// <para>The y coordinate of the point that connects output wires.</para>
        /// <para>Please note that the y input coordinate of the gate is the param "y" plus or minus 1</para>
        /// </param>
        public void AND(int x, int y)
        {
            file.WriteLine("    <comp lib=\"1\" loc=\"(" + x * 10 + "," + y * 10 + ")\" name=\"AND Gate\">");
            file.WriteLine("      <a name=\"size\" val=\"30\"/>");
            file.WriteLine("      <a name=\"inputs\" val=\"2\"/>");
            file.WriteLine("    </comp>");
        }
        /// <summary>
        /// Place a XOR Gate.
        /// See the circ format for more.
        /// </summary>
        /// <param name="x">
        /// <para>The x coordinate of the point that connects output wires.</para>
        /// <para>Please note that the x input coordinate of the gate is the param "x" minus 4</para>
        /// </param>
        /// <param name="y">
        /// <para>The y coordinate of the point that connects output wires.</para>
        /// <para>Please note that the y input coordinate of the gate is the param "y" plus or minus 1</para>
        /// </param>
        public void XOR(int x, int y)
        {
            file.WriteLine("    <comp lib=\"1\" loc=\"(" + x * 10 + "," + y * 10 + ")\" name=\"XOR Gate\">");
            file.WriteLine("      <a name=\"size\" val=\"30\"/>");
            file.WriteLine("      <a name=\"inputs\" val=\"2\"/>");
            file.WriteLine("    </comp>");
        }
        /// <summary>
        /// Place a OR Gate.
        /// See the circ format for more.
        /// </summary>
        /// <param name="x">
        /// <para>The x coordinate of the point that connects output wires.</para>
        /// <para>Please note that the x input coordinate of the gate is the param "x" minus 3</para>
        /// </param>
        /// <param name="y">
        /// <para>The y coordinate of the point that connects output wires.</para>
        /// <para>Please note that the y input coordinate of the gate is the param "y" plus or minus 1</para>
        /// </param>
        public void OR(int x, int y)
        {
            file.WriteLine("    <comp lib=\"1\" loc=\"(" + x * 10 + "," + y * 10 + ")\" name=\"OR Gate\">");
            file.WriteLine("      <a name=\"size\" val=\"30\"/>");
            file.WriteLine("      <a name=\"inputs\" val=\"2\"/>");
            file.WriteLine("    </comp>");
        }
        /// <summary>
        /// <para>Closes the stream.</para>
        /// <para>Always remember to cast this function before ending the program.</para>
        /// </summary>
        public void Close()
        {
            file.Write("  </circuit>\n</project>");
            file.Close();
        }
    }
}
