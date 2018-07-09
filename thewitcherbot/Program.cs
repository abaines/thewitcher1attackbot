using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
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
         comboHashes.Add("d520ae2a660de45be42d24014fd5f77a");
         comboHashes.Add("2289fcfc643e7d0b83a03bacdeca9a62");
      }

      static void Main(string[] args)
      {
         Process.Start(cwd);
         Console.WriteLine(cwd);

         // forever
         while (true)
         {
            derp();
            Thread.Sleep(10);
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
         using (Bitmap bmp = ExternalWrapper.GetCursorIcon())
         {
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

            string hash = "";
            hash = getHash(bmp);

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
         }
      }

      static MD5 md5Hash = MD5.Create();

      static string getHash(Bitmap bmp)
      {
         byte[] bytes = getBytes(bmp);

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
         MemoryStream ms = new MemoryStream();
         bmp.Save(ms, ImageFormat.Bmp);
         return ms.ToArray();
      }
   }
}
