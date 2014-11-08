using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BrainTrainWindowsPhone.Resources;
using BrainTrainLogic;
using System.Windows.Threading;

namespace BrainTrainWindowsPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        public static PlayerStats round;
        TimeSpan timeToAnswerTheQuestion;
        TimeSpan timerResolution = new TimeSpan(0, 0, 0, 1);
        DispatcherTimer tickingTimer = new DispatcherTimer();

        public MainPage()
        {
            this.InitializeComponent();
            this.tickingTimer.Interval = timerResolution;
            tickingTimer.Tick += tickingTimer_Tick;
            ButtonDisable();
            StartButtonDisable();
            Rules.IsOpen = true;
        }
        
        void tickingTimer_Tick(object sender, EventArgs e)
        {
            if (timeToAnswerTheQuestion.TotalMilliseconds <= 0)
            {
                tickingTimer.Stop();
                healtPointsSub();
                DefaultState();
                playerHP.Text = (round.healthPoints).ToString();
            }
            else
            {
                timeToAnswerTheQuestion = timeToAnswerTheQuestion.Subtract(timerResolution);
            }
            timerLabel.Text = timeToAnswerTheQuestion.TotalSeconds.ToString();
        }
        public void start_Click(object sender, RoutedEventArgs e)
        {
            ButtonEnable();
            round = new PlayerStats();                               
            questionDeploy();
            timerLabel.Text = timeToAnswerTheQuestion.TotalSeconds.ToString();           
        }
        public void questionDeploy()
        {   
            timeToAnswerTheQuestion = new TimeSpan(0, 0, 5);
            playerHP.Text = (round.healthPoints).ToString();
            score.Text = round.score.ToString();
            questionLabel.Text = Question.generateProblem(round) + "=?";
            tickingTimer.Start();
            LevelLabel.Text = round.level.ToString();
        }
        private void enter_Click(object sender, RoutedEventArgs e)
        {
            questionLabel.Text = Question.checkAnswer(inputs_output.Text);
            inputs_output.Text = String.Empty;
            answerResultProcess(questionLabel.Text);     
        }
        public void answerResultProcess(string result)
        {
            if (result == "Wrong!")
            {
                healtPointsSub();
                DefaultState();            
            }
            else
            {
                round.score++;
                if(round.score % 5 == 0 )
                {
                    Question.levelUP(round);
                    LevelLabel.Text = round.level.ToString();
                }
                questionDeploy();
            }
        }
        public static void healtPointsSub()
        {
            round.healthPoints--;
        }
        private void number_btn_Click(object sender, RoutedEventArgs e)
        {
            inputs_output.Text += ((Button)sender).Content;
        }
        private void DefaultState()
        {
            if (round.healthPoints == 0)
            {
                questionLabel.Text = "Game Over. Your score: " + round.score.ToString();
                playerHP.Text = "0";          
                tickingTimer.Stop();
                timerLabel.Text = "0";
                ButtonDisable();               
                inputs_output.Text = String.Empty;
            }
            else
            {
                questionDeploy();
            }                                 
        }
        public void ButtonDisable()
        {
            number1_btn.IsEnabled = false;
            number2_btn.IsEnabled = false;
            number3_btn.IsEnabled = false;
            number4_btn.IsEnabled = false;
            number5_btn.IsEnabled = false;
            number6_btn.IsEnabled = false;
            number7_btn.IsEnabled = false;
            number8_btn.IsEnabled = false;
            number9_btn.IsEnabled = false;
            minus_sign.IsEnabled = false;
            btn0.IsEnabled = false;
            enter.IsEnabled = false;
            backspace.IsEnabled = false;
        }
        public void ButtonEnable()
        {
            number1_btn.IsEnabled = true;
            number2_btn.IsEnabled = true;
            number3_btn.IsEnabled = true;
            number4_btn.IsEnabled = true;
            number5_btn.IsEnabled = true;
            number6_btn.IsEnabled = true;
            number7_btn.IsEnabled = true;
            number8_btn.IsEnabled = true;
            number9_btn.IsEnabled = true;
            minus_sign.IsEnabled = true;
            btn0.IsEnabled = true;
            enter.IsEnabled = true;
            backspace.IsEnabled = true;
        }
        private void backspace_Click(object sender, RoutedEventArgs e)
        {
            if (inputs_output.Text.Length != 0)
            {
                inputs_output.Text = inputs_output.Text.Remove(inputs_output.Text.Length - 1, 1);
            }
        }
        private void rulesButton_Click(object sender, RoutedEventArgs e)
        {
            Rules.IsOpen = false;
            start.IsEnabled = true;
        }
        private void StartButtonDisable()
        {
            start.IsEnabled = false;
        }
    }
}