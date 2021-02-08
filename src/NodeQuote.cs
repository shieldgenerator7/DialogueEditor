using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor.src
{
    public class NodeQuote : NodeComponent
    {
        public readonly Quote quote;
        public override DialogueComponent data => quote;

        private static OpenFileDialog ofdPicture;
        public Image image { get; private set; }

        public override string QuoteText
        {
            get =>
                (
                    (!String.IsNullOrEmpty(quote.characterName))
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
                textBox.text = QuoteText;
            }
        }

        public override int OrderCode => 2;
        public string SortString => "" + quote.Index;

        public TextDisplayable textBox;

        public NodeQuote(Quote quote) : base()
        {
            this.quote = quote;

            if (ofdPicture == null)
            {
                ofdPicture = new OpenFileDialog();
                ofdPicture.Filter = "PNG Files (*.png)|*.png|All files (*.*)|*.*";
                ofdPicture.Title = "Choose picture";
            }
            //
            int imageWidth = DisplayManager.portraitSize + DisplayManager.BUFFER_WIDTH;
            this.textBox = new TextDisplayable(QuoteText, DisplayManager.MAX_WIDTH - imageWidth);
        }

        private void selectPicture(object sender, EventArgs e)
        {
            //Open file dialog
            DialogResult dr = ofdPicture.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.quote.imageFileName = ofdPicture.FileName;
                refreshImage();
                Managers.Node.setDefaultImageFileName(
                    quote.characterName,
                    quote.imageFileName
                    );
            }
        }

        public void refreshImage()
        {
            if (this.quote.imageFileName != null && this.quote.imageFileName.EndsWith(".png"))
            {
                try
                {
                    image = Managers.Images.getImage(this.quote.imageFileName);
                }
                catch (FileNotFoundException fnfe)
                {
                    //do nothing
                }
            }
        }

        public override int CompareTo(Node n)
            => this.quote.Index - ((NodeQuote)n).quote.Index;

        public static bool operator <(NodeQuote a, NodeQuote b)
            => a.quote.Index < b.quote.Index;


        public static bool operator >(NodeQuote a, NodeQuote b)
            => a.quote.Index > b.quote.Index;

    }
}
