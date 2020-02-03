using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace FirstWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class Result
    {
        public Book[] Items { get; set; }
    }
    public class Book
    {
        public BookInfo VolumeInfo { get; set; }

    }

    public class BookInfo
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string[] authors { get; set; }
        public Imginfo imageLinks { get; set; }
        public string infoLink { get; set; }
        public float Price { get; set; }
    }
    public class Imginfo
    {
        public string thumbnail { get; set; }
    }
  

    public partial class MainWindow : Window
    {
        int credit = 100;
        DateTime data = DateTime.Now;
        public MainWindow()
        {
            InitializeComponent();

            Credit.Text = $"{credit:C}";
            Time.Text = data.ToString();
        }
       

        private void LogOut(object sender, RoutedEventArgs e)
        {
            var newForm = new LogInWindow();
            newForm.Show();
            this.Close();
        }
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (Toggle.IsChecked == true)
            {
                var bc = new BrushConverter();
                mainwindow.Background = (Brush)bc.ConvertFrom("#212121");
                leftside.Background = (Brush)bc.ConvertFrom("#484848");
                mainwindow.Foreground = new SolidColorBrush(Colors.White);
                hidenimg.Foreground = new SolidColorBrush(Colors.Black);
                shopBackg.Background = new SolidColorBrush(Colors.Gray);
                SearchBar.Foreground = new SolidColorBrush(Colors.Black);
            }
        }
     
        private void Toggle_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Toggle.IsChecked == false)
            {
                mainwindow.Background = new SolidColorBrush(Colors.White);
                leftside.Background = new SolidColorBrush(Colors.White);
                mainwindow.Foreground = new SolidColorBrush(Colors.Black);
                shopBackg.Background = new SolidColorBrush(Colors.White);

            }
        }

        private void Shop(object sender, RoutedEventArgs e)
        {
            ShopWindow.Visibility = Visibility.Visible;
        }
        private void closeShop(object sender, RoutedEventArgs e)
        {
            ShopWindow.Visibility = Visibility.Collapsed;
            ItemControl.ItemsSource = null;
            SearchBar.Clear();
        }

        public void Price(List<BookInfo> bok)
        {
                Random price = new Random();

           
            for(int i = 0; i < bok.Count; i++)
            {
                float number = price.Next(10,60);

                bok[i].Price = number * 3 / 2;
            }
        }

        private async void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            ItemControl.ItemsSource = null;

            HttpClient client = new HttpClient();
         

            try
            {
                string Title = SearchBar.Text.Trim();

                HttpResponseMessage response = await client.GetAsync($"https://www.googleapis.com/books/v1/volumes?q={Title}+inauthor:keyes");
                response.EnsureSuccessStatusCode();

                
                var res = JsonConvert.DeserializeObject<Result>(await response.Content.ReadAsStringAsync());

                List<BookInfo> books = new List<BookInfo>();
                for (int i = 0; i < res.Items.Length; i++)
                {
                    books.Add(new BookInfo { Subtitle = res.Items[0].VolumeInfo.Subtitle, 
                        Title = res.Items[i].VolumeInfo.Title, authors = res.Items[i].VolumeInfo.authors, 
                        imageLinks = res.Items[i].VolumeInfo.imageLinks,
                        infoLink = res.Items[i].VolumeInfo.infoLink});
                }
                Price(books);
                ItemControl.ItemsSource = books;
            }
            catch
            {
                MessageBox.Show("No Books Found");
            }
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

       
    }

}
