using AAS.Tools.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using AAS.Tools.Types.IDs;

namespace AAS.Tools.Binders
{
    public class IDModelBinderProvider : IModelBinderProvider
    {
        private readonly IDModelBinder _binder = new();

        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            return (context.Metadata.ModelType == typeof(ID) || context.Metadata.ModelType == typeof(ID?)) ? _binder : null;
        }
    }
}
