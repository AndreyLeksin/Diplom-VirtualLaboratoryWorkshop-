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
    public partial class Authorization : Form
    {
        readonly DB db = new DB();
        public Authorization()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#1A2D37");
            AcceptButton = btnLogin;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var login = LoginTextBox.Text;
            var password = PasswordTextBox2.Text;

            if(label4.Visible || label5.Visible)
            {
                label4.Visible = false;
                label5.Visible = false;
            }

            string query = $"SELECT Login, Password, Is_Admin, Is_Teacher FROM [Authorization] WHERE Login = @Login " +
                $"AND Password = @Password";
            SqlCommand command = new SqlCommand(query, db.getconnection());

            db.openConnection();

            command.Parameters.AddWithValue("Login", login);
            command.Parameters.AddWithValue("Password", password);

            command.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count == 1)
            {
                var user = new CheckUser(table.Rows[0].ItemArray[0].ToString(), Convert.ToBoolean(table.Rows[0].ItemArray[2]),
                    Convert.ToBoolean(table.Rows[0].ItemArray[3]));

                Form1 form1 = new Form1(user);
                this.Hide();
                form1.ShowDialog();
                this.Dispose();
            }
            else if(LoginTextBox.Text == "" || PasswordTextBox2.Text == "")
            {
                label4.Visible = true;
                label4.Text = "*Поля 'Логин' и 'Пароль' должны быть заполнены!";
            }
            else
            {
                label5.Visible = true;
                label5.Text = "Такого аккаунта не существует!";
            }
        }

        private void Authorization_Load(object sender, EventArgs e)
        {
            LoginTextBox.MaxLength = 30;
            PasswordTextBox2.MaxLength = 30;
            PasswordTextBox2.PasswordChar = '*';
            pictureBox2.Visible = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            LoginTextBox.Text = "";
            PasswordTextBox2.Text = "";
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

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
