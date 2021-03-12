using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintApp.Models
{
    // Member class for use with MongoDB
    public class Member
    {

        /*************************
        *       PROPERTIES       *
        * ***********************/
        public ObjectId _id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }


        /*************************
        *      CONSTRUCTORS      *
        * ***********************/

        public Member() { }


        public Member(string _name, string _password, string _email)
        {
            name = _name;
            password = _password;
            email = _email;
        }


    } // END Member class

} // END namespace