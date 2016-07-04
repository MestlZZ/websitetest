using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using mySite.DomainModel.Entities;
using mySite.DomainModel.Repositories;

namespace mySite.DataAccess.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public void Save()
        {
            _dataContext.Save();
        }

        public ICollection<Student> GetStudentsStudienOnUniversity(string university)
        {
            return GetCollection(student => student.University == university);
        }
    }
}
