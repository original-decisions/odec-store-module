using Microsoft.AspNetCore.Mvc;

namespace odec.Store.Service.Controllers
{
    
    public class StoreController : Controller
    {
        public IActionResult Get()
        {
            return Json(new { });
        }

        public IActionResult Save()
        {
            return Json(new{});
        }

        public IActionResult Delete()
        {
            return Json(new { });
        }

        public IActionResult GetById()
        {
            return Json(new { });
        }
    }
}