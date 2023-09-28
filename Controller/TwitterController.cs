using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tweetinvi;
using Tweetinvi.Models;
using TwitterXScheduler.Models;

namespace TwitterXScheduler.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TwitterController : ControllerBase
    {
        [HttpPost]

        public async Task<IActionResult> PostTweet(PostTweetDto postTweet){
            var client = new TwitterClient("e8ElaTbiw958oupDNSM4gvjsW",
            "CT6rRcsNLqI7DhqoejxdK1KMvdrLhMzxOh44fAuKndgnH018uz",
            "1707113308724117504-w04Ly4hPmGudxWosq0SBOyulcAV2pg",
            "XHGiHFjJljGzNeEKLgfS4zgySCe6dxSFprTfaVs9dOx6W");

            var result = await client.Execute.AdvanceRequestAsync(BuildTwitterRequest(postTweet,client));
            return Ok(result.Content);
        }

        public static Action<ITwitterRequest> BuildTwitterRequest(PostTweetDto postTweet, TwitterClient twitterClient){
                return(ITwitterRequest request) => 
                {
                    var jsonBody = twitterClient.Json.Serialize(postTweet);
                    var content = new StringContent(jsonBody,Encoding.UTF8,"application/json");
                    request.Query.Url = "https://api.twitter.com/2/tweets";
                    request.Query.HttpMethod = Tweetinvi.Models.HttpMethod.POST;
                    request.Query.HttpContent = content;

                };
        }
    }
}