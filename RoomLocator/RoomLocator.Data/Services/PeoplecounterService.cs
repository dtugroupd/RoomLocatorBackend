using System.Net.Http;

namespace RoomLocator.Data.Services
{
    /// <summary>
    ///     <author>Amal Qasim, s132957</author>
    /// </summary>
    public class PeoplecounterService
    {
        public HttpRequestMessage  RequestsForHttp()
        {
            var url = "https://eds.modcam.io/v1/peoplecounter/installations/" +
                      "590b336ef8cbb2000a8fa212/hours/2019-11-01/2019-11-12?st=08:00&et=09:00";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            
            request.Headers.Add("x-client-id", "DTUAPI");
            request.Headers.Add("x-api-key", "3593e5b65f4ad590f859a876f976ba18");
            //request.Headers.Add("Authorization: Bearer", "[eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJkNGNmOTdkOS03NmZlLTQ3YTctYjYyZC1lZTQ0OGQwM2IxNjkiLCJzdWIiOiJzMTY1MjQxIiwiZXhwIjoxNTczNTk3Njc0LCJpc3MiOiJSb29tTG9jYXRvciIsImF1ZCI6Imh0dHBzOi8vc2UyLXdlYmFwcDA0LmNvbXB1dGUuZHR1LmRrIn0.yLlpxGSx4QBB7109hhgVigkuYrovc3iyVUFmmb-MwY8]");


            return request;
        }
        
    }
}