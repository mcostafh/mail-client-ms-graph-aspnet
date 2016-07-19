using Microsoft.Graph;
using Microsoft_Graph_Mail_Client.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Resources;

namespace Microsoft_Graph_Mail_Client.Controllers
{
    public class InboxController : Controller
    {
        // GET: Inbox
        public ActionResult Index()
        {
        
            return View();
        }

        public async Task<ActionResult> GetInboxMail()
        {
            try
            {

                // Initialize the GraphServiceClient.
                GraphServiceClient graphClient = SDKHelper.GetAuthenticatedClient();

                // Get the current user. 
                // This sample only needs the user's email address, so select the mail and userPrincipalName properties.
                // If the mail property isn't defined, userPrincipalName should map to the email for all account types. 
                User me = await graphClient.Me.Request().Select("mail,userPrincipalName").GetAsync();
                ViewBag.Email = me.Mail ?? me.UserPrincipalName;
                return View("Graph");
            }
            catch (ServiceException se)
            {
                if (se.Error.Message == Resource.Error_AuthChallengeNeeded) return new EmptyResult();
                return RedirectToAction("Index", "Error", new { message = Resource.Error_Message + Request.RawUrl + ": " + se.Error.Message });
            }
        }
    }
}