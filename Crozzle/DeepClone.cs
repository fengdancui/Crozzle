using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle
{
    class DeepClone
    {
        //1d
        public List<string> copy_1d(List<string> word_1d){
            List<string> temp_1d = new List<string>(word_1d);
            return temp_1d;
        }

        //2d string
        public List<List<string>> copy_2d_string(List<List<string>> word_2d)
        {
            List<List<string>> temp_2d = new List<List<string>>();
            foreach (List<string> child_1d in word_2d)
            {
                List<string> temp_1d = new List<string>(child_1d);
                /*foreach (string child_word in child_1d)
                {
                    word_1d.Add(child_word);
                }*/
                temp_2d.Add(temp_1d);  
            }
            return temp_2d;
        }

        //2d int
        public List<List<int>> copy_2d_int(List<List<int>> word_2d)
        {
            List<List<int>> temp_2d = new List<List<int>>();
            foreach (List<int> child_1d in word_2d)
            {
                List<int> temp_1d = new List<int>(child_1d);
                temp_2d.Add(temp_1d);
            }
            return temp_2d;
        }

        //3d
        public List<List<List<string>>> copy_3d(List<List<List<string>>> word_3d)
        {
            List<List<List<string>>> temp_3d = new List<List<List<string>>>();
            foreach (List<List<string>> child_2d in word_3d)
            {
                List<List<string>> word_2d = new List<List<string>>();
                foreach (List<string> child_1d in child_2d)
                {
                    List<string> word_1d = new List<string>(child_1d);
                    word_2d.Add(word_1d);
                }
                temp_3d.Add(word_2d);
            }
            return temp_3d;
        }
    }
}
