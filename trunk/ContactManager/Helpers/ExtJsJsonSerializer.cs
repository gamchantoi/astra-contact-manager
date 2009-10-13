using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using ContactManager.Models;
using System.Reflection;

namespace ContactManager.Helpers
{
    public class ExtJsJsonSerializer
    {
        public static string JsonSerializeList<T>(T obj) where T : class
        {
            var list = obj as List<Client>;
            var str = new StringBuilder();
            str.Append("{" + "\"Clients\"" + ":[");
            foreach (var item in list)
            {
                str.Append(JsonSerialize(item));
                str.Append(",");
            }
            str.Remove(str.Length - 1, 1);
            str.Append("]}");
            return str.ToString();
        }

        public static string JsonSerialize<T>(T obj) where T : class
        {
            var sb = new StringBuilder("{");

            var parentType = obj.GetType(); // I get type from given object 

            // use reflection to retrieve all properties of that type
            var ms = parentType.GetMembers().Where(v => v.MemberType
                == MemberTypes.Property).ToList<MemberInfo>();

            const string doubleQuote = "\"";
            var counter = 0;
            var stringTypes = new List<String> { "String", "Guid", "Boolean", "Decimal" };

            //Following Types are used by Entity Framework. So no need to 
            //serialize those types.
            var ignoreEntityTypes = new List<String> { "EntityReference`1", "EntityCollection`1", "EntityState", "EntityKey", "EntitySetName" };

            //Start iteration to navigate each property
            foreach (PropertyInfo p in ms)
            {
                counter++;
                var kvp = new StringBuilder();
                var propertyName = p.Name;
                var propertyType = p.PropertyType;
                var propertyValue = p.GetValue(obj, p.GetIndexParameters());

                //If property type is matched with ignoreTypes then
                //goto next loop
                if (ignoreEntityTypes.Contains(propertyType.Name))
                    continue;

                if (stringTypes.Contains(propertyType.Name))
                {
                    if (propertyValue == null)
                        sb.Append(doubleQuote + propertyName + doubleQuote + ":" + "null");

                    else
                        sb.Append(doubleQuote + propertyName + doubleQuote + ":" + doubleQuote + propertyValue.ToString() + doubleQuote);
                }

                else if (propertyType != null && propertyType.IsPrimitive)
                {
                    sb.Append(doubleQuote + propertyName + doubleQuote + ":" + propertyValue.ToString());
                }

                //Still I have doubt how Date Time will be handled.
                //else if (propertyType.Name == "DateTime")
                //{
                //    var dt = (DateTime)propertyValue;
                //    sb.Append(doubleQuote + propertyName + doubleQuote + ":" + "new Date(" + dt.Ticks.ToString() + ")");
                //}
                //else if (propertyValue != null)
                //{
                //    sb.Append(doubleQuote + propertyType.Name + doubleQuote + ":" + doubleQuote + propertyValue.ToString() + doubleQuote);
                //    //If property value is another entity, then
                //    //call the method recursively.
                //    //sb.Append(JsonSerialize(propertyValue));
                //}
                else
                    continue;
                //If it is not the last property, then add comma
                if (counter < ms.Count)
                    sb.Append(",");
            }
            sb.Append("}");
            var result = sb.ToString().Replace(",}", "}");
            return result;
        }


        public static string Serialize(IList<Client> obj, string rootName)
        {
            var arrl = new List<string>();
            var objType = obj.GetType();
            StringBuilder builder = new StringBuilder();
            builder.Append("{" + rootName + ":");
            var first = true;
            foreach (var item in obj)
            {
                if (!first)
                {
                    builder.Append(",");
                    first = false;
                }
                builder.Append('{');
                var properties = item.GetType().GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    var prop = new StringBuilder();
                    if (prop.Length > 0)
                        prop.Append(",");
                    prop.Append(property.Name);
                    prop.Append(":");
                    prop.Append(property.GetValue(item, property.GetIndexParameters()));
                    builder.Append(prop);
                }
                builder.Append("}");
            }
            builder.Append("}");
            return builder.ToString();
        }



        public static string ToExtJsReader(object obj, string rootName)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                StringBuilder sb = new StringBuilder();
                sb.Append("{" + rootName + ":");
                sb.Append(Encoding.Default.GetString(ms.ToArray()));
                sb.Append("}");
                return sb.ToString();
            }
        }
    }
}
