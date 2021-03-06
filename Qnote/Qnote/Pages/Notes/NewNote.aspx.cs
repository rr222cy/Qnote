﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Qnote.Models;

namespace Qnote.Pages
{
    public partial class NewNote : System.Web.UI.Page
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

        public IEnumerable<CollectionName> DropDownListCollection_GetData()
        {
            try
            {
                // Retrieves all the collectionnames and sends them to the dropdownlist.
                return Service.GetCollectionNames();
            }
            catch (Exception)
            {
                // If something goes wrong, ModelState saves the day by presenting a error that the user can DO NOTHING ABOUT muahaha.
                ModelState.AddModelError("", "Ett fel inträffade då samlingar skulle hämtas, försök skriva en ny anteckning om en stund!");
                FormViewNewNote.Visible = false;
                return null;
            }          
        }

        public void FormViewNewNote_InsertItem(Models.QnoteCollectionID qnoteCollectionID)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Adds the UserID to the object
                    qnoteCollectionID.UserID = Int32.Parse(Session["userID"].ToString());

                    // Saves the note.
                    Service.CreateNoteAndCollection(qnoteCollectionID);

                    // Sets the confirm-message and redirects to the notelist.
                    Session["Success"] = "Anteckningen har nu sparats!";

                    Response.RedirectToRoute("AllNotes");
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (Exception)
                {
                    // If something goes wrong, ModelState saves the day by presenting a error that the user can DO NOTHING ABOUT muahaha.
                    ModelState.AddModelError("", "Ett fel inträffade då anteckningen skulle läggas till, försök igen om en stund!");
                }                   
            }
        }
    }
}