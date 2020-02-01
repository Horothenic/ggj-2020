using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

namespace CFLFramework.Data
{
    public class UserData
    {
        #region FIELDS

        private const string IdKey = "id";
        private const string UsernameKey = "username";
        private const string EmailKey = "email";
        private const string DataKey = "data";

        #endregion

        #region PROPERTIES

        [JsonProperty(IdKey)] public string Id { get; private set; }
        [JsonProperty(UsernameKey)] public string Username { get; private set; }
        [JsonProperty(EmailKey)] public string Email { get; private set; }
        [JsonProperty(DataKey)] public Dictionary<string, object> Data { get; private set; }

        #endregion

        #region CONSTRUCTORS

        public UserData() : this(null, null, null, new Dictionary<string, object>()) { }

        public UserData(string username) : this(null, username, null, new Dictionary<string, object>()) { }

        public UserData(string id, string username) : this(id, username, null, new Dictionary<string, object>()) { }

        public UserData(UserData user, string email) : this(user.Id, user.Username, email, user.Data) { }

        [JsonConstructor]
        public UserData(string id, string username, string email, Dictionary<string, object> data)
        {
            Id = id;
            Username = username;
            Email = email;
            Data = data;
        }

        #endregion

        #region BEHAVIORS

        public void AddData<T>(string[] keys, T data, bool inArray = false)
        {
            Dictionary<string, object> dictionary = Data;
            string finalKey = keys.Last();

            for (int i = 0; i < keys.Length; i++)
            {
                string currentKey = keys[i];
                if (currentKey != finalKey)
                {
                    if (!dictionary.ContainsKey(currentKey))
                        dictionary.Add(currentKey, new Dictionary<string, object>());

                    dictionary = dictionary[currentKey] as Dictionary<string, object>;
                }
                else
                {
                    if (dictionary.ContainsKey(currentKey))
                    {
                        if (inArray)
                        {
                            try
                            {
                                ((List<object>)dictionary[currentKey]).Add(data);
                            }
                            catch (InvalidCastException)
                            {
                                dictionary[currentKey] = new List<object>();
                                ((List<object>)dictionary[currentKey]).Add(data);
                            }
                        }
                        else
                        {
                            dictionary[currentKey] = (T)data;
                        }
                    }
                    else
                    {
                        if (inArray)
                        {
                            dictionary[currentKey] = new List<object>();
                            ((List<object>)dictionary[currentKey]).Add(data);
                        }
                        else
                        {
                            dictionary.Add(currentKey, (T)data);
                        }
                    }
                }
            }
        }

        public T GetData<T>(string[] keys, object defaultValue)
        {
            Dictionary<string, object> dictionary = Data;
            object data = defaultValue;
            string finalKey = keys.Last();

            try
            {
                for (int i = 0; i < keys.Length; i++)
                {
                    string currentKey = keys[i];
                    if (currentKey != finalKey)
                    {
                        if (!dictionary.ContainsKey(currentKey))
                            break;

                        dictionary = dictionary[currentKey] as Dictionary<string, object>;
                    }
                    else
                    {
                        data = dictionary[currentKey];
                    }
                }

                if (typeof(T) != typeof(object) && data.GetType() == typeof(List<object>))
                    data = ConvertList((List<object>)data, typeof(T));

                return (T)CastObject(data);
            }
            catch (KeyNotFoundException)
            {
                return (T)CastObject(defaultValue);
            }
        }

        private object ConvertList(List<object> items, Type type, bool performConversion = false)
        {
            var containedType = type.GenericTypeArguments.First();
            var enumerableType = typeof(System.Linq.Enumerable);
            var castMethod = enumerableType.GetMethod(nameof(System.Linq.Enumerable.Cast)).MakeGenericMethod(containedType);
            var toListMethod = enumerableType.GetMethod(nameof(System.Linq.Enumerable.ToList)).MakeGenericMethod(containedType);

            IEnumerable<object> itemsToCast = null;
            itemsToCast = items.Select(item => Convert.ChangeType(item, containedType));

            var castedItems = castMethod.Invoke(null, new[] { itemsToCast });

            return toListMethod.Invoke(null, new[] { castedItems });
        }

        private object CastObject(object data)
        {
            if (data.GetType() == typeof(double))
                data = Convert.ToSingle(data);

            return data;
        }

        #endregion
    }
}
