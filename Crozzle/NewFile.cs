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
    /// open crozzle file and progress
    /// </summary>
    public class NewFile
    {
        public string level;
        public string word_num;
        public string path;
        public int rows;
        public int cols;
        string[] new_first;
        public string[] word_list;
        public string[] word_list_copy;
        List<string> word;
        DeepClone dc = new DeepClone();
        Define define = new Define();
        Easy easy = new Easy();
        Medium medium = new Medium();
        Hard hard = new Hard();
        
        public void main()
        {
            OpenFile of = new OpenFile();
            if (of.file_open)
            {
                //Succeed
                path = of.new_file;
                StreamReader new_sr = new StreamReader(of.new_file);
                char[] separator = { ',' };
                new_first = new_sr.ReadLine().Split(separator);
                level = new_first[0];
                word_num = new_first[1];
                rows = Convert.ToInt16(new_first[2]);
                cols = Convert.ToInt16(new_first[3]);
                word_list = new_sr.ReadLine().Split(separator);
                word_list_copy = (string[])word_list.Clone();
                word = new List<string>(word_list_copy);
                word = word.OrderByDescending(word_child => word_child.Length).ToList();
            }
        }

        /// <summary>
        /// judge the insert word in an array
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public List<List<List<string>>> insert_add(List<string> word)
        {
            List<List<List<string>>> group_3dword = new List<List<List<string>>>();
            for(int i = 0; i < word.Count; i++)
            {
                for(int j= i+1; j < word.Count - 1; j++)
                {
                    if (same_letter(word[i], word[j]).Count != 0)
                    {
                        add_3dword(same_letter(word[i], word[j]), group_3dword, word[i], word[j]);
                        word[i] = null;
                        word[j] = null;
                        break;
                    }
                }
            }
            return group_3dword;
        }

        /// <summary>
        /// judge whether there is same letter of two words
        /// </summary>
        /// <param name="word1"></param>
        /// <param name="word2"></param>
        /// <returns></returns>
        public List<List<int>> same_letter(string word1, string word2)
        {
            List<List<int>> letter_2dindex = new List<List<int>>();
            if(word1!=null && word2 != null)
            {
                for (int i = 0; i < word1.Length; i++)
                {
                    for (int j = 0; j < word2.Length; j++)
                    {
                         if (word1[i] == word2[j])
                        {
                            List<int> letter_1dindex = new List<int>();
                            letter_1dindex.Add(i);
                            letter_1dindex.Add(j);
                            letter_2dindex.Add(letter_1dindex);
                        }
                    }
                }
            }
            return letter_2dindex;
        }

        /// <summary>
        /// generate 1d list
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public List<string> add_1dword(string direction, int row, int col, string word)
        {
            List<string> group_1dword = new List<string>(); 
            group_1dword.Add(direction);
            group_1dword.Add(Convert.ToString(row));
            group_1dword.Add(Convert.ToString(col));
            group_1dword.Add(word);
            return group_1dword;
        }

        /// <summary>
        /// generate inserted word group (2 word each group)
        /// </summary>
        /// <param name="letter_2dindex"></param>
        /// <param name="group_3dword"></param>
        /// <param name="word_1"></param>
        /// <param name="word_2"></param>
        public void add_3dword(List<List<int>> letter_2dindex, List<List<List<string>>> group_3dword, string word_1, string word_2)
        {
            foreach (List<int> group_index in letter_2dindex)
            {
                List<List<string>> group_2dword = new List<List<string>>();
                group_2dword.Add(add_1dword(define.Direction_h, group_index[1] + 1, 1, word_1));
                group_2dword.Add(add_1dword(define.Direction_v, 1, group_index[0] + 1, word_2));
                group_3dword.Add(group_2dword);
            }
        }
        
        /// <summary>
        /// add groups together and validate
        /// </summary>
        /// <param name="final"></param>
        /// <param name="group_3dword"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<List<List<string>>> add_group(List<List<List<string>>> final, List<List<List<string>>> group_3dword, int count)
        {
            //对于hard只能取值最大的
            List<List<List<string>>> insert = new List<List<List<string>>>();
            List<List<List<string>>> final_temp = new List<List<List<string>>>();
            final_temp = dc.copy_3d(final);
            //写个递归函数在这个方法内继续调用这个方法
            for (int m = 0; m < final.Count; m++)
            {
                //insert[j]是要插入的二维list
                insert = new List<List<List<string>>>(get_insert_group(final[m], group_3dword[count]));
                if (insert.Count != 0)
                {
                    int index = 0;
                    for (int j = 0; j < insert.Count; j++)
                    {
                        if (index == 0)
                        {
                            final_temp[m].Add(insert[j][0]);//越界
                            final_temp[m].Add(insert[j][1]);
                            if (valid(final_temp[m]) == false)
                            {
                                final_temp[m].Remove(insert[j][0]);
                                final_temp[m].Remove(insert[j][1]);
                            }
                            else if(valid_cover(final_temp[m], word))
                            {
                                index++;
                                if (j < insert.Count - 1)
                                {
                                    List<List<string>> temp_2d = new List<List<string>>();
                                    temp_2d = dc.copy_2d_string(final[m]);
                                    final_temp.Add(temp_2d);
                                } 
                            }
                            else
                            {
                                final_temp[m].Remove(insert[j][0]);
                                final_temp[m].Remove(insert[j][1]);
                            }
                        }
                        else
                        {
                            int temp_index = final_temp.Count - 1;
                            final_temp[temp_index].Add(insert[j][0]);
                            final_temp[temp_index].Add(insert[j][1]);
                            if (valid(final_temp[temp_index]) == false)
                            {
                                final_temp[temp_index].Remove(insert[j][0]);
                                final_temp[temp_index].Remove(insert[j][1]);
                                if (j == insert.Count - 1)
                                {
                                    final_temp.RemoveAt(temp_index);
                                }
                            }
                            else if(valid_cover(final_temp[temp_index], word))
                            {
                                index++;
                                if (j < insert.Count - 1)
                                {
                                    List<List<string>> temp_2d = new List<List<string>>();
                                    temp_2d = dc.copy_2d_string(final[m]);
                                    final_temp.Add(temp_2d);
                                } 
                            }
                            else
                            {
                                final_temp[temp_index].Remove(insert[j][0]);
                                final_temp[temp_index].Remove(insert[j][1]);
                                if (j == insert.Count - 1)
                                {
                                    final_temp.RemoveAt(temp_index);
                                }
                            }
                        }  
                    }
                }
            }
            count++;
            final = new List<List<List<string>>>();
            final = dc.copy_3d(final_temp);
            if (count < group_3dword.Count) 
            {
                return add_group(final, group_3dword, count);
            }
            return final;
        }
        /// <summary>
        /// validate the list
        /// </summary>
        /// <param name="valid_list"></param>
        /// <returns></returns>
        public bool valid(List<List<string>> valid_list)
        {
            bool flag = true;
            ValidCom vc = new ValidCom(valid_list, rows, cols);
            if (level == easy.Level)
            {
                ValidEasy ve = new ValidEasy(valid_list);
                if (ve.valid_easy == false || vc.valid_common == false)
                {
                    flag = false;
                }
            }
            if (level == medium.Level)
            {
                ValidMed vm = new ValidMed(valid_list);
                if (vm.valid_med == false || vc.valid_common == false)
                {
                    flag = false;
                }
            }
            if (level == hard.Level)
            {
                if (vc.valid_common == false)
                {
                    flag = false;
                }
            }
            return flag;
        }
        /// <summary>
        /// validate whether the new list will cover old list
        /// </summary>
        /// <param name="valid_list"></param>
        /// <param name="word_list"></param>
        /// <returns></returns>
        public bool valid_cover(List<List<string>> valid_list, List<string> word_list)
        {
            bool flag = true;
            ValidCom vc = new ValidCom(valid_list, rows, cols, word_list);
            if (vc.valid_cover == false)
            {
                flag = false;
            }
            return flag;
        }
        /// <summary>
        /// decide whether two group insert with each other, if, add
        /// </summary>
        /// <param name="group_word1"></param>
        /// <param name="group_word2"></param>
        /// <returns></returns>
        public List<List<List<string>>> get_insert_group(List<List<string>> group_word1, List<List<string>> group_word2)
        {
            List<List<List<string>>> insert_group = new List<List<List<string>>>();
            for(int j = 0; j < group_word1.Count; j++) 
            {
                if (group_word1[j][3] != group_word2[0][3] && group_word1[j][3] != group_word2[1][3])
                {
                    for (int i = 0; i < group_word2.Count; i++)
                    {
                        List<List<int>> insert_index =new List<List<int>>();
                        insert_index = same_letter(group_word1[j][3], group_word2[i][3]);
                        foreach(List<int> index in insert_index){
                            if (index.Count != 0)
                            {
                                List<List<string>> temp_word2 = new List<List<string>>();
                                List<List<string>> temp_new = new List<List<string>>();
                                temp_word2 = dc.copy_2d_string(group_word2);
                                change_wordinfo(index, group_word1[j], temp_word2[i]);
                                temp_new = compare_list(group_word2, temp_word2);
                                if (delete_same(group_word1, temp_word2))
                                {
                                    insert_group.Add(temp_new);
                                }
                            }
                        }
                    }
                }
            }
            return insert_group;
        }

        /// <summary>
        /// delete same words if any
        /// </summary>
        /// <param name="group_1word"></param>
        /// <param name="group_2word"></param>
        /// <returns></returns>
        public bool delete_same(List<List<string>> group_1word, List<List<string>> group_2word)
        {
            bool flag = true;
            for(int i = 0; i <group_2word.Count; i++)
            {
                for(int j=0; j < group_1word.Count; j++) 
                {
                    if (group_2word[i][3] == group_1word[j][3])
                    {
                         flag = false;
                    }
                }
            }
            return flag;
        }

        /// <summary>
        /// change the axis of the other word according to the changed one
        /// </summary>
        /// <param name="group_2d_old"></param>
        /// <param name="group_2d_new"></param>
        /// <returns></returns>
        public List<List<string>> compare_list(List<List<string>> group_2d_old, List<List<string>> group_2d_new)
        {
            List<List<string>> result = new List<List<string>>();
            result = dc.copy_2d_string(group_2d_new);
            if (group_2d_new[0][1] != group_2d_old[0][1] || group_2d_new[0][2] != group_2d_old[0][2])
            {
                int rela_x, rela_y;
                if (group_2d_new[0][0] != define.Direction_h)
                {
                    result[1][0] = define.Direction_h;
                    result[1][1] = group_2d_new[1][2];
                    result[1][2] = group_2d_new[1][1];
                    rela_x = Convert.ToInt16(group_2d_new[0][1]) - Convert.ToInt16(group_2d_old[0][2]);
                    rela_y = Convert.ToInt16(group_2d_new[0][2]) - Convert.ToInt16(group_2d_old[0][1]);
                }
                else
                {
                    rela_x = Convert.ToInt16(group_2d_new[0][1]) - Convert.ToInt16(group_2d_old[0][1]);
                    rela_y = Convert.ToInt16(group_2d_new[0][2]) - Convert.ToInt16(group_2d_old[0][2]);
                }
                int new_x = Convert.ToInt16(result[1][1]);
                int new_y = Convert.ToInt16(result[1][2]);
                new_x = new_x + rela_x;
                new_y = new_y + rela_y;
                result[1][1] = Convert.ToString(new_x);
                result[1][2] = Convert.ToString(new_y);  
            }
            if (group_2d_new[1][1] != group_2d_old[1][1] || group_2d_new[1][2] != group_2d_old[1][2])
            {
                int rela_x, rela_y;
                if (group_2d_new[1][0] != define.Direction_v)
                {
                    result[0][0] = define.Direction_v;
                    result[0][1] = group_2d_new[0][2];
                    result[0][2] = group_2d_new[0][1];
                    rela_x = Convert.ToInt16(group_2d_new[1][1]) - Convert.ToInt16(group_2d_old[1][2]);
                    rela_y = Convert.ToInt16(group_2d_new[1][2]) - Convert.ToInt16(group_2d_old[1][1]);
                }
                else
                {
                    rela_x = Convert.ToInt16(group_2d_new[1][1]) - Convert.ToInt16(group_2d_old[1][1]);
                    rela_y = Convert.ToInt16(group_2d_new[1][2]) - Convert.ToInt16(group_2d_old[1][2]);
                }
                int new_x = Convert.ToInt16(result[0][1]);
                int new_y = Convert.ToInt16(result[0][2]);
                new_x = new_x + rela_x;
                new_y = new_y + rela_y;
                result[0][1] = Convert.ToString(new_x);
                result[0][2] = Convert.ToString(new_y);
            }
            return result;
        }
        /// <summary>
        /// change word information of the one which need to be inserted
        /// </summary>
        /// <param name="insert_index"></param>
        /// <param name="group_1d_word1"></param>
        /// <param name="group_1d_word2"></param>
        public void change_wordinfo(List<int> insert_index, List<string> group_1d_word1, List<string> group_1d_word2)
        {
            int x1=Convert.ToInt16(group_1d_word1[1]);
            int y1=Convert.ToInt16(group_1d_word1[2]);
            int x2=Convert.ToInt16(group_1d_word2[1]);
            int y2=Convert.ToInt16(group_1d_word2[2]);
            int insert_x1 = 0, insert_y1 = 0, insert_x2 = 0, insert_y2 = 0;
            if (group_1d_word1[0] == define.Direction_h)
            {
                insert_x1 = x1;
                insert_y1 = insert_index[0] + y1;
                if (group_1d_word2[0] == define.Direction_h)
                {
                    insert_x2 = insert_index[1] + 1;
                    insert_y2 = x2;
                    group_1d_word2[0] = define.Direction_v;
                    int temp = x2;
                    x2 = y2;
                    y2 = temp;
                }
                else
                {
                    insert_x2 = insert_index[1] + 1;
                    insert_y2 = y2;
                }
            }
            if (group_1d_word1[0] == define.Direction_v)
            {
                insert_x1 = insert_index[0] + x1;
                insert_y1 = y1;
                if (group_1d_word2[0] == define.Direction_h)
                {
                    insert_x2 = x2;
                    insert_y2 = insert_index[1] + 1;
                }
                else
                {
                    insert_x2 = y2;
                    insert_y2 = insert_index[1] + 1;
                    group_1d_word2[0] = define.Direction_h;
                    int temp = x2;
                    x2 = y2;
                    y2 = temp;
                } 
            }
            List<string> word2_axis = new List<string>();
            word2_axis = get_word_axis(x2, y2, rela_axis(insert_x1, insert_y1, insert_x2, insert_y2));
            group_1d_word2[1] = word2_axis[0];
            group_1d_word2[2] = word2_axis[1];
        }
        public List<int> rela_axis(int x1_insert, int y1_insert, int x2_insert, int y2_insert)
        {
            List<int> rela_value = new List<int>();
            int rela_x = x1_insert - x2_insert;
            int rela_y = y1_insert - y2_insert;
            rela_value.Add(rela_x);
            rela_value.Add(rela_y);
            return rela_value;
        }
        public List<string> get_word_axis(int x2_new, int y2_new, List<int> rela_value)
        {
            List<string> axis_new = new List<string>();
            x2_new = x2_new + rela_value[0];
            y2_new = y2_new + rela_value[1];
            axis_new.Add(Convert.ToString(x2_new));
            axis_new.Add(Convert.ToString(y2_new));
            return axis_new;
        }
        public List<string> str_list(List<ScoreArr> list_arr)
        {
            List<string> sort_word = new List<string>();
            foreach (ScoreArr child in list_arr)
            {
                sort_word.Add(child.Word);
            }
            return sort_word;
        }
        public List<List<List<string>>> str_list(List<ScoreList> list_2d)
        {
            List<List<List<string>>> sort_2d = new List<List<List<string>>>();
            foreach (ScoreList child in list_2d)
            {
                sort_2d.Add(child.Word_List);
            }
            return sort_2d;
        }
        /// <summary>
        /// add more words for easy and medium if any
        /// </summary>
        /// <param name="word_3d"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public List<List<List<string>>> add_more(List<List<List<string>>> word_3d, int i)
        {
            List<List<List<string>>> temp_3d = new List<List<List<string>>>(); 
            temp_3d = dc.copy_3d(word_3d);
            for (int m = 0; m < word_3d.Count; m++) 
            {
                if (judge_same(word_3d[m], word[i]))
                {
                    foreach (List<string> child2 in word_3d[m])
                    {
                        List<List<string>> insert_list = word_axis(child2, word[i]);
                        int index = 0;
                        for (int j = 0; j < insert_list.Count; j++)
                        {
                            if (index == 0)
                            {
                                temp_3d[m].Add(insert_list[j]);
                                if (valid(temp_3d[m]) == false)
                                {
                                    temp_3d[m].Remove(insert_list[j]);
                                }
                                else if (valid_cover(temp_3d[m], word))
                                {
                                    index++;
                                    if (j < insert_list.Count - 1)
                                    {
                                        List<List<string>> temp_2d = new List<List<string>>();
                                        temp_2d = dc.copy_2d_string(word_3d[m]);
                                        temp_3d.Add(temp_2d);
                                    }
                                }
                                else
                                {
                                    temp_3d[m].Remove(insert_list[j]);
                                }
                            }
                            else
                            {
                                int temp_index = temp_3d.Count - 1;
                                temp_3d[temp_index].Add(insert_list[j]);
                                if (valid(temp_3d[temp_index]) == false)
                                {
                                    temp_3d[temp_index].Remove(insert_list[j]);
                                    if (j == insert_list.Count - 1)
                                    {
                                        temp_3d.RemoveAt(temp_index);
                                    }
                                }
                                else if (valid_cover(temp_3d[temp_index], word))
                                {
                                    index++;
                                    if (j < insert_list.Count - 1)
                                    {
                                        List<List<string>> temp_2d = new List<List<string>>();
                                        temp_2d = dc.copy_2d_string(temp_3d[m]);
                                        temp_3d.Add(temp_2d);
                                    }
                                }
                                else
                                {
                                    temp_3d[temp_index].Remove(insert_list[j]);
                                    if (j == insert_list.Count - 1)
                                    {
                                        temp_3d.RemoveAt(temp_index);
                                    }
                                }
                            }
                        }
                    }
                }   
            }
            i++;
            word_3d = new List<List<List<string>>>();
            word_3d = dc.copy_3d(temp_3d);
            if (i < word.Count)
            {
                return add_more(word_3d, i);
            }
            return word_3d;
        }  
        /// <summary>
        /// generate word axis
        /// </summary>
        /// <param name="word_2d"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public List<List<string>> word_axis(List<string> word_2d, string word)
        {
            List<List<string>> axis_word = new List<List<string>>();
            List<List<int>> same_let = same_letter(word_2d[3], word);
            if (same_let.Count != 0)
            {
                foreach (List<int> child in same_let)
                {
                    int x, y;
                    string direction;
                    List<string> temp = new List<string>();
                    if (word_2d[0] == define.Direction_h)
                    {
                        int insert_x = Convert.ToInt16(word_2d[1]);
                        int insert_y = Convert.ToInt16(word_2d[2]) + child[0];
                        y = insert_y;
                        x = insert_x - child[1];
                        direction = define.Direction_v;
                    }
                    else
                    {
                        int insert_x = Convert.ToInt16(word_2d[1]) + child[0];
                        int insert_y = Convert.ToInt16(word_2d[1]);
                        x = insert_x;
                        y = insert_x - child[1];
                        direction = define.Direction_h;
                    }
                    temp.Add(direction);
                    temp.Add(Convert.ToString(x));
                    temp.Add(Convert.ToString(y));
                    temp.Add(word);
                    axis_word.Add(temp);
                }
            }
            return axis_word;
        }
        public bool judge_same(List<List<string>> word_2d, string word)
        {
            bool flag = true;
            foreach (List<string> child in word_2d)
            {
                if (child[3] == word)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
    }
}
