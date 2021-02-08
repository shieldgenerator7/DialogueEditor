using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor.src
{
    public abstract class Node
    {
        public Vector size;
        public Vector position;

        public abstract DialoguePath Path { get; }

        public Node() : base()
        {
        }

        public bool overlap(Vector pos)
            => pos.x >= position.x && pos.x <= position.x + size.x
            && pos.y >= position.y && pos.y <= position.y + size.y;

        public static implicit operator bool(Node node)
            => node != null;
    }
}
