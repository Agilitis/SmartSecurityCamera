using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using SampleBinaryClassification.Model.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ObjectDetection.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowFrontEnd")]
    public class SentimentController : ControllerBase
    {
        private const string MODEL_FILEPATH = @"../SampleBinaryClassification/SampleBinaryClassification.Model/MLModel.zip";

        [HttpPost]
        public async Task<IActionResult> PostFile(string sentence)
        {
            MLContext mlContext = new MLContext();

            // Training code used by ML.NET CLI and AutoML to generate the model
            //ModelBuilder.CreateModel();

            ITransformer mlModel = mlContext.Model.Load(GetAbsolutePath(MODEL_FILEPATH), out DataViewSchema inputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<SentimentModelInput, SentimentModelOutput>(mlModel);

            // Create sample data to do a single prediction with it 
            var modelInput = new SentimentModelInput();
            modelInput.Sentence = sentence;

            // Try a single prediction
            var predictionResult = predEngine.Predict(modelInput);

            Console.WriteLine(predictionResult);
            return Ok(predictionResult.Prediction);
        }

        private Stream GetAbsolutePath(object mODEL_FILEPATH)
        {
            throw new NotImplementedException();
        }
    }
}