using System.Linq;
using System.Net;
using System.Web.Mvc;
using HMS.Infrastructure;
using HMS.Models;

namespace HMS.Controllers
{
    public class BiometricsController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Biometrics
        public ActionResult Index()
        {
            return View(db.Biometrics.ToList());
        }

        // GET: Biometrics/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Biometric biometric = db.Biometrics.Find(id);
            if (biometric == null)
            {
                return HttpNotFound();
            }
            return View(biometric);
        }       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
