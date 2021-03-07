using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using ICG.Modules.DnnQuiz.Components.Controllers;
using ICG.Modules.DnnQuiz.Components.InfoObjects;

namespace ICG.Modules.DnnQuiz
{
    /// <summary>
    ///     Main view control for working with the module
    /// </summary>
    public partial class ViewICGDNNQuiz : PortalModuleBase, IActionable
    {
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
                //Short circuit if a postback!
                if (IsPostBack) return;

                if (UserInfo.UserID > 0)
                {
                    //Try to get the quizzes
                    List<UserQuizDisplay> quizList;

                    //If we can edit, or the user is a superuser get all quizzes
                    if (IsEditable || UserInfo.IsSuperUser)
                        quizList = QuizController.GetAllQuizzesForDisplay(UserId, ModuleId);
                    else
                    {
                        //Otherwise get my quizzes!
                        quizList = QuizController.GetQuizzesForUserDisplay(UserId, ModuleId);
                    }

                    //If we have them bind and display
                    if (quizList != null && quizList.Count > 0)
                    {
                        rptQuizList.DataSource = quizList;
                        rptQuizList.DataBind();
                        lblNoQuizzes.Visible = false;
                        lblMustLogin.Visible = false;
                    }
                    else
                    {
                        rptQuizList.Visible = false;
                        lblNoQuizzes.Visible = true;
                        lblMustLogin.Visible = false;
                    }
                }
                else
                {
                    //Unauthenticated user
                    rptQuizList.Visible = false;
                    lblNoQuizzes.Visible = false;
                    lblMustLogin.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        #region IActionable Members

        /// <summary>
        /// Gets the module actions which allow users to add quizzes
        /// </summary>
        /// <value>The module actions.</value>
        public ModuleActionCollection ModuleActions
        {
            get
            {
                //create a new action to add an item, this will be added to the controls
                //dropdown menu
                var actions = new ModuleActionCollection();
                actions.Add(GetNextActionID(), Localization.GetString("AddQuiz", LocalResourceFile),
                            ModuleActionType.AddContent, "", "", EditUrl("QuizId", "-1"), false,
                            SecurityAccessLevel.Edit,
                            true, false);

                return actions;
            }
        }

        #endregion

        /// <summary>
        /// Handles the ItemDataBound event of the rptQuizList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void rptQuizList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Only work with items
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) 
                return;

            //Get the data object and the literal
            var oInfo = (UserQuizDisplay) e.Item.DataItem;
            var litItem = (Literal) e.Item.FindControl("litItem");

            //Get the template
            var oBuilder = new StringBuilder(Localization.GetString("ItemTemplate", LocalResourceFile));

            //Can edit, show edit and view results
            if (IsEditable)
            {
                oBuilder.Replace("[EDIT]", "<a href='" + EditUrl("QuizId", oInfo.QuizId.ToString()) + "'>Edit</a>");
                oBuilder.Replace("[VIEWRESULTS]",
                                 "&nbsp;<a href='" + EditUrl("Quizid", oInfo.QuizId.ToString(), "QuizResults") +
                                 "'>View Results</a>");
            }
            else
            {
                oBuilder.Replace("[EDIT]", "");
                oBuilder.Replace("[VIEWRESULTS]", "");
            }

            //Replace the title
            oBuilder.Replace("[QUIZTITLE]", oInfo.QuizTitle);


            //Build links
            var takeQuiz = Globals.NavigateURL("TakeQuiz", "mid=" + ModuleId,
                                               "quizId=" + Server.UrlEncode(UrlUtils.EncryptParameter(oInfo.QuizId.ToString())));
            var certificate = Globals.NavigateURL("QuizCert", "mid=" + ModuleId,
                                                  "quizId=" + Server.UrlEncode(UrlUtils.EncryptParameter(oInfo.QuizId.ToString())),
                                                  "exp=" +
                                                  Server.UrlEncode(UrlUtils.EncryptParameter(
                                                      Server.UrlEncode(oInfo.ExpirationDate.ToShortDateString()))),
                                                  "resId=" + oInfo.ResultId);
            const string noSkinQsParams =
                "&SkinSrc=[G]Skins%2f_default%2fNo+Skin&ContainerSrc=[G]Containers%2f_default%2fNo+Container";

            //Set the status, take, and complete items
            if (oInfo.Passed)
            {
                if (oInfo.CanExpire)
                {
                    //Set status, and completion link
                    oBuilder.Replace("[STATUS]",
                                     Localization.GetString("CompleteWithExpiration", LocalResourceFile).Replace(
                                         "[EXPIRATION]", oInfo.ExpirationDate.ToShortDateString()));
                    oBuilder.Replace("[COMPLETIONCERTIFICATE]",
                                     "<a href='" + certificate + "'>" +
                                     Localization.GetString("CompletionCertificate", LocalResourceFile) +
                                     "</a>");
                    if (DateTime.Now > oInfo.ExpirationDate.AddDays(-30))
                        oBuilder.Replace("[TAKEQUIZ]",
                                         "<a href='" + takeQuiz + "'>" +
                                         Localization.GetString("TakeQuiz", LocalResourceFile) + "</a>");
                    else
                        oBuilder.Replace("[TAKEQUIZ]", "");
                    oBuilder.Replace("[COMPLETIONCERTIFICATEPRINT]",
                                     "<a href='" + certificate + noSkinQsParams + "'>" +
                                     Localization.GetString("CompletionCertificate", LocalResourceFile) +
                                     "</a>");
                }
                else
                {
                    oBuilder.Replace("[STATUS]",
                                     Localization.GetString("Complete", LocalResourceFile));
                    oBuilder.Replace("[COMPLETIONCERTIFICATE]",
                                     "<a href='" + certificate + "'>" +
                                     Localization.GetString("CompletionCertificate", LocalResourceFile) +
                                     "</a>");
                    oBuilder.Replace("[TAKEQUIZ]", "");
                    oBuilder.Replace("[COMPLETIONCERTIFICATEPRINT]",
                                     "<a href='" + certificate + noSkinQsParams + "'>" +
                                     Localization.GetString("CompletionCertificate", LocalResourceFile) +
                                     "</a>");
                }
            }
            else
            {
                oBuilder.Replace("[STATUS]", Localization.GetString("NotComplete", LocalResourceFile));
                //If taken, not passed, and not allowed retake hide link
                if (oInfo.ResultId > 0 && !oInfo.Passed && @oInfo.AllowRetake)
                    oBuilder.Replace("[TAKEQUIZ]", string.Empty);
                else
                    oBuilder.Replace("[TAKEQUIZ]",
                                     "<a href='" + takeQuiz + "'>" +
                                     Localization.GetString("TakeQuiz", LocalResourceFile) + "</a>");
                oBuilder.Replace("[COMPLETIONCERTIFICATE]", "");
                oBuilder.Replace("[COMPLETIONCERTIFICATEPRINT]", "");
            }

            //Store it
            litItem.Text = oBuilder.ToString();
        }
    }
}