using MongoDB.Driver;
using SprintApp.Models;
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

namespace SprintApp.Pages
{
    /// <summary>
    /// Interaction logic for UpdateProjectPage.xaml
    /// </summary>
    public partial class UpdateProjectPage : Page
    {

        /*************************
        *      CONSTRUCTOR       *
        * ***********************/
        public UpdateProjectPage()
        {
            InitializeComponent();

            FillProjectsComboBox();
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



        private void UpdateProject_Click(object sender, RoutedEventArgs e)
        {
            // Check if required info has been entered
            if (!ValidateData())
            {
                return;
            }

            DateTime startDate = new DateTime();
            DateTime dueDate = new DateTime(); 

            // Parse Start Date to String
            try
            {
                startDate = DateTime.Parse(startDateTextBox.Text);
            }
            catch (FormatException)
            {
                Console.WriteLine("Unable to parse the specified date");
            }

            // Parse Due Date to String
            try
            {
                dueDate = DateTime.Parse(dueDateTextBox.Text);
            }
            catch (FormatException)
            {
                Console.WriteLine("Unable to parse the specified date");
            }

            // Get project developpers
            List<string> devList = new List<string>();

            foreach (var item in membersListBox.Items)
            {
                devList.Add(item.ToString());
            }

            // Create filter and update for database update
            long projId = long.Parse(projectIdTextBox.Text);

            var filter = Builders<Project>.Filter.Eq("ProjectId", projId);
            var update = Builders<Project>.Update.Set("ProjectName",projectNameTextBox.Text).Set("Manager", managerTextBox.Text)
                                                 .Set("Client", clientTextBox.Text).Set("Members", devList)
                                                 .Set("StartDate", startDate).Set("DueDate", dueDate);

            DBConnection.UpdateDocument<Project>("projects", filter, update);

            ClearFields();

        } // END UpdateProject_Click()



        private void ProjectCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem != null)
            {
                string currentSelection = (sender as ComboBox).SelectedItem.ToString();

                // Get project info data into fields
                var projectList = DBConnection.GetCollectionAsList<Project>("projects");
                var selectedProject = projectList.Find(b => b.ProjectName == currentSelection);

                projectIdTextBox.Text = selectedProject.ProjectId.ToString();
                projectNameTextBox.Text = selectedProject.ProjectName;
                managerTextBox.Text = selectedProject.Manager;
                clientTextBox.Text = selectedProject.Client;
                startDateTextBox.Text = selectedProject.StartDate.ToString("MM/dd/yyyy");
                dueDateTextBox.Text = selectedProject.DueDate.ToString("MM/dd/yyyy");

                // Clear any previous data and reload
                membersListBox.Items.Clear();
                devComboBox.Items.Clear();
                FillMembersComboBox();

                // Load current project developers
                foreach (var dev in selectedProject.Members)
                {
                    membersListBox.Items.Add(dev);
                    devComboBox.Items.Remove(dev);
                }

            }

        } // END ProjectCB_SelectionChanged()



        private void DeleteProject_Click(object sender, RoutedEventArgs e)
        {
            long projId = long.Parse(projectIdTextBox.Text);

            var filter = Builders<Project>.Filter.Eq("ProjectId", projId);

            DBConnection.DeleteDocument<Project>("projects", filter);

            ClearFields();
        }



        /*************************
        *     HELPER METHODS     *
        *************************/

        private void FillProjectsComboBox()
        {
            var projectList = DBConnection.GetCollectionAsList<Project>("projects");
            projectsComboBox.Items.Clear();

            foreach (Project project in projectList)
            {
                projectsComboBox.Items.Add(project.ProjectName);
            }

        } // END FillProjectsComboBox()


        private void FillMembersComboBox()
        {
            var memberList = DBConnection.GetCollectionAsList<Member>("members");
            devComboBox.Items.Clear();

            foreach (Member mbr in memberList)
            {
                devComboBox.Items.Add(mbr.name);
            }

        } // END FillMembersComboBox()


        public void ClearFields()
        {
            projectNameTextBox.Text = "";
            managerTextBox.Text = "";
            clientTextBox.Text = "";
            startDateTextBox.Text = "";
            dueDateTextBox.Text = "";

            devComboBox.Items.Clear();
            membersListBox.Items.Clear();
            projectsComboBox.Items.Clear();

            FillProjectsComboBox();

        } // END ClearFields()


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



    } // END UpdateProjectPage class

} // END namespace