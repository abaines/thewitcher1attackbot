using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;

namespace thewitcherbot
{
   class Program
   {
      static HashSet<string> hashes = new HashSet<string>();
      static string cwd = Directory.GetCurrentDirectory();

      static HashSet<string> comboHashes = new HashSet<string>();

      static Program()
      {
         comboHashes.Add("ff9bae344efb00bdc26af07d419e3bd1");
         comboHashes.Add("5d0bea99cb7cebb511dd7e0af955ea4c");

         //comboHashes.Add("ddff2712037266faff4ac5b88d3063f7");
         //comboHashes.Add("e99cb15837be42718f933b363f0784d2");
      }

      static void Main(string[] args)
      {
         Process.Start(cwd);
         Console.WriteLine(cwd);

         // forever
         while (true)
         {
            derp();
            //Console.WriteLine("");
            //Thread.Sleep(50);
            Thread.Sleep(0);
         }
      }




      static void DoMouseClick()
      {
         //Call the imported function with the cursor's current position
         uint X = (uint)Cursor.Position.X;
         uint Y = (uint)Cursor.Position.Y;
         //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
         ExternalWrapper.LeftClickMouse(X, Y);
         string d = DateTime.Now.ToString("HH:mm:ss ff");
         Console.Out.WriteLine(d + "   x=" + X + " y=" + Y);
      }







      static void derp()
      {
         //CURSORINFO pci;
         //pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));

         //if (GetCursorInfo(out pci))
         //{
         //   if (pci.flags == CURSOR_SHOWING)
         //   {
         //      //Console.WriteLine(pci.hCursor);

         //      Bitmap bmp = new Bitmap(32, 32);
         //      using (Graphics g = Graphics.FromImage(bmp))
         //      {
         //         DrawIcon(g.GetHdc(), 0, 0, pci.hCursor);
         //         g.ReleaseHdc();
         //      }

         Bitmap bmp = ExternalWrapper.GetCursorIcon();
         //Bitmap bmp = new Bitmap(32, 32);

         //if (false)
         //   using (Form form = new Form())
         //   {
         //      form.StartPosition = FormStartPosition.CenterScreen;
         //      form.Size = bmp.Size;

         //      PictureBox pb = new PictureBox();
         //      pb.Dock = DockStyle.Fill;
         //      pb.Image = bmp;

         //      form.Controls.Add(pb);
         //      form.ShowDialog();
         //   }

         string hash = getHash(bmp);
         //string hash = "";

         if (!hashes.Contains(hash))
         {
            hashes.Add(hash);
            Console.WriteLine(hash);
            bmp.Save(Path.Combine(cwd, hash + ".bmp"));
         }

         if (comboHashes.Contains(hash))
         {
            DoMouseClick();
            Thread.Sleep(200);
         }

         //g.ReleaseHdc();
         //   }
         //}

         // TODO: Marshal.?Release?( pci.cbSize )
      }

      static string getHash(Bitmap bmp)
      {
         byte[] bytes = getBytes(bmp);

         MD5 md5Hash = MD5.Create();

         byte[] data = md5Hash.ComputeHash(bytes);

         // Create a new Stringbuilder to collect the bytes
         // and create a string.
         StringBuilder sBuilder = new StringBuilder();

         // Loop through each byte of the hashed data 
         // and format each one as a hexadecimal string.
         for (int i = 0; i < data.Length; i++)
         {
            sBuilder.Append(data[i].ToString("x2"));
         }

         // Return the hexadecimal string.
         return sBuilder.ToString();
      }

      static byte[] getBytes(Bitmap bmp)
      {
         // Lock the bitmap's bits.  
         Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
         BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

         // Get the address of the first line.
         IntPtr ptr = bmpData.Scan0;

         // Declare an array to hold the bytes of the bitmap.
         int bytes = bmpData.Stride * bmp.Height;
         byte[] rgbValues = new byte[bytes];
         //byte[] r = new byte[bytes / 3];
         //byte[] g = new byte[bytes / 3];
         //byte[] b = new byte[bytes / 3];

         // Copy the RGB values into the array.
         Marshal.Copy(ptr, rgbValues, 0, bytes);

         //int count = 0;
         //int stride = bmpData.Stride;

         //for (int column = 0; column < bmpData.Height; column++)
         //{
         //   for (int row = 0; row < bmpData.Width; row++)
         //   {
         //      b[count] = (byte)(rgbValues[(column * stride) + (row * 3)]);
         //      g[count] = (byte)(rgbValues[(column * stride) + (row * 3) + 1]);
         //      r[count++] = (byte)(rgbValues[(column * stride) + (row * 3) + 2]);
         //   }
         //}
         return rgbValues;
      }

      //static Bitmap CaptureScreen(bool CaptureMouse)
      //{
      //   Bitmap result = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format24bppRgb);

      //   try
      //   {
      //      using (Graphics g = Graphics.FromImage(result))
      //      {
      //         g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);

      //         if (CaptureMouse)
      //         {
      //            CURSORINFO pci;
      //            pci.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(CURSORINFO));

      //            if (GetCursorInfo(out pci))
      //            {
      //               if (pci.flags == CURSOR_SHOWING)
      //               {
      //                  DrawIcon(g.GetHdc(), pci.ptScreenPos.x, pci.ptScreenPos.y, pci.hCursor);
      //                  g.ReleaseHdc();
      //               }
      //            }
      //         }
      //      }
      //   }
      //   catch
      //   {
      //      result = null;
      //   }

      //   return result;
      //}
   }
}
