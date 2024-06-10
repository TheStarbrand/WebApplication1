using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Extensions.Configuration;
namespace WebApplication1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TodoAppController : ControllerBase
	{
		private IConfiguration _configuration;
		public TodoAppController(IConfiguration configuration)
		{
			_configuration = configuration;	
		}

		[HttpGet]
		[Route("GetNotes")]
		public JsonResult GetNotes()
		{
			string query = "select * from dbo.Notes";
			DataTable table = new();
			string sqlDataSource = _configuration.GetConnectionString("defaultConnection");
			SqlDataReader myReader;

			using (SqlConnection myCon = new SqlConnection(sqlDataSource))
			{
				myCon.Open();
				using (SqlCommand myCommand = new (query, myCon))
				{
					myReader = myCommand.ExecuteReader(); 
					table.Load(myReader);
					myReader.Close();
					myCon.Close();
				}
			}

			return new JsonResult(table);

		}


	}

}


