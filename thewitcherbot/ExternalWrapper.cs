using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace thewitcherbot
{
   class ExternalWrapper
   {
      [DllImport("user32.dll")]
      static extern bool GetCursorInfo(out CURSORINFO pci);

      [DllImport("user32.dll")]
      static extern bool DrawIcon(IntPtr hDC, int X, int Y, IntPtr hIcon);

      [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
      static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

      // Mouse actions
      private const int MOUSEEVENTF_LEFTDOWN = 0x02;
      private const int MOUSEEVENTF_LEFTUP = 0x04;
      private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
      private const int MOUSEEVENTF_RIGHTUP = 0x10;

      [StructLayout(LayoutKind.Sequential)]
      struct CURSORINFO
      {
         public Int32 cbSize;
         public Int32 flags;
         public IntPtr hCursor;
         public POINTAPI ptScreenPos;
      }

      [StructLayout(LayoutKind.Sequential)]
      struct POINTAPI
      {
         public int x;
         public int y;
      }

      const Int32 CURSOR_SHOWING = 0x00000001;

      static public void LeftClickMouse(uint dx, uint dy)
      {
         mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, dx, dy, 0, 0);
      }

      static public Bitmap GetCursorIcon()
      {
         Bitmap bmp = new Bitmap(32, 32);

         CURSORINFO pci;
         pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));

         if (GetCursorInfo(out pci))
         {
            if (pci.flags == CURSOR_SHOWING)
            {
               using (Graphics g = Graphics.FromImage(bmp))
               {
                  DrawIcon(g.GetHdc(), 0, 0, pci.hCursor);
                  g.ReleaseHdc();
               }
            }
         }

         return bmp;
      }
   }
}
