using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace VirtualLaboratoryWorkshop
{
    public partial class Registr : Form
    {
        DB db = new DB();
        public Registr()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#1A2D37");
            AcceptButton = btnRegistr;
        }

        private void Registr_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.Authorization". При необходимости она может быть перемещена или удалена.
            this.authorizationTableAdapter.Fill(this.dataSet1.Authorization);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.Teacher". При необходимости она может быть перемещена или удалена.
            this.teacherTableAdapter.Fill(this.dataSet1.Teacher);
            //загрузка данных в comboBox
            AddElementInComboBox();
            PasswordTextBox2.PasswordChar = '*';
            PasswordRetrytextBox.PasswordChar = '*';
            LoginTextBox.MaxLength = 30;
            PasswordTextBox2.MaxLength = 30;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
        }

        //метод для загрузки данных в comboBox ForTeacherComboBox
        private void AddElementInComboBox()
        {
            string query = $"SELECT CONCAT(Surname, + ' ' + Name, + ' ' + Patronymic) FROM Teacher";
            SqlCommand command = new SqlCommand(query, db.getconnection());
            db.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                TeachercomboBox1.Items.Add(reader.GetString(0));
            reader.Close();
            db.closeConnection();

        }
        private void btnRegistr_Click(object sender, EventArgs e)
        {
            var teacher = TeachercomboBox1.Text;
            var login = LoginTextBox.Text;
            var password = PasswordTextBox2.Text;

            if (checkuser())
                return;

            string query = $"INSERT INTO [Authorization] (ID_Teacher, Login, Password, Is_Teacher, Is_Admin) " +
                $"VALUES ((SELECT ID_Teacher FROM Teacher WHERE CONCAT(Surname, + ' ' + Name, + ' ' + Patronymic) = @Teacher), @Login, @Password, 1, 0)";

            SqlCommand command = new SqlCommand(query, db.getconnection());

            db.openConnection();

            command.Parameters.AddWithValue("Teacher", teacher);
            command.Parameters.AddWithValue("Login", login);
            command.Parameters.AddWithValue("Password", password);

            if (TeachercomboBox1.Text == "" || LoginTextBox.Text == "" || PasswordTextBox2.Text == "" || 
                PasswordRetrytextBox.Text == "")
                MessageBox.Show("Некоторые поля не заполнены!", "Ошибка!");
            else if(PasswordRetrytextBox.Text == PasswordTextBox2.Text && command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан!", "Успех!");
                this.Close();
            }
            else
                MessageBox.Show("Пароль не совпадает! Аккаунт не создан.", "Ошибка!");
            db.closeConnection();
        }
        //метод для проверки существующего аккаунта
        public Boolean checkuser()
        {
            var login = LoginTextBox.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string query = $"SELECT Login from [Authorization] WHERE Login = @Login";

            SqlCommand command = new SqlCommand(query, db.getconnection());

            db.openConnection();

            command.Parameters.AddWithValue("Login", login);

            command.ExecuteNonQuery();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Аккаунт с таким логином уже существует!");
                return true;
            }
            else
                return false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            TeachercomboBox1.Text = "";
            LoginTextBox.Text = "";
            PasswordTextBox2.Text = "";
            PasswordRetrytextBox.Text = "";
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PasswordTextBox2.UseSystemPasswordChar = true;
            pictureBox1.Visible = false;
            pictureBox2.Visible = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            PasswordTextBox2.UseSystemPasswordChar = false;
            pictureBox1.Visible = true;
            pictureBox2.Visible = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            PasswordRetrytextBox.UseSystemPasswordChar = true;
            pictureBox4.Visible = false;
            pictureBox3.Visible = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            PasswordRetrytextBox.UseSystemPasswordChar = false;
            pictureBox4.Visible = true;
            pictureBox3.Visible = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //this.Dispose();
            RegistrStudent student = new RegistrStudent();
            student.ShowDialog();
        }
    }
}
