using ArtShop.Data.Model;
using ArtShop.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OdeToFood.WebSite.Services
{
    public class Logger
    {
        private Logger()
        {
        }
        public readonly static Logger Instance = new Logger();
        public void LogException(Exception exception)
        {
            // try-catch because database itself could be down or Request context is unknown.

            try
            {
                string userId = null;
                try { userId = HttpContext.Current.User.Identity.Name; }
                catch {/* no hacer nada, o enviar un correo electrónico al webmaster */ }

                // ** Prototype pattern. El objeto Error tiene sus valores predeterminados inicializados
                var error = new Error()
                {
                    UserId = userId,
                    Exception = exception.GetType().FullName,
                    Message = exception.Message,
                    Everything = exception.ToString(),
                    IpAddress = HttpContext.Current.Request.UserHostAddress,
                    UserAgent = HttpContext.Current.Request.UserAgent,
                    PathAndQuery = HttpContext.Current.Request.Url == null ? "" : HttpContext.Current.Request.Url.PathAndQuery,
                    HttpReferer = HttpContext.Current.Request.UrlReferrer == null ? "" : HttpContext.Current.Request.UrlReferrer.PathAndQuery,
                    
                };
                var db = new BaseDataService<Error>() ;
                db.Create(error);
            }
            catch (Exception ex) {

                /* no hacer nada, o enviar un correo electrónico al webmaster */
            }
        }
    }
}