using System;
using System.Collections.Generic;

namespace Student.API
{
    public partial class Course
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }

        public virtual Student Student { get; set; }
    }
}
