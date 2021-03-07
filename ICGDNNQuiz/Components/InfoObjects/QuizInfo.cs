namespace ICG.Modules.DnnQuiz.Components.InfoObjects
{
    /// <summary>
    ///     This class holds information about the quiz for a given week
    /// </summary>
    public class QuizInfo
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the quiz id.
        /// </summary>
        /// <value>The quiz id.</value>
        public int QuizId { get; set; }

        /// <summary>
        ///     Gets or sets the module id.
        /// </summary>
        /// <value>The module id.</value>
        public int ModuleId { get; set; }

        /// <summary>
        ///     Gets or sets the quiz title.
        /// </summary>
        /// <value>The quiz title.</value>
        public string QuizTitle { get; set; }

        /// <summary>
        ///     Gets or sets the name of the role.
        /// </summary>
        /// <value>The name of the role.</value>
        public string RoleName { get; set; }

        public int EmailResultsMode { get; set; }

        /// <summary>
        ///     Gets or sets the role id.
        /// </summary>
        /// <value>The role id.</value>
        public int RoleId { get; set; }

        /// <summary>
        ///     Gets or sets the pass percentage.
        /// </summary>
        /// <value>The pass percentage.</value>
        public decimal PassPercentage { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is published.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is published; otherwise, <c>false</c>.
        /// </value>
        public bool IsPublished { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance can expire.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance can expire; otherwise, <c>false</c>.
        /// </value>
        public bool CanExpire { get; set; }

        /// <summary>
        ///     Gets or sets the duration of the expire.
        /// </summary>
        /// <value>The duration of the expire.</value>
        public int ExpireDuration { get; set; }

        /// <summary>
        ///     Gets or sets the role to add.
        /// </summary>
        /// <value>The role to add.</value>
        public string RoleToAdd { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [allow retake].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [allow retake]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowRetake { get; set; }

        #endregion
    }
}