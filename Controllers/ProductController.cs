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
public class ProductInfoController : ControllerBase
{

    private string? yourConnectionString = "Server=localhost;User ID=root;Password=tester;Database=pipe";
    MySqlConnection connection;
    private static List<ProductInfo> productInfoList = new List<ProductInfo>();

    public ProductInfoController()
    {
        connection = new MySqlConnection(yourConnectionString);
    }

    [HttpGet]
    [Route("product-get-all")]
    public ActionResult<dynamic> GetAllProducts()
    {
        string query = @"SELECT * FROM product_info";

        MySqlDataReader myReader;
        DataTable table = new DataTable();
        connection.Open();

        MySqlCommand myCommand = new MySqlCommand(query, connection);
        myReader = myCommand.ExecuteReader();
        table.Load(myReader);

        myReader.Close();
        connection.Close();

        List<object> allProductInfo = new List<object>();
        for (int i = 0; i < table.Rows.Count; i++)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>        //แปลงjson
                {
                    { "id", table.Rows[i]["id"] },
                    { "title", table.Rows[i]["title"] },
                    { "price", table.Rows[i]["price"] },
                    { "description", table.Rows[i]["description"] },
                    { "category", table.Rows[i]["category"] },
                    { "image", table.Rows[i]["image"] },
                    { "rate", table.Rows[i]["rate"] },
                    { "count", table.Rows[i]["count"] }
                };
            allProductInfo.Add(dict);
        }
        return allProductInfo;
    }

    [HttpPost]
    [Route("add-product")]
    public ActionResult<dynamic> CreateProduct(ProductInfo product)
    {
        //string? pass = product.pass;
        //string hashedPass = ComputeSha256Hash(pass);

        string query = @"INSERT INTO product_info ( `title`,`price`, `description`,
                        `category`,`image`,`rate`,`count`)
                        VALUES (@title, @price, @description, @category, @image,@rate,@count);";


        DataTable table = new DataTable();
        connection.Open();
        MySqlCommand myCommand = new MySqlCommand(query, connection);

        myCommand.Parameters.AddWithValue("@title", product.title);
        myCommand.Parameters.AddWithValue("@price", product.price);
        myCommand.Parameters.AddWithValue("@description", product.description);
        myCommand.Parameters.AddWithValue("@category", product.category);
        myCommand.Parameters.AddWithValue("@image", product.image);
        myCommand.Parameters.AddWithValue("@rate", product.rate);
        myCommand.Parameters.AddWithValue("@count", product.count);
        int rowAff = myCommand.ExecuteNonQuery();

        connection.Close();

        Console.WriteLine("Records Inserted Successfully");

        if (rowAff == 0)
        {
            return BadRequest("Error Generated");
        }
        connection.Close();
        return Ok("Records Inserted Successfully");
    }

    [HttpGet]
    [Route("get-product-byid")]
    public IActionResult GetProductById(int id)
    {
        string query = @"SELECT * FROM product_info WHERE id = @pid";

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
            { "title", table.Rows[0]["title"] },
            { "price", table.Rows[0]["price"] },
            { "description", table.Rows[0]["description"] },
            { "category", table.Rows[0]["category"] },
            { "image", table.Rows[0]["image"] },
            { "rate", table.Rows[0]["rate"] },
            { "count", table.Rows[0]["count"] }
        };
        return Ok(dict);

    }

    [HttpPut]
    [Route("update-product-info")]
    public IActionResult UpdateProduct(ProductInfo product)
    {
        string query = @"UPDATE product_info
                        SET title = @title, price = @price, description = @description, category = @category
                        , image = @image, rate = @rate, count = @count
                        WHERE id = @pid;";

        MySqlDataReader myReader;
        DataTable table = new DataTable();
        connection.Open();

        MySqlCommand myCommand = new MySqlCommand(query, connection);

        myCommand.Parameters.AddWithValue("@pid", product.id);
        myCommand.Parameters.AddWithValue("@title", product.title);
        myCommand.Parameters.AddWithValue("@price", product.price);
        myCommand.Parameters.AddWithValue("@description", product.description);
        myCommand.Parameters.AddWithValue("@category", product.category);
        myCommand.Parameters.AddWithValue("@image", product.image);
        myCommand.Parameters.AddWithValue("@rate", product.rate);
        myCommand.Parameters.AddWithValue("@count", product.count);
        int rowAff = myCommand.ExecuteNonQuery();

        myReader = myCommand.ExecuteReader();
        table.Load(myReader);

        myReader.Close();
        connection.Close();

        if (rowAff == 0)
        {
            return BadRequest("Error Generated");
        }
        connection.Close();
        return Ok("Records Inserted Successfully");

    }


    [HttpDelete]
    [Route("delete-product")]
    public IActionResult DeleteProductInfo(int id)
    {
        var product = productInfoList.FirstOrDefault(p => p.id == id);
        if (product == null)
            return NotFound();

        productInfoList.Remove(product);
        return Ok(product);
    }
}
