/*
 * Copyright (c) 2007-2011 IowaComputerGurus Inc (http://www.iowacomputergurus.com)
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

namespace ICG.Modules.DnnQuiz.Components.InfoObjects
{
    /// <summary>
    /// Stores the users answer to a specific question
    /// </summary>
    public class UserQuestionAnswerInfo
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the result id.
        /// </summary>
        /// <value>The result id.</value>
        public int ResultId { get; set; }
        /// <summary>
        /// Gets or sets the answer id.
        /// </summary>
        /// <value>The answer id.</value>
        public int AnswerId { get; set; }
        /// <summary>
        /// Gets or sets the question id.
        /// </summary>
        /// <value>The question id.</value>
        public int QuestionId { get; set; }
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the answer.
        /// </summary>
        /// <value>The answer.</value>
        public string Answer { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [was correct].
        /// </summary>
        /// <value><c>true</c> if [was correct]; otherwise, <c>false</c>.</value>
        public bool WasCorrect { get; set; }
        #endregion

    }
}