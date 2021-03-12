using System;
using System.Collections.Generic;
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
using SprintApp.Models;

namespace SprintApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        /*************************
        *        METHODS         *
        *************************/

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            // Check if required info has been entered
            if (!ValidateData())
            {
                return;
            }

            // Access collection
            List<Member> membersColl = DBConnection.GetCollectionAsList<Member>("members");

            // Create login Member object
            Member loginMember = new Member();
            bool isValidMember = false;
            foreach (var member in membersColl)
            {
                if (loginEmailTextBox.Text == member.email && loginPasswordBox.Password == member.password)
                {
                    isValidMember = true;
                    loginMember.email = member.email;
                    loginMember.password = member.password;
                    loginMember.name = member.name;
                }
            }

            if (isValidMember)
            {
                Application.Current.Properties["loginMember"] = loginMember;
                HomeWindow homeWindow = new HomeWindow();
                homeWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Email or Password. Please try again.");
                ClearFields();
                return;
            }
        } // END Login_Click()

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        } // END Cancel_Click()

        /*
         * Purpose: Allows user to drag application
         *          by holding down on title bar.
         */
        private void DragWindow(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }

        } // END DragWindow()


        /*************************
       *     HELPER METHODS     *
       *************************/

        private bool ValidateData()
        {
            bool isValid = false;

            // IF required data missing, inform user
            if (loginEmailTextBox.Text == "")
            {
                MessageBox.Show("Enter an Email");
            }
            else if (loginPasswordBox.Password == "")
            {
                MessageBox.Show("Enter a password");
            }
            else
            {
                isValid = true;
            }
            return isValid;

        } // END ValidateData()

        public void ClearFields()
        {
            loginEmailTextBox.Text = "";
            loginPasswordBox.Password = "";
        } // END ClearFields()
    }
}
