/*
 * Copyright (c) 2007-2011 IowaComputerGurus Inc (http://www.iowacomputergurus.com)
 * Copyright Contact: webmaster@iowacomputergurus.com
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy 
 * of this software and associated documentation files (the "Software"), to deal 
 * in the Software without restriction, including without limitation the rights to use, 
 * copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
 * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial 
 * portions of the Software. 
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT 
 * NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE
 * */


using System;
using System.Data;
using DotNetNuke.Framework;

namespace ICG.Modules.DnnQuiz.Components
{
    public abstract class DataProvider
    {

        #region common methods

        /// <summary>
        /// var that is returned in the this singleton
        /// pattern
        /// </summary>
        private static readonly DataProvider instance;

        /// <summary>
        /// private static cstor that is used to init an
        /// instance of this class as a singleton
        /// </summary>
        static DataProvider()
        {
            instance = (DataProvider)Reflection.CreateObject("data", "ICG.Modules.DnnQuiz.Components", "");
        }

        /// <summary>
        /// Exposes the singleton object used to access the database with
        /// the conrete dataprovider
        /// </summary>
        /// <returns></returns>
        public static DataProvider Instance()
        {
            return instance;
        }

        #endregion

        #region Quiz Abstract Methods

        public abstract IDataReader GetQuizById(int quizId, int moduleId);
        public abstract IDataReader GetAllQuizzes();
        public abstract int SaveQuiz(int quizId, int moduleId, string quizTitle, string roleName, int roleId, decimal passPercentage, bool isPublished, bool canExpire, int expireDuration, string roleToAdd, bool allowRetake, int emailResultsMode, string certificateTemplatePath);

        public abstract IDataReader GetQuizzesForUserDisplay(int userId, int moduleId);
        public abstract IDataReader GetAllQuizzesForDisplay(int userId, int moduleId);
        public abstract IDataReader GetQuizzesForNotification();
        #endregion

        #region Quiz Question Abstract Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="moduleId"></param>
        /// <param name="moveNumber"></param>
        public abstract void MoveQuestion(int questionId, int moduleId, int moveNumber);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="moduleId"></param>
        public abstract void DeleteQuestion(int questionId, int moduleId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="quizId"></param>
        /// <param name="moduleId"></param>
        /// <param name="promptText"></param>
        /// <param name="answer1"></param>
        /// <param name="answer2"></param>
        /// <param name="answer3"></param>
        /// <param name="answer4"></param>
        /// <param name="answer5"></param>
        /// <param name="correctAnswer"></param>
        /// <param name="points"></param>
        public abstract void SaveQuestion(int questionId, int quizId, int moduleId, string promptText, string answer1, string answer2, string answer3, string answer4, string answer5, string correctAnswer);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="quizId"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public abstract IDataReader GetQuizQuestions(int quizId, int moduleId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public abstract IDataReader GetQuizQuestion(int questionId, int moduleId);
        public abstract void SaveUserQuestionAnswer(int resultId, int questionId, int userId, string answer, bool wasCorrect);
        /// <summary>
        /// Saves the user quiz results.
        /// </summary>
        /// <param name="quizId">The quiz id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="dateTaken">The date taken.</param>
        /// <param name="userIpAddress">The user ip address.</param>
        /// <param name="numberCorrect">The number correct.</param>
        /// <param name="numberIncorrect">The number incorrect.</param>
        /// <param name="percentage">The percentage.</param>
        /// <param name="passed">if set to <c>true</c> [passed].</param>
        /// <param name="expirationDate">The expiration date.</param>
        /// <param name="reminderSent">if set to <c>true</c> [reminder sent].</param>
        /// <param name="isMostCurrent">if set to <c>true</c> [is most current].</param>
        /// <returns></returns>
        public abstract int SaveUserQuizResults(int quizId, int userId, DateTime dateTaken, string userIpAddress, int numberCorrect, int numberIncorrect, decimal percentage, bool passed, DateTime expirationDate, bool reminderSent, bool isMostCurrent);
        #endregion

        #region Search methods
        public abstract IDataReader GetCounties();
        public abstract IDataReader SearchResults(string lastName, string company, string county, string quizzes);
        #endregion

        #region Reports Methods
        public abstract IDataReader Report_QuizResults(int quizId);
        #endregion

    }



}
