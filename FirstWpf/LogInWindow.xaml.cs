using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FirstWpf
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        List<Person> people = new List<Person>();

        public LogInWindow()
        {

            InitializeComponent();
        }

        private void LogInBtn_Click(object sender, RoutedEventArgs e)
        {
            Random newRandom = new Random();
            int ID = newRandom.Next(5, 100);
            EncodingData DataEncoding = new EncodingData();
            DataAccess db = new DataAccess();
            people = db.GetPeople(userName.Text);
      
                bool containsItem = people.Any(item => item.UserName.Trim() == userName.Text);
            if (!containsItem && AddUser.IsChecked == false)
            {
                MessageBox.Show("User does not exist");
            }
           
            if (AddUser.IsChecked.Value)
            {
                if (people.Any(person => person.UserName.Trim() == userName.Text))
                {

                    MessageBox.Show("Already Exist");
                }
                else
                {
                    DataAccess ac = new DataAccess();
                    if(people.Any(person => person.EmployeeId != ID))
                    {
                        ac.AddEmployees(userName.Text, passwoardUI.Password, ID);
                        MessageBox.Show("Successfully Enrolled");
                    }
                    else
                    {
                        int id = newRandom.Next(100, 1000);
                        ac.AddEmployees(userName.Text, passwoardUI.Password, id);
                        MessageBox.Show("Successfully Enrolled");
                    }
                   
                }
            }
            else 
            {
                foreach (Person pers in people)
                {
                    string pas = pers.Password;
                    string DecryptedPassword = DataEncoding.Decript(pas);
                    if (people.Any(person => person.UserName.Trim() == userName.Text) && people.Any(person => DecryptedPassword == passwoardUI.Password))
                    {
                        MessageBox.Show("Logged in");
                    }
                    else
                    {
                        MessageBox.Show($"Acces Denied");
                    }
                  
                }
               
            }
        }
       
        private void AddUser_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
     
    

