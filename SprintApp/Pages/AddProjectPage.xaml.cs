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
using MongoDB.Bson;
using MongoDB.Driver;
using SprintApp.Models;

namespace SprintApp.Pages
{
    /// <summary>
    /// Interaction logic for AddProjectPage.xaml
    /// </summary>
    public partial class AddProjectPage : Page
    {

        /*************************
        *      CONSTRUCTOR       *
        * ***********************/
        public AddProjectPage()
        {
            InitializeComponent();

            FillMembersComboBox();
        }



        /*************************
        *        METHODS         *
        *************************/

        private void AddDev_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string currentSelection = devComboBox.SelectedItem.ToString();
                membersListBox.Items.Add(currentSelection);
                devComboBox.Items.Remove(devComboBox.SelectionBoxItem);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Must pick a developer first");
                return;
            }

        } // END AddDev_Click()



        private void ClearList_Click(object sender, RoutedEventArgs e)
        {
            membersListBox.Items.Clear();
            FillMembersComboBox();

        } // END ClearList_Click()



        private void AddNewProject_Click(object sender, RoutedEventArgs e)
        {
            // Check if required info has been entered
            if (!ValidateData())
            {
                return;
            }

            // Access collection
            var projectsColl = DBConnection.GetCollectionAsList<Project>("projects");

            // Create new Project object
            Project newProject = new Project();

            newProject.ProjectId = DBConnection.GetCollectionCount<Project>("projects") + 1;
            newProject.ProjectName = projectNameTextBox.Text;
            newProject.Manager = managerTextBox.Text;
            newProject.Client = clientTextBox.Text;

            // Get project developpers
            foreach (var item in membersListBox.Items)
            {
                newProject.Members.Add(item.ToString());
            }

            // Parse Start Date to String
            try
            {
                newProject.StartDate = DateTime.Parse(startDateTextBox.Text);
            }
            catch (FormatException)
            {
                Console.WriteLine("Unable to parse the specified date");
            }

            // Parse Due Date to String
            try
            {
                newProject.DueDate = DateTime.Parse(dueDateTextBox.Text);
            }
            catch (FormatException)
            {
                Console.WriteLine("Unable to parse the specified date");
            }

            // Insert document
            DBConnection.InsertDocument<Project>("projects", newProject);

            ClearFields();

        } // END AddNewProject_Click()



        /*************************
        *     HELPER METHODS     *
        *************************/

        private void FillMembersComboBox()
        {
            var memberList = DBConnection.GetCollectionAsList<Member>("members");

            devComboBox.Items.Clear();

            foreach (Member mbr in memberList)
            {
                devComboBox.Items.Add(mbr.name);
            }

        } // END FillMembersComboBox()



        private bool ValidateData()
        {

            bool isValid = false;

            // IF required data missing, inform user
            if (projectNameTextBox.Text == "")
            {
                MessageBox.Show("A Project Name Is Required");
            }
            else if (managerTextBox.Text == "")
            {
                MessageBox.Show("A Manager Name Is Required");
            }
            else if (clientTextBox.Text == "")
            {
                MessageBox.Show("A Client Name Is Required");
            }
            else if (startDateTextBox.Text == "")
            {
                MessageBox.Show("A Start Date Is Required");
            }
            else if (dueDateTextBox.Text == "")
            {
                MessageBox.Show("An Due Date Is Required");
            }
            else
            {
                isValid = true;
            }

            return isValid;

        } // END ValidateData()



        public void ClearFields()
        {
            projectNameTextBox.Text = "";
            managerTextBox.Text = "";
            clientTextBox.Text = "";
            startDateTextBox.Text = "";
            dueDateTextBox.Text = "";
            membersListBox.Items.Clear();

            FillMembersComboBox();

        } // END ClearFields()


    } // END AddProjectPage class

} // END namespace