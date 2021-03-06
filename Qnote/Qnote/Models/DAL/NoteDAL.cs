﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Qnote.Models.DAL
{
    // This DAL handles retrieving/handling data related to the Note table. With the exception that a Note ALWAYS when created/updated is related to the Collection/CollectionNameID table.
    // Therefore methods for that is included here and not in a separate DAL. Makes life easier.
    public class NoteDAL : DALBase
    {
        // Collection for looping out references to the Qnote-object (Quick-notes, gets all for a specific user).
        public IEnumerable<Qnote> GetNotes(int UserID)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var qnotes = new List<Qnote>(25);

                    var cmd = new SqlCommand("app.usp_GetNotes", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int, 4).Value = UserID;

                    conn.Open();

                    // Creates references to the data from the database.
                    using (var reader = cmd.ExecuteReader())
                    {
                        // Figures out the index of the DB-columns.
                        var noteIdIndex = reader.GetOrdinal("NoteID");
                        var headerIndex = reader.GetOrdinal("Header");
                        var noteIndex = reader.GetOrdinal("Note");
                        var dateIndex = reader.GetOrdinal("Date");
                        var userIdIndex = reader.GetOrdinal("UserID");

                        while (reader.Read())
                        {
                            qnotes.Add(new Qnote
                            {
                                NoteID = reader.GetInt32(noteIdIndex),
                                Header = reader.GetString(headerIndex),
                                Note = reader.GetString(noteIndex),
                                Date = reader.GetDateTime(dateIndex),
                                UserID = reader.GetInt32(userIdIndex)
                            });
                        }
                    }

                    qnotes.TrimExcess();
                    return qnotes;
                }
                catch
                {
                    throw new ApplicationException("An error occured when trying to access and get data from database.");
                }
            }
        }

        // Retrives a specific Note.
        public Qnote GetNoteByID(int NoteID)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("app.usp_GetNote", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@NoteID", SqlDbType.Int, 4).Value = NoteID;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        // Figures out the index of the DB-columns.
                        var noteIdIndex = reader.GetOrdinal("NoteID");
                        var headerIndex = reader.GetOrdinal("Header");
                        var noteIndex = reader.GetOrdinal("Note");
                        var dateIndex = reader.GetOrdinal("Date");
                        var userIdIndex = reader.GetOrdinal("UserID");

                        // If a post matching my parameter exists, an object containing the data will be created.
                        if (reader.Read())
                        {
                            return new Qnote
                            {
                                NoteID = reader.GetInt32(noteIdIndex),
                                Header = reader.GetString(headerIndex),
                                Note = reader.GetString(noteIndex),
                                Date = reader.GetDateTime(dateIndex),
                                UserID = reader.GetInt32(userIdIndex)
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                catch
                {
                    throw new ApplicationException("An error occured when trying to access and get data from database.");
                }
            }
        }

        // Creates a new note.
        public void CreateNoteAndCollection(QnoteCollectionID qnoteCollectionID)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("app.usp_CreateNoteAndCollection", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Here I'm creating the parameters that will be used.
                    cmd.Parameters.Add("@Header", SqlDbType.VarChar, 60).Value = qnoteCollectionID.Header;
                    cmd.Parameters.Add("@Note", SqlDbType.VarChar, 2000).Value = qnoteCollectionID.Note;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int, 4).Value = qnoteCollectionID.UserID;
                    cmd.Parameters.Add("@CollectionNameID", SqlDbType.Int, 4).Value = qnoteCollectionID.CollectionNameID;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("An error occured when trying to add a note connected to a collection to the database.");
                }
            }
        }

        // Updates a existing note.
        public void UpdateNoteAndCollection(QnoteCollectionID qnote)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("app.usp_UpdateNoteAndCollection", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Here im creating the parameters that will be used.                   
                    cmd.Parameters.Add("@Header", SqlDbType.VarChar, 60).Value = qnote.Header;
                    cmd.Parameters.Add("@Note", SqlDbType.VarChar, 2000).Value = qnote.Note;
                    cmd.Parameters.Add("@NoteID", SqlDbType.Int, 4).Value = qnote.NoteID;
                    cmd.Parameters.Add("@CollectionNameID", SqlDbType.Int, 4).Value = qnote.CollectionNameID;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("An error occured when trying to update a note.");
                }
            }
        }

        // Deletes a note
        public void DeleteNote(int NoteID)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("app.usp_DeleteNote", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@NoteID", SqlDbType.Int, 4).Value = NoteID;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("An error occured when trying to remove data from the database.");
                }
            }
        }
    }
}