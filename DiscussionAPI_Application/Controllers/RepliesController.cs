using System;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DiscussionAPI_Application.Models;

namespace DiscussionAPI_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RepliesController(IConfiguration configuration)
        {

            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                string query = @"select * from dbo.Replies";
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
                string query = @"select * from dbo.Replies where Id=@Id";
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

        [HttpPost]
        public JsonResult Post(Replies reply)
        {
            try
            {
                if (reply.discussionId == null || reply.parentId == null || reply.parentType == null)
                {
                    if (reply.discussionId == null)
                    {
                        return new JsonResult("Which discussion are you responding to? Please provide the id");
                    }
                    if (reply.parentId == null)
                    {
                        return new JsonResult("Are you responding to a discussion or a reply? Please provide the id");
                    }
                    if (reply.parentType == null)
                    {
                        return new JsonResult("Are you replying to a discussion or a reply? Please provide the response as Discussion or Reply");
                    }
                }
                string query = @"insert into dbo.Replies 
                            values(@Content,@User_Id,@Discussion_Id,@Parent_id,@Parent_type)";
                DataTable table = new DataTable();

                CommonClass commonClass = new CommonClass();
                string sqlDataSource = commonClass.GetConnectionString(_configuration);

                SqlDataReader dataReader;
                using (SqlConnection conn = new SqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Content", reply.Content);
                        command.Parameters.AddWithValue("@User_Id", reply.userId);
                        command.Parameters.AddWithValue("@Discussion_Id", reply.discussionId);
                        command.Parameters.AddWithValue("@Parent_id", reply.parentId);
                        command.Parameters.AddWithValue("@Parent_type", reply.parentType.ToString().ToLower());
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
