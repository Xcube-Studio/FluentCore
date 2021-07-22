﻿using FluentCore.Model.Launch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentCore.Interface
{
    public interface IDependencesResolver
    {
        GameCore GameCore { get; set; }

        IEnumerable<IDependence> GetDependences();

        IEnumerable<IDependence> GetLostDependences();
    }
}