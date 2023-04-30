#region

using AAS.Tools.Types.IDs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

#endregion

namespace AAS.Tools.Binders;

public class IDModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

        string? value;

        if (bindingContext.HttpContext.Request.Method == "POST" && bindingContext.BindingSource == BindingSource.Body)
        {
            using StreamReader sr = new(bindingContext.HttpContext.Request.Body);
            value = sr.ReadToEndAsync().Result.Trim('"');
            sr.Dispose();
        }
        else
        {
            value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
        }

        if (string.IsNullOrWhiteSpace(value) || value == "null")
            bindingContext.Result = ModelBindingResult.Success(null);
        else
            bindingContext.Result = ModelBindingResult.Success(ID.Parse(value));


        return Task.CompletedTask;
    }
}