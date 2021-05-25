using ASP.NET_Core_Web_API__Task_1_.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_Web_API__Task_1_.Binders
{
    public class PointBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;
            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            var coordinate = 0;
            var coords = value.Split(',').Where(x => int.TryParse(x, out coordinate)).Select(_ => coordinate).ToArray();


            if (coords.Length != 3)
            {
                bindingContext.ModelState.TryAddModelError(modelName, "Invalid location parameters");
                return Task.CompletedTask;
            }

            var model = new Point
            {
                X = coords[0],
                Y = coords[1],
                Z = coords[2]
            };

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}