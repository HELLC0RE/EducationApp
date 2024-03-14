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
    public partial class Authorization : Form
    {
        private string connectionString = "Host=localhost;Username=postgres;Password=qwerty123;Database=postgres";
        private int failedLoginAttempts = 0;
        private const int maxFailedAttempts = 3;
        private string userLogin;
        public Authorization()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            registration.ShowDialog();   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = loginStr.Text;
            string password = passStr.Text;
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль.");
                return;
            }
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "SELECT roles_id FROM users WHERE login = @login AND password = @password";
                using (NpgsqlCommand command = new NpgsqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);
                    object result = command.ExecuteScalar();
                    if (result == null)
                    {
                        failedLoginAttempts++;
                        if (failedLoginAttempts >= maxFailedAttempts)
                        {
                            DialogResult dialogResult = MessageBox.Show($"Вы израсходовали все попытки для входа. Хотите сбросить пароль?", "Предупреждение", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                ResetPasswordForm resetPasswordForm = new ResetPasswordForm();
                                resetPasswordForm.ShowDialog();
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Неверный логин или пароль. Попытка {failedLoginAttempts} из {maxFailedAttempts}.");
                        }
                        return;
                    }
                    failedLoginAttempts = 0;
                    int roleId = Convert.ToInt32(result);
                    userLogin = login;
                    switch (roleId)
                    {
                        case 1:
                            MessageBox.Show("Добро пожаловать, Администратор!");
                            AdminForm adminForm = new AdminForm(userLogin);
                            adminForm.ShowDialog();
                            break;
                        case 2:
                            MessageBox.Show("Добро пожаловать, Менеджер!");
                            ManagerForm managerForm = new ManagerForm(userLogin);
                            managerForm.ShowDialog();
                            break;
                    }                   
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

        private void Authorization_Load(object sender, EventArgs e)
        {

        }
    }
}
