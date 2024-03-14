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
    public partial class ManagerForm : Form
    {
        private string connectionString = "Host=localhost;Username=postgres;Password=qwerty123;Database=postgres";
        private string userLogin;
        public ManagerForm(string userLogin)
        {
            InitializeComponent();
            comboBoxDataType.Items.AddRange(new string[] { "Физические лица", "Образовательные программы" });
            comboBoxDataType.SelectedIndex = 0;
            this.userLogin = userLogin;
            label2.Text = userLogin;
        }
        private void RefreshDataGridView()
        {
            if (comboBoxDataType.SelectedIndex == 0)
            {
                LoadPhysicalPersonsData();
            }
            else
            {
                LoadEducationalProgramsData();
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

        private void DeleteRecordFromDatabase(int id)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string deleteQuery;

                if (comboBoxDataType.SelectedIndex == 0)
                {
                    deleteQuery = "DELETE FROM natural_person WHERE id = @ID";
                }
                else
                {
                    deleteQuery = "DELETE FROM education_program WHERE id = @ID";
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
                string insertQuery;

                if (comboBoxDataType.SelectedIndex == 0)
                {
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
                }
                else
                {
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
                }
            }
        }
        private void UpdateRecordInDatabase(int id)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string updateQuery;

                if (comboBoxDataType.SelectedIndex == 0)
                {
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
                }
                else
                {
                    updateQuery = "UPDATE education_program SET " +
                        "name = @name, " +
                        "period_of_study = @period_of_study, " +
                        "qualification = @qualification, " +
                        "education_cost = @education_cost " +
                        "WHERE id = @ID";
                }

                using (NpgsqlCommand command = new NpgsqlCommand(updateQuery, conn))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    if (comboBoxDataType.SelectedIndex == 0)
                    {
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
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@name", selectedRow.Cells[0].Value.ToString());
                        command.Parameters.AddWithValue("@period_of_study", selectedRow.Cells[1].Value.ToString());
                        command.Parameters.AddWithValue("@qualification", selectedRow.Cells[2].Value.ToString());
                        command.Parameters.AddWithValue("@education_cost", Convert.ToDecimal(selectedRow.Cells[3].Value));
                    }

                    command.ExecuteNonQuery();
                }
            }
        }
        private void InsertNewRecord()
        {
            InsertRecordIntoDatabase();
            RefreshDataGridView();
            MessageBox.Show("Данные успешно добавлены в базу данных.");
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
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void buttonDeleteData_Click(object sender, EventArgs e)
        {
            DeleteSelectedRecord();
            MessageBox.Show("Данные успешно удалены в базе данных.");
        }

        private void buttonInsertData_Click(object sender, EventArgs e)
        {
            InsertNewRecord();
        }

        private void dealgenerate_Click(object sender, EventArgs e)
        {
            DealGenerate dealGenerate = new DealGenerate();
            dealGenerate.Show();
        }      
    }
}
