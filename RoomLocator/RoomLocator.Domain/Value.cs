using System;

namespace RoomLocator.Domain
{
    public class Value
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public static Value Create(string text) => new Value {Id = Guid.NewGuid().ToString(), Text = text};
    }
}