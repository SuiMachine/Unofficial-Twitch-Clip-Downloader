using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TwitchClipDownloader
{
    [Serializable]
    public class ApplicationConfig
    {
        [XmlElement] public string UserName { get; set; }
        [XmlElement] public string FilePath { get; set; }
        [XmlElement] public uint ClipLimit { get; set; }
        [XmlElement] public string BearerToken { get; set; }

        [XmlIgnore] static readonly string FILENAME = "Config.xml";


        public ApplicationConfig()
        {
            UserName = "";
            BearerToken = "";
            FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Clips");
            ClipLimit = 20;
        }

        internal static ApplicationConfig Load()
        {
            if (File.Exists(FILENAME))
            {
                ApplicationConfig obj;
                XmlSerializer serializer = new XmlSerializer(typeof(ApplicationConfig));
                FileStream fs = new FileStream(FILENAME, FileMode.Open); //Extension is NOT *.xml on purpose so that in case of streaming monitor, it's not tied to normal text editors, as it contains authy token (password).
                obj = (ApplicationConfig)serializer.Deserialize(fs);
                fs.Close();
                return obj;
            }
            else
                return new ApplicationConfig();
        }

        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ApplicationConfig));
            StreamWriter fw = new StreamWriter(FILENAME);
            serializer.Serialize(fw, this);
            fw.Close();
        }
    }
}
