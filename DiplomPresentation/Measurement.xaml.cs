using System.Windows.Controls;
using System.Windows.Media;
using IndesysAPI.UnitDefs;
using IndesysMS.Forms;

namespace DiplomPresentation
{
    /// <summary>
    /// Interaction logic for Measurement.xaml
    /// </summary>
    public partial class Measurement : Page
    {
        public Measurement()
        {
            InitializeComponent();

            System.Windows.Forms.Application.EnableVisualStyles();

            var measuremForm = new MeasurementToDBFormXML();
            var measSys = new MeasurementSystem();

            measSys.SetPreambula(SiPreambuledUnitType.Hz, SiPreambula.Giga);

            measuremForm.MeasurementSystem = measSys;

            measureHost.Child = measuremForm;

            // Cause the OnFlowDirectionChange delegate to be called.
            measureHost.FlowDirection = System.Windows.FlowDirection.LeftToRight;

            // Cause the OnClipChange delegate to be called.
            measureHost.Clip = new RectangleGeometry();
        }
    }
}
