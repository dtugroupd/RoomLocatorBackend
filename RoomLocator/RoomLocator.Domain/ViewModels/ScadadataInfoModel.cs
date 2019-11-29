using System.Collections.Generic;
using System.ComponentModel.Design;

namespace RoomLocator.Domain.ViewModels
{
    /// <summary>
    ///    <author>Andreas Gøricke, s153804</author>
    /// </summary>
    public class ScadadataInfoModel
    {
        public string Status { get; set;  }

        public IEnumerable<ScadadataScoresModel> Details { get; set; }
        
        public ScadadataInfoModel(string status, IEnumerable<ScadadataScoresModel> details)
        {
            Status = status;
            Details = details;
        }
    }
}