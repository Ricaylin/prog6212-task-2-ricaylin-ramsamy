using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using progtask.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace progtask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Text = "";
            StudyAppContext context = new StudyAppContext();

            User user = new User()
            {
                Username = UsernameTextBox.Text,
                Password = ComputeHash(PasswordPasswordBox.Password)
            };

            context.Add(user);

            try
            {
                int response = await context.SaveChangesAsync();
                ErrorTextBlock.Text = "Successfully created account " + UsernameTextBox.Text;

            }
            catch (UniqueConstraintException ex)
            {
                ErrorTextBlock.Text = ex.Message;
            }
        }

        // Taken From https://www.technologycrowds.com/2019/10/compute-sha-256-hash-using-csharp-for-effective-secruity.html
        static string ComputeHash(string plainText)
        {
            // Create a SHA256 hash from string   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Computing Hash - returns here byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));

                // now convert byte array to a string   
                StringBuilder stringbuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringbuilder.Append(bytes[i].ToString("x2"));
                }
                return stringbuilder.ToString();
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Text = "";
            string hashedPassword = ComputeHash(PasswordPasswordBox.Password);
            StudyAppContext context = new StudyAppContext();
            var user = await(context.users.FirstOrDefaultAsync(b => b.Username == UsernameTextBox.Text && b.Password == hashedPassword));

            if (user == null)
            {
                ErrorTextBlock.Text = "You have inputted invalid details!";
            }
            else
            {
                ErrorTextBlock.Text = "Welcome " + user.Username;
                CurrentProfile.user = user;
                Window ModulesWindow = new ModulesWindow();
                ModulesWindow.Show();
                this.Hide();
            }
        }
    }
}
