using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Google.Cloud.Translation.V2;
using Newtonsoft.Json; // Install Newtonsoft.Json with NuGet
using TextRazor.Net;

namespace BL
{
    public class TranslateBL
    {
        private static readonly string subscriptionKey = "91d93a27-0dcf-4c58-a41f-c2efb380fbb9";
        private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";

        // Add your location, also known as region. The default is global.
        // This is required if using a Cognitive Services resource.
        private static readonly string location = "East US";

        public static string Translate(string text)
        //public static void translateByMicrosoft(string sentenceToTranslate)
        {

            string path = "C:\\Users\\hadar\\Desktop\\למידה מרחוק\\פרויקט\\neighbooraid\\SERVER\\API\\card.json";

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS",path);

            // Input and output languages are defined as parameters.
            TranslationClient client = TranslationClient.Create();
            TranslationResult result = client.TranslateText(
                text: text,
                targetLanguage: "en",
                sourceLanguage: "he",
                model: TranslationModel.NeuralMachineTranslation);
           return result.TranslatedText;
        }

        public static async Task Analysis(string text)
        {
            TextRazorClient textRazor = null;
            ApiResponse response = null;

            try
            {
                textRazor = new TextRazorClient("http://localhost:44314/patientCase/sentence", "609f93b005493f2a387f36f9bee78b8b2bbfe72f023737d9cde5c4d7");
                response= await textRazor.Analyze(text, TextRazor.Net.Models.ExtratorsType.Words);
            }
            catch (Exception ex)
            {
                return;
            }
            //if (response.Ok)
            //{
                //List<string> relactionList = new List<string>();
                var relactionList = response.Response.Relations;
                var topicList = response.Response.Topics;
                var nounList = response.Response.NounPhrases;
            //}
        }
    }

}