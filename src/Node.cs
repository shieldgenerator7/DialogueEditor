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
        protected Vector size;
        public Vector Size { get => size; set => size = value; }

        public Vector position;

        /// <summary>
        /// Used to determine which types should be sorted before other types
        /// </summary>
        public abstract int OrderCode { get; }

        public Node() : base()
        {
        }

        public abstract int CompareTo(Node n);

        public static implicit operator bool(Node node)
            => node != null;
    }
}
