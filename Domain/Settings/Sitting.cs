using System;
namespace Domain.Settings
{
    public class Sitting
    {
        public string Key { get; set; }
        public string Value { get; set; }

        protected Sitting() { } // EF

        public Sitting(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
