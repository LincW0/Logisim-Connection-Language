#include<iostream>
namespace structure
{
	struct Connection;
	class Node
	{
		public:
			Node *out[9999];
			Connection *father;
			int i; 
			Node(Connection *fat)
			{
				this->father=fat;
				this->i=0;
			}
			Node()
			{
				this->father=NULL;
				this->i=0;
			}
			void connect(Node* cnct_to)
			{
				this->out[i]=cnct_to;//ANDs[0].in[0]
				this->i=this->i+1;
				return;
			}
	};
	struct Connection
	{
		Node *in[2],*out;
		char typ;
		Connection()
		{
			in[0]=new Node(this);
			in[1]=new Node(this);
			out=new Node(this);
		}
	};
}