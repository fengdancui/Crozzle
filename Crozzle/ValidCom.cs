using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle
{
    /// <summary>
    /// validate common constrains
    /// </summary>
    public class ValidCom
    {
        public bool valid_common = true;
        public bool valid_cover = true;
        CacScore cs = new CacScore();
        Define define = new Define();
        public ValidCom(List<List<string>> group_2d, int rows, int cols)
        {
            if (within_edge(group_2d, rows, cols) == false || judge_over(group_2d) == false)
            {
                valid_common = false;
            }
        }
        public ValidCom(List<List<string>> word_group, int row, int col, List<string> word_list)
        {
            if (!find_word(h_v_touch(word_group, row, col), word_list))
            {
                valid_cover = false;
            }
        }
        //with edge
        public bool within_edge(List<List<string>> group_2d, int rows, int cols)
        {
            bool flag = true;
            int max_x = Convert.ToInt16(group_2d[0][1]);
            int max_y = Convert.ToInt16(group_2d[0][2]);
            int min_x = Convert.ToInt16(group_2d[0][1]);
            int min_y = Convert.ToInt16(group_2d[0][2]);
            for (int i = 0; i < group_2d.Count; i++)
            {
                int x = Convert.ToInt16(group_2d[i][1]);
                int y = Convert.ToInt16(group_2d[i][2]);
                int word_length = group_2d[i][3].Length;
                if (group_2d[i][0] == define.Direction_h)
                {
                    if (x < min_x)
                    {
                        min_x = x;
                    }
                    if (x > max_x)
                    {
                        max_x = x;
                    }
                    if (y < min_y)
                    {
                        min_y = y;
                    }
                    if (y + word_length - 1 > max_y)
                    {
                        max_y = y + word_length - 1;
                    }
                }
                if (group_2d[i][0] == define.Direction_v)
                {
                    if (x < min_x)
                    {
                        min_x = x;
                    }
                    if (x + word_length - 1 > max_x)
                    {
                        max_x = x + word_length - 1;
                    }
                    if (y < min_y)
                    {
                        min_y = y;
                    }
                    if (y > max_y)
                    {
                        max_y = y;
                    }
                }
            }
            int difference_x = max_x - min_x;
            int difference_y = max_y - min_y;
            if (difference_x + 1 > rows || difference_y + 1 > cols || max_y > cols || max_x > rows)
            {
                flag = false;
            }
            return flag;
        }
        //cover old word
        public bool judge_over(List<List<string>> word_group)
        {
            bool flag = true;
            foreach (List<string> word_1 in word_group)
            {
                foreach (List<string> word_2 in word_group)
                {
                    if (word_1[3] != word_2[3])
                    {
                        if (word_1[0] == define.Direction_h && word_2[0] == define.Direction_h)
                        {
                            if (word_1[1] == word_2[1] && (Convert.ToInt16(word_2[2]) >= Convert.ToInt16(word_1[2]) - word_2[3].Length) && (Convert.ToInt16(word_2[2]) <= Convert.ToInt16(word_1[2]) + word_1[3].Length))
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (word_1[0] == define.Direction_v && word_2[0] == define.Direction_v)
                        {
                            if (word_1[2] == word_2[2] && (Convert.ToInt16(word_2[1]) >= Convert.ToInt16(word_1[1]) - word_2[3].Length) && (Convert.ToInt16(word_2[1]) <= Convert.ToInt16(word_1[1]) + word_1[3].Length))
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (word_1[0] == define.Direction_h && word_2[0] == define.Direction_v)
                        {
                            if (h_v_over(word_1, word_2) == false)
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (word_1[0] == define.Direction_v && word_2[0] == define.Direction_h)
                        {
                            if (h_v_over(word_2, word_1) == false)
                            {
                                flag = false;
                                break;
                            }
                        }
                    }
                }
                if (!flag)
                    break;
            }
            return flag;
        }

        public bool h_v_over(List<string> word1, List<string> word2)
        {
            bool flag = true;
            int x1 = Convert.ToInt16(word1[1]);
            int y1 = Convert.ToInt16(word1[2]);
            int x2 = Convert.ToInt16(word2[1]);
            int y2 = Convert.ToInt16(word2[2]);
            if (y2 >= y1 && y2 <= y1 + word1[3].Length - 1)
            {
                if (x2 == x1 + 1 || x2 + word2[3].Length - 1 == x1 - 1)
                {
                    flag = false;
                }
            }
            for (int i = 0; i < word1[3].Length; i++)
            {
                for (int j = 0; j < word2[3].Length; j++)
                {
                    if (x1 == x2 + j && y1 + i == y2)
                    {
                        if (word1[3][i] != word2[3][j])
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                    break;
            }
            return flag;
        }
        //word with each other
        public List<string> h_v_touch(List<List<string>> word_group, int row, int col)
        {
            List<string> store_str = new List<string>();
            List<List<string>> change_axis = new List<List<string>>();
            change_axis = cs.change_aixs_list(word_group);
            char[,] word_hor = cs.store_hor(word_group, row, col);
            char[,] word_ver = cs.store_ver(word_group, row, col);
            store_str.AddRange(h_str(word_hor, row, col));
            store_str.AddRange(h_str(word_ver, row, col));
            return store_str;
        }
        public List<string> h_str(char[,] word_arr, int row, int col)
        {
            List<string> str_hv = new List<string>();
            for (int i = 0; i < row; i++)
            {
                char[] temp_h = new char[col];
                for (int j = 0; j < col; j++)
                {
                    if (word_arr[i, j] != define.Init_cha)
                    {
                        temp_h[j] = word_arr[i, j];
                    }
                    else
                    {
                        temp_h[j] = define.Init_cha;
                    }
                }
                string str_h = new string(temp_h);
                str_hv.Add(str_h);
            }
            return str_hv;
        }
      
      
        public bool find_word(List<string> store_str, List<string> word_list)
        {
            bool flag = true;
            foreach (string str in store_str)
            {
                string[] split_str = str.Split(define.Init_cha);
                for (int i = 0; i < split_str.Length; i++)
                {
                    if (word_list.Contains(split_str[i]) == false && split_str[i].Length > 1)
                    {
                        flag = false;
                        break;
                    }
                }
            }
            return flag;
        }
    }
}
