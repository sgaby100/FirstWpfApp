using DataAcces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FirstWpf
{
    public class DataAccess
    {
        readonly EncodingData  DataEncoding = new EncodingData();
        public List<Emplyee> GetPeople(string name)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EmplyeesDB")))
            {
                DataBaseConectionDataContext dbContext = new DataBaseConectionDataContext();

                var solutie = from employe in dbContext.Emplyees
                              where employe.UserName == name
                              select employe;


                var result = solutie.ToList();
                          
              
                return result;
            }
        }

        public void AddEmployees(string userName, string passwoardUI,int id)
        {

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EmplyeesDB")))
            {
                string EncryptedPassword = (DataEncoding.Encrypt(passwoardUI));

                DataBaseConectionDataContext dbContext = new DataBaseConectionDataContext();

                Emplyee pers = new Emplyee
                {
                ID = id,
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