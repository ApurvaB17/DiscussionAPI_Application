using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DiscussionAPI_Application
{
    public class CommonClass
    {
        public enum parentTypes
        {
            Discussion,
            Reply
        }

        public string GetConnectionString(IConfiguration configuration)
        {
            var exepath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            string rootPath = new FileInfo(exepath).DirectoryName;
            if (rootPath.Contains("DiscussionsAPI_UnitTest"))
            {
                rootPath = rootPath.Remove(rootPath.Length - 40);
            }
            else
            {
                rootPath = rootPath.Remove(rootPath.Length - 25);
            }

            string sqlDataSource = configuration.GetConnectionString("DiscussionAPIAppCon");
            if (sqlDataSource.Contains("%CONTENTROOTPATH%"))
            {
                sqlDataSource = sqlDataSource.Replace("%CONTENTROOTPATH%", rootPath);
            }

            return sqlDataSource;
        }

    }
}
