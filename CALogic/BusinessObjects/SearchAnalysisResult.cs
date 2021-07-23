using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelAdvisor
{
    public class SearchAnalysisResult
    {
        public IList<SearchOccurrence> PhraseOccurrence{get;set;}

        public IList<SearchOccurrence> SkuOccurrence { get; set; }

        public IList<SearchOccurrence> TermOccurrence { get; set; }

        public int TotalCount { get; set; }

        public SearchAnalysisResult()
        {
            PhraseOccurrence = new List<SearchOccurrence>();
            SkuOccurrence = new List<SearchOccurrence>();
            TermOccurrence = new List<SearchOccurrence>();
        }
    }
}
