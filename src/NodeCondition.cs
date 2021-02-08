using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor.src
{
    public class NodeCondition : NodeComponent
    {
        public readonly Condition condition;
        public override DialogueComponent data => condition;

        public override string QuoteText
        {
            get { return ""; }
            set { }
        }

        public override int OrderCode => 1;

        public TextDisplayable txtVariableName;
        public TextDisplayable txtTestType;
        public TextDisplayable txtTestValue;

        public NodeCondition(Condition condition) : base()
        {
            this.condition = condition;

            txtVariableName = new TextDisplayable(
                condition.variableName,
                3 * DisplayManager.MAX_WIDTH / 5
                );
            txtTestType = new TextDisplayable(
                condition.TestTypeString,
                DisplayManager.MAX_WIDTH / 5 - DisplayManager.BUFFER_WIDTH
                );
            txtTestValue = new TextDisplayable(
                "" + condition.testValue,
                DisplayManager.MAX_WIDTH / 5 - DisplayManager.BUFFER_WIDTH
                );
        }


        public override int CompareTo(Node n)
           => this.condition.variableName.CompareTo(((NodeCondition)n).condition.variableName);
    }
}
