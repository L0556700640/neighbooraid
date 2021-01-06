using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{

    public class RootBL
    {
        public static string analyzeWord(string word)
        {
            char[] wordLetters = word.ToCharArray();
            string wordRoot = "";
            if (wordLetters.Length == 3)
            {
                foreach (var letter in wordLetters)
                {
                    wordRoot += letter;
                }
            }
            else
            {
                if (word.Length == 4)
                {
                    if (wordLetters[0] == 'א' ||
                       wordLetters[0] == 'ת' ||
                       wordLetters[0] == 'י' ||
                       wordLetters[0] == 'נ' ||
                       wordLetters[0] == 'מ')
                    {
                        for (int i = 0; i < wordLetters.Length; i++)
                        {
                            if (i != 0)
                            {
                                wordRoot += wordLetters[i];
                            }
                        }
                    }
                    else
                    {
                        if (wordLetters[1] == 'ו')
                        {
                            for (int i = 0; i < wordLetters.Length; i++)
                            {
                                if (i != 1)
                                {
                                    wordRoot += wordLetters[i];
                                }
                            }
                        }
                        else
                        {
                            if (wordLetters[3] == 'ת' ||
                                wordLetters[3] == 'ה' ||
                                wordLetters[3] == 'ו' ||
                                wordLetters[3] == 'י')
                            {
                                for (int i = 0; i < wordLetters.Length; i++)
                                {
                                    if (i != 3)
                                    {
                                        wordRoot += wordLetters[i];
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (wordLetters.Length == 5)
                    {
                        if ((wordLetters[3] == 'ת' && wordLetters[4] == 'ם') ||
                            (wordLetters[3] == 'ת' && wordLetters[4] == 'י') ||
                            (wordLetters[3] == 'ת' && wordLetters[4] == 'ן') ||
                            (wordLetters[3] == 'נ' && wordLetters[4] == 'ו') ||
                            (wordLetters[3] == 'נ' && wordLetters[4] == 'ה'))
                        {
                            for (int i = 0; i < wordLetters.Length; i++)
                            {
                                if (i != 3 && i != 4)
                                {
                                    wordRoot += wordLetters[i];
                                }
                            }
                        }
                        else
                        {
                            if ((wordLetters[0] == 'ת' && wordLetters[4] == 'י') ||
                                (wordLetters[0] == 'ת' && wordLetters[4] == 'ו') ||
                                (wordLetters[0] == 'י' && wordLetters[4] == 'ו') ||
                                (wordLetters[0] == 'מ' && wordLetters[4] == 'ת')
                                )
                            {
                                for (int i = 0; i < wordLetters.Length; i++)
                                {
                                    if (i != 0 && i != 4)
                                    {
                                        wordRoot += wordLetters[i];
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (wordLetters.Length == 6)
                        {
                            if ((wordLetters[0] == 'ת' && wordLetters[4] == 'נ' && wordLetters[5] == 'ה') ||
                                (wordLetters[0] == 'מ' && wordLetters[4] == 'י' && wordLetters[5] == 'ם') ||
                                (wordLetters[0] == 'מ' && wordLetters[4] == 'ו' && wordLetters[5] == 'ת')
                                )
                            {
                                for (int i = 0; i < wordLetters.Length; i++)
                                {
                                    if (i != 0 && i < 4)
                                    {
                                        wordRoot += wordLetters[i];
                                    }
                                }
                            }


                        }
                    }
                }
            }
            return wordRoot;
        }
        public static bool IsTheWordIsFromThisRoot(string word, string root)
        {
            //todo: fill the function
            return false;
        }

        public static void translateSearchSentenceByMicrosoft()
        {
            var client = new RestClient("https://microsoft-translator-text.p.rapidapi.com/translate?to=undefined&api-version=3.0&profanityAction=NoAction&textType=plain");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("x-rapidapi-key", "b8cc91e82amsh6abf96f948748c9p19f114jsn31497aaab51c");
            request.AddHeader("x-rapidapi-host", "microsoft-translator-text.p.rapidapi.com");
            request.AddParameter("application/json", "[\r {\r \"Text\": \"I would really like to drive your car around the block a few times.\"\r }\r]", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            //           JsonParameter jsonParameter= {
            //            "detectedLanguage":{
            //                                "score":""
            //                        }
            //                "translations":
            //                    [
            //                    0:{
            //                    "text":""
            //                    "transliteration":
            //                        {
            //                         "text":""
            //                         "script":""
            //                        }
            //                    "to":""
            //                    "alignment":{
            //                       "proj":""
            //}
            //                    "sentLen":{
            //                        2 items
            //                    "srcSentLen":[1 item
            //                    0:{ ...}
            //                        1 item
            //                    ]
            //"transSentLen":[1 item
            //                    0:{ ...}
            //                        1 item
            //                    ]
            //}
            //                }
            //]
            //}
            //]

            JsonParameter Subscription = new JsonParameter
                (
                "Subscription ID", "91d93a27 - 0dcf - 4c58 - a41f - c2efb380fbb9"
                );
        }
    }
}
