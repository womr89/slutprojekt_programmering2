using System;

namespace slutprojekt_programmering2 {
#if WINDOWS || XBOX
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main( string[] args ) {
            using ( Game game = new Game() ) {
                game.Run();
            }
        }
    }
#endif
}

