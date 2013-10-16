using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace DiplomPresentation
{
    /// <summary>
    /// Interaction logic for ApplicationToolbar.xaml
    /// </summary>
    public partial class ApplicationToolbar 
    {
        static ApplicationToolbar()
        {
            FrameworkPropertyMetadata md = new FrameworkPropertyMetadata();
            ApplicationToolbar.CurrentIndexProperty = DependencyProperty.Register("CurrIndex", typeof (int),
                                                                                  typeof (ApplicationToolbar), md);
        }

        public ApplicationToolbar()
        {
            InitializeComponent();
        }

        private void NavigationCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
           
        }

        private List<string> pagesList = new List<string> { "Title.xaml", "Introduction.xaml", "Objectives.xaml", "Database.xaml", "Measurement.xaml", "PageAddStatistics.xaml", "Histogram.xaml", "Wafer.xaml", "Shewhart.xaml", "Ekonom.xaml", };

        private int currIndex = 0;

        public static readonly DependencyProperty CurrentIndexProperty;

        public int CurrIndex
        {
            get { return currIndex; }
            set { currIndex = value; }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (currIndex - 1 < 0)
                return;

            currIndex--;

            UpdatePage();
        }

        private void UpdatePage()
        {
            NavigationService service = NavigationService.GetNavigationService(this);
            service.Navigate(new Uri(pagesList[currIndex], UriKind.Relative));
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            if (currIndex + 1 >= pagesList.Count)
                return;

            currIndex++;

            UpdatePage();
        }

        private void Page1_Click(object sender, RoutedEventArgs e)
        {
            currIndex = 0;
            UpdatePage();
        }

        private void Page2_Click(object sender, RoutedEventArgs e)
        {
            currIndex = 1;
            UpdatePage();
        }

        private void Page3_Click(object sender, RoutedEventArgs e)
        {
            currIndex = 2;
            UpdatePage();
        }

        private void Page4_Click(object sender, RoutedEventArgs e)
        {
            currIndex = 3;
            UpdatePage();
        }
    }
}