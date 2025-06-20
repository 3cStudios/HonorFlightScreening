using HonorFlightScreening.Data;
using HonorFlightScreening.Services;
using Microsoft.AspNetCore.Components;


namespace HonorFlightScreening.Components.Pages
{
    public partial class VeteranScreenings: ComponentBase
    {
        [Inject] private VeteranScreeningService ScreeningService { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;


        private List<VeteranScreening>? _screenings;
        private List<VeteranScreening>? _filteredScreenings;

        private string? _searchVeteranName;
        private string? _searchSoundOffNumber;


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
        protected override async Task OnInitializedAsync()
        {
            _screenings = await ScreeningService.GetAllScreeningsAsync();
            _filteredScreenings = _screenings.ToList();

            CalculateSummary();
        }

        protected override void OnParametersSet()
        {
            CalculateSummary();
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
            _searchSoundOffNumber = string.Empty;
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
                string.IsNullOrEmpty(_searchSoundOffNumber))
            {
                _filteredScreenings = _screenings.ToList();
                return;
            }
            IEnumerable<VeteranScreening> query = _screenings;
            if (!string.IsNullOrWhiteSpace(_searchVeteranName))
                query = query.Where(s => s.VeteranName.Contains(_searchVeteranName, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(_searchSoundOffNumber))
                query = query.Where(s => s.SoundOffNumber.Contains(_searchSoundOffNumber, StringComparison.OrdinalIgnoreCase));
            _filteredScreenings = query.ToList();
        }

        private void NavigateToScreening(int id)
        {
            Navigation.NavigateTo($"/screening/{id}");
        }

        private void CreateNewScreening()
        {
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
