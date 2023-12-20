using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using MySqlConnector;
using System.Security.Cryptography;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using vuejs_backend.Models;


namespace vuejs_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PipeInfoController : ControllerBase
{

    private string? yourConnectionString = "Server=localhost;User ID=root;Password=tester;Database=pipe";
    MySqlConnection connection;
    private static List<PipeInfo> pipeInfoList = new List<PipeInfo>();

    public PipeInfoController()
    {
        connection = new MySqlConnection(yourConnectionString);
    }

    static string ComputeSha256Hash(string? rawData)
    {
        if (rawData == null)
        {
            return "";
        }
        string salt = "should to random";

        string saltData = rawData + "" + salt;
        //create a Sha256                
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(saltData));

            // Convert byte array to a string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();

        }

    }


    [HttpGet]
    [Route("pipe-get-all")]
    public ActionResult<dynamic> GetAllPipes()
    {
        string query = @"SELECT * FROM pipe_info";

        MySqlDataReader myReader;
        DataTable table = new DataTable();
        connection.Open();

        MySqlCommand myCommand = new MySqlCommand(query, connection);
        myReader = myCommand.ExecuteReader();
        table.Load(myReader);

        myReader.Close();
        connection.Close();

        List<object> allPipeInfo = new List<object>();
        for (int i = 0; i < table.Rows.Count; i++)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>        //แปลงjson
                {
                    { "id", table.Rows[i]["id"] },
                    { "tag_no", table.Rows[i]["tag_no"] },
                    { "pipe_size", table.Rows[i]["pipe_size"] },
                    { "pipe_location", table.Rows[i]["pipe_location"] },
                    { "installation_date", table.Rows[i]["installation_date"] },
                    { "inspection_interval", table.Rows[i]["inspection_interval"] },
                    { "is_active", table.Rows[i]["is_active"] }
                };
            allPipeInfo.Add(dict);
        }
        return allPipeInfo;
    }


    [HttpPost]
    [Route("add-pipe")]
    public ActionResult<dynamic> CreatePipe(PipeInfo pipe)
    {
        //string? pass = pipe.pass;
        //string hashedPass = ComputeSha256Hash(pass);

        string query = @"INSERT INTO pipe_info ( `tag_no`,`pipe_size`, `pipe_location`,
                        `installation_date`,`inspection_interval`,`is_active`)
                        VALUES (@tagNo, @pipeSize, @pipeLo, @inDate, @insIn,True);";


        DataTable table = new DataTable();
        connection.Open();
        MySqlCommand myCommand = new MySqlCommand(query, connection);

        myCommand.Parameters.AddWithValue("@tagNo", pipe.tag_no);
        myCommand.Parameters.AddWithValue("@pipeSize", pipe.pipe_size);
        myCommand.Parameters.AddWithValue("@pipeLo", pipe.pipe_location);
        myCommand.Parameters.AddWithValue("@inDate", pipe.installation_date);
        myCommand.Parameters.AddWithValue("@insIn", pipe.inspection_interval);

        Console.WriteLine("Records Inserted Successfully");

        int rowAff = myCommand.ExecuteNonQuery();
        if (rowAff == 0)
        {
            return BadRequest("Error Generated");
        }
        connection.Close();
        return Ok("Records Inserted Successfully");
    }

    [HttpGet]
    [Route("get-pipe-byid")]
    public IActionResult GetPipeById(int id)
    {
        string query = @"SELECT * FROM pipe_info WHERE id = @pid";

        MySqlDataReader myReader;
        DataTable table = new DataTable();
        connection.Open();

        MySqlCommand myCommand = new MySqlCommand(query, connection);
        myCommand.Parameters.AddWithValue("@pid", id);

        myReader = myCommand.ExecuteReader();
        table.Load(myReader);

        myReader.Close();
        connection.Close();

        if (table.Rows.Count == 0)
        {
            return NotFound();
        }
        Dictionary<string, object> dict = new Dictionary<string, object>        //แปลงjson
        {
            { "id", table.Rows[0]["id"] },
            { "tag_no", table.Rows[0]["tag_no"] },
            { "pipe_size", table.Rows[0]["pipe_size"] },
            { "pipe_location", table.Rows[0]["pipe_location"] },
            { "installation_date", table.Rows[0]["installation_date"] },
            { "inspection_interval", table.Rows[0]["inspection_interval"] },
            { "is_active", table.Rows[0]["is_active"] }
        };
        return Ok(dict);

    }

    [HttpPut]
    [Route("update-pipe")]
    public IActionResult UpdatePipeInfo(int id, PipeInfo updatedPipe)
    {
        var pipe = pipeInfoList.FirstOrDefault(p => p.id == id);
        if (pipe == null)
            return NotFound();

        return Ok(pipe);
    }

    [HttpDelete]
    [Route("delete-pipe")]
    public IActionResult DeletePipeInfo(int id)
    {
        var pipe = pipeInfoList.FirstOrDefault(p => p.id == id);
        if (pipe == null)
            return NotFound();

        pipeInfoList.Remove(pipe);
        return Ok(pipe);
    }
}
