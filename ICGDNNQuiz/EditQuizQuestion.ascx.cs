/*
 * Copyright (c) 2007-2021 IowaComputerGurus Inc (http://www.iowacomputergurus.com)
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
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.UI.UserControls;
using ICG.Modules.DnnQuiz.Components.Controllers;
using ICG.Modules.DnnQuiz.Components.InfoObjects;

namespace ICG.Modules.DnnQuiz
{
    /// <summary>
    /// UI for question editing
    /// </summary>
    public partial class EditQuizQuestion : PortalModuleBase
    {
        private int _questionId = -1;
        private int _quizId = -1;


        /// <summary>
        /// DotNetNuke Text Editor Declaration for Runtime Support.  DO NOT REMOVE!!
        /// </summary>
        protected TextEditor txtPromptTextRich;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Load parameters
            if (Request.QueryString["questionId"] != null)
                _questionId = int.Parse(Request.QueryString["questionId"]);
            if (Request.QueryString["quizId"] != null)
                _quizId = int.Parse(Request.QueryString["quizId"]);

            if (!IsPostBack)
            {
                if (_questionId > 0 && _quizId > 0)
                {
                    //Load the question
                    var questionInfo = QuizController.GetQuizQuestion(_questionId, this.ModuleId);

                    if (questionInfo != null)
                    {
                        txtPromptTextRich.Text = questionInfo.PromptText;
                        txtAnswer1.Text = questionInfo.Answer1;
                        txtAnswer2.Text = questionInfo.Answer2;
                        txtAnswer3.Text = questionInfo.Answer3;
                        txtAnswer4.Text = questionInfo.Answer4;
                        txtAnswer5.Text = questionInfo.Answer5;

                        //Take action for correct answers
                        if (questionInfo.CorrectAnswer.Equals(txtAnswer1.Text))
                            ddlCorrectAnswer.SelectedValue = "1";
                        else if (questionInfo.CorrectAnswer.Equals(txtAnswer2.Text))
                            ddlCorrectAnswer.SelectedValue = "2";
                        else if (questionInfo.CorrectAnswer.Equals(txtAnswer3.Text))
                            ddlCorrectAnswer.SelectedValue = "3";
                        else if (questionInfo.CorrectAnswer.Equals(txtAnswer4.Text))
                            ddlCorrectAnswer.SelectedValue = "4";
                        else if (questionInfo.CorrectAnswer.Equals(txtAnswer5.Text))
                            ddlCorrectAnswer.SelectedValue = "5";
                    }
                    else
                    {
                        //Invalid question id, redirect as an add
                        Response.Redirect(EditUrl("questionId", "-1", "EditQuestion"));
                    }
                }
                else if (_quizId == -1)
                {
                    //Invalid, direct back to the module
                    Response.Redirect(Globals.NavigateURL(this.TabId));
                }
            }
        }

        /// <summary>
        /// Returns the user back to the quiz
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(EditUrl("quizId", _quizId.ToString()));
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Clear all validation errors
            lblAnswer3Required.Visible = false;
            lblAnswer4Required.Visible = false;
            lblInvalidAnswer.Visible = false;

            //start nested validation
            if (Page.IsValid)
            {
                if (IsCorrectAnswerValid())
                {
                    if (IsAnswerListValid())
                    {
                        //We can now save the question
                        var toSave = new QuizQuestionInfo
                                        {
                                            QuestionId = _questionId,
                                            QuizId = _quizId,
                                            ModuleId = this.ModuleId,
                                            PromptText = txtPromptTextRich.Text,
                                            Answer1 = txtAnswer1.Text,
                                            Answer2 = txtAnswer2.Text,
                                            Answer3 = txtAnswer3.Text,
                                            Answer4 = txtAnswer4.Text,
                                            Answer5 = txtAnswer5.Text
                                        };

                        //Save the correct answer
                        switch (ddlCorrectAnswer.SelectedValue)
                        {
                            case "1":
                                toSave.CorrectAnswer = txtAnswer1.Text;
                                break;
                            case "2":
                                toSave.CorrectAnswer = txtAnswer2.Text;
                                break;
                            case "3":
                                toSave.CorrectAnswer = txtAnswer3.Text;
                                break;
                            case "4":
                                toSave.CorrectAnswer = txtAnswer4.Text;
                                break;
                            case "5":
                                toSave.CorrectAnswer = txtAnswer5.Text;
                                break;
                        }
                      
                        //Persist value
                        QuizController.SaveQuestion(toSave);

                        //Call cancel to redirect
                        btnCancel_Click(sender, e);
                    }
                }
            }
        }

        /// <summary>
        /// Validates that the selected option actually has a value.
        /// </summary>
        /// <returns></returns>
        private bool IsCorrectAnswerValid()
        {
            var returnValue = false;

            switch (ddlCorrectAnswer.SelectedValue)
            {
                case "1":
                case "2":
                    //Validators guarantee this one
                    returnValue = true;
                    break;
                case "3":
                    if(txtAnswer3.Text.Length == 0)
                        lblInvalidAnswer.Visible = true;
                    else
                        returnValue = true;
                    break;
                case "4":
                    if(txtAnswer4.Text.Length == 0)
                        lblInvalidAnswer.Visible = true;
                    else
                        returnValue = true;
                    break;
                case "5":
                    if(txtAnswer5.Text.Length == 0)
                        lblInvalidAnswer.Visible = true;
                    else
                        returnValue = true;
                    break;
            }

            return returnValue;
        }

        /// <summary>
        /// Validates that proper answer ordering is completed
        /// </summary>
        /// <returns></returns>
        private bool IsAnswerListValid()
        {
            bool returnValue = true;

            //If option 5 filled, 3 and 4 must be
            if (txtAnswer5.Text.Length > 0)
            {
                if (txtAnswer4.Text.Length == 0)
                {
                    returnValue = false;
                    lblAnswer4Required.Visible = true;
                }

                if (txtAnswer3.Text.Length == 0)
                {
                    returnValue = false;
                    lblAnswer3Required.Visible = true;
                }
            }

            //if option 4 is filled, 3 must be as well
            if (txtAnswer4.Text.Length > 0)
            {
                if (txtAnswer3.Text.Length == 0)
                {
                    returnValue = false;
                    lblAnswer3Required.Visible = true;
                }
            }

            //Return results
            return returnValue;
        }
    }
}