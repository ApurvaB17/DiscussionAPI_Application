using DiscussionAPI_Application.Controllers;
using System;
using System.IO;
using Castle.Core.Configuration;
using DiscussionAPI_Application;
using DiscussionAPI_Application.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Xunit;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Newtonsoft.Json;

namespace DiscussionsAPI_UnitTest
{
    public class RepliesControllerTest
    {
        private readonly IConfiguration config;

        public RepliesControllerTest()
        {
            config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile(@"appsettings.json",false,false).AddEnvironmentVariables()
                .Build();
        }
        [Fact]
        public void GetAllReplies()
        {

            var controller = new RepliesController(config);
            var result = controller.Get();
            
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(result.Value);
            Assert.Equal("[{\"Id\":1,\"Content\":\"It is going great, Thanks!\",\"User_Id\":\"stud_123  \",\"Discussion_Id\":1,\"Parent_id\":1,\"Parent_type\":\"Discussion\"}," +
                         "{\"Id\":2,\"Content\":\"Unfortunately, I had a bad day!\",\"User_Id\":\"stud_234  \",\"Discussion_Id\":1,\"Parent_id\":1,\"Parent_type\":\"Discussion\"}," +
                         "{\"Id\":3,\"Content\":\"Oh no! Sorry to hear that. What happened?\",\"User_Id\":\"stud_345  \",\"Discussion_Id\":1,\"Parent_id\":2,\"Parent_type\":\"Reply     \"}," +
                         "{\"Id\":8,\"Content\":\"I want to learn Maths!\",\"User_Id\":\"stud_123\",\"Discussion_Id\":2,\"Parent_id\":2,\"Parent_type\":\"Discussion\"}," +
                         "{\"Id\":9,\"Content\":\"I love Maths. I would like that too!\",\"User_Id\":\"stud_234\",\"Discussion_Id\":2,\"Parent_id\":8,\"Parent_type\":\"Reply     \"}," +
                         "{\"Id\":11,\"Content\":\"Vacation was amazing\",\"User_Id\":\"stud_234\",\"Discussion_Id\":5,\"Parent_id\":5,\"Parent_type\":\"Discussion\"}]", JSONString);
        }

        [Fact]
        public void GetAllReplies_DiscussionIdBased()
        {

            var controller = new RepliesController(config);
            var result = controller.Get(2);

            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(result.Value);
            Assert.Equal("[{\"Id\":8,\"Content\":\"I want to learn Maths!\",\"User_Id\":\"stud_123\",\"Discussion_Id\":2,\"Parent_id\":2,\"Parent_type\":\"Discussion\"}," +
                    "{\"Id\":9,\"Content\":\"I love Maths. I would like that too!\",\"User_Id\":\"stud_234\",\"Discussion_Id\":2,\"Parent_id\":8,\"Parent_type\":\"Reply     \"}]", JSONString);
        }

        [Fact]
        public void PostANewReply()
        {

            var controller = new RepliesController(config);
            Replies replies = new Replies();
            replies.Content = "Vacation was amazing";
            replies.userId = "stud_234";
            replies.discussionId = 5;
            replies.parentId = 5;
            replies.parentType = CommonClass.parentTypes.Discussion;
            string JSONString = string.Empty;
            var result = controller.Post(replies);
            JSONString = JsonConvert.SerializeObject(result.Value);
            Assert.Equal("\"Added successfully!\"", JSONString);
        }
        [Fact]
        public void PostANewDiscussion_NullParentType()
        {

            var controller = new RepliesController(config);
            Replies replies = new Replies();
            replies.Content = "Vacation was amazing";
            replies.userId = "stud_234";
            replies.discussionId = 5;
            replies.parentId = 5;
            string JSONString = string.Empty;
            var result = controller.Post(replies);
            JSONString = JsonConvert.SerializeObject(result.Value);
            Assert.Equal("\"Are you replying to a discussion or a reply? Please provide the response as Discussion or Reply\"", JSONString);
        }
    }
}
