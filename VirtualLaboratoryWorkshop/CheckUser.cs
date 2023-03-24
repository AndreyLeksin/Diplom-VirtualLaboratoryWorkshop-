using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLaboratoryWorkshop
{
    public class CheckUser
    {
        public string Login { get; set; }
        public bool IsAdmin { get; }
        public bool IsTeacher { get; }

        //public bool status;
        public string Status()
        {
            if (IsAdmin == true)
                return "Админ";
            else if (IsTeacher == true)
                return "Преподаватель";
            else
                return "Студент";
        }

        public CheckUser(string login, bool isAdmin, bool isTeacher)
        {
            Login = login.Trim();
            IsAdmin = isAdmin;
            IsTeacher = isTeacher;
        }

    }
}
