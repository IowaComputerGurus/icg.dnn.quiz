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
using System.Collections.Generic;
using System.Text;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Mail;
using DotNetNuke.Services.Scheduling;
using ICG.Modules.DnnQuiz.Components.Controllers;
using ICG.Modules.DnnQuiz.Components.InfoObjects;

namespace ICG.Modules.DnnQuiz.Components.Jobs
{
    public class ReminderTask : SchedulerClient
    {
        #region Default Constructor
        /// <summary>
        /// Constructor needed to obtain the Schedule History item
        /// </summary>
        /// <param name="oItem"></param>
        public ReminderTask (ScheduleHistoryItem oItem)
        {
            this.ScheduleHistoryItem = oItem;
        }
        #endregion

        #region Working Methods
        public override void DoWork()
        {
            try
            {
                //Perform required items for logging
                this.Progressing();

                //Call our process method
                SendNotifications();

                //Show success
                this.ScheduleHistoryItem.Succeeded = true;
                InsertLogNote("Task completed 100%");
            }
            catch (Exception ex)
            {
                this.ScheduleHistoryItem.Succeeded = false;
                InsertLogNote("Exception= " + ex);
                this.Errored(ref ex);
                Exceptions.LogException(ex);
            }
        }

        public void SendNotifications()
        {
            //See if we have any
            List<QuizNotificationInfo> oNotifications = QuizController.GetQuizzesForNotification();

            if (oNotifications != null && oNotifications.Count > 0)
            {
                //Notify
                InsertLogNote("A total of " + oNotifications.Count + " notifications were found that need to be sent");

                //Notify
                InsertLogNote("Loading Portal Settings");
                var oPController = new PortalController();
                PortalInfo oPInfo = oPController.GetPortal(0);
                string fromAddress = oPInfo.Email;
                InsertLogNote("Portal Settings Loaded");

                //Load template
                string messageTemplate;

                SettingsInfo oInfo = QuizController.GetSettings();

                if (oInfo != null)
                    messageTemplate = oInfo.EmailTemplate;
                else
                    messageTemplate = GetDefaultMessageTemplate();

                InsertLogNote("Processing started!");
                //Loop through them
                foreach (QuizNotificationInfo oCurrent in oNotifications)
                {
                    //Prepare message
                    string message = messageTemplate;
                    message = message.Replace("[FIRSTNAME]", oCurrent.FirstName).Replace("[LASTNAME]", oCurrent.LastName).Replace("[QUIZTITLE]", oCurrent.QuizTitle);
                    message = message.Replace("[EXPIRATIONDATE]", oCurrent.ExpirationDate.ToShortDateString());

                    //Send mail
                    Mail.SendMail(fromAddress, oCurrent.Email, "", "Upcoming Recertification Needed", message, "", "html", string.Empty, string.Empty, string.Empty, string.Empty);

                    //Record as being sent
                    QuizController.RecordNotificationSent(oCurrent.ResultId);
                }

                //Notify that all were processed!
                InsertLogNote("Processed All Entries!");
            }
            else
            {
                //Notify
                InsertLogNote("No user notifications found for processing.");
            }
        }
        #endregion

        #region Log Helpers
        /// <summary>
        /// Helper method to keep the code uncluttered
        /// </summary>
        /// <param name="message"></param>
        private void InsertLogNote(string message)
        {
            this.ScheduleHistoryItem.AddLogNote(message + "<br />");
        }
        #endregion

        #region Message Helpers
        private string GetDefaultMessageTemplate()
        {
            var oMessage = new StringBuilder();
            oMessage.Append("Hello [FIRSTNAME],");
            oMessage.Append("<p>Your BASMAA certificate will expire on [EXPIRATIONDATE].</p>");
            oMessage.Append("<p>Please renew it here: <a href='http://www.basmaa.org'>http://www.basmaa.org</a>.</p>");
            oMessage.Append("<p>Best regards,</p>");
            oMessage.Append("<p>The BASMAA team<br /><a href='http://www.basmaa.org'>http://www.basmaa.org</a></p>");
            return oMessage.ToString();
        }
        #endregion
    }
}
