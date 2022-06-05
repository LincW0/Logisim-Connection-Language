/*compiler*/
#include <iostream>
#include "structure.h"
#include "fundamentals.h"
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
	//freopen("E:\\Chip (potato)\\ALU Design\\Independent\\Examine LCL\\test.lcl","r",stdin);
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
	string line="";
	while(getline(cin,line)){
		if(line[0]=='I'&&line[1]=='N'&&line[2]==' '){
			int bits=line.length()-4;
			int sum=0;
			for(int i = 3; i<=bits+2; i++){
				sum=sum*10+int(line[i])-48;
			}
			number_of_input=sum;
		}
		else if(line[0]=='O'&&line[1]=='U'&&line[2]=='T'&&line[3]==' '){
			int bits=line.length()-5;
			int sum=0;
			for(int i = 4; i<=bits+3; i++){
				sum=sum*10+int(line[i])-48;
			}
			number_of_output=sum;
		}
		else if(line[0]=='O'&&line[1]=='R'&&line[2]==' '){
			int bits=line.length()-4;
			int sum=0;
			for(int i = 3; i<=bits+2; i++){
				sum=sum*10+int(line[i])-48;
			}
			number_of_or=sum;
		}
		else if(line[0]=='X'&&line[1]=='O'&&line[2]=='R'&&line[3]==' '){
			int bits=line.length()-5;
			int sum=0;
			for(int i = 4; i<=bits+3; i++){
				sum=sum*10+int(line[i])-48;
			}
			number_of_xor=sum;
		}
		else if(line[0]=='A'&&line[1]=='N'&&line[2]=='D'&&line[3]==' '){
			int bits=line.length()-5;
			int sum=0;
			for(int i = 4; i<=bits+3; i++){
				sum=sum*10+int(line[i])-48;
			}
			number_of_and=sum;
		}
		else if(line[0]=='N'&&line[1]=='O'&&line[2]=='T'&&line[3]==' '){
			int bits=line.length()-5;
			int sum=0;
			for(int i = 4; i<=bits+3; i++){
				sum=sum*10+int(line[i])-48;
			}
			number_of_not=sum;
		}
		else if(line[0]=='C'&&line[1]=='N'&&line[2]=='C'&&line[3]=='T'&&line[4]==' '){
			int num1;
			int num2;//importing
			int num3;
			int num4;
			
			int bits1=line.find("]",4)-line.find("[",4)-1;
			int bits2=line.find("]",line.find("]")+1)-line.find("[",line.find("]"))-1;
			int bits3=line.find("]",line.find(","))-line.find("[",line.find(","))-1;
			int bits4=line.find("]",line.find("]",line.find(","))+1)-line.find("[",line.find("]",line.find(",")))-1;
			
			//CNCT input[0][0],output[0][0];
			
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
	}
}
void constructRelativePosition()
{
	occupied=0;
	taken=0;
	for(int i=0;i<number_of_input;++i)
	{
		//cout<<taken<<endl;
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
	/*cout<<XORs[0].in[0]<<endl;
	cout<<&INPUTs[0]<<endl;
	cout<<XORs[0].in[1]<<endl;
	cout<<&INPUTs[1]<<endl;*/
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
	//cout<<number_of_input<<endl;
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
	//cout<<XORs[0].in[0]->ypos<<" "<<XORs[0].in[1]->ypos<<endl;
	for(int i=0;i<number_of_xor;++i)
	{
		//cout<<XORs[i].in[0]->ypos<<" "<<XORs[i].in[1]->ypos<<endl;
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
