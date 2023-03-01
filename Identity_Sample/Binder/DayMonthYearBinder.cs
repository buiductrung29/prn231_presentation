using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Identity_Sample.Binder
{
    public class DayMonthYearBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            string modelName = bindingContext.ModelName;
            ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult == null)
            {
                return Task.CompletedTask;
            }
            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);
            string valueString = valueProviderResult.FirstValue;
            if (string.IsNullOrEmpty(valueString))
            {
                return Task.CompletedTask;
            }
            DateTime? date = null;
            try
            {
                date = DateTime.ParseExact(valueString, "dd/MM/yyyy", null);
            }
            catch
            {
                bindingContext.ModelState.TryAddModelError(modelName, "Input date error - need dd/MM/yyyy format.");
                return Task.CompletedTask;
            }
            if (date < DateTime.Parse("1/1/1945"))
            {
                bindingContext.ModelState.TryAddModelError(modelName, "Internal System Error");
                return Task.CompletedTask;
            }
            bindingContext.Result = ModelBindingResult.Success(date);
            return Task.CompletedTask;
        }
    }
}