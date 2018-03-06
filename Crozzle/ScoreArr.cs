using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle
{
    /// <summary>
    /// define for array
    /// </summary>
    public class ScoreArr
    {
        private int score_word;
        private string word;
        private int length;
        public ScoreArr(int score, string word, int length)
        {
            this.score_word = score;
            this.word = word;
            this.length = length;
        }
        public int Score_Word
        {
            get { return score_word; }
            set { score_word = value; }
        }
        public string Word
        {
            get { return word; }
            set { word = value; }
        }
        public int Length
        {
            get { return length; }
            set { length = value; }
        }
    }
}
