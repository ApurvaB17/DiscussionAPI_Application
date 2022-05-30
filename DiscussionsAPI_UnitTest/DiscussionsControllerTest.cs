using DiscussionAPI_Application.Controllers;
using System;
using System.IO;
using Castle.Core.Configuration;
using DiscussionAPI_Application.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Xunit;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Newtonsoft.Json;

namespace DiscussionsAPI_UnitTest
{
    public class DiscussionsControllerTest
    {
        private readonly IConfiguration config;

        public DiscussionsControllerTest()
        {
            config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile(@"appsettings.json",false,false).AddEnvironmentVariables()
                .Build();
        }
        [Fact]
        public void GetAllDiscussions()
        {

            var controller = new DiscussionsController(config);
            var result = controller.Get();
            
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(result.Value);
            Assert.Equal("[{\"Id\":1,\"Question\":\"How is your day going so far?\",\"User_Id\":\"Prof_123\"},{\"Id\":2,\"Question\":\"What do you want to learn today?\",\"User_Id\":\"prof_123\"}," +
                         "{\"Id\":5,\"Question\":\"How was your vacation?\",\"User_Id\":\"Prof_234\"}]", JSONString);
        }

        [Fact]
        public void GetAllDiscussions_IdBased()
        {

            var controller = new DiscussionsController(config);
            var result = controller.Get(2);

            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(result.Value);
            Assert.Equal("[{\"Id\":2,\"Question\":\"What do you want to learn today?\",\"User_Id\":\"prof_123\"}]", JSONString);
        }

        [Fact]
        public void PostANewDiscussion()
        {

            var controller = new DiscussionsController(config);
            Discussions discussions = new Discussions();
            discussions.Question = "How was your vacation?";
            discussions.userId = "Prof_234";
            string JSONString = string.Empty;
            var result = controller.Post(discussions);
            JSONString = JsonConvert.SerializeObject(result.Value);
            Assert.Equal("\"Added successfully!\"", JSONString);
        }
        [Fact]
        public void PostANewDiscussion_NullQuestion()
        {

            var controller = new DiscussionsController(config);
            Discussions discussions = new Discussions();
            discussions.Question = null;
            discussions.userId = "Prof_234";
            string JSONString = string.Empty;
            var result = controller.Post(discussions);
            JSONString = JsonConvert.SerializeObject(result.Value);
            Assert.Equal("\"What is your question?\"", JSONString);
        }

        [Fact]
        public void GetAllReplies_DiscussionIdBased()
        {

            var controller = new DiscussionsController(config);
            var result = controller.GetReplies(2);

            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(result.Value);
            Assert.Equal("[{\"Id\":8,\"Content\":\"I want to learn Maths!\",\"User_Id\":\"stud_123\",\"Discussion_Id\":2,\"Parent_id\":2,\"Parent_type\":\"Discussion\"}," +
                         "{\"Id\":9,\"Content\":\"I love Maths. I would like that too!\",\"User_Id\":\"stud_234\",\"Discussion_Id\":2,\"Parent_id\":8,\"Parent_type\":\"Reply     \"}," +
                         "{\"Id\":1011,\"Content\":\"I love Science. I want to learn that!\",\"User_Id\":\"stud_345\",\"Discussion_Id\":2,\"Parent_id\":8,\"Parent_type\":\"Reply     \"}," +
                         "{\"Id\":1014,\"Content\":\"I love Science. I want to learn that!\",\"User_Id\":\"stud_345\",\"Discussion_Id\":2,\"Parent_id\":8,\"Parent_type\":\"reply     \"}]", JSONString);
        }
    }
}
