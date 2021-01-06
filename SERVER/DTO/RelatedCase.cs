using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RelatedCase
    {
        public Cases relatedCase { get; set; } = new Cases();
        public int sumOfNumOfUsingThisSearch { get; set; }



        public RelatedCase(Cases relatedCase, int sumOfNumOfUsingThisSearch)
        {
            this.relatedCase = relatedCase;
            this.sumOfNumOfUsingThisSearch = sumOfNumOfUsingThisSearch;
        }

        public RelatedCase()
        {
        }

    }
}
