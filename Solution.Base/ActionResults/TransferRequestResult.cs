using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Solution.Base.ActionResults
{
    public class TransferRequestResult : ActionResult
    {
        private string url;

        public TransferRequestResult(string url)
        {
            this.url = url;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Server.TransferRequest(this.url);
        }
    }
}
