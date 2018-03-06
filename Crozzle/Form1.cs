using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crozzle
{
    /// <summary>
    /// Open file dialog and invoke functions
    /// </summary>
    public partial class Form1 : Form

    {
        //Assignment 2
        List<int> score = new List<int>();
        List<List<List<string>>> group_3dword = new List<List<List<string>>>();
        List<ScoreArr> score_array = new List<ScoreArr>();
        List<ScoreList> score_2d = new List<ScoreList>();
        Crozzle crozzleDis = new Crozzle();
        int point_per;
        const string h2_head=@"<h2>";
        const string h2_foot=@"</h2><br>";
        const string title = "Score:";
        const string time_head = @"<h2>Time:";
        const string time_m = @" m";
        const string time_s = @" s";
        //Assignment 1
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
        string errConfiSecond = null;
        string errorConfNum = null;
        string errorConfGroup = null;
        int[] chaScore = new int[52]; //Store the score of each letter
        int point; //point per word

        /// <summary>
        /// Name tab pages
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            this.tabCrozzle.TabPages[0].Text = "Crozzle";
            this.tabCrozzle.TabPages[1].Text = "Error";
        }
        
        /// <summary>
        /// Step 1: Open file dialog
        /// Step 2: Select input file
        /// Step 3: Obtain filename
        /// Step 4: Validate input file and crozzle
        /// Step 5: Calculate score
        /// Step 6: Display crozzle, score, and errors and log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();//Define OpenFileDialog
            open.Title = "Open Folders";//Dialog Title
            open.Filter = "File（.txt）|*.txt";//File extension
            if (open.ShowDialog() == DialogResult.OK)
            {
                ValidFile val = new ValidFile();
                //Succeed
                string file = open.FileName;
                MessageBox.Show(file, "selected reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StreamReader sr1 = new StreamReader(open.FileName);
                int lineNum = sr1.ReadToEnd().Split('\n').Length - 2;  //obtain the number of word in crozzle
                sr1.Close();

                StreamReader sr = new StreamReader(open.FileName);
                char[] separator = { ',' };
                string line;
                string[] wordArr = new string[lineNum];
                int[] hor=new int[lineNum];
                int[] ver=new int[lineNum];
                string[] direction = new string[lineNum];
                string[] firstLine = sr.ReadLine().Split(separator);
                string errorFirstLen = @"<p>"+ val.validdateFirstLen(firstLine.Length) +@"</p>";
                string[] secondLine = sr.ReadLine().Split(separator);
                string errorWordList = null;
                //validate whether the second line match specific pattern
                for (int m = 0; m < secondLine.Length; m++)
                {
                    if (secondLine[m] == "")
                    {
                        errorWordList = @"<p>There is a space in wold list, which is invalid.</p>"+errorWordList;
                    }
                    if (!val.validateIfWord(secondLine[m]))
                        errorWordList = @"<p>" + secondLine[m] + @" is an invalid word in word list" + errorWordList;
                }

                string level=firstLine[0];
                string[] errorFirst = new string[firstLine.Length-1];
                int[] numFirst = new int[firstLine.Length-1];
                string firstError=null;
                //Validate whether the first line match specific pattern
                for (int k = 0; k < firstLine.Length - 1; k++) 
                {
                    if (val.validateIfNum(firstLine[k+1]))
                    {
                        numFirst[k] = Convert.ToInt16(firstLine[k+1]);
                    }
                    else if(firstLine[k+1]=="")
                    {
                        errorFirst[k] = @"<p>There is a space in the first line, which is invalid.</p>";
                    }
                    else
                    {
                        errorFirst[k] = @"<p>"+firstLine[k+1] + @" is not a number in the first line.</p>";
                        
                    }firstError = firstError + errorFirst[k];
                }

                int wordList = numFirst[0];
                string errorActNum = val.validateActuNum(wordList, secondLine);
                int rowsNum = numFirst[1];
                int colsNum = numFirst[2];
                char[,] wordcha = new char[rowsNum, colsNum];
                char[,] wordHor = new char[rowsNum, colsNum];
                char[,] wordVer = new char[rowsNum, colsNum];
                for (int rows = 0; rows < rowsNum; rows++)
                {
                    for (int cols = 0; cols < colsNum; cols++)
                    {
                        wordcha[rows, cols] = '\0';
                    }
                }

                //Crozzle crozzleDis = new Crozzle();
                string errorDirec = null;
                string errorWord = null;
                string errorEdge = null;
                int i = 0;
                //start to read input file from the third line
                while ((line = sr.ReadLine()) != null)
                {
                    string[] wordSpl = line.Split(separator);
                    //separate and store each line to an array
                    direction[i] = wordSpl[0];
                    hor[i] = Convert.ToInt16(wordSpl[1]);
                    ver[i] = Convert.ToInt16(wordSpl[2]);
                    wordArr[i] = wordSpl[3];
                    if (!val.validateIfWord(wordArr[i])) //check whether word is a word
                        errorWord = @"<p>" + errorWord + wordArr[i] + @" is an invalid word in crozzle";
                    if (!val.validateDirec(direction[i])) //check whether direction is HORIZONTAL or VERTICAL
                        errorDirec = errorDirec + @"<p>There is a word " + direction[i] + @" cannot be seen as direction. (HORIZONTAL or VERTICAL)</p> "; 
                    if (direction[i] == "HORIZONTAL")
                    {
                        if (val.validateEdgeHor(ver[i], colsNum, hor[i], rowsNum, wordArr[i]) == null)
                        {
                            crozzleDis.addHor(hor[i], ver[i], wordArr[i], wordcha, wordHor); //add word to crozzle array and horizontal array
                        }
                        else
                        {
                            errorEdge = errorEdge + val.validateEdgeHor(ver[i], colsNum, hor[i], rowsNum, wordArr[i]);
                        }
                        
                    }
                    if (direction[i] == "VERTICAL")
                    {
                        if (val.validateEdgeVer(hor[i], rowsNum,ver[i], colsNum, wordArr[i]) == null)
                        {
                            crozzleDis.addVer(hor[i], ver[i], wordArr[i], wordcha, wordVer); 
                        }
                        else
                        {
                            errorEdge = errorEdge + val.validateEdgeVer(hor[i], rowsNum, ver[i], colsNum, wordArr[i]);
                        }
                    }
                    i++;

                }
                Configuration config = new Configuration();
                ValidCro valCro = new ValidCro();
                int[] insert=new int[lineNum];
                string errorInsert = null;
                string errorGroup = null;
                char[,] insertArr = new char[rowsNum, colsNum];
                config.storeInsert(wordHor, wordVer, rowsNum, colsNum, insertArr);
                //check different level constraints
                for (int m = 0; m < wordArr.Length; m++)
                {
                    if (direction[m] == "HORIZONTAL")
                    {
                        if (val.validateEdgeHor(ver[m], colsNum, hor[m], rowsNum, wordArr[m]) == null)
                        {
                            insert[m]=valCro.InsertHor(hor[m], ver[m], wordArr[m], insertArr, rowsNum, colsNum);
                            if (level == "EASY")
                            {
                                errorInsert = errorInsert + valCro.insertEasyNum(insert[m], wordArr[m], direction[m]);
                            }
                            if (level == "MEDIUM")
                            {
                                errorInsert = errorInsert + valCro.insertMedNum(insert[m], wordArr[m], direction[m]);
                            }
                            if (level == "HARD")
                            {
                                errorInsert = errorInsert + valCro.insertHardNum(insert[m], wordArr[m], direction[m]);
                            }

                        }
                        
                    }
                    if (direction[m] == "VERTICAL")
                    {
                        if (val.validateEdgeVer(hor[m], rowsNum, ver[m], colsNum, wordArr[m]) == null)
                        {
                            insert[m] = valCro.InsertVer(hor[m], ver[m], wordArr[m], insertArr, rowsNum, colsNum);
                            if (level == "EASY")
                            {
                                errorInsert = errorInsert + valCro.insertEasyNum(insert[m], wordArr[m], direction[m]);
                            }
                            if (level == "MEDIUM")
                            {
                                errorInsert = errorInsert + valCro.insertMedNum(insert[m], wordArr[m], direction[m]);
                            }
                            if (level == "HARD")
                            {
                                errorInsert = errorInsert + valCro.insertHardNum(insert[m], wordArr[m], direction[m]);
                            }

                        }
                        
                    }

                        
                }
                string errorTouch = null;
                if (level == "EASY") //check touch
                {
                    errorTouch=valCro.easyTouchHor(wordHor, rowsNum, colsNum)+ valCro.easyTouchVer(wordVer, rowsNum, colsNum);
                   
                }
                if (level == "HARD") //check group
                {
                    errorGroup = valCro.decideGroup(direction, wordArr, hor, ver);
                }
             
                int score = config.score(config.decideInsert(wordHor, wordVer, rowsNum, colsNum), chaScore); //caculate score
                string error = errorConfGroup + errConfiSecond + errorConfNum + val.validateWordListNum(wordList) + val.validateLevel(level) + val.validateRowsNum(rowsNum) + val.validateColNum(colsNum) + val.validateWordExist(wordArr, secondLine) + val.validateRepeat(wordArr) + errorTouch + errorGroup + firstError + errorFirstLen + errorActNum + errorInsert + errorWordList + errorDirec + errorEdge + errorWord;
               
                int finalSco = 0;
                if (error == "<p></p>")
                {
                    finalSco = point * wordArr.Length + score;
                    error = "No Errors";
                }
                
                string scoreDis = @"<h1>Score: " + finalSco + @"</h1>";
                string levelDis = @"<h3>Level: " + level + @"</h3>";
                //display
                webBrowsercrozzle.DocumentText = header + crozzleDis.display(rowsNum, colsNum, wordcha) + scoreDis + levelDis +  footer;
                webBrowsererror.DocumentText = header + error + footer; 
                //log errors
                LogFile lf = new LogFile();
                lf.Log(error, "error");
            }
        }

        private void openConfiguarionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            errorConfGroup = null;
            errorConfNum = null;
            errConfiSecond = null;
            OpenFileDialog open = new OpenFileDialog();//new open file dialog
            open.Title = "Open Folders";//title
            open.Filter = "File（.txt）|*.txt";//extension
            if (open.ShowDialog() == DialogResult.OK)
            {
                //succeed
                string file = open.FileName;
                MessageBox.Show(file, "selected reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StreamReader sr = new StreamReader(open.FileName);
                string strGroup = sr.ReadLine();
                string pointPerWord = sr.ReadLine();
                ValidFile valFil = new ValidFile();
                string regex = @"(\d+)";
                errorConfGroup = valFil.valConfGroup(strGroup);
                int group = match(regex, strGroup);
                if (valFil.valConfSecond(pointPerWord) != null)
                {
                    errConfiSecond = valFil.valConfSecond(pointPerWord);
                }
                else
                {
                    point = match(regex, pointPerWord);
                }
                
                int i = 0;
                string line;
                //int[] chaScore = new int[52];
                //stat to read configuration file from the third line
                while ((line = sr.ReadLine()) != null)
                {
                    if (valFil.valConfNum(line) != null)
                    {
                        errorConfNum = valFil.valConfNum(line) +errorConfNum;
                    }
                    else
                    {
                        chaScore[i] = match(regex, line);
                    }
                    
                    //Console.Write(chaScore[i]);
                    i++;
                }
            }
        }
        /// <summary>
        /// Obtain the number from the third line to the last
        /// </summary>
        /// <param name="regex">pattern</param>
        /// <param name="str">string</param>
        /// <returns></returns>
        public int match(string regex, string str)
        {
            Match mstr = Regex.Match(str, regex);
            int number = Convert.ToInt16(mstr.Groups[1].Value);
            return number;
        }

        private void webBrowsercrozzle_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("First step: Click 'New Configuration to choose a file. Second step: Click 'New File to choose a file.");
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void existToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
        /// <summary>
        /// Open file and create the new crozzle and score
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DeepClone dc = new DeepClone();
            CacScore cs = new CacScore();
            SaveCro sc = new SaveCro();
            Easy easy = new Easy();
            List<List<List<string>>> result = new List<List<List<string>>>();
            List<List<List<string>>> final_3dword = new List<List<List<string>>>();
            List<List<List<string>>> sort_3d = new List<List<List<string>>>();
            List<ScoreList> nosort_result = new List<ScoreList>();
            NewFile nf = new NewFile();
            nf.main();
            DateTime start = DateTime.Now;
            Console.WriteLine(start);
            ScoreSort ss = new ScoreSort();
            score_array = ss.store_score_array(nf.word_list_copy, score);//get score of each word
            //sort word according to score
            List<ScoreArr> sorted_word = score_array.OrderByDescending(word => word.Score_Word).ThenBy(word => word.Word.Length).ToList();
            List<string> sort_list = new List<string>();
            sort_list = nf.str_list(sorted_word);
            group_3dword = nf.insert_add(sort_list);
            score_2d = ss.store_score_list(group_3dword, score);
            List<ScoreList> sorted_list = score_2d.OrderByDescending(word => word.Score_Word).ToList();
            sort_3d = nf.str_list(sorted_list);
            if (nf.level == easy.Level)
            {
                List<List<string>> test = new List<List<string>>();
                final_3dword.Add(sort_3d[19]);
                result = nf.add_more(final_3dword, 0);
            }
            else
            {
                final_3dword.Add(sort_3d[0]);
                result = nf.add_group(final_3dword, sort_3d, 0);
            }
            foreach (List<List<string>> child in result)
            {
                int final_score = cs.score(child, point_per, score, nf.rows, nf.cols);
                nosort_result.Add(new ScoreList(final_score, child));
            }
            List<ScoreList> sort_result = nosort_result.OrderByDescending(word => word.Score_Word).ToList();
            char[,] word_all = cs.store_all(sort_result[0].Word_List, nf.rows, nf.cols);
            DateTime end = DateTime.Now;
            Console.WriteLine(end);
            TimeSpan ts = end - start;
            string time = time_head + ts.Minutes.ToString() + time_m + ts.Seconds.ToString() + time_s + h2_foot;
            webBrowsercrozzle.DocumentText = crozzleDis.header + h2_head + title + sort_result[0].Score_Word + crozzleDis.display(nf.rows, nf.cols, word_all) + time + crozzleDis.footer;
            string hv_num = sc.cacu_hv(sort_result[0].Word_List);
            string first = nf.level + "," + nf.word_num + "," + Convert.ToString(nf.rows)+ "," + Convert.ToString(nf.cols) + "," + hv_num;
            sc.write_txt(first, sort_result[0].Word_List, nf.word_list_copy, nf.path, nf.level);
        }

        /// <summary>
        /// Open configuration file and caculate score
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            NewConf nc = new NewConf();
            nc.main();
            score = nc.score;
            point_per = nc.point_per_word;
        }
        
    }
}
