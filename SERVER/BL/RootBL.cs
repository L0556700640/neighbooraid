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
    }
}
