﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qnote.Models.DAL;

namespace Qnote.Models
{
    public class Service
    {
        #region Note CRUD
        // Init of the NoteDAL
        private NoteDAL _noteDAL;
        private NoteDAL NoteDAL
        {
            get { return _noteDAL ?? (_noteDAL = new NoteDAL()); }
        }

        // Gets all notes.
        public IEnumerable<Qnote> GetNotes()
        {
            return NoteDAL.GetNotes();
        }

        // Gets specific note.
        public Qnote GetNote(int NoteID)
        {
            return NoteDAL.GetNoteByID(NoteID);
        }

        // Deletes specific note.
        public void DeleteNote(int NoteID)
        {
            NoteDAL.DeleteNote(NoteID);
        }
        #endregion

        #region Collection CRUD
        // Init of the CollectionDAL
        private CollectionDAL _collectionDAL;
        private CollectionDAL CollectionDAL
        {
            get { return _collectionDAL ?? (_collectionDAL = new CollectionDAL()); }
        }

        // Gets collection data for a specific note.
        public Collection GetCollection(int NoteID)
        {
            return CollectionDAL.GetCollectionByNoteID(NoteID);
        }
        #endregion

        #region CollectionName R
        // Init of the CollectionNameDAL
        private CollectionNameDAL _collectionNameDAL;
        private CollectionNameDAL CollectionNameDAL
        {
            get { return _collectionNameDAL ?? (_collectionNameDAL = new CollectionNameDAL()); }
        }

        // Gets collection name
        public CollectionName GetCollectionName(int CollectionNameID)
        {
            return CollectionNameDAL.GetCollectionNameByID(CollectionNameID);
        }
        #endregion
    }
}