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

using System.Collections.Generic;
using DotNetNuke.Common.Utilities;
using ICG.Modules.DnnQuiz.Components.InfoObjects;

namespace ICG.Modules.DnnQuiz.Components.Controllers
{
    public static class QuizController
    {
        #region Quiz Methods
        /// <summary>
        /// Gets the quiz by id.
        /// </summary>
        /// <param name="quizId">The quiz id.</param>
        /// <param name="moduleId">The module id.</param>
        /// <returns></returns>
        public static QuizInfo GetQuizById(int quizId, int moduleId)
        {
            return CBO.FillObject<QuizInfo>(DataProvider.Instance().GetQuizById(quizId, moduleId));
        }
        /// <summary>
        /// Gets all quizzes.
        /// </summary>
        /// <returns></returns>
        public static List<QuizInfo> GetAllQuizzes()
        {
            return CBO.FillCollection<QuizInfo>(DataProvider.Instance().GetAllQuizzes());
        }
        /// <summary>
        /// Saves the quiz.
        /// </summary>
        /// <param name="oInfo">The o info.</param>
        /// <returns></returns>
        public static int SaveQuiz(QuizInfo oInfo)
        {
            return DataProvider.Instance().SaveQuiz(oInfo.QuizId, oInfo.ModuleId, oInfo.QuizTitle, oInfo.RoleName, oInfo.RoleId, oInfo.PassPercentage, oInfo.IsPublished, oInfo.CanExpire, oInfo.ExpireDuration, oInfo.RoleToAdd, oInfo.AllowRetake);
        }

        /// <summary>
        /// Gets the quizzes for user display.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="moduleId">The module id.</param>
        /// <returns></returns>
        public static List<UserQuizDisplay> GetQuizzesForUserDisplay(int userId, int moduleId)
        {
            return CBO.FillCollection<UserQuizDisplay>(DataProvider.Instance().GetQuizzesForUserDisplay(userId, moduleId));
        }

        /// <summary>
        /// Gets all quizzes for display.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <param name="moduleId">The module id.</param>
        /// <returns></returns>
        public static List<UserQuizDisplay> GetAllQuizzesForDisplay(int userid, int moduleId)
        {
            return CBO.FillCollection<UserQuizDisplay>(DataProvider.Instance().GetAllQuizzesForDisplay(userid, moduleId));
        }

        /// <summary>
        /// Gets the quizzes for notification.
        /// </summary>
        /// <returns></returns>
        public static List<QuizNotificationInfo> GetQuizzesForNotification()
        {
            return CBO.FillCollection<QuizNotificationInfo>(DataProvider.Instance().GetQuizzesForNotification());
        }
        #endregion

        #region Quiz Question Methods
        /// <summary>
        /// Moves the question.
        /// </summary>
        /// <param name="questionId">The question id.</param>
        /// <param name="moduleId">The module id.</param>
        /// <param name="moveValue">The move value.</param>
        public static void MoveQuestion(int questionId, int moduleId, int moveValue)
        {
            DataProvider.Instance().MoveQuestion(questionId, moduleId, moveValue);
        }

        /// <summary>
        /// Deletes the question.
        /// </summary>
        /// <param name="questionId">The question id.</param>
        /// <param name="moduleId">The module id.</param>
        public static void DeleteQuestion(int questionId, int moduleId)
        {
            DataProvider.Instance().DeleteQuestion(questionId, moduleId);
        }

        /// <summary>
        /// Saves the question.
        /// </summary>
        /// <param name="oInfo">The o info.</param>
        public static void SaveQuestion(QuizQuestionInfo oInfo)
        {
            DataProvider.Instance().SaveQuestion(oInfo.QuestionId, oInfo.QuizId, oInfo.ModuleId, oInfo.PromptText, oInfo.Answer1, oInfo.Answer2, oInfo.Answer3, oInfo.Answer4, oInfo.Answer5, oInfo.CorrectAnswer);
        }

        /// <summary>
        /// Gets the quiz questions.
        /// </summary>
        /// <param name="quizId">The quiz id.</param>
        /// <param name="moduleId">The module id.</param>
        /// <returns></returns>
        public static List<QuizQuestionInfo> GetQuizQuestions(int quizId, int moduleId)
        {
            return CBO.FillCollection<QuizQuestionInfo>(DataProvider.Instance().GetQuizQuestions(quizId, moduleId));
        }

        /// <summary>
        /// Gets the quiz question.
        /// </summary>
        /// <param name="questionId">The question id.</param>
        /// <param name="moduleId">The module id.</param>
        /// <returns></returns>
        public static QuizQuestionInfo GetQuizQuestion(int questionId, int moduleId)
        {
            return CBO.FillObject<QuizQuestionInfo>(DataProvider.Instance().GetQuizQuestion(questionId, moduleId));
        }
        /// <summary>
        /// Saves the user question answer.
        /// </summary>
        /// <param name="oInfo">The o info.</param>
        public static void SaveUserQuestionAnswer(UserQuestionAnswerInfo oInfo)
        {
            DataProvider.Instance().SaveUserQuestionAnswer(oInfo.ResultId, oInfo.QuestionId, oInfo.UserId, oInfo.Answer, oInfo.WasCorrect);
        }
        /// <summary>
        /// Saves the user quiz results.
        /// </summary>
        /// <param name="oInfo">The o info.</param>
        /// <returns></returns>
        public static int SaveUserQuizResults(UserQuizResultInfo oInfo)
        {
            return DataProvider.Instance().SaveUserQuizResults(oInfo.QuizId, oInfo.UserId, oInfo.DateTaken, oInfo.UserIpAddress, oInfo.NumberCorrect, oInfo.NumberIncorrect, oInfo.Percentage, oInfo.Passed, oInfo.ExpirationDate, oInfo.ReminderSent, oInfo.IsMostCurrent);
        }

        #endregion
        
    }
}
