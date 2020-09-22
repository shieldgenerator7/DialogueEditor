using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor.src
{
    /// <summary>
    /// Visual representation of a Quote
    /// </summary>
    public class Node
    {

        public Quote quote;
        private Vector _position;
        public virtual Vector position
        {
            get => _position;
            set
            {
                _position = value;
                if (label != null)
                {
                    label.Location = _position.toPoint();
                    if (Editing)
                    {
                        textBox.Location = _position.toPoint();
                    }
                }
            }
        }
        public Vector pickupOffset;
        public virtual Size size
        {
            get => (Editing) ? textBox.Size : label.Size;
            set
            {
                label.Size = value;
                if (Editing)
                {
                    textBox.Size = value;
                }
            }
        }

        protected static RichTextBox textBox;

        public Label label { get; protected set; }

        public virtual bool Editing => !label.Visible;

        /// <summary>
        /// Only to be called by subclasses
        /// </summary>
        protected Node() { }

        public Node(Quote quote) : this(quote, Vector.zero) { }

        public Node(Quote quote, Vector position)
        {
            this.quote = quote;
            this.position = position;
            //
            this.label = new Label();
            this.label.AutoSize = true;
            this.label.BackColor = Color.FromArgb(53, 70, 127);
            this.label.Font = new System.Drawing.Font("Calibri", 12);
            this.label.ForeColor = Color.FromArgb(240, 240, 200);
            this.label.Location = position.toPoint();
            this.label.MaximumSize = new Size(200, 0);
            this.label.MinimumSize = new Size(100, 20);
            this.label.Size = new Size(100, 28);
            this.label.Text = quote.text;
            Managers.Form.Controls.Add(this.label);
            this.label.BringToFront();
        }

        public virtual Rectangle getRect()
        {
            return new Rectangle(
                position.x,
                position.y,
                size.Width,
                size.Height);
        }

        public virtual void editNode(bool edit)
        {
            if (edit)
            {
                label.BringToFront();
                label.Hide();
                if (textBox == null)
                {
                    initTextBox();
                }
                textBox.Size = new Size(
                    200,
                    label.Size.Height + 10
                    );
                textBox.TextChanged -= acceptText;
                textBox.TextChanged += acceptText;
                textBox.Location = position.toPoint();
                textBox.Text = quote.text;
                textBox.Show();
                textBox.BringToFront();
                textBox.Focus();
                textBox.SelectionStart = textBox.Text.Length;
                textBox.SelectionLength = 0;
            }
            else
            {
                textBox.TextChanged -= acceptText;
                textBox.Hide();
                label.Show();
            }
            Managers.Form.Refresh();
        }

        protected virtual void acceptText(object sender, EventArgs e)
        {
            string text = ((RichTextBox)sender).Text;
            if (text.Contains('\n'))
            {
                //Special processing,
                //Create new nodes
                string[] split = text.Split('\n');
                text = split[0];
                List<string> splitList = new List<string>(split);
                splitList.RemoveAt(0);
                split = splitList.ToArray();
                Managers.Control.receiveInfoDump(quote.path, split);
            }
            //Normal procedure
            quote.text = text;
            label.Text = text;
            Managers.Form.Refresh();
        }

        protected void initTextBox()
        {
            textBox = new RichTextBox();
            textBox.Anchor = AnchorStyles.Left;
            textBox.AutoSize = true;
            textBox.Multiline = true;
            textBox.ScrollBars = RichTextBoxScrollBars.None;
            textBox.ForeColor = Color.FromArgb(53, 70, 127);
            textBox.Font = new System.Drawing.Font("Calibri", 12);
            textBox.BackColor = Color.FromArgb(240, 240, 200);
            textBox.MaximumSize = new Size(200, 0);
            textBox.MinimumSize = new Size(100, 20);
            textBox.ContentsResized += rtb_ContentsResized;
            //textBox.Size = new Size(100, 41);
            Managers.Form.Controls.Add(textBox);
            textBox.BringToFront();
        }

        //2020-09-21: copied from https://stackoverflow.com/a/16607756/2336212
        private void rtb_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            ((RichTextBox)sender).Height = e.NewRectangle.Height + 5;
        }

        public void pickup(Vector pickupPos)
        {
            pickupOffset = position - pickupPos;
        }

        public virtual void moveTo(Vector pos, bool useOffset = true)
        {
            if (useOffset)
            {
                position = pos + pickupOffset;
            }
            else
            {
                position = pos;
            }
        }

        public virtual void dispose()
        {
            DialoguePath path = quote.path;
            path.quotes.Remove(quote);
            Managers.Form.Controls.Remove(label);
        }

        public static implicit operator Boolean(Node gameObjectSprite)
        {
            return gameObjectSprite != null;
        }

        public virtual int CompareTo(Node gos)
        {
            float thisSize = this.size.toVector().Magnitude;
            float goSize = gos.size.toVector().Magnitude;
            return (int)(this.size.toVector().Magnitude - gos.size.toVector().Magnitude);
        }

        public static bool operator <(Node a, Node b)
        {
            float aSize = a.size.toVector().Magnitude;
            float bSize = b.size.toVector().Magnitude;
            return aSize < bSize;
        }

        public static bool operator >(Node a, Node b)
        {
            float aSize = a.size.toVector().Magnitude;
            float bSize = b.size.toVector().Magnitude;
            return aSize > bSize;
        }
    }
}
