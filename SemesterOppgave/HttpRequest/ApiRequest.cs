using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequest {
    public class ApiRequests {

        /// <summary>
        /// Fetches all resources
        /// </summary>
        /// <param name="url">Endpoint to resource</param>
        /// <returns>ResponseMessage</returns>
        public static async Task<string> Get(string url)
        {
            using (var httpClient = new HttpClient()) {
                var response = await httpClient.GetAsync(url);
                return (await response.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// Fetches a resource
        /// </summary>
        /// <param name="url">Endpoint to resource</param>
        /// <param name="id">Resource id</param>
        /// <returns>ResponseMessage</returns>
        public static async Task<string> Get(string url, int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url + id);
                return (await response.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// Fetches resource(s) based on parameter. url/parm/parm or url/?att=value
        /// </summary>
        /// <param name="url">Endpoint to resource</param>
        /// <param name="parameters"></param>
        /// <returns>ResponseMessage</returns>
        public static async Task<string> Get(string url, string parameters)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url + parameters);
                return (await response.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// Creates a new resource 
        /// </summary>
        /// <param name="url">Endpoint to resource</param>
        /// <param name="json">Serialized object</param>
        /// <returns>ResponseMessage</returns>
        public static async Task<string> Post(string url, string json)
        {
            using (var httpClient = new HttpClient())
            {
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(url, content);
                return (await response.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// Updates a resource
        /// </summary>
        /// <param name="url">Endpoint to resource</param>
        /// <param name="id">Resource id</param>        
        /// <param name="json">Serialized object</param>
        /// <returns>ResponseMessage</returns>
        public static async Task<string> Put(string url, int id, string json)
        {
            using (var httpClient = new HttpClient())
            {
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync(url + id, content);
                return (await response.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// Deletes a resource
        /// </summary>
        /// <param name="url">Endpoint to resource</param>
        /// <param name="id">Resource id</param>
        /// <returns>ResponseMessage</returns>
        public static async Task<string> Delete(string url, int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(url + id);
                return (await response.Content.ReadAsStringAsync());
            }
        }


    }
}