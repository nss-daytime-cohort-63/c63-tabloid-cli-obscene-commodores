using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    public class BlogRepository : DatabaseConnector, IRepository<Blog>
    {
        public BlogRepository(string connectionString) : base(connectionString) { }

        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"ALTER TABLE BlogTag
                                      DROP CONSTRAINT[FK_BlogTag_Blog]
                                      ALTER TABLE BlogTag
                                      ADD CONSTRAINT [FK_BlogTag_Blog]
                                      FOREIGN KEY (BlogId) REFERENCES Blog(Id) ON DELETE CASCADE
                                      DELETE FROM Blog WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Blog Get(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT b.Id AS BlogId,
                                               b.Title,
                                               t.Id AS TagId,
                                               t.Name
                                          FROM Blog b 
                                               LEFT JOIN BlogTag bt on b.Id = bt.BlogId
                                               LEFT JOIN Tag t on t.Id = bt.TagId
                                         WHERE b.id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    Blog blog = null;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (blog == null)
                        {
                            blog = new Blog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("BlogId")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                            };
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("TagId")))
                        {
                                 blog.Tags.Add(new Tag()
                             {
                                      Id = reader.GetInt32(reader.GetOrdinal("TagId")),
                                      Name = reader.GetString(reader.GetOrdinal("Name")),
                             });
                        }
                    }

                    reader.Close();

                    return blog;
                }
            }
        }

        public List<Blog> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Blog.Id AS BlogId,
                                               Blog.Title AS BlogTitle,
                                               Blog.Url AS BlogUrl,
                                               Tag.Name AS TagName,
                                               Tag.Id AS TagId
                                          FROM Blog
                                          LEFT JOIN BlogTag ON BlogTag.BlogId = Blog.Id
                                          LEFT JOIN Tag ON BlogTag.TagId = Tag.Id";

                    List<Blog> blogs = new List<Blog>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Blog blogMatch = blogs.FirstOrDefault(b => b.Id == reader.GetInt32(reader.GetOrdinal("BlogId")));
                        if (blogMatch == null)
                        {
                            blogMatch = new Blog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("BlogId")),
                                Title = reader.GetString(reader.GetOrdinal("BlogTitle")),
                                Url = reader.GetString(reader.GetOrdinal("BlogUrl")),
                                Tags = new List<Tag>()
                            };
                            blogs.Add(blogMatch);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("TagId")))
                        {
                            blogMatch.Tags.Add(new Tag()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("TagId")),
                                Name = reader.GetString(reader.GetOrdinal("TagName"))
                            });
                        }
                    }
                reader.Close();
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
                    cmd.CommandText = @"INSERT INTO BlogTag (BlogId, TagId)
                                                       VALUES (@blogId, @tagId)";
                    cmd.Parameters.AddWithValue("@blogId", blog.Id);
                    cmd.Parameters.AddWithValue("@tagId", tag.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void DeleteTag(int blogId, int tagId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM BlogTag 
                                         WHERE BlogId = @blogid AND 
                                               TagId = @tagId";
                    cmd.Parameters.AddWithValue("@blogId", blogId);
                    cmd.Parameters.AddWithValue("@tagId", tagId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
