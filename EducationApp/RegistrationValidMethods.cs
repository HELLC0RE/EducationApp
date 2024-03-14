using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EducationApp
{
    public class RegistrationValidMethods
    {
        public int GetRoleIdByName(string roleName, NpgsqlConnection connection)
        {
            string query = "SELECT id FROM roles WHERE role_name = @roleName";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@roleName", roleName);
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int roleId))
                {
                    return roleId;
                }
                return -1;
            }
        }
        public bool IsPasswordValid(string password)
        {
            if (password.Length < 5)
            {
                return false;
            }

            int digitCount = password.Count(char.IsDigit);

            if (digitCount != 3)
            {
                return false;
            }

            if (!password.Any(c => "@#%)(.<".Contains(c)))
            {
                return false;
            }

            return true;
        }
        public bool IsLoginUnique(string login, NpgsqlConnection connection)
        {
            string query = "SELECT * FROM users WHERE login = @login";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@login", login);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count == 0;
            }
        }
    }
}
