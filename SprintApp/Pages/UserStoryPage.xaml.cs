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
    /// Interaction logic for UserStoryPage.xaml
    /// </summary>
    public partial class UserStoryPage : Page
    {

        /*************************
        *      CONSTRUCTOR       *
        * ***********************/
        public UserStoryPage()
        {
            InitializeComponent();
        }



        /*************************
        *        METHODS         *
        *************************/

         /*
         * Purpose: Toggles between selected page.
         */
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            switch (e.NewValue)
            {
                case 0:
                    userStoryFrame.Navigate(new AddUserStoryPage());
                    break;

                case 1:
                    userStoryFrame.Navigate(new UpdateUserStoryPage());
                    break;

                default:
                    break;
            }

        } // END Slider_ValueChanged()



    } // END UserStoryPage

} // END namespace