using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainTrainLogic
{
    public static class Question
    {
        public static string stringResult = String.Empty;
        public static string rightOrWrong = String.Empty;
        public static string playerstat = String.Empty;
        

        public static string checkAnswer(string userAnswer)
        {

            if (userAnswer == stringResult)
            {
                rightOrWrong = "Right!";
            }
            else
            {
                rightOrWrong = "Wrong!";
            };

            return rightOrWrong;
        }

        public static string generateProblem(PlayerStats round)
        {
            Random aa = new Random();

            int a = aa.Next(round.numRange);
            int b = aa.Next(round.numRange);
            int a1 = aa.Next(round.lvl2NumRange);
            int b1 = aa.Next(round.lvl2NumRange);
            int sign = aa.Next(round.signRange);
            string question = String.Empty;
            int result;

            switch (sign)
            {
                case 0:
                    result = a + b;
                    stringResult = result.ToString();
                    question = a.ToString() + '+' + b.ToString();
                    break;

                case 1:
                    result = a - b;
                    stringResult = result.ToString();
                    question = a.ToString() + '-' + b.ToString();
                    break;

                case 2:
                    result = a1 * b1;
                    stringResult = result.ToString();
                    question = a1.ToString() + '*' + b1.ToString();
                    break;
               
                //case 3:
                //    result = a1 / b1;
                //    stringResult = result.ToString();
                //    question = a1.ToString() + '/' + b1.ToString();
                //    break;
            }

            return question;
              
        }
        public static void levelUP(PlayerStats round)
        {
            round.level++;
            round.numRange += 10;
            if (round.level == 5)
            {
                round.signRange = 3;
            }

            if (round.level % 10 == 0)
            {
                round.lvl2NumRange += 5;
            }
        }
    }
}
