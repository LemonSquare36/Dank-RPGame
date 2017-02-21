using System;
using System.IO;
using System.Windows.Forms;

namespace RPGame
{
    class CrashHandler : Global
    {
        //Creates a crash file that if not removed on closing the game will report that it wasnt shut down properly
        public void CrashFileMake()
        {
            createErrorFolder();
            StreamWriter CrashWriter = new StreamWriter(Path.Combine(getErrorPath(), "CrashCheck"));
            CrashWriter.WriteLine("Crash");
            CrashWriter.Close();
        }
        //Will see if crash file exist. If it does then it will prompt the user on startup about if they want to send a crash report. 
        public void CrashCheck()
        {
            createErrorFolder();

            string line;

            if (File.Exists(Path.Combine(getErrorPath(), "CrashCheck")))
            {
                StreamReader CrashChecker = new StreamReader(Path.Combine(getErrorPath(), "CrashCheck"));
                line = CrashChecker.ReadLine();
                CrashChecker.Close();
                if (line == "Crash")
                {
                    CrashForm();
                }
            }
        }
        //Removes the Crash File on properly closing the application
        public void CrashFileRemove()
        {
            File.Delete(Path.Combine(getErrorPath(), "CrashCheck"));
        }
        //Creates the winform
        private static void CrashForm()
        {
            Application.EnableVisualStyles();
            Application.Run(new CrashForm());
        }
        //Closes the application out
        public void MainClose()
        {
            Environment.Exit(0);
        }

    }
}
