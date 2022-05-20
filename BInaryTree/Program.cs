using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BInaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
                        NodeTree tree = new NodeTree();

                        NodeTree.Print(tree.Main);
                        Console.WriteLine();
            */

            string calc = "3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3";


            Pshe q = new Pshe();

            var t = q.ConvertToPostfixNotation(calc);

            foreach (var item in t)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();

            NodeTreePshe test = new NodeTreePshe();
            test.Create(t);
        }

    }


    class Node
    {
        private int val;
        public int Val
        {
            get => val;
            set => val = value;
        }

        private Node lSon;

        public Node LSon
        {
            get { return lSon; }
            set { lSon = value; }
        }

        private Node rSon;

        public Node RSon
        {
            get { return rSon; }
            set { rSon = value; }
        }


        public Node(int val)
        {
            this.val = val;
        }

    }

    class NodeTree
    {
        Node main;
        public Node Main
        {
            get => main;

        }




        public void Add(int value)
        {
            if (main == null)
                main = new Node(value);
            else
            {
                Node subFather = null;
                Node subMain = main;

                while (subMain != null)
                {
                    subFather = subMain;


                    if (value < subMain.Val)
                        subMain = subMain.LSon;
                    else
                        subMain = subMain.RSon;
                }

                subMain = new Node(value);

                if (value < subFather.Val)
                    subFather.LSon = subMain;
                else
                    subFather.RSon = subMain;
            }

        }

        public void Find(int val, Node cur)
        {
            if (main == null)
            {
                main = new Node(val);
                return;
            }

            if (cur.Val != val)
                if (cur.Val > val)
                    if (cur.LSon != null)
                        Find(val, cur.LSon);
                    else
                        cur.LSon = new Node(val);
                else
                    if (cur.RSon != null)
                    Find(val, cur.RSon);
                else
                    cur.RSon = new Node(val);


        }



        static public void Print(Node cur)
        {
            if (cur == null)
                return;
            Print(cur.LSon);
            Console.Write(cur.Val + " ");
            Print(cur.RSon);

        }



    }

    class NodePshe
    {
        private string val;
        public string Val
        {
            get => val;
            set => val = value;
        }

        private NodePshe lSon;

        public NodePshe LSon
        {
            get { return lSon; }
            set { lSon = value; }
        }

        private NodePshe rSon;

        public NodePshe RSon
        {
            get { return rSon; }
            set { rSon = value; }
        }


        public NodePshe(string val)
        {
            this.val = val;
        }

        public NodePshe()
        { }

    }

    class NodeTreePshe
    {
        NodePshe main;
        public NodePshe Main
        {
            get => main;

        }

        List<string> psheSTR = new List<string>();
        int item = 0;
        public void Create(List<string> pshe)
        {
            if (pshe.Count <= 0)
                return;
            psheSTR = pshe;
            item = psheSTR.Count;
            main = new NodePshe(pshe.Last());

            //NodePshe runner = Main;
            item -= 1;
            main.RSon = new NodePshe();
            Place(main.RSon);
            main = main.RSon;

            Print(main);

            //item -= 2;
            //main.LSon= new NodePshe();
            //Place(main.LSon);




        }

        void Place(NodePshe sonNode)
        {
            if (item < 0)
                return;

            int num = 0;

            if (int.TryParse(psheSTR[item], out num))
            {
                sonNode.Val = psheSTR[item];
                return;
            }
            else
            {
                sonNode.Val  = psheSTR[item];
                sonNode.RSon = new NodePshe();
                item -= 1;
                Place(sonNode.RSon);
                item -= 1;
                sonNode.LSon = new NodePshe();
                Place(sonNode.LSon);


            }
        }

        static public void Print(NodePshe cur)
        {
            if (cur == null)
                return;
            Print(cur.LSon);
            Console.Write(cur.Val + " ");
            Print(cur.RSon);

        }



    }

    class Pshe
    {


        private List<string> operators = new List<string>(new string[] { "(", ")", "+", "-", "*", "/", "^" });

        private List<string> Separate(string input)
        {

            List<string> ans = new List<string>();
            int pos = 0;
            while (pos < input.Length)
            {
                string s = input[pos].ToString();
                if (!operators.Contains(input[pos].ToString()))
                {
                    if (Char.IsDigit(input[pos]))
                        for (int i = pos + 1; i < input.Length &&
                            (Char.IsDigit(input[i]) || input[i] == ',' || input[i] == '.'); i++)
                            s += input[i];
                    else if (Char.IsLetter(input[pos]))
                        for (int i = pos + 1; i < input.Length &&
                            (Char.IsLetter(input[i]) || Char.IsDigit(input[i])); i++)
                            s += input[i];
                }
                ans.Add(s);
                pos += s.Length;
            }

            return ans;
        }
        private byte GetPriority(string s)
        {
            switch (s)
            {
                case "(":
                case ")":
                    return 0;
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                case "^":
                    return 3;
                default:
                    return 4;
            }
        }

        public List<string> ConvertToPostfixNotation(string input)
        {
            List<string> outputSeparated = new List<string>();
            Stack<string> stack = new Stack<string>();
            foreach (string c in Separate(input))
            {
                if (operators.Contains(c))
                {
                    if (stack.Count > 0 && !c.Equals("("))
                    {
                        if (c.Equals(")"))
                        {
                            string s = stack.Pop();
                            while (s != "(")
                            {
                                outputSeparated.Add(s);
                                s = stack.Pop();
                            }
                        }
                        else if (GetPriority(c) > GetPriority(stack.Peek()))
                            stack.Push(c);
                        else
                        {
                            while (stack.Count > 0 && GetPriority(c) <= GetPriority(stack.Peek()))
                            {
                                outputSeparated.Add(" ");
                                outputSeparated.Add(stack.Pop());
                            }
                            stack.Push(c);
                        }
                    }
                    else
                        stack.Push(c);
                }
                else
                    outputSeparated.Add(c);
            }
            if (stack.Count > 0)
                foreach (string c in stack)
                {
                    outputSeparated.Add(" ");
                    outputSeparated.Add(c);
                }


            int i = 0;
            while (i < outputSeparated.Count)
            {
                if (string.IsNullOrWhiteSpace(outputSeparated[i]))
                {
                    outputSeparated.RemoveAt(i);
                    continue;
                }
                i++;
            }

            return outputSeparated;
        }






    }

}
