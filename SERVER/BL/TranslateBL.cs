using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Google.Cloud.Translation.V2;
using Newtonsoft.Json; // Install Newtonsoft.Json with NuGet
namespace BL
{
    public class TranslateBL
    {
        private static readonly string subscriptionKey = "91d93a27-0dcf-4c58-a41f-c2efb380fbb9";
        private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";

        // Add your location, also known as region. The default is global.
        // This is required if using a Cognitive Services resource.
        private static readonly string location = "East US";

        public static string  Translate(string text,string path)
        //public static void translateByMicrosoft(string sentenceToTranslate)
        {
             path =path+"\\card.json";

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
        }
    
}