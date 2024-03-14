using System;
using System.Data;
using Npgsql;
using System.Windows.Forms;

namespace EducationApp
{
    public partial class DealGenerate : Form
    {
        private string connectionString = "Host=localhost;Username=postgres;Password=qwerty123;Database=postgres";
        private Generator generator = new Generator();
        public DealGenerate()
        {
            InitializeComponent();
            FillUserDataGridView();
            FillProgramsDataGridView();
        }

        private void FillUserDataGridView()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM natural_person";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "NaturalPersons");
                dataGridPersons.DataSource = dataSet.Tables["NaturalPersons"];
                dataGridPersons.Columns[0].HeaderText = "ID";
                dataGridPersons.Columns[1].HeaderText = "Фамилия";
                dataGridPersons.Columns[2].HeaderText = "Имя";
                dataGridPersons.Columns[3].HeaderText = "Отчество";
                dataGridPersons.Columns[4].HeaderText = "Дата рождения";
                dataGridPersons.Columns[5].HeaderText = "Паспортные данные";
                dataGridPersons.Columns[6].HeaderText = "Адрес";
                dataGridPersons.Columns[7].HeaderText = "Почта";
                dataGridPersons.Columns[8].HeaderText = "Номер телефона";
                dataGridPersons.Columns[9].HeaderText = "Должность";
                dataGridPersons.Columns[10].HeaderText = "Место работы";
            }
        }

        private void FillProgramsDataGridView()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM education_program";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "EducationPrograms");
                dataGridPrograms.DataSource = dataSet.Tables["EducationPrograms"];
                dataGridPrograms.Columns[0].HeaderText = "ID";
                dataGridPrograms.Columns[1].HeaderText = "Название";
                dataGridPrograms.Columns[2].HeaderText = "Срок обучения";
                dataGridPrograms.Columns[3].HeaderText = "Квалификация";
                dataGridPrograms.Columns[4].HeaderText = "Стоимость обучения";
            }
        }

        private void buttonDealGenerateTXT_Click(object sender, EventArgs e)
        {
            // Получение выбранных данных из dataGridPersons
            int selectedPersonIndex = dataGridPersons.CurrentCell.RowIndex;
            string personFirstName = dataGridPersons.Rows[selectedPersonIndex].Cells[1].Value.ToString();
            string personLastName = dataGridPersons.Rows[selectedPersonIndex].Cells[2].Value.ToString();
            string personMidName = dataGridPersons.Rows[selectedPersonIndex].Cells[3].Value.ToString();
            DateTime personBirthday = Convert.ToDateTime(dataGridPersons.Rows[selectedPersonIndex].Cells[4].Value);
            string personAddress = dataGridPersons.Rows[selectedPersonIndex].Cells[6].Value.ToString();
            string personPassportData = dataGridPersons.Rows[selectedPersonIndex].Cells[5].Value.ToString();
            string personPhoneNumber = dataGridPersons.Rows[selectedPersonIndex].Cells[8].Value.ToString();

            // Получение выбранной образовательной программы из dataGridPrograms
            int selectedProgramIndex = dataGridPrograms.CurrentCell.RowIndex;
            string educationProgram = dataGridPrograms.Rows[selectedProgramIndex].Cells[1].Value.ToString();
            string educationCost = dataGridPrograms.Rows[selectedPersonIndex].Cells[4].Value.ToString();

            // Создание объекта DealInfo на основе выбранных данных
            DealInfo selectedDealInfo = new DealInfo(
                selectedPersonIndex,
                personFirstName,
                personLastName,
                personMidName,
                personBirthday,
                personAddress,
                personPassportData,
                personPhoneNumber,
                educationProgram,
                educationCost
            );
            generator.GenerateTXT("Амогус", selectedDealInfo);
        }

        private void buttonDealGeneratePDF_Click(object sender, EventArgs e)
        {
            // Получение выбранных данных из dataGridPersons
            int selectedPersonIndex = dataGridPersons.CurrentCell.RowIndex;
            string personFirstName = dataGridPersons.Rows[selectedPersonIndex].Cells[1].Value.ToString();
            string personLastName = dataGridPersons.Rows[selectedPersonIndex].Cells[2].Value.ToString();
            string personMidName = dataGridPersons.Rows[selectedPersonIndex].Cells[3].Value.ToString();
            DateTime personBirthday = Convert.ToDateTime(dataGridPersons.Rows[selectedPersonIndex].Cells[4].Value);
            string personAddress = dataGridPersons.Rows[selectedPersonIndex].Cells[6].Value.ToString();
            string personPassportData = dataGridPersons.Rows[selectedPersonIndex].Cells[5].Value.ToString();
            string personPhoneNumber = dataGridPersons.Rows[selectedPersonIndex].Cells[8].Value.ToString();

            // Получение выбранной образовательной программы из dataGridPrograms
            int selectedProgramIndex = dataGridPrograms.CurrentCell.RowIndex;
            string educationProgram = dataGridPrograms.Rows[selectedProgramIndex].Cells[1].Value.ToString();
            string educationCost = dataGridPrograms.Rows[selectedPersonIndex].Cells[4].Value.ToString();

            // Создание объекта DealInfo на основе выбранных данных
            DealInfo selectedDealInfo = new DealInfo(
                selectedPersonIndex,
                personFirstName,
                personLastName,
                personMidName,
                personBirthday,
                personAddress,
                personPassportData,
                personPhoneNumber,
                educationProgram,
                educationCost
            );
            generator.GeneratePDF("Амогус", selectedDealInfo);
        }

        private void buttonQrPdf_Click(object sender, EventArgs e)
        {
            // Получение выбранных данных из dataGridPersons
            int selectedPersonIndex = dataGridPersons.CurrentCell.RowIndex;
            string personFirstName = dataGridPersons.Rows[selectedPersonIndex].Cells[1].Value.ToString();
            string personLastName = dataGridPersons.Rows[selectedPersonIndex].Cells[2].Value.ToString();
            string personMidName = dataGridPersons.Rows[selectedPersonIndex].Cells[3].Value.ToString();
            DateTime personBirthday = Convert.ToDateTime(dataGridPersons.Rows[selectedPersonIndex].Cells[4].Value);
            string personAddress = dataGridPersons.Rows[selectedPersonIndex].Cells[6].Value.ToString();
            string personPassportData = dataGridPersons.Rows[selectedPersonIndex].Cells[5].Value.ToString();
            string personPhoneNumber = dataGridPersons.Rows[selectedPersonIndex].Cells[8].Value.ToString();

            // Получение выбранной образовательной программы из dataGridPrograms
            int selectedProgramIndex = dataGridPrograms.CurrentCell.RowIndex;
            string educationProgram = dataGridPrograms.Rows[selectedProgramIndex].Cells[1].Value.ToString();
            string educationCost = dataGridPrograms.Rows[selectedPersonIndex].Cells[4].Value.ToString();

            // Создание объекта DealInfo на основе выбранных данных
            DealInfo selectedDealInfo = new DealInfo(
                selectedPersonIndex,
                personFirstName,
                personLastName,
                personMidName,
                personBirthday,
                personAddress,
                personPassportData,
                personPhoneNumber,
                educationProgram,
                educationCost
            );
            generator.GeneratePdfPaymentList("Амогус", selectedDealInfo);
        }
    }
}
