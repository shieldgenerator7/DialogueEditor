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
        public readonly DialogueComponent data;

        public Node(DialogueComponent component) : base()
        {
            this.data = component;
            Click += (sender, e) => Managers.Control.select(this);
        }

        public static implicit operator bool(Node node)
            => node != null;
    }
}
