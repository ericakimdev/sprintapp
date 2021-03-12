using System;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SprintApp.Models
{
    // Backlog class for use with MongoDB
    public class Backlog
    {

        /*************************
        *       PROPERTIES       *
        * ***********************/
        public ObjectId _id { get; set; }
        public int ProjectId { get; set; }
        public string SprintName { get; set; }
        public string UserStory { get; set; }
        public List<List<string>> Subtasks { get; set; }
        public double RelativeEstimates { get; set; }
        public int Priority { get; set; }
        public double ReEstimate { get; set; }
        public string Status { get; set; }
        public List<string> Notes { get; set; }



        /*************************
        *      CONSTRUCTORS      *
        * ***********************/

        public Backlog()
        {
            Subtasks = new List<List<string>>();
            Notes = new List<string>();
        }


        public Backlog(int _projectid, string _sprintname, string _userstory, List<List<string>> _subtasks, double _relativeestimates, double _reestimate, int _priority, string _status, List<string> _notes)
        {
            ProjectId = _projectid;
            SprintName = _sprintname;
            UserStory = _userstory;
            Subtasks = _subtasks;
            RelativeEstimates = _relativeestimates;
            ReEstimate = _reestimate;
            Priority = _priority;
            Status = _status;
            Notes = _notes;
        }


    } // END Backlog class

} // END namespace