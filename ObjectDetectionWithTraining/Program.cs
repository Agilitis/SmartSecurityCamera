using System;
using System.IO;
using ObjectDetectionWithTrainingML.Model;

namespace ObjectDetectionWithTraining
{
    class Program
    {

        static int MAX_IMAGES = 15;
        static void Main(string[] args)
        {
            var input = new ModelInput();
            var files = Directory.GetFiles(@"E:\Machine Learning datasets\weapons\Weapons\test\Pistol Hand Guns", "*.jpg");
            
            for(int i = 0; i < 30; i++)
            {
                input.ImageSource = files[i].ToString();


                ModelOutput result = ConsumeModel.Predict(input);

                var highestScore = 0.0;

                foreach (var score in result.Score)
                {
                    if (highestScore < score)
                    {
                        highestScore = score;
                    }
                }

                Console.WriteLine($"The image: {input.ImageSource} contains: {result.Prediction} with a chance of {highestScore * 100}%");
            }

            

        }
    }
}
