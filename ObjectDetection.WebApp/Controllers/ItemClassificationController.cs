using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using ObjectDetectionWithTrainingML.Model;

namespace ObjectDetection.WebApp.Controllers
{
    [EnableCors("allowFrontEnd")]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemClassificationController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PostFile([FromBody]ItemModel item)
        {
            var input = new ModelInput();
            //input.InspectionType = item.Type;
            //input.ViolationDescription = item.Issue;

            var result = ConsumeModel.Predict(input);

            Console.WriteLine(result);
            return Ok(new { result });
        }
    }

    public class ItemModel
    {
        public string Type { get; set; }
        public string Issue { get; set; }
    }
}