using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ForgetTheMilk.Controllers
{
    public class LinkValidator : ILinkValidator
    {
        public void Validate(string link)
        {
            var request = WebRequest.Create(link);
            request.Method = "HEAD";
            try
            {
                request.GetResponse();
            }
            catch(WebException ex)
            {
                throw new ApplicationException("Invalid link " + link);
            }
        }
    }

    public interface ILinkValidator
    {
        void Validate(string link);
    }
}