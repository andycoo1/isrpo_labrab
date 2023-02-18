using kursach;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using SD = System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form9 : Form
    {
        public SD.DataTableCollection tableCollection = null;
        public Form9()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1;
            if (main != null)
            {
                try
                {
                    SD.DataRow nRow = main.accountDataSet.Tables[0].NewRow();
                    int rc = main.dataGridView2.RowCount + 1;
                    nRow[1] = rc;
                    nRow[2] = textBox1.Text;
                    nRow[3] = comboBox1.Text;
                    nRow[4] = comboBox2.Text;
                    nRow[5] = comboBox3.Text;

                    try { nRow[6] = maskedTextBox1.Text; }
                    catch { MessageBox.Show("Введена неверная дата!", "Ошибка!"); return; };

                    nRow[7] = comboBox4.Text;
                    nRow[8] = textBox2.Text;
                    nRow[9] = textBox3.Text;
                    nRow[10] = textBox4.Text;
                    main.accountDataSet.Tables[0].Rows.Add(nRow);
                    main.mPTableAdapter.Update(main.accountDataSet.MP);
                    main.accountDataSet.Tables[0].AcceptChanges();
                    main.dataGridView2.Refresh();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    maskedTextBox1.Text = "";
                }
                catch { MessageBox.Show("Необходимо заполнить все поля!", "Ошибка!"); };
            }
            main.dataGridView2.ClearSelection();
            main.proof = 0;
            main.mPTableAdapter.Update(main.accountDataSet.MP);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

}
