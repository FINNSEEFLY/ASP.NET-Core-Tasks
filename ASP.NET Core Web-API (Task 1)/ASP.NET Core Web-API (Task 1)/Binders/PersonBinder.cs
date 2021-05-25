using ASP.NET_Core_Web_API__Task_1_.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_Core_Web_API__Task_1_.Binders
{
    public class PersonBinder : IModelBinder
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


            Guid userGuid;
            try
            {
                userGuid = new Guid(Encoding.UTF8.GetString(Convert.FromBase64String(value)));
            }
            catch (DecoderFallbackException)
            {
                bindingContext.ModelState.TryAddModelError(modelName, "Invalid utf8 string");
                return Task.CompletedTask;
            }
            catch (FormatException)
            {
                bindingContext.ModelState.TryAddModelError(modelName, "Invalid base64 guid format");
                return Task.CompletedTask;
            }
            catch (ArgumentException)
            {
                bindingContext.ModelState.TryAddModelError(modelName, "Invalid encoded value");
                return Task.CompletedTask;
            }
            catch
            {
                bindingContext.ModelState.TryAddModelError(modelName, "Unknown error");
                return Task.CompletedTask;
            }

            var model = new Person { Id = userGuid };

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}