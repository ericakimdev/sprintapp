using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintApp.Models
{
    // Used as ViewModel for DataGrid control
    public class DevEstimate
    {

        /*************************
        *       PROPERTIES       *
        * ***********************/
        public string Name { get; set; }
        public double TotalEstimate { get; set; }
        public double ActualTime { get; set; }



        /*************************
        *      CONSTRUCTOR       *
        * ***********************/
        public DevEstimate()
        {

        }


    } // END DevEstimate class

} // END namespace