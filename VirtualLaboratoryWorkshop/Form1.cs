using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualLaboratoryWorkshop
{
    public partial class Form1 : Form
    {
        //метод, который разрешает доступ к функциям для админа
        private void isAdmin()
        {
            dbBtn.Visible = _user.IsAdmin;
            registerBtn.Visible = _user.IsAdmin;
        }

        private readonly CheckUser _user;
        public Form1(CheckUser user)
        {
            _user = user;
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#1A2D37");

            
        }

        //метод котрый выполняется при выборе эл-та в меню
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Задачи")
            {
                TaskForm taskForm = new TaskForm(_user);
                taskForm.ShowDialog();
            }
            else if (e.ClickedItem.Text == "О нас")
                MessageBox.Show("Дипломная работа на тему \"Лабораторный практикум для изучения распространения электромагнитных " +
                    "полей в двумерном пространстве.\" \n Выполнил: Родионов Егор Александрович", "О нас!");
            else
                if (MessageBox.Show("Выйти?", "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.Close();
        }

        private void menuBtn_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Items.Clear();
            contextMenuStrip1.Items.Add("Задачи");
            contextMenuStrip1.Items.Add("О нас");
            contextMenuStrip1.Items.Add("Выйти");

            contextMenuStrip1.Show(menuBtn, new Point(0, menuBtn.Height));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            contextMenuStrip1 = new ContextMenuStrip();
            contextMenuStrip1.Font = new Font("Microsoft Sans Serif", 24);
            //при щелчке объекта выполняется действие в скобках
            contextMenuStrip1.ItemClicked += new ToolStripItemClickedEventHandler(contextMenuStrip1_ItemClicked);

            RoleLabel2.Text = $"{_user.Status()}";
            Loginlabel2.Text = $"{_user.Login}";
            isAdmin();
        }
        
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
                Authorization authorization = new Authorization();
                this.Dispose();
                authorization.ShowDialog();
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            Registr registr = new Registr();
            registr.ShowDialog();
            
        }

        //переменные для вычисления
        private double a, b, h, Hm, W, t, u, x, y;

        private void dbBtn_Click(object sender, EventArgs e)
        {
            DataBaseForm dbform = new DataBaseForm();
            dbform.ShowDialog();
        }

        private void saveParametrs_Click(object sender, EventArgs e)
        {
            if (TextBoxHm.Text != "" && TextBoxw.Text != "" && TextBox_t.Text != "" && TextBox_x.Text != "" && TextBox_u.Text != "" &&
                textBox_h.Text != "" && textBox_A.Text != "" && textBox_B.Text != "")
            {
                SaveParamsForm saveParamsForm = new SaveParamsForm(_user);
                saveParamsForm.Owner = this;
                saveParamsForm.ShowDialog();
            }
            else
                MessageBox.Show("Поля параметров должны быть заполнены!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void Loginlabel2_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile(_user);
            profile.Owner = this;
            profile.ShowDialog();
        }


        private void reset_Click(object sender, EventArgs e)
        {
            this.chart1.Series[0].Points.Clear();
            this.chart2.Series[0].Points.Clear();
            TextBoxHm.Text = ""; TextBoxw.Text = ""; TextBox_t.Text = ""; TextBox_x.Text = ""; TextBox_u.Text = ""; textBox_h.Text = "";
            textBox_A.Text = ""; textBox_B.Text = "";
        }

        private void calculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_A.Text == "" || textBox_B.Text == "" || textBox_h.Text == "" || TextBoxHm.Text == "" || TextBoxw.Text == "" ||
                    TextBox_t.Text == "" || TextBox_x.Text == "" || TextBox_u.Text == "")
                {
                    MessageBox.Show("Поля параметров должны быть заполнены!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                    //DefaultParams();
                }
                else if (textBox_A.Text == "0" || textBox_B.Text == "0" || textBox_h.Text == "0" || TextBoxHm.Text == "0" || TextBoxw.Text == "0" ||
                    TextBox_t.Text == "0" || TextBox_x.Text == "0" || TextBox_u.Text == "0")
                {
                    MessageBox.Show("Поля не должны иметь нулевые значения");
                    return;
                }
                else
                {
                    a = Convert.ToDouble(textBox_A.Text);
                    b = Convert.ToDouble(textBox_B.Text);
                    h = Convert.ToDouble(textBox_h.Text);
                    Hm = Convert.ToDouble(TextBoxHm.Text);
                    W = Convert.ToDouble(TextBoxw.Text);
                    t = Convert.ToDouble(TextBox_t.Text);
                    x = Convert.ToDouble(TextBox_x.Text);
                    u = Convert.ToDouble(TextBox_u.Text);
                }

                //this.chart1.Series[0].Points.Clear();
                //this.chart2.Series[0].Points.Clear();
                //while (x <= b)
                //{
                //    y = Math.Sin(x);
                //    this.chart1.Series[0].Points.AddXY(x, y);
                //    this.chart2.Series[0].Points.AddXY(x, y);
                //    x += h;
                //}
                this.chart1.Series[0].Points.Clear();
                this.chart2.Series[0].Points.Clear();
                while (x <= b)
                {
                    y = Hm * Math.Cos(W * (t - (x / u)));
                    this.chart1.Series[0].Points.AddXY(x, y);
                    this.chart2.Series[0].Points.AddXY(x, y);
                    x += h;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DefaultParams()
        {
            Hm = 8; W = 12; t = 1; x = 15; u = 3; a = 1; b = 30; h = 1;
        }
    }
}
