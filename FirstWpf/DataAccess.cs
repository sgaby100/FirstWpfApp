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
                DataClasses1DataContext dbContext = new DataClasses1DataContext();

                var solutie = from employe in dbContext.Emplyees
                              where employe.UserName == name
                              select employe;
                List<Person> pers = new List<Person>();

                foreach (var stat in solutie)
                {
                    pers.Add(new Person {UserName = stat.UserName,EmployeeId = Int32.Parse(stat.EmployeeId),Password = stat.Password});
                    return pers;
                }
                return pers;
            }
        }

        public void AddEmployees(string userName, string passwoardUI,int id)
        {

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EmplyeesDB")))
            {
                string EncryptedPassword = (DataEncoding.Encrypt(passwoardUI));

                DataClasses1DataContext dbContext = new DataClasses1DataContext();

                Emplyee pers = new Emplyee
                {
                EmployeeId = id.ToString(),
                UserName = userName,
                Password = EncryptedPassword,
                };

                dbContext.Emplyees.InsertOnSubmit(pers);

                try
                {
                    dbContext.SubmitChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }

            }
        }
    }
}