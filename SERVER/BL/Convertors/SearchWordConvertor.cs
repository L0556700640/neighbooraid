using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Convertors
{
    public static class SearchWordConvertor
    {
        public static DTO.SearchWord ConvertSearchWordToDTO(DAL.SearchWord searchWord)
        {
            return new DTO.SearchWord
            {
                caseId = searchWord.caseId,
                numberSearchWord = searchWord.numberSearchWord,
                word = searchWord.word,
                wordId = searchWord.wordId
            };
        }
    }
}
