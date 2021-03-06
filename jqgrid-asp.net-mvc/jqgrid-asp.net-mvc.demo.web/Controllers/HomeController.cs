﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jqgrid_asp.net_mvc.demo.web.Models;
using jqgrid_asp.net_mvc;

namespace jqgrid_asp.net_mvc.demo.web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private CRUDDemoDBContext db = new CRUDDemoDBContext();

        #region jqgrid read and search

        public ActionResult IndexJsonList(bool _search, string nd, int? rows, int? page, string sidx, string sord, jqgrid_asp.net_mvc.Filter filters)
        {
            return JqGrid.Load(
                db.Persons, //Please replace to your business logic code
                d => d.FirstName,//Please replace to your business logic code
                s => new //Please replace to your business logic code
                {                    
                    s.ID,
                    s.FirstName,
                    s.LastName,
                    s.City,
                    s.Zip,

                }, rows, page, _search, ref filters);

        }

        #endregion

        #region jqgrid add, edit, delete

        public ActionResult UpdateForJqGrid(Person person, string oper)
        {
            return JqGrid.UpdateForJqGrid(person, oper, AddPerson, EditPerson, DeletePerson);
        }

        private ActionResult DeletePerson(Person person)
        {
            //Please replace to your business logic code
            var deletepersonentity = db.Persons.Single(p => p.ID == person.ID);
            db.Persons.Remove(deletepersonentity);

            db.SaveChanges();

            return Content("Delete success");
        }

        private ActionResult EditPerson(Person person)
        {
            //Please replace to your business logic code
            var editpersonentity = db.Persons.Single(p => p.ID == person.ID);
            editpersonentity.FirstName = person.FirstName;
            editpersonentity.LastName = person.LastName;
            editpersonentity.City = person.City;
            editpersonentity.Zip = person.Zip;

            db.SaveChanges();

            return Content("Update success");

        }

        private ActionResult AddPerson(Person person)
        {
            //Please replace to your business logic code
            var newpersonentity = new Person();
            newpersonentity.FirstName = person.FirstName;
            newpersonentity.LastName = person.LastName;
            newpersonentity.City = person.City;
            newpersonentity.Zip = person.Zip;

            db.Persons.Add(newpersonentity);
            db.SaveChanges();

            return Content("Add success");
        }

        #endregion
    }
}