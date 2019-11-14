﻿using System;
using System.Globalization;

namespace it
{
    public class TimespanActions : IAction
    {
        private readonly string[] DateFormats =
        {
            "dd.MM.yyyy",
            "dd-MM-yyyy"
        };

        private readonly Form1 form1;

        private DateTime? prevDate;

        public TimespanActions(Form1 form1)
        {
            this.form1 = form1;
        }

        public bool TryExecute(string clipboardText)
        {
            if (!DateTime.TryParseExact(clipboardText, DateFormats, CultureInfo.CurrentCulture,
                DateTimeStyles.AssumeLocal, out var newDate))
            {
                prevDate = null;
                return false;
            }

            if (prevDate != null)
            {
                var difference = newDate - prevDate;
                if (difference != null)
                    ShowNotification("Days between:", difference.Value.Days.ToString(CultureInfo.InvariantCulture));
                prevDate = null;
            }
            else
            {
                prevDate = newDate;
            }

            return true;
        }

        private void ShowNotification(string question, string answer)
        {
            form1.ShowNotification(question, answer);
        }
    }
}