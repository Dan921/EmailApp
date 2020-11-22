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
    public class PostShiftQueryHelper
    {
        private string link = "https://post-shift.ru/api.php?action=";

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

        public List<Letter> GetListLetters(string key)
        {

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create($"{link}getlist&key={key}&type=json");
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

        public string GetLetterText(string key, int id)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create($"{link}getmail&key={key}&id={id}&forced=1");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                return stream.ReadToEnd();
            }
        }

        public string GetEmailLiveTime(string key)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create($"{link}livetime&key={key}");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                return stream.ReadToEnd();
            }
        }

        public string ProlongEmailLiveTime(string key)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create($"{link}update&key={key}");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                return stream.ReadToEnd();
            }
        }

        public string DeleteEmail(string key)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create($"{link}clear&key={key}");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                return stream.ReadToEnd();
            }
        }

        public string ClearEmail(string key)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create($"{link}clear&key={key}");
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
