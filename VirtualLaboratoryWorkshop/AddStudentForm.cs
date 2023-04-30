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
    public partial class AddStudentForm : Form
    {
        DB db = new DB();
        public AddStudentForm()
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (GroupcomboBox.SelectedItem != null && SurnametextBox.Text != "" && NametextBox2.Text != "" && PatronymictextBox3.Text != "")
            {
                db.openConnection();
                var group = GroupcomboBox.Text;
                var surname = SurnametextBox.Text;
                var name = NametextBox2.Text;
                var patronymic = PatronymictextBox3.Text;
                var email = EmailtextBox.Text;

                string query = $"INSERT INTO [Student] (ID_Group, Surname, Name, Patronymic, Email) " +
                    $"VALUES ((SELECT ID_Group FROM [Group] WHERE Title_Group = @group), @surname, @name, @patronymic, @email)";

                SqlCommand command = new SqlCommand(query, db.getconnection());
                command.Parameters.AddWithValue("group", group);
                command.Parameters.AddWithValue("surname", surname);
                command.Parameters.AddWithValue("name", name);
                command.Parameters.AddWithValue("patronymic", patronymic);
                command.Parameters.AddWithValue("email", email);

                db.openConnection();
                command.ExecuteNonQuery();
                MessageBox.Show("Запись успешно создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                db.closeConnection();
                DataBaseForm dbfofm = this.Owner as DataBaseForm;
                dbfofm.RefreshDataGrid(dbfofm.StudentsdataGridView);
                this.Close();
            }
            else
                MessageBox.Show("Поля должны быть заполнены!", "Ошибка");
        }

        private void AddStudentForm_Load(object sender, EventArgs e)
        {
            AddElementInComboBox();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }
    }
}
