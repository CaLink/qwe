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
            NodeTree tree = new NodeTree();

            tree.Find(10,tree.Main);
            tree.Find(5,tree.Main);
                 
            tree.Find(3,tree.Main);
            tree.Find(2,tree.Main);
            tree.Find(4,tree.Main);
            tree.Find(1,tree.Main);
                 
            tree.Find(7,tree.Main);
            tree.Find(6,tree.Main);
            tree.Find(8,tree.Main);
            tree.Find(9,tree.Main);
                 
                 
            tree.Find(15,tree.Main);
                 
            tree.Find(13,tree.Main);
            tree.Find(12,tree.Main);
            tree.Find(14,tree.Main);
            tree.Find(11,tree.Main);
                 
            tree.Find(17,tree.Main);
            tree.Find(19,tree.Main);
            tree.Find(16,tree.Main);
            tree.Find(18,tree.Main);
            tree.Find(20,tree.Main);


            /*tree.Add(10);

            tree.Add(5);

            tree.Add(3);
            tree.Add(2);
            tree.Add(4);
            tree.Add(1);

            tree.Add(7);
            tree.Add(6);
            tree.Add(8);
            tree.Add(9);


            tree.Add(15);

            tree.Add(13);
            tree.Add(12);
            tree.Add(14);
            tree.Add(11);

            tree.Add(17);
            tree.Add(19);
            tree.Add(16);
            tree.Add(18);
            tree.Add(20);
            */

            NodeTree.Print(tree.Main);
            Console.WriteLine();



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


        //private node father;

        //public node father
        //{
        //    get { return father; }
        //    set { father = value; }
        //}

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

}
