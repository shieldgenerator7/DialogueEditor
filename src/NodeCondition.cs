using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor.src
{
    public class NodeCondition : Node
    {
        public readonly Condition condition;

        public override int OrderCode => 1;

        public NodeCondition(Condition condition) : base()
        {
            this.condition = condition;
        }


        public override int CompareTo(Node n)
           => this.condition.variableName.CompareTo(((NodeCondition)n).condition.variableName);
        public override void paint(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}
