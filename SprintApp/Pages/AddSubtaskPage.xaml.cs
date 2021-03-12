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
    /// Interaction logic for AddSubtaskPage.xaml
    /// </summary>
    public partial class AddSubtaskPage : Page
    {

        /*************************
        *      CONSTRUCTOR       *
        * ***********************/
        public AddSubtaskPage()
        {
            InitializeComponent();
            FillProjectsComboBox();
            FillMembersComboBox();
        }


        /*************************
        *        METHODS         *
        *************************/

        private void AddSubtask_Click(object sender, RoutedEventArgs e)
        {
            // Check if required info has been entered
            if (!ValidateData())
            {
                return;
            }

            // Create List with subtask values
            List<string> newSubtask = new List<string>();

            newSubtask.Add(subtaskNameTextBox.Text);
            newSubtask.Add(devComboBox.SelectedItem.ToString());
            newSubtask.Add(estimateTimeTextBox.Text);
            newSubtask.Add("");

            // Define where/what function will be performed on DB
            var filter = Builders<Backlog>.Filter.Eq("UserStory", userStoryComboBox.SelectedItem.ToString());
            var update = Builders<Backlog>.Update.Push("Subtasks", newSubtask);

            // Insert document
            DBConnection.UpdateDocument<Backlog>("backlogs", filter, update);

            ClearFields();

        } // END AddSubtask_Click()



        private void ProjectCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem != null)
            {
                // Get currently selected combobox item
                string currentSelection = (sender as ComboBox).SelectedItem.ToString();

                // Find project matching selection(for ProjectId)
                var projectList = DBConnection.GetCollectionAsList<Project>("projects");
                var selectedProject = projectList.Find(b => b.ProjectName == currentSelection);

                // Get list of all backlogs
                var backlogsList = DBConnection.GetCollectionAsList<Backlog>("backlogs");

                // Fill new combobox if user stories are available
                foreach (Backlog item in backlogsList)
                {
                    if (item.ProjectId == selectedProject.ProjectId)
                    {
                        userStoryComboBox.Items.Add(item.UserStory);
                    }
                }

            } // END If

        } // END ProjectCB_SelectionChanged()



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
            subtaskNameTextBox.Text = "";
            estimateTimeTextBox.Text = "";
            devComboBox.SelectedIndex = -1;

        } // END ClearFields()


        private bool ValidateData()
        {

            bool isValid = false;

            // IF required data missing, inform user
            if (subtaskNameTextBox.Text == "")
            {
                MessageBox.Show("A Subtask Name Is Required");
            }
            else if (estimateTimeTextBox.Text == "")
            {
                MessageBox.Show("An Estimate Time Is Required");
            }
            else
            {
                isValid = true;
            }

            return isValid;

        } // END ValidateData()



    } // END AddSubtaskPage class

} // END namespace