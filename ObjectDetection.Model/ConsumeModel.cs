using Microsoft.ML;
using System;

namespace ObjectDetection.Model
{
    public class ConsumeModel
    {
        // For more info on consuming ML.NET models, visit https://aka.ms/model-builder-consume
        // Method for consuming model in your app
        public static ObjectDetectionModelOutput Predict(ModelInput input)
        {

            // Create new MLContext
            MLContext mlContext = new MLContext();

            // Load model & create prediction engine
            string modelPath = AppDomain.CurrentDomain.BaseDirectory + "MLModel.zip";
            ITransformer mlModel = mlContext.Model.Load(modelPath, out var modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ObjectDetectionModelOutput>(mlModel);

            // Use model to make prediction on input data
            ObjectDetectionModelOutput result = predEngine.Predict(input);
           return result;
            
        }
    }
}
