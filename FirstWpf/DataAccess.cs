using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Dapper;

namespace FirstWpf
{
    public class DataAccess
    {
        EncodingData DataEncoding = new EncodingData();
        public List<Person> GetPeople(string name)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EmplyeesDB")))
            {
                var output = connection.Query<Person>($"select * from Emplyees where UserName = '{name}'").ToList();

                return output;
            }
        }

        public void AddEmployees(string userName, string passwoardUI,int id)
        {
            string sql = "INSERT INTO Emplyees (UserName, Password,EmployeeId) Values (@UserName, @Password, @EmployeeId);";
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EmplyeesDB")))
            {
                string EncryptedPassword = (DataEncoding.Encrypt(passwoardUI));
                Person c = new Person { UserName = userName, Password = EncryptedPassword, EmployeeId = id};
                var affectedRows = connection.Execute(sql, c);
            }
        }
    }
}