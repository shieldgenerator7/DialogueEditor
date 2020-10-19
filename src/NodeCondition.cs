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
        public const int SIZE_PICTURE = 43;
        public const int WIDTH_LABEL = 200;

        public Condition condition { get; private set; }

        private ComboBox cmbVarName;
        private ComboBox comboBox;
        private NumericUpDown numberBox;

        public override int OrderCode => 1;

        public NodeCondition(Condition condition) : base(condition)
        {

            // Panel (self) properties
            AutoSize = true;
            AutoScroll = false;
            Location = new System.Drawing.Point(3, 3);
            Size = new System.Drawing.Size(280, 80);

            // TextBox properties
            cmbVarName = new ComboBox();
            this.Controls.Add(cmbVarName);
            cmbVarName.Font = new Font("Calibri", 12);
            cmbVarName.MinimumSize = new System.Drawing.Size(150, 0);
            cmbVarName.TextChanged += acceptVariableName;
            cmbVarName.GotFocus += (sender, e) => {
                cmbVarName.TextChanged -= acceptVariableName;
                List<string> vars = Managers.Node.dialogueData.Variables;
                vars.Sort();
                cmbVarName.DataSource = vars;
                cmbVarName.SelectedItem = condition.variableName;
                cmbVarName.TextChanged += acceptVariableName;
            };
            cmbVarName.Click += (sender, e) => Managers.Control.select(this);
            // ComboBox properties
            comboBox = new ComboBox();
            this.Controls.Add(comboBox);
            comboBox.Font = new Font("Calibri", 12);
            comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox.MaximumSize = new Size(43, 0);
            comboBox.Items.AddRange(new object[] {
                "==",
                "!=",
                ">",
                ">=",
                "<",
                "<="
            });
            comboBox.SelectedIndexChanged += (sender, e) =>
            {
                condition.TestTypeString = (string)comboBox.SelectedItem;
            };
            comboBox.Click += (sender, e) => Managers.Control.select(this);
            // NumberBox properties
            numberBox = new NumericUpDown();
            this.Controls.Add(numberBox);
            numberBox.Font = new Font("Calibri", 12);
            numberBox.MaximumSize = new Size(43, 0);
            numberBox.Value = condition.testValue;
            numberBox.ValueChanged += (sender, e) =>
            {
                condition.testValue = (int)numberBox.Value;
            };
            numberBox.Click += (sender, e) => Managers.Control.select(this);
        }

        public void init(Condition condition)
        {
            base.initBase(condition);
            this.condition = condition;
            cmbVarName.Text = condition.variableName;
            comboBox.SelectedItem = condition.TestTypeString;
        }

        protected void acceptVariableName(object sender, EventArgs e)
        {
            string sentText = cmbVarName.Text;
            //2020-09-26: copied from https://stackoverflow.com/a/7316298/2336212
            string pureText = new String(sentText.Where(
                c => Char.IsLetter(c) || Char.IsNumber(c) || c == '_'
                ).ToArray());
            if (pureText != cmbVarName.Text)
            {
                cmbVarName.Text = pureText;
            }
            this.condition.variableName = sentText;
        }

        public override int CompareTo(Node n)
           => this.condition.variableName.CompareTo(((NodeCondition)n).condition.variableName);

    }
}
