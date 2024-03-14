using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace EducationApp
{
    public partial class Registration : Form
    {
        private string connectionString = "Host=localhost;Username=postgres;Password=qwerty123;Database=postgres";
        private RegistrationValidMethods registrationValidMethods;  
        public Registration()
        {
            InitializeComponent();
            registrationValidMethods = new RegistrationValidMethods();
            LoadDataInComboBox();
        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }
        private void LoadDataInComboBox()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                var command = new NpgsqlCommand("Select role_name from roles", conn);

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBoxPosition.Items.Add(reader.GetString(0));
                    }
                }
            }
        }
        private void buttonReg_Click(object sender, EventArgs e)
        {
            string login = loginStr.Text;
            string password = passStr.Text;
            string selectedRole = comboBoxPosition.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль.");
                return;
            }
            if (string.IsNullOrWhiteSpace(selectedRole))
            {
                MessageBox.Show("Выберите роль из списка.");
                return;
            }
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            try
            {
                conn.Open();
                if (!IsLoginUnique(login, conn))
                {
                    MessageBox.Show("Логин уже занят. Пожалуйста, выберите другой логин.");
                    return;
                }

                if (IsPasswordValid(password))
                {
                    int roleId = GetRoleIdByName(selectedRole, conn);
                    string insertQuery = "INSERT INTO users (roles_id, login, password) VALUES (@roleId, @login, @password)";

                    using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, conn))
                    {
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@roleId", roleId);

                        try
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Вы успешно зарегистрированы.");
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при добавлении данных в базу данных: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        private int GetRoleIdByName(string roleName, NpgsqlConnection connection)
        {
            return registrationValidMethods.GetRoleIdByName(roleName, connection);
        }

        private bool IsPasswordValid(string password)
        {
            return registrationValidMethods.IsPasswordValid(password);
        }

        private bool IsLoginUnique(string login, NpgsqlConnection connection)
        {
            return registrationValidMethods.IsLoginUnique(login, connection);
        }
    }
}
