using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintApp.Models
{
    // Used as ViewModel for DataGrid control
    public class Estimate
    {

        /*************************
        *       PROPERTIES       *
        * ***********************/
        public string UserStory { get; set; }
        public string Name { get; set; }
        public double EstimatedTime { get; set; }
        public double ActualTime { get; set; }
        public double Accuracy { get; set; }


        /*************************
        *      CONSTRUCTOR       *
        * ***********************/
        public Estimate()
        {

        }


    } // END Estimate class

} // END namespace