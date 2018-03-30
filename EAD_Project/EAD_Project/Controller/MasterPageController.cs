using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EAD_Project.Controller
{
    public class MasterPageController
    {

        public void ShowUserFunction(string role, Control c)
        {
            foreach (var item in c.Controls)
            {
                if (item is HyperLink)
                {
                    if (((HyperLink)item).ID.Contains("HyperLink" + role + "Func"))
                    {
                        ((HyperLink)item).Visible = true;
                    }

                }
            }
        }

        public void GetQueueCounter(Control c)
        {
            int count = 0;
            foreach (var item in c.Controls)
            {
                if (item is HyperLink)
                {
                    if (((HyperLink)item).ID.Contains("HyperLinkCounter"))
                    {
                        count = count + 1;
                        ((HyperLink)item).NavigateUrl = "~/QueueAdministration.aspx?Counter=" + count;
                    }
                    
                }

            }
        }
    }
}