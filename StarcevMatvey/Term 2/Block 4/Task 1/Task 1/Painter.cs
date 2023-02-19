using static System.Console;

namespace Task_1
{
    public class Painter
    {
        public void Draw(string str)
        {
            Write(str);
        }

        public void DrawBlue(string str)
        {
            ForegroundColor = ConsoleColor.Blue;
            Write(str);
            ResetColor();
        }

        public void DrawMagneta(string str)
        {
            ForegroundColor = ConsoleColor.Magenta;
            Write(str);
            ResetColor();
        }

        public void DrawYellow(string str)
        {
            ForegroundColor= ConsoleColor.Yellow;
            Write(str);
            ResetColor();
        }
    }
}
