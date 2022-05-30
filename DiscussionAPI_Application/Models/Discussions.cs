using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionAPI_Application.Models
{
    public class Discussions
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string userId { get; set; }
    }
}
