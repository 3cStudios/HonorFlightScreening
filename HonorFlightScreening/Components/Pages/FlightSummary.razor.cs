using HonorFlightScreening.Data;
using HonorFlightScreening.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace HonorFlightScreening.Components.Pages
{
    public partial class FlightSummary : ComponentBase
    {
        [Inject] private VeteranScreeningService ScreeningService { get; set; } = default!;
        [Inject] private HonorFlightService HonorFlightService { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        [Inject] private IJSRuntime JsRuntime { get; set; } = default!;
        [CascadingParameter] public required HonorFlightSession HonorFlightSession { get; set; }

        private List<VeteranScreening>? _screenings;
        private List<VeteranScreening>? _filteredScreenings;
        private List<HonorFlight>? _honorFlights;
        private int? _selectedHonorFlightId;

        private string? _searchVeteranName;
        private int? _searchSoundOffNumber;

        private readonly Dictionary<string, bool> _sortAscendingByColumn = new()
        {
            { "VeteranName", true },
            { "SoundOffNumber", true },
            { "HasMedicalAlerts", true },
            { "HasMobilityAlerts", true },
            { "HasSpecialAlerts", true },
            { "AssistiveDeviceType", true },
            { "LiftRequired", true },
            { "UseOxygen", true },
            { "HowMuchOxygen", true }
        };

        private bool _veteranNameFilterApplied;
        private bool _soundOffNumberFilterApplied;
        private string _sortColumn = "VeteranName";
        private int _summaryUseOxygen;
        private int _summaryLiftRequiredYes;
        private int _summaryMedicalAlerts;
        private int _summaryMobilityAlerts;
        private int _summarySpecialAlerts;
        private int _summaryPCPSignature;

        private bool _showAddFlightDatePicker = false;
        private DateTime _newHonorFlightDate = DateTime.Today;
        

        protected override async Task OnInitializedAsync()
        {
            
            _honorFlights = await HonorFlightService.GetAllHonorFlightsAsync();
            if (_honorFlights.Count > 0)
            {
                _selectedHonorFlightId = HonorFlightSession.SelectedHonorFlightId ?? _honorFlights.First().Id;
                
            }
            await LoadScreeningsForSelectedFlight();
            CalculateSummary();
        }
              

        private async Task LoadScreeningsForSelectedFlight()
        {
            if (_selectedHonorFlightId.HasValue)
            {
                _screenings = await ScreeningService.GetAllScreeningsByHonorFlightIdAsync(_selectedHonorFlightId.Value);
                _filteredScreenings = _screenings.ToList();
            }
            else
            {
                _screenings = new List<VeteranScreening>();
                _filteredScreenings = new List<VeteranScreening>();
            }
        }

        private async Task OnHonorFlightSelected(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value?.ToString(), out var id))
            {
                _selectedHonorFlightId = id;
                HonorFlightSession.SelectedHonorFlightId = id;
                await LoadScreeningsForSelectedFlight();
                CalculateSummary();
                StateHasChanged();
            }
        }

        private async Task DeleteHonorFlight()
        {
            if (_selectedHonorFlightId.HasValue)
            {
                bool confirmed = await JsRuntime.InvokeAsync<bool>(
                    "confirm",
                    "Are you sure you want to delete this Honor Flight and ALL associated screenings? This action cannot be undone."
                );
                if (!confirmed)
                    return;
                await HonorFlightService.DeleteHonorFlightAsync(_selectedHonorFlightId.Value);
                _honorFlights = await HonorFlightService.GetAllHonorFlightsAsync();
                _selectedHonorFlightId = _honorFlights.FirstOrDefault()?.Id;
                HonorFlightSession.SelectedHonorFlightId = _selectedHonorFlightId;
                await LoadScreeningsForSelectedFlight();
                StateHasChanged();
            }
        }

        private void ShowAddHonorFlightDatePicker()
        {
            _newHonorFlightDate = DateTime.Today;
            _showAddFlightDatePicker = true;
        }

        private async Task ConfirmAddHonorFlight()
        {
            var newFlight = new HonorFlight { FlightDate = _newHonorFlightDate };
            var created = await HonorFlightService.CreateHonorFlightAsync(newFlight);
            _honorFlights = await HonorFlightService.GetAllHonorFlightsAsync();
            _selectedHonorFlightId = created.Id;
            HonorFlightSession.SelectedHonorFlightId = created.Id;
            await LoadScreeningsForSelectedFlight();
            _showAddFlightDatePicker = false;
            StateHasChanged();
        }

        private void CancelAddHonorFlight()
        {
            _showAddFlightDatePicker = false;
        }

        private void ApplyVeteranNameFilter()
        {
            _veteranNameFilterApplied = true;
            FilterScreenings();
        }

        private void ClearVeteranNameFilter()
        {
            _veteranNameFilterApplied = false;
            _searchVeteranName = string.Empty;
            FilterScreenings();
        }

        private void ApplySoundOffNumberFilter()
        {
            _soundOffNumberFilterApplied = true;
            FilterScreenings();
        }

        private void ClearSoundOffNumberFilter()
        {
            _soundOffNumberFilterApplied = false;
            _searchSoundOffNumber = null;
            FilterScreenings();
        }

        private void FilterScreenings()
        {
            if (_screenings == null)
            {
                _filteredScreenings = new List<VeteranScreening>();
                return;
            }
            if (string.IsNullOrEmpty(_searchVeteranName) &&
                _searchSoundOffNumber == null)
            {
                _filteredScreenings = _screenings.ToList();
                return;
            }
            IEnumerable<VeteranScreening> query = _screenings;
            if (!string.IsNullOrWhiteSpace(_searchVeteranName))
                query = query.Where(s => s.VeteranName.Contains(_searchVeteranName, StringComparison.OrdinalIgnoreCase));
            if (_searchSoundOffNumber != null)
                query = query.Where(s => s.SoundOffNumber == _searchSoundOffNumber);
            _filteredScreenings = query.ToList();
        }

        private void NavigateToScreening(int id)
        {

            Navigation.NavigateTo($"/screening/{id}");
        }

        private void CreateNewScreening()
        {
            if (_selectedHonorFlightId.HasValue)
            {
                HonorFlightSession ??= new HonorFlightSession();
                HonorFlightSession.SelectedHonorFlightId = _selectedHonorFlightId.Value;
            }
            else
            {
                return; // No Honor Flight selected, cannot create a new screening
            }
            Navigation.NavigateTo("/screening/new");
        }

        private void OnSort(string column)
        {
            if (_sortColumn == column)
            {
                _sortAscendingByColumn[column] = !_sortAscendingByColumn[column];
            }
            else
            {
                _sortColumn = column;
                _sortAscendingByColumn.TryAdd(column, true);
            }
            ApplySort();
        }

        private string GetSortIcon(string column)
        {
            var ascending = !_sortAscendingByColumn.TryGetValue(column, out var asc) || asc;
            var icon = ascending ? " ▲" : " ▼";
            return _sortColumn == column ? $"<span style='font-weight:bold'>{icon}</span>" : $"<span class='text-muted'>{icon}</span>";
        }

        private void ApplySort()
        {
            if (_filteredScreenings == null)
            {
                return;
            }
            var ascending = !_sortAscendingByColumn.TryGetValue(_sortColumn, out var asc) || asc;
            _filteredScreenings = _sortColumn switch
            {
                "VeteranName" => ascending ? _filteredScreenings.OrderBy(s => s.VeteranName).ToList() : _filteredScreenings.OrderByDescending(s => s.VeteranName).ToList(),
                "SoundOffNumber" => ascending ? _filteredScreenings.OrderBy(s => s.SoundOffNumber).ToList() : _filteredScreenings.OrderByDescending(s => s.SoundOffNumber).ToList(),
                "HasMedicalAlerts" => ascending ? _filteredScreenings.OrderBy(s => s.HasMedicalAlerts).ToList() : _filteredScreenings.OrderByDescending(s => s.HasMedicalAlerts).ToList(),
                "HasMobilityAlerts" => ascending ? _filteredScreenings.OrderBy(s => s.HasMobilityAlerts).ToList() : _filteredScreenings.OrderByDescending(s => s.HasMobilityAlerts).ToList(),
                "HasSpecialAlerts" => ascending ? _filteredScreenings.OrderBy(s => s.HasSpecialAlerts).ToList() : _filteredScreenings.OrderByDescending(s => s.HasSpecialAlerts).ToList(),
                "AssistiveDeviceType" => ascending ? _filteredScreenings.OrderBy(s => s.AssistiveDeviceType).ToList() : _filteredScreenings.OrderByDescending(s => s.AssistiveDeviceType).ToList(),
                "LiftRequired" => ascending ? _filteredScreenings.OrderBy(s => s.LiftRequired).ToList() : _filteredScreenings.OrderByDescending(s => s.LiftRequired).ToList(),
                "UseOxygen" => ascending ? _filteredScreenings.OrderBy(s => s.UseOxygen).ToList() : _filteredScreenings.OrderByDescending(s => s.UseOxygen).ToList(),
                "HowMuchOxygen" => ascending ? _filteredScreenings.OrderBy(s => s.HowMuchOxygen).ToList() : _filteredScreenings.OrderByDescending(s => s.HowMuchOxygen).ToList(),
                "HasPcpSignature" => ascending
                    ? _filteredScreenings.OrderBy(s => s.HasPcpSignature ?? false).ToList()
                    : _filteredScreenings.OrderByDescending(s => s.HasPcpSignature ?? false).ToList(),
                _ => _filteredScreenings
            };
        }

        private void CalculateSummary()
        {
            if (_filteredScreenings == null)
            {
                _summaryUseOxygen = _summaryLiftRequiredYes = _summaryMedicalAlerts = _summaryMobilityAlerts = _summarySpecialAlerts = 0;
                return;
            }
            _summaryUseOxygen = _filteredScreenings.Count(s => s.UseOxygen == true);
            _summaryLiftRequiredYes = _filteredScreenings.Count(s => string.Equals(s.LiftRequired, "Yes", StringComparison.OrdinalIgnoreCase));
            _summaryMedicalAlerts = _filteredScreenings.Count(s => s.HasMedicalAlerts == true);
            _summaryMobilityAlerts = _filteredScreenings.Count(s => s.HasMobilityAlerts == true);
            _summarySpecialAlerts = _filteredScreenings.Count(s => s.HasSpecialAlerts == true);
            _summaryPCPSignature = _filteredScreenings.Count(s => s.HasPcpSignature == false);
        }
    }
}
