using System;
using System.Collections.Generic;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Localization;
using ICG.Modules.DnnQuiz.Components.Controllers;
using ICG.Modules.DnnQuiz.Components.InfoObjects;

namespace ICG.Modules.DnnQuiz
{
    /// <summary>
    ///     Report to allow users to view quiz results
    /// </summary>
    public partial class ViewQuizResults : PortalModuleBase
    {
        private int _quizId = -1;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Try to get the quid if needed
            if (Request.QueryString["quizId"] != null)
            {
                try
                {
                    _quizId = Int32.Parse(Request.QueryString["quizId"]);
                }
                catch (Exception)
                {
                }
            }

            //Exit if a postback
            if (IsPostBack) 
                return;

            //Get the quiz info
            var oInfo = QuizController.GetQuizById(_quizId, ModuleId);

            if (oInfo != null)
            {
                litHeader.Text = Localization.GetString("HeaderText", LocalResourceFile)
                                             .Replace("[QUIZNAME]", oInfo.QuizTitle);

                //LLocalize
                Localization.LocalizeDataGrid(ref dgrQuizResults, LocalResourceFile);

                //Call bind
                BindGrid();
            }
            else
            {
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId));
            }
        }

        /// <summary>
        ///     Binds the grid.
        /// </summary>
        private void BindGrid()
        {
            var oReport = ReportingController.QuizResultsReport(_quizId) ??
                          new List<QuizResultsReportInfo>();

            dgrQuizResults.DataSource = oReport;
            dgrQuizResults.DataBind();
        }
    }
}