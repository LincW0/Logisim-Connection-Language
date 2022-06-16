using System;

namespace Structure
{
    class Node
    {
        private Component? father; //Whose output node is this?
        private List<Component> outputs; //Whose input node is this?
        private int yCoordinate; //Y coordinate of the wire carrying this node's signal.
        private static int currentMaxY; //The biggest y coordinate of a wire carrying a node's signal yet.
        //This will be used to assign a y coordinate to each object.

        public Node(Component? father)
        {
            this.father = father; //Set "father".
            outputs = new List<Component>(); //Init "outputs".
            yCoordinate = currentMaxY + 2; //Assign y coordinate.
            currentMaxY = yCoordinate;
        }

        public static void InitializeNodes()
        {
            currentMaxY = -1; //Init the module: assign values starting from 0. 
        }

        public void AddOutput(Component destination)
        {
            if(destination.inputs.Count >= 2 || (destination.type == ComponentType.Output && destination.inputs.Count >= 1))
            {
                throw new ArgumentException("Error: The target component already has " + destination.inputs.Count + " inputs!");
            }
            outputs.Add(destination);
            destination.inputs.Add(this);
        }

        public int Y
        {
            get
            {
                return yCoordinate;
            }
        }
        public Component? Father
        {
            get
            {
                return father;
            }
        }
        public List<Component> Outputs
        {
            get
            {
                return new List<Component>(outputs.ToArray());
            }
        }
        public static int MaxY
        {
            get
            {
                return currentMaxY;
            }
        }
        public void WriteSelf(Circ.CircStream circStream)
        {
            circStream.WireHorizontally(yCoordinate, 2, Component.MaxX+1);
        }
    }
    public enum ComponentType
    {
        Output,
        AND,
        OR,
        XOR,
        NOT,
    }
    class Component
    {
        public List<Node> inputs;
        private Node? output;
        private int xCoordinate; 
        private static int currentMaxX; 
        public ComponentType type;

        public Component(ComponentType type)
        {
            inputs = new List<Node>();
            this.type = type;
            if(this.type != ComponentType.Output)
            {
                xCoordinate = currentMaxX + ComponentSpace;
                currentMaxX = xCoordinate;
                output = new Node(this);
            }
            else
            {
                xCoordinate = -1;
                output = null;
            }
        }

        public static void InitializeComponents()
        {
            currentMaxX = 2; //Init the module: assign values starting from 0. 
        }

        public int ComponentLength
        {
            get
            {
                int length = type switch
                {
                    ComponentType.Output => 2,
                    ComponentType.AND => 4,
                    ComponentType.OR => 4,
                    ComponentType.XOR => 5,
                    ComponentType.NOT => 2,
                    _ => -1,
                };
                if (length == -1) throw new ArgumentException("Error: Type not defined.");
                return length;
            }
        }

        public int ComponentSpace
        {
            get
            {
                int length = type switch
                {
                    ComponentType.Output => 0,
                    ComponentType.AND => 9,
                    ComponentType.OR => 9,
                    ComponentType.XOR => 10,
                    ComponentType.NOT => 6,
                    _ => -1,
                };
                if (length == 0) throw new NotImplementedException("Error: No space is allocated to output components.");
                if (length == -1) throw new ArgumentException("Error: Type not defined.");
                return length;
            }
        }

        public int X
        {
            get
            {
                if (type == ComponentType.Output) throw new NotImplementedException("Error: No coordinate is assigned to a output component.");
                return xCoordinate;
            }
        }

        public Node Output
        {
            get
            {
                if (type == ComponentType.Output || output == null) throw new NotImplementedException("Error: Output components doesn't have a output node.");
                return output;
            }
        }

        public ComponentType Type
        {
            get
            {
                return type;
            }
        }

        public int InputX
        {
            get
            {
                if (type == ComponentType.Output) return currentMaxX + 1;
                return xCoordinate - ComponentLength;
            }
        }

        public static int MaxX
        {
            get
            {
                return currentMaxX;
            }
        }

        public void WriteSelf(Circ.CircStream circStream)
        {
            if (type == ComponentType.Output || output == null) throw new NotImplementedException("Error: This method doesn't work with output components.");
            if (inputs.Count == 0 || (inputs.Count != 2 && type != ComponentType.NOT)) throw new NotImplementedException("Error: Setup inputs before calling this method.");
            if (type != ComponentType.NOT)
            {
                circStream.WireVertically(InputX - 3, inputs[0].Y, Node.MaxY + 2);
                circStream.WireVertically(InputX - 1, inputs[1].Y, Node.MaxY + 4);

                circStream.WireHorizontally(Node.MaxY + 2, InputX - 3, InputX);
                circStream.WireHorizontally(Node.MaxY + 4, InputX - 1, InputX);

                switch (type)
                { 
                    case ComponentType.AND:
                        circStream.AND(X - 1, Node.MaxY + 3);
                        break;
                    case ComponentType.OR:
                        circStream.OR(X - 1, Node.MaxY + 3);
                        break;
                    case ComponentType.XOR:
                        circStream.XOR(X - 1, Node.MaxY + 3);
                        break;
                }

                circStream.WireHorizontally(Node.MaxY + 3, X - 1, X);

                circStream.WireVertically(X, output.Y, Node.MaxY + 3);
            }
            else
            {
                circStream.WireVertically(InputX - 1, inputs[0].Y, Node.MaxY + 2);
                circStream.WireHorizontally(Node.MaxY + 2, InputX - 1, InputX);

                circStream.NOT(X - 1, Node.MaxY + 2);

                circStream.WireHorizontally(Node.MaxY + 2, X - 1, X);
                circStream.WireVertically(X, output.Y, Node.MaxY + 2);
            }
            Output.WriteSelf(circStream);
        }
    }
}
