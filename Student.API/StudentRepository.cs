using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student.API
{
  public class StudentRepository : IStudentRepository
  {
    public async Task<Student> CreateStudentAsync(Student student)
    {
      try
      {
        using (var context = new SchoolManagementContext())
        {
          context.Student.Add(student);

          await context.SaveChangesAsync();

          return student;
          
        }
      }

      catch (Exception)
      {
        throw;
      }
    }

    public async Task DeleteStudentAsync(Guid id)
    {
      try
      {
        using (var context = new SchoolManagementContext())
        {
          var student = context.Student.Where(s => s.RepoId == id).SingleOrDefault();
          if(student == null)
          {
            throw new Exception("Student does not exist");
          }

          context.Student.Remove(student);

          await context.SaveChangesAsync();
        }
      }

      catch (Exception)
      {
        throw;
      }
    }

    public async Task<Student> GetStudentByIdAsync(Guid id)
    {
      try
      {
        using (var context = new SchoolManagementContext())
        {
          var student = context.Student.AsNoTracking().Where(s => s.RepoId == id).SingleOrDefault();
          if (student == null)
          {
            throw new Exception("Student does not exist");
          }

          return student;

        }
      }

      catch (Exception)
      {
        throw;
      }
    }

    public async Task<IEnumerable<Student>> GetStudentsAsync()
    {
      try
      {
        using (var context = new SchoolManagementContext())
        {
          var students = context.Student.AsNoTracking();
          if (students == null)
          {
            throw new Exception("Student does not exist");
          }

          return students;

        }
      }

      catch (Exception)
      {
        throw;
      }
    }

    public async Task<IEnumerable<Student>> GetStudentsByIdAsync(List<Guid> ids)
    {
      try
      {
        using (var context = new SchoolManagementContext())
        {
          var students = context.Student.AsNoTracking().Where(s=> ids.Contains(s.RepoId));
          if (students == null)
          {
            throw new Exception("Student does not exist");
          }

          return students;

        }
      }

      catch (Exception)
      {
        throw;
      }
    }

    public async Task<Student> UpdateStudentAsync(Guid id, Student student)
    {
      if(id == Guid.Empty)
      {
        throw new Exception("Student id can not be null");
      }

      try
      {
        using (var context = new SchoolManagementContext())
        {
          var existingStudent = context.Student.Where(s => s.RepoId == id).SingleOrDefault();
          if (existingStudent == null)
          {
            throw new Exception("Student does not exist");
          }

          student.RepoId = id;
          existingStudent = student;

          await context.SaveChangesAsync();

          return existingStudent;
        }
      }

      catch (Exception)
      {
        throw;
      }
    }
  }
}
