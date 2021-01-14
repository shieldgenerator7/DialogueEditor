using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor.src
{
    public abstract class Node
    {
        private Vector size;
        public Vector Size => size;

        public Vector position;

        /// <summary>
        /// Used to determine which types should be sorted before other types
        /// </summary>
        public abstract int OrderCode { get; }

        public Node() : base()
        {
            Click += (sender, e) => Managers.Control.select(this);
        }

        public bool isOnScreen(Vector mapPos, Vector screenSize)
            => position.x + size.x >= mapPos.x
                && mapPos.x + screenSize.x >= position.x
                && position.y + size.y >= mapPos.y
                && mapPos.y + screenSize.y >= position.y;

        public abstract int CompareTo(Node n);

        public static implicit operator bool(Node node)
            => node != null;
    }
}
