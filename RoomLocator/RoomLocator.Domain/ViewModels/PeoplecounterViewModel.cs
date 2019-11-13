using System.Collections.Generic;

namespace RoomLocator.Domain.ViewModels
{
    /// <summary>
    ///     <author>Amal Qasim, s132957</author>
    /// </summary>
    public class PeoplecounterViewModel
    {
        public string Installation { get; set; }
        public IEnumerable<DataViewModel> Data { get; set; }
    }

    public class DataViewModel
    {
           public string Device { get; set; }
                public string Projection { get; set; }
                public string Count { get; set; }
                public IEnumerable<ItemsViewModel> Items { get; set; }
    }
    public class ItemsViewModel
    {
        public string Date { get; set; }
        public string In { get; set; }
        public string Out { get; set; }
        
    }
}