// This file was auto-generated by ML.NET Model Builder. 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.ML;
using ObjectDetectionWithTrainingML.Model;

namespace ObjectDetectionWithTrainingML.Model
{
    public class ConsumeModel
    {
        public static PredictionEngine<ModelInput, ModelOutput> PredictionEngine { get; set; }

        // For more info on consuming ML.NET models, visit https://aka.ms/model-builder-consume
        // Method for consuming model in your app
        public static ModelOutput Predict(ModelInput input)
        {
            // Use model to make prediction on input data
            var prediciontEngine = GetPredictionEngine();
            ModelOutput result = prediciontEngine.Predict(input);
            return result;
        }

        public static PredictionEngine<ModelInput, ModelOutput> GetPredictionEngine()
        {
            if (PredictionEngine == null)
            {
                // Create new MLContext
                MLContext mlContext = new MLContext();

                // Load model & create prediction engine
                var cwd = Directory.GetCurrentDirectory();
                string modelPath = @"MLModel.zip";
                ITransformer mlModel = mlContext.Model.Load(modelPath, out var modelInputSchema);
                PredictionEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
            }
            return PredictionEngine;
        }
    }
}