using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor.src
{
    public class NodeDialogue : FlowLayoutPanel
    {
        public readonly DialoguePath path;

        public string TitleText
        {
            get => path.title;
            set
            {
                path.title = value;
                titleBox.Text = value;
            }
        }

        private TextBox titleBox;

        public NodeDialogue(DialoguePath path) : base()
        {
            //Instance variables
            this.path = path;
            //Settings
            AutoSize = true;
            AutoScroll = false;
            FlowDirection = FlowDirection.TopDown;
            Padding = new Padding(10);
            MinimumSize = new Size(
                NodeQuote.SIZE_PICTURE + NodeQuote.WIDTH_LABEL
                    + this.Padding.Left + this.Padding.Right,
                150
                );
            BackColor = Managers.Colors.platformColor;
            BorderStyle = BorderStyle.FixedSingle;
            WrapContents = false;
            Click += (sender, e) => Managers.Control.select(this);
            //Title Box
            titleBox = new TextBox();
            titleBox.Size = new Size(200, 24);
            titleBox.Font = new Font("Calibri", 12);
            titleBox.BackColor = Managers.Colors.platformColor;
            titleBox.BorderStyle = BorderStyle.None;
            titleBox.Name = "titleBox";
            titleBox.Text = TitleText;
            this.Controls.Add(this.titleBox);
            TitleText = TitleText;
            titleBox.TextChanged += acceptText;
            titleBox.Click += (sender, e) => Managers.Control.select(this);
        }

        protected virtual void acceptText(object sender, EventArgs e)
        {
            string sentText = ((TextBox)sender).Text;
            if (sentText.Contains('\n'))
            {
                sentText = sentText.Split('\n')[0];
            }
            //Normal procedure
            sentText = sentText.Trim();
            path.title = sentText;
        }

        public void AddNode(Node n)
        {
            this.SuspendLayout();
            this.Controls.Add(n);
            sortList();
            this.ResumeLayout();
        }

        private void sortList()
        {
            Control[] controlArray = new Control[this.Controls.Count];
            this.Controls.CopyTo(controlArray, 0);
            Array.Sort(
                controlArray,
                (c1, c2) =>
                {
                    //Put the non-Node controls first
                    if (!(c1 is Node))
                    {
                        return -1;
                    }
                    if (!(c2 is Node))
                    {
                        return 1;
                    }
                    //Sort the Node subtypes
                    Node n1 = (Node)c1;
                    Node n2 = (Node)c2;
                    //If they're different types,
                    if (n1.OrderCode != n2.OrderCode)
                    {
                        //Group them by type
                        return n1.OrderCode - n2.OrderCode;
                    }
                    //Sort them within a group
                    return n1.CompareTo(n2);
                }
                );
            this.Controls.Clear();
            this.Controls.AddRange(controlArray);
        }

        public bool empty()
        {
            if (Controls.Count == 0)
            {
                return true;
            }
            foreach (Control control in Controls)
            {
                if (control is NodeQuote nq)
                {
                    string text = nq.QuoteText.Trim();
                    if (text != null && text != "")
                    {
                        //not empty
                        return false;
                    }
                }
            }
            //empty
            return true;
        }

    }
}
