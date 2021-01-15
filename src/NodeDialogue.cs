using System;
using System.Collections.Generic;
using System.Drawing;

namespace DialogueEditor.src
{
    public class NodeDialogue : Node
    {
        public readonly DialoguePath path;
        public override DialoguePath Path => path;

        private List<NodeComponent> nodes = new List<NodeComponent>();
        public List<NodeComponent> Nodes => nodes;

        public string TitleText
        {
            get => path.title;
            set
            {
                path.title = value;
            }
        }

        public NodeDialogue(DialoguePath path) : base()
        {
            //Instance variables
            this.path = path;
        }


        public void AddNode(NodeComponent n)
        {
            nodes.Add(n);
            sortList();
        }

        private void sortList()
        {
            this.nodes.Sort(
                (n1, n2) =>
                {
                    //If they're different types,
                    if (n1.OrderCode != n2.OrderCode)
                    {
                        //Group them by type
                        return n1.OrderCode - n2.OrderCode;
                    }
                    //Sort them within a group
                    return n1.CompareTo(n2);
                }
                );
        }
    }
}
