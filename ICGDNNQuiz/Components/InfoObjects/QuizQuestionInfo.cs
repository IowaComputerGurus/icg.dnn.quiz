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

namespace ICG.Modules.DnnQuiz.Components.InfoObjects
{
    ///<summary>
    /// This holds information on a question
    ///</summary>
    public class QuizQuestionInfo
    {
        /// <summary>
        /// Gets or sets the question id.
        /// </summary>
        /// <value>The question id.</value>
        public int QuestionId { get; set; }
        /// <summary>
        /// Gets or sets the quiz id.
        /// </summary>
        /// <value>The quiz id.</value>
        public int QuizId { get; set; }
        /// <summary>
        /// Gets or sets the module id.
        /// </summary>
        /// <value>The module id.</value>
        public int ModuleId { get; set; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        public int Position { get; set; }
        /// <summary>
        /// Gets or sets the prompt text.
        /// </summary>
        /// <value>The prompt text.</value>
        public string PromptText { get; set; }
        /// <summary>
        /// Gets or sets the answer1.
        /// </summary>
        /// <value>The answer1.</value>
        public string Answer1 { get; set; }
        /// <summary>
        /// Gets or sets the answer2.
        /// </summary>
        /// <value>The answer2.</value>
        public string Answer2 { get; set; }
        /// <summary>
        /// Gets or sets the answer3.
        /// </summary>
        /// <value>The answer3.</value>
        public string Answer3 { get; set; }
        /// <summary>
        /// Gets or sets the answer4.
        /// </summary>
        /// <value>The answer4.</value>
        public string Answer4 { get; set; }
        /// <summary>
        /// Gets or sets the answer5.
        /// </summary>
        /// <value>The answer5.</value>
        public string Answer5 { get; set; }
        /// <summary>
        /// Gets or sets the correct answer.
        /// </summary>
        /// <value>The correct answer.</value>
        public string CorrectAnswer { get; set; }

        ///<summary>
        /// Default code constructor
        ///</summary>
        public QuizQuestionInfo()
        {
            //Initalize any needed values
            QuestionId = -1;
        }
    }
}