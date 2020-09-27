using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor.src
{
    public class Node : FlowLayoutPanel
    {
        public readonly DialogueComponent data;

        public Node(DialogueComponent component) : base()
        {
            this.data = component;
        }
    }
}
