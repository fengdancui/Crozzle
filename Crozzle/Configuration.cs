using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle
{
    /// <summary>
    /// Calculate score
    /// </summary>
    class Configuration
    {
        /// <summary>
        /// Calculate the corresponding product of two one-dimensional arrays
        /// </summary>
        /// <param name="charNum">An array store the frequency of each letter</param>
        /// <param name="chaScore">An array to store the score of each letter</param>
        /// <returns></returns>
        public int score(int[] charNum, int[] chaScore)
        {
            int finalScore = 0;
            for (int i = 0; i < charNum.Length; i++)
            {
                finalScore = charNum[i] * chaScore[i] + finalScore;
            }
            return finalScore;
        }
        

        /// <summary>
        /// Obtain the frequency of each letter
        /// </summary>
        /// <param name="hor"></param>
        /// <param name="ver"></param>
        /// <param name="horNum"></param>
        /// <param name="verNum"></param>
        /// <returns></returns>
        public int[] decideInsert(char[,] hor, char[,] ver, int horNum, int verNum)
        {
            int[] chaNum=new int[52];
            int indexInsert = 0;
            for(int i=0;i<chaNum.Length;i++){
                chaNum[i]=0;
            }
            for(int i=0; i <horNum; i++){
                for (int j = 0; j < verNum; j++)
                {
                    if (hor[i, j] == ver[i, j]&& hor[i,j]!='\0'&&ver[i,j]!='\0')
                    {
                        indexInsert=(int)hor[i,j]-65;
                        chaNum[indexInsert] = chaNum[indexInsert] + 1;
                    }
                    else if(hor[i,j]!=ver[i,j]&&hor[i,j]=='\0'&&ver[i,j]!='\0')
                    {
                        int indexNonVer = (int)ver[i, j] -65 + 26;
                        chaNum[indexNonVer] = chaNum[indexNonVer] + 1;
                    }
                    else if (hor[i, j] != ver[i, j] && hor[i, j] != '\0' && ver[i, j] == '\0')
                    {
                        int indexNonHor = (int)hor[i, j] -65 + 26;
                        chaNum[indexNonHor] = chaNum[indexNonHor] + 1;
                    }
                }
            }
            return chaNum;
        }


        /// <summary>
        /// Store intersection letter to an array
        /// </summary>
        /// <param name="hor"></param>
        /// <param name="ver"></param>
        /// <param name="horNum"></param>
        /// <param name="verNum"></param>
        /// <param name="insert"></param>
        public void storeInsert(char[,] hor, char[,] ver, int horNum, int verNum, char[,] insert)
        {
            for (int m = 0; m < horNum; m++)
            {
                for (int n = 0; n < verNum; n++)
                {
                    insert[m, n] = '\0';
                }
            }
                for (int i = 0; i < horNum; i++)
                {
                    for (int j = 0; j < verNum; j++)
                    {
                        if (hor[i, j] == ver[i, j] && hor[i, j] != '\0' && ver[i, j] != '\0')
                        {
                            insert[i, j] = hor[i, j];
                        }
                    }
                }
        }
    }
}
