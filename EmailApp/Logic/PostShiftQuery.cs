using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmailApp.Models
{
    public class PostShiftQuery
    {
        private string link = "https://post-shift.ru/api.php?action=";

        public string Key { get; set; }

        public EmailInfo GetNewMailInfo()
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create($"{link}new&type=json");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                return JsonConvert.DeserializeObject<EmailInfo>(stream.ReadToEnd());
            }
        }

        public List<Letter> GetListLetters()
        {

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create($"{link}getlist&key={Key}&type=json");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                try
                {
                    return (List<Letter>)JsonConvert.DeserializeObject(stream.ReadToEnd(), typeof(List<Letter>));
                }
                catch
                {
                    return null;
                }
            }
        }

        public string GetLetterText(int id)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create($"{link}getmail&key={Key}&id={id}&forced=1");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                return stream.ReadToEnd();
            }
        }

        public string GetEmailLiveTime()
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create($"{link}livetime&key={Key}");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                return stream.ReadToEnd();
            }
        }

        public string ProlongEmailLiveTime()
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create($"{link}update&key={Key}");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                return stream.ReadToEnd();
            }
        }

        public string DeleteEmail()
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create($"{link}clear&key={Key}");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                return stream.ReadToEnd();
            }
        }

        public string ClearEmail()
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create($"{link}clear&key={Key}");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                return stream.ReadToEnd();
            }
        }

        public string DeleteAllEmailByIP()
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create($"{link}deleteall");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                return stream.ReadToEnd();
            }
        }
    }
}
