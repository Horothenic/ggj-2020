using UnityEngine;

using Newtonsoft.Json;

namespace CFLFramework.Data
{
    public class DataManager : MonoBehaviour
    {
        #region FIELDS

        private const string UserDataKey = "UserData";
        private const string TemporalUserDataKey = "TemporalUserData";

        #endregion

        #region PROPERTIES

        public UserData User { get; private set; }
        public UserData TemporalUser { get; private set; }

        public bool IsSynchronizationPending { get => TemporalUser.Data.Count > 0; }

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            LoadLocalData();
        }

        private void LoadLocalData()
        {
            User = JsonConvert.DeserializeObject<UserData>(PlayerPrefs.GetString(UserDataKey), new GenericConverter());
            TemporalUser = JsonConvert.DeserializeObject<UserData>(PlayerPrefs.GetString(TemporalUserDataKey), new GenericConverter());

            if (User == null)
                User = new UserData();

            if (TemporalUser == null)
                TemporalUser = new UserData();

            SaveLocalData();
        }

        public void ResetUser()
        {
            User = new UserData();
            TemporalUser = new UserData();
            SaveLocalData();
        }

        public void ResetTemporalUser()
        {
            TemporalUser = new UserData();
            SaveLocalData();
        }

        private void SaveLocalData()
        {
            PlayerPrefs.SetString(UserDataKey, SerializeUser(User));
            PlayerPrefs.SetString(TemporalUserDataKey, SerializeUser(TemporalUser));
        }

        private string SerializeUser(object user)
        {
            return JsonConvert.SerializeObject(user, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        public void SetUser(UserData user)
        {
            if (user == null)
                return;

            User = user;
            TemporalUser = new UserData();
            SaveLocalData();
        }

        public void SetAuthentication(string authenticationId, string authenticationUsername)
        {
            User = new UserData(authenticationId, authenticationUsername);
            SaveLocalData();
        }

        public void LinkEmail(string email)
        {
            User = new UserData(User, email);
            TemporalUser = new UserData(TemporalUser, email);
            SaveLocalData();
        }

        public void SetData<T>(string[] keys, T data, bool inArray = false)
        {
            User.AddData(keys, data, inArray);
            TemporalUser.AddData(keys, GetData<object>(keys));

            SaveLocalData();
        }

        public T GetData<T>(string[] keys, object defaultValue = null)
        {
            return User.GetData<T>(keys, defaultValue);
        }

        #endregion
    }
}
