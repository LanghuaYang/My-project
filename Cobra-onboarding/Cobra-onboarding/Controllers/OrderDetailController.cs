﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cobra_onboarding;

namespace Cobra_onboarding.Controllers
{
    public class OrderDetailController : Controller
    {
        private CobraDBContext db = new CobraDBContext();

        // GET: OrderDetail
        //public ActionResult Index()
        //{
        //    var orderDetails = db.OrderDetails.Include(o => o.OrderHeader).Include(o => o.Product);
        //    return View(orderDetails.ToList());
        //}

        public ActionResult Index()
        {
            return View();
        }

        // GET: OrderDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: OrderDetail/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.OrderHeaders, "OrderId", "OrderId");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            return View();
        }

        // POST: OrderDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderId,ProductId")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(db.OrderHeaders, "OrderId", "OrderId", orderDetail.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: OrderDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.OrderHeaders, "OrderId", "OrderId", orderDetail.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // POST: OrderDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderId,ProductId")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.OrderHeaders, "OrderId", "OrderId", orderDetail.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: OrderDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
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
