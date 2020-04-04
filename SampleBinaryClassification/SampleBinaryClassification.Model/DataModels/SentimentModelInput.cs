//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using Microsoft.ML.Data;

namespace SampleBinaryClassification.Model.DataModels
{
    public class SentimentModelInput
    {
        [ColumnName("col0"), LoadColumn(0)]
        public string Sentence { get; set; }


        [ColumnName("Label"), LoadColumn(1)]
        public bool Label { get; set; }


    }
}
