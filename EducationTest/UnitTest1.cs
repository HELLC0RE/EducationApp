using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System.Data;

namespace EducationApp.Tests
{
    [TestClass]
    public class RegistrationValidMethodsTests
    {
        private const string ConnectionString = "Host=localhost;Username=postgres;Password=qwerty123;Database=postgres";
        private RegistrationValidMethods registrationValidMethods;

        [TestInitialize]
        public void Init()
        {
            registrationValidMethods = new RegistrationValidMethods();
        }

        [TestMethod]
        public void GetRoleIdByName_ValidRoleName()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                string roleName = "администратор";
                int expectedRoleId = 1;
                int actualRoleId = registrationValidMethods.GetRoleIdByName(roleName, connection);
                Assert.AreEqual(expectedRoleId, actualRoleId);
            }
        }

        [TestMethod]
        public void GetRoleIdByName_NotValidRoleName()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                string roleName = "абоба";
                int expectedRoleId = -1;
                int actualRoleId = registrationValidMethods.GetRoleIdByName(roleName, connection);
                Assert.AreEqual(expectedRoleId, actualRoleId);
            }
        }

        [TestMethod]
        public void IsPasswordValid_ValidPassword()
        {
            string validPassword = "123fafaa@";
            bool isValid = registrationValidMethods.IsPasswordValid(validPassword);
            Assert.IsTrue(isValid);
        }
        [TestMethod]
        public void IsPasswordValid_NotValidPassword()
        {
            string validPassword = "1451251";
            bool isValid = registrationValidMethods.IsPasswordValid(validPassword);
            Assert.IsFalse(isValid);
        }
        [TestMethod]
        public void IsLoginUnique_ExistingLogin()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                string Login = "1234";
                bool isUnique = registrationValidMethods.IsLoginUnique(Login, connection);
                Assert.IsFalse(isUnique);
            }
        }
        [TestMethod]
        public void IsLoginUnique_NotExistingLogin()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                string Login = "121851afgaga";
                bool isUnique = registrationValidMethods.IsLoginUnique(Login, connection);
                Assert.IsTrue(isUnique);
            }
        }
    }
}
