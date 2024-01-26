using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //sql kütüphanesi

namespace Film_Öneri_Projesi
{
    public partial class Form1 : Form
    {
        SqlConnection baglanti = new SqlConnection("server=.; Initial catalog=VeriTabanıProjesi; Integrated Security=SSPI");        
        DataSet ds;
        SqlDataAdapter verial;
        SqlCommand komut;
        string tablo = "";


        public Form1()
        {
            InitializeComponent();
        }
        public void listele()
        {
            baglanti.Open();

            verial = new SqlDataAdapter("Select * from Filmler", baglanti);
            ds = new DataSet();
            verial.Fill(ds);//gelen veriler dataset içerisine doldurulsun
            dataGridView2.DataSource = ds.Tables[0];

            baglanti.Close();
        }

        public void listelee()
        {
            baglanti.Open();

            verial = new SqlDataAdapter("Select * from Director", baglanti);
            ds = new DataSet();
            verial.Fill(ds);//gelen veriler dataset içerisine doldurulsun
            dataGridView2.DataSource = ds.Tables[0];

            baglanti.Close();
        }
        public void listeleee()
        {
            baglanti.Open();

            verial = new SqlDataAdapter("Select * from Actress", baglanti);
            ds = new DataSet();
            verial.Fill(ds);//gelen veriler dataset içerisine doldurulsun
            dataGridView2.DataSource = ds.Tables[0];

            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string filtrele = "";

            
            if (checkBox1.Checked == true)
            {

                if (comboBox1.Text == "" ) { MessageBox.Show("İşaretlediğiniz Kutunun Seçeneklerinden Birini Seçiniz Ya Da İşaretinizi Kaldırınız Aksi Halde Yanlış Filtreleme Olabilir."); }
                else
                { 
                    comboBox1.Enabled = true;
                    filtrele += "and Tur= '" + comboBox1.Text + "'"; //türler gelecek
                }
            }
            if (checkBox2.Checked == true)
            {
                    if (comboBox2.Text == "") { MessageBox.Show("İşaretlediğiniz Kutunun Seçeneklerinden Birini Seçiniz Ya Da İşaretinizi Kaldırınız Aksi Halde Yanlış Filtreleme Olabilir."); }
                    else
                    {
                        comboBox2.Enabled = true;
                        filtrele += "and OyuncuAdi='" + comboBox2.Text + "'"; //oyuncular gelecek
                    }
            }
            if (checkBox3.Checked == true)
            {
                    if (comboBox3.Text == "") { MessageBox.Show("İşaretlediğiniz Kutunun Seçeneklerinden Birini Seçiniz Ya Da İşaretinizi Kaldırınız Aksi Halde Yanlış Filtreleme Olabilir."); }
                    else
                    {
                        comboBox3.Enabled = true;
                        filtrele += "and YonetmenAdi='" + comboBox3.Text + "'"; //yönetmenler gelecek
                    }
            }

            SqlDataAdapter verial1 = new SqlDataAdapter("select distinct Filmismi, FilmSuresi, FilmYili, Platform from Actress a, Director d, Type t, FilmActress fa, FilmDirector fd, FilmType ft, Filmler f " +
            "where a.OyuncuID = fa.OyuncuID and d.YonetmenID = fd.YonetmenID and t.TurID = ft.TurID and f.FilmID = fa.FilmID and f.FilmID = fd.FilmID and f.FilmID = ft.FilmID " + filtrele, baglanti);

            DataSet ds1 = new DataSet();
            verial1.Fill(ds1);
            dataGridView1.DataSource = ds1.Tables[0];
            baglanti.Close();                        
        }

        private void button2_Click(object sender, EventArgs e)
        {            
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "") 
            { 
                MessageBox.Show("İstenilen Bilgilerin Kutularını Boş Bırakmayalım Lütfen");

            }
            else
            {
                komut = new SqlCommand("insert into Filmler (Filmismi, FilmSuresi, FilmYili) values (@isim, @filmsuresi, @filmyili)", baglanti);
                komut.Parameters.AddWithValue("@isim", textBox1.Text);
                komut.Parameters.AddWithValue("@filmsuresi", Convert.ToInt32(textBox2.Text));
                komut.Parameters.AddWithValue("@filmyili", Convert.ToInt32(textBox3.Text));
                try
                {
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                }
                catch
                { MessageBox.Show("Bağlantı Hatası"); }

                MessageBox.Show("Öneriniz İçin Teşekkürler.");
                listele();
            }     

        }
        private void button3_Click(object sender, EventArgs e)
        {
            if(tablo=="Director")
            {
                if (label8.Text == "") MessageBox.Show("Lütfen Tablodan Güncellemek İstediğiniz Yönetmeni Seçiniz");
                else
                {
                    komut = new SqlCommand("update Director set YonetmenAdi=@yonetmenadi where YonetmenID=@yonetmenid", baglanti);
                    komut.Parameters.AddWithValue("@yonetmenadi", textBox4.Text);
                    komut.Parameters.AddWithValue("@yonetmenid", label8.Text);

                    try
                    {
                        baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                    }
                    catch { MessageBox.Show("Bağlantı Hatası"); }

                    MessageBox.Show("Yönetmenler Tablosu Güncellendi");
                    listelee();
                }
            }    
            
            if(tablo=="Actress")
            {
                if (label9.Text == "") MessageBox.Show("Lütfen Tablodan Güncellemek İstediğiniz Oyuncuyu Seçiniz");
                else
                {
                    komut = new SqlCommand("update Actress set OyuncuAdi=@oyuncuadi where OyuncuID=@oyuncuid", baglanti);
                    komut.Parameters.AddWithValue("@oyuncuadi", textBox5.Text);
                    komut.Parameters.AddWithValue("@oyuncuid", label9.Text);

                    try
                    {
                        baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                    }
                    catch { MessageBox.Show("Bağlantı Hatası"); }

                    MessageBox.Show("Oyuncular Tablosu Güncellendi");
                    listeleee();
                }
            }

            if(tablo=="Filmler")
            {
                if (label12.Text == "") MessageBox.Show("Lütfen Tablodan Güncellemek İstediğiniz Filmi Seçiniz");
                else
                {
                    komut = new SqlCommand("update Filmler set Filmismi=@filmismi, FilmSuresi=@filmsuresi, FilmYili=@filmyili, Platform=@platform where FilmID=@filmid", baglanti);
                    komut.Parameters.AddWithValue("@filmismi", textBox6.Text);
                    komut.Parameters.AddWithValue("@filmsuresi",textBox7.Text);
                    komut.Parameters.AddWithValue("@filmyili",textBox8.Text);
                    komut.Parameters.AddWithValue("@filmid", label12.Text);
                    komut.Parameters.AddWithValue("@platform", textBox9.Text);

                    try
                    {
                        baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                    }
                    catch { MessageBox.Show("Bağlantı Hatası"); }

                    MessageBox.Show("Filmler Tablosu Güncellendi");
                    listele();
                }
            }

            
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            if (label8.Text == "" && label9.Text == "")
            {
                if (tablo == "Director")
                {
                    komut = new SqlCommand("insert into Director(YonetmenAdi) values (@yonetmenadi) ", baglanti);
                    komut.Parameters.AddWithValue("@yonetmenadi", textBox4.Text);

                    try
                    {
                        baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                    }
                    catch { MessageBox.Show("Bağlantı Hatası"); }

                    MessageBox.Show("Ekleme İşlemi Başarılı Oldu");
                    listelee();
                }

                if (tablo == "Actress")
                {
                    komut = new SqlCommand("insert into Actress(OyuncuAdi) values (@oyuncuadi)", baglanti);
                    komut.Parameters.AddWithValue("@oyuncuadi", textBox5.Text);

                    try
                    {
                        baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                    }
                    catch { MessageBox.Show("Bağlantı Hatası"); }

                    MessageBox.Show("Ekleme İşlemi Başarılı Oldu");
                    listelee();
                }
            }

            else { MessageBox.Show("Ekleme Yapamazsınız Hücre Dolu "); }

        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (label12.Text == "") MessageBox.Show("Film Seçiniz");
            else
            {
                DialogResult tus = MessageBox.Show("Filmi Silmek İstediğinize Emin Misiniz?", "Filmler Tablosu", MessageBoxButtons.YesNo);
                if (tus == DialogResult.Yes)
                {
                    komut = new SqlCommand("delete from Filmler where FilmID=@filmID", baglanti);

                    komut.Parameters.AddWithValue("@filmID", label12.Text);
                    try
                    {
                        baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                    }
                    catch { MessageBox.Show("Bağlantı Hatası"); }
                    MessageBox.Show("Silme İşlemi Başarılı Oldu");
                    listele();

                }                
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Text="Film Süresi";
            textBox8.Text = "Film Yılı";
            textBox9.Text = "Platform";
            label8.Text = "";
            label9.Text = "";
            label12.Text = "";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            baglanti.Open();

            verial = new SqlDataAdapter("Select * from Actress", baglanti);
            tablo = "Actress";
            ds = new DataSet();

            verial.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];

            baglanti.Close();
        }      
        
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            baglanti.Open();

            verial = new SqlDataAdapter("Select * from Director", baglanti);
            tablo = "Director";
            ds = new DataSet();
            verial.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];

            baglanti.Close();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            baglanti.Open();

            verial = new SqlDataAdapter("Select * from Filmler", baglanti);
            tablo = "Filmler";
            ds = new DataSet();
            verial.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];

            baglanti.Close();
        }



        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tablo=="Director")
            {
                textBox5.Text = "";
                label9.Text = "";
                textBox6.Text = "";
                label12.Text = "";
                textBox4.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                label8.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            }
            
            if(tablo=="Actress")
            {
                textBox4.Text = "";
                label8.Text = "";
                textBox6.Text = "";
                label12.Text = "";
                textBox5.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                label9.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            }
            if(tablo=="Filmler")
            {
                textBox4.Text = "";
                textBox5.Text = "";
                label8.Text = "";
                label9.Text = "";
                textBox9.Text ="";
                textBox7.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                textBox8.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
                textBox6.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                textBox9.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
                label12.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();

            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) comboBox1.Enabled = true;
            else
            {
                comboBox1.Enabled = false;
                comboBox1.Text = "";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true) comboBox2.Enabled = true;
            else
            {
                comboBox2.Enabled = false;
                comboBox2.Text = "";
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox3.Checked == true) comboBox3.Enabled = true;
            else
            {
                comboBox3.Enabled = false;
                comboBox3.Text = "";
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'veriTabanıProjesiDataSet6.Director' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.directorTableAdapter.Fill(this.veriTabanıProjesiDataSet6.Director);
            // TODO: Bu kod satırı 'veriTabanıProjesiDataSet5.Actress' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.actressTableAdapter.Fill(this.veriTabanıProjesiDataSet5.Actress);
            // TODO: Bu kod satırı 'veriTabanıProjesiDataSet4.Type' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.typeTableAdapter.Fill(this.veriTabanıProjesiDataSet4.Type);
            // TODO: Bu kod satırı 'veriTabanıProjesiDataSet3.Filmler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
                      
        }      
        
        private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void tabPage1_Click(object sender, EventArgs e)
        {
        }
        private void tabPage3_Click(object sender, EventArgs e)
        {
        }

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}
