using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Navigation;
using App.GUI.Graphs.GraphicsDotNet.Controls;
using App.GUI.Graphs.GraphicsDotNet.Data;
using App.GUI.Graphs.GraphicsDotNet.Draw;
using App.GUI.Graphs.GraphicsDotNet.Plots;
using IndesysAPI.ComplexArithmetics;
using IndesysAPI.GraphOutput;
using IndesysAPI.UnitDefs;
using IndesysMS.DB.Calcualtions;
using IndesysMS.DB.GraphicRepresentation;
using IndesysMS.DB.Measurements;
using IndesysMS.DB.Measurements.Categories.Characterization;
using IndesysMS.DB.Measurements.Categories.PortMeasurements;
using IndesysMS.DB.Pages;
using IndesysMS.DB.TreeNodes;

namespace DiplomPresentation
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Histogram : Page
    {
        private IntegralGraphControl _integralPlotControl;
        private GraphicFilterPage _filterPage;

        public Histogram()
        {
            InitializeComponent();

            System.Windows.Forms.Application.EnableVisualStyles();

            // Create a Windows Forms checkbox control and assign 
            // it as the WindowsFormsHost element's child.

            var measSys = new MeasurementSystem();
            measSys.SetPreambula(SiPreambuledUnitType.Hz, SiPreambula.Giga);

            _integralPlotControl = new IntegralGraphControl();
            _integralPlotControl.Data = MakeData();

            var graphPlotControl = (GraphPlotControl)_integralPlotControl.Graphs[0];
            graphPlotControl.BorderStyle = BorderStyle.None;
            graphPlotControl.MeasurementSystem = measSys;
            graphPlotControl.Plot.CoordinateSystem.SweepValueQuantityType = PhysicalQuantityType.Frequencies;
            graphPlotControl.GraphTitle = "Histogram";

            var ftMeas = new FtMeasurement
            {
                Foundry = 0,
                Process = 0,
                Wafer = "C2",
                ElementType = "Transistor",
                ElementSubTypes = new List<string> { "T240" },
            };

            var megaMeas = new MegaMeasurement();

            megaMeas.Add(ftMeas);

            graphPlotControl.Tag = megaMeas;

            graphicsHost.Child = _integralPlotControl;

            // Cause the OnFlowDirectionChange delegate to be called.
            graphicsHost.FlowDirection = System.Windows.FlowDirection.LeftToRight;

            // Cause the OnClipChange delegate to be called.
            graphicsHost.Clip = new RectangleGeometry();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private Traces MakeData()
        {
            var function = new TraceReal
                               {
                                   Pen = new System.Drawing.Pen(System.Drawing.Color.SkyBlue),
                                   Name = "Ft, C2, Transistor, T240",
                                   IsGistogram = true,
                                   IsVisibleInMarker = true,
                                   Visible = true,
                                   GraphIndex = 0,
                                   CoordinateSystemType = CoordinateSystemType.Rectangle,
                                   ArgumentQuantityType = PhysicalQuantityType.Frequencies
                               };

            var symbol = new MarkerSymbol
            {
                Type = MarkerSymbolType.Circle,
                Visible = false
            };

            function.Symbol = symbol;
            function.SymbolInterval = 1;
            function.SymbolsAutoInterval = false;

            var values = new[] { 38603499999.999992, 32817999999.999996, 36808000000.0, 40000000000.0, 31820499999.999996, 40000000000.0, 32817999999.999996, 40000000000.0, 40000000000.0, 40000000000.0, 37606000000.0, 40000000000.0, 40000000000.0, 40000000000.0, 40000000000.0, 40000000000.0, 32418999999.999996, 40000000000.0, 40000000000.0, 27431500000.0 };

            var gistogram = Calculations.Gistogram(values);

            if (gistogram.Keys.Count == 1)
            {
                var dataSector = new GraphDataSectorReal
                                     {
                                         new PointValueReal(gistogram.Keys.ElementAt(0),
                                                            gistogram[gistogram.Keys.ElementAt(0)])
                                     };

                function.Add(dataSector);
            }
            else
            {
                for (int i = 0; i < gistogram.Keys.Count - 1; i++)
                {
                    var dataSector = new GraphDataSectorReal();

                    dataSector.Add(new PointValueReal(gistogram.Keys.ElementAt(i),
                                                      gistogram[gistogram.Keys.ElementAt(i + 1)]));

                    dataSector.Add(new PointValueReal(gistogram.Keys.ElementAt(i + 1),
                                                      gistogram[gistogram.Keys.ElementAt(i + 1)]));

                    function.Add(dataSector);
                }
            }

            var result = new Traces();

            result.RealFunctions.Add(function);

            var meanFunction = new TraceReal
            {
                Pen = new System.Drawing.Pen(System.Drawing.Color.Green),
                Name = "Mean Ft, C2, Transistor, T240",
                IsGistogram = true,
                IsVisibleInMarker = true,
                Visible = true,
                GraphIndex = 0,
                CoordinateSystemType = CoordinateSystemType.Rectangle,
                ArgumentQuantityType = PhysicalQuantityType.Frequencies
            };

            var meanSymbol = new MarkerSymbol
            {
                Type = MarkerSymbolType.Circle,
                Visible = true
            };

            meanFunction.Symbol = meanSymbol;
            meanFunction.SymbolInterval = 1;
            meanFunction.SymbolsAutoInterval = false;

            var meanValue = Calculations.MeanValue(values);
            var maxDiapason = Calculations.MaxDiapasonValue(values);

            var meanDataSector = new GraphDataSectorReal
                                 {
                                     new PointValueReal(meanValue, maxDiapason)
                                 };

            meanFunction.Add(meanDataSector);

            result.RealFunctions.Add(meanFunction);

            var sigmaFunction = new TraceReal
            {
                Pen = new System.Drawing.Pen(System.Drawing.Color.Blue),
                Name = "3 sigma Ft, C2, Transistor, T240",
                IsGistogram = true,
                IsVisibleInMarker = true,
                Visible = true,
                GraphIndex = 0,
                CoordinateSystemType = CoordinateSystemType.Rectangle,
                ArgumentQuantityType = PhysicalQuantityType.Frequencies
            };

            sigmaFunction.Symbol = meanSymbol;
            sigmaFunction.SymbolInterval = 1;
            sigmaFunction.SymbolsAutoInterval = false;

            var sigma = Calculations.Sigma(values);

            var sigmaDataSector = new GraphDataSectorReal
                                 {
                                     new PointValueReal(meanValue - 3 * sigma, maxDiapason)
                                 };

            sigmaFunction.Add(sigmaDataSector);

            sigmaDataSector = new GraphDataSectorReal
                                 {
                                     new PointValueReal(meanValue + 3 * sigma, maxDiapason)
                                 };

            sigmaFunction.Add(sigmaDataSector);

            result.RealFunctions.Add(sigmaFunction);

            var gausFunction = new TraceReal
            {
                Pen = new System.Drawing.Pen(System.Drawing.Color.Red),
                Name = "Gaus Ft, C2, Transistor, T240",
                IsGistogram = false,
                IsVisibleInMarker = true,
                Visible = true,
                GraphIndex = 0,
                CoordinateSystemType = CoordinateSystemType.Rectangle,
                ArgumentQuantityType = PhysicalQuantityType.Frequencies
            };

            gausFunction.Symbol = symbol;
            gausFunction.SymbolInterval = 1;
            gausFunction.SymbolsAutoInterval = false;

            var gausDataSector = new GraphDataSectorReal();
            var gaus = Calculations.Gaussian(values);
            foreach (var valueReal in gaus)
            {
                gausDataSector.Add(valueReal);
            }

            gausFunction.Add(gausDataSector);

            result.RealFunctions.Add(gausFunction);

            return result;
        }

       
    }
}
