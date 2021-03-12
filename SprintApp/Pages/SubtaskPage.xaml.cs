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
    /// Interaction logic for SubtaskPage.xaml
    /// </summary>
    public partial class SubtaskPage : Page
    {

        /*************************
        *      CONSTRUCTOR       *
        * ***********************/
        public SubtaskPage()
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
                    subtaskFrame.Navigate(new AddSubtaskPage());
                    break;

                case 1:
                    subtaskFrame.Navigate(new UpdateSubtaskPage());
                    break;

                default:
                    break;
            }

        } // END Slider_ValueChanged()


    } // END SubtaskPage class

} // END namespace