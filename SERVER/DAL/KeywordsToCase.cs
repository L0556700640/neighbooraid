//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class KeywordsToCase
    {
        public int caseId { get; set; }
        public int keywordId { get; set; }
        public int numOfUsingThisRelation { get; set; }
    
        public virtual Case Case { get; set; }
        public virtual Keyword Keyword { get; set; }
    }
}
