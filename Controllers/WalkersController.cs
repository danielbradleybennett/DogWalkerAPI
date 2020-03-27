//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using System.Data;
//using Microsoft.Data.SqlClient;

//using Microsoft.AspNetCore.Http;

//namespace DogWalkerAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class WalkersController : ControllerBase

//    {
//        private readonly IConfiguration _config;

//        public WalkersController(IConfiguration config)
//        {
//            _config = config;
//        }

//        public SqlConnection Connection
//        {
//            get
//            {
//                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
//            }
//        }

//        [HttpGet]
//        public async Task<IActionResult> Get()
//        {
//            using (SqlConnection conn = Connection)
//            {
//                conn.Open();
//                using (SqlCommand cmd = conn.CreateCommand())
//                {
//                    cmd.CommandText = "SELECT Id, Name, NeighborhoodId FROM Walker";
//                    SqlDataReader reader = cmd.ExecuteReader();
//                    List<Walker> walkers = new List<Walker>();

//                    while (reader.Read())
//                    {
//                        Walker walker = new Walker
//                        {
//                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
//                            Name = reader.GetString(reader.GetOrdinal("Name")),
//                            NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                           

//                        };

//                        walkers.Add(walker);
//                    }
//                    reader.Close();

//                    return Ok(walkers);
//                }
//            }
//        }

//        [HttpGet("{id}", Name = "GetWalker")]
//        public async Task<IActionResult> Get([FromRoute] int id)
//        {
//            using (SqlConnection conn = Connection)
//            {
//                conn.Open();
//                using (SqlCommand cmd = conn.CreateCommand())
//                {
//                    cmd.CommandText = @"
//                        SELECT
//                            Id, Name, OwnerId
//                        FROM Dog
//                        WHERE Id = @id";
//                    cmd.Parameters.Add(new SqlParameter("@id", id));
//                    SqlDataReader reader = cmd.ExecuteReader();

//                    Dog dog = null;

//                    if (reader.Read())
//                    {
//                        Dog dog = new Dog
//                        {
//                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
//                            Name = reader.GetString(reader.GetOrdinal("Name")),
//                            OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
//                            Breed = reader.GetString(reader.GetOrdinal("Breed")),
//                            Notes = reader.GetString(reader.GetOrdinal("Notes"))

//                        };
//                    }
//                    reader.Close();

//                    return Ok(dog);
//                }
//            }
//        }

//        [HttpPost]
//        public async Task<IActionResult> Post([FromBody] Dog dog)
//        {
//            using (SqlConnection conn = Connection)
//            {
//                conn.Open();
//                using (SqlCommand cmd = conn.CreateCommand())
//                {
//                    cmd.CommandText = @"INSERT INTO Dog (Name, OwnerId, Breed, Notes)
//                                        OUTPUT INSERTED.Id
//                                        VALUES (@Name, @OwnerId, @Breed, @Notes)";
//                    cmd.Parameters.Add(new SqlParameter("@Name", dog.Name));
//                    cmd.Parameters.Add(new SqlParameter("@OwnerId", dog.OwnerId));
//                    cmd.Parameters.Add(new SqlParameter("@Breed", dog.Breed));
//                    cmd.Parameters.Add(new SqlParameter("@Notes", dog.Notes));


//                    int newId = (int)cmd.ExecuteScalar();
//                    department.Id = newId;
//                    return CreatedAtRoute("GetDog", new { id = newId }, dog);
//                }
//            }
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Dog dog)
//        {
//            try
//            {
//                using (SqlConnection conn = Connection)
//                {
//                    conn.Open();
//                    using (SqlCommand cmd = conn.CreateCommand())
//                    {
//                        cmd.CommandText = @"UPDATE Dog
//                                            SET Name = @Name,
//                                                OwnerId = @Id,
//                                                Breed = @Breed,
//                                                Notes = @Notes

//                                            WHERE Id = @id";
//                        cmd.Parameters.Add(new SqlParameter("@Name", dog.Name));
//                        cmd.Parameters.Add(new SqlParameter("@Id", dog.OwnerId));
//                        cmd.Parameters.Add(new SqlParameter("@Name", dog.Name));
//                        cmd.Parameters.Add(new SqlParameter("@Notes", dog.Notes));



//                        cmd.Parameters.Add(new SqlParameter("@id", id));

//                        int rowsAffected = cmd.ExecuteNonQuery();
//                        if (rowsAffected > 0)
//                        {
//                            return new StatusCodeResult(StatusCodes.Status204NoContent);
//                        }
//                        throw new Exception("No rows affected");
//                    }
//                }
//            }
//            catch (Exception)
//            {
//                if (!DogExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete([FromRoute] int id)
//        {
//            try
//            {
//                using (SqlConnection conn = Connection)
//                {
//                    conn.Open();
//                    using (SqlCommand cmd = conn.CreateCommand())
//                    {
//                        cmd.CommandText = @"DELETE FROM Dog WHERE Id = @id";
//                        cmd.Parameters.Add(new SqlParameter("@id", id));

//                        int rowsAffected = cmd.ExecuteNonQuery();
//                        if (rowsAffected > 0)
//                        {
//                            return new StatusCodeResult(StatusCodes.Status204NoContent);
//                        }
//                        throw new Exception("No rows affected");
//                    }
//                }
//            }
//            catch (Exception)
//            {
//                if (!DogExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }
//        }

//        private bool DogExists(int id)
//        {
//            using (SqlConnection conn = Connection)
//            {
//                conn.Open();
//                using (SqlCommand cmd = conn.CreateCommand())
//                {
//                    cmd.CommandText = @"
//                        SELECT Id, Name, OwnerId, Breed, Notes
//                        FROM Dog
//                        WHERE Id = @id";
//                    cmd.Parameters.Add(new SqlParameter("@id", id));

//                    SqlDataReader reader = cmd.ExecuteReader();
//                    return reader.Read();
//                }
//            }
//        }
//    }
//}

