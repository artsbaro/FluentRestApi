using Dapper;
using HttpRestApi.Log.Models;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace HttpRestApi.Log.Data.Repository
{
    public class SqlRepository : RepositoryBase
    {
        /*
         * 
         * Adicioniar na Startup
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
             */


        /*

           "ConnectionStrings": {
                "DefaultConnection": "Data Source=SeuIP;Initial Catalog=SeuDB;Integrated Security=True"
                //"DefaultConnection": "Data Source=SeuIP;Initial Catalog=SeuDB;Integrated Security=False;User ID=sa;Password=SeuPassWord;"
            }
         */

        public SqlRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public void Insert(RequestPayload entity)
        {
            Connection.Execute(
                "SProc_RequestPayload_Insert",
                commandType: CommandType.StoredProcedure,
                param: new
                {
                    entity.Id
                    ,entity.Uri
                    ,entity.HttpVerb
                    ,entity.RequestHeaders
                    ,entity.JsonContent
                    ,entity.Created
                }
            );
        }

        public void Insert(ResponsePayload entity)
        {
            Connection.Execute(
                "SProc_ResponsePayload_Insert",
                commandType: CommandType.StoredProcedure,
                param: new
                {
                    entity.Id
                    ,entity.ResponseHeaders
                    ,entity.ContentHeaders
                    ,entity.JsonContent
                    ,entity.Created
                }
            );
        }
    }
}
