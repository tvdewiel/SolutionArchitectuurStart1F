using CityRepoBL.Interfaces;
using CityRepoBL.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityRepoDL
{
    public class CityRepository : ICityRepository
    {
        private string connectionString;

        public CityRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddAttraction(int id, Attraction attraction)
        {
            string SQL = "INSERT INTO attraction(name,description,type,location,city_id,organizer) output INSERTED.ID VALUES(@name,@description,@type,@location,@city_id,@organizer)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@organizer", attraction.Organizer);
                    cmd.Parameters.AddWithValue("@name", attraction.Name);
                    cmd.Parameters.AddWithValue("@description", attraction.Description);
                    cmd.Parameters.AddWithValue("@type", attraction.Type);
                    cmd.Parameters.AddWithValue("@location", attraction.Location);
                    cmd.Parameters.AddWithValue("@city_id", id);
                    int aid = (int)cmd.ExecuteScalar();
                    attraction.Id = aid;
                }
                catch (Exception ex)
                {
                    throw new Exception("AddAttraction", ex);
                }
            }
        }

        public void AddCity(City city)
        {
            string SQLcity = "INSERT INTO city(name,description,country) output INSERTED.ID VALUES(@name,@description,@country)";
            string SQLattraction = "INSERT INTO attraction(name,description,type,location,city_id) output INSERTED.ID VALUES(@name,@description,@type,@location,@city_id,@organizer)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    cmd.Transaction = conn.BeginTransaction();
                    conn.Open();
                    cmd.CommandText = SQLcity;
                    cmd.Parameters.AddWithValue("@name", city.Name);
                    cmd.Parameters.AddWithValue("@description", city.Description);
                    cmd.Parameters.AddWithValue("@country", city.Country);
                    int id = (int)cmd.ExecuteScalar();
                    city.Id = id;
                    cmd.CommandText = SQLattraction;
                    cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@description", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@type", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@location", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@city_id", SqlDbType.Int));
                    foreach (Attraction attraction in city.Attractions)
                    {

                        cmd.Parameters["@name"].Value = attraction.Name;
                        cmd.Parameters["@description"].Value = attraction.Description;
                        cmd.Parameters["@type"].Value = attraction.Type;
                        cmd.Parameters["@location"].Value = attraction.Location;
                        cmd.Parameters["@city_id"].Value = city.Id;
                        cmd.Parameters["@organizer"].Value = attraction.Organizer;
                        int aid = (int)cmd.ExecuteScalar();
                        attraction.Id = aid;
                    }
                    cmd.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    cmd.Transaction.Rollback();
                    throw new Exception("City Save", ex);
                }
            }
        }

        public City GetCity(int id)
        {
            City city = null;
            string SQL = "SELECT t1.id cityid,t1.name cityname,t1.country,t1.description citydescription,t2.* FROM [City] t1 left join Attraction t2 on t1.id=t2.city_id WHERE t1.id=@id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@id", id);
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (city == null) city = new City(id, (string)reader["cityname"], (string)reader["citydescription"], (string)reader["country"]);
                        if (!reader.IsDBNull(reader.GetOrdinal("id")))
                        {
                            Attraction attraction = new Attraction((int)reader["id"], (string)reader["name"], (string)reader["description"], (string)reader["type"], (string)reader["location"], (string)reader["organizer"]);
                            city.AddAttraction(attraction);
                        }
                    }
                    return city;
                }
                catch (Exception ex)
                {
                    throw new Exception("Get City", ex);
                }
            }
        }
    }
}
