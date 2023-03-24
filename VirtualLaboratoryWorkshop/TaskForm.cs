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
    public partial class TaskForm : Form
    {
        DB db = new DB();

        //метод, который разрешает доступ к функциям для преподавателя
        private void IsTeacher()
        {
            UpdateBtn.Visible = _user.IsTeacher;
            CreateTaskBtn.Visible = _user.IsTeacher;
            OpenTaskBtn.Visible = _user.IsTeacher;
            CloseTaskBtn.Visible = _user.IsTeacher;
            DeleteTaskBtn.Visible = _user.IsTeacher;
        }

        private readonly CheckUser _user;
        public TaskForm(CheckUser user)
        {
            _user = user;
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#1A2D37");
        }
            

        private void CreateColumn()
        {
            TaskDataGridView.Columns.Add("ID_Task", "№ задачи");
            TaskDataGridView.Columns.Add("Surname", "Фамилия");
            TaskDataGridView.Columns.Add("Name", "Имя");
            TaskDataGridView.Columns.Add("Patronymic", "Отчество");
            TaskDataGridView.Columns.Add("Title_Task", "Название задачи");
            TaskDataGridView.Columns.Add("Content", "Содержание");
            TaskDataGridView.Columns.Add("Accessibility", "Доступность");
        }

        public void RefreshDataGrid(DataGridView dgv) //метод для заполнения DataGridView
        {
            Form1 form1 = new Form1(_user);
            dgv.Rows.Clear();

            if (_user.IsAdmin == true || _user.IsTeacher == true)
            {
                string querystring = $"SELECT ID_Task, S.Surname, S.Name, S.Patronymic, Title_Task, Content, Accessibility FROM Individual_Task, Student S " +
                    $"WHERE S.ID_Student = Student_ID";

                SqlCommand command = new SqlCommand(querystring, db.getconnection());
                db.openConnection();

                SqlDataReader reader = command.ExecuteReader(); //считываем и извлекаем данные

                while (reader.Read())
                    dgv.Rows.Add(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetBoolean(6));

                //изменение цвета сток
                foreach (DataGridViewRow row in TaskDataGridView.Rows)
                    if (Convert.ToBoolean(row.Cells["Accessibility"].Value) == false)
                    {
                        row.DefaultCellStyle.BackColor = Color.RoyalBlue;
                        row.DefaultCellStyle.SelectionBackColor = Color.RoyalBlue;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.Lime;
                        row.DefaultCellStyle.SelectionBackColor = Color.Lime;
                    }

                reader.Close();
                db.closeConnection();
            }
            else
            {
                string login = _user.Login;
                string querystring2 = $"SELECT ID_Task, S.Surname, S.Name, S.Patronymic, Title_Task, Content, Accessibility FROM Individual_Task, Student S, [Authorization] a " +
                    $"WHERE S.ID_Student = Student_ID AND a.Login = @Login AND Student_ID = a.ID_Student";

                SqlCommand command = new SqlCommand(querystring2, db.getconnection());
                command.Parameters.AddWithValue("Login", login);
                db.openConnection();

                SqlDataReader reader = command.ExecuteReader(); //считываем и извлекаем данные
                
                while (reader.Read())
                    dgv.Rows.Add(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetBoolean(6));
                reader.Close();

                //открытие и закрытие задач для студента
                for (int i = 0; i < TaskDataGridView.Rows.Count - 1; i++)
                {
                    if (Convert.ToBoolean(TaskDataGridView.Rows[i].Cells["Accessibility"].Value) == true)
                        TaskDataGridView.Rows[i].Visible = true;
                    else
                        TaskDataGridView.Rows[i].Visible = false;
                }
                
                reader.Close();
                db.closeConnection();

            }
        }

        private void TaskForm_Load(object sender, EventArgs e)
        {
            CreateColumn();
            RefreshDataGrid(TaskDataGridView);
            IsTeacher();
        }

        private void CreateTaskBtn_Click(object sender, EventArgs e)
        {
            CreateTaskForm createTaskForm = new CreateTaskForm();
            createTaskForm.ShowDialog();
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(TaskDataGridView);
        }

        private void DeleteTaskBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить эту запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            var selectedRowIndex = TaskDataGridView.CurrentCell.RowIndex;
            var id = TaskDataGridView.Rows[selectedRowIndex].Cells[0].Value;

            db.openConnection();

            var deleteQuery = $"DELETE FROM Individual_Task WHERE ID_Task = @id";

            SqlCommand command = new SqlCommand(deleteQuery, db.getconnection());

            command.Parameters.AddWithValue("id", id);

            command.ExecuteNonQuery();

            db.closeConnection();

            RefreshDataGrid(TaskDataGridView);
        }

        private void CloseTaskBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите закрыть эту задачу?", "Закрытие задачи", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            var OpenAccess = false;
            db.openConnection();

            var selectedRowIndexForAccess = TaskDataGridView.CurrentCell.RowIndex;
            var id = TaskDataGridView.Rows[selectedRowIndexForAccess].Cells[0].Value;
            var updateQuery = $"UPDATE Individual_Task SET Accessibility = @OpenAccess WHERE ID_Task = @id";

            SqlCommand command = new SqlCommand(updateQuery, db.getconnection());

            command.Parameters.AddWithValue("OpenAccess", OpenAccess);
            command.Parameters.AddWithValue("id", id);

            command.ExecuteNonQuery();
            db.closeConnection();
            RefreshDataGrid(TaskDataGridView);
        }

        private void OpenTaskBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите открыть эту задачу?", "Открытие задачи", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            var OpenAccess = true;
            db.openConnection();

            var selectedRowIndexForAccess2 = TaskDataGridView.CurrentCell.RowIndex;
            var id = TaskDataGridView.Rows[selectedRowIndexForAccess2].Cells[0].Value;
            var updateQuery = $"UPDATE Individual_Task SET Accessibility = @OpenAccess WHERE ID_Task = @id";

            SqlCommand command = new SqlCommand(updateQuery, db.getconnection());

            command.Parameters.AddWithValue("OpenAccess", OpenAccess);
            command.Parameters.AddWithValue("id", id);

            command.ExecuteNonQuery();
            db.closeConnection();
            RefreshDataGrid(TaskDataGridView);
        }
    }
}
