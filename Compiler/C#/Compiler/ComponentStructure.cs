using System;

namespace Structure
{
    class Node
    {
        private List<Component> outputs; //Whose input node is this?

        public Node(Component? father)
        {
            Father = father; //Set "father".
            outputs = new List<Component>(); //Init "outputs".
            Y = MaxY + 2; //Assign y coordinate.
            MaxY = Y;
        }

        public static void InitializeNodes()
        {
            MaxY = -1; //Init the module: assign values starting from 0. 
        }

        public void AddOutput(Component destination)
        {
            if(!destination.NeedMoreInput)
            {
                throw new ArgumentException("Error: The target component already has " + destination.InputCount + " inputs!");
            }
            outputs.Add(destination);
            destination.AddInput(this);
        }

        public int Y //Y coordinate of the wire carrying this node's signal.
        {
            get; private set;
        }
        public Component? Father //Whose output node is this?
        {
            get; private set;
        }
        public List<Component> Outputs
        {
            get
            {
                return new List<Component>(outputs.ToArray());
            }
        }
        public static int MaxY //The biggest y coordinate of a wire carrying a node's signal yet.
        {
            get; private set;
        }
        //This will be used to assign a y coordinate to each object.
        public void WriteSelf(Circ.CircStream circStream)
        {
            circStream.WireHorizontally(Y, 2, Component.MaxX+1);
        }
    }

    class Component
    {
        protected List<Node> inputs;

        public Component(bool assign, bool output=true)
        {
            inputs = new List<Node>();
            if(assign)
            {
                X = MaxX + ComponentSpace;
                MaxX = X;
            }
            else
            {
                X = -1;
            }
            if(output)
            {
                Output = new Node(this);
            }
        }

        public static void InitializeComponents()
        {
            MaxX = 2; //Init the module: assign values starting from 0. 
        }

        public virtual void AddInput(Node node)
        {
            if (NeedMoreInput)
            {
                inputs.Add(node);
            }
        }

        public virtual int InputCount
        {
            get 
            {
                return inputs.Count; 
            }
        }

        public virtual bool NeedMoreInput
        {
            get
            {
                return inputs.Count < 2;
            }
        }

        public virtual int ComponentLength
        {
            get
            {
                throw new NotImplementedException("This should never be called.");
            }
        }

        public virtual int ComponentSpace
        {
            get
            {
                throw new NotImplementedException("This should never be called.");
            }
        }

        public virtual int X
        {
            get; protected set;
        }

        public virtual Node? Output
        {
            get; protected set;
        }

        public virtual int InputX
        {
            get
            {
                return X - ComponentLength;
            }
        }

        public static int MaxX
        {
            get; protected set;
        }

        public virtual void WriteSelf(Circ.CircStream circStream)
        {
            throw new NotImplementedException("This should never be called.");
        }
    }

    class ANDComponent : Component
    {
        public ANDComponent() : base(true, true) { }

        public override int ComponentSpace
        {
            get
            {
                return 9;
            }
        }
        public override int ComponentLength
        {
            get
            {
                return 4;
            }
        }

        public override void WriteSelf(Circ.CircStream circStream)
        {
            if (NeedMoreInput) throw new NotImplementedException("Error: Setup inputs before calling this method.");
            circStream.WireVertically(InputX - 3, inputs[0].Y, Node.MaxY + 2);
            circStream.WireVertically(InputX - 1, inputs[1].Y, Node.MaxY + 4);

            circStream.WireHorizontally(Node.MaxY + 2, InputX - 3, InputX);
            circStream.WireHorizontally(Node.MaxY + 4, InputX - 1, InputX);

            circStream.AND(X - 1, Node.MaxY + 3);

            circStream.WireHorizontally(Node.MaxY + 3, X - 1, X);

            circStream.WireVertically(X, Output.Y, Node.MaxY + 3);

            Output.WriteSelf(circStream);
        }
    }

    class ORComponent : Component
    {
        public ORComponent() : base(true, true) { }

        public override int ComponentSpace
        {
            get
            {
                return 9;
            }
        }
        public override int ComponentLength
        {
            get
            {
                return 4;
            }
        }

        public override void WriteSelf(Circ.CircStream circStream)
        {
            if (NeedMoreInput) throw new NotImplementedException("Error: Setup inputs before calling this method.");
            circStream.WireVertically(InputX - 3, inputs[0].Y, Node.MaxY + 2);
            circStream.WireVertically(InputX - 1, inputs[1].Y, Node.MaxY + 4);

            circStream.WireHorizontally(Node.MaxY + 2, InputX - 3, InputX);
            circStream.WireHorizontally(Node.MaxY + 4, InputX - 1, InputX);

            circStream.OR(X - 1, Node.MaxY + 3);

            circStream.WireHorizontally(Node.MaxY + 3, X - 1, X);

            circStream.WireVertically(X, Output.Y, Node.MaxY + 3);

            Output.WriteSelf(circStream);
        }
    }

    class XORComponent : Component
    {
        public XORComponent() : base(true, true) { }

        public override int ComponentSpace
        {
            get
            {
                return 10;
            }
        }
        public override int ComponentLength
        {
            get
            {
                return 5;
            }
        }

        public override void WriteSelf(Circ.CircStream circStream)
        {
            if (NeedMoreInput) throw new NotImplementedException("Error: Setup inputs before calling this method.");
            circStream.WireVertically(InputX - 3, inputs[0].Y, Node.MaxY + 2);
            circStream.WireVertically(InputX - 1, inputs[1].Y, Node.MaxY + 4);

            circStream.WireHorizontally(Node.MaxY + 2, InputX - 3, InputX);
            circStream.WireHorizontally(Node.MaxY + 4, InputX - 1, InputX);

            circStream.AND(X - 1, Node.MaxY + 3);

            circStream.WireHorizontally(Node.MaxY + 3, X - 1, X);

            circStream.WireVertically(X, Output.Y, Node.MaxY + 3);

            Output.WriteSelf(circStream);
        }
    }

    class NOTComponent : Component
    {
        public NOTComponent() : base(true, true) { }

        public override int ComponentSpace
        {
            get
            {
                return 6;
            }
        }
        public override int ComponentLength
        {
            get
            {
                return 2;
            }
        }

        public override bool NeedMoreInput
        {
            get
            {
                return InputCount < 1;
            }
        }

        public override void WriteSelf(Circ.CircStream circStream)
        {
            if (NeedMoreInput) throw new NotImplementedException("Error: Setup inputs before calling this method.");
            circStream.WireVertically(InputX - 1, inputs[0].Y, Node.MaxY + 2);
            circStream.WireHorizontally(Node.MaxY + 2, InputX - 1, InputX);

            circStream.NOT(X - 1, Node.MaxY + 2);

            circStream.WireHorizontally(Node.MaxY + 2, X - 1, X);
            circStream.WireVertically(X, Output.Y, Node.MaxY + 2);

            Output.WriteSelf(circStream);
        }
    }

    class OutputComponent : Component
    {
        public OutputComponent() : base(false, false) { }

        public override int ComponentSpace
        {
            get
            {
                throw new NotImplementedException("Error: No space is allocated to output components.");
            }
        }
        public override int ComponentLength
        {
            get
            {
                return 2;
            }
        }

        public override bool NeedMoreInput
        {
            get
            {
                return InputCount < 1;
            }
        }

        public override int InputX
        {
            get
            {
                throw new NotImplementedException("Error: No x coordinate is assigned to a output component.");
            }
        }

        public override int X
        {
            get
            {
                throw new NotImplementedException("Error: No x coordinate is assigned to a output component.");
            }
        }

        public override Node? Output
        {
            get
            {
                throw new NotImplementedException("Error: Output components doesn't have any output node.");
            }
            protected set
            {
                throw new NotImplementedException("Error: Output components doesn't have any output node.");
            }
        }

        public override void WriteSelf(Circ.CircStream circStream)
        {
            throw new NotImplementedException("Error: This method doesn't work with output components.");
        }
    }

    class InputComponent : Component
    {
        public InputComponent() : base(false, true) { }

        public override int ComponentSpace
        {
            get
            {
                throw new NotImplementedException("Error: No space is allocated to output components.");
            }
        }
        public override int ComponentLength
        {
            get
            {
                return 2;
            }
        }

        public override bool NeedMoreInput
        {
            get
            {
                throw new NotImplementedException("Error: Input components doesn't have any input node.");
            }
        }

        public override int InputCount
        {
            get
            {
                throw new NotImplementedException("Error: Input components doesn't have any input node.");
            }
        }

        public override int InputX
        {
            get
            {
                throw new NotImplementedException("Error: No x coordinate is assigned to a output component.");
            }
        }

        public override int X
        {
            get
            {
                throw new NotImplementedException("Error: No x coordinate is assigned to a input component.");
            }
        }

        public override void WriteSelf(Circ.CircStream circStream)
        {
            Output.WriteSelf(circStream);
        }
    }
}
