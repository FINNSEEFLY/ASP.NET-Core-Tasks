using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NET_Core_Web_API__Task_1_.Binders;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_API__Task_1_.Data
{
    [ModelBinder(BinderType = typeof(PersonBinder))]
    public class Person
    {
        public Guid Id { get; init; }
    }
}
