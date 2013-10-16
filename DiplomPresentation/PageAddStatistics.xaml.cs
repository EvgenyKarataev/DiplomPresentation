using System.Windows.Controls;
using System.Windows.Media;
using IndesysAPI.UnitDefs;
using IndesysMS.DB.Pages;

namespace DiplomPresentation
{
    /// <summary>
    /// Interaction logic for PageAddStatistics.xaml
    /// </summary>
    public partial class PageAddStatistics : Page
    {
        public PageAddStatistics()
        {
            InitializeComponent();

            var statisticsPage = new AddMeasurementPageXML();

            var measSys = new MeasurementSystem();
            measSys.SetPreambula(SiPreambuledUnitType.Hz, SiPreambula.Giga);

            var data = new AddMeasurementPageXMLData() {MeasurementSystem = measSys};

            statisticsPage.SetData(data);

            AddStatisticsHost.Child = statisticsPage;

            // Cause the OnFlowDirectionChange delegate to be called.
            AddStatisticsHost.FlowDirection = System.Windows.FlowDirection.LeftToRight;

            // Cause the OnClipChange delegate to be called.
            AddStatisticsHost.Clip = new RectangleGeometry();
        }
    }
}
