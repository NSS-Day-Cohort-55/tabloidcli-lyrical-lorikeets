﻿using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;
using TabloidCLI.UserInterfaceManagers;
using System.Data;

namespace TabloidCLI.Repositories
{
    public class NoteRepository : DatabaseConnector, IRepository<Note>
    {
        public NoteRepository(string connectionString) : base(connectionString) { }

        public List<Note> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Title, Content, CreateDateTime, PostId FROM Note";

                    List<Note> notes = new List<Note>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Note note = new Note()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            //note.PostId = reader.GetInt32(reader.GetOrdinal("note.PostId"))

                            //Post = new Post()
                            //{
                            //    Id = reader.GetInt32(reader.GetOrdinal("postId")),
                            //    Title = reader.GetString(reader.GetOrdinal("postTitle")),
                            //    Url = reader.GetString(reader.GetOrdinal("postURL"))
                            //}
                        };
                        notes.Add(note);
                    }
                    reader.Close();
                    return notes;
                }
            }
        }

        public Note Get(int id)
        {
            throw new NotImplementedException();
        }

       
        public void Insert(Note note)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Note (Title, Content, CreateDateTime, PostId)
                    VALUES (@NoteTitle, @NoteContent, @CreateDateTime, @PostId)";

                    cmd.Parameters.AddWithValue("@NoteTitle", note.Title);
                    cmd.Parameters.AddWithValue("@NoteContent", note.Content);
                    cmd.Parameters.AddWithValue("@CreateDateTime", note.CreateDateTime);
                    cmd.Parameters.AddWithValue("@PostId", note.Post.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        public void Update(Note note)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Note WHERE Id = @id";               
                    cmd.Parameters.AddWithValue("@id",id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}


