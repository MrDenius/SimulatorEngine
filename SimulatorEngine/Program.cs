using System;

namespace SimulatorEngine
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {

        public static GameSE gameSE {get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (gameSE = new GameSE())
                gameSE.Run();
        }
    }
#endif
}
