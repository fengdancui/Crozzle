using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle
{
    /// <summary>
    /// define variables for medium level
    /// </summary>
    class Medium
    {
        private string level = "MEDIUM";
        private int insert_low = 1;
        private int insert_up = 3;
        public string Level
        {
            get { return level; }
        }
        public int Insert_low
        {
            get { return insert_low; }
        }
        public int Insert_up
        {
            get { return insert_up; }
        }
    }
}
