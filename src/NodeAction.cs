using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor.src
{
    public class NodeAction : Node
    {

        public readonly Action action;

        public override int OrderCode => 3;

        public NodeAction(Action action) : base()
        {
            this.action = action;
        }

        public override int CompareTo(Node n)
           => this.action.variableName.CompareTo(((NodeAction)n).action.variableName);

    }
}
