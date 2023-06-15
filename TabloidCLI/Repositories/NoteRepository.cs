﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
namespace TabloidCLI.Repositories
{
    public class NoteRepository : DatabaseConnector, IRepository<Note>
    {
        public NoteRepository(string connectionString) : base(connectionString)
        {
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Note Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Note> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Title, CreateDateTime, Content FROM Note";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Note> notes = new List<Note>();
                        while (reader.Read())
                        {
                            int TitleColumn = reader.GetOrdinal("Title");
                            int CreateDateTimeColumn = reader.GetOrdinal("CreateDateTime");
                            int ContentColumn = reader.GetOrdinal("Content");

                            string TitleValue = reader.GetString(TitleColumn);
                            DateTime CreateDateTimeValue = reader.GetDateTime(CreateDateTimeColumn);
                            string ContentValue = reader.GetString(ContentColumn);

                            Note note = new Note()
                            {
                                Title = TitleValue,
                                CreateDateTime = CreateDateTimeValue,
                                Content = ContentValue
                            };
                            notes.Add(note);
                        }
                        return notes;
                    }
                }
            }
        }

        public void Insert(Note entry)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Note (Title, Content, CreateDateTime, PostId)
                                        OUTPUT INSERTED.Id
                                        VALUES (@title, @content, @createDateTime, @postId)";
                    cmd.Parameters.AddWithValue("@title", entry.Title);
                    cmd.Parameters.AddWithValue("@content", entry.Content);
                    cmd.Parameters.AddWithValue("@createDateTime", entry.CreateDateTime);
                    cmd.Parameters.AddWithValue("@postId", entry.PostId);
                    int id = (int)cmd.ExecuteScalar();
                    entry.Id = id;
                }
            }
        }

        public void Update(Note entry)
        {
            throw new NotImplementedException();
        }
    }
}
