/*compiler*/
#include <iostream>
#include <regex>
#include <cstring>
#include <cstdlib>
#include "structure.hpp"
#include "fundamentals.hpp"
using namespace std;
using namespace structure; 
Connection ANDs[1001];
Connection ORs[1001];
Connection XORs[1001];
Connection NOTs[1001];
Connection OUTPUTs[1001];
Node INPUTs[1001];
int number_of_input;
int number_of_not;
int number_of_or;
int number_of_xor;
int number_of_and;
int number_of_output;
int taken,xend;
void parseLCL();
void constructRelativePosition();
void constructCIRC();
int main(int argc,char *argv[]){
	if(argc==1) return 0;
	freopen(argv[1],"r",stdin);
	freopen((string(argv[1]).substr(0,string(argv[1]).find_last_of('.'))+".circ").c_str(),"w",stdout);
	//freopen("E:\\GitHub\\Logisim-Connection-Language\\Test\\02.lcl","r",stdin);
	parseLCL();
	constructRelativePosition();
	constructCIRC();
	return 0;
}
void parseLCL()
{
	for(int i=0;i<1001;++i)
	{
		ANDs[i].typ='a';
		ORs[i].typ='r';
		XORs[i].typ='x';
		NOTs[i].typ='n';
		OUTPUTs[i].typ='o';
		ANDs[i].out=new Node(&ANDs[i],1);
		ORs[i].out=new Node(&ORs[i],1);
		XORs[i].out=new Node(&XORs[i],1);
		NOTs[i].out=new Node(&NOTs[i],1);
		OUTPUTs[i].out=new Node(&OUTPUTs[i],1);
	}
	number_of_input=0;
	number_of_output=0;
	number_of_xor=0;
	number_of_and=0;
	number_of_or=0;
	number_of_not=0;
	string line="";
	while(getline(cin,line)){
		if(line[0]=='C'&&line[1]=='N'&&line[2]=='C'&&line[3]=='T'&&line[4]==' '){
			int num1;
			int num2;//importing
			int num3;
			int num4;
			regex *r;
			smatch results;
			int tmp;
			
			int bits1=line.find("]",4)-line.find("[",4)-1;
			int bits2=line.find("]",line.find("]")+1)-line.find("[",line.find("]"))-1;
			int bits3=line.find("]",line.find(","))-line.find("[",line.find(","))-1;
			int bits4=line.find("]",line.find("]",line.find(","))+1)-line.find("[",line.find("]",line.find(",")))-1;
			
			int sum1=0;
			for(int i = line.find("[",4)+1; i<=bits1+line.find("[",4); i++){
				sum1=sum1*10+int(line[i])-48;
			}
			num1=sum1;
			
			int sum2=0;
			for(int i = line.find("[",line.find("]"))+1; i<=bits2+line.find("[",line.find("]")); i++){
				sum2=sum2*10+int(line[i])-48;
			}
			num2=sum2;
			
			int sum3=0;
			for(int i = line.find("[",line.find(","))+1; i<=bits3+line.find("[",line.find(",")); i++){
				sum3=sum3*10+int(line[i])-48;
			}
			num3=sum3;
			
			int sum4=0;
			for(int i = line.find("[",line.find("]",line.find(",")))+1; i<=bits4+line.find("[",line.find("]",line.find(","))); i++){
				sum4=sum4*10+int(line[i])-48;
			}
			num4=sum4;

			r=new regex("\\band\\b");
			if(regex_search(line,results,*r))
			{
				tmp=atoi(line.substr(results.position(0)+4,line.find("]",results.position(0))-results.position(0)-4).c_str())+1;
				if(tmp>number_of_and)
				{
					number_of_and=tmp;
				}
			}
			if(results.size()==2)
			{
				tmp=atoi(line.substr(results.position(1)+4,line.find("]",results.position(1))-results.position(1)-4).c_str())+1;
				if(tmp>number_of_and)
				{
					number_of_and=tmp;
				}
			}
			delete r;

			r=new regex("\\boutput\\b");
			if(regex_search(line,results,*r))
			{
				tmp=atoi(line.substr(results.position(0)+7,line.find("]",results.position(0))-results.position(0)-7).c_str())+1;
				if(tmp>number_of_output)
				{
					number_of_output=tmp;
				}
			}
			if(results.size()==2)
			{
				tmp=atoi(line.substr(results.position(1)+7,line.find("]",results.position(1))-results.position(1)-7).c_str())+1;
				if(tmp>number_of_output)
				{
					number_of_output=tmp;
				}
			}
			delete r;

			r=new regex("\\binput\\b");
			if(regex_search(line,results,*r))
			{
				tmp=atoi(line.substr(results.position(0)+6,line.find("]",results.position(0))-results.position(0)-6).c_str())+1;
				if(tmp>number_of_input)
				{
					number_of_input=tmp;
				}
			}
			if(results.size()==2)
			{
				tmp=atoi(line.substr(results.position(1)+6,line.find("]",results.position(1))-results.position(1)-6).c_str())+1;
				if(tmp>number_of_input)
				{
					number_of_input=tmp;
				}
			}

			r=new regex("\\bxor\\b");
			if(regex_search(line,results,*r))
			{
				tmp=atoi(line.substr(results.position(0)+4,line.find("]",results.position(0))-results.position(0)-4).c_str())+1;
				if(tmp>number_of_xor)
				{
					number_of_xor=tmp;
				}
			}
			if(results.size()==2)
			{
				tmp=atoi(line.substr(results.position(1)+4,line.find("]",results.position(1))-results.position(1)-4).c_str())+1;
				if(tmp>number_of_xor)
				{
					number_of_xor=tmp;
				}
			}
			delete r;

			r=new regex("\\bor\\b");
			if(regex_search(line,results,*r))
			{
				tmp=atoi(line.substr(results.position(0)+3,line.find("]",results.position(0))-results.position(0)-3).c_str())+1;
				if(tmp>number_of_or)
				{
					number_of_or=tmp;
				}
			}
			if(results.size()==2)
			{
				tmp=atoi(line.substr(results.position(1)+3,line.find("]",results.position(1))-results.position(1)-3).c_str())+1;
				if(tmp>number_of_or)
				{
					number_of_or=tmp;
				}
			}

			r=new regex("\\bnot\\b");
			if(regex_search(line,results,*r))
			{
				tmp=atoi(line.substr(results.position(0)+4,line.find("]",results.position(0))-results.position(0)-4).c_str())+1;
				if(tmp>number_of_not)
				{
					number_of_not=tmp;
				}
			}
			if(results.size()==2)
			{
				tmp=atoi(line.substr(results.position(1)+4,line.find("]",results.position(1))-results.position(1)-4).c_str())+1;
				if(tmp>number_of_not)
				{
					number_of_not=tmp;
				}
			}
			if(line.substr(5,5)=="input"){
				if(line.substr(line.find(",")+1,3)=="and") INPUTs[num1].connect(ANDs+num3,num4);
				if(line.substr(line.find(",")+1,6)=="output") INPUTs[num1].connect(OUTPUTs+num3,num4);
				if(line.substr(line.find(",")+1,2)=="or") INPUTs[num1].connect(ORs+num3,num4);
				if(line.substr(line.find(",")+1,3)=="xor") INPUTs[num1].connect(XORs+num3,num4);
				if(line.substr(line.find(",")+1,3)=="not") INPUTs[num1].connect(NOTs+num3,num4);
			}
			if(line.substr(5,3)=="and"){
				if(line.substr(line.find(",")+1,3)=="and") ANDs[num1].out->connect(ANDs+num3,num4);
				if(line.substr(line.find(",")+1,6)=="output") ANDs[num1].out->connect(OUTPUTs+num3,num4);
				if(line.substr(line.find(",")+1,2)=="or") ANDs[num1].out->connect(ORs+num3,num4);
				if(line.substr(line.find(",")+1,3)=="xor") ANDs[num1].out->connect(XORs+num3,num4);
				if(line.substr(line.find(",")+1,3)=="not") ANDs[num1].out->connect(NOTs+num3,num4);
			}
			if(line.substr(5,2)=="or"){
				if(line.substr(line.find(",")+1,3)=="and") ORs[num1].out->connect(ANDs+num3,num4);
				if(line.substr(line.find(",")+1,6)=="output") ORs[num1].out->connect(OUTPUTs+num3,num4);
				if(line.substr(line.find(",")+1,2)=="or") ORs[num1].out->connect(ORs+num3,num4);
				if(line.substr(line.find(",")+1,3)=="xor") ORs[num1].out->connect(XORs+num3,num4);
				if(line.substr(line.find(",")+1,3)=="not") ORs[num1].out->connect(NOTs+num3,num4);
			}
			if(line.substr(5,3)=="xor"){
				if(line.substr(line.find(",")+1,3)=="and") XORs[num1].out->connect(ANDs+num3,num4);
				if(line.substr(line.find(",")+1,6)=="output") XORs[num1].out->connect(OUTPUTs+num3,num4);
				if(line.substr(line.find(",")+1,2)=="or") XORs[num1].out->connect(ORs+num3,num4);
				if(line.substr(line.find(",")+1,3)=="xor") XORs[num1].out->connect(XORs+num3,num4);
				if(line.substr(line.find(",")+1,3)=="not") XORs[num1].out->connect(NOTs+num3,num4);
			}
			if(line.substr(5,3)=="not"){
				if(line.substr(line.find(",")+1,3)=="and") NOTs[num1].out->connect(ANDs+num3,num4);
				if(line.substr(line.find(",")+1,6)=="output") NOTs[num1].out->connect(OUTPUTs+num3,num4);
				if(line.substr(line.find(",")+1,2)=="or") NOTs[num1].out->connect(ORs+num3,num4);
				if(line.substr(line.find(",")+1,3)=="xor") NOTs[num1].out->connect(XORs+num3,num4);
				if(line.substr(line.find(",")+1,3)=="not") NOTs[num1].out->connect(NOTs+num3,num4);
			}
		}
		else if(line[0]=='I'&&line[1]=='M'&&line[2]=='P'&&line[3]=='T'&&line[4]==' ')
		{
			//IMPT "C:\xxx.lcl",xxx;
			string filename="";
			string id_="";
			for(int i = line.find("\"",3)+1; i<=line.find("\"",line.find("\"",3)+1)-1; i++){
				filename+=line[i];
			}
			for(int i = line.find(",",line.find("\"",line.find("\"",3)+1)-1)+1; i<=line.find(";")-1; i++){
				id_+=line[i];
			}
		}
	}
}
void constructRelativePosition()
{
	occupied=0;
	taken=0;
	for(int i=0;i<number_of_input;++i)
	{
		INPUTs[i].setYPos(taken);
		taken++;
	}
	for(int i=0;i<number_of_and;++i)
	{
		ANDs[i].out->setYPos(taken);
		taken++;
	}
	for(int i=0;i<number_of_xor;++i)
	{
		XORs[i].out->setYPos(taken);
		taken++;
	}
	for(int i=0;i<number_of_or;++i)
	{
		ORs[i].out->setYPos(taken);
		taken++;
	}
	for(int i=0;i<number_of_not;++i)
	{
		NOTs[i].out->setYPos(taken);
		taken++;
	}
	for(int i=0;i<number_of_input;++i)
	{
		for(int j=0;j<1001;++j)
		{
			if(INPUTs[i].out[j]==NULL) break;
			if(INPUTs[i].out[j]->typ=='o') continue;
			xend=(((INPUTs[i].out[j]->xpos+5)>xend)?(INPUTs[i].out[j]->xpos+5):xend);
			if(INPUTs[i].out[j]->xpos==-1)
			{
				INPUTs[i].out[j]->setXPos();
			}
		}
	}
	for(int i=0;i<number_of_and;++i)
	{
		for(int j=0;j<1001;++j)
		{
			if(ANDs[i].out->out[j]==NULL) break;
			if(ANDs[i].out->out[j]->typ=='o') continue;
			xend=(((ANDs[i].out->out[j]->xpos+5)>xend)?(ANDs[i].out->out[j]->xpos+5):xend);
			if(ANDs[i].out->out[j]->xpos==-1)
			{
				ANDs[i].out->out[j]->setXPos();
			}
		}
	}
	for(int i=0;i<number_of_xor;++i)
	{
		for(int j=0;j<1001;++j)
		{
			if(XORs[i].out->out[j]==NULL) break;
			if(XORs[i].out->out[j]->typ=='o') continue;
			xend=(((XORs[i].out->out[j]->xpos+5)>xend)?(XORs[i].out->out[j]->xpos+5):xend);
			if(XORs[i].out->out[j]->xpos==-1)
			{
				XORs[i].out->out[j]->setXPos();
			}
		}
	}
	for(int i=0;i<number_of_or;++i)
	{
		for(int j=0;j<1001;++j)
		{
			if(ORs[i].out->out[j]==NULL) break;
			if(ORs[i].out->out[j]->typ=='o') continue;
			xend=(((ORs[i].out->out[j]->xpos+5)>xend)?(ORs[i].out->out[j]->xpos+5):xend);
			if(ORs[i].out->out[j]->xpos==-1)
			{
				ORs[i].out->out[j]->setXPos();
			}
		}
	}
	for(int i=0;i<number_of_not;++i)
	{
		for(int j=0;j<1001;++j)
		{
			if(NOTs[i].out->out[j]==NULL) break;
			if(NOTs[i].out->out[j]->typ=='o') continue;
			xend=(((NOTs[i].out->out[j]->xpos+5)>xend)?(NOTs[i].out->out[j]->xpos+5):xend);
			if(NOTs[i].out->out[j]->xpos==-1)
			{
				NOTs[i].out->out[j]->setXPos();
			}
		}
	}
}
void constructCIRC()
{
	fundamentals::init();
	for(int i=0;i<number_of_input;++i)
	{
		fundamentals::IO(false,2,1+i*2);
		fundamentals::wire(true,2,1+i*2,xend*2-1);
	}
	for(int i=0;i<number_of_and;++i)
	{
		fundamentals::wire(true,2,1+ANDs[i].out->ypos*2,xend*2-1);
		fundamentals::wire(false,3+ANDs[i].xpos*2,1+ANDs[i].in[0]->ypos*2,taken*2-ANDs[i].in[0]->ypos*2);
		fundamentals::wire(false,5+ANDs[i].xpos*2,1+ANDs[i].in[1]->ypos*2,taken*2-ANDs[i].in[1]->ypos*2+2);
		fundamentals::wire(false,10+ANDs[i].xpos*2,1+ANDs[i].out->ypos*2,taken*2-ANDs[i].out->ypos*2+1);
		fundamentals::AND(9+ANDs[i].xpos*2,taken*2+2);
		fundamentals::wire(true,3+ANDs[i].xpos*2,taken*2+1,3);
		fundamentals::wire(true,5+ANDs[i].xpos*2,taken*2+3,1);
		fundamentals::wire(true,9+ANDs[i].xpos*2,taken*2+2,1);
	}
	for(int i=0;i<number_of_or;++i)
	{
		fundamentals::wire(true,2,1+ORs[i].out->ypos*2,xend*2-1);
		fundamentals::wire(false,3+ORs[i].xpos*2,1+ORs[i].in[0]->ypos*2,taken*2-ORs[i].in[0]->ypos*2);
		fundamentals::wire(false,5+ORs[i].xpos*2,1+ORs[i].in[1]->ypos*2,taken*2-ORs[i].in[1]->ypos*2+2);
		fundamentals::wire(false,10+ORs[i].xpos*2,1+ORs[i].out->ypos*2,taken*2-ORs[i].out->ypos*2+1);
		fundamentals::OR(9+ORs[i].xpos*2,taken*2+2);
		fundamentals::wire(true,3+ORs[i].xpos*2,taken*2+1,3);
		fundamentals::wire(true,5+ORs[i].xpos*2,taken*2+3,1);
		fundamentals::wire(true,9+ORs[i].xpos*2,taken*2+2,1);
	}
	for(int i=0;i<number_of_xor;++i)
	{
		fundamentals::wire(true,2,1+XORs[i].out->ypos*2,xend*2-1);
		fundamentals::wire(false,3+XORs[i].xpos*2,1+XORs[i].in[0]->ypos*2,taken*2-XORs[i].in[0]->ypos*2);
		fundamentals::wire(false,5+XORs[i].xpos*2,1+XORs[i].in[1]->ypos*2,taken*2-XORs[i].in[1]->ypos*2+2);
		fundamentals::wire(false,10+XORs[i].xpos*2,1+XORs[i].out->ypos*2,taken*2-XORs[i].out->ypos*2+1);
		fundamentals::XOR(10+XORs[i].xpos*2,taken*2+2);
		fundamentals::wire(true,3+XORs[i].xpos*2,taken*2+1,3);
		fundamentals::wire(true,5+XORs[i].xpos*2,taken*2+3,1);
	}
	for(int i=0;i<number_of_not;++i)
	{
		fundamentals::wire(true,2,1+NOTs[i].out->ypos*2,xend*2-1);
		fundamentals::wire(false,3+NOTs[i].xpos*2,1+NOTs[i].in[0]->ypos*2,taken*2-NOTs[i].in[0]->ypos*2);
		fundamentals::wire(false,8+NOTs[i].xpos*2,1+NOTs[i].out->ypos*2,taken*2-NOTs[i].out->ypos*2);
		fundamentals::NOT(8+NOTs[i].xpos*2,taken*2+1);
		fundamentals::wire(true,3+NOTs[i].xpos*2,taken*2+1,3);
	}
	for(int i=0;i<number_of_output;++i)
	{
		fundamentals::wire(true,xend*2+1,OUTPUTs[i].in[0]->ypos*2+1,1+i);
		fundamentals::wire(false,xend*2+2+i,i*2+1,OUTPUTs[i].in[0]->ypos*2-i*2);
		fundamentals::wire(true,xend*2+2+i,i*2+1,number_of_output-i);
		fundamentals::IO(true,xend*2+2+number_of_output,i*2+1);
	}
	fundamentals::end();
}
