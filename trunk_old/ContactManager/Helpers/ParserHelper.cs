using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ContactManager.Models;

namespace ContactManager.Helpers
{
    public class ParserHelper
    {
        private const string PATTERN = @"
                ^?(?:\s*)(?<Section>\d+)(?:\s+)
                ( (?!\d+\s)                 # Stop matching new section
                  (?<Key>[^=]*)(?:\=)       # Key found
                  (?(?=[\x22\x27])          # If lookahead shows quotes
                     (?:[\x22\x27])         # Get quoted value
                     (?<Value>[^\x22\x27]*)
                     (?:[\x22\x27])
                   |                        # else
                     (?<Value>[^\s]*)       # Get not quoted
                   )
                  (?:\s*)
                )+                          # Many Key Value Pairs";

        public List<PPPSecret> BuildPPPSecrets(String ppp)
        {
            var pppSec = new List<PPPSecret>();
            foreach (var val in Regex.Split(ppp, @"\r\n\s?(?:\d+)"))
            {
                var pppS = new PPPSecret();
                var properties = BuildProperties(val);
                pppS.Name = GetValue(properties, "name");
                pppS.Password = GetValue(properties, "password");
                pppS.Profile = GetValue(properties, "profile");
                pppS.LocalAddress = GetValue(properties, "local-address");
                pppS.RemoteAddress = GetValue(properties, "remote-address");
                pppS.Status = int.Parse(GetValue(properties, "status"));
                pppS.Comment = GetValue(properties, "comment");
                if (!String.IsNullOrEmpty(pppS.Name))
                    pppSec.Add(pppS);
            }
            return pppSec;
        }

        public List<Profile> BuildProfiles(string profiles)
        {
            var _profiles = new List<Profile>();
            foreach (var val in Regex.Split(profiles, "\r\n\r\n"))
            {
                var _profile = new Profile();
                var properties = BuildProperties(val);
                _profile.Name = GetValue(properties, "name");
                _profile.LocalAddress = GetValue(properties, "local-address");
                _profile.PoolName = GetValue(properties, "remote-address");
                _profile.RateLimit = GetValue(properties, "rate-limit");
                if (!String.IsNullOrEmpty(_profile.Name))
                    _profiles.Add(_profile);
            }



            //var r = new Regex(PATTERN, RegexOptions.IgnorePatternWhitespace);
            //var r2 = new Regex(@"\x0D\x0A\x20+", RegexOptions.IgnorePatternWhitespace);
            //var str = r2.Split(queue);
            //var safeQueue = new StringBuilder();
            //foreach (var s in str)
            //{
            //    safeQueue.Append(s);
            //}
            //var m = r.Match(safeQueue.ToString());
            //while (m.Success)
            //{
            //    var gk = m.Groups["Key"];
            //    var gv = m.Groups["Value"];
            //    var profile = new Profile();
            //    for (var i = 0; i < gk.Captures.Count; i++)
            //    {
            //        switch (gk.Captures[i].Value)
            //        {
            //            case "name":
            //                profile.Name = gv.Captures[i].Value.Trim();
            //                break;
            //            case "* name":
            //                profile.Name = gv.Captures[i].Value.Trim();
            //                break;
            //            case "local-address":
            //                profile.LocalAddress = gv.Captures[i].Value.Trim();
            //                break;
            //            case "remote-address":
            //                profile.PoolName = gv.Captures[i].Value.Trim();
            //                break;
            //            case "rate-limit":
            //                profile.RateLimit = gv.Captures[i].Value.Trim();
            //                break;
            //            //case "max-limit":
            //            //    var l = gv.Captures[i].Value.Trim().Split(new Char[] { '/' });
            //            //    tariff.MaxUploadLimit = BuildSpeed(l[0].Trim());
            //            //    tariff.MaxDownloadLimit = BuildSpeed(l[1].Trim());
            //            //    break;
            //        }
            //    }
            //    if (!String.IsNullOrEmpty(profile.Name))
            //        profiles.Add(profile);
            //    m = m.NextMatch();
            //}
            return _profiles;
        }

        public List<Pool> BuildIPPools(String pool)
        {
            var pools = new List<Pool>();
            foreach (var val in Regex.Split(pool, "\r\n\r\n"))
            {   var properties = BuildProperties(val);
                var name = GetValue(properties, "name");

                if (name.Contains("(") || name.Contains(")")) continue;
                
                var _pool = new Pool
                                {
                                    Name = name,
                                    Addresses = GetValue(properties, "ranges"),
                                    NextPoolName = GetValue(properties, "next-pool")
                                };
                if (!String.IsNullOrEmpty(_pool.Name))
                    pools.Add(_pool);
            }
            return pools;
        }

        public List<ActiveConnections> BuildActiveConnections(String connections)
        {
            var _connections = new List<ActiveConnections>();
            var r = new Regex(PATTERN, RegexOptions.IgnorePatternWhitespace);
            var m = r.Match(connections);
            while (m.Success)
            {
                var gk = m.Groups["Key"];
                var gv = m.Groups["Value"];
                var pppS = new ActiveConnections();
                for (var i = 0; i < gk.Captures.Count; i++)
                {
                    switch (gk.Captures[i].Value)
                    {
                        case "name":
                            pppS.Name = gv.Captures[i].Value.Trim();
                            break;
                        case "service":
                            pppS.Service = gv.Captures[i].Value.Trim();
                            break;
                        case "caller-id":
                            pppS.CallerId = gv.Captures[i].Value.Trim();
                            break;
                        case "address":
                            pppS.Address = gv.Captures[i].Value.Trim();
                            break;
                        case "uptime":
                            pppS.Uptime = gv.Captures[i].Value.Trim();
                            break;
                        case "encoding":
                            pppS.Encoding = gv.Captures[i].Value.Trim();
                            break;
                        case "session-id":
                            pppS.SessionId = gv.Captures[i].Value.Trim();
                            break;
                        case "limit-bytes-in":
                            pppS.LimitBytesIn = gv.Captures[i].Value.Trim();
                            break;
                        case "limit-bytes-out":
                            pppS.LimitBytesOut = gv.Captures[i].Value.Trim();
                            break;
                    }
                }
                if (!String.IsNullOrEmpty(pppS.Name))
                    _connections.Add(pppS);
                m = m.NextMatch();
            }
            return _connections;
        }

        private int BuildSpeed(string p)
        {
            p = p.Replace("M", "000000");
            p = p.Replace("k", "000");
            return int.Parse(p);
        }

        //private bool IsNameField(string p, PPPSecret user)
        //{
        //    //Check if Name contains Comment
        //    if (p.EndsWith("name"))
        //    {
        //        if (p.StartsWith("X"))
        //            user.Status = 0;
        //        else
        //            user.Status = 1;
        //        return true;
        //    }
        //    return false;
        //}

        private static Dictionary<string, string> BuildProperties(string str)
        {
            var result = new Dictionary<string, string>();
            try
            {
                var tmpKey = String.Empty;
                var Key = String.Empty;
                var tmpValue = String.Empty;
                var quotesVal = false;
                var status = Regex.Match(str, @"\s+\w\s");
                str = GetComment(str, result);
                result.Add("status", status.Success ? "0" : "1");
                for (var i = 0; i < str.Length; i++)
                {
                    switch (str[i].ToString())
                    {
                        case "=":
                            if (Key.Length > 0)
                            {
                                var val = tmpValue.Replace(tmpKey, "").Trim();
                                if (!quotesVal)
                                    result.Add(Key, val);
                                else if (val.EndsWith("\""))
                                {
                                    result.Add(Key, val.Remove(val.Length - 1, 1).Remove(0, 1));
                                }
                                else
                                    continue;
                            }
                            quotesVal = str[i + 1].ToString().Equals("\"");

                            Key = tmpKey;
                            tmpKey = "";
                            tmpValue = "";
                            break;
                        case " ":
                            tmpKey = "";
                            tmpValue += str[i].ToString();
                            break;
                        default:
                            tmpKey += str[i].ToString();
                            tmpValue += str[i].ToString();
                            break;
                    }
                }
                result.Add(Key, tmpValue);
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }

        private static string GetComment(string str, IDictionary<string, string> kvp )
        {
            if (!str.Trim().StartsWith(";;;")) return str;

            var _string = str.Split('\n');
            foreach(var item in _string)
            {
                if (!str.Trim().StartsWith(";;;")) continue;
                kvp.Add("comment", item.Trim().Substring(3).Trim());
                return str.Replace(item, "");
            }
            return str;
        }

        private static string GetValue(IDictionary<string, string> properties, string property)
        {
            string value;
            return properties.TryGetValue(property, out value) ? value : string.Empty;
        }
    }
}