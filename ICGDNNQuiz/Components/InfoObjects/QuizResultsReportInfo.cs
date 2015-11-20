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

using System;

namespace ICG.Modules.DnnQuiz.Components.InfoObjects
{
    /// <summary>
    /// Displays information on user quiz results
    /// </summary>
    public class QuizResultsReportInfo
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the date taken.
        /// </summary>
        /// <value>The date taken.</value>
        public DateTime DateTaken { get; set; }

        /// <summary>
        /// Gets or sets the user ip address.
        /// </summary>
        /// <value>The user ip address.</value>
        public string UserIpAddress { get; set; }

        /// <summary>
        /// Gets or sets the number correct.
        /// </summary>
        /// <value>The number correct.</value>
        public int NumberCorrect { get; set; }
        /// <summary>
        /// Gets or sets the number incorrect.
        /// </summary>
        /// <value>The number incorrect.</value>
        public int NumberIncorrect { get; set; }
        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>The percentage.</value>
        public decimal Percentage { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="QuizResultsReportInfo"/> is passed.
        /// </summary>
        /// <value><c>true</c> if passed; otherwise, <c>false</c>.</value>
        public bool Passed { get; set; }

        /// <summary>
        /// Gets the percentage display.
        /// </summary>
        /// <value>The percentage display.</value>
        public string PercentageDisplay
        {
            get
            {
                return Math.Round(Percentage, 2) + "%";
            }
        }
    }
}