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
        public Vector position;
        public Vector pickupOffset;
        public Size size = new Size(100, 20);

        private static TextBox textBox;

        public Node(Quote quote) : this(quote, Vector.zero) { }

        public Node(Quote quote, Vector position)
        {
            this.quote = quote;
            this.position = position;
        }

        public virtual Rectangle getRect()
        {
            return new Rectangle(
                position.x,
                position.y,
                size.Width,
                size.Height);
        }

        public void editNode(bool edit)
        {
            if (edit)
            {
                if (textBox == null)
                {
                    textBox = new TextBox();
                    textBox.Anchor = AnchorStyles.Left;
                    textBox.Size = new System.Drawing.Size(100, 31);
                    Managers.Form.Controls.Add(textBox);
                    textBox.BringToFront();
                }
                textBox.TextChanged -= acceptText;
                textBox.TextChanged += acceptText;
                textBox.Location = position.toPoint();
                textBox.Text = quote.text;
                textBox.Focus();
            }
            else
            {
                textBox.TextChanged -= acceptText;
                Managers.Form.Controls.Remove(textBox);
            }
            Managers.Form.Refresh();
        }

        private void acceptText(object sender, EventArgs e)
        {
            quote.text = ((TextBox)sender).Text;
            Managers.Form.Refresh();
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
