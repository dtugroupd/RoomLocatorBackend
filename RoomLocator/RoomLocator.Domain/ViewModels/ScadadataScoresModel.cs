using System.Runtime.CompilerServices;

namespace RoomLocator.Domain.ViewModels
{
   
    public class ScadadataScoresModel
    {
        public string Type { get; set; }
        public double Value { get; set; }
        
        public ScadadataScoresModel(string type, double value)
        {
            Type = type;
            Value = value;
        }
    }
    
    
}