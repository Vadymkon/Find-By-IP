using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Вычислить_по_IP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            KeyDown += (a, e) => { if (e.KeyValue == (char)Keys.Enter) button1_Click(button1, null); };
            webBrowser1.ScriptErrorsSuppressed = true;
        }
        
        void Cmd(string line)
        {
            Process.Start(new ProcessStartInfo { FileName = "cmd", Arguments = $"/c {line}", WindowStyle = ProcessWindowStyle.Hidden });
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (string numb in textBox1.Text.Split('.')) { if (Convert.ToInt32(numb) < 0 || Convert.ToInt32(numb) > 255) { MessageBox.Show("тупой чи шо? айпишник только от 0 до 255"); return; } }

            StringBuilder querryadress = new StringBuilder();
            querryadress.Append("https://www.google.com.ua/maps?q=");

            string x = "", y = "", line = "";

            using (WebClient wc = new WebClient())
                line = wc.DownloadString($@"http://ipwho.is/{textBox1.Text}");
            Match match = Regex.Match(line, "latitude\":(.*?),(.*?)longitude\":(.*?),");
            x = match.Groups[1].Value; y = match.Groups[3].Value;

                querryadress.Append($"{x},+{y}");
            webBrowser1.Navigate(querryadress.ToString());
        }

         void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox1.Text, "[^0-9-.]"))
            {
                MessageBox.Show("Только цифры!");
                textBox1.Text = textBox1.Text.Substring(0, textBox1.TextLength - 1);
                textBox1.SelectionStart = textBox1.TextLength;
            }
        }
    }
}
