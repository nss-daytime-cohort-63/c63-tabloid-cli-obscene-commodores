using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    internal class BlogRepository : DatabaseConnector, IRepository<Blog>
    {
        public BlogRepository(string connectionString) : base(connectionString)
        {
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Blog WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Blog Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Blog> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Title, Url FROM Blog";
                    List<Blog> blogs = new List<Blog>();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Blog blog = new Blog()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Url = reader.GetString(reader.GetOrdinal("Url")),
                        };
                        blogs.Add(blog);
                    }
                    return blogs;
                }
            }
        }

        public void Insert(Blog entry)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Blog (Title, Url)
                                        OUTPUT INSERTED.Id
                                        Values (@title, @url)";
                    cmd.Parameters.AddWithValue("@title", entry.Title);
                    cmd.Parameters.AddWithValue("@url", entry.Url);
                    int id = (int)cmd.ExecuteScalar();
                    entry.Id = id;
                }
            }
        }

        public void Update(Blog entry)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Blog
                                        SET Title = @title,
                                            Url = @url
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@title", entry.Title);
                    cmd.Parameters.AddWithValue("@url", entry.Url);
                    cmd.Parameters.AddWithValue("@id", entry.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void InsertTag(Blog blog, Tag tag)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO AuthorTag (BlogId, TagId)
                                                       VALUES (@blogId, @tagId)";
                    cmd.Parameters.AddWithValue("@blogId", blog.Id);
                    cmd.Parameters.AddWithValue("@tagId", tag.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
