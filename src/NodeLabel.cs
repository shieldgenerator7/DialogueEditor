using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor.src
{
    class NodeLabel:Label
    {
        public Node node;

        public NodeLabel(Node node)
        {
            this.node = node;
        }
    }
}
