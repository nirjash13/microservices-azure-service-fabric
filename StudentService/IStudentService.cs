using Microsoft.ServiceFabric.Services.Remoting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentService
{
  public interface IStudentService : IService
  {
    Task<IEnumerable<JObject>> GetStudentsAsync();
    Task<JObject> GetStudentByIdAsync(Guid id);
    Task<IEnumerable<JObject>> GetStudentsByIdAsync(List<Guid> ids);
    Task<JObject> CreateStudentAsync(JObject student);
    Task<JObject> UpdateStudentAsync(Guid id, JObject student);
    Task DeleteStudentAsync(Guid id);
  }
}
