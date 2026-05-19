using Microsoft.AspNetCore.Components;
using OverlapssytemApplication.Common.Result;
using OverlapssytemApplication.Common.Errors;
using Microsoft.Extensions.Logging;

public abstract class AppComponentBase : ComponentBase
{

    //Vi bruger AppComponentBase som en baseklasse for alle vores Blazor-komponenter for at centralisere håndteringen af loading state og fejl.
    //På denne måde undgår vi en masse redundant kode i hver komponent

    [Inject]
    protected ILogger<AppComponentBase> Logger { get; set; } = default!;

    // IsLoading bruges til at indikere, om der er en asynkron handling i gang, så vi kan vise en loading-indikator i UI'et.
    protected bool IsLoading { get; private set; }

    // CurrentError bruges til at holde styr på den aktuelle fejl, der skal vises i UI'et.
    protected Error? CurrentError { get; set; }

    // Dictionary til at holde styr på valideringsfejl for hvert felt
    protected Dictionary<string, string> _fieldErrors = new();

    // Hjælpefunktion til at generere en unik nøgle for et felt baseret på ID og felt navn 
    //Bruges til at håndtere fejl-meddelelser i DepartmentTask tabellen og EmployeePhone tabellen
    protected string GetKey(int id, string field)
    {
        return $"{id}_{field}";
    }

    // Hjælpefunktioner til validering og visning af fejl
    protected bool HasError(string field) => _fieldErrors.ContainsKey(field);
    protected string? GetError(string field) => _fieldErrors.TryGetValue(field, out var error) ? error : null;

    // Overloads for at håndtere fejl baseret på ID og felt navn
    protected bool HasErrorForId(int id, string field) => _fieldErrors.ContainsKey(GetKey(id, field));
    protected string? GetErrorForId(int id, string field) => _fieldErrors.TryGetValue(GetKey(id, field), out var error) ? error : null;



    // Metode til at udføre en asynkron handling med indbygget håndtering af loading state og fejl
    protected async Task ExecuteAsync(Func<Task<Result>> action)
    {
        try
        {
            // Når vi starter en asynkron handling, sætter vi IsLoading til true og nulstiller CurrentError
            IsLoading = true;
            CurrentError = null;

            // Vi kalder StateHasChanged for at opdatere UI'et, så brugeren kan se, at der er en handling i gang
            StateHasChanged();

            var result = await action();

            // Hvis handlingen ikke lykkedes, sætter vi CurrentError til den fejl, der blev returneret
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
