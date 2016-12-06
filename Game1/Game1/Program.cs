using System;

namespace RPGame
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        static Global global = new Global();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //try
            //{
                using (var game = new Main())
                    game.Run();
           // }
           // catch (Exception ex) { global.ErrorHandling(ex.Message, global.GetType().Name, ex); Environment.Exit(0); }
        }
    }
#endif
}
