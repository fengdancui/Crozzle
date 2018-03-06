using System;
using System.Collections.Generic;

namespace Crozzle
{
    /// <summary>
    /// Mainly for checking crozzle constraints of different levels.
    /// </summary>
    class ValidCro
    {
        /// <summary>
        /// Check whether a horizontal word has been inserted with limited number of vertical words.
        /// </summary>
        /// <param name="x">Horizontal coordinate of the first letter of a word</param>
        /// <param name="y">Vertical coordinate of the first letter of a word</param>
        /// <param name="word">The word which should be checked</param>
        /// <param name="insert">An array to store the intersection of words</param>
        /// <param name="rowsNum">The number of horizontal lines</param>
        /// <param name="colsNum">The number of vertical lines</param>
        /// <returns></returns>
        public int InsertHor(int x, int y, string word, char[,] insert, int rowsNum, int colsNum)
        {
            int counter = 0;    //number of intersection
            for (int i = y-1; i < y+word.Length-1; i++)
            {
                if (insert[x-1, i] != '\0')
                {
                    counter=counter+1;
                }
                //if (counter < 1 || counter > 2)
                       // errorEasyInsHor =errorEasyInsHor+ @"<p>" + word[i] + @" has been inserted for " + counter + @" times, which is uncorrect in easy level.</p>";
                    
             }
            return counter;
        }


        //For vertical word
        public int InsertVer(int x, int y, string word, char[,] insert, int rowsNum, int colsNum)
        {
            int counter = 0;
            for (int i = x - 1; i < x + word.Length - 1; i++)
            {
                if (insert[i, y-1] != '\0')
                {
                    counter++;
                }
                //if (counter < 1 || counter > 2)
                   // errorEasyInsVer = errorEasyInsVer + @"<p>" + word + @" has been inserted for " + counter + @" times, which is uncorrect in easy level.</p>";

            }     
               return counter; 
       }


        /*----Solution to solve touch problem (horizontal)----*/
        /*  Step 1: Loop the array from the first row to reciprocal second row.
         *  Step 2: Loop the array from the second column to reciprocal second column.
         *  Step 3: If there is a grid which is not null, check lower left quarter and lower right quarter to determine whether it is null.
         *  Step 4: If it is none for all letters, the word cannot touch other. If not, return error string.
         */

        /// <summary>
        /// Check "A horizontal word cannot touch any other horizontal word in easy level".
        /// </summary>
        /// <param name="wordHor">Two dimensional array which stores horizontal words.</param>
        /// <param name="rowsNum">The number of horizontal lines</param>
        /// <param name="colsNum">The number of vertical lines</param>
        /// <returns></returns>
        public string easyTouchHor(char[,] wordHor, int rowsNum, int colsNum)
        {
            bool touch=true;     //whether touch
            string error_touchHor=null;
            for (int i = 0; i < rowsNum-1; i++)
            {
                for (int j = 0; j < colsNum-2; j++)
                {
                    if(wordHor[i,j+1]!='\0'){
                        if (wordHor[i + 1, j] != '\0' || wordHor[i + 1, j + 2] != '\0')
                            touch = false;
                    }
                }
            }
            if (!touch)
                error_touchHor = @"<p> There is a horizontal word that touch any other horizontal word, which is not correct for easy level.</p> ";
            return error_touchHor;
        }




     
        //Check "A vertical word cannot touch any other vertical word in easy level".
        public string easyTouchVer(char[,] wordVer, int rowsNum, int colsNum)
        {
            bool touch = true;
            string error_touchVer = null;
            for (int i = 0; i < rowsNum-2; i++)
            {
                for (int j = 0; j < colsNum-1; j++)
                {
                    if (wordVer[i + 1, j] != '\0')
                    {
                        if (wordVer[i, j + 1] != '\0' || wordVer[i + 2, j + 1] != '\0')
                            touch = false;
                    }
                }
            }
            if (!touch)
                error_touchVer = @"<p> There is (a) vertical word that touch any other horizontal word, which is not correct for easy level.</p> ";
            return error_touchVer;
        }




        //decide the group
        /*----solution to tackle group problem.-----*/
        /*  Step 1: new list(list1) to store the first word in word array.
         *  Step 2: new list(list2) to store last words in word array.
         *  Step 3: loop list1 and list2 and detect whether there is a word(word2) in list2 that insert the word(word1) in list1.
         *  Step 4: If there is, add word2 to list1 and remove word2 in list2. Do Step 3 again.
         *  Step 5: It the length of list1 will not change, stop looping.
         *  Step 6: Compare the length of list1 with word array. If equal, one group. If not, more than one.
         */
         
        /// <summary>
        /// Check the group of crozzle. Step 1 + Step 2 + Step 6
        /// </summary>
        /// <param name="direction">"HORIZONTAL" or "VERTICAL"</param>
        /// <param name="wordArr">An array of all words in crozzle.</param>
        /// <param name="hor">An array of the horizontal coordinate of each word.</param>
        /// <param name="ver">An array of the vertical coordinate of each word.</param>
        /// <returns></returns>
        public string decideGroup(string[] direction, string[] wordArr, int[] hor, int[] ver)
        {
            string errorGroup = null;
            List<string> directionList = new List<string>();
            List<string> insertList = new List<string>();
            List<int> horList = new List<int>();
            List<int> verList = new List<int>();
            directionList.Add(direction[0]);
            insertList.Add(wordArr[0]);
            horList.Add(hor[0]);
            verList.Add(ver[0]);
            List<string> direction_copy=new List<string>();  
            List<string> wordArr_copy=new List<string>();
            List<int> hor_copy=new List<int>();
            List<int> ver_copy=new List<int>();
            for (int i = 0; i < wordArr.Length-1; i++)   //copy the data in array to list.
            {
                direction_copy.Add(direction[i + 1]);
                wordArr_copy.Add(wordArr[i + 1]);
                hor_copy.Add(hor[i + 1]);
                ver_copy.Add(ver[i + 1]);
                //Console.Write(wordArr_copy[i]);
            }
            countInsert(directionList, insertList, horList, verList, direction_copy, wordArr_copy, hor_copy, ver_copy);
            if (insertList.Count != wordArr.Length)
            {
                errorGroup = @"<p>Crozzle does not contain one group of connected words.</p>";
            }
            //Console.Write(test);
            Console.Write(insertList.Count);
            return errorGroup;
        }


        //Step 3 + Step 4
        public void countInsert(List<string> directionList, List<string> insertList, List<int> horList, List<int> verList, List<string> direction_copy, List<string> wordArr_copy, List<int> hor_copy, List<int> ver_copy)
        {
            int length_old = insertList.Count;
            for (int m = 0; m < insertList.Count; m++)
            {
                for (int n = 0; n < wordArr_copy.Count; n++)
                {
                    int[,] wordLoc_old = getLocation(directionList[m], insertList[m], horList[m], verList[m]);
                    int[,] wordLoc_new = getLocation(direction_copy[n], wordArr_copy[n], hor_copy[n], ver_copy[n]);
                    if (decideInsert(wordLoc_old, wordLoc_new, insertList[m], wordArr_copy[n]))
                    {
                        directionList.Add(direction_copy[n]);
                        insertList.Add(wordArr_copy[n]);
                        horList.Add(hor_copy[n]);
                        verList.Add(ver_copy[n]);
                        direction_copy.RemoveAt(n);
                        wordArr_copy.RemoveAt(n);
                        hor_copy.RemoveAt(n);
                        ver_copy.RemoveAt(n);
                        //length_new = insertList.Count;
                    }
                }
            }
            int length_new = insertList.Count;
           // Console.Write( insertList.Count);
            if (length_new != length_old)
            {
                countInsert(directionList, insertList, horList, verList, direction_copy, wordArr_copy, hor_copy, ver_copy);
            }
        }


        //Get the coordinate of word.
        public int[,] getLocation(string direction, string word, int hor, int ver)
        {
            int[,] wordLoc = new int[word.Length, 2];
            for (int i = 0; i < word.Length; i++)
            {
                if(direction == "HORIZONTAL")
                {
                    wordLoc[i, 0] = hor;
                    wordLoc[i, 1] = ver+i;
                }
                if (direction == "VERTICAL")
                {
                    wordLoc[i, 0] = hor + i;
                    wordLoc[i, 1] = ver;
                }
            }
            return wordLoc;
        }


        //To determine whether the words insert with each other
        public bool decideInsert(int[,] wordLoc_old, int[,] wordLoc_new, string word_old, string word_new)
        {
            bool insert = false;
            for (int i = 0; i < word_old.Length; i++)
            {
                for (int j = 0; j < word_new.Length; j++)
                {
                    if (wordLoc_old[i, 0] == wordLoc_new[j, 0] && wordLoc_old[i, 1] == wordLoc_new[j, 1])
                    {
                        insert = true;
                        break;
                    }
                }
            }
            return insert;
                
        }
      
        /// <summary>
        /// Check "A horizontal word is limited to intersecting at least 1 and at most 2 other vertical words".
        /// </summary>
        /// <param name="counter">number of intersection</param>
        /// <param name="word">word which need to be judged</param>
        /// <param name="direction">horizontal or vertical</param>
        /// <returns></returns>
        public string insertEasyNum(int counter, string word, string direction)
        {
            string errorInsert = null;
            if (counter > 2 || counter < 1)
            {
                errorInsert = @"<p>There is a "+direction+ @" word "+word+@" inserts " + counter + @" words, which is not correct for easy level.</p>";
            }
            return errorInsert;
        }
        //Medium intersection
        public string insertMedNum(int counter, string word, string direction)
        {
            string errorInsert = null;
            if (counter > 3 || counter < 1)
            {
                errorInsert = @"<p>There is a " + direction + @" word "+word+@" inserts " + counter + @" words, which is not correct for medium level.</p>";
            }
            return errorInsert;
        }
        //Hard level intersection
        public string insertHardNum(int counter, string word, string direction)
        {
            string errorInsert = null;
            if (counter < 1)
            {
                errorInsert = @"<p>There is a " + direction + @" word "+word+@" inserts " + counter + @" words, which is not correct for hard level.</p>";
            }
            return errorInsert;
        }
    }
}
