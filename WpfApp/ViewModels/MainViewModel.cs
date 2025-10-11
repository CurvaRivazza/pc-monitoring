using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.ViewModels
{
    // управляет навигацией между страницами
    public partial class MainViewModel : ObservableObject
    {
        private readonly CpuMonitorViewModel _cpuMonitorViewModel;

        [ObservableProperty]
        private ObservableCollection<PageViewModel> _pages = new ObservableCollection<PageViewModel>();  // коллекция всех доступных страниц

        private PageViewModel _currentPage;  // текущая выбранная страница

        public MainViewModel(CpuMonitorViewModel cpuMonitorViewModel)
        {
            _cpuMonitorViewModel = cpuMonitorViewModel;
            _pages.Add(_cpuMonitorViewModel);
            _currentPage = _pages[0];
        }

        [RelayCommand]
        private void NavigateToPage(PageViewModel page)
        {
            _currentPage = page;
        }
    }
}
