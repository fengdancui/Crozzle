using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle
{
    /// <summary>
    /// sort according to score
    /// </summary>
    public class ScoreSort
    {
        const int ascii = 65;
        const int difference = 26;
        /// <summary>
        /// sort array
        /// </summary>
        /// <param name="word_arr"></param>
        /// <param name="score_list"></param>
        /// <returns></returns>
        public List<ScoreArr> store_score_array(string[] word_arr, List<int> score_list)
        {
            List<ScoreArr> score_word = new List<ScoreArr>();
            for(int i = 0; i < word_arr.Length; i++)
            {
                int score = word_caculate(word_arr[i], score_list);
                int word_length = word_arr[i].Length;
                score_word.Add(new ScoreArr(score, word_arr[i], word_length));
            }
            return score_word;
        }
        /// <summary>
        /// sort list
        /// </summary>
        /// <param name="word_3d"></param>
        /// <param name="score_list"></param>
        /// <returns></returns>
        public List<ScoreList> store_score_list(List<List<List<string>>> word_3d, List<int> score_list)
        {
            List<ScoreList> score_2d = new List<ScoreList>();
            for(int i = 0; i < word_3d.Count; i++)
            {
                int score = list2d_caculate(word_3d[i], score_list);
                score_2d.Add(new ScoreList(score, word_3d[i]));
            }
            return score_2d;
        }
        /// <summary>
        /// caculate score part
        /// </summary>
        /// <param name="word"></param>
        /// <param name="score_list"></param>
        /// <returns></returns>
        public int word_caculate(string word, List<int> score_list)
        {
            int score = 0;
            char[] cha = word.ToArray();
            for (int i = 0; i < cha.Length; i++)
            {
                int index = (int)cha[i] - ascii;
                try
                {
                    score += score_list[index + difference];
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.Write(e);
                }
            }
            return score;
        }
        //2d list score
        public int list2d_caculate(List<List<string>> word_2d, List<int> score_list)
        {
            int score_h = word_caculate(word_2d[0][3], score_list);
            int score_v = word_caculate(word_2d[1][3], score_list);
            char[] cha = word_2d[0][3].ToArray();
            int insert_index = Convert.ToInt16(word_2d[1][2]);
            char insert_letter = cha[insert_index - 1];
            int index = (int)insert_letter - ascii;
            int score_ins = score_list[index];
            int score_non = score_list[index + difference];
            int score = (score_h + score_v) - 2 * score_non + score_ins;
            return score;
        }
    }
}
