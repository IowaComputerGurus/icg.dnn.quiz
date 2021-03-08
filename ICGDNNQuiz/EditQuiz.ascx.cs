using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Security.Roles;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Skins;
using DotNetNuke.UI.Skins.Controls;
using ICG.Modules.DnnQuiz.Components.Controllers;
using ICG.Modules.DnnQuiz.Components.InfoObjects;

namespace ICG.Modules.DnnQuiz
{
    /// <summary>
    ///     Edit interface for an individual quiz
    /// </summary>
    public partial class EditQuiz : PortalModuleBase
    {
        private int _quizId = -1;

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
                //Update the quiz id if needed
                int.TryParse(Request.QueryString["quizId"], out _quizId);

                if (!IsPostBack)
                {
                    //Bind lookup values
                    BindRoleList();

                    if (_quizId > 0)
                    {
                        //Try to load existing quiz
                        var quizInfo = QuizController.GetQuizById(_quizId, ModuleId);

                        if (quizInfo != null)
                        {
                            //Load successfully
                            txtQuizTitle.Text = quizInfo.QuizTitle;
                            ddlPassPercentage.SelectedValue = Math.Round(quizInfo.PassPercentage, 0).ToString();
                            ddlViewRole.SelectedValue = quizInfo.RoleName;
                            chkIsPublished.Checked = quizInfo.IsPublished;
                            chkExpires.Checked = quizInfo.CanExpire;
                            txtExpireDuration.Text = quizInfo.ExpireDuration.ToString();
                            chkExpires_CheckedChanged(sender, e);
                            chkAllowRetake.Checked = quizInfo.AllowRetake;
                            ListItem oItem = ddlAddRole.Items.FindByValue(quizInfo.RoleToAdd);
                            if (oItem != null)
                                ddlAddRole.SelectedValue = oItem.Value;
                            var mode = ddlEmailResultsMode.Items.FindByValue(quizInfo.EmailResultsMode.ToString());
                            if (mode != null)
                                ddlEmailResultsMode.SelectedValue = mode.Value;
                            if (!string.IsNullOrEmpty(quizInfo.CertificateTemplatePath))
                            {
                                divExistingCert.Visible = true;
                                hlExistingCertificate.NavigateUrl = quizInfo.CertificateTemplatePath;
                            }

                            //Now, load the questions
                            LoadQuizQuestions();

                            //SHow questions
                            pnlQuestions.Visible = true;

                            //Notify if redirected
                            if (Request.QueryString["saved"] != null)
                                Skin.AddModuleMessage(this, Localization.GetString("SaveSuccessful", LocalResourceFile),
                                                      ModuleMessage.ModuleMessageType.GreenSuccess);

                            //See if we need to restrict view
                            var setting = Settings["ICG_DQuiz_DisableEdit"];
                            if (setting != null && setting.ToString().Equals("Y"))
                            {
                                var data = ReportingController.QuizResultsReport(_quizId);
                                if (data.Count >0)
                                {
                                    litCannotEdit.Visible = true;
                                    litCannotEdit.Text = Localization.GetString("CanNotEdit", LocalResourceFile);
                                    btnAddQuestion.Visible = false;
                                    btnDeleteQuestion.Visible = false;
                                    btnEditQuestion.Visible = false;
                                    btnMoveDown.Visible = false;
                                    btnMoveUp.Visible = false;
                                    txtQuizTitle.Enabled = false;
                                    ddlPassPercentage.Enabled = false;
                                    ddlViewRole.Enabled = false;
                                    chkExpires.Enabled = false;
                                    chkAllowRetake.Enabled = false;
                                    ddlAddRole.Enabled = false;
                                }
                            }
                        }
                        else
                        {
                            Skin.AddModuleMessage(this, Localization.GetString("InvalidIdPassed", LocalResourceFile),
                                                  ModuleMessage.ModuleMessageType.RedError);

                            //Disable button
                            btnSave.Enabled = false;

                            //Toggle "expire" option
                            chkExpires_CheckedChanged(sender, e);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        ///     Binds the listing of roles
        /// </summary>
        private void BindRoleList()
        {
            var roleController = new RoleController();
            var rolesList = roleController.GetRoles(PortalId);
            ddlViewRole.DataSource = rolesList;
            ddlViewRole.DataBind();
            ddlAddRole.DataSource = rolesList;
            ddlAddRole.DataBind();
        }

        /// <summary>
        ///     Loads the quiz questions.
        /// </summary>
        private void LoadQuizQuestions()
        {
            //Clear the list
            lstQuestions.Items.Clear();
            lstQuestions.DataSource = QuizController.GetQuizQuestions(_quizId, ModuleId);
            lstQuestions.DataTextField = "PromptText";
            lstQuestions.DataValueField = "QuestionId";
            lstQuestions.DataBind();
        }

        /// <summary>
        ///     Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="EventArgs" /> instance containing the event data.
        /// </param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var existingPath = hlExistingCertificate.NavigateUrl;
                if (fleCertificateTemplate.HasFile)
                {
                    var newFile = $"~/DesktopModules/ICG/DNNQuiz/CertTemplate/{Guid.NewGuid()}.pdf";
                    fleCertificateTemplate.SaveAs(Server.MapPath(newFile));
                    existingPath = newFile;
                }
                var roleController = new RoleController();
                var toSave = new QuizInfo
                                {
                                    QuizId = _quizId,
                                    ModuleId = ModuleId,
                                    QuizTitle = txtQuizTitle.Text,
                                    RoleName = ddlViewRole.SelectedValue,
                                    RoleId =
                                        roleController.GetRoleByName(PortalId, ddlViewRole.SelectedValue)
                                                       .RoleID,
                                    EmailResultsMode = int.Parse(ddlEmailResultsMode.SelectedValue),
                                    PassPercentage = decimal.Parse(ddlPassPercentage.SelectedValue),
                                    IsPublished = chkIsPublished.Checked,
                                    CanExpire = chkExpires.Checked,
                                    CertificateTemplatePath = existingPath,
                                    RoleToAdd = ddlAddRole.SelectedValue,
                                    AllowRetake = chkAllowRetake.Checked
                                };
                toSave.ExpireDuration = toSave.CanExpire ? int.Parse(txtExpireDuration.Text) : 0;
                var quizId = QuizController.SaveQuiz(toSave);

                //Redirect
                Response.Redirect(EditUrl("quizId", quizId.ToString(), "Edit", "saved=y"));
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        ///     Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="EventArgs" /> instance containing the event data.
        /// </param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Globals.NavigateURL(TabId), true);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        ///     Handles the Click event of the btnMoveUp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="ImageClickEventArgs" /> instance containing the event data.
        /// </param>
        protected void btnMoveUp_Click(object sender, ImageClickEventArgs e)
        {
            if (lstQuestions.SelectedIndex >= 1)
            {
                QuizController.MoveQuestion(int.Parse(lstQuestions.SelectedValue), ModuleId, -1);

                //Reload questions
                LoadQuizQuestions();
            }
        }

        /// <summary>
        ///     Handles the Click event of the btnMoveDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="ImageClickEventArgs" /> instance containing the event data.
        /// </param>
        protected void btnMoveDown_Click(object sender, ImageClickEventArgs e)
        {
            if (lstQuestions.SelectedIndex >= 0 && lstQuestions.SelectedIndex < lstQuestions.Items.Count - 1)
            {
                QuizController.MoveQuestion(int.Parse(lstQuestions.SelectedValue), ModuleId, 1);

                //Reload questions
                LoadQuizQuestions();
            }
        }

        /// <summary>
        ///     Handles the Click event of the btnEditQuestion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="ImageClickEventArgs" /> instance containing the event data.
        /// </param>
        protected void btnEditQuestion_Click(object sender, ImageClickEventArgs e)
        {
            if (lstQuestions.SelectedIndex >= 0)
            {
                Response.Redirect(ModuleContext.EditUrl("questionId", lstQuestions.SelectedValue, "EditQuestion",
                                                        "quizId=" + _quizId));
            }
        }

        /// <summary>
        ///     Handles the Click event of the btnDeleteQuestion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="ImageClickEventArgs" /> instance containing the event data.
        /// </param>
        protected void btnDeleteQuestion_Click(object sender, ImageClickEventArgs e)
        {
            if (lstQuestions.SelectedIndex >= 0)
            {
                QuizController.DeleteQuestion(int.Parse(lstQuestions.SelectedValue), ModuleId);

                //Reload questions
                LoadQuizQuestions();
            }
        }

        /// <summary>
        ///     Handles the Click event of the btnAddQuestion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="ImageClickEventArgs" /> instance containing the event data.
        /// </param>
        protected void btnAddQuestion_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(ModuleContext.EditUrl("questionId", "-1", "EditQuestion", "quizId=" + _quizId));
        }

        /// <summary>
        ///     Handles the CheckedChanged event of the chkExpires control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="EventArgs" /> instance containing the event data.
        /// </param>
        protected void chkExpires_CheckedChanged(object sender, EventArgs e)
        {
            divExpiration.Visible = chkExpires.Checked;
        }
    }
}