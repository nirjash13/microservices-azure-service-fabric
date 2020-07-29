using System;
using System.Collections.Generic;

namespace Student.API
{
  public partial class Student
  {
    public Student()
    {
      Course = new HashSet<Course>();
    }

    public Guid RepoId { get; set; }
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Status { get; set; }

    public virtual ICollection<Course> Course { get; set; }
  }
}
