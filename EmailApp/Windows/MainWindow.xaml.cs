using EmailApp.Models;
using EmailApp.Windows;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmailApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EmailInfo _emailInfo = new EmailInfo();
        private PostShiftQuery postShiftQuery = new PostShiftQuery();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            _emailInfo = postShiftQuery.GetNewMailInfo();
            if (_emailInfo.Key == null)
            {
                MessageBox.Show("Error: On the server technical work!");
                _emailInfo = null;
            }
            else
                AddressLabel.Content += _emailInfo.Email;

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (_emailInfo != null)
            {
                LettersGrid.ItemsSource = postShiftQuery.GetListLetters();
            }
            else
            {
                MessageBox.Show("Создайте почту");
                LettersGrid.ItemsSource = null;
            }
        }

        private void ReadLetterButton_Click(object sender, RoutedEventArgs e)
        {
            if (LettersGrid.SelectedItems.Count > 0)
            {
                Letter letter = (Letter)LettersGrid.SelectedItems[0];
                EmailTextWindow emailTextWindow = new EmailTextWindow();
                emailTextWindow.EmailTextBox.Text = postShiftQuery.GetLetterText(letter.Id);
                emailTextWindow.Show();
            }
            else
            {
                MessageBox.Show("Что прочитать?");
            }
        }

        private void LiveTimeUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (_emailInfo != null)
            {
                LiveTimeLabel.Content = $"Почте осталось: {postShiftQuery.GetEmailLiveTime()} секунд";
            }
            else
            {
                MessageBox.Show("Создайте почту");
            }
        }

        private void ProlongEmailButton_Click(object sender, RoutedEventArgs e)
        {
            if (_emailInfo != null)
            {
                LiveTimeLabel.Content = $"Почте осталось: {postShiftQuery.ProlongEmailLiveTime()} секунд";
            }
            else
            {
                MessageBox.Show("Создайте почту");
            }
        }

        private void DeleteEmailButton_Click(object sender, RoutedEventArgs e)
        {
            if (_emailInfo != null)
            {
                MessageBox.Show(postShiftQuery.DeleteEmail());
                Clear();
            }
            else
            {
                MessageBox.Show("Создайте почту");
            }
        }

        private void DeleteAllEmailByIPButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(postShiftQuery.DeleteAllEmailByIP());
            Clear();
        }

        private void Clear()
        {
            AddressLabel.Content = "Адрес почты: ";
            LiveTimeLabel.Content = "";
            LettersGrid.ItemsSource = null;
            _emailInfo = null;
        }

        private void CopyEmailButton_Click(object sender, RoutedEventArgs e)
        {
            if(_emailInfo != null)
            {
                Clipboard.SetData(DataFormats.Text, (Object)_emailInfo.Email);
            }
            else
            {
                MessageBox.Show("Создайте почту");
            }
        }

        private void ClearEmailButton_Click(object sender, RoutedEventArgs e)
        {
            if (_emailInfo != null)
            {
                MessageBox.Show(postShiftQuery.DeleteEmail());
                LettersGrid.ItemsSource = null;
            }
            else
            {
                MessageBox.Show("Создайте почту");
            }
        }
    }
}
