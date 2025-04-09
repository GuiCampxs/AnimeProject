using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeProject.Application.Dtos
{
    public class AnimeRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Genre { get; set; }
    }
}
