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
    /// Interaction logic for BacklogPage.xaml
    /// </summary>
    public partial class BacklogPage : Page
    {

        /*************************
        *      CONSTRUCTOR       *
        * ***********************/
        public BacklogPage()
        {
            InitializeComponent();
        }



        /*************************
        *        METHODS         *
        *************************/

         /*
         * Purpose: Checks which list menu item was clicked and
         *          navigates to selected WPF page.
         */
        private void SwitchTab_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Content.ToString())
            {
                case "User Stories":
                    backlogFrame.Navigate(new UserStoryPage());
                    break;

                case "Subtasks":
                    backlogFrame.Navigate(new SubtaskPage());
                    break;

                default:
                    break;
            }

        } // END SwitchTab_Click()



    } // END BacklogPage class

} // END namespace