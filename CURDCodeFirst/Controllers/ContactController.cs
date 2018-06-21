using CF.Data;
using CF.Repo;
using CURDCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CURDCodeFirst.Controllers
{
    public class ContactController : Controller
    {
        private readonly UnitOfWork unitOfWork;
        private Repository<Contact> contactRepository;

        public ContactController()
        {
            unitOfWork = new UnitOfWork();
            contactRepository = unitOfWork.Repository<Contact>();
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<ContactViewModel> contacts = contactRepository.Table.AsEnumerable().Select(s => new ContactViewModel
            {
                Id = s.Id,
                Name = $"{s.FirstName} {s.LastName}",
                Email = s.Email,
                PhoneNo = s.PhoneNumber
            });
            return View(contacts);
        }

        [HttpGet]
        public PartialViewResult AddEditContact(long? id)
        {
            ContactViewModel model = new ContactViewModel();
            if (id.HasValue)
            {
                Contact contact = contactRepository.GetById(id.Value);
                model.Id = contact.Id;
                model.FirstName = contact.FirstName;
                model.LastName = contact.LastName;
                model.PhoneNo = contact.PhoneNumber;
                model.Email = contact.Email;
            }
            return PartialView("~/Views/Contact/_AddEditContact.cshtml", model);
        }

        [HttpPost]
        public ActionResult AddEditContact(long? id, ContactViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isNew = !id.HasValue;
                    Contact contact = isNew ? new Contact
                    {
                        AddedDate = DateTime.UtcNow
                    } : contactRepository.GetById(id.Value);
                    contact.FirstName = model.FirstName;
                    contact.LastName = model.LastName;
                    contact.PhoneNumber = model.PhoneNo;
                    contact.Email = model.Email;
                    contact.IPAddress = Request.UserHostAddress;
                    contact.ModifiedDate = DateTime.UtcNow;
                    if (isNew)
                    {
                        contactRepository.Insert(contact);
                    }
                    else
                    {
                        contactRepository.Update(contact);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public PartialViewResult DeleteContact(long id)
        {
            Contact contact = contactRepository.GetById(id);
            ContactViewModel model = new ContactViewModel
            {
                Name = $"{contact.FirstName} {contact.LastName}"
            };
            return PartialView("~/Views/Contact/_DeleteContact.cshtml", model);
        }
        [HttpPost]
        public ActionResult DeleteContact(long id, FormCollection form)
        {
            Contact contact = contactRepository.GetById(id);
            contactRepository.Delete(contact);
            return RedirectToAction("Index");
        }
    }
}