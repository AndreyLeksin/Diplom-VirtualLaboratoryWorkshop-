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
    public partial class izmenenieStudentaForm : Form
    {
        DB db = new DB();
        public izmenenieStudentaForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void AddElementInComboBox()
        {
            string query = $"SELECT Title_Group FROM [Group]";
            SqlCommand command = new SqlCommand(query, db.getconnection());
            db.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                GroupcomboBox.Items.Add(reader.GetString(0));
            reader.Close();
            db.closeConnection();

        }

        private void InsertTextInTextBox()
        {
            DataBaseForm dbform = this.Owner as DataBaseForm;
            var selectedRowIndex = dbform.StudentsdataGridView.CurrentCell.RowIndex;
            var textgroup = dbform.StudentsdataGridView.Rows[selectedRowIndex].Cells[1].Value;
            var textsurname = dbform.StudentsdataGridView.Rows[selectedRowIndex].Cells[2].Value;
            var textname = dbform.StudentsdataGridView.Rows[selectedRowIndex].Cells[3].Value;
            var textpatronymic = dbform.StudentsdataGridView.Rows[selectedRowIndex].Cells[4].Value;
            var textemail = dbform.StudentsdataGridView.Rows[selectedRowIndex].Cells[5].Value;

            GroupcomboBox.Text = textgroup.ToString();
            SurnametextBox.Text = textsurname.ToString();
            NametextBox2.Text = textname.ToString();
            PatronymictextBox3.Text = textpatronymic.ToString();
            EmailtextBox.Text = textemail.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (GroupcomboBox.SelectedItem != null && SurnametextBox.Text != "" && NametextBox2.Text != "" && PatronymictextBox3.Text != "")
            {
                if (MessageBox.Show("Вы уверены, что хотите изменить данные этого студента?", "Изменение", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.Cancel) return;

                db.openConnection();

                DataBaseForm dbform = this.Owner as DataBaseForm;
                var selectedRowIndex = dbform.StudentsdataGridView.CurrentCell.RowIndex;
                var id = dbform.StudentsdataGridView.Rows[selectedRowIndex].Cells[0].Value;
                var group = GroupcomboBox.Text;
                var surname = SurnametextBox.Text;
                var name = NametextBox2.Text;
                var patronymic = PatronymictextBox3.Text;
                var email = EmailtextBox.Text;

                dbform.StudentsdataGridView.Rows[selectedRowIndex].Cells[1].Value = group;
                dbform.StudentsdataGridView.Rows[selectedRowIndex].Cells[2].Value = surname;
                dbform.StudentsdataGridView.Rows[selectedRowIndex].Cells[3].Value = name;
                dbform.StudentsdataGridView.Rows[selectedRowIndex].Cells[4].Value = patronymic;
                dbform.StudentsdataGridView.Rows[selectedRowIndex].Cells[5].Value = email;

                string query = $"UPDATE Student SET ID_Group = (SELECT ID_Group FROM [Group] WHERE Title_Group = @group), " +
                    $"Surname = @surname, Name = @name, Patronymic = @patronymic, Email = @email WHERE ID_Student = @id";

                SqlCommand command = new SqlCommand(query, db.getconnection());
                command.Parameters.AddWithValue("group", group);
                command.Parameters.AddWithValue("surname", surname);
                command.Parameters.AddWithValue("name", name);
                command.Parameters.AddWithValue("patronymic", patronymic);
                command.Parameters.AddWithValue("email", email);
                command.Parameters.AddWithValue("id", id);

                command.ExecuteNonQuery();
                MessageBox.Show("Запись успешно изменена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                db.closeConnection();
                dbform.RefreshDataGrid(dbform.StudentsdataGridView);
                this.Close();
            }
            else
                MessageBox.Show("Поля должны быть заполнены!", "Ошибка");
        }

        private void izmenenieStudentaForm_Load(object sender, EventArgs e)
        {
            AddElementInComboBox();
            InsertTextInTextBox();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }
    }
}
