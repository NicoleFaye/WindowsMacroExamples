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
//Needed for changing window size and location
using WindowsWindowManager;

namespace MacroConsoleAppExample
{

    class Program
    {

        //Default path to paint in windows 10 and hopefully others
        static String PaintPath = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Accessories\Paint";


        static void Main(string[] args)
        {
            //Initialize input object
            InputSimulator input = new InputSimulator();

            //start paint
            Process paintProcess = Process.Start(PaintPath);

            //wait for the process to have a window handle
            WWM.waitForWindow(paintProcess);

            //Maximize window
            WWM.showWindowMaximized(paintProcess);

            //get the paint process' window rectangle
            WWM.Rect paintWindowRect = WWM.getWindowRect(paintProcess);

            //Calculate relevant values related to the rectangle
            int height = paintWindowRect.Bottom - paintWindowRect.Top;
            int width = paintWindowRect.Right - paintWindowRect.Left;
            int midWidth = (width / 2) + paintWindowRect.Left;
            int midHeight = (height / 2) + paintWindowRect.Top;
            //97% width
            int almostWidth = (int)(width - (((double)width) * .03));
            //81% height
            int almostHeight= (int)(height- (((double)height) * .19));

            //resize canvas to match closer to screen size
            input.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
            input.Keyboard.KeyPress(VirtualKeyCode.VK_E);
            input.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
            input.Keyboard.TextEntry(almostWidth.ToString());
            input.Keyboard.KeyPress(VirtualKeyCode.TAB);
            input.Keyboard.TextEntry(almostHeight.ToString());
            input.Keyboard.KeyPress(VirtualKeyCode.RETURN);




            //Print values
            Console.WriteLine("Paint Window Details:");
            WWM.printRect(paintWindowRect);
            Console.WriteLine("Height: " + height);
            Console.WriteLine("Width: " + width);
            Console.WriteLine("Midpoint: (" + midWidth + "," + midHeight + ")");

            //Move the mouse to the middle of the paint window
            moveMouseToCoords(midWidth, midHeight, input);


            //Draw a shape of some kind of shape 
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






            Console.WriteLine("Press enter to close this window and the paint window.");
            Console.ReadKey();
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

    }
}
