using HonorFlightScreening.Data;
using HonorFlightScreening.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace HonorFlightScreening.Components.Pages
{
    public partial class VeteranScreeningForm: ComponentBase
    {
        [Inject] private VeteranScreeningService ScreeningService { get; set; } = default!;
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

        [Parameter] public int? Id { get; set; }

        private VeteranScreening _screening = new();
        private string? _currentUserId;
        private int NotesLength => _screening?.Notes?.Length ?? 0;
        private bool IsNewScreening => !Id.HasValue;
        private const string Yes = "Yes";
        private const string No = "No";
        private const string Maybe = "Maybe";

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            _currentUserId = authState.User.Identity?.Name;

            if (Id.HasValue && !string.IsNullOrEmpty(_currentUserId))
            {
                var existingScreening = await ScreeningService.GetScreeningAsync(Id.Value);
                if (existingScreening != null)
                {
                    _screening = existingScreening;
                }
                else
                {
                    Navigation.NavigateTo("/FlightSummary");
                }
            }
        }

        private async Task SaveScreening()
        {
            if (string.IsNullOrEmpty(_currentUserId))
                return;

            if (IsNewScreening)
            {
                _screening = await ScreeningService.CreateScreeningAsync(_screening);
            }
            else
            {
                await ScreeningService.UpdateScreeningAsync(_screening);
            }

            Navigation.NavigateTo("/FlightSummary");
        }



        private void GoBack()
        {
            Navigation.NavigateTo("/FlightSummary");
        }

        private void OnNotesInput(ChangeEventArgs e)
        {
            _screening.Notes = e.Value?.ToString() ?? string.Empty;
            StateHasChanged();
        }

        private void SetLiftRequiredYes()
        {
            _screening.LiftRequired = Yes;
        }
        private void SetLiftRequiredNo()
        {
            _screening.LiftRequired = No;
        }
        private void SetLiftRequiredMaybe()
        {
            _screening.LiftRequired = Maybe;
        }

        private static string GetDeviceIcon(AssistiveDeviceType deviceType)
        {
            return deviceType switch
            {
                AssistiveDeviceType.Cane => "fas fa-blind",
                AssistiveDeviceType.Walker => "fas fa-walking",
                AssistiveDeviceType.Wheelchair => "fas fa-wheelchair",
                AssistiveDeviceType.Scooter => "fas fa-motorcycle",
                _ => "fas fa-question"
            };
        }
    }
}
