using System;
using System.Collections.Generic;
using System.Text;

using ChannelAdvisor.CAStoreService;

namespace ChannelAdvisor
{
    public class StoreService
    {
        const int searchGlobalTimeframe = 167;
        const int searchCallTimeFrame = 71;

        /// <summary>
        /// Retrieve search analysis result from Channel Advisor
        /// </summary>
        /// <param name="profileId">Profile identifier</param>
        /// <returns>Search analysis result from Channel Advisor</returns>
        public static SearchAnalysisResult GetSearchAnalisisResult(int profileId)
        {
            //Create web service object
            CAStoreService.StoreService storeService = CAUtils.GetStoreService();
            // Get API key for profile
            string apiKey = CAUtils.GetProfileAPIKey(profileId);

            SearchAnalysisResult result = new SearchAnalysisResult();
            int residue = searchGlobalTimeframe;
            while (residue > 0)
            {
                CAStoreService.APIResultOfStoreSearchAnalysisResult response = storeService.GetSearchAnalysisStats(apiKey,
                    DateTime.UtcNow.AddHours(-residue), (searchCallTimeFrame - residue) > 0 ? DateTime.UtcNow : DateTime.UtcNow.AddHours(searchCallTimeFrame - residue));

                if ((response.Status == ResultStatus.Failure) || (response.ResultData == null))
                    throw new Exception("Fail to load data from Channel Advisor Store Service");

                FillSearchAnalisisResult(result, response);
                residue -= searchCallTimeFrame;
            }

            return result;
        }

        /// <summary>
        /// Add result of new response to global result
        /// </summary>
        /// <param name="result">Global result</param>
        /// <param name="response">Current response</param>
        private static void FillSearchAnalisisResult(SearchAnalysisResult result, APIResultOfStoreSearchAnalysisResult response)
        {
            // Fill phrase search results
            if (response.ResultData.PhraseOccurrenceList != null)
            {
                foreach (SearchPhraseOccurrence phrase in response.ResultData.PhraseOccurrenceList)
                {
                    SearchOccurrence so = new SearchOccurrence(phrase.Phrase, phrase.Count);

                    int index = result.PhraseOccurrence.IndexOf(so);
                    if (index < 0)
                    {
                        result.PhraseOccurrence.Add(so);
                    }
                    else
                    {
                        result.PhraseOccurrence[index].SearchCount += phrase.Count;
                    }
                }
            }

            // Fill sku search results
            if (response.ResultData.SkuOccurrenceList != null)
            {
                foreach (SearchSkuOccurrence sku in response.ResultData.SkuOccurrenceList)
                {
                    SearchOccurrence so = new SearchOccurrence(sku.Sku, sku.Count);

                    int index = result.SkuOccurrence.IndexOf(so);
                    if (index < 0)
                    {
                        result.SkuOccurrence.Add(so);
                    }
                    else
                    {
                        result.SkuOccurrence[index].SearchCount += sku.Count;
                    }
                }
            }

            // Fill sku search results
            if (response.ResultData.TermOccurrenceList != null)
            {
                foreach (SearchTermOccurrence term in response.ResultData.TermOccurrenceList)
                {
                    SearchOccurrence so = new SearchOccurrence(term.Term, term.Count);

                    int index = result.TermOccurrence.IndexOf(so);
                    if (index < 0)
                    {
                        result.TermOccurrence.Add(so);
                    }
                    else
                    {
                        result.TermOccurrence[index].SearchCount += term.Count;
                    }
                }
            }

            result.TotalCount += response.ResultData.TotalSearches;
        }
    }
}
