using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle
{
    class LogFile
    {
        /// <summary>
        /// create log file
        /// </summary>
        /// <param name="message">the message of log</param>
        /// <param name="title">the title of log</param>
        /// <returns></returns>
        private string ToFileNmae;//Storage path of log file
        
        public void Log(string message, string title)
        {
            string path = this.ToFileNmae;
            string filename = path + "Log.txt";
            string cont = "";
            FileInfo fileInf = new FileInfo(filename);
            if (File.Exists(filename))  //if the file exists
            {
                FileStream myFss = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamReader r = new StreamReader(myFss);
                cont = r.ReadToEnd();
                r.Close();
                myFss.Close();
            }
            #region Generate log file
            FileStream myFs = new FileStream(filename, FileMode.Create,FileAccess.ReadWrite,FileShare.ReadWrite);
            StreamWriter n = new StreamWriter(myFs);
            n.WriteLine(cont);
            n.WriteLine("-------------------------------Begin---------------------------------------------");
            n.WriteLine("*****" + title + "*****");
            n.WriteLine("Time：" + DateTime.Now.ToString());
            n.WriteLine("Message：" + message);
            n.WriteLine("-------------------------------end---------------------------------------------");
            n.Close();
            myFs.Close();
            if (fileInf.Length >= 1024 * 1024 * 200)
            {
                string NewName = path + "Log" + time() + ".txt";
                File.Move(filename, NewName);
            }
            #endregion
        }
        /// <summary>
        /// Conversion time format
        /// </summary>
        /// <param></param>
        /// <param></param>
        /// <returns></returns>
        public string time()
        {
            string dNow = DateTime.Now.ToString().Trim().Replace("/", "").Replace(":", "");
            string fileName = dNow.ToString();
            return fileName;
        }
    }
}
