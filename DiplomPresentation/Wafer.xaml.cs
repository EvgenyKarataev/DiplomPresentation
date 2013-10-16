using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Navigation;
using App.GUI.Graphs.GraphicsDotNet.Controls;
using App.GUI.Graphs.GraphicsDotNet.Data;
using App.GUI.Graphs.GraphicsDotNet.Plots;
using IndesysAPI.ComplexArithmetics;
using IndesysAPI.UnitDefs;
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
    public partial class Page2 : Page
    {
        private IntegralGraphControl _integralPlotControl;
        private GraphicFilterPage _filterPage;

        public Page2()
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
            graphPlotControl.Plot.CoordinateSystem = new WaferDiagram();
            graphPlotControl.Plot.Legend.Visible = false;
            graphPlotControl.BorderStyle = BorderStyle.None;
            graphPlotControl.MeasurementSystem = measSys;
            graphPlotControl.Plot.CoordinateSystem.SweepValueQuantityType = PhysicalQuantityType.Frequencies;
            graphPlotControl.GraphTitle = "Wafer";

            var sMeas = new SMeasurement
                            {
                                Foundry = 0,
                                Process = 0,
                                Wafer = "C2",
                                ElementType = "Transistor",
                                ElementSubTypes = new List<string> {"T240"},
                                Frequency = 10000000000,
                                TransformMode = ComplexToComplexTransformMode.Magnitude,
                                IsDB = true
                            };

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
            megaMeas.Add(sMeas);

            graphPlotControl.Tag = megaMeas;

            graphicsHost.Child = _integralPlotControl;

            // Cause the OnFlowDirectionChange delegate to be called.
            graphicsHost.FlowDirection = System.Windows.FlowDirection.LeftToRight;

            // Cause the OnClipChange delegate to be called.
            graphicsHost.Clip = new RectangleGeometry();

            _filterPage = new GraphicFilterPage();
            var wafer = graphPlotControl.Plot.CoordinateSystem as WaferDiagram;
            var data = new GraphicFilterPage.Data { GraphPlot = graphPlotControl, FilterCriteria = wafer.FilterCriteria };
            _filterPage.MeasurementSystem = graphPlotControl.MeasurementSystem;

            _filterPage.SetData(data);       // Передаем данные в диалог настроек.

            filterPageHost.Child = _filterPage;

            UpdateRadioButtons();
        }

        private void UpdateRadioButtons()
        {
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private Traces MakeData()
        {
            var ft_row1 = new GraphDataSectorComplex
                           {
                               new PointValueComplex(38603000000, new Complex(1, 1)) 
                                    {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                               new PointValueComplex(32818000000, new Complex(1, 2)) 
                                    {SourceName = "T240_A01_3V_16mA_-0V5.s2p"}
                           };

            var s21_row1 = new GraphDataSectorComplex
                               {
                                   new PointValueComplex(9.3146, new Complex(1, 1))
                                       {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                   new PointValueComplex(8.002, new Complex(1, 2))
                                       {SourceName = "T240_A01_3V_16mA_-0V5.s2p"}
                               };

            var ft_row2 = new GraphDataSectorComplex
                              {
                                  new PointValueComplex(38808000000, new Complex(2, 1))
                                      {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                  new PointValueComplex(40000000000, new Complex(2, 2))
                                      {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                  new PointValueComplex(31820000000, new Complex(2, 3))
                                      {SourceName = "T240_A01_3V_16mA_-0V5.s2p"}
                              };

            var s21_row2 = new GraphDataSectorComplex
                               {
                                   new PointValueComplex(9.0279, new Complex(2, 1))
                                       {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                   new PointValueComplex(10.318, new Complex(2, 2))
                                       {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                   new PointValueComplex(5.3264, new Complex(2, 3))
                                       {SourceName = "T240_A01_3V_16mA_-0V5.s2p"}
                               };

            var ft_row3 = new GraphDataSectorComplex
                              {
                                  new PointValueComplex(40000000000, new Complex(3, 1))
                                      {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                  new PointValueComplex(32818000000, new Complex(3, 2))
                                      {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                              };

            var s21_row3 = new GraphDataSectorComplex
                               {
                                   new PointValueComplex(10.38, new Complex(3, 1))
                                       {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                   new PointValueComplex(8.2851, new Complex(3, 2))
                                       {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                               };

            var ft_row4 = new GraphDataSectorComplex
                              {
                                  new PointValueComplex(40000000000, new Complex(4, 1))
                                      {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                  new PointValueComplex(40000000000, new Complex(4, 2))
                                      {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                  new PointValueComplex(40000000000, new Complex(4, 3))
                                      {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                  new PointValueComplex(37606000000, new Complex(4, 4))
                                      {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                              };

            var s21_row4 = new GraphDataSectorComplex
                               {
                                   new PointValueComplex(9.9512, new Complex(4, 1))
                                       {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                   new PointValueComplex(10.44, new Complex(4, 2))
                                       {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                   new PointValueComplex(10.258, new Complex(4, 3))
                                       {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                   new PointValueComplex(8.3711, new Complex(4, 4))
                                       {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                               };

            var ft_row5 = new GraphDataSectorComplex
                              {
                                  new PointValueComplex(40000000000, new Complex(5, 1))
                                      {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                  new PointValueComplex(40000000000, new Complex(5, 2))
                                      {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                  new PointValueComplex(40000000000, new Complex(5, 3))
                                      {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                  new PointValueComplex(40000000000, new Complex(5, 4))
                                      {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                      new PointValueComplex(40000000000, new Complex(5, 5))
                                      {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                      new PointValueComplex(32419000000, new Complex(5, 6))
                                      {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                              };

            var s21_row5 = new GraphDataSectorComplex
                               {
                                   new PointValueComplex(10.049, new Complex(5, 1))
                                       {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                   new PointValueComplex(9.4842, new Complex(5, 2))
                                       {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                   new PointValueComplex(7.1313, new Complex(5, 3))
                                       {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                   new PointValueComplex(9.9078, new Complex(5, 4))
                                       {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                        new PointValueComplex(10.931, new Complex(5, 5))
                                       {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                        new PointValueComplex(5.3135, new Complex(5, 6))
                                       {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                               };

            var ft_row6 = new GraphDataSectorComplex
                              {
                                  new PointValueComplex(40000000000, new Complex(6, 1))
                                      {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                  new PointValueComplex(40000000000, new Complex(6, 2))
                                      {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                  new PointValueComplex(double.NaN, new Complex(6, 3))
                                      {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                  new PointValueComplex(27432000000, new Complex(6, 4))
                                      {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                    
                              };

            var s21_row6 = new GraphDataSectorComplex
                               {
                                   new PointValueComplex(11.923, new Complex(6, 1))
                                       {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                   new PointValueComplex(9.4732, new Complex(6, 2))
                                       {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                   new PointValueComplex(double.NaN, new Complex(6, 3))
                                       {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                                   new PointValueComplex(5.4425, new Complex(6, 4))
                                       {SourceName = "T240_A01_3V_16mA_-0V5.s2p"},
                               };

            var ft = new TraceComplex
                         {
                             Visible = true,
                             IsVisibleInMarker = true,
                             Name = "Ft, C2, Transistor, T240",
                             Pen = new System.Drawing.Pen(System.Drawing.Color.Black),
                             GraphIndex = 0,
                             //CoordinateSystemType = megaMeasurement.StatisticType.CoordianatSystem,
                             ArgumentQuantityType = PhysicalQuantityType.Frequencies
                         };

            var s21 = new TraceComplex
                         {
                             Visible = false,
                             IsVisibleInMarker = true,
                             Name = "DB(|S[2,1]|) @10 GHz, C2, Transistor, T240",
                             Pen = new System.Drawing.Pen(System.Drawing.Color.Black),
                             GraphIndex = 0,
                             //CoordinateSystemType = megaMeasurement.StatisticType.CoordianatSystem,
                             ArgumentQuantityType = PhysicalQuantityType.Scalar
                         };

            ft.AddRange(new[] { ft_row1, ft_row2, ft_row3, ft_row4, ft_row5, ft_row6 });
            s21.AddRange(new[] {s21_row1, s21_row2, s21_row3, s21_row4, s21_row5, s21_row6});
            var result = new Traces();
            result.ComplexFunctions.Add(ft);
            result.ComplexFunctions.Add(s21);

            return result;
        }

        private void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            var graphPlotControl = (GraphPlotControl) _integralPlotControl.Graphs[0];
            var wafer = graphPlotControl.Plot.CoordinateSystem as WaferDiagram;
            object objData;
            _filterPage.GetData(out objData);
            wafer.FilterCriteria = ((GraphicFilterPage.Data)objData).FilterCriteria;

            wafer.TransparentList = StatisticsTreeNode.SetTransparency(wafer.FilterCriteria, graphPlotControl.Plot.Data, graphPlotControl.MeasurementSystem);

            graphPlotControl.UpdateData();
        }

        private void FtRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (_integralPlotControl == null) return;

            _integralPlotControl.Data.ComplexFunctions[0].Visible = true;
            _integralPlotControl.Data.ComplexFunctions[1].Visible = false;

            var graphPlotControl = (GraphPlotControl)_integralPlotControl.Graphs[0];
            graphPlotControl.Plot.CoordinateSystem.SweepValueQuantityType = PhysicalQuantityType.Frequencies;

            _integralPlotControl.UpdateData();
        }

        private void S21RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            _integralPlotControl.Data.ComplexFunctions[0].Visible = false;
            _integralPlotControl.Data.ComplexFunctions[1].Visible = true;

            var graphPlotControl = (GraphPlotControl)_integralPlotControl.Graphs[0];
            graphPlotControl.Plot.CoordinateSystem.SweepValueQuantityType = PhysicalQuantityType.Scalar;

            _integralPlotControl.UpdateData();
        }

        private void SplitTraces_Click(object sender, RoutedEventArgs e)
        {
            Measurements.Visibility = SplitTraces.Visibility = System.Windows.Visibility.Collapsed;
            JoinTraces.Visibility = System.Windows.Visibility.Visible;

            _integralPlotControl.Data.ComplexFunctions[1].GraphIndex = 1;
            _integralPlotControl.Data.ComplexFunctions[0].Visible = true;
            _integralPlotControl.Data.ComplexFunctions[1].Visible = true;
            
            _integralPlotControl.UpdateAllGraphs();

            var measSys = new MeasurementSystem();
            measSys.SetPreambula(SiPreambuledUnitType.Hz, SiPreambula.Giga);
            var graphPlotControl = (GraphPlotControl)_integralPlotControl.Graphs[0];
            graphPlotControl.Plot.CoordinateSystem = new WaferDiagram();
            graphPlotControl.Plot.Legend.Visible = false;
            graphPlotControl.BorderStyle = BorderStyle.None;
            graphPlotControl.MeasurementSystem = measSys;
            graphPlotControl.Plot.CoordinateSystem.SweepValueQuantityType = PhysicalQuantityType.Frequencies;
            graphPlotControl.GraphTitle = "Wafer 1";
            graphPlotControl.UpdateData();

            graphPlotControl = (GraphPlotControl)_integralPlotControl.Graphs[1];
            graphPlotControl.Plot.CoordinateSystem = new WaferDiagram();
            graphPlotControl.Plot.Legend.Visible = false;
            graphPlotControl.BorderStyle = BorderStyle.None;
            graphPlotControl.MeasurementSystem = measSys;
            graphPlotControl.Plot.CoordinateSystem.SweepValueQuantityType = PhysicalQuantityType.Scalar;
            graphPlotControl.GraphTitle = "Wafer 2";
            graphPlotControl.UpdateData();
        }

        private void JoinTraces_Click(object sender, RoutedEventArgs e)
        {
            JoinTraces.Visibility = System.Windows.Visibility.Collapsed;
            Measurements.Visibility = SplitTraces.Visibility = System.Windows.Visibility.Visible;

            _integralPlotControl.Data.ComplexFunctions[1].GraphIndex = 0;
            _integralPlotControl.Data.ComplexFunctions[0].Visible = true;
            _integralPlotControl.Data.ComplexFunctions[1].Visible = false;
            _integralPlotControl.UpdateAllGraphs();

            var measSys = new MeasurementSystem();
            measSys.SetPreambula(SiPreambuledUnitType.Hz, SiPreambula.Giga);
            var graphPlotControl = (GraphPlotControl)_integralPlotControl.Graphs[0];
            graphPlotControl.Plot.CoordinateSystem = new WaferDiagram();
            graphPlotControl.Plot.Legend.Visible = false;
            graphPlotControl.BorderStyle = BorderStyle.None;
            graphPlotControl.MeasurementSystem = measSys;
            graphPlotControl.Plot.CoordinateSystem.SweepValueQuantityType = PhysicalQuantityType.Frequencies;
            graphPlotControl.GraphTitle = "Wafer";
            graphPlotControl.UpdateData();
        }
    }
}
