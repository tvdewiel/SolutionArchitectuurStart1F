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
    public class AttractionDAO
    {
        private string connectionString;

        public AttractionDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public AttractionTO Save(AttractionTO attraction)
        {
            string SQL = "INSERT INTO attraction(name,description,type,location,city_id,organizer) output INSERTED.ID VALUES(@name,@description,@type,@location,@city_id,@organizer)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@name", attraction.Name);
                    cmd.Parameters.AddWithValue("@description", attraction.Description);
                    cmd.Parameters.AddWithValue("@type", attraction.Type);
                    cmd.Parameters.AddWithValue("@location", attraction.Location);
                    cmd.Parameters.AddWithValue("@organizer", attraction.Organizer);
                    cmd.Parameters.AddWithValue("@city_id", attraction.City_Id);
                    int id = (int)cmd.ExecuteScalar();
                    attraction.ID = id;
                    return attraction;
                }
                catch (Exception ex)
                {
                    throw new Exception("attraction Save", ex);
                }
            }
        }
        public void Delete(int id)
        {
            string SQL = "DELETE FROM attraction WHERE id=@id";
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
                    throw new Exception("attraction Delete", ex);
                }
            }
        }
        public List<AttractionTO> GetByCity(int cityId)
        {
            List<AttractionTO> attractions = new();
            string SQL = $"SELECT * FROM attraction WHERE city_id=@id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@id", cityId);
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        attractions.Add(new AttractionTO((int)reader["id"], (string)reader["name"], (string)reader["Description"], (string)reader["Type"], (string)reader["Location"], cityId, (string)reader["organizer"]));
                    }
                    return attractions;
                }
                catch (Exception ex)
                {
                    throw new Exception("attraction GetForCity", ex);
                }
            }
        }
        public AttractionTO GetById(int id)
        {
            string SQL = "SELECT * FROM attraction WHERE id=@id";
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
                    return new AttractionTO(id, (string)reader["name"], (string)reader["Description"], (string)reader["Type"], (string)reader["Location"], (int)reader["city_id"], (string)reader["organizer"]);
                }
                catch (Exception ex)
                {
                    throw new Exception("attraction GetById", ex);
                }
            }
        }
        public bool CityHasAttractions(int id)
        {
            string SQL = "SELECT count(*) FROM [attraction] WHERE city_id=@id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@id", id);
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true; else return false;
                }
                catch (Exception ex)
                {
                    throw new Exception("CityHasAttractions", ex);
                }
            }
        }
    }
}
