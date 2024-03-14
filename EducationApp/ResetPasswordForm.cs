using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EducationApp
{
    public partial class ResetPasswordForm : Form
    {
        public ResetPasswordForm()
        {
            InitializeComponent();
        }
        private string connectionString = "Host=localhost;Username=postgres;Password=qwerty123;Database=postgres";
        private bool IsLoginExists(string login, NpgsqlConnection connection)
        {
            string query = "SELECT * FROM users WHERE login = @login";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@login", login);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }

        private bool UpdatePassword(string login, string newPassword, NpgsqlConnection connection)
        {
            string query = "UPDATE users SET password = @newPassword WHERE login = @login";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@newPassword", newPassword);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при обновлении пароля: " + ex.Message);
                    return false;
                }
            }
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            string newPassword = newPass.Text;
            string loginToUpdate = loginStr.Text;
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("Введите новый пароль.");
                return;
            }
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            try
            {
                conn.Open();
                if (!IsLoginExists(loginToUpdate, conn))
                {
                    MessageBox.Show($"Пользователя с логином {loginToUpdate} не существует.");
                    return;
                }
                if (UpdatePassword(loginToUpdate, newPassword, conn))
                {
                    MessageBox.Show("Пароль успешно обновлен.");
                    this.Close(); 
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}

