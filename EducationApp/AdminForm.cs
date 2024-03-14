using Npgsql;
using NpgsqlTypes;
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
    public partial class AdminForm : Form
    {
        private string connectionString = "Host=localhost;Username=postgres;Password=qwerty123;Database=postgres";
        private string userLogin;
        public AdminForm(string userLogin)
        {
            InitializeComponent();
            this.userLogin = userLogin;
            comboBoxDataType.Items.AddRange(new string[] { "Физические лица", "Образовательные программы", "Роли", "Пользователи", "Договора" });
            comboBoxDataType.SelectedIndex = 0;
            label2.Text = userLogin;
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }
        private void RefreshDataGridView()
        {
            switch (comboBoxDataType.SelectedIndex)
            {
                case 0:
                    LoadPhysicalPersonsData();
                    break;
                case 1:
                    LoadEducationalProgramsData();
                    break;
                case 2:
                    LoadRolesData();
                    break;
                case 3:
                    LoadUsersData();
                    break;
                case 4:
                    LoadDealData();
                    break;
                default:
                    break;
            }
        }
        private void LoadPhysicalPersonsData()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT last_name, first_name, midle_name, birthday, passport_data, address, email, phone_number, position, workplace FROM natural_person";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "NaturalPersons");
                dataGridView1.DataSource = dataSet.Tables["NaturalPersons"];
                // Установка заголовков
                dataGridView1.Columns[0].HeaderText = "Фамилия";
                dataGridView1.Columns[1].HeaderText = "Имя";
                dataGridView1.Columns[2].HeaderText = "Отчество";
                dataGridView1.Columns[3].HeaderText = "Дата рождения";
                dataGridView1.Columns[4].HeaderText = "Паспортные данные";
                dataGridView1.Columns[5].HeaderText = "Адрес";
                dataGridView1.Columns[6].HeaderText = "Почта";
                dataGridView1.Columns[7].HeaderText = "Номер телефона";
                dataGridView1.Columns[8].HeaderText = "Должность";
                dataGridView1.Columns[9].HeaderText = "Место работы";
            }
        }

        private void LoadEducationalProgramsData()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT name, period_of_study, qualification, education_cost FROM education_program";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "EducationPrograms");
                dataGridView1.DataSource = dataSet.Tables["EducationPrograms"];
                // Установка заголовков
                dataGridView1.Columns[0].HeaderText = "Название";
                dataGridView1.Columns[1].HeaderText = "Срок обучения";
                dataGridView1.Columns[2].HeaderText = "Квалификация";
                dataGridView1.Columns[3].HeaderText = "Стоимость обучения";
            }
        }
        private void LoadRolesData()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT role_name FROM roles";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Roles");
                dataGridView1.DataSource = dataSet.Tables["Roles"];
                // Установка заголовков
                dataGridView1.Columns[0].HeaderText = "Название роли";
            }
        }
        private void LoadUsersData()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT login, password FROM users";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Users");
                dataGridView1.DataSource = dataSet.Tables["Users"];
                // Установка заголовков
                dataGridView1.Columns[0].HeaderText = "Логин";
                dataGridView1.Columns[1].HeaderText = "Пароль";
            }
        }
        private void LoadDealData()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT student_full_name, target, payer_full_name, payer_address, kbk, oktmo, total_sum FROM deal";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Deals");
                dataGridView1.DataSource = dataSet.Tables["Deals"];
                dataGridView1.Columns[0].HeaderText = "Студент";
                dataGridView1.Columns[1].HeaderText = "Цель";
                dataGridView1.Columns[2].HeaderText = "Плательщик";
                dataGridView1.Columns[3].HeaderText = "Адрес плательщика";
                dataGridView1.Columns[4].HeaderText = "КБК";
                dataGridView1.Columns[5].HeaderText = "ОКТМО";
                dataGridView1.Columns[6].HeaderText = "Общая сумма";
            }
        }
        private void DeleteRecordFromDatabase(int id)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string deleteQuery = "";

                switch (comboBoxDataType.SelectedIndex)
                {
                    case 0:
                        deleteQuery = "DELETE FROM natural_person WHERE id = @ID";
                        break;
                    case 1:
                        deleteQuery = "DELETE FROM education_program WHERE id = @ID";
                        break;
                    case 2:
                        deleteQuery = "DELETE FROM roles WHERE id = @ID";
                        break;
                    case 3:
                        deleteQuery = "DELETE FROM users WHERE id = @ID";
                        break;
                    case 4:
                        deleteQuery = "DELETE FROM deal WHERE id = @ID";
                        break;
                    default:
                        break;
                }

                using (NpgsqlCommand command = new NpgsqlCommand(deleteQuery, conn))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void DeleteSelectedRecord()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = dataGridView1.SelectedRows[0].Index + 1;
                DeleteRecordFromDatabase(id);
                RefreshDataGridView();
            }
        }
        private void InsertRecordIntoDatabase()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string insertQuery = "";

                switch (comboBoxDataType.SelectedIndex)
                {
                    case 0:
                        insertQuery = "INSERT INTO natural_person (last_name, first_name, midle_name, birthday, passport_data, address, email, phone_number, position, workplace)" +
                            " VALUES (@last_name, @first_name, @midle_name, @birthday, @passport_data, @address, @email, @phone_number, @position, @workplace)";
                        using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, conn))
                        {
                            DataGridViewRow lastRow = dataGridView1.Rows[dataGridView1.Rows.Count - 2];
                            command.Parameters.AddWithValue("@last_name", lastRow.Cells[0].Value.ToString());
                            command.Parameters.AddWithValue("@first_name", lastRow.Cells[1].Value.ToString());
                            command.Parameters.AddWithValue("@midle_name", lastRow.Cells[2].Value.ToString());
                            command.Parameters.AddWithValue("@birthday", lastRow.Cells[3].Value.ToString());
                            command.Parameters.AddWithValue("@passport_data", Convert.ToInt32(lastRow.Cells[4].Value));
                            command.Parameters.AddWithValue("@address", lastRow.Cells[5].Value.ToString());
                            command.Parameters.AddWithValue("@email", lastRow.Cells[6].Value.ToString());
                            command.Parameters.AddWithValue("@phone_number", Convert.ToInt32(lastRow.Cells[7].Value));
                            command.Parameters.AddWithValue("@position", lastRow.Cells[8].Value.ToString());
                            command.Parameters.AddWithValue("@workplace", lastRow.Cells[9].Value.ToString());
                            command.ExecuteNonQuery();
                        }
                        break;
                    case 1:
                        insertQuery = "INSERT INTO education_program (name, period_of_study, qualification, education_cost) VALUES " +
                            "(@name, @period_of_study, @qualification, @education_cost)";
                        using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, conn))
                        {
                            DataGridViewRow lastRow = dataGridView1.Rows[dataGridView1.Rows.Count - 2];
                            command.Parameters.AddWithValue("@name", lastRow.Cells[0].Value.ToString());
                            command.Parameters.AddWithValue("@period_of_study", lastRow.Cells[1].Value.ToString());
                            command.Parameters.AddWithValue("@qualification", lastRow.Cells[2].Value.ToString());
                            command.Parameters.AddWithValue("@education_cost", Convert.ToDecimal(lastRow.Cells[3].Value));
                            command.ExecuteNonQuery();
                        }
                        break;
                    case 2:
                        insertQuery = "INSERT INTO roles (role_name) VALUES (@role_name)";
                        using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, conn))
                        {
                            DataGridViewRow lastRow = dataGridView1.Rows[dataGridView1.Rows.Count - 2];
                            command.Parameters.AddWithValue("@role_name", lastRow.Cells[0].Value.ToString());
                            command.ExecuteNonQuery();
                        }
                        break;
                    case 3:
                        insertQuery = "INSERT INTO users (login, password) VALUES (@login, @password)";
                        using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, conn))
                        {
                            DataGridViewRow lastRow = dataGridView1.Rows[dataGridView1.Rows.Count - 2];
                            command.Parameters.AddWithValue("@login", lastRow.Cells[0].Value.ToString());
                            command.Parameters.AddWithValue("@password", lastRow.Cells[1].Value.ToString());
                            command.ExecuteNonQuery();
                        }
                        break;
                    case 4:
                        insertQuery = "INSERT INTO deal (student_full_name, target, payer_full_name, payer_address, kbk, oktmo, total_sum) VALUES " +
                            "(@student_full_name, @target, @payer_full_name, @payer_address, @kbk, @oktmo, @total_sum)";
                        using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, conn))
                        {
                            DataGridViewRow lastRow = dataGridView1.Rows[dataGridView1.Rows.Count - 2];
                            command.Parameters.AddWithValue("@student_full_name", lastRow.Cells[0].Value.ToString());
                            command.Parameters.AddWithValue("@target", lastRow.Cells[1].Value.ToString());
                            command.Parameters.AddWithValue("@payer_full_name", lastRow.Cells[2].Value.ToString());
                            command.Parameters.AddWithValue("@payer_address", lastRow.Cells[3].Value.ToString());
                            command.Parameters.AddWithValue("@kbk", lastRow.Cells[4].Value.ToString());
                            command.Parameters.AddWithValue("@oktmo", lastRow.Cells[5].Value.ToString());
                            command.Parameters.AddWithValue("@total_sum", Convert.ToDecimal(lastRow.Cells[6].Value));
                            command.ExecuteNonQuery();
                        }
                        break;
                    default:
                        break;
                }               
            }
        }
        private void UpdateRecordInDatabase(int id)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string updateQuery = "";

                switch (comboBoxDataType.SelectedIndex)
                {
                    case 0:
                        updateQuery = "UPDATE natural_person SET " +
                            "last_name = @last_name, " +
                            "first_name = @first_name, " +
                            "midle_name = @midle_name, " +
                            "birthday = @birthday, " +
                            "passport_data = @passport_data, " +
                            "address = @address, " +
                            "email = @email, " +
                            "phone_number = @phone_number, " +
                            "position = @position, " +
                            "workplace = @workplace " +
                            "WHERE id = @ID";
                        break;
                    case 1:
                        updateQuery = "UPDATE education_program SET " +
                            "name = @name, " +
                            "period_of_study = @period_of_study, " +
                            "qualification = @qualification, " +
                            "education_cost = @education_cost " +
                            "WHERE id = @ID";
                        break;
                    case 2:
                        updateQuery = "UPDATE roles SET role_name = @role_name WHERE id = @ID";
                        break;
                    case 3:
                        updateQuery = "UPDATE users SET login = @login, password = @password WHERE id = @ID";
                        break;
                    case 4:
                        updateQuery = "UPDATE deal SET " +
                            "student_full_name = @student_full_name, " +
                            "target = @target, " +
                            "payer_full_name = @payer_full_name, " +
                            "payer_address = @payer_address, " +
                            "kbk = @kbk, " +
                            "oktmo = @oktmo, " +
                            "total_sum = @total_sum " +
                            "WHERE id = @ID";
                        break;
                    default:
                        break;
                }

                using (NpgsqlCommand command = new NpgsqlCommand(updateQuery, conn))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    switch (comboBoxDataType.SelectedIndex)
                    {
                        case 0:
                            DataGridViewRow lastRow = dataGridView1.Rows[dataGridView1.Rows.Count - 2];
                            command.Parameters.AddWithValue("@last_name", selectedRow.Cells[0].Value.ToString());
                            command.Parameters.AddWithValue("@first_name", selectedRow.Cells[1].Value.ToString());
                            command.Parameters.AddWithValue("@midle_name", selectedRow.Cells[2].Value.ToString());
                            command.Parameters.AddWithValue("@birthday", selectedRow.Cells[3].Value.ToString());
                            command.Parameters.AddWithValue("@passport_data", Convert.ToInt32(selectedRow.Cells[4].Value));
                            command.Parameters.AddWithValue("@address", selectedRow.Cells[5].Value.ToString());
                            command.Parameters.AddWithValue("@email", selectedRow.Cells[6].Value.ToString());
                            command.Parameters.AddWithValue("@phone_number", Convert.ToInt32(selectedRow.Cells[7].Value));
                            command.Parameters.AddWithValue("@position", selectedRow.Cells[8].Value.ToString());
                            command.Parameters.AddWithValue("@workplace", selectedRow.Cells[9].Value.ToString());
                            command.ExecuteNonQuery();
                            break;
                        case 1:
                            command.Parameters.AddWithValue("@name", selectedRow.Cells[0].Value.ToString());
                            command.Parameters.AddWithValue("@period_of_study", selectedRow.Cells[1].Value.ToString());
                            command.Parameters.AddWithValue("@qualification", selectedRow.Cells[2].Value.ToString());
                            command.Parameters.AddWithValue("@education_cost", Convert.ToDecimal(selectedRow.Cells[3].Value));
                            command.ExecuteNonQuery();
                            break;
                        case 2:
                            command.Parameters.AddWithValue("@role_name", selectedRow.Cells[0].Value.ToString());
                            command.ExecuteNonQuery();
                            break;
                        case 3:
                            command.Parameters.AddWithValue("@login", selectedRow.Cells[0].Value.ToString());
                            command.Parameters.AddWithValue("@password", selectedRow.Cells[1].Value.ToString());
                            command.ExecuteNonQuery();
                            break;
                        case 4:
                            command.Parameters.AddWithValue("@student_full_name", selectedRow.Cells[0].Value.ToString());
                            command.Parameters.AddWithValue("@target", selectedRow.Cells[1].Value.ToString());
                            command.Parameters.AddWithValue("@payer_full_name", selectedRow.Cells[2].Value.ToString());
                            command.Parameters.AddWithValue("@payer_address", selectedRow.Cells[3].Value.ToString());
                            command.Parameters.AddWithValue("@kbk", selectedRow.Cells[4].Value.ToString());
                            command.Parameters.AddWithValue("@oktmo", selectedRow.Cells[5].Value.ToString());
                            command.Parameters.AddWithValue("@total_sum", Convert.ToDecimal(selectedRow.Cells[6].Value));
                            command.ExecuteNonQuery();
                            break;
                        default:
                            break;
                    }
                    RefreshDataGridView();
                }
            }
        }
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }
       
        private void buttonRefreshData_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = dataGridView1.SelectedRows[0].Index + 1;
                UpdateRecordInDatabase(id);
                RefreshDataGridView();
            }
        }

        private void buttonDeleteData_Click(object sender, EventArgs e)
        {
            DeleteSelectedRecord();
            MessageBox.Show("Данные успешно удалены в базе данных.");
        }

        private void buttonInsertData_Click(object sender, EventArgs e)
        {
            InsertRecordIntoDatabase();
        }
    }
}
