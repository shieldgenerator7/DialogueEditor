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
        public const int SIZE_PICTURE = 43;
        public const int WIDTH_LABEL = 200;

        public readonly Action action;

        private ComboBox cmbVarName;
        private ComboBox comboBox;
        private NumericUpDown numberBox;


        public NodeAction(Action action) : base(action)
        {
            this.action = action;

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
            cmbVarName.Text = action.variableName;
            cmbVarName.TextChanged += acceptVariableName;
            cmbVarName.GotFocus += (sender, e) => cmbVarName.DataSource = Managers.Node.dialogueData.Variables;
            cmbVarName.Click += (sender, e) => Managers.Control.select(this);
            // ComboBox properties
            comboBox = new ComboBox();
            this.Controls.Add(comboBox);
            comboBox.Font = new Font("Calibri", 12);
            comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox.MaximumSize = new Size(43, 0);
            comboBox.Items.AddRange(new object[] {
                "=",
                "+=",
                "-=",
                "*=",
                "/="
            });
            comboBox.SelectedItem = action.ActionTypeString;
            comboBox.SelectedIndexChanged += (sender, e) =>
            {
                action.ActionTypeString = (string)comboBox.SelectedItem;
            };
            comboBox.Click += (sender, e) => Managers.Control.select(this);
            // NumberBox properties
            numberBox = new NumericUpDown();
            this.Controls.Add(numberBox);
            numberBox.Font = new Font("Calibri", 12);
            numberBox.MaximumSize = new Size(43, 0);
            numberBox.Value = action.actionValue;
            numberBox.ValueChanged += (sender, e) =>
            {
                action.actionValue = (int)numberBox.Value;
            };
            numberBox.Click += (sender, e) => Managers.Control.select(this);
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
            this.action.variableName = sentText;
        }

    }
}
