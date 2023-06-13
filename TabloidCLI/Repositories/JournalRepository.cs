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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Update(Journal entry)
        {
            throw new NotImplementedException();
        }
    }
}
