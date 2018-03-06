using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle
{
    /// <summary>
    /// validate different level insert
    /// </summary>
    class ValidLevel
    {
        Define define = new Define();
        Easy easy = new Easy();
        Medium medium = new Medium();
        Hard hard = new Hard();
        //easy touch
        public bool touch(List<List<string>> word)
        {
            bool flag = true;
            foreach (List<string> child_word1 in word)
            {
                foreach (List<string> child_word2 in word)
                {
                    if (!judge_touch(child_word1, child_word2))
                    {
                        flag = false;
                        break;
                    }
                }
            }
            return flag;
        }

        public bool judge_touch(List<string> word1, List<string> word2)
        {
            bool flag = true;
            if (word1[3] != word2[3])
            {
                int x1 = Convert.ToInt16(word1[1]);
                int y1 = Convert.ToInt16(word1[2]);
                int word_length = word1[3].Length;
                int x2 = Convert.ToInt16(word2[1]);
                int y2 = Convert.ToInt16(word2[2]);
                if (word1[0] == define.Direction_h && word2[0] == define.Direction_h)
                {
                    if ((x2 == (x1 - 1) || x2 == (x1 + 1)) && y2 >= (y1 - 1) && y2 <= (y1 + word_length + 1))
                    {
                        flag = false;
                    }
                }
                if (word1[0] == define.Direction_v && word2[0] == define.Direction_v)
                {
                    if ((y2 == (y1 - 1) || y2 == (y1 + 1)) && x2 >= (x1 - 1) && x2 <= (x1 + word_length + 1))
                    {
                        flag = false;
                    }
                }
            }
            return flag;
        }
        //easy insert
        public bool insert_easy(List<List<string>> group_word)
        {
            bool flag = true;
            List<int> easy_count = new List<int>();
            easy_count = insert(group_word);
            foreach (int insert_no in easy_count)
            {
                if (insert_no > easy.Insert_up || insert_no < easy.Insert_low)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        //medium insert
        public bool insert_med(List<List<string>> group_word)
        {
            bool flag = true;
            List<int> med_count = new List<int>();
            med_count = insert(group_word);
            foreach (int insert_no in med_count)
            {
                if (insert_no > medium.Insert_up || insert_no < medium.Insert_low)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
        //validate insert,返回的是交点个数的list
        public List<int> insert(List<List<string>> group_word)
        {
            List<int> count = new List<int>();
            foreach (List<string> word1 in group_word)
            {
                int number = 0;
                foreach (List<string> word2 in group_word)
                {
                    if (decide_insert(word1, word2))
                    {
                        number++;
                    }
                }
                count.Add(number);
            }
            return count;
        }

        //range
        public bool decide_insert(List<string> word1, List<string> word2)
        {
            bool flag = false;
            if (word1[3] != word2[3])
            {
                if (word1[0] == define.Direction_h && word2[0] == define.Direction_v)
                {
                    if (Convert.ToInt16(word2[2]) >= Convert.ToInt16(word1[2]) && Convert.ToInt16(word2[2]) <= (Convert.ToInt16(word1[2]) + word1[3].Length))
                    {
                        flag = true;
                    }
                }
                if (word1[0] == define.Direction_v && word2[0] == define.Direction_h)
                {
                    if (Convert.ToInt16(word1[2]) >= Convert.ToInt16(word2[2]) && Convert.ToInt16(word1[2]) <= (Convert.ToInt16(word2[2]) + word2[3].Length))
                    {
                        flag = true;
                    }
                }
            }
            return flag;
        }
    }
}
