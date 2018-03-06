using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crozzle
{
    /// <summary>
    /// open config file and progress
    /// </summary>
    public class NewConf
    {
        string group_limit;
        string point_word;
        string line;
        const string regex = @"(\d+)";
        public int point_per_word;
        public List<int> score = new List<int>();

        public void main()
        {
            OpenFile of = new OpenFile();
            if (of.file_open)
            {
                //Succeed
                StreamReader sr = new StreamReader(of.new_file);
                group_limit = sr.ReadLine();
                point_word = sr.ReadLine();
                point_per_word = match(point_word);//得到每个单词的分数
                while ((line = sr.ReadLine()) != null)
                {
                    score.Add(match(line));
                }
                //NewFile nf = new NewFile();
            }
        }

        /// <summary>
        /// get number in string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int match(string str)
        {
            Match mstr = Regex.Match(str, regex);
            int number = Convert.ToInt16(mstr.Groups[1].Value);
            return number;
        }
    }
}
