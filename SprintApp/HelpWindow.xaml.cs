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
using System.Windows.Shapes;

namespace SprintApp
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();
        }

        /*************************
        *        METHODS         *
        *************************/

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        } // END Close_Click()

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
    }
}
