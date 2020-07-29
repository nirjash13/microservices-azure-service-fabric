using Newtonsoft.Json.Linq;
using Student.API;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentService
{
  public class StudentServiceProvider : IStudentService
  {
    #region Fields
    
    private readonly IStudentRepository studentRepository; 

    #endregion

    #region Constructor

    public StudentServiceProvider(IStudentRepository studentRepository)
    {
      this.studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
    } 

    #endregion

    #region IStudentService

    public async Task<JObject> CreateStudentAsync(JObject student)
    {
      if (student == null)
      {
        throw new ArgumentNullException(nameof(student));
      }

      try
      {
        var studentObject = student.ToObject<Student.API.Student>();
        var result = await this.studentRepository.CreateStudentAsync(studentObject);

        return JObject.FromObject(result);

      }
      catch (Exception)
      {

        throw;
      }

    }

    public async Task DeleteStudentAsync(Guid id)
    {
      if (id == Guid.Empty)
      {
        throw new ArgumentNullException(nameof(id));
      }

      await this.studentRepository.DeleteStudentAsync(id);

    }

    public async Task<JObject> GetStudentByIdAsync(Guid id)
    {
      if (id == Guid.Empty)
      {
        throw new ArgumentNullException(nameof(id));
      }

      try
      {
        var student = await this.studentRepository.GetStudentByIdAsync(id);

        var jObject = JObject.FromObject(student);

        return jObject;
      }
      catch (Exception)
      {

        throw;
      }

    }

    public async Task<IEnumerable<JObject>> GetStudentsAsync()
    {
      try
      {
        var students = await this.studentRepository.GetStudentsAsync();

        var studentList = new List<JObject>();

        foreach (var student in students)
        {
          studentList.Add(JObject.FromObject(student));
        }

        return studentList;
      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<IEnumerable<JObject>> GetStudentsByIdAsync(List<Guid> ids)
    {
      var studentList = new List<JObject>();
      if (ids.Count == 0)
      {
        return studentList;
      }
      try
      {
        var students = await this.studentRepository.GetStudentsByIdAsync(ids);

        foreach (var student in students)
        {
          studentList.Add(JObject.FromObject(student));
        }

        return studentList;
      }
      catch (Exception)
      {

        throw;
      }
    }

    public async Task<JObject> UpdateStudentAsync(Guid id, JObject student)
    {
      if (student == null)
      {
        throw new ArgumentNullException(nameof(student));
      }

      try
      {
        var studentObject = student.ToObject<Student.API.Student>();
        var result = await this.studentRepository.UpdateStudentAsync(id, studentObject);
        return JObject.FromObject(result);
      }
      catch (Exception)
      {

        throw;
      }
    } 

    #endregion
  }
}
