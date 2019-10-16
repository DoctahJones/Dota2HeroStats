using Dota2HeroStats.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Dota2HeroStats.Services.GameFetcher
{
    public class OpenDotaGameFetcher : IGameFetcher
    {
        private readonly IDataSource dataSource;
        private readonly IAPIRequestSender requestSender;
        private readonly IRateLimitedAPIRequestDispatcher rateLimitedRequestDispatcher;

        public OpenDotaGameFetcher(IDataSource dataSource, IAPIRequestSender requestSender, IRateLimitedAPIRequestDispatcher rateLimitedRequestDispatcher)
        {
            this.dataSource = dataSource;
            this.requestSender = requestSender;
            this.rateLimitedRequestDispatcher = rateLimitedRequestDispatcher;

        }

        public async Task<AbilityDraftMatch> FetchGame(string matchId)
        {
            //create api request
            var matchRequest = new OpenDotaMatchAPIRequest(requestSender, matchId);
            //send request
            var stringResult = await SendHttpRequestAndBundleExceptions(matchRequest);
            //try to convert string to AbilityDraftMatch
            return await ConvertJSONStringToAbilityDraftMatch(stringResult);
        }



        private async Task<string> SendHttpRequestAndBundleExceptions(OpenDotaMatchAPIRequest matchRequest)
        {
            try
            {
                return await rateLimitedRequestDispatcher.MakeAPIRequestAsync(matchRequest, CancellationToken.None, TimeSpan.FromSeconds(10));
            }
            catch (TaskCanceledException e)
            {
                //timed out
                throw new GameFetcherException("The request timed out.", e);
            }
            catch (HttpRequestException e)
            {
                //The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.
                throw new GameFetcherException("The external request failed. There could be an issue with network connectivity, DNS failure, server certificate validation or timeout", e);
            }
        }

        private async Task<AbilityDraftMatch> ConvertJSONStringToAbilityDraftMatch(string stringResult)
        {
            var stringToModelConverter = new JSONModels.JSONStringToModelConverter();
            JSONModels.AbiltiyDraftGameJSON abilityDraftGameJSON = null;
            bool error = false;
            try
            {
                abilityDraftGameJSON = stringToModelConverter.ConvertToModel<JSONModels.AbiltiyDraftGameJSON>(stringResult);
            }
            catch (JsonSerializationException)
            {
                error = true;
            }
            if (error || abilityDraftGameJSON.match_id == 0)
            {
                try
                {
                    var errorJSON = stringToModelConverter.ConvertToModel<JSONModels.ErrorJSON>(stringResult);
                    if (errorJSON.error != null)
                    {
                        if (String.Equals(errorJSON.error, "Not Found"))
                        {
                            return null; //if there is no match with that id we return null rather than throw an exception
                        }
                        throw new GameFetcherException("An error occurred on the request. Error received: " + errorJSON.error + ". ", null); 
                    }
                    else
                    {
                        throw new GameFetcherException("Unable to deserialize json string to AbiltiyDraftGameJSON.");
                    }
                }
                catch (JsonSerializationException e)
                {
                    throw new GameFetcherException("Unable to deserialize json string to AbiltiyDraftGameJSON.", e);
                }
            }
            var externalModelToLocalModelConverter = new JSONModels.JSONAbilityDraftGameToLocalCopy(dataSource);
            var converted = await externalModelToLocalModelConverter.ToLocalAbilityDraftMatch(abilityDraftGameJSON);
            return converted;
        }


    }
}