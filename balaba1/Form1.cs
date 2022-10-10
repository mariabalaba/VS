using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace balaba1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            oleDbDataAdapter1.Fill(dataSet11);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;

            oleDbSelectCommand1.CommandText = "SELECT ArtistID, ArtistName FROM Artist\r\n" + "WHERE (ArtistName LIKE ? + '%')";
            oleDbSelectCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter());
            oleDbSelectCommand1.Parameters[0].Value = "A";

            oleDbDataAdapter1.Fill(dataSet11);

            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }



        private void btnLoadList_Click(object sender, EventArgs e)
        {
            NewLoad();
        }

        void NewLoad()
        {
            String text = txtCustLimit.Text.Trim();
            txtCustLimit.Text = text;
            oleDbSelectCommand1.Parameters[0].Value = text;

            dataSet11.Clear();
            oleDbDataAdapter1.Fill(dataSet11);
        }

        private void txtCustLimit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                NewLoad();
            txtCustLimit.Text = "искать";//подсказка
            txtCustLimit.ForeColor = Color.Gray;
        }

        private void txtCustLimit_Enter(object sender, EventArgs e)//происходит когда элемент стает активным
        {
            txtCustLimit.Text = null;
            txtCustLimit.ForeColor = Color.Black;
        }
 

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataSet21.Clear();

            if (listBox1.SelectedIndex != -1)
            {
                oleDbSelectCommand2.Parameters[0].Value = listBox1.SelectedValue;
                oleDbDataAdapter2.Fill(dataSet21);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.BindingContext[dataSet21, "Artist"].CancelCurrentEdit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        private void SaveRecord()
        {
            this.BindingContext[dataSet21, "Artist"].EndCurrentEdit();
            oleDbDataAdapter2.Update(dataSet21, "Artist");

            int index = listBox1.SelectedIndex;
            NewLoad();
            int count = listBox1.Items.Count - 1;
            index = index < count ? index : count;
            listBox1.SelectedIndex = index;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.BindingContext[dataSet21, "Artist"].AddNew();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count <= 0)
                return;

            int index = this.BindingContext[dataSet21, "Artist"].Position;
            this.BindingContext[dataSet21, "Artist"].RemoveAt(index);

            SaveRecord();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveRecord();
        }
    }
}

