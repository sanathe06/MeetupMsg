using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Google.Apis.Mirror.v1;
using Google.Apis.Mirror.v1.Data;
using Google.Apis.Services;
using MeetupMsg.Models;

namespace MeetupMsg.Controllers
{
    public class MeetupController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("SendMsg")]
        public ActionResult SendMsg()
        {
        Context db = new Context();
        List<StoredCredential> storedCredentials = db.StoredCredentials.Where(p => p.AccessToken != null).ToList();
        foreach (var glassUser in storedCredentials)
            {
            var state = Utils.GetStoredCredentials(glassUser.UserId);
            if (state == null)
                {
                return Redirect(Url.Action("StartAuth", "Auth"));
                }

            MirrorService Service = new MirrorService(new BaseClientService.Initializer()
            {
                Authenticator = Utils.GetAuthenticatorFromState(state)
            });


            string msgText = Request.Form.Get("message");
            if (!String.IsNullOrEmpty(msgText))
                {

                TimelineItem timelineItem = new TimelineItem();
                timelineItem.Creator = new Contact()
                {
                    Id = "MEETUP_MSG",
                    DisplayName = "Meetup Updates",
                };


                timelineItem.Text = msgText;
                timelineItem.Notification = new NotificationConfig() { Level = "DEFAULT" };
                timelineItem.MenuItems = new List<MenuItem>() {
                                                                  new MenuItem() { Action = "READ_ALOUD" },
                                                                  new MenuItem(){ Action ="DELETE"},
                                                                  new MenuItem(){ Action ="SHARE"},
                                                                  };

                Service.Timeline.Insert(timelineItem).Fetch();
                }

            }
        return Redirect(Url.Action("Index", "Meetup"));
        }
    }
}
