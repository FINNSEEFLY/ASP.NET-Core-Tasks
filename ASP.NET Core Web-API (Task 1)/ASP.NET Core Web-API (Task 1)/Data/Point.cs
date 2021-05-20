using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NET_Core_Web_API__Task_1_.Binders;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_API__Task_1_.Data
{
    [ModelBinder(BinderType = typeof(PointBinder))]
    public class Point
    {
        public int X { get; init; }
        public int Y { get; init; }
        public int Z { get; init; }

    }
}
