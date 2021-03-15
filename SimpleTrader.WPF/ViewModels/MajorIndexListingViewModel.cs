using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class MajorIndexListingViewModel : ViewModelBase
    {
        public MajorIndexListingViewModel(IMajorIndexService majorIndexService)
        {
            LoadMajorIndexCommand = new LoadMajorIndexCommand(this, majorIndexService);
        }

        public ICommand LoadMajorIndexCommand { get; }

        private MajorIndex _dowJones;

        public MajorIndex DowJones
        {
            get { return _dowJones; }
            set 
            { 
                _dowJones = value;
                OnPropertyChanged(nameof(DowJones));
            }
        }

        private MajorIndex _sP500;

        public MajorIndex SP500
        {
            get { return _sP500; }
            set 
            { 
                _sP500 = value;
                OnPropertyChanged(nameof(SP500));
            }
        }

        private MajorIndex _nasdaq;

        public MajorIndex Nasdaq
        {
            get { return _nasdaq; }
            set 
            { 
                _nasdaq = value;
                OnPropertyChanged(nameof(Nasdaq));
            }
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get 
            { 
                return _isLoading;
            }
            set 
            { 
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }


        public static MajorIndexListingViewModel LoadMajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            MajorIndexListingViewModel majorIndexListingViewModel = new MajorIndexListingViewModel(majorIndexService);
            majorIndexListingViewModel.LoadMajorIndexCommand.Execute(null);
            return majorIndexListingViewModel;
        }

    }
}
