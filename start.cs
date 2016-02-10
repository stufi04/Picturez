using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Picturez
{
    public partial class start : Form
    {
        public start()
        {
            InitializeComponent();
            this.Select();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1(1);
            f.Show();
            this.Hide();
        }

        private void start_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1(2);
            f.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1(3);
            f.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Програмата е направена от Мартина Коцева и Стефан Иванов. All rights reserved 2013");
        }
    }
}
