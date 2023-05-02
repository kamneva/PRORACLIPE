using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime;
using System.Data.SqlClient;

namespace KursovayaPoBD
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;

        public Form1()
        {
            InitializeComponent();

            List<string> Haracternagruzki = new List<string>
            {
             "Спокойная нагрузка. Пусковая нагрузка до 120% номинальной",
             "Умеренные колебания нагрузки. Пусковая нагрузка до 150%",
             "Значительные колебания нагрузки. Пусковая нагрузка до 200% номинальной",
             "Весьма неравномерная и ударная нагрузка. Пусковая нагрузка до 300% номинальной"

            };
            domainUpDown2.Items.AddRange(Haracternagruzki);
            domainUpDown2.TextChanged += domainUpDown2_TextChanged;


            List<string> TipDvigatelya = new List<string>
            {
             "I",
             "II"
            };
            domainUpDown4.Items.AddRange(TipDvigatelya);
            domainUpDown4.TextChanged += domainUpDown4_SelectedItemChanged;

            List<string> ProfilRemnya = new List<string>
            {
             "К",
             "Л"
            };
            domainUpDown1.Items.AddRange(ProfilRemnya);
            domainUpDown1.TextChanged += domainUpDown1_SelectedItemChanged;

            List<string> VidNatyazheniya = new List<string>
            {
             "Автоматическое натяжение ремня",
             "Механическое натяжение ремня"
            };
            domainUpDown5.Items.AddRange(VidNatyazheniya);
            domainUpDown5.TextChanged += domainUpDown5_SelectedItemChanged;

        }

        private async void Form1_Load_1(object sender, EventArgs e)
        {
            this.Text = "PRORACLIPE";
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Database.mdf;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Det]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                listBox1.Items.Add("С о х р а н е н н ы е   д а н н ы е : ");
                while (await sqlReader.ReadAsync())
                {
                        listBox1.Items.Add("ID: " + Convert.ToString(sqlReader["DetID"]) + "\tХарактер нагрузки: " + Convert.ToString(sqlReader["XH"]) + "\tТип машины: " +
                        Convert.ToString(sqlReader["TM"]) + "\tТип двигателя: " + Convert.ToString(sqlReader["TD"]) + "\tВид натяжения ремня: " +
                        Convert.ToString(sqlReader["VNR"]) + "\talpha1: " + Convert.ToString(sqlReader["alpha1"]) + "\tПрофиль ремня: " +
                        Convert.ToString(sqlReader["PR"]) + "\tZ: " + Convert.ToString(sqlReader["Z"]) + "\tv: " +
                        Convert.ToString(sqlReader["v"]) + "\tC3:" +
                        Convert.ToString(sqlReader["C3"]) + "\tS10:" + Convert.ToString(sqlReader["S10"]) + "\tq10:" +
                        Convert.ToString(sqlReader["q10"]) + "\tC1:" + Convert.ToString(sqlReader["C1"]) + "\tX:" +
                        Convert.ToString(sqlReader["X"]) + "\tФ0: " + Convert.ToString(sqlReader["F0"]) + "\tУгол О: " +
                        Convert.ToString(sqlReader["O"]) + "\ty1: " + Convert.ToString(sqlReader["y1"]) + "\tR: " +
                        Convert.ToString(sqlReader["R"]) + "\tg0: " + Convert.ToString(sqlReader["g0"]) + "\tQ0: " +
                        Convert.ToString(sqlReader["Q0"]) + "\tДостоверность рассчета: " + Convert.ToString(sqlReader["dost"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown1.BackColor = Color.White;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            domainUpDown1.BackColor = Color.White;
            if (domainUpDown1.Text == "К")
            {
                textBox4.Text = "60";
                textBox5.Text = "0,09";
                numericUpDown2.Minimum = 0.75m;
                numericUpDown2.Maximum = 0.85m;
                numericUpDown2.Value = 0.75m;
            }
            if (domainUpDown1.Text == "Л")
            {
                textBox4.Text = "330";
                textBox5.Text = "0,45";
                numericUpDown2.Minimum = 0.65m;
                numericUpDown2.Maximum = 0.75m;
                numericUpDown2.Value = 0.65m;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double F=0, f0=0, C1=0, C3=0, v=0, S10=0, Z=0, g0=0, X=0, q10=0, Q0=0, a1=0, y1=0, O=0, R=0, tanO = 0;
            try
            {
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";
                textBox13.Text = "";
                textBox1.BackColor = Color.White;
                textBox2.BackColor = Color.White;
                textBox3.BackColor = Color.White;
                numericUpDown1.BackColor = Color.White;
                domainUpDown1.BackColor = Color.White;
                domainUpDown2.BackColor = Color.White;
                domainUpDown3.BackColor = Color.White;
                domainUpDown4.BackColor = Color.White;
                domainUpDown5.BackColor = Color.White;
                numericUpDown3.BackColor = Color.White;
                numericUpDown2.BackColor = Color.White;

                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" ||
                   numericUpDown1.Text == "" || domainUpDown1.Text == "" || domainUpDown2.Text == "" ||
                   domainUpDown3.Text == "" || domainUpDown4.Text == "" || domainUpDown5.Text == "" ||
                   textBox2.Text == "" || numericUpDown2.Text == "" ||
                   numericUpDown3.Text == "")
                {
                    MessageBox.Show(
                    "Введите все необходимые значения в пустых ячейках",
                    "Внимание",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
                    if (numericUpDown3.Text == "")
                        numericUpDown3.BackColor = Color.DarkOrange;
                    if (numericUpDown2.Text == "")
                        numericUpDown2.BackColor = Color.DarkOrange;
                    if (textBox1.Text == "")
                        textBox1.BackColor = Color.DarkOrange;
                    if (textBox2.Text == "")
                        textBox2.BackColor = Color.DarkOrange;
                    if (textBox3.Text == "")
                        textBox3.BackColor = Color.DarkOrange;
                    if (numericUpDown1.Text == "")
                        numericUpDown1.BackColor = Color.DarkOrange;
                    if (domainUpDown1.Text == "")
                        domainUpDown1.BackColor = Color.DarkOrange;
                    if (domainUpDown2.Text == "")
                        domainUpDown2.BackColor = Color.DarkOrange;
                    if (domainUpDown3.Text == "")
                        domainUpDown3.BackColor = Color.DarkOrange;
                    if (domainUpDown4.Text == "")
                        domainUpDown4.BackColor = Color.DarkOrange;
                    if (domainUpDown5.Text == "")
                        domainUpDown5.BackColor = Color.DarkOrange;
                    if (textBox2.Text == "")
                        textBox2.BackColor = Color.DarkOrange;
                }

                if (textBox2.Text != "")
                {
                    if ((Convert.ToDouble(textBox2.Text) <= 0 || Convert.ToDouble(textBox2.Text) >= 180))
                    {
                        MessageBox.Show(
                        "Значение угла альфа1 должно быть больше 0 и меньше 180 градусов",
                        "Внимание",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
                        textBox2.BackColor = Color.DeepPink;
                        textBox2.Text = "";
                        textBox7.Text = "";
                    }
                    else
                    {
                        if (Convert.ToDouble(textBox2.Text) < 74.45 && Convert.ToDouble(textBox2.Text) > 0)
                            textBox7.Text = "0,56";
                        if (Convert.ToDouble(textBox2.Text) >= 74.45 && Convert.ToDouble(textBox2.Text) < 84.45)
                            textBox7.Text = "0,62";
                        if (Convert.ToDouble(textBox2.Text) >= 84.45 && Convert.ToDouble(textBox2.Text) < 94.45)
                            textBox7.Text = "0,68";
                        if (Convert.ToDouble(textBox2.Text) >= 94.45 && Convert.ToDouble(textBox2.Text) < 104.45)
                            textBox7.Text = "0,74";
                        if (Convert.ToDouble(textBox2.Text) >= 104.45 && Convert.ToDouble(textBox2.Text) < 114.45)
                            textBox7.Text = "0,79";
                        if (Convert.ToDouble(textBox2.Text) >= 114.45 && Convert.ToDouble(textBox2.Text) < 124.45)
                            textBox7.Text = "0,83";
                        if (Convert.ToDouble(textBox2.Text) >= 124.45 && Convert.ToDouble(textBox2.Text) < 134.45)
                            textBox7.Text = "0,87";
                        if (Convert.ToDouble(textBox2.Text) >= 134.45 && Convert.ToDouble(textBox2.Text) < 144.45)
                            textBox7.Text = "0,90";
                        if (Convert.ToDouble(textBox2.Text) >= 144.45 && Convert.ToDouble(textBox2.Text) < 154.45)
                            textBox7.Text = "0,93";
                        if (Convert.ToDouble(textBox2.Text) >= 154.45 && Convert.ToDouble(textBox2.Text) < 164.45)
                            textBox7.Text = "0,96";
                        if (Convert.ToDouble(textBox2.Text) >= 164.45 && Convert.ToDouble(textBox2.Text) < 174.45)
                            textBox7.Text = "0,98";
                        if (Convert.ToDouble(textBox2.Text) >= 174.45 && Convert.ToDouble(textBox2.Text) < 180)
                            textBox7.Text = "1,0";

                    }
                }
            }
            catch
            {
                MessageBox.Show(
                        "Ошибка! Неверно введенный формат данных",
                        "Внмиание!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
            }
            try
            {
                y1 = 180 - Convert.ToDouble(textBox2.Text);
                C1 = Convert.ToDouble(textBox7.Text);
                f0 = Convert.ToDouble(numericUpDown2.Text);
                C3 = Convert.ToDouble(textBox6.Text);
                S10 = Convert.ToDouble(textBox5.Text);
                q10 = Convert.ToDouble(textBox4.Text);
                Z = Convert.ToDouble(numericUpDown1.Text);
                a1 = Convert.ToDouble(textBox2.Text);
                v = Convert.ToDouble(textBox3.Text);
                X = Convert.ToDouble(numericUpDown3.Text);
                F = Convert.ToDouble(textBox1.Text);

                g0 = 5 * F / (f0 * C1 * C3 * S10 * Z);
                Q0 = (g0 * S10 + (1 - X) * q10 * v * v) * Z / 10;
                R = 2 * g0 * (S10 / 10) * Z * Math.Sin(a1 / 2);
                tanO = (5 * F / (g0 * S10 * Z)) * Math.Tan(y1 / 2);
                O = Math.Atan((5 * F / (g0 * S10 * Z)) * Math.Tan(y1 / 2) * (180 / Math.PI));
                if (tanO >= 0)
                    textBox14.Text = "достоверный";
                else
                    textBox14.Text = "недостоверный";
                textBox9.Text = Convert.ToString(y1);
                textBox10.Text = Convert.ToString(g0);
                textBox11.Text = Convert.ToString(Q0);
                textBox12.Text = Convert.ToString(R);
                textBox13.Text = Convert.ToString(O);
                button2.Visible = true;
            }
            catch { }
        }

        private void domainUpDown2_TextChanged(object sender, EventArgs e)
        {
        }
        private void domainUpDown2_SelectedItemChanged(object sender, EventArgs e)
        {
            if(domainUpDown2.Text != "")
                MessageBox.Show(domainUpDown2.Text);

            domainUpDown2.BackColor = Color.White;
            textBox6.Text = "";
            domainUpDown4.Text = "";
            domainUpDown3.Text = "";
            domainUpDown3.Items.Clear();
            List<string> TipMashin1 = new List<string>
            {
             "Ленточный транспортер",
             "Токарный станок",
             "Сверлильный станок",
             "Шлифовальный станок"
            };

            List<string> TipMashin2 = new List<string>
            {
             "Пластинчатый транспортер",
             "Станок-автомат",
             "Фрезерный станок",
             "Зубофрезерный станок",
             "Револьверный станок",
            };

            List<string> TipMashin3 = new List<string>
            {
             "Реверсивный привод",
             "Строгальный станок",
             "Долбежный станок",
             "Зубодолбежный станок",
             "Винтовой транспортер",
             "Скребковый транспортер",
             "Элеватор",
             "Винтовой пресс с относительно тяжелыми маховиками",
             "Эксцентриковый пресс с относительно тяжелыми маховиками",
            };

            List<string> TipMashin4 = new List<string>
            {
             "Подъемник",
             "Винтовой пресс с относительно легкими маховиками",
             "Эксцентриковый пресс с относительно легкими маховикам",
             "Ножницы",
             "Молот",
             "Бегун",
             "Мельница"
            };

            if (domainUpDown2.Text != "" && domainUpDown2.Text == "Спокойная нагрузка. Пусковая нагрузка до 120% номинальной")
            {
                domainUpDown3.Items.AddRange(TipMashin1);
                domainUpDown3.TextChanged += domainUpDown3_SelectedItemChanged;
            }
            if (domainUpDown2.Text != "" && domainUpDown2.Text == "Умеренные колебания нагрузки. Пусковая нагрузка до 150%")
            {
                domainUpDown3.Items.AddRange(TipMashin2);
                domainUpDown3.TextChanged += domainUpDown3_SelectedItemChanged;
            }
            if (domainUpDown2.Text != "" && domainUpDown2.Text == "Значительные колебания нагрузки. Пусковая нагрузка до 200% номинальной")
            {
                domainUpDown3.Items.AddRange(TipMashin3);
                domainUpDown3.TextChanged += domainUpDown3_SelectedItemChanged;
            }
            if (domainUpDown2.Text != "" && domainUpDown2.Text == "Весьма неравномерная и ударная нагрузка. Пусковая нагрузка до 300% номинальной")
            {
                domainUpDown3.Items.AddRange(TipMashin4);
                domainUpDown3.TextChanged += domainUpDown3_SelectedItemChanged;
            }


        }
        private void domainUpDown5_SelectedItemChanged(object sender, EventArgs e)
        { 
            domainUpDown5.BackColor = Color.White;
            if (domainUpDown5.Text == "Автоматическое натяжение ремня")
            {
                numericUpDown3.Maximum = 1;
                numericUpDown3.Minimum = 1;
                numericUpDown3.Value = 1;
            }

            if (domainUpDown5.Text == "Механическое натяжение ремня")
            {
                numericUpDown3.Maximum = 0.25m;
                numericUpDown3.Minimum = 0.1m;
                numericUpDown3.Value = 0.1m;
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void domainUpDown3_SelectedItemChanged(object sender, EventArgs e)
        {
            domainUpDown3.BackColor = Color.White;
            textBox6.Text = "";
            domainUpDown4.Text = "";
        }

        private void domainUpDown4_SelectedItemChanged(object sender, EventArgs e)
        {
            domainUpDown4.BackColor = Color.White;
            textBox6.Text = "";
            if (domainUpDown2.Text != "" && domainUpDown3.Text != "")
            {
                if ((domainUpDown2.Text == "Спокойная нагрузка. Пусковая нагрузка до 120% номинальной") && (domainUpDown4.Text == "I"))
                { textBox6.Text = "1,0"; }
                if ((domainUpDown2.Text == "Спокойная нагрузка. Пусковая нагрузка до 120% номинальной") && (domainUpDown4.Text == "II"))
                { textBox6.Text = "0,9"; }
                if ((domainUpDown2.Text == "Умеренные колебания нагрузки. Пусковая нагрузка до 150%") && (domainUpDown4.Text == "I"))
                { textBox6.Text = "0,9"; }
                if ((domainUpDown2.Text == "Умеренные колебания нагрузки. Пусковая нагрузка до 150%") && (domainUpDown4.Text == "II"))
                { textBox6.Text = "0,8"; }
                if ((domainUpDown2.Text == "Значительные колебания нагрузки. Пусковая нагрузка до 200% номинальной") && (domainUpDown4.Text == "I"))
                { textBox6.Text = "0,8"; }
                if ((domainUpDown2.Text == "Значительные колебания нагрузки. Пусковая нагрузка до 200% номинальной") && (domainUpDown4.Text == "II"))
                { textBox6.Text = "0,7"; }
                if ((domainUpDown2.Text == "Весьма неравномерная и ударная нагрузка. Пусковая нагрузка до 300% номинальной") && (domainUpDown4.Text == "I"))
                { textBox6.Text = "0,7"; }
                if ((domainUpDown2.Text == "Весьма неравномерная и ударная нагрузка. Пусковая нагрузка до 300% номинальной") && (domainUpDown4.Text == "II"))
                { textBox6.Text = "0,6"; }

            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown2.BackColor = Color.White;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
           numericUpDown3.BackColor = Color.White;
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            { sqlConnection.Close(); }
        }

        private void Form1_FormClossing(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            { sqlConnection.Close(); }
        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [Det] (XH, TM, TD, VNR, alpha1, PR, Z, v, C3, S10, q10, C1, X, F0, O, y1, R, g0, Q0, dost)VALUES(@XH, @TM, @TD, @VNR, @alpha1, @PR, @Z, @v, @C3, @S10, @q10, @C1, @X, @F0, @O, @y1, @R, @g0, @Q0, @dost)", sqlConnection);

            command.Parameters.AddWithValue("XH", domainUpDown2.Text);
            command.Parameters.AddWithValue("TM", domainUpDown3.Text);
            command.Parameters.AddWithValue("TD", domainUpDown4.Text);
            command.Parameters.AddWithValue("VNR", domainUpDown5.Text);
            command.Parameters.AddWithValue("alpha1", textBox2.Text);
            command.Parameters.AddWithValue("PR", domainUpDown1.Text);
            command.Parameters.AddWithValue("Z", numericUpDown1.Text);
            command.Parameters.AddWithValue("v", textBox3.Text);
            command.Parameters.AddWithValue("C3", textBox6.Text);
            command.Parameters.AddWithValue("S10", textBox4.Text);
            command.Parameters.AddWithValue("q10", textBox5.Text);
            command.Parameters.AddWithValue("C1", textBox7.Text);
            command.Parameters.AddWithValue("X", numericUpDown3.Text);
            command.Parameters.AddWithValue("F0", numericUpDown2.Text);
            command.Parameters.AddWithValue("O", textBox13.Text);
            command.Parameters.AddWithValue("y1", textBox9.Text);
            command.Parameters.AddWithValue("R", textBox12.Text);
            command.Parameters.AddWithValue("g0", textBox10.Text);
            command.Parameters.AddWithValue("Q0", textBox11.Text);
            command.Parameters.AddWithValue("dost", textBox14.Text);
            
            await command.ExecuteNonQueryAsync();
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Det]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                listBox1.Items.Add("С о х р а н е н н ы е   д а н н ы е : ");
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add("ID: " + Convert.ToString(sqlReader["DetID"]) + "\tХарактер нагрузки: " + Convert.ToString(sqlReader["XH"]) + "\tТип машины: " +
                    Convert.ToString(sqlReader["TM"]) + "\tТип двигателя: " + Convert.ToString(sqlReader["TD"]) + "\tВид натяжения ремня: " +
                    Convert.ToString(sqlReader["VNR"]) + "\talpha1: " + Convert.ToString(sqlReader["alpha1"]) + "\tПрофиль ремня: " +
                    Convert.ToString(sqlReader["PR"]) + "\tZ: " + Convert.ToString(sqlReader["Z"]) + "\tv: " +
                    Convert.ToString(sqlReader["v"]) + "\tC3:" +
                    Convert.ToString(sqlReader["C3"]) + "\tS10:" + Convert.ToString(sqlReader["S10"]) + "\tq10:" +
                    Convert.ToString(sqlReader["q10"]) + "\tC1:" + Convert.ToString(sqlReader["C1"]) + "\tX:" +
                    Convert.ToString(sqlReader["X"]) + "\tФ0: " + Convert.ToString(sqlReader["F0"]) + "\tУгол О: " +
                    Convert.ToString(sqlReader["O"]) + "\ty1: " + Convert.ToString(sqlReader["y1"]) + "\tR: " +
                    Convert.ToString(sqlReader["R"]) + "\tg0: " + Convert.ToString(sqlReader["g0"]) + "\tQ0: " +
                    Convert.ToString(sqlReader["Q0"]) + "\tДостоверность рассчета: " + Convert.ToString(sqlReader["dost"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result;
                result = MessageBox.Show(
                        "Вы точно хотите удалить параметры данной передачи?",
                        "Внмиание!",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                    if (textBox8.Text != "")
                    {
                        SqlCommand command = new SqlCommand("DELETE FROM [Det] WHERE [DetID] = @DetID", sqlConnection);

                        command.Parameters.AddWithValue("DetID", textBox8.Text);

                        await command.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        label23.Visible = true;
                    }
                }
            }
            catch { }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result;
                result = MessageBox.Show(
                        "Вы точно хотите очистить все введенные данные?",
                        "Внмиание!",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                   textBox2.Text = "";
                   textBox7.Text = "";
                   numericUpDown2.Text = "";
                   textBox6.Text = "";
                   textBox5.Text = "";
                   textBox4.Text = "";
                   numericUpDown1.Text = "";
                   textBox3.Text = "";
                   numericUpDown3.Text = "";
                   textBox1.Text = "";
                   textBox14.Text = "";
                   textBox9.Text = "";
                   textBox10.Text = "";
                   textBox11.Text = "";
                   textBox12.Text = "";
                   textBox13.Text = "";
                   domainUpDown4.Text = "";
                   domainUpDown3.Text = "";
                   domainUpDown2.Text = "";
                   domainUpDown1.Text = "";
                   button2.Visible = false;

                }
            }
            catch { }
        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }
    }
}
