using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor.src
{
    public class NodeLabel : RichTextBox
    {
        public Quote quote;

        public string QuoteText
        {
            get =>
                (
                    (quote.characterName != "" && quote.characterName != null)
                        ? quote.characterName + ": "
                        : ""
                )
                + quote.text;
            set
            {
                if (value.Contains(":"))
                {
                    int index = value.IndexOf(':');
                    quote.characterName = value.Substring(0, index).Trim();
                    if (index < value.Length - 1)
                    {
                        quote.text = value.Substring(index + 1).Trim();
                    }
                    else
                    {
                        quote.text = "";
                    }
                }
                else
                {
                    quote.text = value;
                }
            }
        }

        private bool _editing = false;
        public bool Editing
        {
            get => _editing;
            set
            {
                _editing = value;
                if (_editing)
                {
                    BackColor = Managers.Colors.textBackColor;
                    ForeColor = Managers.Colors.textForeColor;
                    BorderStyle = BorderStyle.Fixed3D;
                    //
                    Focus();
                    SelectionStart = Text.Length;
                    SelectionLength = 0;
                }
                else
                {
                    BackColor = Managers.Colors.labelBackColor;
                    ForeColor = Managers.Colors.labelForeColor;
                    BorderStyle = BorderStyle.None;
                }
                ReadOnly = !_editing;
            }
        }

        public NodeLabel(Quote quote) : base()
        {
            this.quote = quote;

            //Set properties
            AutoSize = true;
            Font = new Font("Calibri", 12);
            MinimumSize = new Size(200, 0);
            MaximumSize = new Size(200, 0);
            Size = new Size(100, 96);
            Cursor = Cursors.Hand;
            ScrollBars = RichTextBoxScrollBars.None;
            Text = QuoteText;
            //Event Listeners
            ContentsResized += rtb_ContentsResized;
            TextChanged += acceptText;
            DoubleClick += (sender, e) => Editing = !Editing;
            //
            Editing = false;
            BringToFront();
        }

        //2020-09-21: copied from https://stackoverflow.com/a/16607756/2336212
        private void rtb_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            ((RichTextBox)sender).Height = e.NewRectangle.Height + 5;
        }

        protected void acceptText(object sender, EventArgs e)
        {
            string sentText = ((RichTextBox)sender).Text;
            if (sentText.Contains('\n'))
            {
                //Special processing,
                //Create new nodes
                string[] split = sentText.Split('\n');
                sentText = split[0];
                List<string> splitList = new List<string>(split);
                splitList.RemoveAt(0);
                split = splitList.ToArray();
                Managers.Control.receiveInfoDump(quote.path, split);
                Text = sentText;
            }
            //Normal procedure
            sentText = sentText.Trim();
            if (sentText.StartsWith(":"))
            {
                sentText = sentText.Substring(1).Trim();
            }
            this.QuoteText = sentText;
        }

        public static implicit operator bool(NodeLabel node)
            => node != null;

        public int CompareTo(NodeLabel gos)
            => this.quote.Index - gos.quote.Index;

        public static bool operator <(NodeLabel a, NodeLabel b)
            => a.quote.Index < b.quote.Index;


        public static bool operator >(NodeLabel a, NodeLabel b)
            => a.quote.Index > b.quote.Index;

    }
}
