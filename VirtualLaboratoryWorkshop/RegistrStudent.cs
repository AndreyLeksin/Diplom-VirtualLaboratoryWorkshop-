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
    public partial class RegistrStudent : Form
    {
        DB db = new DB();
        public RegistrStudent()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#1A2D37");
            AcceptButton = btnRegistr;
        }

        private void btnRegistr_Click(object sender, EventArgs e)
        {
            var student = StudentcomboBox1.Text;
            var group = GroupcomboBox.Text;
            var login = LoginTextBox.Text;
            var password = PasswordTextBox2.Text;

            //проверка сущ. аккаунт
            if (checkuser())
                return;

            string query = $"INSERT INTO [Authorization] (ID_Student, ID_Group, Login, Password, Is_Teacher, Is_Admin) " +
                $"VALUES ((SELECT ID_Student FROM Student WHERE CONCAT(Surname, + ' ' + Name, + ' ' + Patronymic) = @fio), " +
                $"(SELECT ID_Group FROM [Group] WHERE Title_Group = @TitleGroup), @Login, @Password, 0, 0)";

            SqlCommand command = new SqlCommand(query, db.getconnection());

            db.openConnection();

            command.Parameters.AddWithValue("fio", student);
            command.Parameters.AddWithValue("TitleGroup", group);
            command.Parameters.AddWithValue("Login", login);
            command.Parameters.AddWithValue("Password", password);

            if(StudentcomboBox1.Text == "" || GroupcomboBox.Text == "" || LoginTextBox.Text == "" || 
                PasswordTextBox2.Text == "" || PasswordRetrytextBox.Text == "")
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
            var student = StudentcomboBox1.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string query = $"SELECT Login, ID_Student from [Authorization] WHERE Login = @Login OR " +
                $"ID_Student = (SELECT ID_Student FROM Student WHERE Surname = @Student)";

            SqlCommand command = new SqlCommand(query, db.getconnection());

            db.openConnection();

            command.Parameters.AddWithValue("Login", login);
            command.Parameters.AddWithValue("Student", student);

            command.ExecuteNonQuery();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Аккаунт с таким логином или студентом уже существует!");
                return true;
            }
            else
                return false;
        }

        private void RegistrStudent_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.Group". При необходимости она может быть перемещена или удалена.
            this.groupTableAdapter.Fill(this.dataSet1.Group);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.Authorization". При необходимости она может быть перемещена или удалена.
            this.authorizationTableAdapter.Fill(this.dataSet1.Authorization);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.Student". При необходимости она может быть перемещена или удалена.
            this.studentTableAdapter.Fill(this.dataSet1.Student);
            //загрузка данных в comboBox
            AddElementInComboBox();
            PasswordTextBox2.PasswordChar = '*';
            PasswordRetrytextBox.PasswordChar = '*';
            LoginTextBox.MaxLength = 30;
            PasswordTextBox2.MaxLength = 30;
            pictureBox4.Visible = false;
            pictureBox2.Visible = false;
        }

        //метод для загрузки данных в comboBox ForTeacherComboBox
        private void AddElementInComboBox()
        {
            string query = $"SELECT CONCAT(Surname, + ' ' + Name, + ' ' + Patronymic) FROM Student";
            SqlCommand command = new SqlCommand(query, db.getconnection());
            db.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                StudentcomboBox1.Items.Add(reader.GetString(0));
            reader.Close();
            db.closeConnection();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            StudentcomboBox1.Text = "";
            GroupcomboBox.Text = "";
            LoginTextBox.Text = "";
            PasswordTextBox2.Text = "";
            PasswordRetrytextBox.Text = "";
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            PasswordRetrytextBox.UseSystemPasswordChar = true;
            pictureBox3.Visible = false;
            pictureBox4.Visible = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            PasswordRetrytextBox.UseSystemPasswordChar = false;
            pictureBox3.Visible = true;
            pictureBox4.Visible = false;
        }
    }
}
