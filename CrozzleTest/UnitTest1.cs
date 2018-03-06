using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crozzle;
using System.Collections.Generic;

namespace CrozzleTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_Cacu_Score()
        {
            //arrange
            string word_test = "JOCER";
            List<int> score_test = new List<int>();
            for (int i = 0; i < 52; i++)
            {
                score_test.Add(1);
            }
            int expect_score = 5;
            //act
            ScoreSort ss = new ScoreSort();
            int act_score = ss.word_caculate(word_test, score_test);
            //assert
            Assert.AreEqual(expect_score, act_score, "Word score is caculated wrong");
        }
        [TestMethod]
        public void Test_Match()
        {
            //arrange
            string test_str = "INSERT A=2";
            int expect_num = 2;
            //act
            NewConf nc = new NewConf();
            int act_num = nc.match(test_str);
            //assert
            Assert.AreEqual(expect_num, act_num, "Match function is wrong");
        }
        [TestMethod]
        public void Test_Same_Let()
        {
            //arrange
            string word1_test = "JOHRTAD";
            string word2_test = "RFGO";
            List<int> test_1 = new List<int>() { 1, 3 };
            List<int> test_2 = new List<int>() { 3, 0 };
            List<List<int>> expect_result = new List<List<int>>();
            expect_result.Add(test_1);
            expect_result.Add(test_2);
            //act
            NewFile nf = new NewFile();
            List<List<int>> act_result = nf.same_letter(word1_test, word2_test);
            //assert
            for (int i = 0; i < act_result.Count; i++)
            {
                Assert.AreEqual(expect_result[i][0], act_result[i][0], "Same letter index for word 1 is wrong");
                Assert.AreEqual(expect_result[i][1], act_result[i][1], "Same letter index for word 2 is wrong");
            }
        }
        [TestMethod]
        public void Test_Add1d()
        {
            //arrange
            string direction = "HORIZAONTAL";
            int row = 3;
            int col = 2;
            string word = "THANK";
            int expect_len = 4;
            //act
            NewFile nf = new NewFile();
            int act_len = nf.add_1dword(direction, row, col, word).Count;
            //assert
            Assert.AreEqual(expect_len, act_len, "The word is wrong insert");
        }
        [TestMethod]
        public void Test_Within()
        {
            //arrange
            List<string> test_1 = new List<String>() { "HORIZONTAL", "2", "3", "WORD1" };
            List<string> test_2 = new List<String>() { "VERTICAL", "5", "6", "WORD2" };
            List<List<string>> test = new List<List<string>>();
            test.Add(test_1);
            test.Add(test_2);
            int row = 2;
            int col = 1;
            //act
            ValidCom vc = new ValidCom(test, row, col);
            //assert
            Assert.IsFalse(vc.within_edge(test, row, col));
        }
    }
}
