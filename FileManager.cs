using DialogueEditor.src;
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
        private OpenFileDialog ofd;

        public FileManager()
        {
            //Save Dialog
            sfd = new SaveFileDialog();
            sfd.Filter = "Dialogue JSON Files (*.json)|*.json|All files|*.*";
            sfd.FileName = filename;
            sfd.Title = "Save Dialogue";
            sfd.DefaultExt = ".json";
            //Open Dialog
            ofd = new OpenFileDialog();
            ofd.Filter = "Dialogue JSON Files (*.json)|*.json|All files|*.*";
            ofd.FileName = filename;
            ofd.Title = "Open Dialogue";
            ofd.DefaultExt = ".json";
        }

        public void saveFile()
        {
            if (filename == null)
            {
                sfd.FileName = filename;
                DialogResult dr = sfd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    //2020-09-22: copied from https://stackoverflow.com/a/16921677/2336212
                    using (StreamWriter file = new StreamWriter(sfd.OpenFile()))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        //serialize object directly into file stream
                        serializer.Serialize(file, Managers.Node.dialogueData);
                    }
                }
            }
        }

        public void openFile()
        {
            ofd.FileName = filename;
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //2020-09-22: copied from https://stackoverflow.com/a/13297964/2336212
                using (StreamReader r = new StreamReader(ofd.OpenFile()))
                {
                    string json = r.ReadToEnd();
                    try
                    {
                        //Open file from version 0.0.3 onward
                        DialogueData dialogueData = JsonConvert.DeserializeObject<DialogueData>(json);
                        Managers.Node.acceptInfoFromFile(dialogueData);
                    }
                    catch (JsonSerializationException jse)
                    {
                        //Open file from version 0.0.2
                        List<DialoguePath> dialogues = JsonConvert.DeserializeObject<List<DialoguePath>>(json);
                        Managers.Node.acceptInfoFromFile(new DialogueData(dialogues));
                    }
                }
            }
        }

        public void newFile()
        {
            Managers.Node.clear();
            Managers.Control.createQuote();
        }

    }
}
