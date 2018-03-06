using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crozzle
{
    /// <summary>
    /// save crozzle to a file
    /// </summary>
    class SaveCro
    {
        /// <summary>
        /// write in
        /// </summary>
        /// <param name="first"></param>
        /// <param name="final"></param>
        /// <param name="word_list"></param>
        /// <param name="path"></param>
        /// <param name="name"></param>
        public void write_txt(string first, List<List<string>> final, string[] word_list, string path, string name)
        {
            DirectoryInfo di = new DirectoryInfo(string.Format(@"{0}..\..\..\", path));
            string filename = di.FullName + @"Saved Crozzle\" + name + @".txt";
            string cont = "";
            try
            {
                //如果文件不存在则创建该文件 
                if (File.Exists(filename))
                {
                    FileStream myFss = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    StreamReader r = new StreamReader(myFss);
                    cont = r.ReadToEnd();
                    r.Close();
                    myFss.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                FileStream myFs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamWriter w = new StreamWriter(myFs);
                w.WriteLine(first);
                for (int i = 0; i < word_list.Length; i++)
                {
                    w.Write(word_list[i]);
                    if (i < word_list.Length - 1)
                    {
                        w.Write(",");
                    }
                }
                foreach (List<string> word_1d in final)
                {
                    for (int i = 0; i < word_1d.Count; i++)
                    {
                        w.Write(word_1d[i]);
                        if (i < word_1d.Count - 1)
                        {
                            w.Write(",");
                        }
                    }
                    w.Write('\n');
                }
                
                w.Close();
                myFs.Close();
            }
        }
        /// <summary>
        /// caculate the number of horizontal and vertical
        /// </summary>
        /// <param name="final_word"></param>
        /// <returns></returns>
        public string cacu_hv(List<List<string>> final_word)
        {
            Define define=new Define();
            int h_num = 0;
            int v_num = 0;
            foreach (List<string> word_2d in final_word)
            {
                if (word_2d[0] == define.Direction_h)
                {
                    h_num++;
                }
                else
                {
                    v_num++;
                }    
            }
            string nv_num = Convert.ToString(h_num) + "," + Convert.ToString(v_num);
            return nv_num;
        }
    }
}
