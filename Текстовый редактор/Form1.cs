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

namespace Текстовый_редактор
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) //Проверим был бы выбран ли файл
            {
                richTextBox1.Clear(); //Очищаем richTextBox
                openFileDialog1.Filter = "Text Files (*.txt)|*.txt"; //Указываем что нас интересуют только текствые файлы
                string fileName = openFileDialog1.FileName; //получаем наименование файл и путь к нему
                richTextBox1.Text = File.ReadAllText(fileName, Encoding.GetEncoding(1251)); //Передаем содержимое файл и путь к нему
            }
        }

        private void buttonSave_Click_1(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "text Files*.txt"; //Задаем доступные расширения
            saveFileDialog1.DefaultExt = ".txt"; //Задаем расширение по умолчанию

            if (saveFileDialog1.ShowDialog() == DialogResult.OK) //Проверяем подтверждение сохранения информации
            {
                var name = saveFileDialog1.FileName; //Задаем имя файлу
                File.WriteAllText(name, richTextBox1.Text, Encoding.GetEncoding(1251)); //Записываем в файл со держимое textBox с кодировкой 1251
            }
            richTextBox1.Clear();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            string copyText = string.Empty;

            if (richTextBox1.SelectionLength == 0)
                copyText = richTextBox1.Text.Replace("\n", Environment.NewLine);

            else
                copyText = richTextBox1.SelectedText.Replace("\n", Environment.NewLine);

            try
            {
                Clipboard.SetDataObject(copyText, true, 3, 400);
            }

            catch (System.Runtime.InteropServices.ExternalException)
            {
                MessageBox.Show(this, "Не удалось очистить буфер обмена. Возможно буфер обмена используется другим процессом.",
                    "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPaste_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.Red;
        }

        private void buttonFont_Click(object sender, EventArgs e)
        {
            int newFontSize = 14;
            if (newFontSize == 14)
            {
                newFontSize = 20; //размер
                FontStyle style = (FontStyle.Bold | FontStyle.Italic | FontStyle.Underline); //жирный, курсив, подчеркнутый
                richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, (float)newFontSize, style);
            }
        }
    }
}
