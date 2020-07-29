using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Student.API
{
  public interface IStudentRepository
  {
    Task<IEnumerable<Student>> GetStudentsAsync();
    Task<Student> GetStudentByIdAsync(Guid id);
    Task<IEnumerable<Student>> GetStudentsByIdAsync(List<Guid> ids);
    Task<Student> CreateStudentAsync(Student student);
    Task<Student> UpdateStudentAsync(Guid id, Student student);
    Task DeleteStudentAsync(Guid id);

  }
}
