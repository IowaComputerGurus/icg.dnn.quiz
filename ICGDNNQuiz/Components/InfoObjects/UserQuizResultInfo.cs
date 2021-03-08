using System;

namespace ICG.Modules.DnnQuiz.Components.InfoObjects
{
    /// <summary>
    ///     Represents a users quiz result
    /// </summary>
    public class UserQuizResultInfo
    {
        /// <summary>
        ///     Gets or sets the result id.
        /// </summary>
        /// <value>The result id.</value>
        public int ResultId { get; set; }

        /// <summary>
        ///     Gets or sets the quiz id.
        /// </summary>
        /// <value>The quiz id.</value>
        public int QuizId { get; set; }

        /// <summary>
        ///     Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public int UserId { get; set; }

        /// <summary>
        ///     Gets or sets the date taken.
        /// </summary>
        /// <value>The date taken.</value>
        public DateTime DateTaken { get; set; }

        /// <summary>
        ///     Gets or sets the user ip address.
        /// </summary>
        /// <value>The user ip address.</value>
        public string UserIpAddress { get; set; }

        /// <summary>
        ///     Gets or sets the number correct.
        /// </summary>
        /// <value>The number correct.</value>
        public int NumberCorrect { get; set; }

        /// <summary>
        ///     Gets or sets the number incorrect.
        /// </summary>
        /// <value>The number incorrect.</value>
        public int NumberIncorrect { get; set; }

        /// <summary>
        ///     Gets or sets the percentage.
        /// </summary>
        /// <value>The percentage.</value>
        public decimal Percentage { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="UserQuizResultInfo" /> is passed.
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
        ///     Gets or sets a value indicating whether [reminder sent].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [reminder sent]; otherwise, <c>false</c>.
        /// </value>
        public bool ReminderSent { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is most current.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is most current; otherwise, <c>false</c>.
        /// </value>
        public bool IsMostCurrent { get; set; }
    }
}