using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor
{
    public class FileManager
    {

        public string filename = null;

        private SaveFileDialog sfd;

        public FileManager()
        {
            sfd = new SaveFileDialog();
            sfd.Filter = "Dialogue JSON Files (*.json)|*.json|All files|*.*";
            sfd.FileName = filename;
            sfd.Title = "Save Dialogue";
            sfd.DefaultExt = ".json";
        }

        public void saveFile()
        {
            if (filename == null)
            {
                sfd.FileName = filename;
                DialogResult dr = sfd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    MessageBox.Show("Here's where the file would be saved.");
                    //2020-09-22: copied from https://stackoverflow.com/a/16921677/2336212
                    using (StreamWriter file = new StreamWriter(sfd.OpenFile()))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        //serialize object directly into file stream
                        serializer.Serialize(file, Managers.Node.dialogues);
                    }
                }
            }
        }

        public void openFile()
        {

        }

    }
}
