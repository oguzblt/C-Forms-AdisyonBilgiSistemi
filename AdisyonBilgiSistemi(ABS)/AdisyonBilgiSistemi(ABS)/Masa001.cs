﻿using AdisyonBilgiSistemi_ABS_.ABSDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdisyonBilgiSistemi_ABS_
{
    public partial class Masa001 : Form
    {
        public Masa001()
        {
            InitializeComponent();
        }
        ABSDataSetTableAdapters.Masa001TableAdapter ms = new ABSDataSetTableAdapters.Masa001TableAdapter();
        private void btnanasayfa_Click(object sender, EventArgs e)
        {
            this.Hide();
            AnaSayfa ana = new AnaSayfa();
            ana.ShowDialog();
            this.Close();
        }

        private void Masa001_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ms.GetData();
            int toplam = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                toplam += Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
            }
            lbltutar1.Text = toplam.ToString();
            if (dataGridView1.RowCount > 1)
            {
                label1.Text = "Masa 001 (açık)";
            }
            else
            {
                label1.Text = "Masa 001";
            }
        }

        private void btngor_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ms.GetData();
            int toplam = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                toplam += Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
            }
            lbltutar1.Text = toplam.ToString();
            if (dataGridView1.RowCount > 1)
            {
                label1.Text = "Masa 001 (açık)";
            }
            else
            {
                label1.Text = "Masa 001";
            }
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ms.InsertQueryMasa1(comboBox1.Text,Convert.ToInt32(numericUpDown1.Value),Convert.ToDouble(textBox1.Text), Convert.ToDouble(numericUpDown1.Value)*Convert.ToDouble(textBox1.Text));
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ms.DeleteQueryMasa1(comboBox1.Text);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;
            string id = dataGridView1.Rows[secim].Cells[0].Value.ToString();
            string ad = dataGridView1.Rows[secim].Cells[1].Value.ToString();
            int adet = Convert.ToInt32(dataGridView1.Rows[secim].Cells[2].Value);
            string fiyat = dataGridView1.Rows[secim].Cells[3].Value.ToString();
            string toplamfiyat = (Convert.ToDouble(adet)*Convert.ToDouble(fiyat)).ToString();

            textBox2.Text = id;
            comboBox1.Text = ad;
            numericUpDown1.Value = adet;
            textBox1.Text = fiyat;
            textBox3.Text = toplamfiyat;
        }

        private void btnduzenle_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ms.UpdateQueryMasa1(comboBox1.Text, Convert.ToInt32(numericUpDown1.Value), Convert.ToDouble(textBox1.Text), Convert.ToDouble(numericUpDown1.Value)*Convert.ToDouble(textBox1.Text),Convert.ToInt32(textBox2.Text));
        }

        private void btnhesap_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Hesabı ödeme işlemini gerçekleştirmek istiyor musunuz?","Hesab Ödeme Ekranı" ,MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                double sonuc = Convert.ToDouble(lbltutar1.Text);
                KasaIslemleri kasa = new KasaIslemleri();
                kasa.dataGridView1.DataSource = KasaIslemleri.ks.InsertQuery("Masa 001",sonuc);
                dataGridView1.DataSource = ms.FullDeleteQueryMasa1();
                MessageBox.Show("Hesap Ödendi","Onay",MessageBoxButtons.OK);
                this.Hide();
                KasaIslemleri ks = new KasaIslemleri();
                ks.ShowDialog();
                this.Close();
            }
            else
            {
                //
            }
        }
    }
}
