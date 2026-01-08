using System;

namespace LibraryConsoleUI
{
    public static class VoiceManagement
    {
        private static void Beep(int freq, int dur)
        {
            if (!OperatingSystem.IsWindows()) return;

            try
            {
                Console.Beep(freq, dur);
            }
            catch
            {
                
            }
        }

        public static void Menu() => Beep(800, 40);
        public static void Select() => Beep(1000, 30);
        public static void Success() => Beep(1200, 60);
        public static void Warning() => Beep(600, 70);
        public static void Error() => Beep(300, 120);
        public static void Exit() => Beep(700, 50);
    }
}
