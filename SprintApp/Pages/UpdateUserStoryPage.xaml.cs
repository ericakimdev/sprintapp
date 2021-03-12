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
    /// Interaction logic for UpdateUserStoryPage.xaml
    /// </summary>
    public partial class UpdateUserStoryPage : Page
    {

        /*************************
        *      CONSTRUCTOR       *
        * ***********************/
        public UpdateUserStoryPage()
        {
            InitializeComponent();

            FillProjectsComboBox();
        }



        /*************************
        *        METHODS         *
        *************************/

        private void AddNotes_Click(object sender, RoutedEventArgs e)
        {
            notesListBox.Items.Add(notesTextBox.Text);
            notesTextBox.Text = "";

        } // END AddNotes_Click()


        private void ClearNotes_Click(object sender, RoutedEventArgs e)
        {
            notesListBox.Items.Clear();

        } // END ClearNotes_Click()



        private void UpdateUserStory_Click(object sender, RoutedEventArgs e)
        {
            // Check if required info has been entered
            if (!ValidateData())
            {
                return;
            }

            // Access collection
            var backlogColl = DBConnection.GetCollectionAsList<Backlog>("backlogs");

            // Create new Backlog object
            Backlog userStory = new Backlog();

            userStory.UserStory = userStoryTextBox.Text;
            userStory.SprintName = sprintTextBox.Text;
            userStory.Status = statusComboBox.SelectionBoxItem.ToString();

            int intNum;

            bool isParsable = int.TryParse(projectIdTextBox.Text, out intNum);

            if (isParsable)
            {
                userStory.ProjectId = intNum;
            }

            isParsable = int.TryParse(priorityTextBox.Text, out intNum);

            if (isParsable)
            {
                userStory.Priority = intNum;
            }

            isParsable = int.TryParse(relativeEstimateTextBox.Text, out intNum);

            if (isParsable)
            {
                userStory.RelativeEstimates = intNum;
            }

            double reEstNum;

            isParsable = double.TryParse(reestimateTextBox.Text, out reEstNum);

            if (isParsable)
            {
                userStory.ReEstimate = reEstNum;
            }


            // Get project developpers
            foreach (var item in notesListBox.Items)
            {
                userStory.Notes.Add(item.ToString());
            }
            
            // Define where/what function will be performed on DB
            var filter = Builders<Backlog>.Filter.Eq("UserStory", userStoryComboBox.SelectedItem.ToString());
            var update = Builders<Backlog>.Update.Set("SprintName", userStory.SprintName)
                                                 .Set("UserStory", userStory.UserStory)
                                                 .Set("RelativeEstimates", userStory.RelativeEstimates)
                                                 .Set("Priority", userStory.Priority)
                                                 .Set("ReEstimate", userStory.ReEstimate)
                                                 .Set("Status", userStory.Status)
                                                 .Set("Notes", userStory.Notes);

            // Update document
            DBConnection.UpdateDocument<Backlog>("backlogs", filter, update);

            ClearFields();

        } // END AddUserStory_Click()



        private void ProjectCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem != null)
            {

                string currentSelection = (sender as ComboBox).SelectedItem.ToString();

                // Get project info and populate fields
                var projectList = DBConnection.GetCollectionAsList<Project>("projects");
                var selectedProject = projectList.Find(b => b.ProjectName == currentSelection);

                projectIdTextBox.Text = selectedProject.ProjectId.ToString();
                projectNameTextBox.Text = selectedProject.ProjectName;

                userStoryComboBox.Items.Clear();

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
                ClearUserStoryFields();

                // Get currently selected combobox item
                string currentSelection = (sender as ComboBox).SelectedItem.ToString();

                // Get list of all backlogs
                var backlogsList = DBConnection.GetCollectionAsList<Backlog>("backlogs");

                // Fill new combobox if user stories are available
                foreach (Backlog item in backlogsList)
                {
                    if (item.UserStory == currentSelection)
                    {
                        projectIdTextBox.Text = item.ProjectId.ToString();
                        userStoryTextBox.Text = item.UserStory;
                        priorityTextBox.Text = item.Priority.ToString();
                        relativeEstimateTextBox.Text = item.RelativeEstimates.ToString();
                        reestimateTextBox.Text = item.ReEstimate.ToString();
                        statusComboBox.SelectedItem = statusComboBox.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == item.Status);

                        if (item.SprintName == null)
                        {
                            sprintTextBox.Text = "";
                        }
                        else
                        {
                            sprintTextBox.Text = item.SprintName;
                        }

                        foreach (var note in item.Notes)
                        {
                            notesListBox.Items.Add(note);
                        }

                    }
                }

            } // END If

        } // END UserStoryCB_SelectionChanged()



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


        public void ClearUserStoryFields()
        {
            projectIdTextBox.Text = "";
            sprintTextBox.Text = "";
            userStoryTextBox.Text = "";
            priorityTextBox.Text = "";
            relativeEstimateTextBox.Text = "";
            reestimateTextBox.Text = "";
            statusComboBox.SelectedItem = "Open";

            notesListBox.Items.Clear();

        } // END ClearFields()


        public void ClearFields()
        {
            projectIdTextBox.Text = "";
            projectNameTextBox.Text = "";
            sprintTextBox.Text = "";
            userStoryTextBox.Text = "";
            priorityTextBox.Text = "";
            relativeEstimateTextBox.Text = "";
            reestimateTextBox.Text = "";

            statusComboBox.SelectedIndex = 0;
            projectsComboBox.Items.Clear();
            userStoryComboBox.Items.Clear();
            notesListBox.Items.Clear();

            FillProjectsComboBox();

        } // END ClearFields()


        private bool ValidateData()
        {

            bool isValid = false;

            // IF required data missing, inform user
            if (userStoryTextBox.Text == "")
            {
                MessageBox.Show("A User Story Is Required");
            }
            else if (priorityTextBox.Text == "")
            {
                MessageBox.Show("A Priority # Is Required");
            }
            else if (relativeEstimateTextBox.Text == "")
            {
                MessageBox.Show("An Original Estimate Is Required");
            }
            else
            {
                isValid = true;
            }

            return isValid;

        } // END ValidateData()



    } // END UpdateUserStoryPage

} // END namespace