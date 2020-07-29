using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Newtonsoft.Json.Linq;
using StudentService;
using System;
using System.Configuration;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolManagement.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StudentController : ControllerBase
  {
    private readonly IStudentService studentService;
    public StudentController()
    {
      this.studentService = GetStudentService();
    }
    // GET: api/<StudentController>
    [HttpGet]
    public async Task<ActionResult> Get()
    {
      var students = await this.studentService.GetStudentsAsync();

      return Ok(students);
           
    }

    // GET api/<StudentController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
      var student = await this.studentService.GetStudentByIdAsync(id);

      return Ok(student);
    }

    // POST api/<StudentController>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] JObject student)
    {
      var result = await this.studentService.CreateStudentAsync(student);

      return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
    }

    // PUT api/<StudentController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] JObject student)
    {
      var existingObject = await this.studentService.GetStudentByIdAsync(id);

      if(existingObject == null)
      {
        return NotFound();
      }

      var result = await this.studentService.UpdateStudentAsync(id, student);

      return Ok(result);
    }

    // DELETE api/<StudentController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
      var existingObject = await this.studentService.GetStudentByIdAsync(id);

      if (existingObject == null)
      {
        return NotFound();
      }

      await this.studentService.DeleteStudentAsync(id);

      return NoContent();
    }

    #region Helpers

    public static IStudentService GetStudentService()
    {
      var appSettings = ConfigurationManager.AppSettings;
      if (appSettings.Count == 0)
      {
        throw new Exception("AppSettings is empty.");
      }

      var uriString = appSettings.Get("StudentService.Uri");
      var listener = appSettings.Get("StudentService.Listener");

      if(string.IsNullOrWhiteSpace(uriString) || string.IsNullOrWhiteSpace(listener))
      {
        throw new Exception("uriString or listener is empty.");
      }

      var uri = new Uri("fabric:/ServiceFabricMicroservices/StudentService");
      return ServiceProxy.Create<IStudentService>(uri, listenerName: "StudentService_V1");
    }

    #endregion
  }
}
