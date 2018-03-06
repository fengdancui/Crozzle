using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle
{
    /// <summary>
    /// validate easy constraints
    /// </summary>
    class ValidEasy
    {
        public bool valid_easy = false;
        ValidLevel vl = new ValidLevel();
        public ValidEasy(List<List<string>> word)
        {
            if (vl.insert_easy(word) && vl.touch(word))
            {
                valid_easy = true;
            }
        }
    }
}
