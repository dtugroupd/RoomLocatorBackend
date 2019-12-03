using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomLocator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Data.Config
{
    class SensorFluentConfig : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder.HasKey(x => new { x.Id });
        }
    }
}
