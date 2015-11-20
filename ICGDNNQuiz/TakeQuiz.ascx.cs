using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Security.Roles;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Skins.Controls;
using ICG.Modules.DnnQuiz.Components.Controllers;
using ICG.Modules.DnnQuiz.Components.InfoObjects;

namespace ICG.Modules.DnnQuiz
{
    /// <summary>
    ///     UI Class for actual quiz completion
    /// </summary>
    public partial class TakeQuiz : PortalModuleBase
    {
        #region Private Members (Binding Helpers)

        private bool _finished;
        private List<UserQuestionAnswerInfo> _answers;
        private int _quizId;
        private int _questionIndex;

        #endregion

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="EventArgs" /> instance containing the event data.
        /// </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Ensure that we have a quiz id
                if (Request.QueryString["quizId"] == null)
                {
                    DotNetNuke.UI.Skins.Skin.AddModuleMessage(this,
                        "QuizId Was Not Found - please check your link and try again.",
                        ModuleMessage.ModuleMessageType.RedError);
                    return;
                }

                //Decode encryption
                var decodedParamater = Server.UrlDecode(Request.QueryString["quizId"]);

                _quizId = int.Parse(UrlUtils.DecryptParameter(decodedParamater));

                if (!IsPostBack && _quizId > 0)
                {
                    //Try to obtain the quiz
                    var oQuizInfo = QuizController.GetQuizById(_quizId, ModuleId);
                    lblIntroText.Text = Localization.GetString("Header", LocalResourceFile)
                                                    .Replace("[QUIZTITLE]", oQuizInfo.QuizTitle);

                    //Bind it
                    BindAndDisplayQuestions(_quizId);
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        ///     Pulls and binds the questions!
        /// </summary>
        /// <param name="quizId"></param>
        /// <param name="oController"></param>
        private void BindAndDisplayQuestions(int quizId)
        {
            var oQuestions = QuizController.GetQuizQuestions(quizId, ModuleId);
            rptChallengeQuestions.DataSource = oQuestions;
            rptChallengeQuestions.DataBind();
            pnlChallengeDisplay.Visible = true;
        }

        /// <summary>
        ///     This method binds the list of questions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptChallengeQuestions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Get controls and data
                var oQuestion = (QuizQuestionInfo) e.Item.DataItem;
                var hfQuestionId = (HiddenField) e.Item.FindControl("hfQuestionId");
                var litPrompt = (Literal) e.Item.FindControl("litPromptText");
                var rblQuestions = (RadioButtonList) e.Item.FindControl("rblQuestions");
                var litQuestions = (Literal) e.Item.FindControl("litQuestions");
                var rfvQuestion = (RequiredFieldValidator) e.Item.FindControl("rfvQuestion");
                var promptTemplate = new StringBuilder(Localization.GetString("PromptTemplate", LocalResourceFile));

                //Store the question id
                hfQuestionId.Value = oQuestion.QuestionId.ToString();

                //Setup the prompt text
                promptTemplate.Replace("[QUESTIONNUMBER]", oQuestion.Position.ToString());
                promptTemplate.Replace("[PROMPTTEXT]", oQuestion.PromptText);
                litPrompt.Text = promptTemplate.ToString();

                //Now, bind questions in the proper manner
                if (_finished)
                {
                    //Scored and the user has taken it, get answers
                    List<string> questionList = LoadQuestionWithCorrectUserAnswer(oQuestion);

                    //Build the display list
                    var oBuilder = new StringBuilder();
                    oBuilder.Append("<ul class='Normal'>");
                    foreach (string current in questionList)
                    {
                        oBuilder.Append("<li>" + current + "</li>");
                    }
                    oBuilder.Append("</ul");
                    litQuestions.Text = oBuilder.ToString();
                    rblQuestions.Visible = false;

                    //Increment question index
                    _questionIndex++;
                }
                else
                {
                    //Allow the user to take it
                    rblQuestions.DataSource = LoadQuestion(oQuestion);
                    rblQuestions.DataBind();

                    //Enable validator
                    rfvQuestion.Enabled = true;
                    rfvQuestion.Text = Localization.GetString("AnswerRequired", LocalResourceFile);
                }
            }
            else if (e.Item.ItemType == ListItemType.Separator)
            {
                var litSeparator = (Literal) e.Item.FindControl("litSeparator");
                litSeparator.Text = Localization.GetString("SeparatorTemplate", LocalResourceFile);
            }
        }

        /// <summary>
        ///     This method returns a listing of the answers, with correct and user answers
        /// </summary>
        /// <param name="oQuestion"></param>
        /// <returns></returns>
        private List<string> LoadQuestionWithCorrectUserAnswer(QuizQuestionInfo oQuestion)
        {
            var answerList = new List<string>();

            //Grab the users answer
            var oUserInfo = _answers[_questionIndex];
            var yourCorrectAnswer = Localization.GetString("YourCorrectAnswer", LocalResourceFile);
            var correctAnswer = Localization.GetString("CorrectAnswer", LocalResourceFile);
            var yourAnswer = Localization.GetString("YourAnswer", LocalResourceFile);

            //First two, simply check for answer
            if (oQuestion.Answer1 == oQuestion.CorrectAnswer)
            {
                if (oQuestion.Answer1 == oUserInfo.Answer)
                    answerList.Add(oQuestion.Answer1 + yourCorrectAnswer);
                else
                    answerList.Add(oQuestion.Answer1 + correctAnswer);
            }
            else
            {
                if (oQuestion.Answer1 == oUserInfo.Answer)
                    answerList.Add(oQuestion.Answer1 + yourAnswer);
                else
                    answerList.Add(oQuestion.Answer1);
            }

            if (oQuestion.Answer2 == oQuestion.CorrectAnswer)
            {
                if (oQuestion.Answer2 == oUserInfo.Answer)
                    answerList.Add(oQuestion.Answer2 + yourCorrectAnswer);
                else
                    answerList.Add(oQuestion.Answer2 + correctAnswer);
            }
            else
            {
                if (oQuestion.Answer2 == oUserInfo.Answer)
                    answerList.Add(oQuestion.Answer2 + yourAnswer);
                else
                    answerList.Add(oQuestion.Answer2);
            }

            //Remaining three, check for existing and answer
            if (oQuestion.Answer3.Length > 0 & oQuestion.Answer3 == oQuestion.CorrectAnswer)
            {
                if (oQuestion.Answer3 == oUserInfo.Answer)
                    answerList.Add(oQuestion.Answer3 + yourCorrectAnswer);
                else
                    answerList.Add(oQuestion.Answer3 + correctAnswer);
            }
            else if (oQuestion.Answer3.Length > 0)
            {
                if (oQuestion.Answer3 == oUserInfo.Answer)
                    answerList.Add(oQuestion.Answer3 + yourAnswer);
                else
                    answerList.Add(oQuestion.Answer3);
            }

            if (oQuestion.Answer4.Length > 0 & oQuestion.Answer4 == oQuestion.CorrectAnswer)
            {
                if (oQuestion.Answer4 == oUserInfo.Answer)
                    answerList.Add(oQuestion.Answer4 + yourCorrectAnswer);
                else
                    answerList.Add(oQuestion.Answer4 + correctAnswer);
            }
            else if (oQuestion.Answer4.Length > 0)
            {
                if (oQuestion.Answer4 == oUserInfo.Answer)
                    answerList.Add(oQuestion.Answer4 + yourAnswer);
                else
                    answerList.Add(oQuestion.Answer4);
            }

            if (oQuestion.Answer5.Length > 0 & oQuestion.Answer5 == oQuestion.CorrectAnswer)
            {
                if (oQuestion.Answer5 == oUserInfo.Answer)
                    answerList.Add(oQuestion.Answer5 + yourCorrectAnswer);
                else
                    answerList.Add(oQuestion.Answer5 + correctAnswer);
            }
            else if (oQuestion.Answer5.Length > 0)
            {
                if (oQuestion.Answer5 == oUserInfo.Answer)
                    answerList.Add(oQuestion.Answer5 + yourAnswer);
                else
                    answerList.Add(oQuestion.Answer5);
            }

            return answerList;
        }

        /// <summary>
        ///     This method returns the questions
        /// </summary>
        /// <param name="oQuestion"></param>
        /// <returns></returns>
        private List<string> LoadQuestion(QuizQuestionInfo oQuestion)
        {
            //Start with the first two
            var answerList = new List<string> {oQuestion.Answer1, oQuestion.Answer2};

            //Remaining three, check for data then add
            if (oQuestion.Answer3.Length > 0)
                answerList.Add(oQuestion.Answer3);
            if (oQuestion.Answer4.Length > 0)
                answerList.Add(oQuestion.Answer4);
            if (oQuestion.Answer5.Length > 0)
                answerList.Add(oQuestion.Answer5);

            return answerList;
        }

        /// <summary>
        ///     This method will loop through each row, scoring the quiz, then re-binding the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmitAnswers_Click(object sender, EventArgs e)
        {
            //Fast exit if invalid
            if (!Page.IsValid)
                return;

            var oQuizInfo = QuizController.GetQuizById(_quizId, ModuleId);

            //Variables for scoring
            var correctCount = 0;
            var totalCount = 0;

            //Clear the answer list
            _answers = new List<UserQuestionAnswerInfo>();

            //Loop through the questions
            foreach (RepeaterItem oCurrentRecord in rptChallengeQuestions.Items)
            {
                //Get the controls
                var oQuestion = (QuizQuestionInfo) oCurrentRecord.DataItem;
                var hfQuestionId = (HiddenField) oCurrentRecord.FindControl("hfQuestionId");
                var rblQuestions = (RadioButtonList) oCurrentRecord.FindControl("rblQuestions");

                //Increment the total count
                totalCount++;

                //Add the users answer to the list
                var oUserInfo = new UserQuestionAnswerInfo();
                oUserInfo.Answer = rblQuestions.SelectedItem.Text;
                oUserInfo.QuestionId = int.Parse(hfQuestionId.Value);
                oUserInfo.UserId = UserId;

                //Get the question, see if they were correct
                if (oQuestion == null)
                    oQuestion = QuizController.GetQuizQuestion(oUserInfo.QuestionId, ModuleId);

                if (oQuestion.CorrectAnswer == oUserInfo.Answer)
                {
                    oUserInfo.WasCorrect = true;
                    correctCount++;
                }
                else
                    oUserInfo.WasCorrect = false;

                //Add it to the collection
                _answers.Add(oUserInfo);
            }

            //Prepare results
            var oResults = new UserQuizResultInfo
                               {
                                   DateTaken = DateTime.Now,
                                   IsMostCurrent = true,
                                   NumberCorrect = correctCount,
                                   NumberIncorrect = totalCount - correctCount,
                                   Percentage =
                                       Math.Round(
                                           (decimal.Parse(correctCount.ToString())/
                                            decimal.Parse(totalCount.ToString()))*100, 2),
                                   QuizId = _quizId,
                                   ReminderSent = false,
                                   UserId = UserId,
                                   UserIpAddress = Request.UserHostAddress
                               };

            //Declare placeholder for the result it
            int resultId;

            //Did they pass
            if (oResults.Percentage >= oQuizInfo.PassPercentage)
            {
                oResults.Passed = true;
                oResults.ExpirationDate = oQuizInfo.CanExpire
                                              ? DateTime.Now.AddDays(oQuizInfo.ExpireDuration)
                                              : DateTime.Now.AddYears(10);

                //Now save it
                resultId = QuizController.SaveUserQuizResults(oResults);

                var certificate = Globals.NavigateURL("QuizCert", "mid=" + ModuleId,
                                                      "quizId=" + UrlUtils.EncryptParameter(_quizId.ToString()),
                                                      "exp=" +
                                                      UrlUtils.EncryptParameter(
                                                          Server.UrlEncode(
                                                              oResults.ExpirationDate.ToShortDateString())),
                                                      "resId=" + resultId);
                var noSkinQsParams =
                    "&SkinSrc=[G]Skins%2f_default%2fNo+Skin&ContainerSrc=[G]Containers%2f_default%2fNo+Container";

                //Setup pass notification
                var notification = new StringBuilder(Localization.GetString("PassTemplate", LocalResourceFile));
                notification.Append(Localization.GetString("ReturnLinkTemplate", LocalResourceFile));
                notification.Replace("[YOURSCORE]", Math.Round(oResults.Percentage, 0).ToString());
                notification.Replace("[REQUIREDSCORE]",
                                     Math.Round(oQuizInfo.PassPercentage, 0).ToString());
                notification.Replace("[CERTLINK]", certificate);
                notification.Replace("[CERTLINKPRINT]", certificate + noSkinQsParams);
                notification.Replace("[RETURNLINK]", Globals.NavigateURL(TabId));
                lblStatus.Text = notification.ToString();

                //Add to role if needed
                if (!oQuizInfo.RoleToAdd.Equals("-1"))
                {
                    var oRoleController = new RoleController();
                    var oRoleToAdd = oRoleController.GetRoleByName(PortalId, oQuizInfo.RoleToAdd);
                    if (oRoleToAdd != null)
                    {
                        oRoleController.AddUserRole(PortalId, UserId, oRoleToAdd.RoleID,
                                                    oQuizInfo.CanExpire
                                                        ? DateTime.Now.AddDays(oQuizInfo.ExpireDuration)
                                                        : Null.NullDate);
                    }
                    else
                        Exceptions.LogException(
                            new ArgumentException("Unable to add role, unable to find " + oQuizInfo.RoleName +
                                                  " role for assignment"));
                }
            }
            else
            {
                oResults.Passed = false;
                oResults.ExpirationDate = Null.NullDate;

                //Now save it
                resultId = QuizController.SaveUserQuizResults(oResults);

                //Setup fail notification
                var takeQuizLink = Globals.NavigateURL("TakeQuiz", "mid=" + ModuleId,
                                                       "quizId=" + UrlUtils.EncryptParameter(_quizId.ToString()));
                var notification = new StringBuilder(Localization.GetString("FailTemplate", LocalResourceFile));
                notification.Append(Localization.GetString("ReturnLinkTemplate", LocalResourceFile));
                notification.Replace("[YOURSCORE]", Math.Round(oResults.Percentage, 0).ToString());
                notification.Replace("[REQUIREDSCORE]",
                                     Math.Round(oQuizInfo.PassPercentage, 0).ToString());
                notification.Replace("[TESTLINK]", takeQuizLink);
                notification.Replace("[RETURNLINK]", Globals.NavigateURL(TabId));
                lblStatus.Text = notification.ToString();

                //Setup the re-take link
                btnTakeAgain.NavigateUrl = takeQuizLink;
                btnTakeAgain.Visible = true;
            }

            //Hide the submit button
            btnSubmitAnswers.Visible = false;

            //Now, we need to save the question answers
            foreach (UserQuestionAnswerInfo oAnswer in _answers)
            {
                oAnswer.ResultId = resultId;
                QuizController.SaveUserQuestionAnswer(oAnswer);
            }

            //Now, flag for rebinding
            _questionIndex = 0;
            _finished = true;

            BindAndDisplayQuestions(_quizId);
        }
    }
}