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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MongoDB.Driver;
using SprintApp.Models;

namespace SprintApp.Pages
{
    /// <summary>
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        private Member loginMember = new Member();
        public Member LoginMember
        {
            get { return loginMember; }
            set { loginMember = value; }
        }

        public AccountPage()
        {
            InitializeComponent();
            loginMember = (Member)Application.Current.Properties["loginMember"];
            memberNameTextBox.Text = loginMember.name;
            memberEmailTextBox.Text = loginMember.email;
            memberPasswordBox.Password = loginMember.password;
            memberConfirmPasswordBox.Password = loginMember.password;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            // Check if required info has been entered
            if (!ValidateData())
            {
                return;
            }

            loginMember = (Member)Application.Current.Properties["loginMember"];
            var filter = Builders<Member>.Filter.Eq("email", loginMember.email);

            var update = Builders<Member>.Update.Set("name", memberNameTextBox.Text)
                                                .Set("email", memberEmailTextBox.Text)
                                                .Set("password", memberPasswordBox.Password);

            DBConnection.UpdateDocument<Member>("members", filter, update);

            Member updateMember = new Member();
            updateMember.name = memberNameTextBox.Text;
            updateMember.password = memberPasswordBox.Password;
            updateMember.email = memberEmailTextBox.Text;            

            Application.Current.Properties["loginMember"] = updateMember;

            MessageBox.Show("Updated Successfully");
        }


        /*************************
        *     HELPER METHODS     *
        *************************/

        private bool ValidateData()
        {
            bool isValid = false;

            // IF required data missing, inform user
            if (memberNameTextBox.Text == "")
            {
                MessageBox.Show("Enter a name");
            }            
            else if (memberEmailTextBox.Text == "")
            {
                MessageBox.Show("Enter an Email");
            }
            else if (memberPasswordBox.Password == "")
            {
                MessageBox.Show("Enter an password");
            }
            else if (memberConfirmPasswordBox.Password == "")
            {
                MessageBox.Show("Confirm password");
            }
            else if (memberPasswordBox.Password != memberConfirmPasswordBox.Password)
            {
                MessageBox.Show("Password not matched");
            }
            else
            {
                isValid = true;
            }
            return isValid;

        } // END ValidateData()

        public void ClearFields()
        {
            memberNameTextBox.Text = "";
            memberPasswordBox.Password = "";
            memberEmailTextBox.Text = "";
            memberConfirmPasswordBox.Password = "";

        } // END ClearFields()
    }
}
