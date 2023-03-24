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
    public partial class CreateTaskForm : Form
    {
        DB db = new DB();

        public CreateTaskForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#1A2D37");
        }

        private void CreateTaskForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.Student". При необходимости она может быть перемещена или удалена.
            this.studentTableAdapter.Fill(this.dataSet1.Student);

            //загрузка данных в comboBox
            AddElementInComboBox();
        }

        //метод для загрузки данных в comboBox ForStudentComboBox
        private void AddElementInComboBox()
        {
            string query = $"SELECT CONCAT(Surname, + ' ' + Name, + ' ' + Patronymic) FROM Student";
            SqlCommand command = new SqlCommand(query, db.getconnection());
            db.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                ForStudentComboBox.Items.Add(reader.GetString(0));
            reader.Close();
            db.closeConnection();
        }
        private void CreateBtn_Click(object sender, EventArgs e)
        {
            if(ForStudentComboBox.Text != "" && TitleTaskTextBox.Text != "" && ContentTextBox.Text != "")
            {
                var fio = ForStudentComboBox.Text;
                var titleTask = TitleTaskTextBox.Text;
                var content = ContentTextBox.Text;

                string querystring = $"INSERT INTO Individual_Task (Student_ID, Title_Task, Content, Accessibility) VALUES ((SELECT ID_Student FROM Student " +
                    $"WHERE CONCAT(Surname, + ' ' + Name, + ' ' + Patronymic) = @fio), @titleTask, @content, 0)";

                SqlCommand command = new SqlCommand(querystring, db.getconnection());

                db.openConnection();
                command.Parameters.AddWithValue("fio", fio);
                command.Parameters.AddWithValue("titleTask", titleTask);
                command.Parameters.AddWithValue("content", content);

                command.ExecuteNonQuery();

                MessageBox.Show("Задача успешно добавлена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                db.closeConnection();
                this.Close();
            }
            else
                MessageBox.Show("Поля должны быть заполнены!", "Ошибка");
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите отменить создание задачи?", "Выйти", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }
    }
}
