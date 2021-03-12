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
    /// Interaction logic for AddUserStoryPage.xaml
    /// </summary>
    public partial class AddUserStoryPage : Page
    {

        /*************************
        *      CONSTRUCTOR       *
        * ***********************/
        public AddUserStoryPage()
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



        private void AddUserStory_Click(object sender, RoutedEventArgs e)
        {
            // Check if required info has been entered
            if (!ValidateData())
            {
                return;
            }

            // Access collection
            var backlogColl = DBConnection.GetCollectionAsList<Backlog>("backlogs");

            // Create new Backlog object
            Backlog newUserStory = new Backlog();

            newUserStory.UserStory = userStoryTextBox.Text;
            newUserStory.SprintName = sprintTextBox.Text;
            newUserStory.Status = statusComboBox.SelectionBoxItem.ToString();

            int intNum;

            bool isParsable = int.TryParse(projectIdTextBox.Text, out intNum);

            if (isParsable)
            {
                newUserStory.ProjectId = intNum;
            }

            isParsable = int.TryParse(priorityTextBox.Text, out intNum);

            if (isParsable)
            {
                newUserStory.Priority = intNum;
            }

            isParsable = int.TryParse(relativeEstimateTextBox.Text, out intNum);

            if (isParsable)
            {
                newUserStory.RelativeEstimates = intNum;
            }

            double reEstNum;

            isParsable = double.TryParse(reestimateTextBox.Text, out reEstNum);

            if (isParsable)
            {
                newUserStory.ReEstimate = reEstNum;
            }


            // Get project developpers
            foreach (var item in notesListBox.Items)
            {
                newUserStory.Notes.Add(item.ToString());
            }

            // Insert document
            DBConnection.InsertDocument<Backlog>("backlogs", newUserStory);

            ClearFields();

        } // END AddUserStory_Click()



        private void ProjectCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem != null)
            {
                string currentSelection = (sender as ComboBox).SelectedItem.ToString();

                // Get project data
                var projectList = DBConnection.GetCollectionAsList<Project>("projects");
                var selectedProject = projectList.Find(b => b.ProjectName == currentSelection);

                // Populate fields
                projectIdTextBox.Text = selectedProject.ProjectId.ToString();
                projectNameTextBox.Text = selectedProject.ProjectName;
            }

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


        public void ClearFields()
        {
            projectIdTextBox.Text = "";
            projectNameTextBox.Text = "";
            userStoryTextBox.Text = "";
            priorityTextBox.Text = "";
            relativeEstimateTextBox.Text = "";
            reestimateTextBox.Text = "";

            notesListBox.Items.Clear();

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



    } // END AddUserStoryPage class

} // END namespace