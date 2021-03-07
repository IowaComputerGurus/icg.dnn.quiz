using System;
using System.Text;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Skins;
using DotNetNuke.UI.Skins.Controls;
using ICG.Modules.DnnQuiz.Components.Controllers;

namespace ICG.Modules.DnnQuiz
{
    /// <summary>
    ///     Displays the completion certificate for a user.
    /// </summary>
    /// <remarks>
    ///     This control should be called using the default "No skin" skin.
    /// </remarks>
    public partial class CompletionCertificate : PortalModuleBase
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
            if (!IsPostBack)
            {
                try
                {
                    var quizPara = Server.UrlDecode(Request.QueryString["quizId"]);
                    var quizId = int.Parse(UrlUtils.DecryptParameter(quizPara));
                    var expirationParam = Server.UrlDecode(Request.QueryString["exp"]);
                    var expiration = Server.UrlDecode(UrlUtils.DecryptParameter(expirationParam));

                    //double check
                    if (quizId > 0)
                    {
                        //Get the quiz info
                        var quizInfo = QuizController.GetQuizById(quizId, ModuleId);

                        //Get the users completion information
                        var certBuilder =
                            new StringBuilder(Localization.GetString("CertificateTemplate", LocalResourceFile));
                        certBuilder.Replace("[FIRSTNAME]", UserInfo.FirstName);
                        certBuilder.Replace("[LASTNAME]", UserInfo.LastName);
                        certBuilder.Replace("[EXPIRATIONDATE]", expiration);
                        certBuilder.Replace("[QUIZTITLE]", quizInfo.QuizTitle);
                        certBuilder.Replace("[RESULTID]", Request.QueryString["resId"]);
                        certBuilder.Replace("[QUIZDATE]", DateTime.Parse(expiration).AddYears(-1).ToShortDateString());


                        //Update the display
                        litCertificate.Text = certBuilder.ToString();
                    }
                }
                catch (Exception)
                {
                    Skin.AddModuleMessage(this, Localization.GetString("InvalidId", LocalResourceFile),
                                          ModuleMessage.ModuleMessageType.RedError);
                }
            }
        }
    }
}