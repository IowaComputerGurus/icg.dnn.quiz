using System;

namespace ICG.Modules.DnnQuiz.Components.InfoObjects
{
    /// <summary>
    ///     Used for displaying the quiz to the user
    /// </summary>
    public class UserQuizDisplay : QuizInfo
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the percentage.
        /// </summary>
        /// <value>The percentage.</value>
        public decimal Percentage { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="UserQuizDisplay" /> is passed.
        /// </summary>
        /// <value>
        ///     <c>true</c> if passed; otherwise, <c>false</c>.
        /// </value>
        public bool Passed { get; set; }

        /// <summary>
        ///     Gets or sets the expiration date.
        /// </summary>
        /// <value>The expiration date.</value>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        ///     Gets or sets the result id.
        /// </summary>
        /// <value>The result id.</value>
        public int ResultId { get; set; }

        #endregion
    }
}