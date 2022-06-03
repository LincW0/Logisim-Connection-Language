#include <iostream>
namespace fundamentals
{
    void init()
    {
        std::cout<<"<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>\n<project source=\"2.7.1\" version=\"1.0\">\nThis file is intended to be loaded by Logisim (http://www.cburch.com/logisim/).\n<lib desc=\"#Wiring\" name=\"0\"/>\n  <lib desc=\"#Gates\" name=\"1\"/>\n  <lib desc=\"#Plexers\" name=\"2\"/>\n  <lib desc=\"#Arithmetic\" name=\"3\"/>\n  <lib desc=\"#Memory\" name=\"4\"/>\n  <lib desc=\"#I/O\" name=\"5\"/>\n  <lib desc=\"#Base\" name=\"6\">\n    <tool name=\"Text Tool\">\n      <a name=\"text\" val=\"\"/>\n      <a name=\"font\" val=\"SansSerif plain 12\"/>\n      <a name=\"halign\" val=\"center\"/>\n      <a name=\"valign\" val=\"base\"/>\n    </tool>\n  </lib>\n  <main name=\"main\"/>\n  <options>\n    <a name=\"gateUndefined\" val=\"ignore\"/>\n    <a name=\"simlimit\" val=\"1000\"/>\n    <a name=\"simrand\" val=\"0\"/>\n  </options>\n  <mappings>\n    <tool lib=\"6\" map=\"Button2\" name=\"Menu Tool\"/>\n    <tool lib=\"6\" map=\"Button3\" name=\"Menu Tool\"/>\n    <tool lib=\"6\" map=\"Ctrl Button1\" name=\"Menu Tool\"/>\n  </mappings>\n  <toolbar>\n    <tool lib=\"6\" name=\"Poke Tool\"/>\n    <tool lib=\"6\" name=\"Edit Tool\"/>\n    <tool lib=\"6\" name=\"Text Tool\">\n      <a name=\"text\" val=\"\"/>\n      <a name=\"font\" val=\"SansSerif plain 12\"/>\n      <a name=\"halign\" val=\"center\"/>\n      <a name=\"valign\" val=\"base\"/>\n    </tool>\n    <sep/>\n    <tool lib=\"0\" name=\"Pin\">\n      <a name=\"tristate\" val=\"false\"/>\n    </tool>\n    <tool lib=\"0\" name=\"Pin\">\n      <a name=\"facing\" val=\"west\"/>\n      <a name=\"output\" val=\"true\"/>\n      <a name=\"labelloc\" val=\"east\"/>\n    </tool>\n    <tool lib=\"1\" name=\"NOT Gate\"/>\n    <tool lib=\"1\" name=\"AND Gate\"/>\n    <tool lib=\"1\" name=\"OR Gate\"/>\n  </toolbar>\n  <circuit name=\"main\">\n    <a name=\"circuit\" val=\"main\"/>\n    <a name=\"clabel\" val=\"\"/>\n    <a name=\"clabelup\" val=\"east\"/>\n    <a name=\"clabelfont\" val=\"SansSerif plain 12\"/>"<<std::endl;
    }
    void wire(bool horizontal,int x,int y,int length)
    {
        if(horizontal)
        {
            std::cout<<"    <wire from=\"("<<x*10<<","<<y*10<<")\" to=\"("<<x*10+length*10<<","<<y*10<<")\"/>"<<std::endl;
        }
        else
        {
            std::cout<<"    <wire from=\"("<<x*10<<","<<y*10<<")\" to=\"("<<x*10<<","<<y*10+length*10<<")\"/>"<<std::endl;
        }
    }
    void IO(bool output,int x,int y)
    {
        std::cout<<"    <comp lib=\"0\" loc=\"("<<x*10<<","<<y*10<<")\" name=\"Pin\">"<<std::endl;
        std::cout<<"      <a name=\"output\" val=\""<<(output?"true":"false")<<"\"/>"<<std::endl;
        std::cout<<"      <a name=\"facing\" val=\""<<(output?"west":"east")<<"\"/>"<<std::endl;
        std::cout<<"      <a name=\"tristate\" val=\"false\"/>"<<std::endl;
        std::cout<<"    </comp>"<<std::endl;
    }
    void NOT(int x,int y)
    {
        std::cout<<"    <comp lib=\"1\" loc=\"("<<x*10<<","<<y*10<<")\" name=\"NOT Gate\">"<<std::endl;
        std::cout<<"      <a name=\"size\" val=\"20\"/>"<<std::endl;
        std::cout<<"    </comp>"<<std::endl;
    }
    void OR(int x,int y)
    {
        std::cout<<"    <comp lib=\"1\" loc=\"("<<x*10<<","<<y*10<<")\" name=\"OR Gate\">"<<std::endl;
        std::cout<<"      <a name=\"size\" val=\"30\"/>"<<std::endl;
        std::cout<<"      <a name=\"inputs\" val=\"2\"/>"<<std::endl;
        std::cout<<"    </comp>"<<std::endl;
    }
    void XOR(int x,int y)
    {
        std::cout<<"    <comp lib=\"1\" loc=\"("<<x*10<<","<<y*10<<")\" name=\"XOR Gate\">"<<std::endl;
        std::cout<<"      <a name=\"size\" val=\"30\"/>"<<std::endl;
        std::cout<<"      <a name=\"inputs\" val=\"2\"/>"<<std::endl;
        std::cout<<"    </comp>"<<std::endl;
    }
    void AND(int x,int y)
    {
        std::cout<<"    <comp lib=\"1\" loc=\"("<<x*10<<","<<y*10<<")\" name=\"AND Gate\">"<<std::endl;
        std::cout<<"      <a name=\"size\" val=\"30\"/>"<<std::endl;
        std::cout<<"      <a name=\"inputs\" val=\"2\"/>"<<std::endl;
        std::cout<<"    </comp>"<<std::endl;
    }
    void NAND(int x,int y)
    {
        std::cout<<"    <comp lib=\"1\" loc=\"("<<x*10<<","<<y*10<<")\" name=\"NAND Gate\">"<<std::endl;
        std::cout<<"      <a name=\"size\" val=\"30\"/>"<<std::endl;
        std::cout<<"      <a name=\"inputs\" val=\"2\"/>"<<std::endl;
        std::cout<<"    </comp>"<<std::endl;
    }
    void NOR(int x,int y)
    {
        std::cout<<"    <comp lib=\"1\" loc=\"("<<x*10<<","<<y*10<<")\" name=\"NOR Gate\">"<<std::endl;
        std::cout<<"      <a name=\"size\" val=\"30\"/>"<<std::endl;
        std::cout<<"      <a name=\"inputs\" val=\"2\"/>"<<std::endl;
        std::cout<<"    </comp>"<<std::endl;
    }
    void XNOR(int x,int y)
    {
        std::cout<<"    <comp lib=\"1\" loc=\"("<<x*10<<","<<y*10<<")\" name=\"XNOR Gate\">"<<std::endl;
        std::cout<<"      <a name=\"size\" val=\"30\"/>"<<std::endl;
        std::cout<<"      <a name=\"inputs\" val=\"2\"/>"<<std::endl;
        std::cout<<"    </comp>"<<std::endl;
    }
    void end()
    {
        std::cout<<"  </circuit>\n</project>"<<std::endl;
    }
}