using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            Managers.init(this);
        }

        void refresh()
        {
            this.Invalidate();
            pnlDisplay.Invalidate();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
        }

        //private void pnlDisplay_Click(object sender, EventArgs e)
        //{
        //    //Managers.Control.mouseDown
        //}

        private void pnlDisplay_DoubleClick(object sender, EventArgs e)
        {
            Managers.Control.mouseDoubleClick();
            refresh();
        }

        //private void pnlDisplay_MouseClick(object sender, MouseEventArgs e)
        //{

        //}

        private void pnlDisplay_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Managers.Control.mouseDoubleClick();
            refresh();
        }

        private void pnlDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            Managers.Control.mouseDown();
            refresh();
        }

        private void pnlDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            Vector mouseVector = e.Location.toVector();
            if (Managers.Control.mouseMove(mouseVector))
            {
                refresh();
            }
        }

        private void pnlDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            Managers.Control.mouseUp();
            refresh();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //2020-09-21: copied from https://stackoverflow.com/a/54960523/2336212
            if (keyData == Keys.Return)
            {
                Managers.Control.enterPressed();
                refresh();
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                Managers.Control.escapePressed();
                refresh();
                return true;
            }
            else if (keyData == Keys.Delete)
            {
                Managers.Control.deletePressed();
                refresh();
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
    }
}
