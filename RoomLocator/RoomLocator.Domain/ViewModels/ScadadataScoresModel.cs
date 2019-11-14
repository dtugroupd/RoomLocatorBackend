using System.Runtime.CompilerServices;

namespace RoomLocator.Domain.ViewModels
{
    /// <summary>
    ///    <author>Andreas Gøricke, s153804</author>
    /// </summary>
    public class ScadadataScoresModel
    {
        public string Type { get; set; }
        public string Value { get; set; }
        
        public ScadadataScoresModel(string type, string value)
        {
            Type = type;
            Value = value;
        }
    }
    
    
}