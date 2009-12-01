﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ContactManager.SSH.Intefaces;
using Tamir.SharpSsh;

namespace ContactManager.SSH.Models
{
    public class SSHRepository : ISSHRepository
    {
        private SshStream _sshStream;

        public string Connect(string host, string username, string password)
        {
            _sshStream = new SshStream(host, username, password)
                             {
                                 RemoveTerminalEmulationCharacters = true,
                                 Prompt = @"\[[^@]*@[^]]*]\s>\s?$"
                             };
            return _sshStream.ReadResponse();
        }

        public void Disconnect()
        {
            if (_sshStream != null)
                _sshStream.Close();
        }

        public string RunCommand(string command)
        {
            _sshStream.Write(command);
            return ParseResponse(_sshStream.ReadResponse(), command);
        }

        public string BuildCommand(String key, String value)
        {
            if (String.IsNullOrEmpty(value)) return String.Empty;
            var builder = new StringBuilder(value);
            builder.Replace("\"", "\\\"")
                .Replace("(", "\\(")
                .Replace(")", "\\)")
                .Replace("[", "\\[")
                .Replace("]", "\\]");
            return String.Format("{0}=\"{1}\" ", key, builder.ToString().Trim());
        }

        public string ParseResponse(string response, string command)
        {
            string error;
            if (!IsSuccess(response, out error))
                throw new Exception(" Command: '" + command + "' - " + error);
            return response;
        }

        public bool IsSuccess(string result, out string error)
        {
            error = "";
            if (result.Contains("input does not match any value"))
                error = "input does not match any value";
            if (result.Contains("invalid value"))
                error = "invalid value";
            if (result.Contains("invalid item number"))
                error = "invalid item number";
            if (result.Contains("no such command or directory"))
                error = "no such command or directory";
            if (result.Contains("not enough permissions"))
                error = "not enough permissions";
            if (error.Length > 0)
                return false;
            return true;
        }

        public string GetValue(IDictionary<string, string> properties, string property)
        {
            string value;
            return properties.TryGetValue(property, out value) ? value : string.Empty;
        }

        public Dictionary<string, string> BuildProperties(string str)
        {
            var result = new Dictionary<string, string>();
            try
            {
                var tmpKey = string.Empty;
                var Key = string.Empty;
                var tmpValue = string.Empty;
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

        public string GetComment(string str, IDictionary<string, string> kvp)
        {
            if (!str.Trim().StartsWith(";;;")) return str;

            var _string = str.Split('\n');
            foreach (var item in _string)
            {
                if (!str.Trim().StartsWith(";;;")) continue;
                kvp.Add("comment", item.Trim().Substring(3).Trim());
                return str.Replace(item, "");
            }
            return str;
        }
    }
}