using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Core.Data
{
    public interface ICustomModelBuilder
    {
        ModelBuilder Build(ModelBuilder modelBuilder);
    }
}
