using InfoTrackAdCount.Models;
using InfoTrackAdCount.Service;
using System.Configuration;
using System.Web.Mvc;

namespace InfoTrackAdCount.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private readonly IRetrieveSearchDataService _retrieveSearchDataService;

        #endregion

        public HomeController(IRetrieveSearchDataService retrieveSearchDataService)
        {
            // Injected from Autofac
            _retrieveSearchDataService = retrieveSearchDataService;
        }

        public ActionResult Index()
        {
            ViewModel viewModel = new ViewModel
            {
                Keyword = ConfigurationManager.AppSettings["DefaultKeyword"],
                Url = ConfigurationManager.AppSettings["DefaultUrl"]
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(ViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string webAddress = _retrieveSearchDataService.CombineURL(model.Keyword);
            int count = _retrieveSearchDataService.GetSearchCount(model.Url, webAddress);
            model.Result = count == 0 ? "" : count.ToString();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Application description";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact";

            return View();
        }

        #region Helpers

        ////Combine google search url "https://www.google.com.au/search?q=online+title+search&num=100";
        //public static string CombineUrl(string keyword)
        //{
        //    string webAddressAndKeyword = ConfigurationManager.AppSettings["SearchWebsite"] + keyword.Replace(" ", "+");
        //    // Set the number of top result
        //    string numberOfResult = "&num=" + ConfigurationManager.AppSettings["NumberOfResult"];
        //    return webAddressAndKeyword + numberOfResult;
        //}

        ////Get google search result
        //public static int GetSearchCount(string url, string webAddress)
        //{
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webAddress);
        //    request.AddRange(100);
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    int count = 0;
        //    if (response.StatusCode == HttpStatusCode.OK)
        //    {
        //        Stream receiveStream = response.GetResponseStream();
        //        StreamReader readStream = null;

        //        if (response.CharacterSet == null)
        //        {
        //            readStream = new StreamReader(receiveStream);
        //        }
        //        else
        //        {
        //            readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
        //        }

        //        string data = readStream.ReadToEnd();
        //        // Debug.WriteLine(data);

        //        response.Close();
        //        readStream.Close();
        //        SaveSearchPageToFile(data);

        //        count = AllIndexesOf(data, url);
        //    }
        //    return count;
        //}

        //// Save search webpage to a file
        //public static void SaveSearchPageToFile(string data)
        //{
        //    DateTime dt = DateTime.Now;
        //    // Date fomat as file name  
        //    string fileName = string.Format("GoogleSearch-{0:yyyyMMddhhmmss}.html", DateTime.Now);
        //    // Set a variable to the Documents path.
        //    string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        //    // Write the string to a new file named "WriteLines.txt".
        //    using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, fileName)))
        //    {
        //        outputFile.WriteLine(data);
        //    }
        //}

        ////Count the number of strings
        //public static int AllIndexesOf(string str, string substr, bool ignoreCase = false)
        //{
        //    // url prefix equal to ">" or ">https://"  
        //    string[] urlPrefix = ConfigurationManager.AppSettings["CountedUrlPrefix"].Split(',');

        //    if (string.IsNullOrWhiteSpace(str) || string.IsNullOrWhiteSpace(substr))
        //    {
        //        throw new ArgumentException("String or substring is not specified.");
        //    }

        //    //find the result position
        //    var indexes = new List<int>();
        //    int index = 0;

        //    while ((index = str.IndexOf(substr, index, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)) != -1)
        //    {
        //        foreach (string item in urlPrefix)
        //        {
        //            if (str.Substring(index - item.Length, item.Length) == item)
        //            {
        //                Debug.WriteLine(index);
        //                Debug.WriteLine(str.Substring(index - item.Length, item.Length));
        //                indexes.Add(index);
        //                break;
        //            }
        //        }
        //        index++;
        //    }
        //    return indexes.Count;
        //}

        #endregion
    }
}