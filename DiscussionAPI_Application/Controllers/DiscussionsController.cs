using System;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DiscussionAPI_Application.Models;
using Newtonsoft.Json;

namespace DiscussionAPI_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DiscussionsController(IConfiguration configuration)
        {

            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                string query = @"select * from dbo.Discussions";
                DataTable table = new DataTable();

                CommonClass commonClass = new CommonClass();
                string sqlDataSource = commonClass.GetConnectionString(_configuration);

                SqlDataReader dataReader;
                using (SqlConnection conn = new SqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        dataReader = command.ExecuteReader();
                        table.Load(dataReader);
                        dataReader.Close();
                        conn.Close();
                    }
                }

                return new JsonResult(table);
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }

        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            try
            {
                string query = @"select * from dbo.Discussions where Id=@Id";
                DataTable table = new DataTable();

                CommonClass commonClass = new CommonClass();
                string sqlDataSource = commonClass.GetConnectionString(_configuration);

                SqlDataReader dataReader;
                using (SqlConnection conn = new SqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        dataReader = command.ExecuteReader();
                        table.Load(dataReader);
                        dataReader.Close();
                        conn.Close();
                    }
                }

                return new JsonResult(table);
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }


        }

        [HttpGet("{discussionId}/replies")]
        public JsonResult GetReplies(int discussionId)
        {
            try
            {
                string query = @"select * from dbo.Replies where Discussion_Id=@Discussion_Id";
                DataTable table = new DataTable();

                CommonClass commonClass = new CommonClass();
                string sqlDataSource = commonClass.GetConnectionString(_configuration);

                SqlDataReader dataReader;
                using (SqlConnection conn = new SqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Discussion_Id", discussionId);
                        dataReader = command.ExecuteReader();
                        table.Load(dataReader);
                        dataReader.Close();
                        conn.Close();
                    }
                }
                return new JsonResult(table);
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }

        }

        [HttpPost]
        public JsonResult Post(Discussions discussion)
        {
            try
            {
                if (discussion.Question == null || discussion.userId == null)
                {
                    if (discussion.Question == null)
                    {
                        return new JsonResult("What is your question?");
                    }

                    if (discussion.userId == null)
                    {
                        return new JsonResult("Please provide your name or id");
                    }
                }

                string query = @"insert into dbo.Discussions
                            values (@Question,@User_Id)";
                DataTable table = new DataTable();

                CommonClass commonClass = new CommonClass();
                string sqlDataSource = commonClass.GetConnectionString(_configuration);

                SqlDataReader dataReader;
                using (SqlConnection conn = new SqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Question", discussion.Question);
                        command.Parameters.AddWithValue("@User_Id", discussion.userId);
                        dataReader = command.ExecuteReader();

                        table.Load(dataReader);
                        dataReader.Close();
                        conn.Close();
                    }
                }

                return new JsonResult("Added successfully!");
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }

        }
    }
}
            