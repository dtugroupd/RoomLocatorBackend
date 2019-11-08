/// <summary>
/// 	<author>Amal Qasim, s132957</author>
/// </summary>

using System.Collections.Generic;

namespace RoomLocator.Domain.ViewModels
{
    public class ModcamInstallationsViewModel
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string NameSpace { get; set; }
        public IEnumerable<string> Components { get; set; }
      
    }
}