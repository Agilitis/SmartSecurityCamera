using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using SampleBinaryClassification.Model.DataModels;
using System;
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
        public async Task<IActionResult> PostFile([FromBody]SentenceModel sentenceModel)
        {
            MLContext mlContext = new MLContext();

            // Training code used by ML.NET CLI and AutoML to generate the model
            //ModelBuilder.CreateModel();

            ITransformer mlModel = mlContext.Model.Load(MODEL_FILEPATH, out DataViewSchema inputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<SentimentModelInput, SentimentModelOutput>(mlModel);

            // Create sample data to do a single prediction with it 
            var modelInput = new SentimentModelInput();
            modelInput.Sentence = sentenceModel.Sentence;

            // Try a single prediction
            var predictionResult = predEngine.Predict(modelInput);

            Console.WriteLine(predictionResult);
            return Ok(new { predictionResult.Score, predictionResult.Prediction });
        }

    }

    public class SentenceModel
    {
        public string Sentence { get; set; }
    }
}