//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.Entity;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using ImageEdit.Models;
//using Microsoft.AspNet.Identity;

//namespace ImageEdit.Controllers
//{
//    public class PhotosController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        // GET: Photos
//        public ActionResult Index()
//        {
//            var photos = db.Photos.ToList();
//            //foreach(var photo in photos)
//            //{
//            //    photo.ImagePath = ConfigurationManager.AppSettings["ImagePathBase"] + photo.ImagePath;
//            //}
//            return View(photos);
//        }

//        public ActionResult MyPics()
//        {
//            var id = User.Identity.GetUserId();
//            var photos = db.Photos.Where(x => x.UserId == id).ToList();
//            //foreach(var photo in photos)
//            //{
//            //    photo.ImagePath = ConfigurationManager.AppSettings["ImagePathBase"] + photo.ImagePath;
//            //}
//            return View(photos);
//        }

//        // GET: Photos/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            var vm = new PhotosDetailsViewModel();
//            vm.Photo = db.Photos.Find(id);
//            if (vm.Photo == null)
//            {
//                return HttpNotFound();
//            }
//            vm.Category = db.Categories.Find(vm.Photo.CategoryId);

//            return View(vm);
//        }

//        // GET: Photos/Create
//        public ActionResult Create()
//        {
//            var vm = new PhotosCreateViewModel();
//            vm.Categories = db.Categories.ToList();
//            return View(vm);
//        }

//        // POST: Photos/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create(PhotosCreateViewModel vm)
//        {
//            //if (ModelState.IsValid)
//            //{
//            //    db.Photos.Add(vm.Photo);
//            //    db.SaveChanges();
//            //    return RedirectToAction("Index");
//            //}

//            //return View(vm);
//            if (ModelState.IsValid)
//            {
//                var photo = db.Photos.FirstOrDefault(p => p.Id == vm.Photo.Id);
//                var userId = User?.Identity?.GetUserId();

//                if (!string.IsNullOrEmpty(userId))
//                {
//                    if (photo.UserId == userId)
//                    {
//                        photo.CategoryId = vm.Photo.CategoryId;
//                    }
//                    else
//                    {
//                        var fileName = User.Identity.GetUserId() + "-" + DateTime.Now.ToString("yyyyMMddTHHmmss") + "-" + photo.ImagePath;

//                        string userDir = AppDomain.CurrentDomain.BaseDirectory + "UserImages";
//                        string genDir = AppDomain.CurrentDomain.BaseDirectory + "jpeg";

//                        System.IO.File.Copy(Path.Combine(genDir, photo.ImagePath), Path.Combine(userDir, fileName), true);

//                        var newPhoto = new Photo
//                        {
//                            CategoryId = vm.Photo.CategoryId,
//                            UserId = userId,
//                            ImagePath = fileName
//                        };

//                        db.Photos.Add(newPhoto);

//                    }
//                    //vm.Photo.UserId = User.Identity.GetUserId();
//                    //db.Entry(vm.Photo).State = EntityState.Modified;
//                    db.SaveChanges();

//                    return RedirectToAction("MyPics");
//                }
//            }
//            return View(vm);
//        }

//        // GET: Photos/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Photo photo = db.Photos.Find(id);
//            if (photo == null)
//            {
//                return HttpNotFound();
//            }
//            var vm = new PhotosCreateViewModel();
//            vm.Photo = photo;
//            vm.Categories = db.Categories.ToList();

//            return View(vm);
//        }

//        // POST: Photos/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit(PhotosCreateViewModel vm)
//        {

//            if (ModelState.IsValid)
//            {
//                var phone = db.Photos.FirstOrDefault(p => p.Id == vm.Photo.Id);

//                phone.CategoryId = vm.Photo.CategoryId;
//                //vm.Photo.UserId = User.Identity.GetUserId();
//                //db.Entry(vm.Photo).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(vm);
//        }
//        [HttpPost]
//        public void UploadImage(UploadImageModel model)
//        {
//            const string prefix = "data:image/png;base64,";
//            string path = AppDomain.CurrentDomain.BaseDirectory + "UserImages";
//            var fileName = User.Identity.GetUserId() + "-" + DateTime.Now.ToString("yyyyMMddTHHmmss") + "-" + model.FileName;

//            Photo photo = new Photo();
//            photo.UserId = User.Identity.GetUserId();
//            photo.ImagePath = fileName;
//            photo.CategoryId = 1; // category is not passed in update the model to add it
//            db.Photos.Add(photo);
//            db.SaveChanges();

//            using (FileStream fs = new FileStream(Path.Combine(path, fileName), FileMode.Create))
//            {
//                using (BinaryWriter bw = new BinaryWriter(fs))
//                {
//                    string base64;
//                    if (model.Image.StartsWith(prefix))
//                    {
//                        base64 = model.Image.Substring(prefix.Length);
//                    }
//                    else
//                    {
//                        base64 = model.Image;
//                    }
//                    byte[] data = Convert.FromBase64String(base64);
//                    bw.Write(data);
//                    bw.Close();
//                }
//            }

//        }

//        // GET: Photos/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Photo photo = db.Photos.Find(id);
//            if (photo == null)
//            {
//                return HttpNotFound();
//            }
//            return View(photo);
//        }

//        // POST: Photos/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Photo photo = db.Photos.Find(id);
//            db.Photos.Remove(photo);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        public ActionResult RemovePhoto(int id)
//        {
//            Photo photo = db.Photos.Find(id);
//            photo.UserId = null;
//            db.SaveChanges();
//            return RedirectToAction("MyPics");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}

using ImageEdit.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ImageEdit.Controllers
{
    public class PhotosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Photos
        public ActionResult Index()
        {
            var photos = db.Photos.ToList();
            //foreach(var photo in photos)
            //{
            //    photo.ImagePath = ConfigurationManager.AppSettings["ImagePathBase"] + photo.ImagePath;
            //}
            return View(photos);
        }

        public ActionResult MyPics()
        {
            var id = User.Identity.GetUserId();
            var photos = db.Photos.Where(x => x.UserId == id).ToList();
            //foreach(var photo in photos)
            //{
            //    photo.ImagePath = ConfigurationManager.AppSettings["ImagePathBase"] + photo.ImagePath;
            //}
            return View(photos);
        }

        // GET: Photos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vm = new PhotosDetailsViewModel
            {
                Photo = db.Photos.Find(id)
            };
            if (vm.Photo == null)
            {
                return HttpNotFound();
            }
            vm.Category = db.Categories.Find(vm.Photo.CategoryId);

            return View(vm);
        }

        // GET: Photos/Create
        public ActionResult Create()
        {
            var vm = new PhotosCreateViewModel
            {
                Categories = db.Categories.ToList()
            };
            return View(vm);
        }

        // POST: Photos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PhotosCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                db.Photos.Add(vm.Photo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: Photos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            var vm = new PhotosCreateViewModel
            {
                Photo = photo,
                Categories = db.Categories.ToList()
            };

            return View(vm);
        }

        // POST: Photos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PhotosCreateViewModel vm)
        { 
            //if (ModelState.IsValid) hello?
            //{
            //    vm.Photo.UserId = User.Identity.GetUserId();
            //    db.Entry(vm.Photo).State = EntityState.Modified;
            //    //db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            // return View(vm);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public void UploadImage(UploadImageModel model)
        {
            const string prefix = "data:image/png;base64,";
            string path = AppDomain.CurrentDomain.BaseDirectory + "UserImages";
            var fileName = User.Identity.GetUserId() + "-" + DateTime.Now.ToString("yyyyMMddTHHmmss") + "-" + model.FileName;

            var photo = db.Photos.FirstOrDefault(p => p.Id == model.Id);
            var userId = User?.Identity?.GetUserId();

            if (photo == null || photo.UserId != userId)
            {
                photo = new Photo
                {
                    UserId = User.Identity.GetUserId(),
                    ImagePath = fileName,
                    CategoryId = model.CategoryId // category is not passed in update the model to add it
                };
                db.Photos.Add(photo);
            }
            else
            {
                photo.ImagePath = fileName;
            }

            db.SaveChanges();

            using (FileStream fs = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    string base64;
                    if (model.Image.StartsWith(prefix))
                    {
                        base64 = model.Image.Substring(prefix.Length);
                    }
                    else
                    {
                        base64 = model.Image;
                    }
                    byte[] data = Convert.FromBase64String(base64);
                    bw.Write(data);
                    bw.Close();
                }
            }

        }

        // GET: Photos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = db.Photos.Find(id);
            db.Photos.Remove(photo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult RemovePhoto(int id)
        {
            Photo photo = db.Photos.Find(id);
            photo.UserId = null;
            db.SaveChanges();
            return RedirectToAction("MyPics");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}