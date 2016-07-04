using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySite.DomainModel.Entities
{
    public class Student : Entity
    {
        public Student()
        {

        }

        public Student(string createdBy)
            :base(createdBy)
        {

        }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string University { get; set; }
    }
}
