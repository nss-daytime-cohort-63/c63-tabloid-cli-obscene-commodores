using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
using Microsoft.Data.SqlClient;

namespace TabloidCLI.Repositories
{
    public class JournalRepository : DatabaseConnector, IRepository<Journal>
    {
        public JournalRepository(string connectionString) : base(connectionString)
        {
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand()) 
                {
                    cmd.CommandText = @"Delete from Journal where Id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("journal deleted");
                
                }
            }
        }

        public Journal Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Journal> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand()) 
                {
                    cmd.CommandText = "select * from Journal";

                    List<Journal> journals = new List<Journal>();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read()) 
                    {
                        Journal journal = new Journal()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))

                        };
                        journals.Add(journal);
                    }
                    return journals;
                }
            }
        }

        public void Insert(Journal entry)
        {
           using(SqlConnection conn = Connection) 
            {
                conn.Open();
                using (SqlCommand cmd =conn.CreateCommand())
                {
                    cmd.CommandText = @"Insert into Journal (Title, Content, CreateDateTime)
                                        values (@title, @content, @datetime)";
                    cmd.Parameters.AddWithValue("@title", entry.Title);
                    cmd.Parameters.AddWithValue("@content", entry.Content);
                    cmd.Parameters.AddWithValue("@datetime", entry.CreateDateTime);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Journal entry)
        {
            throw new NotImplementedException();
        }
    }
}
