using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crozzle
{
    /// <summary>
    /// display functions
    /// </summary>
    class Crozzle
    {
        public string header = @"<!DOCTYPE html><html><head><title></title><style>
                                table, td {
                                    border: 1px solid black;
                                    border-collapse: collapse;
                                }
                                td {
                                    width:24px;
                                    height:24px;
                                    text-align: center;
                                }
                                </style>
                                </head><body><table runat='server'>";
        public string footer = @"</body></html>";

        /// <summary>
        /// Loop html and display crozzle
        /// </summary>
        /// <param name="hor">rows</param>
        /// <param name="ver">columns</param>
        /// <param name="cha">letter</param>
        /// <returns></returns>
        public string display(int hor, int ver, char[,] cha)
        {
            string table = null;
            for (int i = 0; i < hor; i++)
            {
                table = table + @"<tr id=table>";
                for (int j = 0; j < ver; j++)
                {
                    table = table + @"<td>" + cha[i, j] + @"</td>";
                }
                table = table + @"</tr>";
            }
            return table;
        }

        /// <summary>
        /// Add word into word list array and horizontal array.
        /// </summary>
        /// <param name="x">location</param>
        /// <param name="y"></param>
        /// <param name="word"></param>
        /// <param name="wordList"></param>
        /// <param name="wordHor"></param>
        public void addHor(int x, int y, string word, char[,] wordList, char[,] wordHor)
        {
            char[] cha = word.ToArray();
            for (int m = 0; m < cha.Length; m++)
            {
                wordList[x - 1, y + m - 1] = cha[m];
                wordHor[x - 1, y + m - 1] = cha[m];
            }
        }
        public void addVer(int x, int y, string word, char[,] wordList, char[,] wordVer)
        {
            char[] cha = word.ToArray();
            for (int m = 0; m < cha.Length; m++)
            {
                wordList[x + m - 1, y - 1] = cha[m];
                wordVer[x + m - 1, y - 1] = cha[m];
            }
        }
    
    }
}
