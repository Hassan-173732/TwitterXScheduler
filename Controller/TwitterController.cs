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
            var client = new TwitterClient("0pc8cLqD6vWfXL05pbzvYgERP",
            "Ybg2E4pMlW77LqIdqvhBdPflYJDI2VbCQsQM2zI9h4R5YVXnCT",
            "1707113308724117504-mqK1wTqBjHvQXhrl04R3EgARD3mVdY",
            "5wdP2icRQZF1iINC3kYTZUfiqZsnFpzp4hMVX75cPCysO");

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