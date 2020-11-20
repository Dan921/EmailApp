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
        private string site = "https://post-shift.ru/api.php?action=";

        public EmailInfo GetNewMailInfo()
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(site + "new&type=json");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                return JsonConvert.DeserializeObject<EmailInfo>(stream.ReadToEnd());
            }
        }

        public List<Letter> GetListLetters(string key)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(site + $"getlist&key={key}&type=json");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                return JsonConvert.DeserializeObject<List<Letter>>(stream.ReadToEnd());
            }
        }

        public string GetLetterText(string key, int id)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(site + $"getmail&key={key}&id={id}&type=json");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                return stream.ReadToEnd();
            }
        }

        public string GetEmailLiveTime(string key)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(site + $"livetime&key={key}");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                return stream.ReadToEnd();
            }
        }

        public string ProlongEmailLiveTime(string key)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(site + $"update&key={key}");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                return stream.ReadToEnd();
            }
        }

        public string DeleteEmail(string key)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(site + $"clear&key={key}");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                return stream.ReadToEnd();
            }
        }

        public string DeleteAllEmailByIP()
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(site + $"deleteall");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                 resp.GetResponseStream(), Encoding.UTF8))
            {
                return stream.ReadToEnd();
            }
        }
    }
}
