using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle
{
    /// <summary>
    /// validate medium constraints
    /// </summary>
    class ValidMed
    {
        ValidLevel vl = new ValidLevel();
        public bool valid_med = false;
        public ValidMed(List<List<string>> word)
        {
            if (vl.insert_med(word))
            {
                valid_med = true;
            }
        }
    }
}
