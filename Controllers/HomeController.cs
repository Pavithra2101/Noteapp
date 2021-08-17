using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Noteapp.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Noteapp.Controllers
{
    public class HomeController : Controller
    { 

     private readonly NoteWebdbContext db= new();
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }
       
        [HttpPost]
        public JsonResult UpdateNote(Notes note)
        {
            Notes oldNoteModel = db.Notes.Find(note.Noteid);
            if (oldNoteModel == null)
            {
                //Add new
                note.Noteid = Guid.NewGuid();
                note.Notedate = DateTime.UtcNow;
                db.Notes.Add(note);
                db.SaveChanges();
            }
            else
            {
                //Update
                note.Notedate = DateTime.UtcNow;
                db.Entry(oldNoteModel).CurrentValues.SetValues(note);
                db.SaveChanges();
            }

            return Json(note);

        }

        [HttpGet]
        public IActionResult _note_list()
        {
            var result = db.Notes.OrderByDescending(n => n.Notedate).AsQueryable();
           
            return PartialView(result);
        }
        [HttpGet]
        //public IActionResult SearchAction(string Searching)
        //{
        //    var searchrst = db.Notes.Where(x => x.Notedetails.Contains(Searching) || Searching == null).ToList(); 
        
        //    return View(searchrst);
        //}
        //[HttpGet]
        //public IActionResult Index(string searchdetails)
        //{
        //    ViewData["GetDetailSearch"] = searchdetails;
        //    var search = from x in db.Notes select x;
        //    if (!String.IsNullOrEmpty(searchdetails))
        //    {
        //        search = search.Where(x => x.Notedetails.Contains(searchdetails));
        //    }
        //    return View(search.AsNoTracking().ToListAsync());
        //}
        [HttpGet]
        public JsonResult GetNoteDetailById(string noteid)
        {
            var result = db.Notes.Find(new Guid(noteid));
            return Json(result);
        }

        [HttpGet]
        public JsonResult DeleteNoteById(string noteid)
        {
            Notes note = db.Notes.Find(new Guid(noteid));
            db.Entry(note).State = EntityState.Deleted;
            db.SaveChanges();

            return Json("Successfully deleted.");
        }

    }
        }

    


      