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
using SprintApp.Models;

namespace SprintApp.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {

        /*************************
        *      CONSTRUCTOR       *
        * ***********************/
        public RegisterPage()
        {
            InitializeComponent();
        }

        /*************************
        *        METHODS         *
        *************************/

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            // Check if required info has been entered
            if (!ValidateData())
            {
                return;
            }

            // Access collection
            var membersColl = DBConnection.GetCollectionAsList<Member>("members");

            // Create new Member object
            Member newMember = new Member();

            newMember.name = memberNameTextBox.Text;
            newMember.password = memberPasswordBox.Password;
            newMember.email = memberEmailTextBox.Text;

            // Insert document
            DBConnection.InsertDocument<Member>("members", newMember);

            ClearFields();

        } // END Register_Click()




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
            else if (memberPasswordBox.Password == "")
            {
                MessageBox.Show("Enter a password");
            }
            else if (memberConfirmPasswordBox.Password == "")
            {
                MessageBox.Show("Confirm a password");
            }
            else if (memberPasswordBox.Password != memberConfirmPasswordBox.Password)
            {
                MessageBox.Show("Password not matched");
            }
            else if (memberEmailTextBox.Text == "")
            {
                MessageBox.Show("Enter an Email");
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
            memberConfirmPasswordBox.Password = "";
            memberEmailTextBox.Text = "";

        } // END ClearFields()

    } // END RegisterPage class

} // END namespace