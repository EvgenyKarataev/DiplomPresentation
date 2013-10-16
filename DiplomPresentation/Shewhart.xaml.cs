using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using App.GUI.Graphs.GraphicsDotNet.Controls;
using App.GUI.Graphs.GraphicsDotNet.Data;
using App.GUI.Graphs.GraphicsDotNet.Draw;
using App.GUI.Graphs.GraphicsDotNet.Plots;
using IndesysAPI.GraphOutput;
using IndesysAPI.UnitDefs;
using IndesysMS.DB.GraphicRepresentation;
using IndesysMS.DB.GraphicRepresentation.TrendTracking;
using IndesysMS.DB.Measurements;
using IndesysMS.DB.Measurements.Categories.ControlCard;

namespace DiplomPresentation
{
    /// <summary>
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Shewhart : Page
    {
        private GraphPlotControl _graphPlotControl;

        public Shewhart()
        {
            InitializeComponent();

            System.Windows.Forms.Application.EnableVisualStyles();

            // Create a Windows Forms checkbox control and assign 
            // it as the WindowsFormsHost element's child.

            var measSys = new MeasurementSystem();
            measSys.SetPreambula(SiPreambuledUnitType.Hz, SiPreambula.Giga);

            _graphPlotControl = new GraphPlotControl(CoordinateSystemType.Users);

            var measurement = new ShewharMeasurement
            {
                Foundry = 0,
                Process = 0,
                TestName = "CMIM",
                TrendManager = new TrendManager(TrendsFactory.GetAllTrends())
            };

            var shewhartChart = new ShewhartControlChart(measurement.TrendManager);

            shewhartChart.Abscisses[0].Title = "Number";
            shewhartChart.Ordinates[0].Title = "Value";

            _graphPlotControl.Plot.CoordinateSystem = shewhartChart;

            _graphPlotControl.Plot.Legend.Visible = false;
            _graphPlotControl.BorderStyle = BorderStyle.None;
            _graphPlotControl.MeasurementSystem = measSys;
            _graphPlotControl.Plot.CoordinateSystem.SweepValueQuantityType = PhysicalQuantityType.Frequencies;
            _graphPlotControl.Plot.Data = MakeData(measurement);

          //  _graphPlotControl.Tag = megaMeasurement;

            graphicsHost.Child = _graphPlotControl;

            // Cause the OnFlowDirectionChange delegate to be called.
            graphicsHost.FlowDirection = System.Windows.FlowDirection.LeftToRight;

            // Cause the OnClipChange delegate to be called.
            graphicsHost.Clip = new RectangleGeometry();
        }

        private Traces MakeData(ShewharMeasurement measurement)
        {
            var symbol = new MarkerSymbol
                             {
                                 Type = MarkerSymbolType.Circle,
                                 Visible = true
                             };

            var function = new TraceReal
                               {
                                   Pen = new System.Drawing.Pen(System.Drawing.Color.Green) { Width = 1 },
                                   Name = "Shewhart",
                                   IsGistogram = false,
                                   IsVisibleInMarker = true,
                                   Visible = true,
                                   GraphIndex = 0,
                                   Approximator = null,
                                   ArgumentQuantityType = PhysicalQuantityType.Scalar,
                                   SymbolInterval = 1,
                                   SymbolsAutoInterval = false,
                                   Symbol = symbol
                               };

            var noneSymbol = new MarkerSymbol
                                 {
                                     Type = MarkerSymbolType.None,
                                     Visible = false
                                 };

            var meanFunction = new TraceReal
                                   {
                                       Pen = new System.Drawing.Pen(System.Drawing.Color.Blue),
                                       Name = "Mean",
                                       IsGistogram = false,
                                       IsVisibleInMarker = true,
                                       Visible = true,
                                       GraphIndex = 0,
                                       Approximator = null,
                                       ArgumentQuantityType = PhysicalQuantityType.Scalar,
                                       Symbol = noneSymbol
                                   };

            var uclSymbol = new MarkerSymbol
                                {
                                    Type = MarkerSymbolType.TriangleUp,
                                    Visible = true
                                };

            var uclFunction = new TraceReal
                                  {
                                      Pen = new System.Drawing.Pen(System.Drawing.Color.Red),
                                      Name = "UCL",
                                      IsGistogram = false,
                                      IsVisibleInMarker = true,
                                      Visible = true,
                                      GraphIndex = 0,
                                      Approximator = null,
                                      ArgumentQuantityType = PhysicalQuantityType.Scalar,
                                      Symbol = noneSymbol
                                  };

            var cUpFunction = new TraceReal
                                  {
                                      Pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(255, 192, 192)) { DashPattern = new[] { 4.0F, 8.0F } },
                                      Name = "C above mean",
                                      Visible = true,
                                      GraphIndex = 0,
                                      Approximator = null,
                                      ArgumentQuantityType = PhysicalQuantityType.Scalar,
                                      Symbol = noneSymbol
                                  };

            var bUpFunction = new TraceReal
                                  {
                                      Pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(255, 128, 128)) { DashPattern = new[] { 4.0F, 8.0F } },
                                      Name = "B above mean",
                                      Visible = true,
                                      GraphIndex = 0,
                                      Approximator = null,
                                      ArgumentQuantityType = PhysicalQuantityType.Scalar,
                                      Symbol = noneSymbol
                                  };

            var lclSymbol = new MarkerSymbol
                                {
                                    Type = MarkerSymbolType.TriangleDown,
                                    Visible = true
                                };

            var lclFunction = new TraceReal
                                  {
                                      Pen = new System.Drawing.Pen(System.Drawing.Color.Red),
                                      Name = "LCL",
                                      IsGistogram = false,
                                      IsVisibleInMarker = true,
                                      Visible = true,
                                      GraphIndex = 0,
                                      Approximator = null,
                                      ArgumentQuantityType = PhysicalQuantityType.Scalar,
                                      Symbol = noneSymbol
                                  };

            var cDownFunction = new TraceReal
                                    {
                                        Pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(255, 192, 192)) { DashPattern = new[] { 4.0F, 8.0F } },
                                        Name = "C below mean",
                                        Visible = true,
                                        GraphIndex = 0,
                                        Approximator = null,
                                        ArgumentQuantityType = PhysicalQuantityType.Scalar,
                                        Symbol = noneSymbol
                                    };

            var bDownFunction = new TraceReal
                                    {
                                        Pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(255, 128, 128)) { DashPattern = new[] { 4.0F, 8.0F } },
                                        Name = "B below mean",
                                        Visible = true,
                                        GraphIndex = 0,
                                        Approximator = null,
                                        ArgumentQuantityType = PhysicalQuantityType.Scalar,
                                        Symbol = noneSymbol
                                    };

            var minSpecFunction = new TraceReal
                                      {
                                          Pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(64, 64, 64)),
                                          Name = "Min spec",
                                          Visible = true,
                                          GraphIndex = 0,
                                          Approximator = null,
                                          ArgumentQuantityType = PhysicalQuantityType.Scalar,
                                          Symbol = noneSymbol
                                      };

            var maxSpecFunction = new TraceReal
                                      {
                                          Pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(64, 64, 64)),
                                          Name = "Max spec",
                                          Visible = true,
                                          GraphIndex = 0,
                                          Approximator = null,
                                          ArgumentQuantityType = PhysicalQuantityType.Scalar,
                                          Symbol = noneSymbol
                                      };

            var nominalSpecFunction = new TraceReal
                                          {
                                              Pen = new System.Drawing.Pen(System.Drawing.Color.Black),
                                              Name = "Nominal spec",
                                              Visible = true,
                                              GraphIndex = 0,
                                              Approximator = null,
                                              ArgumentQuantityType = PhysicalQuantityType.Scalar,
                                              Symbol = noneSymbol
                                          };

            var functionDataSector = new GraphDataSectorReal();
            var meanDataSector = new GraphDataSectorReal();
            var uclDataSector = new GraphDataSectorReal();
            var cUpDataSector = new GraphDataSectorReal();
            var bUpDataSector = new GraphDataSectorReal();
            var lclDataSector = new GraphDataSectorReal();
            var cDownDataSector = new GraphDataSectorReal();
            var bDownDataSector = new GraphDataSectorReal();
            var minSpecDataSector = new GraphDataSectorReal();
            var maxSpecDataSector = new GraphDataSectorReal();
            var nominalSpecDataSector = new GraphDataSectorReal();

            var values = new List<double> {378, 376, 375, 370, 371, 388, 389, 390, 388, 400, 391, 392, 386, 382, 378, 374, 370, 380, 381, 387};
            var meanvalue = 382.3;
            var sigma = 8.25;
            var uclValue = meanvalue + 3*sigma;
            var bUpValue = meanvalue + 2*sigma;
            var cUpValue = meanvalue + sigma;
            var lclValue = meanvalue - 3*sigma;
            var bDownValue = meanvalue - 2*sigma;
            var cDownValue = meanvalue - sigma;

            double? usersMinSpec = 340;
            double? usersMaxSpec = 420;
            double? usersNominalSpec = 380;

            for (int i = 0; i < values.Count; i++)
            {
                functionDataSector.Add(new PointValueReal(i, values[i]));
                meanDataSector.Add(new PointValueReal(i, meanvalue));
                uclDataSector.Add(new PointValueReal(i, uclValue));
                bUpDataSector.Add(new PointValueReal(i, bUpValue));
                cUpDataSector.Add(new PointValueReal(i, cUpValue));
                lclDataSector.Add(new PointValueReal(i, lclValue));
                bDownDataSector.Add(new PointValueReal(i, bDownValue));
                cDownDataSector.Add(new PointValueReal(i, cDownValue));

                if (usersMinSpec.HasValue)
                    minSpecDataSector.Add(new PointValueReal(i, usersMinSpec.Value));

                if (usersMaxSpec.HasValue)
                    maxSpecDataSector.Add(new PointValueReal(i, usersMaxSpec.Value));

                if (usersNominalSpec.HasValue)
                    nominalSpecDataSector.Add(new PointValueReal(i, usersNominalSpec.Value));
            }

            function.Add(functionDataSector);
            meanFunction.Add(meanDataSector);
            uclFunction.Add(uclDataSector);
            bUpFunction.Add(bUpDataSector);
            cUpFunction.Add(cUpDataSector);
            lclFunction.Add(lclDataSector);
            bDownFunction.Add(bDownDataSector);
            cDownFunction.Add(cDownDataSector);
            minSpecFunction.Add(minSpecDataSector);
            maxSpecFunction.Add(maxSpecDataSector);
            nominalSpecFunction.Add(nominalSpecDataSector);

            var result = new Traces();

            result.RealFunctions.Add(uclFunction);
            result.RealFunctions.Add(bUpFunction);
            result.RealFunctions.Add(cUpFunction);
            result.RealFunctions.Add(meanFunction);
            result.RealFunctions.Add(cDownFunction);
            result.RealFunctions.Add(bDownFunction);
            result.RealFunctions.Add(lclFunction);
            result.RealFunctions.Add(maxSpecFunction);
            result.RealFunctions.Add(nominalSpecFunction);
            result.RealFunctions.Add(minSpecFunction);
            result.RealFunctions.Add(function);

            var points = new List<PointValueReal>();
            foreach (var sector in function)
            {
                foreach (var pointValueBase in sector)
                {
                    points.Add((PointValueReal) pointValueBase);
                }
            }

            measurement.TrendManager.SetData(points.ToArray(), sigma, meanvalue);

           
            var Cp = (usersMaxSpec.Value - usersMinSpec.Value)/(6*sigma);

            var Cpk = Math.Min((usersMaxSpec.Value - meanvalue)/(3*sigma),
                                   (meanvalue - usersMinSpec.Value)/(3*sigma));

            _graphPlotControl.GraphTitle += "\nCp = " + Cp;
            _graphPlotControl.GraphTitle += "\nCpk = " + Cpk;

            return result;
        }

        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
