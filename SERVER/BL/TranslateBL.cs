using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Google.Cloud.Translation.V2;
using Newtonsoft.Json; // Install Newtonsoft.Json with NuGet
using Newtonsoft.Json.Linq;
using RestSharp;

namespace BL
{
    public class TranslateBL
    {
        #region    
        private static readonly string subscriptionKey = "91d93a27-0dcf-4c58-a41f-c2efb380fbb9";
        private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";

        // Add your location, also known as region. The default is global.
        // This is required if using a Cognitive Services resource.
        private static readonly string location = "East US";
        //public static void translateByMicrosoft(string sentenceToTranslate)

        #endregion
        public static string Translate(string text)
        {

            //string path = "C:\\Users\\hadar\\Desktop\\למידה מרחוק\\פרויקט\\neighbooraid\\SERVER\\API\\card.json";
            string path = "C:\\Users\\Owner\\Documents\\לימודים מחשבים אופקים\\PROJECT\\fullProject\\neighbooraid\\SERVER\\API\\card.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            // Input and output languages are defined as parameters.
            TranslationClient client = TranslationClient.Create();
            TranslationResult result = client.TranslateText(
                text: text,
                targetLanguage: "en",
                sourceLanguage: "he",
                model: TranslationModel.NeuralMachineTranslation);
            return result.TranslatedText;
        }

        public static List<string> Analysis(string text)
        {
            #region commit
            // TextRazorClient textRazor = null;
            // ApiResponse response = null;
            //
            //    try
            //    {
            //        textRazor = new TextRazorClient("http://localhost:44314/patientCase/sentence", "609f93b005493f2a387f36f9bee78b8b2bbfe72f023737d9cde5c4d7");
            //        response= await textRazor.Analyze(text, TextRazor.Net.Models.ExtratorsType.Words);
            //    }
            //    catch (Exception ex)
            //    {
            //        return;
            //    }
            //    //if (response.Ok)
            //    //{
            //        //List<string> relactionList = new List<string>();
            //var relactionList = response.Response.;
            //        var topicList = response.Response.Topics;
            //        var nounList = response.Response.NounPhrases;
            //    //}
            #endregion
            var client = new RestClient("https://api.textrazor.com");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("x-textrazor-key", "cdb11bdc9d8349e443743989ee597ac296cb58438a6423bae48f9bd5");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("extractors", "entities");
            request.AddParameter("extractors", "entailments");
            request.AddParameter("text", text);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            dynamic data = JObject.Parse(response.Content);
            List<object> importantWordFromTheString = new List<object>();
            foreach (var item in data.response.entailments)
            {
                if (item.score > 0.985)
                {
                    importantWordFromTheString.Add(item.entailedWords);
                }
            }
            List<object> words = importantWordFromTheString.Distinct().ToList<object>();
            List<string> returnedWords = new List<string>();
            string w;
            var charsToRemove = new string[] { "\n", "\r", " ", "\"", "[", "]" };
            foreach (var item in words)
            {
                w = item.ToString();
                foreach (var c in charsToRemove)
                {
                    w = w.Replace(c, string.Empty);
                }
                returnedWords.Add(w);
            }

            //delete deplicute
            returnedWords = returnedWords.GroupBy(k => k).Select(y => y.First()).ToList();

            return returnedWords;
        }

    }
 }