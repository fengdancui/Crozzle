using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crozzle
{
    /// <summary>
    /// Validate the input file and configuration file.
    /// </summary>
    class ValidFile
    {
        //validate input file.
        /// <summary>
        /// Check "The actual number of words in the list should ranges from 10 to 1000". 
        /// </summary>
        /// <param name="wordList">The number of words in word list.</param>
        /// <returns></returns>
        public string validateWordListNum(int wordList)
        {
            string errorWordListNum = null;
            if (wordList < 10 || wordList > 1000)
            {
                errorWordListNum = @"<p>The wordlist size (" + wordList + @") in the header is not within the range 10 to 1000.</p>";
            }
            return errorWordListNum;
        }

        /// <summary>
        /// Check "The actual number of rows in the crozzle should within 4 to 400".
        /// </summary>
        /// <param name="rowsNum">The number of rows</param>
        /// <returns></returns>
        public string validateRowsNum(int rowsNum)
        {
            string errorRowsNum = null;
            if (rowsNum < 4 || rowsNum > 400)
            {
                errorRowsNum = @"<p>The actual number of rows (" + rowsNum + @") in the crozzle is not within 4 to 400.</p>";
            }
            return errorRowsNum;
        }

        /// <summary>
        /// Check "The actual number of columns in the crozzle should within 8 to 800".
        /// </summary>
        /// <param name="colNum">The number of columns</param>
        /// <returns></returns>
        public string validateColNum(int colNum)
        {
            string errorColNum = null;
            if (colNum < 8 || colNum > 800)
            {
                errorColNum = @"<p>The actual number of columns (" + colNum + @") in the crozzle is not within 8 to 800.</p>";
            }
            return errorColNum;
        }


        /// <summary>
        /// Check "The word in crozzle should be found in word list".
        /// </summary>
        /// <param name="word"></param>
        /// <param name="secondLine">Word list</param>
        /// <returns></returns>
        public string validateWordExist(string[] word, string[] secondLine)
        {
            string errorExist = null;
            for (int j = 0; j < word.Length; j++)
            {
                bool exists = ((IList)secondLine).Contains(word[j]);
                if (exists) { }
                else
                    errorExist = errorExist + @"<p>" + word[j] + @" does not exist</p>";
            }
            return errorExist;
        }


        /// <summary>
        /// Check "A word cannot be inserted in the crozzle more than once".
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public string validateRepeat(string[] word)
        {
            string errorRepeat = null;
            for (int m = 0; m < word.Length - 1; m++)
            {
                for (int n = m + 1; n < word.Length; n++)
                {
                    if (word[n] == word[m])
                    {
                        errorRepeat = errorRepeat + @"<p>" + word[m] + @" is repeated</p>";
                    }
                }
            }
            return errorRepeat;
        }


        /// <summary>
        /// Check whether the level is correct.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public string validateLevel(string level)
        {
            string errorLevel = null;
            if ((level == "EASY") || (level == "MEDIUM") || (level == "HARD")) { }
            else
            {
                errorLevel = @"<p> The file is not a crozzle file because there is no level description.</p>";
            }
            return errorLevel;
        }


        /// <summary>
        /// Check number pattern using regular expression.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool validateIfNum(string str)
        {
            Regex regex = new Regex(@"^\d+$");
            if (regex.IsMatch(str))
                 return true;
            else
                 return false;
                
        }


        /// <summary>
        /// Check the length of first line in each input file.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string validdateFirstLen(int length)
        {
            string errorLength=null;
            if (length > 6)
                errorLength = "The length of the crozzle file firstline is wrong, it should be 6, not " + length;
            return errorLength;
        }



        /// <summary>
        /// Check direction pattern, it should only be HORIZONTAL or VERTICAL
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool validateDirec(string direction)
        {
            if (direction != "HORIZONTAL" && direction != "VERTICAL")
            {
                return false;
            }
            else
                return true;
        }



        /// <summary>
        /// Check word pattern
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool validateIfWord(string word)
        {
            Regex regex = new Regex(@"^[A-Z]*$");
            if (regex.IsMatch(word))
                return true;
            else
                return false;
        }



        /// <summary>
        /// Check "Each word is within the edge of crozzle".
        /// </summary>
        /// <param name="ver">The vertical coordinate of the first letter of a word.</param>
        /// <param name="verLen">Width of crozzle</param>
        /// <param name="hor">The horizontal coordinate of the first letter of a word</param>
        /// <param name="horLen">Height of crozzle</param>
        /// <param name="word"></param>
        /// <returns></returns>
        public string validateEdgeHor(int ver, int verLen, int hor, int horLen, string word)
        {
            string errorEdgeHor = null;
            if ((ver+word.Length-1) > verLen || hor > horLen)
            {
                errorEdgeHor = @"<p>The word " + word + @" is out of the edge of the crozzle</p>";
            }
            return errorEdgeHor;
        }
        public string validateEdgeVer(int hor, int horLen, int ver, int verLen, string word)
        {
            string errorEdgeVer = null;
            if ((hor+word.Length-1) > horLen || ver > verLen)
            {
                errorEdgeVer = @"<p>The word "+ word +@" is out of the edge of the crozzle</p>";
            }
            return errorEdgeVer;
        }
        

        /// <summary>
        /// Check whether the actual number is correct.
        /// </summary>
        /// <param name="wordNum"></param>
        /// <param name="wordList">An array which store all words in crozzle</param>
        /// <returns></returns>
        public string validateActuNum(int wordNum, string[] wordList)
        {
            string errorActuNum = null;
            if (wordNum != wordList.Length)
            {
                errorActuNum = @"<p> The actual word number " + wordList.Length + @" does not equal to " + wordNum;
            }
            return errorActuNum;
        }



        //Configuration file section
        /// <summary>
        /// Check whether the pointperword match correct pattern
        /// </summary>
        /// <param name="secondLine"></param>
        /// <returns></returns>
        public string valConfSecond(string secondLine)
        {
            string errorConfSecond = null;
            Regex regex = new Regex(@"^POINTSPERWORD=\d+");
            if (!regex.IsMatch(secondLine))
            {
                errorConfSecond = @"<p>The second line " + secondLine + @" in this configuration file does not match the pattern.</p>";
            }
            return errorConfSecond;
        }


        /// <summary>
        /// Check whether the last 52 lines match specific pattern
        /// </summary>
        /// <param name="confNum"></param>
        /// <returns></returns>
        public string valConfNum(string confNum)
        {
            string errorConfNum = null;
            Regex regexIns = new Regex(@"^INTERSECTING:[A-Z]=\d+");
            Regex regexNonIns = new Regex(@"^NONINTERSECTING:[A-Z]=\d+");
            if (regexIns.IsMatch(confNum) || regexNonIns.IsMatch(confNum)) { }
            else
            {
                errorConfNum =  @"<p>There is a line " + confNum + @" which does not match the correct pattern.</p>";
            }
            return errorConfNum;
        }
        public string valConfGroup(string confGroup)
        {
            string errorConfGroup = null;
            Regex regexGroup = new Regex(@"^GROUPSPERCROZZLELIMIT=\d+");
            if (regexGroup.IsMatch(confGroup)) { }
            else
            {
                errorConfGroup = @"<p>There is a line " + confGroup + @" which does not match the correct pattern.</p>";
            }
            return errorConfGroup;
        }
    }
}
