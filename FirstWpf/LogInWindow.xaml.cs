using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataAcces;


namespace FirstWpf
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        //List of Person
        List<Emplyee> people = new List<Emplyee>();

        public LogInWindow()
        {
            InitializeComponent();
        }

        //Login Button Functionality
        private void Login(object sender, RoutedEventArgs e)
        {
            Random newRandom = new Random();
            int ID = newRandom.Next(5, 100);
            EncodingData DataEncoding = new EncodingData();
            DataAccess db = new DataAccess();
            people = db.GetPeople(userName.Text);
            string compare = userName.Text;




            //Check if the user is not in the database
            bool containsItem = people.Any(item => item.UserName.Trim() == userName.Text);
            if (!containsItem && AddUser.IsChecked == false)
            {
               MessageBox.Show("User does not exist");
            }
            //Check if the user is already in db
            if (AddUser.IsChecked.Value)
            {
                if (String.IsNullOrEmpty(userName.Text) || String.IsNullOrEmpty(passwoardUI.Password))
                {
                    MessageBox.Show("You did not enter Any Valid UserName or Password");
                    return;
                }
                if (people.Any(person => person.UserName.Trim() == userName.Text))
                {
                   MessageBox.Show("Already Exist");
                }
                else
                {
                    if(userName.Text.Length > 10)
                    {
                        MessageBox.Show("Username Too Long");
                        return;
                    }
                    DataAccess ac = new DataAccess();
                  
                    string[] digit = Regex.Split(compare, @"\D+");
                    foreach(string value in digit)
                    {
                        int number;
                        if (int.TryParse(value, out number))
                        {
                            MessageBox.Show("You cant use numbers in your Username");
                            return;
                        }
                    }

                    if (people.Any(x => x.ID != ID))
                    {
                        ac.AddEmployees(userName.Text, passwoardUI.Password, ID);
                        MessageBox.Show("Successfully Enrolled");
                        GotoMainWindow(sender, e);
                    }
                    else
                    {
                        int id = newRandom.Next(100, 1000);
                        ac.AddEmployees(userName.Text, passwoardUI.Password, id); 
                        MessageBox.Show("Successfully Enrolled");
                        GotoMainWindow(sender, e);
                    }
                }
            }
            else
            {
                //Check credentials of the input with the db ones
                foreach (Emplyee pers in people)
                {
                    string pas = pers.Password;
                    string DecryptedPassword = DataEncoding.Decript(pas);
                    if (people.Any(person => person.UserName.Trim() == userName.Text) && people.Any(person => DecryptedPassword == passwoardUI.Password))
                    {

                        MessageBox.Show("Logged in");
                        GotoMainWindow(sender, e);
                    }
                    else
                    {
                        MessageBox.Show($"Acces Denied");
                    }
                }
            }
        }
        //moving throw windows
        private void GotoMainWindow(object sender, RoutedEventArgs e)
        {
            var newForm = new MainWindow();
            newForm.Show();
            this.Close(); 
        }

      
        //Using enter key to press submite
        private void passwoardUI_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Login(sender,e);
            }
            else if(e.Key == Key.Escape)
            {
                Close();
            }
        }

        //moving cursor with enter key

        private void userName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (e.OriginalSource is Button) return;
                userName.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
    }
}
     
    

