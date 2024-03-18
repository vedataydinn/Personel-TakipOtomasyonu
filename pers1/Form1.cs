using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//System.Data.OleDb kütüphanesinin eklenmesi
using System.Data.OleDb;


//                                                              PERSONEL TAKİP OTOMASYONU
namespace pers1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //veri tabanı dosya yolu ve provider nesnesinin beirlenmesi
        OleDbConnection baglantim=new OleDbConnection("Provider=Microsoft.Ace.OleDb.16.0;Data Source=persveri.accdb");

        //formlar arası veri aktarımında kullanılacak değişkenler
        public static string tcno, adi, soyadi, yetki;

        // yerel değişkenler yani yalnızca bu formada geçerli olan değişkenler
        int hak = 3;bool durum = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Kullanici Girişi...";
            this.AcceptButton = button1;CancelButton = button2;
            label5.Text=Convert.ToString(hak);
            radioButton1.Checked = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (hak != 0)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select*from kullanicilar", baglantim); 
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    if (radioButton1.Checked == true)
                    {
                        if (kayitokuma["kullaniciadi"].ToString()==textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text&& kayitokuma["yetki"].ToString() == "yönetici")
                        {
                            durum= true;
                            tcno=kayitokuma.GetValue(0).ToString();
                            adi=kayitokuma.GetValue(1).ToString();
                            soyadi=kayitokuma.GetValue(2).ToString();
                            yetki=kayitokuma.GetValue(3).ToString();
                            this.Hide();
                            Form2 frm2= new Form2();
                            frm2.Show();
                            break;
                        }
                    }

                    if (radioButton2.Checked == true)
                    {
                        if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text && kayitokuma["yetki"].ToString() == "kullanıcı")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide();
                            Form3 frm3 = new Form3();
                            frm3.Show();
                            break;
                        }
                    }   
                }
                if (durum == false)
                {
                    hak--;
                    baglantim.Close();
                }
                label5.Text = Convert.ToString(hak);
                if(hak == 0)
                {
                    button1.Enabled = false;
                    MessageBox.Show("Giriş hakkı kalmadı!","SKY Personel Takip Programı",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }
        #region
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        #endregion

    }
}
