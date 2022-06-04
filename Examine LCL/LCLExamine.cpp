/*compiler*/
#include <bits/stdc++.h>
#include "structure.hpp"
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
int main(int argc,char *argv[]){
	if(argc==1) return 0;
	freopen(argv[1],"r",stdin);
	//freopen("E:\\Chip (potato)\\ALU Design\\Independent\\Examine LCL\\test.lcl","r",stdin);
	for(int i=0;i<1001;++i)
	{
		ANDs[i].typ='a';
		ORs[i].typ='r';
		XORs[i].typ='x';
		NOTs[i].typ='n';
		OUTPUTs[i].typ='o';
	}
	string line="";
	while(getline(cin,line)){
		cout<<line<<endl;
		if(line[0]=='I'&&line[1]=='N'&&line[1]==' '){
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
			int num2;
			int num3=int(line[line.find("]",line.find(","))+2])-48;
			
			int bits1=line.find("]",4)-line.find("[",4)-1;
			int bits2=line.find("]",line.find(","))-line.find("[",line.find(","))-1;
			int sum1=0;
			for(int i = line.find("[",4)+1; i<=bits1+line.find("[",4); i++){
				sum1=sum1*10+int(line[i])-48;
			}
			num1=sum1;
			int sum2=0;
			for(int i = line.find("[",line.find(","))+1; i<=bits2+line.find("[",line.find(",")); i++){
				sum2=sum2*10+int(line[i])-48;
			}
			num2=sum2;
			
			if(line.substr(5,5)=="input"){
				if(line.substr(line.find(",")+1,3)=="and") INPUTs[num1].connect(ANDs[num2].in[num3]);
				if(line.substr(line.find(",")+1,6)=="output") INPUTs[num1].connect(OUTPUTs[num2].in[num3]);
				if(line.substr(line.find(",")+1,2)=="or") INPUTs[num1].connect(ORs[num2].in[num3]);
				if(line.substr(line.find(",")+1,3)=="xor") INPUTs[num1].connect(XORs[num2].in[num3]);
				if(line.substr(line.find(",")+1,3)=="not") INPUTs[num1].connect(NOTs[num2].in[num3]);
			}
			if(line.substr(5,3)=="and"){
				if(line.substr(line.find(",")+1,3)=="and") ANDs[num1].out->connect(ANDs[num2].in[num3]);
				if(line.substr(line.find(",")+1,6)=="output") ANDs[num1].out->connect(OUTPUTs[num2].in[num3]);
				if(line.substr(line.find(",")+1,2)=="or") ANDs[num1].out->connect(ORs[num2].in[num3]);
				if(line.substr(line.find(",")+1,3)=="xor") ANDs[num1].out->connect(XORs[num2].in[num3]);
				if(line.substr(line.find(",")+1,3)=="not") ANDs[num1].out->connect(NOTs[num2].in[num3]);
			}
			if(line.substr(5,2)=="or"){
				if(line.substr(line.find(",")+1,3)=="and") ORs[num1].out->connect(ANDs[num2].in[num3]);
				if(line.substr(line.find(",")+1,6)=="output") ORs[num1].out->connect(OUTPUTs[num2].in[num3]);
				if(line.substr(line.find(",")+1,2)=="or") ORs[num1].out->connect(ORs[num2].in[num3]);
				if(line.substr(line.find(",")+1,3)=="xor") ORs[num1].out->connect(XORs[num2].in[num3]);
				if(line.substr(line.find(",")+1,3)=="not") ORs[num1].out->connect(NOTs[num2].in[num3]);
			}
			if(line.substr(5,3)=="xor"){
				if(line.substr(line.find(",")+1,3)=="and") XORs[num1].out->connect(ANDs[num2].in[num3]);
				if(line.substr(line.find(",")+1,6)=="output") XORs[num1].out->connect(OUTPUTs[num2].in[num3]);
				if(line.substr(line.find(",")+1,2)=="or") XORs[num1].out->connect(ORs[num2].in[num3]);
				if(line.substr(line.find(",")+1,3)=="xor") XORs[num1].out->connect(XORs[num2].in[num3]);
				if(line.substr(line.find(",")+1,3)=="not") XORs[num1].out->connect(NOTs[num2].in[num3]);
			}
			if(line.substr(5,3)=="not"){
				if(line.substr(line.find(",")+1,3)=="and") NOTs[num1].out->connect(ANDs[num2].in[num3]);
				if(line.substr(line.find(",")+1,6)=="output") NOTs[num1].out->connect(OUTPUTs[num2].in[num3]);
				if(line.substr(line.find(",")+1,2)=="or") NOTs[num1].out->connect(ORs[num2].in[num3]);
				if(line.substr(line.find(",")+1,3)=="xor") NOTs[num1].out->connect(XORs[num2].in[num3]);
				if(line.substr(line.find(",")+1,3)=="not") NOTs[num1].out->connect(NOTs[num2].in[num3]);
			}
		}
	}
	return 0;
}