using CityDAODL.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityDAODL.DAOs
{
    public class CityDAO
    {
        private string connectionString;

        public CityDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public CityTO Save(CityTO city)
        {
            string SQL = "INSERT INTO city(name,description,country) output INSERTED.ID VALUES(@name,@description,@country)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@name", city.Name);
                    cmd.Parameters.AddWithValue("@description", city.Description);
                    cmd.Parameters.AddWithValue("@country", city.Country);
                    int id = (int)cmd.ExecuteScalar();
                    city.Id = id;
                    return city;
                }
                catch (Exception ex)
                {
                    throw new Exception("City Save", ex);
                }
            }
        }
        public void Delete(int id)
        {
            string SQL = "DELETE FROM city WHERE id=@id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("City Delete", ex);
                }
            }
        }
        public List<CityTO> GetAll()
        {
            List<CityTO> cities = new List<CityTO>();
            string SQL = $"SELECT * FROM city";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cities.Add(new CityTO((int)reader["id"], (string)reader["name"], (string)reader["Description"], (string)reader["Country"]));
                    }
                    return cities;
                }
                catch (Exception ex)
                {
                    throw new Exception("City GetAll", ex);
                }
            }
        }
        public CityTO GetById(int id)
        {
            string SQL = "SELECT * FROM city WHERE id=@id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@id", id);
                    IDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    return new CityTO(id, (string)reader["name"], (string)reader["Description"], (string)reader["Country"]);
                }
                catch (Exception ex)
                {
                    throw new Exception("City GetById", ex);
                }
            }
        }
    }
}
