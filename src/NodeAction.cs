using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor.src
{
    public class NodeAction : NodeComponent
    {

        public readonly Action action;
        public override DialogueComponent data => action;

        public override string QuoteText
        {
            get { return ""; }
            set { }
        }

        public override int OrderCode => 3;

        public TextDisplayable txtVariableName;
        public TextDisplayable txtActionType;
        public TextDisplayable txtActionValue;

        public NodeAction(Action action) : base()
        {
            this.action = action;

            txtVariableName = new TextDisplayable(
                action.variableName,
                3 * DisplayManager.MAX_WIDTH / 5
                );
            txtActionType = new TextDisplayable(
                action.ActionTypeString,
                DisplayManager.MAX_WIDTH / 5 - DisplayManager.BUFFER_WIDTH
                );
            txtActionValue = new TextDisplayable(
                "" + action.actionValue,
                DisplayManager.MAX_WIDTH / 5 - DisplayManager.BUFFER_WIDTH
                );
        }

        public override int CompareTo(Node n)
           => this.action.variableName.CompareTo(((NodeAction)n).action.variableName);

    }
}
