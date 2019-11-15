using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace it.Actions
{
    public class StopwatchActions : IAction
    {
        private Stopwatch myStopwatch;
        private string lastClipboard;

        public ActionResult TryExecute(string clipboardText)
        {
            ActionResult actionResult = new ActionResult();

            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            string clipboard = Clipboard.GetText();
            switch (clipboard)
            {
                case "start stopwatch": //start
                    {
                        if (myStopwatch?.IsRunning == true)
                        {
                            actionResult.Title = "Stopwatch";
                            actionResult.Description = "Stopwatch already running";
                        }
                        if (clipboard != lastClipboard)
                        {
                            lastClipboard = clipboard;
                            myStopwatch = new Stopwatch();
                            myStopwatch.Start();
                        }
                        break;
                    }
                case "reset stopwatch": //reset
                    {
                        if (clipboard != lastClipboard)
                        {
                            lastClipboard = clipboard;
                            myStopwatch.Reset();
                            myStopwatch = new Stopwatch();
                            myStopwatch.Start();
                            TimeSpan ts = myStopwatch.Elapsed;
                            switch (currentCulture.LCID)
                            {
                                case 1033: // english-us
                                    actionResult.Title = "Stopwatch reset to";
                                    actionResult.Description = $"{ts.Hours} hours, {ts.Minutes} minutes,  {ts.Seconds} seconds";
                                    break;
                                case 1043: // dutch
                                    actionResult.Title = "Stopwatch gereset naar";
                                    actionResult.Description = $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes";
                                    break;
                            }
                        }
                        break;
                    }
                case "pause stopwatch": //  pause
                    {
                        if (clipboard != lastClipboard)
                        {
                            lastClipboard = clipboard;
                            TimeSpan ts = myStopwatch.Elapsed;
                            myStopwatch.Stop();

                            switch (currentCulture.LCID)
                            {
                                case 1033: // english-us
                                    actionResult.Title = "Stopwatch paused on";
                                    actionResult.Description = $"{ts.Hours} hours, {ts.Minutes} minutes,  {ts.Seconds} seconds";
                                    break;
                                case 1043: // dutch
                                    actionResult.Title = "Stopwatch gepauzeerd op";
                                    actionResult.Description = $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes";
                                    break;
                            }
                        }
                        break;
                    }
                case "resume stopwatch":
                    {
                        if (clipboard != lastClipboard)
                        {
                            lastClipboard = clipboard;
                            TimeSpan ts = myStopwatch.Elapsed;
                            myStopwatch.Start();
                            switch (currentCulture.LCID)
                            {
                                case 1033: // english-us
                                    actionResult.Title = "Stopwatch resumed from";
                                    actionResult.Description = $"{ts.Hours} hours, {ts.Minutes} minutes,  {ts.Seconds} seconds";
                                    break;
                                case 1043: // dutch
                                    actionResult.Title = "Stopwatch gepauzeerd op";
                                    actionResult.Description = $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes";
                                    break;
                            }
                        }
                        break;
                    }
                case "stop stopwatch": //stop
                    {
                        if (lastClipboard is object)
                        {
                            lastClipboard = null;
                            myStopwatch.Stop();
                            TimeSpan ts = myStopwatch.Elapsed;
                            switch (currentCulture.LCID)
                            {
                                case 1033: // english-us
                                    actionResult.Title = "Elapsed time";
                                    actionResult.Description = $"{ts.Hours} hours, {ts.Minutes} minutes,  {ts.Seconds} seconds";
                                    break;
                                case 1043: // dutch
                                    actionResult.Title = "Elapsed time";
                                    actionResult.Description = $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes";
                                    break;
                            }
                        }
                        else
                            actionResult.IsProcessed = false;

                        break;
                    }
                default:
                    actionResult.IsProcessed = false;
                    break;
            }


            return actionResult;
        }
    }
}