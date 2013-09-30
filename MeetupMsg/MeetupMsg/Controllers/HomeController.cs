using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.OAuth2;
using Google.Apis.Mirror.v1;

namespace MeetupMsg.Controllers
    {
        public class HomeController : Controller
        {
            public ActionResult Index()
            {
            Log.D("HomeController Index");
                return View();
            }
        }

    }
