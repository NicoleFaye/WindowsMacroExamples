using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//used to open programs
using System.Diagnostics;
//used to simulate mouse and keyboard
using WindowsInput;
//contains keycodes
using WindowsInput.Native;
//used to get the Rect struct used in window management
//also requires the WindowsBase Assembly to be referenced in the project even if the namespace exists
using System.Windows;
//used to load in external methods
using System.Runtime.InteropServices;
//Needed for Screen method of getting screen size
using System.Windows.Forms;

namespace MacroConsoleAppExample
{

    /// <summary>
    /// Rectangle Struct of a window defined here: https://www.pinvoke.net/default.aspx/user32.getwindowrect
    /// </summary>
    public struct Rect
    {
        /// <summary>
        /// X position of upper-left corner
        /// </summary>
        public int Left { get; set; }
        /// <summary>
        /// Y position of upper-left corner
        /// </summary>
        public int Top { get; set; }
        /// <summary>
        /// X position of lower-right corner
        /// </summary>
        public int Right { get; set; }
        /// <summary>
        /// Y position of lower-right corner
        /// </summary>
        public int Bottom { get; set; }
    }
    class Program
    {

        //Default path to paint in windows 10 and hopefully others
        static String PaintPath = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Accessories\Paint";


        /// <summary>
        /// Given a handlle and a struct reference, this will save the window's rect values into the passed rect
        /// https://www.pinvoke.net/default.aspx/user32.getwindowrect
        /// </summary>
        /// <param name="hwnd">Window handle</param>
        /// <param name="rectangle">Rectangle struct reference</param>
        /// <returns>Success status</returns>
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        static void Main(string[] args)
        {
            //Initialize input object
            InputSimulator input = new InputSimulator();

            //start paint
            Process paintProcess = Process.Start(PaintPath);

            //wait for the process to have a window handle
            waitForWindow(paintProcess);

            //get the paint process' window rectangle
            Rect paintWindowRect = new Rect();
            GetWindowRect(paintProcess.MainWindowHandle, ref paintWindowRect);

            //Calculate relevant values related to the rectangle
            int height = paintWindowRect.Bottom - paintWindowRect.Top;
            int width = paintWindowRect.Right - paintWindowRect.Left;
            int midWidth = (width / 2) + paintWindowRect.Left;
            int midHeight = (height / 2) + paintWindowRect.Top;

            //Print values
            Console.WriteLine("Paint Window Details:");
            Console.WriteLine("Top:" + paintWindowRect.Top.ToString());
            Console.WriteLine("Bottom:" + paintWindowRect.Bottom.ToString());
            Console.WriteLine("Left:" + paintWindowRect.Left.ToString());
            Console.WriteLine("Right:" + paintWindowRect.Right.ToString());
            Console.WriteLine("Height: " + height);
            Console.WriteLine("Width: " + width);
            Console.WriteLine("Midpoint: (" + midWidth + "," + midHeight + ")");

            //Move the mouse to the middle of the paint window
            moveMouseToCoords(midWidth, midHeight, input);


            //Draw a shape of some kind
            input.Mouse.LeftButtonDown();
            input.Mouse.MoveMouseBy(-1 * (width / 4), 0);
            input.Mouse.Sleep(100);
            input.Mouse.MoveMouseBy(1 * (width / 8), 100);
            input.Mouse.Sleep(100);
            input.Mouse.MoveMouseBy(1 * (width / 4), -200);
            input.Mouse.Sleep(100);
            input.Mouse.MoveMouseBy(0, 200);
            input.Mouse.Sleep(100);
            input.Mouse.MoveMouseBy(-1 * (width / 2), 0);
            input.Mouse.Sleep(100);
            input.Mouse.MoveMouseBy(1 * (width / 4), -200);
            input.Mouse.Sleep(100);
            input.Mouse.MoveMouseBy(1 * (width / 4), 0);
            input.Mouse.LeftButtonUp();







            input.Mouse.Sleep(5000);
            //Console.ReadKey();
            try
            {
                paintProcess.Kill();
            }
            catch { }

        }


        /// <summary>
        /// This is a wrapper function for InputSimulator.Mouse.MoveMouseTo that instead of taking an unsigned int between 0 and 65535, takes an int with exact coordinates.
        /// Currently only works for the primary screen
        /// </summary>
        /// <param name="inputX">Desired X coordinate</param>
        /// <param name="inputY">Desired Y coordinate</param>
        /// <param name="input">InputSimulator object</param>
        public static void moveMouseToCoords(int inputX, int inputY, InputSimulator input)
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            ushort newX = 0;
            ushort newY = 0;
            if (inputX > screenWidth || inputY > screenWidth || inputX < 0 || inputY < 0)
            {
                throw new Exception("Invalid input, method only works on the primary display");
            }
            newX = (ushort)(((double)inputX) * (((double)ushort.MaxValue) / ((double)(screenWidth - 1))));
            newY = (ushort)(((double)inputY) * (((double)ushort.MaxValue) / ((double)(screenHeight - 1))));

            input.Mouse.MoveMouseTo(newX, newY);

        }
        public static void waitForWindow(Process proc)
        {
            Rect WindowRect = new Rect();
            do
            {
                System.Threading.Thread.Sleep(10);
                GetWindowRect(proc.MainWindowHandle, ref WindowRect);
            } while (WindowRect.Left == WindowRect.Right);
        }

    }
}
