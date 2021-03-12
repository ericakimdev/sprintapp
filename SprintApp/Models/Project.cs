using System;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintApp.Models
{
    // Project class for use with MongoDB
    public class Project
    {

        /*************************
        *       PROPERTIES       *
        * ***********************/
        public ObjectId _id { get; set; }
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Manager { get; set; }
        public string Client { get; set; }
        public List<string> Members { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }



        /*************************
        *      CONSTRUCTORS      *
        * ***********************/

        public Project()
        {
            Members = new List<string>();
        }


        public Project(int _projectid, string _projectname, string _manager, string _client,
                       List<string> _members, DateTime _startDate, DateTime _dueDate)
        {
            ProjectId = _projectid;
            ProjectName = _projectname;
            Manager = _manager;
            Client = _client;
            Members = _members;
            StartDate = _startDate;
            DueDate = _dueDate;
        }


    } // END Project class

} // END namespace