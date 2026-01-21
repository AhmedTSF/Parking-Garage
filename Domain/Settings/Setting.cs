using System;
namespace Domain.Settings
{
    public class Setting
    {
        public string Key { get; set; }
        public string Value { get; set; }

        protected Setting() { } // EF

        public Setting(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
