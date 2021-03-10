using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleTrader.WPF.Views
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class SellView : UserControl
    {


        public ICommand SelectedAssertCnangedCommand
        {
            get { return (ICommand)GetValue(SelectedAssertCnangedCommandProperty); }
            set { SetValue(SelectedAssertCnangedCommandProperty, value); }
        }

        public static readonly DependencyProperty SelectedAssertCnangedCommandProperty =
            DependencyProperty.Register("SelectedAssertCnangedCommand", typeof(ICommand), typeof(SellView), new PropertyMetadata(null));


        public SellView()
        {
            InitializeComponent();
        }

        private void cbAsserts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbAsserts.SelectedItem != null)
                SelectedAssertCnangedCommand?.Execute(null);
        }
    }
}
