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
    /// Interaction logic for ProjectPage.xaml
    /// </summary>
    public partial class ProjectPage : Page
    {

        /*************************
        *      CONSTRUCTOR       *
        * ***********************/
        public ProjectPage()
        {
            InitializeComponent();

            // Set button color of page currently on
            addBtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF1582A2"));

        }



        /*************************
        *        METHODS         *
        * ***********************/

        /*
         * Purpose: Checks which list menu item was clicked and
         *          navigates to selected WPF page.
         */
        private void SwitchTab_Click(object sender, RoutedEventArgs e)
        {
            SetButtonColor(sender);

            switch ((sender as Button).Content.ToString())
            {
                case "Add":
                    projectFrame.Navigate(new AddProjectPage());
                    break;

                case "Update":
                    projectFrame.Navigate(new UpdateProjectPage());
                    break;

                default:
                    break;
            }

        } // END SwitchTab_Click()




        /*************************
        *     HELPER METHODS     *
        *************************/

        private void SetButtonColor(object sender)
        {
            // Switch color to display currently selected page
            if ((sender as Button).Content.ToString() == "Add")
            {
                addBtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF1582A2"));
                updateBtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF21C3F3"));
            }
            else if ((sender as Button).Content.ToString() == "Update")
            {
                addBtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF21C3F3"));
                updateBtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF1582A2"));
            }

        } // END SetButtonColor()


    } // END ProjectPage class

} // END namespace