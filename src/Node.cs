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
        protected Vector _position;
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
                        textBox.Location = label.Location;
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
        protected static Node textBoxUser = null;

        protected Label label { get; private set; }
        public virtual string Text
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
                label.Text = value;
            }
        }

        public virtual bool Editing => !label.Visible;

        /// <summary>
        /// Only to be called by subclasses
        /// </summary>
        protected Node() { }


        public Node(Quote quote)
        {
            this.quote = quote;
            this.position = position;
            setupLabel();
        }

        protected virtual void setupLabel()
        {
            this.label = new NodeLabel(this);
            this.label.AutoSize = true;
            this.label.BackColor = Color.FromArgb(53, 70, 127);
            this.label.Font = new System.Drawing.Font("Calibri", 12);
            this.label.ForeColor = Color.FromArgb(240, 240, 200);
            this.label.Location = position.toPoint();
            this.label.MinimumSize = new Size(200, 0);
            this.label.MaximumSize = new Size(200, 0);
            this.label.Cursor = Cursors.Hand;
            if (quote != null)
            {
                this.label.Text = this.Text;
            }
            Managers.Form.Controls.Add(this.label);
            this.label.BringToFront();
            //Event Listeners
            label.MouseEnter += Managers.Control.setMousedOver;
            label.Click += Managers.Control.setSelected;
            label.MouseClick += Managers.Control.setSelected;
            label.DoubleClick += Managers.Control.processDoubleClick;
            label.MouseDoubleClick += Managers.Control.processDoubleClick;
        }

        public virtual Rectangle getRect()
        {
            return new Rectangle(
                position.x,
                position.y,
                size.Width,
                size.Height);
        }

        public virtual void editNode(bool edit = true)
        {
            if (edit)
            {
                //If another node is already using the textbox,
                if (textBoxUser)
                {
                    //If that node is this one,
                    if (textBoxUser == this)
                    {
                        //do nothing
                        return;
                    }
                    //If that node is not this one,
                    else
                    {
                        //Tell that node to stop using it.
                        textBoxUser.editNode(false);
                    }
                }
                textBoxUser = this;
                //
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
                if (quote != null)
                {
                    textBox.Text = this.Text;
                }
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
                if (textBoxUser == this)
                {
                    textBoxUser = null;
                }
            }
            Managers.Form.Refresh();
        }

        protected virtual void acceptText(object sender, EventArgs e)
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
            }
            //Normal procedure
            sentText = sentText.Trim();
            if (sentText.StartsWith(":"))
            {
                sentText = sentText.Substring(1).Trim();
            }
            this.Text = sentText;
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
            disposeLabel();
        }
        public void disposeLabel()
        {
            Managers.Form.Controls.Remove(label);
            label.Dispose();
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
