using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle
{
    /// <summary>
    /// define the variables
    /// </summary>
    public class Define
    {
        private string direction_h = "HORIZONTAL";
        private string direction_v = "VERTICAL";
        private int score_num = 52;
        private char init_cha = '\0';
        public string Direction_h
        {
            get { return direction_h; }
        }
        public string Direction_v
        {
            get { return direction_v; }
        }
        public int Score_num
        {
            get { return score_num; }
        }
        public char Init_cha
        {
            get { return init_cha; }
        }
    }
}
