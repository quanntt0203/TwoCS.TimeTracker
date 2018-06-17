namespace TwoCS.TimeTracker.Core.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json.Serialization;

    public static class ContentHelper
    {
        public static TModel JsonToObj<TModel>(this string json)
        {
            return JsonConvert.DeserializeObject<TModel>(json);
        }

        public static object JsonToObj(this string json)
        {
            return JsonConvert.DeserializeObject(json);
        }

        public static JToken DeserializeWithLowerCasePropertyNames(string json)
        {
            using (TextReader textReader = new StringReader(json))
            using (JsonReader jsonReader = new LowerCasePropertyNameJsonReader(textReader))
            {
                JsonSerializer ser = new JsonSerializer();
                return ser.Deserialize<JToken>(jsonReader);
            }
        }

        public static string ObjToJson<TModel>(this TModel model)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(model, jsonSettings);
        }

        public static string FirstCharToLower(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToLower() + input.Substring(1);
            }
        }

        public static StringContent ObjToHttpContent<TModel>(this TModel model)
        {
            return new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        }
    }

    public class LowerCasePropertyNameJsonReader : JsonTextReader
    {
        public LowerCasePropertyNameJsonReader(TextReader textReader)
            : base(textReader)
        {
        }

        public override object Value
        {
            get
            {
                if (TokenType == JsonToken.PropertyName)
                    return ((string)base.Value).FirstCharToLower();

                return base.Value;
            }
        }
    }
}
