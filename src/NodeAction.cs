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
            get => action.variableName + " " + action.ActionTypeString + " " + action.actionValue;
            set
            {
                string[] split = value.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                action.variableName = split[0];
                action.ActionTypeString = split[1];
                action.actionValue = int.Parse(split[2]);
                txtVariableName.text = action.variableName;
                txtActionType.text = action.ActionTypeString;
                txtActionValue.text = "" + action.actionValue;
            }
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
