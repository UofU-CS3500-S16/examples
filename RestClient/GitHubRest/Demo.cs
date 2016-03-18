using System;
using System.Text;
using Newtonsoft.Json;
using System.Dynamic;
using System.Net.Http;

namespace Restful
{
    public class Demo
    {
        // You'll need to put your own persona access token here
        // It needs to have repo deletion capability
        private const string TOKEN = "";

        // You'll need to put your own GitHub user name here
        private const string USER_NAME = "josephzachary";

        // You'll need to put your own login name here
        private const string EMAIL = "zachary@cs.utah.edu";

        // You'll need to put one of your public REPOs here
        private const string PUBLIC_REPO = "repo1";

        public static void Main(string[] args)
        {
            GetDemo();
            Console.ReadLine();
            GetAllDemo();
            Console.ReadLine();
            GetWithParamsDemo();
            Console.ReadLine();
            PostDemo();
            Console.ReadLine();
            PutDemo();
            Console.ReadLine();
            DeleteDemo();
            Console.ReadLine();
        }

        /// <summary>
        /// Creates an HttpClient for communicating with GitHub.  The GitHub API requires specific information
        /// to appear in each request header.
        /// </summary>
        public static HttpClient CreateClient()
        {
            // Create a client whose base address is the GitHub server
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.github.com/");

            // Tell the server that the client will accept this particular type of response data
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");

            // This is an authorization token that you can create by logging in to your GitHub account.
            client.DefaultRequestHeaders.Add("Authorization", "token " + TOKEN);

            // When an http request is made from a browser, the user agent describes the browser.
            // Github requires the email address of the authenticated user.
            client.DefaultRequestHeaders.UserAgent.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", Uri.EscapeDataString(EMAIL));
            
            // There is more client configuration to do, depending on the request.
            return client;
        }

        /// <summary>
        /// Prints out the names of the organizations to which the user belongs.
        /// Illustrates a simple GET request.
        /// </summary>
        public static void GetDemo()
        {
            // Create the HttpClient.  It will be automatically closed when
            // this using block is exited.
            using (HttpClient client = CreateClient())
            {
                // Send a GET request to the server, which will send back its response.
                // The parameter is appended to the base address of the client to 
                // obtain the full URL.  

                // The pathname parameter to GetAsync names a resource (all organizations of the
                // authenticated user).  This style of naming occurs throughout the API.  The
                // URL names the resource on which an operation is to be performed.
                HttpResponseMessage response = client.GetAsync("/user/orgs").Result;

                // If the HTTP response code indicates success, we print out the
                // information that was sent back.
                if (response.IsSuccessStatusCode)
                {
                    // Extract the response data, which is an JSON value.
                    String result = response.Content.ReadAsStringAsync().Result;

                    // According to the GitHub API, the response will be a JSON array
                    // that contains one JSON object per organization.  We convert this
                    // into a C# array of C# objects.

                    // The dynamic keyword needs some explanation.  Any value can be stored
                    // into a variable of type dynamic.  The compiler doesn't do any type
                    // checking for the variable, but if it used improperly at runtime,
                    // an exception will result.  We use dynamic here because we can't
                    // predict the exact type of the deserialized response.
                    dynamic orgs = JsonConvert.DeserializeObject(result);

                    // Loop through the array to obtain objects corresponding to GitHub
                    // organizations.  Print out the "login" property of each object.
                    Console.WriteLine("My organizations:");
                    foreach (dynamic org in orgs)
                    {
                        Console.WriteLine(org.login);
                    }
                }

                // If the HTTP response code indicates failure, display information
                // about the error.
                else
                {
                    Console.WriteLine("Error getting organizations: " + response.StatusCode);
                    Console.WriteLine(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Prints out some of the users of GitHub, along with a link that we could use to
        /// obtain more if we cared to pursue it.  This illustrates a GET request for which
        /// there is too much data to expect a single self-contained result.
        /// </summary>
        public static void GetAllDemo()
        {
            using (HttpClient client = CreateClient())
            {
                HttpResponseMessage response = client.GetAsync("/users?since=46").Result;
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    dynamic users = JsonConvert.DeserializeObject(result);

                    // The response data was an array of objects, each object
                    // corresponding to a user.  We print out the login name
                    // of each user.
                    Console.WriteLine("GitHub users:");
                    foreach (dynamic user in users)
                    {
                        Console.WriteLine(user.login);
                    }

                    // GitHub will only send back one "page" of data at a time.
                    // If there are more pages, it will send back URLs to use
                    // to obtain other pages.  Those URLs are incuded as values
                    // of the Link header in the response.  Here, we print out
                    // all such links.
                    Console.WriteLine();
                    Console.WriteLine("Links:");
                    foreach (String link in response.Headers.GetValues("Link"))
                    {
                        Console.WriteLine(link);
                    }
                }
                else
                {
                    Console.WriteLine("Error getting users: " + response.StatusCode);
                    Console.WriteLine(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Prints out all of the commits that have been made on the master branch of one
        /// of my repositories.  This illustrates a GET request that contains parameters.
        /// </summary>
        public static void GetWithParamsDemo()
        {
            using (HttpClient client = CreateClient())
            {
                // Here we compose the request URL.  The resulting URL will be
                // /repos/josephzachary/repo1/commits?sha=master

                // Note how the user name and the repo name appear as part of
                // the path.  They help to name the resource: Commits made to the
                // repo1 that belongs to josephzachary.  The branch name is included
                // as a parameter in the "query" part of the URL.
                String url = String.Format("/repos/{0}/{1}/commits?sha={2}", USER_NAME, PUBLIC_REPO, Uri.EscapeDataString("master"));

                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;

                    // The deserialized result is an array of objects.  Here, we just print out
                    // the whole thing.
                    dynamic commits = JsonConvert.DeserializeObject(result);
                    Console.WriteLine("Commits:");
                    Console.WriteLine(commits);
                }
                else
                {
                    Console.WriteLine("Error getting commits: " + response.StatusCode);
                    Console.WriteLine(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Creates a new public repository.  Demonstrates the use of a POST request.
        /// For a POST, the parameters go into the body of the request instead of in
        /// the URL.
        /// </summary>
        public static void PostDemo()
        {
            using (HttpClient client = CreateClient())
            {
                // An ExpandoObject is one to which in which we can set arbitrary properties.
                // To create a new public repository, we must send a request parameter which
                // is a JSON object with various properties of the new repo expressed as
                // properties.
                dynamic data = new ExpandoObject();
                data.name = "TestRepo";
                data.description = "A test repository for CS 3500";
                data.has_issues = false; 

                // To send a POST request, we must include the serialized parameter object
                // in the body of the request.
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("/user/repos", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    // The deserialized response value is an object that describes the new repository.
                    String result = response.Content.ReadAsStringAsync().Result;
                    dynamic newRepo = JsonConvert.DeserializeObject(result);
                    Console.WriteLine("New repository: ");
                    Console.WriteLine(newRepo);
                }
                else
                {
                    Console.WriteLine("Error creating repo: " + response.StatusCode);
                    Console.WriteLine(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Commits a file to a repository.  Demonstrates the use of a PUT request.
        /// For a PUT, the parameters go into the body of the request.
        /// </summary>
        public static void PutDemo()
        {
            using (HttpClient client = CreateClient())
            {
                dynamic data = new ExpandoObject();
                data.message = "Committing via API";
                data.content = Convert.ToBase64String(Encoding.UTF8.GetBytes("This is a test"));

                String url = String.Format("/repos/{0}/{1}/contents/{2}", USER_NAME, "TestRepo", "file1");
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("File created");
                }
                else
                {
                    Console.WriteLine("Error putting file: " + response.StatusCode);
                    Console.WriteLine(response.ReasonPhrase);
                }
            }
        }


        /// <summary>
        /// Deletes an existing public repository.  Here, we demonstrate the use of
        /// a DELETE request.  
        /// </summary>
        public static async void DeleteDemo()
        {
            using (HttpClient client = CreateClient())
            {
                // Here the repo name and user name appear in the path.  If there were
                // any parameters, they would go in the body as with POST and PUT.
                String url = String.Format("/repos/{0}/TestRepo", USER_NAME);
                HttpResponseMessage response = await client.DeleteAsync(url);

                // No response object is sent back.
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Deletion succeeded");
                }
                else
                {
                    Console.WriteLine("Error deleting repo: " + response.StatusCode);
                    Console.WriteLine(response.ReasonPhrase);
                }
            }
        }
    }
}
