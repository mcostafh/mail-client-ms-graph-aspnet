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
        public async Task<ActionResult> Index()
        {
            try
            {
                // Initialize the GraphServiceClient.
                GraphServiceClient graphClient = SDKHelper.GetAuthenticatedClient();

                var inboxMail = await graphClient.Me.MailFolders.Inbox.Messages.Request().GetAsync();
                /*// Get the current user. 
                // This sample only needs the user's email address, so select the mail and userPrincipalName properties.
                // If the mail property isn't defined, userPrincipalName should map to the email for all account types. 
                User me = await graphClient.Me.Request().Select("mail,userPrincipalName").GetAsync();*/
                ViewBag.InboxMail = inboxMail;
                return View("Inbox");
            }
            catch (ServiceException se)
            {
                if (se.Error.Message == Resource.Error_AuthChallengeNeeded) return new EmptyResult();
                return RedirectToAction("Index", "Error", new { message = Resource.Error_Message + Request.RawUrl + ": " + se.Error.Message });
            }
        }

        public async Task<ActionResult> GetInboxMail()
        {
            try
            {
                // Initialize the GraphServiceClient.
                GraphServiceClient graphClient = SDKHelper.GetAuthenticatedClient();

                var inboxMail = await graphClient.Me.MailFolders.Inbox.Request().GetAsync();
                /*// Get the current user. 
                // This sample only needs the user's email address, so select the mail and userPrincipalName properties.
                // If the mail property isn't defined, userPrincipalName should map to the email for all account types. 
                User me = await graphClient.Me.Request().Select("mail,userPrincipalName").GetAsync();*/
                ViewBag.InboxMail = inboxMail;
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