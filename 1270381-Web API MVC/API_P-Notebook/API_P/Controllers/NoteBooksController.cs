using API_P.Models;
using API_P.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
namespace API_P.Controllers
{
    public class NoteBooksController : ApiController
    {
        private NoteBookDbContext db = new NoteBookDbContext();
        [HttpGet]
        public IQueryable<NoteBook> GetNoteBooks()
        {
            return db.NoteBooks.Include(x => x.Configurations).AsQueryable();
        }
        [HttpGet]
        public IHttpActionResult GetNoteBook(int id)
        {
            var d = db.NoteBooks.Include(x => x.Configurations).FirstOrDefault(x => x.NoteBookId == id);
            if (d != null)
                return Ok(d);
            else
                return NotFound();
        }
        [HttpPost]
        public IHttpActionResult PostNoteBook(NoteBookInputModel model)
        {
            if (ModelState.IsValid)
            {
                var NoteBook = new NoteBook
                {
                    NoteBookModel = model.NoteBookModel,
                    ManufactureDate = model.ManufactureDate,
                    Price = model.Price,
                    Picture = model.Picture,
                    Instock = model.Instock
                };
                model.Configurations.ForEach(s =>
                {
                    NoteBook.Configurations.Add(new Configuration { ConfigurationDetails = s.ConfigurationDetails, BrandCode = s.BrandCode });
                });
                db.NoteBooks.Add(NoteBook);
                db.SaveChanges();
                return Ok(NoteBook);
            }
            return BadRequest("Data invalid");
        }
        [HttpPut]
        public IHttpActionResult PutNoteBook(int id, NoteBookEditModel model)
        {
            if (id != model.NoteBookId) return BadRequest("Id mismatch");
            if (ModelState.IsValid)
            {
                var NoteBook = db.NoteBooks.Include(x => x.Configurations).First(x => x.NoteBookId == id);
                if (NoteBook == null) return NotFound();
                NoteBook.NoteBookModel = model.NoteBookModel;
                NoteBook.ManufactureDate = model.ManufactureDate;
                NoteBook.Price = model.Price;
                NoteBook.Instock = model.Instock;
                NoteBook.Picture = model.Picture;
                db.Configurations.RemoveRange(NoteBook.Configurations);
                model.Configurations.ForEach(s =>
                {
                    NoteBook.Configurations.Add(new Configuration { ConfigurationDetails = s.ConfigurationDetails, BrandCode = s.BrandCode });
                });
                db.SaveChanges();
                return Ok(NoteBook);
            }
            return BadRequest("Data invalid");
        }
        [HttpDelete]
        public IHttpActionResult DeleteNoteBook(int id)
        {
            var d = db.NoteBooks.FirstOrDefault(x => x.NoteBookId == id);
            if (d == null) return NotFound();
            db.NoteBooks.Remove(d);
            db.SaveChanges();
            return Ok("Deleted");
        }
        [Route("Image/Upload")]
        [HttpPost]
        public IHttpActionResult Upload()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ?
        HttpContext.Current.Request.Files[0] : null;
            if (file != null && file.ContentLength > 0)
            {
                string ext = Path.GetExtension(file.FileName);
                string f = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                string savePath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Pictures"), f);
                file.SaveAs(savePath);
                return Ok(f);
            }
            return BadRequest();
        }
    }
}
