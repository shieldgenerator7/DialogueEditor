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
            //Title Box
            titleBox = new TextBox();
            titleBox.Size = new Size(200, 24);
            titleBox.Font = new Font("Calibri", 12);
            titleBox.BackColor = Managers.Colors.platformColor;
            titleBox.BorderStyle = BorderStyle.None;
            titleBox.Name = "titleBox";
            titleBox.Text = TitleText;
            Controls.Add(this.titleBox);
            TitleText = TitleText;
            titleBox.TextChanged += acceptText;
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
    }
}
