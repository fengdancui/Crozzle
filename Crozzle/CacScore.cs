using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle
{
    /// <summary>
    /// caculate score for 1d and 2d list
    /// </summary>
    public class CacScore
    {
        Define define = new Define();
        /// <summary>
        /// score of 2d
        /// </summary>
        /// <param name="word_list"> 2d word list </param>
        /// <param name="per_word"> score of per word </param>
        /// <param name="score"> list to store score of each letter </param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>result_score</returns>
        public int score(List<List<string>> word_list, int per_word, List<int> score, int row, int col)
        {
            int result_score = 0;
            List<int> word_score = score_list(word_list, row, col);
            for (int i = 0; i < define.Score_num; i++)
            {
                result_score += word_score[i] * score[i];
            }
            result_score += word_list.Count * per_word;
            return result_score;
        }
        /// <summary>
        /// change axis of word
        /// </summary>
        /// <param name="word_list"></param>
        /// <returns></returns>
        public List<List<string>> change_aixs_list(List<List<string>> word_list)
        {
            int min_x = Convert.ToInt16(word_list[0][1]);
            int min_y = Convert.ToInt16(word_list[0][2]);
            foreach (List<string> word_1d in word_list)
            {
                if (Convert.ToInt16(word_1d[1]) < min_x)
                {
                    min_x = Convert.ToInt16(word_1d[1]);
                }
                if (Convert.ToInt16(word_1d[2]) < min_y)
                {
                    min_y = Convert.ToInt16(word_1d[2]);
                }
            }
            if (min_x < 1)
            {
                foreach (List<string> word_1d in word_list)
                {
                    int old_x = Convert.ToInt16(word_1d[1]);
                    int new_x = old_x - min_x + 1;
                    word_1d[1] = Convert.ToString(new_x);
                }
            }
            if (min_y < 1)
            {
                foreach (List<string> word_1d in word_list)
                {
                    int old_y = Convert.ToInt16(word_1d[2]);
                    int new_y = old_y - min_y + 1;
                    word_1d[2] = Convert.ToString(new_y);
                }
            }
            return word_list;
        }
        /// <summary>
        /// get the list to store score
        /// </summary>
        /// <param name="word_list"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public List<int> score_list(List<List<string>> word_list, int row, int col)
        {
            List<int> score_letter = new List<int>();
            for (int i = 0; i < define.Score_num; i++)
            {
                score_letter.Add(0);
            }
            List<List<string>> new_wordlist = change_aixs_list(word_list);
            char[,] hor_list = store_hor(new_wordlist, row, col);
            char[,] ver_list = store_ver(new_wordlist, row, col);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (hor_list[i, j] == ver_list[i, j] && hor_list[i, j] != define.Init_cha && ver_list[i, j] != define.Init_cha) 
                    {
                        int index = (int)hor_list[i, j] - 65;
                        score_letter[index]++;
                    }
                    else if (hor_list[i, j] != define.Init_cha && ver_list[i, j] == define.Init_cha)
                    {
                        int index = (int)hor_list[i, j] - 65 + 26;
                        score_letter[index]++;
                    }
                    else if (hor_list[i, j] == define.Init_cha && ver_list[i, j] != define.Init_cha)
                    {
                        int index = (int)ver_list[i, j] - 65 + 26;
                        score_letter[index]++;
                    }
                }
            }
            return score_letter;
        }
        /// <summary>
        /// store all letter in an array
        /// </summary>
        /// <param name="word_list"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public char[,] store_all(List<List<string>> word_list, int row, int col)
        {
            char[,] hor = store_hor(word_list, row, col);
            char[,] ver = store_ver(word_list, row, col);
            try
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        if (hor[i, j] == define.Init_cha)
                        {
                            hor[i, j] = ver[i, j];
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            return hor;
        }
        public char[,] store_hor(List<List<string>> word_list, int row, int col)
        {
            char[,] word_cha = define_new(row, col);
            foreach (List<string> word in word_list)
            {
                if (word[0] == define.Direction_h)
                {
                    int index_x = Convert.ToInt16(word[1]) - 1;
                    int index_y = Convert.ToInt16(word[2]) - 1;
                    for (int j = 0; j < word[3].Length; j++)
                    {
                        try
                        {
                            word_cha[index_x, index_y + j] = word[3][j];
                        }
                        catch (Exception e)
                        {
                            Console.Write(e);
                        }
                    }
                }
            }
            return word_cha;
        }
        public char[,] store_ver(List<List<string>> word_list, int row, int col)
        {
            char[,] word_cha = define_new(row, col);
            foreach (List<string> word in word_list)
            {
                if (word[0] == define.Direction_v)
                {
                    int index_x = Convert.ToInt16(word[1]) - 1;
                    int index_y = Convert.ToInt16(word[2]) - 1;
                    for (int j = 0; j < word[3].Length; j++)
                    {
                        try
                        {
                            word_cha[index_x + j, index_y] = word[3][j];
                        }
                        catch (Exception e)
                        {
                            Console.Write(e);
                        }
                    }

                }
            }
            return word_cha;
        }
        public char[,] define_new(int row, int col)
        {
            char[,] word_cha = new char[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    word_cha[i, j] = define.Init_cha;
                }
            }
            return word_cha;
        }
    }
}
