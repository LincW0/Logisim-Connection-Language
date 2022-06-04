#include<iostream>
namespace structure
{
	struct Connection;
	int taken,occupied;
	class Node
	{
		public:
			Node *out[9999];
			Connection *father;
			int i,ypos,xend,xstart; 
			bool output;
			Node(Connection *fat,bool ou)
			{
				this->father=fat;
				this->i=0;
				xend=0;
				xstart=-1;
				output=ou;
				for(int i=0;i<9999;++i)
				{
					out[i]=NULL;
				}
			}
			Node()
			{
				this->father=NULL;
				this->i=0;
				ypos=-1;
				xend=0;
				output=1;
				for(int i=0;i<9999;++i)
				{
					out[i]=NULL;
				}
			}
			void connect(Node* cnct_to)
			{
				if(cnct_to->output || (!output))
				{
					std::cerr<<"ERROR:Connection Error"<<std::endl;
				}
				this->out[i]=cnct_to;//ANDs[0].in[0]
				this->i=this->i+1;
				return;
			}
			void setYPos()
			{
				ypos=taken;
				taken++;
			}
			void conW(int x)
			{
				xend=((x>xend)?x:xend);
			}
	};
	struct Connection
	{
		Node *in[2],*out;
		char typ;
		int xpos;
		Connection()
		{
			in[0]=new Node(this,0);
			in[1]=new Node(this,0);
			out=new Node(this,1);
			xpos=-1;
		}
		void setXPos()
		{
			xpos=occupied;
			occupied+=4;
		}
	};
}