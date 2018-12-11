using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using FinalP10.Models;

namespace FinalP10.Controllers
{
    public class CLIENTEController : Controller
    {
        private Ciudad_DollarEntities1 db = new Ciudad_DollarEntities1();

        // GET: CLIENTE
        public ActionResult Index()
        {
            //var cLIENTE = db.CLIENTE.Include(c => c.PIB1).Include(c => c.PRODUCTO1).Include(c => c.UBICACION1);
            //return View(cLIENTE.ToList());

            IEnumerable<CLIENTE> clientes = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52161//api/");
                //GET GETAlumnos
                //el siguente codigo obtiene la informacion de manera asincrona y espera hata obtener la data
                var reponseTask = client.GetAsync("clienteapi");
                reponseTask.Wait();
                var result = reponseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // leer todo el cotenido y lo parseamos a una lista de alumno
                    var leer = result.Content.ReadAsAsync<IList<CLIENTE>>();
                    leer.Wait();
                    clientes = leer.Result;
                }
                else
                {
                    clientes = Enumerable.Empty<CLIENTE>();
                    ModelState.AddModelError(string.Empty, "Error...");
                }

            }
            return View(clientes.ToList());
        }

        // GET: CLIENTE/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //CLIENTE cLIENTE = db.CLIENTE.Find(id);
            //if (cLIENTE == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(cLIENTE);
            CLIENTE clientes = new CLIENTE();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52161/api/");

                // Obtiene asincronamente y esepera hata obteneer la data
                var responsetask = client.GetAsync("clienteapi/" + id);
                responsetask.Wait();
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //leer todo el contenido y parsearlo a la lista
                    var leer = result.Content.ReadAsAsync<CLIENTE>();
                    leer.Wait();
                    clientes = leer.Result;
                }
                else
                {
                    clientes = null;
                    ModelState.AddModelError(string.Empty, "Error...");
                }
            }
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // GET: CLIENTE/Create
        public ActionResult Create()
        {
            ViewBag.Pib = new SelectList(db.PIB, "Id", "Id");
            ViewBag.Producto = new SelectList(db.PRODUCTO, "Id", "Descripcion");
            ViewBag.Ubicacion = new SelectList(db.UBICACION, "Id", "Id");
            return View();
        }

        // POST: CLIENTE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Empresa,Fecha,Donacion,Pib,Ubicacion,Producto,Total")] CLIENTE cLIENTE)
        {
            //if (ModelState.IsValid)
            //{
            


            //    db.CLIENTE.Add(cLIENTE);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.Pib = new SelectList(db.PIB, "Id", "Id", cLIENTE.Pib);
            //ViewBag.Producto = new SelectList(db.PRODUCTO, "Id", "Descripcion", cLIENTE.Producto);
            //ViewBag.Ubicacion = new SelectList(db.UBICACION, "Id", "Id", cLIENTE.Ubicacion);
            //return View(cLIENTE);
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:52161/api/");
                    //HTTP Post
                    REPORTE rp = new REPORTE();
                    if (cLIENTE.Pib == 1)
                    {

                        cLIENTE.Total = ((cLIENTE.Donacion * 5) / 100);
                    }
                    else if (cLIENTE.Pib == 2)
                    {
                        cLIENTE.Total = ((cLIENTE.Donacion * 10) / 100);
                    }
                    cLIENTE.Fecha = DateTime.Now;
                    var postTask = client.PostAsJsonAsync<CLIENTE>("clienteapi", cLIENTE);
                    postTask.Wait();


                    var resul = postTask.Result;
                    if (resul.IsSuccessStatusCode)
                    {

                       return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Error en la insercción,favor contacte al administrador");
            }
            return RedirectToAction("Index");
        }

        // GET: CLIENTE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENTE cLIENTE = db.CLIENTE.Find(id);
            if (cLIENTE == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pib = new SelectList(db.PIB, "Id", "Id", cLIENTE.Pib);
            ViewBag.Producto = new SelectList(db.PRODUCTO, "Id", "Descripcion", cLIENTE.Producto);
            ViewBag.Ubicacion = new SelectList(db.UBICACION, "Id", "Id", cLIENTE.Ubicacion);
            return View(cLIENTE);
            //CLIENTE clientes = new CLIENTE();
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://localhost:52161/api/");

            //    // Obtiene asincronamente y esepera hata obteneer la data
            //    var responsetask = client.GetAsync("clienteapi/" + id);
            //    responsetask.Wait();
            //    var result = responsetask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        //leer todo el contenido y parsearlo a la lista
            //        var leer = result.Content.ReadAsAsync<CLIENTE>();
            //        leer.Wait();
            //        clientes = leer.Result;
            //    }
            //    else
            //    {
            //        clientes = null;
            //        ModelState.AddModelError(string.Empty, "Error...");
            //    }
            //}
            //if (clientes == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(clientes);


        }

        // POST: CLIENTE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Empresa,Fecha,Donacion,Pib,Ubicacion,Producto,Total")] CLIENTE cLIENTE)
        {
            if (ModelState.IsValid)
            {
                  if (cLIENTE.Pib == 1)
                {

                    cLIENTE.Total = ((cLIENTE.Donacion * 5) / 100);
                }
                else if (cLIENTE.Pib == 2)
                {
                    cLIENTE.Total = ((cLIENTE.Donacion * 10) / 100);
                }
                  cLIENTE.Fecha = DateTime.Now;
                db.Entry(cLIENTE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Pib = new SelectList(db.PIB, "Id", "Id", cLIENTE.Pib);
            ViewBag.Producto = new SelectList(db.PRODUCTO, "Id", "Descripcion", cLIENTE.Producto);
            ViewBag.Ubicacion = new SelectList(db.UBICACION, "Id", "Id", cLIENTE.Ubicacion);
            return View(cLIENTE);
            //if (ModelState.IsValid)
            //{
            //    using (var client = new HttpClient())
            //    {
            //        client.BaseAddress = new Uri("http://localhost:52161/api/");
            //            //HTTP Post

            //            if (cLIENTE.Pib == 1)
            //            {

            //                cLIENTE.Total = ((cLIENTE.Donacion * 5) / 100);
            //            }
            //            else if (cLIENTE.Pib == 2)
            //            {
            //                cLIENTE.Total = ((cLIENTE.Donacion * 10) / 100);
            //            }
            //            cLIENTE.Fecha = DateTime.Now;

            //            var postTask = client.PutAsJsonAsync<CLIENTE>("clienteapi", cLIENTE);
            //        postTask.Wait();

            //        var resul = postTask.Result;
            //        if (resul.IsSuccessStatusCode)
            //        {
            //                return RedirectToAction("Index");
            //        }
            //    }
            //    ModelState.AddModelError(string.Empty, "Error en la insercción,favor contacte al administrador");
            //}
            //return RedirectToAction("Index");
        }

        // GET: CLIENTE/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //CLIENTE cLIENTE = db.CLIENTE.Find(id);
            //if (cLIENTE == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(cLIENTE);
            CLIENTE clientes = new CLIENTE();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52161/api/");

                // Obtiene asincronamente y esepera hata obteneer la data
                var responsetask = client.GetAsync("clienteapi/" + id);
                responsetask.Wait();
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //leer todo el contenido y parsearlo a la lista
                    var leer = result.Content.ReadAsAsync<CLIENTE>();
                    leer.Wait();
                    clientes = leer.Result;
                }
                else
                {
                    clientes = null;
                    ModelState.AddModelError(string.Empty, "Error...");
                }
            }
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // POST: CLIENTE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //CLIENTE cLIENTE = db.CLIENTE.Find(id);
            //db.CLIENTE.Remove(cLIENTE);
            //db.SaveChanges();
            //return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:52161/api/");
                    //HTTP Post
                    var postTask = client.DeleteAsync("clienteapi/" + id.ToString());
                    postTask.Wait();

                    var resul = postTask.Result;
                    if (resul.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Error en la insercción,favor contacte al administrador");
            }
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
