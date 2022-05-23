using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class Teacher : ITeacher
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
