using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalP10.Models;

namespace FinalP10.Controllers
{
    public class UBICACIONController : Controller
    {
        private Ciudad_DollarEntities1 db = new Ciudad_DollarEntities1();

        // GET: UBICACION
        public ActionResult Index()
        {
            var uBICACION = db.UBICACION.Include(u => u.CIUDAD1).Include(u => u.CONTINENTE1).Include(u => u.PAIS1);
            return View(uBICACION.ToList());
        }

        // GET: UBICACION/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UBICACION uBICACION = db.UBICACION.Find(id);
            if (uBICACION == null)
            {
                return HttpNotFound();
            }
            return View(uBICACION);
        }

        // GET: UBICACION/Create
        public ActionResult Create()
        {
            ViewBag.Ciudad = new SelectList(db.CIUDAD, "Id", "Descripcion");
            ViewBag.Continente = new SelectList(db.CONTINENTE, "Id", "Descripcion");
            ViewBag.Pais = new SelectList(db.PAIS, "Id", "Descripcion");
            return View();
        }

        // POST: UBICACION/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Continente,Pais,Ciudad")] UBICACION uBICACION)
        {
            if (ModelState.IsValid)
            {
                db.UBICACION.Add(uBICACION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ciudad = new SelectList(db.CIUDAD, "Id", "Descripcion", uBICACION.Ciudad);
            ViewBag.Continente = new SelectList(db.CONTINENTE, "Id", "Descripcion", uBICACION.Continente);
            ViewBag.Pais = new SelectList(db.PAIS, "Id", "Descripcion", uBICACION.Pais);
            return View(uBICACION);
        }

        // GET: UBICACION/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UBICACION uBICACION = db.UBICACION.Find(id);
            if (uBICACION == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ciudad = new SelectList(db.CIUDAD, "Id", "Descripcion", uBICACION.Ciudad);
            ViewBag.Continente = new SelectList(db.CONTINENTE, "Id", "Descripcion", uBICACION.Continente);
            ViewBag.Pais = new SelectList(db.PAIS, "Id", "Descripcion", uBICACION.Pais);
            return View(uBICACION);
        }

        // POST: UBICACION/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Continente,Pais,Ciudad")] UBICACION uBICACION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uBICACION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ciudad = new SelectList(db.CIUDAD, "Id", "Descripcion", uBICACION.Ciudad);
            ViewBag.Continente = new SelectList(db.CONTINENTE, "Id", "Descripcion", uBICACION.Continente);
            ViewBag.Pais = new SelectList(db.PAIS, "Id", "Descripcion", uBICACION.Pais);
            return View(uBICACION);
        }

        // GET: UBICACION/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UBICACION uBICACION = db.UBICACION.Find(id);
            if (uBICACION == null)
            {
                return HttpNotFound();
            }
            return View(uBICACION);
        }

        // POST: UBICACION/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UBICACION uBICACION = db.UBICACION.Find(id);
            db.UBICACION.Remove(uBICACION);
            db.SaveChanges();
            return RedirectToAction("Index");
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
