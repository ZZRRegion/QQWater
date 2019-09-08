using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace QQWater
{
    public static class RGCommon
    {
        public static string Version => "1.0.0.0";
        public static string VersionTime => "2019-09-08";
        public static Models.MyQQ GetMyQQ()
        {
            Models.MyQQ myqq = new Models.MyQQ();
            XDocument doc = XDocument.Load("my.xml");
            foreach(XElement item in doc.Root.Nodes())
            {
                string qq = item.Attribute("qq").Value;
                string pwd = item.Attribute("pwd").Value;
                myqq.QQ = qq;
                myqq.Pwd = pwd;
            }
            return myqq;
        }
    }
}
