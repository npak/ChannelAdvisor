using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelAdvisor
{
    public class SearchOccurrence
    {
        public string SearchCondition { get; set; }

        public int SearchCount { get; set; }

        public SearchOccurrence(string condition, int count)
        {
            SearchCondition = condition;
            SearchCount = count;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SearchOccurrence))
                return false;

            return SearchCondition.Equals((obj as SearchOccurrence).SearchCondition);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
