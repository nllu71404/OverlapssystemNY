using Microsoft.AspNetCore.Components;
using OverlapssytemApplication.Common.Result;
using OverlapssytemApplication.Common.Errors;
using Microsoft.Extensions.Logging;

public abstract class AppComponentBase : ComponentBase
{
    [Inject]
    protected ILogger<AppComponentBase> Logger { get; set; } = default!;

    protected bool IsLoading { get; private set; }


    protected Error? CurrentError { get; set; }

    protected async Task ExecuteAsync(Func<Task<Result>> action)
    {
        try
        {
            IsLoading = true;
            CurrentError = null;

            StateHasChanged();

            var result = await action();

            if (!result.Success)
            {
                CurrentError = result.Error;
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex,
                "Unhandled exception in component {Component}",
                GetType().Name);

            CurrentError = Error.Technical("Der opstod en uventet fejl");
        }
        finally
        {
            IsLoading = false;
            StateHasChanged();
        }
    }

    protected async Task ExecuteAsync<T>(Func<Task<Result<T>>> action)
    {
        try
        {
            IsLoading = true;
            CurrentError = null;

            StateHasChanged();

            var result = await action();

            if (!result.Success)
            {
                CurrentError = result.Error;
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex,
                "Unhandled exception in component {Component}",
                GetType().Name);

            CurrentError = Error.Technical("Der opstod en uventet fejl");
        }
        finally
        {
            IsLoading = false;
            StateHasChanged();
        }
    }

    protected void ClearError()
    {
        CurrentError = null;
    }

    protected async Task<T?> ExecuteAsyncT<T>(Func<Task<Result<T>>> action)
    {
        try
        {
            IsLoading = true;
            CurrentError = null;

            StateHasChanged();

            var result = await action();

            if (!result.Success)
            {
                CurrentError = result.Error;
                return default;
            }

            return result.Value;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex,
                "Unhandled exception in component {Component}",
                GetType().Name);

            CurrentError = Error.Technical("Der opstod en uventet fejl");
            return default;
        }
        finally
        {
            IsLoading = false;
            StateHasChanged();
        }
    }
}
