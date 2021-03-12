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
    /// Interaction logic for DevPage.xaml
    /// </summary>
    public partial class DevPage : Page
    {

        /*************************
        *      CONSTRUCTOR       *
        * ***********************/
        public DevPage()
        {
            InitializeComponent();

            FillProjectsComboBox();
        }



        /*************************
        *        METHODS         *
        *************************/

        private void ProjectCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem != null)
            {
                string currentSelection = (sender as ComboBox).SelectedItem.ToString();

                // Get select project info
                var projectList = DBConnection.GetCollectionAsList<Project>("projects");
                var selectedProject = projectList.Find(b => b.ProjectName == currentSelection);
                var selectedProjectId = selectedProject.ProjectId;

                //Get backlog and estimates time and actual time
                var backlogList = DBConnection.GetCollectionAsList<Backlog>("backlogs");
                var selectedProjectBacklogs = backlogList.FindAll(b => b.ProjectId == selectedProjectId);

                //To store mamber name and total estimates per each member
                Dictionary<string, double> estimatesDic = new Dictionary<string, double>();
                Dictionary<string, double> actualDic = new Dictionary<string, double>();

                string memberNameKey;
                double tempEstimates = 0;
                double tempActual = 0;
                double estValue = 0;
                double actualValue = 0;
                List<DevEstimate> estimatesList = new List<DevEstimate>();


                foreach (var backlog in selectedProjectBacklogs)
                {
                    foreach(var sub in backlog.Subtasks)
                    {
                        memberNameKey = sub[1];
                        estValue = double.Parse(sub[2]);

                        if (sub[3] != "")
                        {
                            actualValue = double.Parse(sub[3]);
                        }
                        else
                        {
                            actualValue = 0;
                        }

                        //total estimates time
                        if (estimatesDic.ContainsKey(memberNameKey))
                        {
                            tempEstimates = estimatesDic[memberNameKey];
                            estimatesDic[memberNameKey] = tempEstimates + estValue;
                        }
                        else
                        {
                            estimatesDic[memberNameKey] = estValue;
                        }

                        //total actual time
                        if (actualDic.ContainsKey(memberNameKey))
                        {
                            tempActual = actualDic[memberNameKey];
                            actualDic[memberNameKey] = tempActual + actualValue;
                        }
                        else
                        {
                            actualDic[memberNameKey] = actualValue;
                        }

                    } // END inner ForEach
                    
                } // END outer ForEach


                foreach (var item in estimatesDic)
                {
                    // Transfer dictionary data to object and add to List
                    DevEstimate estObj = new DevEstimate();
                    estObj.Name = item.Key;
                    estObj.TotalEstimate = item.Value;
                    estObj.ActualTime = actualDic[item.Key];

                    estimatesList.Add(estObj);
                }

                // Bind list to DataGrid
                EstimatesTable.ItemsSource = estimatesList;

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



    } // END DevPage class

} // END namespace