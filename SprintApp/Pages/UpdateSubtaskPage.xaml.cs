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
    /// Interaction logic for UpdateSubtaskPage.xaml
    /// </summary>
    public partial class UpdateSubtaskPage : Page
    {

        /*************************
        *      CONSTRUCTOR       *
        * ***********************/
        public UpdateSubtaskPage()
        {
            InitializeComponent();
            FillProjectsComboBox();
            FillMembersComboBox();
        }



        /*************************
        *        METHODS         *
        *************************/

        private void ProjectCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem != null)
            {
                // Get currently selected combobox item
                string currentSelection = (sender as ComboBox).SelectedItem.ToString();

                userStoryComboBox.Items.Clear();

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



        private void UserStoryCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem != null)
            {
                // Get currently selected combobox item
                string currentSelection = (sender as ComboBox).SelectedItem.ToString();

                subtaskComboBox.Items.Clear();

                // Get list of all backlogs
                var backlogsList = DBConnection.GetCollectionAsList<Backlog>("backlogs");

                // Fill new combobox if user stories are available
                foreach (Backlog item in backlogsList)
                {
                    if (item.UserStory == currentSelection)
                    {
                        foreach (var task in item.Subtasks)
                        {
                            subtaskComboBox.Items.Add(task[0]);
                        }
                    }
                }

            } // END If

        } // END UserStoryCB_SelectionChanged()



        private void SubtaskCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem != null)
            {
                // Get currently selected combobox item
                string currentSelection = (sender as ComboBox).SelectedItem.ToString();

                // Get list of all backlogs
                var backlogsList = DBConnection.GetCollectionAsList<Backlog>("backlogs");

                // Fill new combobox if user stories are available
                foreach (Backlog item in backlogsList)
                {
                    if (item.UserStory == userStoryComboBox.SelectedItem.ToString())
                    {
                        // To keep track of subtask index for future update
                        int index = 0;

                        foreach (var task in item.Subtasks)
                        {
                            if (task[0] == currentSelection)
                            {
                                indexTextBox.Text = index.ToString();
                                subtaskNameTextBox.Text = task[0];
                                devComboBox.SelectedItem = task[1];
                                estimateTimeTextBox.Text = task[2];
                                actualTimeTextBox.Text = task[3];
                                break;
                            }

                            index++;
                            
                        } // END inner ForEach

                    }

                } // END outer ForEach

            } // END outer If

        } // END SubtaskCB_SelectionChanged()



        private void UpdateSubtask_Click(object sender, RoutedEventArgs e)
        {
            // Check if required info has been entered
            if (!ValidateData())
            {
                return;
            }

            // Create List with updated subtask values
            List<string> updatedSubtask = new List<string>();

            updatedSubtask.Add(subtaskNameTextBox.Text);
            updatedSubtask.Add(devComboBox.SelectedItem.ToString());
            updatedSubtask.Add(estimateTimeTextBox.Text);
            updatedSubtask.Add(actualTimeTextBox.Text);

            // Get value of subtask index that needs to be updated
            string index = indexTextBox.Text;

            // Define where/what function will be performed on DB
            var filter = Builders<Backlog>.Filter.Eq("UserStory", userStoryComboBox.SelectedItem.ToString());
            var update = Builders<Backlog>.Update.Set("Subtasks." + index , updatedSubtask);

            // Update document
            DBConnection.UpdateDocument<Backlog>("backlogs", filter, update);

            ClearFields();
            FillProjectsComboBox();

        } // END AddSubtask_Click()




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
            actualTimeTextBox.Text = "";
            devComboBox.SelectedIndex = -1;

            subtaskComboBox.Items.Clear();
            userStoryComboBox.Items.Clear();
            projectsComboBox.Items.Clear();

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



    } // END UpdateSubtaskPage class

} // END namespace