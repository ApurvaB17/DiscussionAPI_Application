using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DiscussionAPI_Application.CommonClass;

namespace DiscussionAPI_Application.Models
{
    public class Replies
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string userId { get; set; }
        public int? discussionId { get; set; }
        public int? parentId { get; set; }
        public parentTypes? parentType { get; set; }
    }
}
