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
    /// Interaction logic for SprintPage.xaml
    /// </summary>
    public partial class SprintPage : Page
    {

        /*************************
        *      CONSTRUCTOR       *
        * ***********************/
        public SprintPage()
        {
            InitializeComponent();

            FillProjectsComboBox();
        }



        /*************************
        *        METHODS         *
        *************************/

        private void ProjectCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Clear display and combobox
            sprintComboBox.Items.Clear();
            EstimatesTable.ItemsSource = null;
            EstimatesTable.Items.Refresh();

            if ((sender as ComboBox).SelectedItem != null)
            {
                string currentSelection = (sender as ComboBox).SelectedItem.ToString();

                // Get currently selected project
                var projectList = DBConnection.GetCollectionAsList<Project>("projects");
                var selectedProject = projectList.Find(b => b.ProjectName == currentSelection);

                // Get list of all backlogs
                var backlogsList = DBConnection.GetCollectionAsList<Backlog>("backlogs");

                // Used to avoid duplicates
                HashSet<string> uniqueSprintNames = new HashSet<string>();

                // Fill new combobox if user stories are available
                foreach (Backlog item in backlogsList)
                {
                    if (item.ProjectId == selectedProject.ProjectId)
                    {
                        uniqueSprintNames.Add(item.SprintName);
                    }
                }

                // Fill combobox
                foreach (string sprintName in uniqueSprintNames)
                {
                    sprintComboBox.Items.Add(sprintName);
                }

            }

        } // END ProjectCB_SelectionChanged()



        private void SprintCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem != null)
            {
                string currentSelection = (sender as ComboBox).SelectedItem.ToString();

                // Get project info
                var projectList = DBConnection.GetCollectionAsList<Project>("projects");
                var selectedProject = projectList.Find(b => b.ProjectName == projectsComboBox.SelectedItem.ToString());
                var selectedProjectId = selectedProject.ProjectId;

                //Get backlog and estimates time and actual time
                var backlogList = DBConnection.GetCollectionAsList<Backlog>("backlogs");
                var selectedProjectBacklogs = backlogList.FindAll(b => b.ProjectId == selectedProjectId);

                // Iterate through lists and obtain required info for sprint estimates
                List<Estimate> estimatesList = new List<Estimate>();

                foreach (var backlog in selectedProjectBacklogs)
                {

                    if (backlog.SprintName == currentSelection)
                    {
                        foreach (var sub in backlog.Subtasks)
                        {
                            // IF it exists, update values
                            if (estimatesList.Any(p => p.Name == sub[1] && p.UserStory == backlog.UserStory))
                            {
                                int index = estimatesList.FindIndex(p => p.Name == sub[1] && p.UserStory == backlog.UserStory);

                                estimatesList[index].EstimatedTime += double.Parse(sub[2]);

                                if (sub[3] != "")
                                {
                                    estimatesList[index].ActualTime += double.Parse(sub[3]);
                                }
                                else
                                {
                                    estimatesList[index].ActualTime += 0;
                                }

                            }
                            // ELSE, add new
                            else
                            {
                                Estimate estObj = new Estimate();
                                estObj.UserStory = backlog.UserStory;
                                estObj.Name = sub[1];
                                estObj.EstimatedTime = double.Parse(sub[2]);

                                if (sub[3] != "")
                                {
                                    estObj.ActualTime = double.Parse(sub[3]);
                                }
                                else
                                {
                                    estObj.ActualTime = 0;
                                }

                                double accPercentage = Math.Round((estObj.ActualTime / estObj.EstimatedTime), 2) * 100;

                                if (accPercentage > 100)
                                {
                                    estObj.Accuracy = accPercentage - (accPercentage * 2);
                                }
                                else
                                {
                                    estObj.Accuracy = accPercentage;
                                }

                                estimatesList.Add(estObj);

                            }

                        } // END inner ForEach
                    }

                } // END outer ForEach

                // Bind to datagrid
                EstimatesTable.ItemsSource = estimatesList;

            } // END If

        } // END SprintCB_SelectionChanged()




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


    } // END SprintPage class

} // END namespace