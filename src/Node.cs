using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor.src
{
    public abstract class Node : FlowLayoutPanel
    {
        public DialogueComponent data { get; private set; }

        /// <summary>
        /// Used to determine which types should be sorted before other types
        /// </summary>
        public abstract int OrderCode{get;}

        public Node(DialogueComponent component) : base()
        {
            this.data = component;
            Click += (sender, e) => Managers.Control.select(this);
        }

        public void initBase(DialogueComponent component)
        {
            this.data = component;
        }

        public abstract int CompareTo(Node n);

        public static implicit operator bool(Node node)
            => node != null;
    }
}
