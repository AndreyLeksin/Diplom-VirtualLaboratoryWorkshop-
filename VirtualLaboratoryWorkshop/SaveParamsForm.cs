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
    public partial class SaveParamsForm : Form
    {
        DB db = new DB();
        private readonly CheckUser _user;
        public SaveParamsForm(CheckUser user)
        {
            _user = user;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            AcceptButton = SaveParamBtn;
        }

        private void SaveParamBtn_Click(object sender, EventArgs e)
        {
            if (TitleParamtextBox.Text != "")
            {
                Form1 form1 = (Form1)this.Owner;
                var TitleParametr = TitleParamtextBox.Text;
                var hm = form1.TextBoxHm.Text;
                var w = form1.TextBoxw.Text;
                var t = form1.TextBox_t.Text;
                var x = form1.TextBox_x.Text;
                var u = form1.TextBox_u.Text;
                var h = form1.textBox_h.Text;
                var A = form1.textBox_A.Text;
                var B = form1.textBox_B.Text;
                var login = _user.Login;

                string query = $"INSERT INTO Parametrs (StudentParam_ID, Title_Parametr, Hm, w, t, x, u, h, A, B) " +
                    $"VALUES ((SELECT ID_Student FROM [Authorization] WHERE Login = @Login), @TitleParametr, @hm, @w, @t, @x, @u, @h, @A, @B)";

                db.openConnection();

                SqlCommand command = new SqlCommand(query, db.getconnection());
                command.Parameters.AddWithValue("Login", login);
                command.Parameters.AddWithValue("TitleParametr", TitleParametr);
                command.Parameters.AddWithValue("hm", hm);
                command.Parameters.AddWithValue("w", w);
                command.Parameters.AddWithValue("t", t);
                command.Parameters.AddWithValue("x", x);
                command.Parameters.AddWithValue("u", u);
                command.Parameters.AddWithValue("h", h);
                command.Parameters.AddWithValue("A", A);
                command.Parameters.AddWithValue("B", B);

                command.ExecuteNonQuery();
                MessageBox.Show("Набор параметров успешно сохранен!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                db.closeConnection();
                this.Close();
            }
            else
                MessageBox.Show("Поле должно быть заполнено!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void Cancelbtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите отменить сохранение?", "Отмена!", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }
    }
}
