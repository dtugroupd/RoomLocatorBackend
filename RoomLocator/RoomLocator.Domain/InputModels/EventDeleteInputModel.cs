using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.InputModels
{
    public class EventDeleteInputModel
    {
        public string Id { get; set; }
        public string LocationId { get; set; }
    }
}
