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
    public partial class Profile : Form
    {
        DB db = new DB();

        private readonly CheckUser _user;
        public Profile(CheckUser user)
        {
            _user = user;
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#1A2D37");
        }

        //метод, который выводит фио пользователя
        private void FIOprofile()
        {
            if (_user.Status() == "Студент")
            {
                string query = $"SELECT S.Surname, S.Name, S.Patronymic FROM Student S, [Authorization] A " +
                    $"WHERE A.Login = @Login AND A.ID_Student = S.ID_Student";

                SqlCommand command = new SqlCommand(query, db.getconnection());
                command.Parameters.AddWithValue("Login", _user.Login);
                db.openConnection();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    ProfileUser.Text = reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetString(2);
                reader.Close();
                db.closeConnection();
            }
            else if(_user.Status() == "Преподаватель")
            {
                string query = $"SELECT T.Surname, T.Name, T.Patronymic FROM Teacher T, [Authorization] A " +
                    $"WHERE A.Login = @Login AND A.ID_Teacher = T.ID_Teacher";

                SqlCommand command = new SqlCommand(query, db.getconnection());
                command.Parameters.AddWithValue("Login", _user.Login);
                db.openConnection();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    ProfileUser.Text = reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetString(2);
                reader.Close();
                db.closeConnection();
            }
            else
                ProfileUser.Text = "Администратор";
        }

        //создание столбцов в datagridview
        private void CreateColumn()
        {
            ParamsDataGridView.Columns.Add("ID_Parametr", "№");
            ParamsDataGridView.Columns.Add("Surname", "Фамилия");
            ParamsDataGridView.Columns.Add("Name", "Имя");
            ParamsDataGridView.Columns.Add("Patronymic", "Отчество");
            ParamsDataGridView.Columns.Add("Title_Parametr", "Название параметра");
            ParamsDataGridView.Columns.Add("Hm", "Hm");
            ParamsDataGridView.Columns.Add("w", "Круговая частота(w)");
            ParamsDataGridView.Columns.Add("t", "Время одного колебания(t)");
            ParamsDataGridView.Columns.Add("x", "Длина волны(x)");
            ParamsDataGridView.Columns.Add("u", "Фазовая скорость распространения волн в среде (u)");
            ParamsDataGridView.Columns.Add("h", "Шаг(h)");
            ParamsDataGridView.Columns.Add("A", "A(от)");
            ParamsDataGridView.Columns.Add("B", "B(до)");
        }

        //метод для заполнения DataGridView
        public void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();

            if (_user.IsAdmin == true || _user.IsTeacher == true)
            {
                string querystring = $"SELECT ID_Parametr, S.Surname, S.Name, S.Patronymic, Title_Parametr, Hm, w, t, x, u, h, A, B FROM Parametrs P, Student S " +
                    $"WHERE P.StudentParam_ID = S.ID_Student";

                SqlCommand command = new SqlCommand(querystring, db.getconnection());
                db.openConnection();

                SqlDataReader reader = command.ExecuteReader(); //считываем и извлекаем данные

                while (reader.Read())
                    dgv.Rows.Add(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), 
                        reader.GetDouble(5), reader.GetDouble(6), reader.GetDouble(7), reader.GetDouble(8), reader.GetDouble(9), reader.GetDouble(10), 
                        reader.GetDouble(11), reader.GetDouble(12));

                reader.Close();
                db.closeConnection();
            }
            else
            {
                string login = _user.Login;

                string querystring = $"SELECT ID_Parametr, S.Surname, S.Name, S.Patronymic, Title_Parametr, Hm, w, t, x, u, h, A, B FROM Parametrs P, Student S, [Authorization] A " +
                    $"WHERE P.StudentParam_ID = S.ID_Student AND A.Login = @login AND P.StudentParam_ID = A.ID_Student";

                SqlCommand command = new SqlCommand(querystring, db.getconnection());
                command.Parameters.AddWithValue("login", login);
                db.openConnection();

                SqlDataReader reader = command.ExecuteReader(); //считываем и извлекаем данные

                while (reader.Read())
                    dgv.Rows.Add(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4),
                        reader.GetDouble(5), reader.GetDouble(6), reader.GetDouble(7), reader.GetDouble(8), reader.GetDouble(9), reader.GetDouble(10),
                        reader.GetDouble(11), reader.GetDouble(12));

                reader.Close();
                db.closeConnection();
            }
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            FIOprofile();
            CreateColumn();
            RefreshDataGrid(ParamsDataGridView);
        }

        private void ParamsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ParamsDataGridView.Rows.Count > 1 || ParamsDataGridView.Rows != null && e.RowIndex >= 0)
            {
                HmTextBox.Text = ParamsDataGridView.CurrentRow.Cells["Hm"].Value.ToString();
                OmegaTextBox.Text = ParamsDataGridView.CurrentRow.Cells["w"].Value.ToString();
                tTextBox.Text = ParamsDataGridView.CurrentRow.Cells["t"].Value.ToString();
                xTextBox.Text = ParamsDataGridView.CurrentRow.Cells["x"].Value.ToString();
                uTextBox.Text = ParamsDataGridView.CurrentRow.Cells["u"].Value.ToString();
                hTextBox.Text = ParamsDataGridView.CurrentRow.Cells["h"].Value.ToString();
                ATextBox.Text = ParamsDataGridView.CurrentRow.Cells["A"].Value.ToString();
                BTextBox.Text = ParamsDataGridView.CurrentRow.Cells["B"].Value.ToString();
            }
        }

        private void LoadParamsBtn_Click(object sender, EventArgs e)
        {
            if (HmTextBox.Text != "" || OmegaTextBox.Text != "" || tTextBox.Text != "" || xTextBox.Text != "" || uTextBox.Text != "" ||
                hTextBox.Text != "" || ATextBox.Text != "" || BTextBox.Text != "")
            {
                Form1 form1 = (Form1)this.Owner; //либо так: Form1 form1 = (Form1)Application.OpenForms["Form1"];
                form1.TextBoxHm.Text = HmTextBox.Text;
                form1.TextBoxw.Text = OmegaTextBox.Text;
                form1.TextBox_t.Text = tTextBox.Text;
                form1.TextBox_x.Text = xTextBox.Text;
                form1.TextBox_u.Text = uTextBox.Text;
                form1.textBox_h.Text = hTextBox.Text;
                form1.textBox_A.Text = ATextBox.Text;
                form1.textBox_B.Text = BTextBox.Text;
                this.Close();
            }
            else
                MessageBox.Show("Нельзя загрузить набор параметров! Поля не были заполнены.", "Ошибка загрузки параметров!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
