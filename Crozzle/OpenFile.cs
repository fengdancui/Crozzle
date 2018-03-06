using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crozzle
{
    /// <summary>
    /// open file
    /// </summary>
    class OpenFile
    {
        public bool file_open;
        public string new_file;
        public OpenFile()
        {
            OpenFileDialog open = new OpenFileDialog();//Define OpenFileDialog
            open.Title = "Open Folders";//Dialog Title
            open.Filter = "File（.txt）|*.txt";//File extension
            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    file_open = true;
                    new_file = open.FileName;
                    MessageBox.Show(new_file, "selected reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    Console.Write(e);
                }
            }
        }
    }
}
