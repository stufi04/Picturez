using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace Picturez
{
    public partial class Form1 : Form
    {

        Image[] pics = new Image[128];
        string[] characters_names = { "", "БАРТ", "ЙОРИ", "СИМБА", "СТИЧ", "НЕМО", "ПИКАЧУ", "ДЪМБО", "МИКИ", "ДОНАЛД", "УОЛИ", "СОНИК", "ТОМ", "ДЖЕРИ", "СКУБИ", "ФРЕД", "ХАРИ", "СКРАТ", "ШРЕК", "ТУРБО", "ТАРЗАН" };
        string[] animals_names = { "", "ЛИСИЦА", "ПИНГВИН", "ЗАЕК", "ЖИРАФ", "КОАЛА", "ПАПАГАЛ", "АКУЛА", "МАЙМУНА", "ДЕЛФИН", "КОН", "ЖАБА", "ГЪЛЪБ", "ПАТИЦА", "КУЧЕ", "КОТКА", "МЕЧКА", "ВЪЛК", "ЕЛЕН", "НОСОРОГ", "ПРАСЕ","КРАВА", "ОРЕЛ", "МРАВКА", "МУХА"};
        string[] objects_names = { "", "КОЛЕЛО", "КЪНКИ", "КИТАРА", "КНИГА", "ТОСТЕР", "ХАМАК", "ВЛАК", "БАЛОН", "ТЕЛЕФОН", "ПОДАРЪК", "ЧАДЪР", "РОБОТ", "ШОКОЛАД", "ОЧИЛА", "МЕТЛА", "ДАРТС", "ЛЕНТА", "МОНЕТА", "ЛОПАТА" };
        ArrayList names = new ArrayList();
        char[] letters = {'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ь', 'Ю', 'Я'};
        Label[] l = new Label[10];
        Button[] b = new Button[10];
        Random r = new Random();
        int br = 1, len, sec=45, score;
        SoundPlayer camera = new SoundPlayer(@"..\sounds\camera-shutter-click-03.wav");

        public Form1(int chosen)
        {
            InitializeComponent();
            timer1.Enabled = true;
            if (chosen == 1) ChangeString(names, characters_names);
            if (chosen == 2) ChangeString(names, animals_names);
            if (chosen == 3) ChangeString(names, objects_names);
            timer1.Start();
            l[1] = label1; l[2] = label2; l[3] = label3; l[4] = label4;
            l[5] = label5; l[6] = label6; l[7] = label7; l[8] = label8;
            int posx=75,posy=50;
            for (int i = 1; i <= 8; i++)
            {
                l[i].Parent = pictureBox2;
                l[i].Location = new Point(posx, posy);
                l[i].BringToFront();
                if (i == 4) { posx = 110; posy = 100; }
                else posx += 75;
            }
            int sz = names.Count;
            for (int i = 1; i < sz; i++)
            {
                string s="";
                if (chosen == 1) s = @"..\pictures\pictures_characters\" + i.ToString() + ".jpg";
                if (chosen == 2) s = @"..\pictures\pictures_animals\" + i.ToString() + ".jpg";
                if (chosen == 3) s = @"..\pictures\pictures_objects\" + i.ToString() + ".jpg";
                pics[i] = Image.FromFile(s);
            }
            for (int i = 1; i <= sz-1; i++)
            {
                int x = r.Next(1, sz);
                string s = (string)names[i-1];
                names[i-1] = (string)names[x-1];
                names[x-1] = s;
                Image img = pics[i];
                pics[i] = pics[x];
                pics[x] = img;
            }
            ShowImage(br);
            br++;
        }

        private void ChangeString(ArrayList s1, string[] s2)
        {
            int x=s2.Length;
            for (int i = 1; i < x; i++)
                s1.Add(s2[i]);
        }

        private void ShowImage(int i)
        {
            try
            {
                score = (i - 1) * 10;
                if (i == names.Count) { MessageBox.Show("Ти спечели!"); back(); }
                camera.Play();
                GuessImage.Image = pics[i];
                string name = (string)names[i-1];
                string namecopy = name;
                len = name.Length;
                for (int j = 0; j < len; j++)
                {
                    Button B = new Button();
                    B.FlatStyle = FlatStyle.Flat;
                    B.BackColor = Color.Transparent;
                    B.FlatAppearance.MouseDownBackColor = Color.Transparent;
                    B.FlatAppearance.MouseOverBackColor = Color.Transparent;
                    B.Parent = this;
                    B.Size = new System.Drawing.Size(50, 50);
                    B.Location = new System.Drawing.Point(340 + (6 - len) * 30 + 60 * j, 270);
                    B.BackgroundImage = (Properties.Resources.empty_letter_space);
                    B.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    B.FlatAppearance.BorderSize = 0;
                    this.Controls.Add(B);
                    b[j] = B;
                    b[j].Click += button_click;
                    b[j].BringToFront();
                    b[j].Tag = 0;
                }
                for (int j = 1; j <= 8 - len; j++)
                {
                    int p = r.Next(0, 30);
                    namecopy += letters[p];
                }
                for (int j = 0; j <= 7; j++)
                {
                    int x = r.Next(0, 8);
                    namecopy = swap(namecopy, j, x);
                }
                for (int j = 1; j <= 8; j++)
                    l[j].Text = namecopy[j - 1].ToString();
            }
            catch (Exception) {;}
        }

        private string swap(string s, int pos1, int pos2)
        {
            string ToReturn = string.Empty;
            char[] cs = s.ToCharArray();
            char c = cs[pos1];
            cs[pos1] = cs[pos2];
            cs[pos2] = c;
            foreach (char ch in cs) ToReturn += ch;
            return ToReturn;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (check() == false) { MessageBox.Show("Грешна дума! Опитай пак!"); return; }
            for (int i = 0; i < len; i++)
                b[i].Dispose();
            for (int i = 1; i <= 8; i++)
                l[i].Show();
            ShowImage(br);
            br++;
        }

        private bool check()
        {
            string ns = string.Empty;
            for (int i = 0; i < len; i++)
            {
                int x = (int)b[i].Tag;
                x += 1039;
                char c = (char)x;
                ns += c;
            }
            if (ns == (string)names[br - 2]) return true;
            return false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            try
            {
                int pos = -1 ;
                Label lab = (Label)sender;
                char c = lab.Text[0];
                int i = (int)c-1039;
                string s = @"..\pictures\letters\" + i.ToString() + ".png";
                for(int j=0;j<len;j++)
                    if ((int)b[j].Tag == (0)) { pos = j; break; }
                if (pos == -1) return;
                lab.Hide();
                b[pos].BackgroundImage = Image.FromFile(s);
                b[pos].Tag = i;
            }
            catch (Exception) {;}
        }

        private void back()
        {
            start f = new start();
            f.Show();
            this.Hide();
        }

        private void button_click(object sender, EventArgs e)
        {
            Button but = (Button)sender;
            if ((int)but.Tag==0) return;
            int x=(int)but.Tag;
            x+=1039;
            char c = (char)x;
            for(int i=1;i<=8;i++)
                if (l[i].Visible == false) 
                { 
                    l[i].Text = c.ToString(); 
                    l[i].Show(); 
                    but.BackgroundImage = (Properties.Resources.empty_letter_space);
                    but.Tag = 0;
                    break; 
                }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            back();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sec--;
            if (sec < 0) { timer1.Enabled = false; MessageBox.Show("Времето ти изтече! Резултатът ти е "+score.ToString()+".\nОпитай пак! :)"); back(); return; }
            string temps = @"..\pictures\digital_clock\" + sec.ToString() + ".png";
            pictureBox4.Image = Image.FromFile(temps);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

    }
}
