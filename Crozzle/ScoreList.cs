using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle
{
    /// <summary>
    /// define for list
    /// </summary>
    public class ScoreList
    {
        private int score_word;
        private List<List<string>> word_list;
        public ScoreList(int score, List<List<string>> word_list)
        {
            this.score_word = score;
            this.word_list = word_list;
        }
        public int Score_Word
        {
            get { return score_word; }
            set { score_word = value; }
        }
        public List<List<string>> Word_List
        {
            get { return word_list; }
            set { word_list = value; }
        }
    }
}
