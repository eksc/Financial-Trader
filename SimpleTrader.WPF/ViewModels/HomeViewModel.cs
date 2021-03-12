using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public AssertSummaryViewModel AssertSummaryViewModel { get; }
        public MajorIndexListingViewModel MajorIndexListingViewModel { get; }

        public HomeViewModel(MajorIndexListingViewModel majorIndexListingViewModel, AssertSummaryViewModel assertSummaryViewModel)
        {
            MajorIndexListingViewModel = majorIndexListingViewModel;
            AssertSummaryViewModel = assertSummaryViewModel;
        }

        public override void Dispose()
        {
            AssertSummaryViewModel.Dispose();
            MajorIndexListingViewModel.Dispose();

            base.Dispose();
        }
    }
}
