using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mySite.DomainModel.Entities;

namespace mySite.DomainModel.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        ICollection<Student> GetStudentsStudienOnUniversity(string university);

        void Save();
    }
}
