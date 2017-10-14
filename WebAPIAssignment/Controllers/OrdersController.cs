using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAPIAssignment.Models;
using Utility;
using System.Web.Http;


namespace WebAPIAssignment.Controllers
{
    public class OrdersController : Controller
    {
        private WebAPIAssignmentContext db = new WebAPIAssignmentContext();

      public ActionResult List()
        {
            return Json(db.Orders.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Json(new Msg { Result = "Failure", Message = "Id is null" });
            }
            Order order = db.Orders.Find(id);
            if(order == null)
            {
                return Json(new Msg { Result = "Failure", Message = "Id not found" });
            }
            return Json(order, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Add([FromBody] Order order)
        {
            if (order == null || order.OrderNbr == null)
                {
                return Json(new Msg { Result = "Failure", Message = "Order parameters not found" });
                }
            db.Orders.Add(order);
            db.SaveChanges();
            return Json(new Msg { Result = "Success", Message = "Add successful order" });
        }

        public ActionResult Change([FromBody] Order order)
        {
            if (order == null || order.OrderNbr == null)
            {
                return Json(new Msg { Result = "Failure", Message = "Order parameters not found" });
            }
            Order tempOrder = db.Orders.Find(order.Id);
            tempOrder.OrderNbr = order.OrderNbr;
            tempOrder.DateReceived = order.DateReceived;
            tempOrder.CustomerId = order.CustomerId;
            tempOrder.Total = order.Total;
            db.SaveChanges();
            return Json(new Msg { Result = "Success", Message = "Change Successful" });
        }


        public ActionResult Remove([FromBody] Order order) 
        {
            if (order == null || order.Id <= 0)
            {
                return Json(new Msg { Result = "Failure", Message = "Order parameter is missing or invalid" });
            }
           
            Order tempOrder = db.Orders.Find(order.Id);
            if (tempOrder == null) 
            {
                return Json(new Msg { Result = "Failure", Message = "Order Id not found." });
            }
            db.Orders.Remove(tempOrder);
            db.SaveChanges();
            return Json(new Msg { Result = "Success", Message = "Change Successful." });
        }

        #region MVC Methods
        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderNbr,DateReceived,CustomerId,Customer,Total")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderNbr,DateReceived,CustomerId,Customer,Total")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
#endregion
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
