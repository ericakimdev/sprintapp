using MongoDB.Bson;
using MongoDB.Driver;
using SprintApp.Pages;
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

namespace SprintApp
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {

        private Member loginMember = new Member();


        /*************************
        *      CONSTRUCTOR       *
        * ***********************/
        public HomeWindow()
        {
            InitializeComponent();

            loginMember = (Member)Application.Current.Properties["loginMember"];
            loginNameText.Text = loginMember.name;
        }



        /*************************
        *        METHODS         *
        * ***********************/

        /*
         * Purpose: Checks which list menu item was clicked and
         *          navigates to selected WPF page.
         */
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    iFrame.Navigate(new HomePage());
                    break;

                case "ItemReg":
                    iFrame.Navigate(new RegisterPage());
                    break;

                case "ItemProject":
                    iFrame.Navigate(new ProjectPage());
                    break;

                case "ItemBacklog":
                    iFrame.Navigate(new BacklogPage());
                    break;

                case "ItemSprint":
                    iFrame.Navigate(new SprintPage());
                    break;

                case "ItemDev":
                    iFrame.Navigate(new DevPage());
                    break;

                default:
                    break;
            }

        } // END ListViewMenu_SelectionChanged()



        /*
         * Purpose: Closes application.
         */
        private void ButtonPopupLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        } // END ButtonPopupLogout_Click()



        /*
         * Purpose: Navigate account page.
         */
        private void Account_Click(object sender, RoutedEventArgs e)
        {
            iFrame.Navigate(new AccountPage());
        }



        /*
         * Purpose: Open help window.
         */
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.Show();
        }



        /*
         * Purpose: Allows user to drag application
         *          by holding down on title bar.
         */
        private void DragWindow(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }

        } // END DragWindow()



    } // END HomeWindow class

} // END namespace