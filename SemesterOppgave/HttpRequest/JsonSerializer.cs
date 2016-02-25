using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace HttpRequest {
    public static class JsonSerializer<TType> where TType : class
    {
        /// <summary>
        /// Serializes an object to JSON
        /// </summary>
        public static string Serialize(TType instance)
        {
            var serializer = new DataContractJsonSerializer(typeof(TType));
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, instance);
                return Encoding.Default.GetString(stream.ToArray());
            }
        }

        public static string SerializeList(List<TType> instance)
        {
            var serializer = new DataContractJsonSerializer(typeof(List<TType>));
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, instance);
                return Encoding.Default.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// DeSerializes an object from JSON
        /// </summary>
        public static TType DeSerialize(string json)
        {
            using (var stream = new MemoryStream(Encoding.Default.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(typeof(TType));
                return serializer.ReadObject(stream) as TType;
            }
        }

        /// <summary>
        /// DeSerializes an List of objects from JSON
        /// </summary>
        public static List<TType> DeSerializeAsList(string json)
        {
            try
            {
                return JsonSerializer<List<TType>>.DeSerialize(json);
            }
            catch (Exception e)
            {
                return new List<TType>();
            }
        }
    }
}