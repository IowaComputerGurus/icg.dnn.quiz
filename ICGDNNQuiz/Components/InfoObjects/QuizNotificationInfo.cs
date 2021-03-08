namespace ICG.Modules.DnnQuiz.Components.InfoObjects
{
    /// <summary>
    ///     Used for notifications
    /// </summary>
    public class QuizNotificationInfo : UserQuizDisplay
    {
        /// <summary>
        ///     Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public int UserId { get; set; }

        /// <summary>
        ///     Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        ///     Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }
    }
}