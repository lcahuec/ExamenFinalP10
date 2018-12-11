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
    public class REPORTEController : Controller
    {
        private Ciudad_DollarEntities1 db = new Ciudad_DollarEntities1();

        // GET: REPORTE
        public ActionResult Index()
        {
            var rEPORTE = db.REPORTE.Include(r => r.CLIENTE1).Include(r => r.SERVICIO1);
            return View(rEPORTE.ToList());
        }

        // GET: REPORTE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REPORTE rEPORTE = db.REPORTE.Find(id);
            if (rEPORTE == null)
            {
                return HttpNotFound();
            }
            return View(rEPORTE);
        }

        // GET: REPORTE/Create
        public ActionResult Create()
        {
            ViewBag.Cliente = new SelectList(db.CLIENTE, "Id", "Nombre");
            ViewBag.Servicio = new SelectList(db.SERVICIO, "Id", "Id");
            ViewBag.Cliente = new SelectList(db.CLIENTE, "Id", "Total");

            return View();
        }

        // POST: REPORTE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Servicio,Cliente,IVA,ISR,AHORRO,MESES,TOTAL")] REPORTE rEPORTE)
        {
            if (ModelState.IsValid)
            {
                CLIENTE cl = new CLIENTE();

                if (((cl.Total) <= 30000))
                {
                    rEPORTE.ISR = ((cl.Total) * 5) / 100;

                }
                else if (((cl.Total) > 30000))
                {
                    rEPORTE.ISR = ((cl.Total) * 7) / 100;

                }

                rEPORTE.IVA = ((cl.Total) * 12) / 100;
                rEPORTE.AHORRO = ((cl.Total) * 5) / 100;
                rEPORTE.TOTAL = ((cl.Total)-(rEPORTE.IVA)-(rEPORTE.ISR)-(rEPORTE.AHORRO)) * rEPORTE.MESES;

                db.REPORTE.Add(rEPORTE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cliente = new SelectList(db.CLIENTE, "Id", "Nombre", rEPORTE.Cliente);
            ViewBag.Servicio = new SelectList(db.SERVICIO, "Id", "Id", rEPORTE.Servicio);
            return View(rEPORTE);
        }

        // GET: REPORTE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REPORTE rEPORTE = db.REPORTE.Find(id);
            if (rEPORTE == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cliente = new SelectList(db.CLIENTE, "Id", "Nombre", rEPORTE.Cliente);
            ViewBag.Servicio = new SelectList(db.SERVICIO, "Id", "Id", rEPORTE.Servicio);
            return View(rEPORTE);
        }

        // POST: REPORTE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Servicio,Cliente,IVA,ISR,AHORRO,MESES,TOTAL")] REPORTE rEPORTE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rEPORTE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cliente = new SelectList(db.CLIENTE, "Id", "Nombre", rEPORTE.Cliente);
            ViewBag.Servicio = new SelectList(db.SERVICIO, "Id", "Id", rEPORTE.Servicio);
            return View(rEPORTE);
        }

        // GET: REPORTE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REPORTE rEPORTE = db.REPORTE.Find(id);
            if (rEPORTE == null)
            {
                return HttpNotFound();
            }
            return View(rEPORTE);
        }

        // POST: REPORTE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            REPORTE rEPORTE = db.REPORTE.Find(id);
            db.REPORTE.Remove(rEPORTE);
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
