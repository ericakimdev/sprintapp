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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private Member loginMember = new Member();
        public Member LoginMember
        {
            get { return loginMember; }
            set { loginMember = value; }
        }

        /*************************
        *      CONSTRUCTOR       *
        * ***********************/
        public HomePage()
        {
            InitializeComponent();

            loginMember = (Member)Application.Current.Properties["loginMember"];

            totalProjectNumText.Text = CountProjects().ToString();

            totalBacklogNumText.Text = CountBacklogs().ToString();

            memberNumText.Text = CountMembers().ToString();

            SetProgressBar();
        }

        /*************************
        *     HELPER METHODS     *
        *************************/

        private long CountProjects()
        {
            long projectNumber = DBConnection.GetCollectionCount<Project>("projects");

            return projectNumber;
        }

        private long CountBacklogs()
        {
            long backlogNumber = DBConnection.GetCollectionCount<Backlog>("backlogs");

            return backlogNumber;
        }

        private long CountMembers()
        {
            long memberNumber = DBConnection.GetCollectionCount<Member>("members");

            return memberNumber;
        }

        //Get project name and calculate time accuracy to set Progress bar
        private void SetProgressBar()
        {
            string proOneName = "---";
            double proOneAcc = 0;

            string proTwoName = "---";
            double proTwoAcc = 0;

            string proThreeName = "---";       
            double proThreeAcc = 0;

            List<Project> projectsByMember = new List<Project>();
            projectsByMember = GetProjectsByMember(loginMember.name);

            DevEstimate devEst0 = new DevEstimate();
            DevEstimate devEst1 = new DevEstimate(); 
            DevEstimate devEst2 = new DevEstimate(); 
            if (projectsByMember != null)
            {
                proOneName = projectsByMember[0].ProjectName;
                devEst0 = GetDevEstimate(projectsByMember[0].ProjectId);
                proOneAcc = CalculateAccuracy(devEst0);

                if(projectsByMember.Count == 2)
                {
                    proTwoName = projectsByMember[1].ProjectName;
                    devEst1 = GetDevEstimate(projectsByMember[1].ProjectId);
                    proTwoAcc = CalculateAccuracy(devEst1);
                }

                if (projectsByMember.Count > 2)
                {
                    proTwoName = projectsByMember[1].ProjectName;
                    devEst1 = GetDevEstimate(projectsByMember[1].ProjectId);
                    proTwoAcc = CalculateAccuracy(devEst1);

                    proThreeName = projectsByMember[2].ProjectName;
                    devEst2 = GetDevEstimate(projectsByMember[2].ProjectId);
                    proThreeAcc = CalculateAccuracy(devEst2);
                }
            }

            proOneNameText.Text = proOneName;
            proOnePercenText.Text = proOneAcc.ToString() + " %";
            proOneProgress.Value = proOneAcc;

            proTwoNameText.Text = proTwoName;
            proTwoPercenText.Text = proTwoAcc.ToString() + " %";
            proTwoProgress.Value = proTwoAcc;

            proThreeNameText.Text = proThreeName;
            proThreePercenText.Text = proThreeAcc.ToString() + " %";
            proThreeProgress.Value = proThreeAcc;
        }

        private double CalculateAccuracy(DevEstimate estObj)
        {
            if(estObj.ActualTime == 0 || estObj.TotalEstimate == 0)
            {
                return 0;
            }

            double accPercentage = Math.Round(estObj.ActualTime / estObj.TotalEstimate, 2) * 100;
            double acc = 0;

            if (accPercentage > 100)
            {
                acc = accPercentage - (accPercentage * 2);
            }
            else
            {
                acc = accPercentage;
            }
            return acc;
        }        

        private List<Project> GetProjectsByMember(string memberName)
        {
            List<Project> projectsByMember = new List<Project>();

            var projectList = DBConnection.GetCollectionAsList<Project>("projects");

            foreach(var pro in projectList)
            {
                foreach(var proMember in pro.Members)
                {
                    if(proMember == memberName)
                    {
                        projectsByMember.Add(pro);
                    }
                }
            }
            return projectsByMember;
        }

        //Get developer estimate time per project
        private DevEstimate GetDevEstimate(long selectedProjectId)
        {
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

            foreach (var backlog in selectedProjectBacklogs)
            {
                foreach (var sub in backlog.Subtasks)
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

            DevEstimate estObj = new DevEstimate();
            foreach (var item in estimatesDic)
            {
                // Transfer dictionary data to object and add to List
                if(item.Key == loginMember.name)
                {
                    estObj.Name = item.Key;
                    estObj.TotalEstimate = item.Value;
                    estObj.ActualTime = actualDic[item.Key];
                }
            }
            return estObj;
        }

    } // END HomePage class

} // END namespace