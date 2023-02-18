using System;
using System.Collections.Generic;
using System.ComponentModel;
using SD = System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ExcelDataReader;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using WindowsFormsApp1;
using System.Text.RegularExpressions;

namespace kursach
{
    public partial class Form1 : Form
    {
        public int newcol = 1;
        public int proof, col, cash, free, child, old, vet, all, ogr, other, young, mode, schet, people, act, online, offline = 0;

        public Form1()
        {
            InitializeComponent();
        }
        //Добавим пару комментариев...
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.mPTableAdapter.Update(this.accountDataSet.MP);
        }

        //Функция сохранения таблицы
        private void SaveTable(DataGridView What_Save)
        {
            string path = System.IO.Directory.GetCurrentDirectory() + @"\" + "Save_File.xlsx";

            Excel.Application exApp = new Excel.Application();
            Excel.Workbook wb = exApp.Workbooks.Add();
            Excel.Worksheet ws = (Excel.Worksheet)exApp.ActiveSheet;

            int i, j;
            for (i = 0; i <= What_Save.RowCount - 1; i++)
            {
                for( j = 0; j <= What_Save.ColumnCount - 1; j++)
                {
                    ws.Cells[1, j + 1] = What_Save.Columns[j].HeaderText.ToString();
                    ws.Cells[i + 2, j + 1] = What_Save[j, i].Value.ToString();
                }
            }
            exApp.AlertBeforeOverwriting = false;
            wb.SaveAs(path);
            exApp.Quit();
        }

        //Функция фильта
        private void DGVFilter(int col)
        {
            foreach (DataGridView dgv in this.Controls.OfType<DataGridView>())
            {
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    dgv.CurrentCell = null;
                    dgv.Rows[i].Visible = false;
                    for (int c = 1; c < dgv.ColumnCount; c++)
                    {
                        if (dgv.Rows[i].Cells[c].Value != null)
                        {
                            if (dgv.Rows[i].Cells[col].Value.ToString().Contains(textBox2.Text))
                            {
                                dgv.Rows[i].Visible = true;
                                break;
                            }
                        }
                    }
                }
            }
        }

        //кнопка сохранить
        private void button1_Click(object sender, EventArgs e)
        {
            try 
            { 
                this.mPTableAdapter.Update(this.accountDataSet.MP);
                MessageBox.Show("Файл успешно сохранен!", "Успех!");
            }
            catch { MessageBox.Show("Исходный файл ничем не отличается от текущего.\nСохранение отменено.", "Информация"); };
        }

        //поиск
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var match = Regex.Match(textBox1.Text, @"\d{2}\.\d{2}.\d{4}");
            foreach (DataGridView dgv in this.Controls.OfType<DataGridView>())
            {
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    dgv.Rows[i].Selected = false;
                    for (int j = 1; j < dgv.ColumnCount; j++)
                        if (dgv.Rows[i].Cells[j].Value != null)
                        {
                            if (dgv.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))
                            {
                                dgv.Rows[i].Selected = true;
                                break;
                            }
                        }
                }
            }
            proof = 0;
            if (textBox1.Text == "") { dataGridView2.ClearSelection(); }
        }
        //очистка поиска
        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        //----------------ВЕРХНЕЕ МЕНЮ
        //выход верхний
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите выйти?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        //вид
        //тема
        //тёмная тема
        private void тёмнаяТемаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView2.EnableHeadersVisualStyles = false;
            this.BackColor = Color.FromArgb(red: 53, green: 51, blue: 88); 
            groupBox1.ForeColor = Color.FromArgb(red: 210, green: 210, blue: 210);
            groupBox2.ForeColor = Color.FromArgb(red: 210, green: 210, blue: 210);
            button1.BackColor = Color.FromArgb(red: 244, green: 172, blue: 196);
            button6.BackColor = Color.FromArgb(red: 244, green: 172, blue: 196);
            button7.BackColor = Color.FromArgb(red: 244, green: 172, blue: 196);
            button9.BackColor = Color.FromArgb(red: 244, green: 172, blue: 196);
            button10.BackColor = Color.FromArgb(red: 244, green: 172, blue: 196);
            button11.BackColor = Color.FromArgb(red: 244, green: 172, blue: 196);
            button1.ForeColor = Color.FromArgb(red: 6, green: 10, blue: 48);
            button6.ForeColor = Color.FromArgb(red: 6, green: 10, blue: 48);
            button7.ForeColor = Color.FromArgb(red: 6, green: 10, blue: 48);
            button9.ForeColor = Color.FromArgb(red: 6, green: 10, blue: 48);
            button10.ForeColor = Color.FromArgb(red: 6, green: 10, blue: 48);
            button11.ForeColor = Color.FromArgb(red: 6, green: 10, blue: 48);
            dataGridView2.BackgroundColor = Color.FromArgb(red: 89, green: 91, blue: 130);
            dataGridView2.GridColor = Color.FromArgb(red: 210, green: 210, blue: 210);
            dataGridView2.ForeColor = Color.FromArgb(red: 89, green: 91, blue: 130);
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                for (int c = 0; c < dataGridView2.ColumnCount; c++)
                {
                    dataGridView2.Rows[i].Cells[c].Style.BackColor = Color.FromArgb(red: 89, green: 91, blue: 130);
                    dataGridView2.Rows[i].Cells[c].Style.ForeColor = Color.FromArgb(red: 210, green: 210, blue: 210);
                    
                }    
            }
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(red: 53, green: 51, blue: 88);
            dataGridView2.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(red: 89, green: 91, blue: 130);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor =
                dataGridView2.RowHeadersDefaultCellStyle.ForeColor = Color.FromArgb(red: 210, green: 210, blue: 210);
            menuStrip1.BackColor = Color.FromArgb(red: 53, green: 51, blue: 88);
            menuStrip1.ForeColor = Color.FromArgb(red: 210, green: 210, blue: 210);
            
        }
        //справка
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа создана в рамках курсового проекта\nАвтор: Бутц А.А. гр. 082", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //выход
        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите выйти?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.mPTableAdapter.Update(this.accountDataSet.MP);
                Application.Exit();
            }
        }
        //информация
        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа работает с базой данных Access.\n" +
                "Разработано в рамках курсового проекта студентом гр. 082 Бутцем А. А.\n\n\n" +
                "Чтобы добавить запись, нажмите на кнопку 'Добавить запись', заполните форму и нажмите 'Добавить'\n\n" +
                "Чтобы удалить запись, выберите мышкой запись которую хотите удалить, нажмите на кнопку 'Удалить запись' и подтвердите действие\n\n" +
                "Чтобы увидеть статистику за определенный промежуток времени, нажмите на кнопку 'Статистика', заполните форму и нажмите на кнопку 'Показать статистику'\n\n" +
                "Чтобы воспользоваться быстрым поиском, впишите в левое поле поиска искомую информацию\n\n" +
                "Чтобы убрать выделение со строк после быстрого поиска, нажмите на кнопку 'Сброс результатов'\n\n" +
                "Чтобы воспользоваться расширенным поиском, впишите в правое поле поиска искомую информацию, поставьте флажок в нужное положение и нажмите 'Поиск'\n\n" +
                "Чтобы убрать результаты поиска, нажмите на кнопку 'Сброс фильтра'\n\n" +
                "Чтобы сохранить результат работы в программе, нажмите кнопку 'Сохранить' и дождитесь сообщения о конце сохранения.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        //добавить запись
        public void button9_Click(object sender, EventArgs e)
        {
            Form9 af = new Form9{ Owner = this };
            af.Show();
        }
        //статистика
        private void button11_Click(object sender, EventArgs e)
        {
            FormStat af = new FormStat { Owner = this };
            af.Show();
        }
        //удалить запись
        private void button10_Click(object sender, EventArgs e)
        {
            if (proof == 1)
            {
                DialogResult result = MessageBox.Show("Вы действительно хотите удалить данную запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        int a = dataGridView2.CurrentRow.Index;
                        dataGridView2.Rows.Remove(dataGridView2.Rows[a]);
                    }
                    catch { MessageBox.Show("Не выделена строка которую нужно удалить", "Ошибка!"); };
                    dataGridView2.ClearSelection();
                    proof = 0;
                }
            }
            else
            {
                MessageBox.Show("Не выделена строка которую нужно удалить!", "Ошибка!");
            }
            this.mPTableAdapter.Update(this.accountDataSet.MP);
        }
        //проверка для удаления
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            proof = 1;
        }

        //------------------------ФИЛЬТРАЦИЯ
        //очистка фильтра
        private void textBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox2.Clear();
        }
        //фильтрация
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true) { col = 2; DGVFilter(col); }
            else if (radioButton3.Checked == true) { col = 3; DGVFilter(col); }
            else if (radioButton4.Checked == true) { col = 4; DGVFilter(col); }
            else if (radioButton5.Checked == true) { col = 5; DGVFilter(col); }
            else if (radioButton6.Checked == true) { col = 6; DGVFilter(col); }
            else if (radioButton1.Checked == true) { col = 7; DGVFilter(col); }
            else if (radioButton8.Checked == true) { col = 8; DGVFilter(col); }
            else if (radioButton7.Checked == true) { col = 9; DGVFilter(col); }
            else if (radioButton9.Checked == true) { col = 10; DGVFilter(col); }
            if (textBox2.Text == "")
            {
                for (int i = 0; i <= dataGridView2.Rows.Count - 1; i++)
                {
                    dataGridView2.CurrentCell = null;
                    dataGridView2.Rows[i].Visible = true;
                }
            }
        }

        //подключение БД
        public void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "accountDataSet.MP". При необходимости она может быть перемещена или удалена.
            mPTableAdapter.Fill(accountDataSet.MP);
            dataGridView2.Sort(dataGridView2.Columns[6], ListSortDirection.Ascending);
            dataGridView2.ClearSelection();
            proof = 0;
        }
    }
}
