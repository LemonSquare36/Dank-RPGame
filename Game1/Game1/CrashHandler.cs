﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;
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
            var CrashWriter = new StreamWriter(Path.Combine(getErrorPath(), "CrashCheck"));
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
                var CrashChecker = new StreamReader(Path.Combine(getErrorPath(), "CrashCheck"));
                line = CrashChecker.ReadLine();

                if (line == "Crash")
                {
                    CrashChecker.Close();
                    CrashForm();
                }
            }
        }
        //Removes the Crash File on properly closing the application
        public void CrashFileRemove()
        {
            File.Delete(Path.Combine(getErrorPath(), "CrashCheck"));
        }

        private static void CrashForm()
        {
            Application.EnableVisualStyles();
            Application.Run(new CrashForm());
        }
        public void MainClose()
        {
            Environment.Exit(0);
        }
        public void GameRunCrash(string log)
        {

        }

    }
}
