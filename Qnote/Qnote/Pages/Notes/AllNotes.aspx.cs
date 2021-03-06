﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Qnote.Models;

namespace Qnote.Pages
{
    public partial class AllNotes : System.Web.UI.Page
    {
        // Lazy init.
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IEnumerable<QnoteCollection> QnoteListView_GetData()
        {
            try
            {
                // Here i get all notes and put them in "qnote" then get the collection for the note, and the name for the collection.
                // After that i throw all the info into a new QnoteCollection object and return that to the listview.
                IEnumerable<QnoteCollection> notes = from qnote in Service.GetNotes(Int32.Parse(Session["userID"].ToString()))
                                                            let collection = Service.GetCollection(qnote.NoteID)
                                                            let collectionName = Service.GetCollectionName(collection.CollectionNameID)
                                                            select new QnoteCollection
                                                            {
                                                                NoteID = qnote.NoteID,
                                                                Header = qnote.Header,
                                                                Note = qnote.Note,
                                                                Date = qnote.Date,
                                                                CollectionNameText = collectionName.CollectionNameText
                                                            }; 
                return notes;
            }
            catch (Exception)
            {
                // If by some reason my FormView fails to see there are no posts, ModelState saves the day!
                ModelState.AddModelError("", "Ett fel inträffade då anteckningar skulle hämtas, försök igen om en stund!");
                QnoteListView.Visible = false;
                return null;
            }  
        }
    }
}