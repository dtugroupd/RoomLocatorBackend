using System.Collections.Generic;

namespace RoomLocator.Domain.ViewModels
{
    /// <summary>
    ///     <author>Amal Qasim, s132957</author>
    /// </summary>
    
    public class NumberOfPeopleViewModel
    {
        public string SectionId { get; set; }

        public IEnumerable<string> People { get; set; }
    }
}