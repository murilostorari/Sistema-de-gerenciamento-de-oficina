using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCC
{
    class EAN13Class
    {
        public static string Barcode13Digits = "";

        public static string EAN13(string Text)
        {
            int i;
            int First;
            int CheckSum = 0;

            string Barcode = "";
            
            bool TableA;

            if (Regex.IsMatch(Text, "^\\d{12}$"))
            {
                for (i = 1; i < 12; i += 2)
                {
                    System.Diagnostics.Debug.WriteLine(Text.Substring(i, 1));
                    CheckSum += Convert.ToInt32(Text.Substring(i, 1));
                }

                CheckSum *= 3;

                for (i = 0; i < 12; i+= 2)
                {
                    CheckSum += Convert.ToInt32(Text.Substring(i, 1));
                }

                Text += (10 - CheckSum % 10) % 10;

                Barcode13Digits = Text.ToString();

                Barcode = Text.Substring(0, 1) + (char)(65 + Convert.ToInt32(Text.Substring(1, 1)));
                First = Convert.ToInt32(Text.Substring(0, 1));

                for (i = 2; i <= 6; i++)
                {
                    TableA = false;

                    switch(i)
                    {
                        case 2:
                            if (First >= 0 && First <= 3)
                                TableA = true;
                            break;
                        case 3:
                            if (First == 0 || First == 4 || First == 7 || First == 8)
                                TableA = true;
                            break;
                        case 4:
                            if (First == 0 || First == 1 || First == 4 || First == 5 || First == 9)
                                TableA = true;
                            break;
                        case 5:
                            if (First == 0 || First == 2 || First == 5 || First == 6 || First == 7)
                                TableA = true;
                            break;
                        case 6:
                            if (First == 0 || First == 3 || First == 6 || First == 8 || First == 9)
                                TableA = true;
                            break;
                    }

                    if (TableA)
                    {
                        Barcode += (char)(65 + Convert.ToInt32(Text.Substring(i, 1)));
                    }
                    else
                    {
                        Barcode += (char)(75 + Convert.ToInt32(Text.Substring(i, 1)));
                    }
                }

                Barcode += "*";
                
                for (i = 7; i <= 12; i++)
                {
                    Barcode += (char)(97 + Convert.ToInt32(Text.Substring(i, 1)));
                }

                Barcode += "+";
            }

            return Barcode;
        }
    }
}