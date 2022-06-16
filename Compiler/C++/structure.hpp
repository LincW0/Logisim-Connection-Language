#include<iostream>
namespace structure
{
	class Node;
	int occupied;
	struct Connection
	{
		Node *in[2],*out;
		char typ;
		int xpos;
		Connection()
		{
			in[0]=NULL;
			in[1]=NULL;
			out=NULL;
			xpos=-1;
		}
		void setXPos()
		{
			xpos=occupied;
			occupied+=4;
		}
	};
	class Node
	{
		public:
			Connection *out[999];
			Connection *father;
			int i,ii,ypos,xend,xstart; 
			bool output;
			Node(Connection *fat,bool ou)
			{
				this->father=fat;
				this->i=0;
				//this->ii=0;
				//xend=0;
				//xstart=-1;
				output=ou;
				for(int j=0;j<999;++j)
				{
					out[j]=NULL;
				}
			}
			Node()
			{
				this->father=NULL;
				this->i=0;
				//this->ii=0;
				ypos=-1;
				//xend=0;
				output=1;
				for(int j=0;j<999;++j)
				{
					out[j]=NULL;
				}
			}
			void connect(Connection* cnct_to,int which)
			{
				this->out[i]=cnct_to;//ANDs[0].in[0]
				this->i=this->i+1;
				cnct_to->in[which]=this;
				return;
			}
			void setYPos(int t)
			{
				ypos=t;
			}
	};
}