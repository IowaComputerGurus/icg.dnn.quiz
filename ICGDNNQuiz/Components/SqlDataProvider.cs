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
using DotNetNuke.Framework.Providers;
using Microsoft.ApplicationBlocks.Data;

namespace ICG.Modules.DnnQuiz.Components
{
    public class SqlDataProvider : DataProvider
    {


        #region vars

        private const string providerType = "data";
        private const string moduleQualifier = "ICG_DQuiz_";

        private ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration(providerType);
        private string connectionString;
        private string providerPath;
        private string objectQualifier;
        private string databaseOwner;

        #endregion

        #region cstor

        /// <summary>
        /// cstor used to create the sqlProvider with required parameters from the configuration
        /// section of web.config file
        /// </summary>
        public SqlDataProvider()
        {
            Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];
            connectionString = DotNetNuke.Common.Utilities.Config.GetConnectionString();

            if (connectionString == string.Empty)
                connectionString = provider.Attributes["connectionString"];

            providerPath = provider.Attributes["providerPath"];

            objectQualifier = provider.Attributes["objectQualifier"];
            if (objectQualifier != string.Empty && !objectQualifier.EndsWith("_"))
                objectQualifier += "_";

            databaseOwner = provider.Attributes["databaseOwner"];
            if (databaseOwner != string.Empty && !databaseOwner.EndsWith("."))
                databaseOwner += ".";
        }

        #endregion

        #region properties

        public string ConnectionString
        {
            get { return connectionString; }
        }


        public string ProviderPath
        {
            get { return providerPath; }
        }

        public string ObjectQualifier
        {
            get { return objectQualifier; }
        }


        public string DatabaseOwner
        {
            get { return databaseOwner; }
        }

        #endregion

        #region private methods

        private string GetFullyQualifiedName(string name)
        {
            return DatabaseOwner + ObjectQualifier + moduleQualifier + name;
        }

        private object GetNull(object field)
        {
            return DotNetNuke.Common.Utilities.Null.GetNull(field, DBNull.Value);
        }

        #endregion

        #region Quiz Override Methods
        public override IDataReader GetQuizById(int quizId, int moduleId)
        {
            return SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetQuizById"), quizId, moduleId);
        }
        public override IDataReader GetAllQuizzes()
        {
            return SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetAllQuizzes"));
        }
        public override int SaveQuiz(int quizId, int moduleId, string quizTitle, string roleName, int roleId, decimal passPercentage, bool isPublished, bool canExpire, int expireDuration, string roleToAdd, bool allowRetake)
        {
            return int.Parse(SqlHelper.ExecuteScalar(connectionString, GetFullyQualifiedName("SaveQuiz"), quizId, moduleId, quizTitle, roleName, roleId, passPercentage, isPublished, canExpire, expireDuration, roleToAdd, allowRetake).ToString());
        }

        public override IDataReader GetQuizzesForUserDisplay(int userId, int moduleId)
        {
            return SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetQuizzesForUserDisplay"), userId, moduleId);
        }

        public override IDataReader GetAllQuizzesForDisplay(int userId, int moduleId)
        {
            return SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetAllQuizzesForDisplay"), userId, moduleId);
        }

        public override IDataReader GetQuizzesForNotification()
        {
            return SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetQuizzesForNotification"));
        }
        #endregion

        #region Quiz Question Override Methods
        public override void MoveQuestion(int questionId, int moduleId, int moveNumber)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("MoveQuestion"), questionId, moduleId, moveNumber);
        }
        public override void DeleteQuestion(int questionId, int moduleId)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("DeleteQuestion"), questionId, moduleId);
        }
        public override void SaveQuestion(int questionId, int quizId, int moduleId, string promptText, string answer1, string answer2, string answer3, string answer4, string answer5, string correctAnswer)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("SaveQuestion"), questionId, quizId, moduleId, promptText, answer1, answer2, answer3, answer4, answer5, correctAnswer);
        }
        public override IDataReader GetQuizQuestions(int quizId, int moduleId)
        {
            return SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetQuizQuestions"), quizId, moduleId);
        }
        public override IDataReader GetQuizQuestion(int questionId, int moduleId)
        {
            return SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetQuizQuestion"), questionId, moduleId);
        }
        public override void SaveUserQuestionAnswer(int resultId, int questionId, int userId, string answer, bool wasCorrect)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("SaveUserQuestionAnswer"), resultId, questionId, userId, answer, wasCorrect);
        }
        public override int SaveUserQuizResults(int quizId, int userId, DateTime dateTaken, string userIpAddress, int numberCorrect, int numberIncorrect, decimal percentage, bool passed, DateTime expirationDate, bool reminderSent, bool isMostCurrent)
        {
            return int.Parse(SqlHelper.ExecuteScalar(connectionString, GetFullyQualifiedName("SaveUserQuizResults"), quizId, userId, dateTaken, userIpAddress, numberCorrect, numberIncorrect, percentage, passed, GetNull(expirationDate), reminderSent, isMostCurrent).ToString());
        }
        public override void RecordNotificationSent(int resultId)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("RecordNotificationSent"), resultId);
        }
        #endregion

        #region Settings Methods
        public override IDataReader GetSettings()
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetSettings"));
        }

        public override void SaveSettings(string notificationText)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("SaveSettings"), notificationText);
        }
        #endregion

        #region Search methods
        public override IDataReader GetCounties()
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetCounties"));
        }
        public override IDataReader SearchResults(string lastName, string company, string county, string quizzes)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("SearchResults"), lastName, company, county, quizzes);
        }
        #endregion

        #region Reports Methods
        public override IDataReader Report_QuizResults(int quizId)
        {
            return SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("Report_QuizResults"), quizId);
        }
        #endregion

    }
}
