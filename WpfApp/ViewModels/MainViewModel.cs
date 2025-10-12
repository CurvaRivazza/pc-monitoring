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

        public ObservableCollection<PageViewModel> _pages = new ObservableCollection<PageViewModel>();  // коллекция всех доступных страниц

        private PageViewModel _currentPage;  // текущая выбранная страница

        public ObservableCollection<PageViewModel> Pages
        {
            get => _pages;
            set => SetProperty(ref _pages, value);
        }

        public PageViewModel CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        public MainViewModel(CpuMonitorViewModel cpuMonitorViewModel)
        {
            _cpuMonitorViewModel = cpuMonitorViewModel;
            _pages.Add(_cpuMonitorViewModel);
            _currentPage = _pages[0];
        }

        [RelayCommand]
        private void NavigateToPage(PageViewModel page)
        {
            CurrentPage = page;
        }
    }
}
