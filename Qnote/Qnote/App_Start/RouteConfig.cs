﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Qnote
{
    // Class for handling all my route configurations, making sure to keep the adressbar tidy and readable.
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("Default", "", "~/Pages/Default.aspx");
            routes.MapPageRoute("NewNote", "notes/new", "~/Pages/NewNote.aspx");
            routes.MapPageRoute("AllNotes", "notes/all", "~/Pages/AllNotes.aspx");
            routes.MapPageRoute("SingleNote", "notes/{id}/{header}", "~/Pages/SingleNote.aspx");
            routes.MapPageRoute("UpdateNote", "edit/notes/{id}", "~/Pages/UpdateNote.aspx");
            routes.MapPageRoute("DeleteNote", "notes/delete/{id}/{header}", "~/Pages/DeleteNote.aspx");
            routes.MapPageRoute("Collections", "collections", "~/Pages/Collections.aspx");
            routes.MapPageRoute("DeleteCollection", "collections/delete/{id}/{header}", "~/Pages/DeleteCollection.aspx");
            routes.MapPageRoute("About", "about", "~/Pages/About.aspx");
        }
    }
}