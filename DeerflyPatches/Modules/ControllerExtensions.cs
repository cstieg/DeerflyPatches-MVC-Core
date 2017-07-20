using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeerflyPatches.Modules
{
    public static class ControllerExtensions
    {
        public static JsonResult JsonOk(this Controller controller)
        {
            return controller.Json(new { Success = "True" });
        }
    }
}
