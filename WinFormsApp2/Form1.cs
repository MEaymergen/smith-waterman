using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //default match mismatch ve gap values..
            matchBox.Text = "1";
            mismatchBox.Text = "-1";
            gapBox.Text = "-2";
        }
        public static string TersCevir(string metin)
        {
            string sonuc = "";
            for (int i = metin.Length - 1; i >= 0; i--)
                sonuc += metin[i];
            return sonuc;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime then = DateTime.Now;
            dataGridView1.Rows.Clear();
            //Read file
            StreamReader SR = new StreamReader("C:\\Users\\MEaym\\Desktop\\LibrarySolution\\WinFormsApp2\\seq1.txt");
            textBox1.Text = SR.ReadLine();
            SR.Close();

            StreamReader SR2 = new StreamReader("C:\\Users\\MEaym\\Desktop\\LibrarySolution\\WinFormsApp2\\seq2.txt");
            textBox2.Text = SR2.ReadLine();
            SR2.Close();
            //

            string baz1;
            baz1 = Convert.ToString(textBox1.Text);
            string baz2;
            baz2 = Convert.ToString(textBox2.Text);
            dataGridView1.RowCount = baz1.Length + 2;
            dataGridView1.ColumnCount = baz2.Length + 2;
            dataGridView1.ColumnHeadersVisible = false;


            //placing bases in rows and columns
            for (int i = 2; i < baz1.Length + 2; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = Convert.ToString(baz1[i - 2]);

            }
            for (int i = 2; i < baz2.Length + 2; i++)
            {
                dataGridView1.Rows[0].Cells[i].Value = Convert.ToString(baz2[i - 2]);
            }//


            int match = Convert.ToInt32(matchBox.Text);
            int mismatch = Convert.ToInt32(mismatchBox.Text);
            int gap = Convert.ToInt32(gapBox.Text);

            dataGridView1.Rows[1].Cells[1].Value = 0;
            //Adding gap values
            for (int i = 2; i < baz2.Length + 2; i++)
            {
                dataGridView1.Rows[1].Cells[i].Value = Convert.ToInt32(dataGridView1.Rows[1].Cells[i - 1].Value) + gap;
                int kontrol = Convert.ToInt32(dataGridView1.Rows[1].Cells[i].Value) + gap;
                if (kontrol < 0)
                {
                    dataGridView1.Rows[1].Cells[i].Value = 0;
                }
            }
            for (int i = 2; i < baz1.Length + 2; i++)
            {
                dataGridView1.Rows[i].Cells[1].Value = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[1].Value) + gap;
                int kontrol = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[1].Value) + gap;
                if (kontrol < 0)
                {
                    dataGridView1.Rows[i].Cells[1].Value = 0;
                }
            }
            int ustdeger;
            int soldeger;
            int solust;

            for (int i = 2; i < baz1.Length + 2; i++)//row
            {
                for (int j = 2; j < baz2.Length + 2; j++)//column
                {
                    ustdeger = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j].Value) + gap;
                    soldeger = Convert.ToInt32(dataGridView1.Rows[i].Cells[j - 1].Value) + gap;

                    if (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value) != Convert.ToString(dataGridView1.Rows[0].Cells[j].Value))//if bases diff
                    {
                        solust = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j - 1].Value) + mismatch;

                        if (soldeger >= ustdeger && soldeger >= solust)//if left bigger
                        {
                            dataGridView1.Rows[i].Cells[j].Value = soldeger;
                            if (Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value) <= 0)
                            {
                                dataGridView1.Rows[i].Cells[j].Value = 0;
                            }
                        }
                        else if (ustdeger >= soldeger && ustdeger >= solust)//if top bigger
                        {
                            dataGridView1.Rows[i].Cells[j].Value = ustdeger;
                            if (Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value) <= 0)
                            {
                                dataGridView1.Rows[i].Cells[j].Value = 0;
                            }

                        }
                        else if (solust >= ustdeger && solust >= soldeger)//if topleft bigger
                        {
                            dataGridView1.Rows[i].Cells[j].Value = solust;
                            if (Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value) <= 0)
                            {
                                dataGridView1.Rows[i].Cells[j].Value = 0;
                            }

                        }
                    }
                    else //if bases same
                    {
                        solust = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j - 1].Value) + match;
                        if (soldeger >= ustdeger && soldeger >= solust)//left values bigger
                        {
                            dataGridView1.Rows[i].Cells[j].Value = soldeger;
                            if (Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value) <= 0)
                            {
                                dataGridView1.Rows[i].Cells[j].Value = 0;
                            }

                        }
                        else if (ustdeger >= soldeger && ustdeger >= solust)//top values bigger
                        {
                            dataGridView1.Rows[i].Cells[j].Value = ustdeger;
                            if (Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value) <= 0)
                            {
                                dataGridView1.Rows[i].Cells[j].Value = 0;
                            }

                        }
                        else if (solust >= ustdeger && solust >= soldeger)//topleft bigger
                        {
                            dataGridView1.Rows[i].Cells[j].Value = solust;
                            if (Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value) <= 0)
                            {
                                dataGridView1.Rows[i].Cells[j].Value = 0;
                            }
                        }
                    }
                }
            }
            int enBuyuk = 0;
            int newI = 0;
            int newJ = 0;
            for (int i = 2; i < baz1.Length + 2; i++)
            {
                for (int j = 2; j < baz2.Length + 2; j++)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value) >= enBuyuk)
                    {
                        enBuyuk = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                        newI = i;
                        newJ = j;
                    }
                }
            }
            int puan = Convert.ToInt32(dataGridView1.Rows[newI].Cells[newJ].Value);
            label7.Text = "Puan : " + puan;

            string text1 = "";
            string text2 = "";

            dataGridView1.Rows[newI].Cells[newJ].Style.BackColor = Color.LightGreen;

            for (int j = newJ, i = newI; j > 1 || i > 1;)
            {
                if (Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value) != 0)
                {
                    if (i == 1)
                    {
                        dataGridView1.Rows[i].Cells[j - 1].Style.BackColor = Color.LightGreen;
                        j--;
                    }
                    else if (j == 1)
                    {
                        dataGridView1.Rows[i - 1].Cells[j].Style.BackColor = Color.LightGreen;
                        i--;
                    }
                    else if (Convert.ToInt32(dataGridView1.Rows[i].Cells[j - 1].Value) + gap >= Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value))
                    {
                        dataGridView1.Rows[i].Cells[j - 1].Style.BackColor = Color.LightGreen;
                        text1 += "-";
                        text2 += Convert.ToString(dataGridView1.Rows[i].Cells[0].Value); j--;
                    }
                    else if (Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j].Value) + gap >= Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value))
                    {
                        dataGridView1.Rows[i - 1].Cells[j].Style.BackColor = Color.LightGreen;
                        text1 += Convert.ToString(dataGridView1.Rows[0].Cells[j].Value);
                        text2 += "-"; i--;
                    }
                    else if (Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j - 1].Value) + gap >= Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value))
                    {
                        dataGridView1.Rows[i - 1].Cells[j - 1].Style.BackColor = Color.LightGreen;
                        text1 += Convert.ToString(dataGridView1.Rows[0].Cells[j - 1].Value);
                        text2 += Convert.ToString(dataGridView1.Rows[i - 1].Cells[0].Value); i--; j--;
                    }
                    else if (Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value) != 0)
                    {
                        dataGridView1.Rows[i - 1].Cells[j - 1].Style.BackColor = Color.LightGreen;
                        text1 += Convert.ToString(dataGridView1.Rows[0].Cells[j - 1].Value);
                        text2 += Convert.ToString(dataGridView1.Rows[i - 1].Cells[0].Value);
                        i--; j--;
                    }
                }
                else
                {

                    dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.LightGreen;
                    break;
                }
            }
            yol.Text = TersCevir(text1) + "\n" + TersCevir(text2);

            /////////////////////////operation time
            DateTime now = DateTime.Now;
            label6.Text = "Çalışma Süresi: " + (now.Millisecond - then.Millisecond) + "ms";

        }

        private void yolLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
