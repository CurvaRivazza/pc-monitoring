using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.ViewModels
{
    // базовый класс с общей логикой для всех ViewModel страниц
    public abstract class PageViewModel : ObservableObject
    {
        public abstract string Title { get; }  // заголовок страницы для отображения в навигации
    }
}
