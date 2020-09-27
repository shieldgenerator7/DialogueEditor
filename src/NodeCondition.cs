﻿using System;
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

        public readonly Condition condition;

        private TextBox textBox;
        private ComboBox comboBox;
        private NumericUpDown numberBox;


        public NodeCondition(Condition condition) : base(condition)
        {
            this.condition = condition;

            // Panel (self) properties
            AutoSize = true;
            AutoScroll = false;
            Location = new System.Drawing.Point(3, 3);
            Size = new System.Drawing.Size(280, 80);

            // TextBox properties
            textBox = new TextBox();
            this.Controls.Add(textBox);
            textBox.Font = new Font("Calibri", 12);
            textBox.MinimumSize = new System.Drawing.Size(150, 0);
            textBox.Text = condition.variableName;
            textBox.TextChanged += acceptVariableName;
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
            comboBox.SelectedItem = condition.TestTypeString;
            comboBox.SelectedIndexChanged += (sender, e) =>
            {
                condition.TestTypeString = (string)comboBox.SelectedItem;
            };
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
        }

        protected void acceptVariableName(object sender, EventArgs e)
        {
            string sentText = textBox.Text;
            //2020-09-26: copied from https://stackoverflow.com/a/7316298/2336212
            string pureText = new String(sentText.Where(
                c => Char.IsLetter(c) || Char.IsNumber(c) || c == '_'
                ).ToArray());
            if (pureText != textBox.Text)
            {
                textBox.Text = pureText;
            }
            this.condition.variableName = sentText;
        }

    }
}
