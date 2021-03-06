﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RestSharp;
using SpaUserControl.Web.Models;

namespace SpaUserControl.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Username, string Password)
        {
            var client = new RestClient("http://localhost:12295");

            var request = new RestRequest("api/security/token", Method.POST);
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", Username);
            request.AddParameter("password", Password);

            IRestResponse<TokenViewModel> response = client.Execute<TokenViewModel>(request);
            var token = response.Data.access_token;

            if (!String.IsNullOrEmpty(token))
            {
                FormsAuthentication.SetAuthCookie(token, false);
                return RedirectToAction("Sistemas");
            }
            else
            return RedirectToAction("Index");


        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }



        public ActionResult About()
        {
            ViewBag.Message = "Stanley Sistemas.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contato.";

            return View();
        }

        public ActionResult Sistemas()
        {
            ViewBag.Message = "Sistemas";

            return View();
        }
    }


}