
using System;
using System.Globalization;
//using CoreText;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Devices;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using LiveChartsCore.Measure;
using System.Runtime.CompilerServices;
using LiveChartsCore.Drawing;
//using Microsoft.VisualBasic;
//using static CoreFoundation.DispatchSource;

namespace BarChartMauiLiveCharts
{



    public partial class MainPageViewModel : ObservableObject
    {

        // https://github.com/beto-rodriguez/LiveCharts2/blob/master/docs/cartesianChart/columnseries.md


        private double displayHeight = DeviceDisplay.Current.MainDisplayInfo.Height;
        DisplayOrientation screenOrientation = DeviceDisplay.Current.MainDisplayInfo.Orientation;

        //DeviceDisplay. changedEvent;

        // DeviceDisplay.Current.MainDisplayInfoChanged += DeviceDisplay.Current.MainDisplayInfoChanged;
        /*
                / Pixel Height: {DeviceDisplay.Current.MainDisplayInfo.Height}");
            sb.AppendLine($"Density: {DeviceDisplay.Current.MainDisplayInfo.Density}");
            sb.AppendLine($"Orientation: {DeviceDisplay.Current.MainDisplayInfo.Orientation}");
            sb.AppendLine($"Rotation: {DeviceDisplay.Current.MainDisplayInfo.Rotation}");
            sb.AppendLine($"Refresh Rate: {DeviceDisplay.Current.MainDisplayInfo.RefreshRate}");
             */

        private float[] actYearValues = new float[366];
        private float[] actYear_minus_1_Values = new float[366];
        private float[] actYear_minus_2_Values = new float[366];
        private float[] actYear_minus_3_Values = new float[366];

        enum Period { Week, Month, Quarter, Year };



        private ISeries[] WeekSeries { get; set; }

        private ISeries[] MonthsSeries { get; set; }

        private ISeries[] QuartersSeries { get; set; }

        private ISeries[] YearsSeries { get; set; }

        private static readonly IFormatProvider formatProviderInvariant = CultureInfo.InvariantCulture.DateTimeFormat;


        #region Constructor
        public MainPageViewModel()
        {
            Random random = new(1357);  // arbitrary seed

            // populate 4 arrays (each for the 365 or 366 days of a year) with 'arbitrary' example float values, representing the last 4 years 
            actYearValues = PopulateArray(DateTime.Today, 0, random);
            actYear_minus_1_Values = PopulateArray(DateTime.Today.AddYears(-1), -10, random);
            actYear_minus_2_Values = PopulateArray(DateTime.Today.AddYears(-2), 10, random);
            actYear_minus_3_Values = PopulateArray(DateTime.Today.AddYears(-3), -10, random);

            DeviceDisplay.Current.MainDisplayInfoChanged += Current_MainDisplayInfoChanged;

            //changedEvent += Current_MainDisplayInfoChanged;

            WeekSeries = ActualizeWeekSeries(DateTime.Today, ref actYearValues, ref actYear_minus_1_Values, ref actYear_minus_2_Values, ref actYear_minus_3_Values);

            Series = WeekSeries;
        }

        private void Current_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            DisplayWidth = e.DisplayInfo.Width;
            displayHeight = e.DisplayInfo.Height;
            screenOrientation = e.DisplayInfo.Orientation;     
        }
        #endregion

        [ObservableProperty]
        private static double widthFactor = 0.6;

        [ObservableProperty]
        //[NotifyPropertyChangedFor(nameof(TableWidth))]
        private double displayWidth = DeviceDisplay.Current.MainDisplayInfo.Width;

        [ObservableProperty]
        private static DateTime currentDate = DateTime.Today;

        [ObservableProperty]
        private string displayTimeSpan = FormattableString.Invariant($"{currentDate.AddDays(-(int)currentDate.DayOfWeek + 1).ToString("dd.MM.yyyy", formatProviderInvariant)} - {currentDate.AddDays(-(int)currentDate.DayOfWeek + 1).AddDays(6).ToString("dd.MM.yyyy", formatProviderInvariant)}");


        private static bool toggleSwitch = false;

       

        #region [RelayCommand] SwitchToYearsDisplay()
        [RelayCommand]
        private void SwitchToYearsDisplay()
        {
            YearsSeries = ActualizeYearsSeries(DateTime.Today, ref actYearValues, ref actYear_minus_1_Values, ref actYear_minus_2_Values, ref actYear_minus_3_Values);

            Series = YearsSeries;

            XAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Last 4 Years",
                    NamePaint = new SolidColorPaint(SKColors.Black),
                    LabelsPaint = new SolidColorPaint(SKColors.CornflowerBlue),
                    TextSize = 20,
                    UnitWidth = 1,
                    MinStep = 1,
                    Labeler = value =>  _ =  string.Concat(new DateTime(currentDate.Year, 1, 1).AddMonths((int)value).ToString("MMM", formatProviderInvariant), " - ",  new DateTime(currentDate.Year, 1, 1).AddMonths(((int)value) + 11).ToString("MMM", formatProviderInvariant)),
                }
            };
        }
        #endregion

        #region [RelayCommand] SwitchToQuartersDisplay()
        [RelayCommand]
        private void SwitchToQuartersDisplay()
        {
            QuartersSeries = ActualizeQuartersSeries(DateTime.Today, ref actYearValues, ref actYear_minus_1_Values, ref actYear_minus_2_Values, ref actYear_minus_3_Values);

            Series = QuartersSeries;

            XAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Quarters of last 4 Years",
                    NamePaint = new SolidColorPaint(SKColors.Black),
                    LabelsPaint = new SolidColorPaint(SKColors.CornflowerBlue),
                    TextSize = 20,
                    UnitWidth = 1,
                    MinStep = 1,
                    Labeler = value =>  _ =  string.Concat(new DateTime(currentDate.Year, 1, 1).AddMonths(3 * (int)value).ToString("MMM", formatProviderInvariant), " - ",  new DateTime(currentDate.Year, 1, 1).AddMonths((3 * (int)value) + 2).ToString("MMM", formatProviderInvariant)),
                }
            };
        }
        #endregion


        #region [RelayCommand] SwitchToMonthsDisplay()
        [RelayCommand]
        private void SwitchToMonthsDisplay()
        {
            MonthsSeries = ActualizeMonthsSeries(DateTime.Today, ref actYearValues, ref actYear_minus_1_Values, ref actYear_minus_2_Values, ref actYear_minus_3_Values);

            Series = MonthsSeries;

            XAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Months of last 4 Years",
                    NamePaint = new SolidColorPaint(SKColors.Black),
                    LabelsPaint = new SolidColorPaint(SKColors.CornflowerBlue),
                    TextSize = 20,
                    UnitWidth = 1,
                    MinStep = 1,
                    Labeler = value =>  _ =  string.Concat(new DateTime(currentDate.Year, 1, 1).AddMonths((int)value).ToString("MMM", formatProviderInvariant )),
                }
            };
        }
        #endregion

        #region [RelayCommand] SwitchToWeekDisplay()
        [RelayCommand]
        private void SwitchToWeekDisplay()
        {
            WeekSeries = ActualizeWeekSeries(DateTime.Today, ref actYearValues, ref actYear_minus_1_Values, ref actYear_minus_2_Values, ref actYear_minus_3_Values);

            Series = WeekSeries;

            XAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Days of Week",
                    NamePaint = new SolidColorPaint(SKColors.Black),
                    LabelsPaint = new SolidColorPaint(SKColors.CornflowerBlue),
                    TextSize = 20,
                    UnitWidth = 1,
                    MinStep = 1,
                    Labeler = value =>  _ =  string.Concat("  ", currentDate.AddDays(-((int)currentDate.DayOfWeek - 1) + (int)value).DayOfWeek.ToString().AsSpan(0,3), "\r\n", currentDate.AddDays(-((int)currentDate.DayOfWeek - 1) + (int)value).ToString("dd.MMM", formatProviderInvariant )),     
                }
            };

            YAxes = new Axis[]
            {
                new Axis
                {          
                    NamePaint = new SolidColorPaint(SKColors.Black),
                    LabelsPaint = new SolidColorPaint(SKColors.CornflowerBlue),
                    TextSize = 20,
                    UnitWidth = 1,
                    MinStep = 1,
                    MinLimit = 0,
                     
                }
            };


            /*
            WidthFactor = toggleSwitch ? 0.6f : 1.0f;
            toggleSwitch = !toggleSwitch;
            */

            int dummy78 = 1;
            
        }
        #endregion

        #region Region [RelayCommand] WeekShiftBack()
        [RelayCommand]
        private void WeekShiftBack()
        {
            CurrentDate = CurrentDate.DayOfYear > 8 ? CurrentDate.AddDays(-7) : CurrentDate;
            DisplayTimeSpan = FormattableString.Invariant($"{currentDate.AddDays(-(int)currentDate.DayOfWeek + 1).ToString("dd.MM.yyyy", formatProviderInvariant)} - {currentDate.AddDays(-(int)currentDate.DayOfWeek + 1).AddDays(6).ToString("dd.MM.yyyy", formatProviderInvariant)}");
            Series = ActualizeWeekSeries(CurrentDate, ref actYearValues, ref actYear_minus_1_Values, ref actYear_minus_2_Values, ref actYear_minus_3_Values);
        }
        #endregion

        #region Region [RelayCommand] WeekShiftFore()
        [RelayCommand]
        private void WeekShiftFore()

        {
            int firstDayOfWeek = CurrentDate.DayOfYear - ((int)CurrentDate.DayOfWeek - 1);

            int firstDayOfThisWeek = DateTime.Today.DayOfYear - ((int)DateTime.Today.DayOfWeek - 1);

            CurrentDate = firstDayOfWeek < firstDayOfThisWeek ? CurrentDate.AddDays(7) : CurrentDate;
            DisplayTimeSpan = FormattableString.Invariant($"{currentDate.AddDays(-(int)currentDate.DayOfWeek + 1).ToString("dd.MM.yyyy", formatProviderInvariant)} - {currentDate.AddDays(-(int)currentDate.DayOfWeek + 1).AddDays(6).ToString("dd.MM.yyyy", formatProviderInvariant)}");

            Series = ActualizeWeekSeries(CurrentDate, ref actYearValues, ref actYear_minus_1_Values, ref actYear_minus_2_Values, ref actYear_minus_3_Values);

        }
        #endregion

        #region Region private ISeries[] series = new ISeries[]
        private ISeries[] series = new ISeries[]
        {
            new ColumnSeries<int>
            {
                Values = new [] { 0 },
            }
        };
        #endregion

        public ISeries[] Series { get => series; set { _ = SetProperty(ref series, value); }}

        #region Region private Axis[] xAxes = new Axis[]
        private Axis[] xAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Days Week",
                    NamePaint = new SolidColorPaint(SKColors.Black),

                    LabelsPaint = new SolidColorPaint(SKColors.CornflowerBlue),
                    TextSize = 20,
                    UnitWidth = 1,
                    MinStep = 1,
                    Labeler = value =>  _ =  string.Concat("  ", currentDate.AddDays(-((int)currentDate.DayOfWeek - 1) + (int)value).DayOfWeek.ToString().AsSpan(0,3), "\r\n", currentDate.AddDays(-((int)currentDate.DayOfWeek - 1) + (int)value).ToString("dd.MMM", formatProviderInvariant )),
                }
            };
        #endregion

        public Axis[] XAxes { get => xAxes; set { _ = SetProperty(ref xAxes, value); } }

        #region Region private Axis[] yAxes = new Axis[]
        private Axis[] yAxes = new Axis[]
        {
             new Axis
             {}
        };
        #endregion
        public Axis[] YAxes { get => yAxes; set { _ = SetProperty(ref yAxes, value); } }


        #region Region ActualizeYearsSeries
        private static ISeries[] ActualizeYearsSeries(DateTime currentDate,
                                            ref float[] actYearValues,
                                            ref float[] actYear_minus_1_Values,
                                            ref float[] actYear_minus_2_Values,
                                            ref float[] actYear_minus_3_Values
                                            )
        {
            var actYear_Minus_0_Years_ColumnSeries = new ColumnSeries<int>();
            var actYear_Minus_1_Years_ColumnSeries = new ColumnSeries<int>();
            var actYear_Minus_2_Years_ColumnSeries = new ColumnSeries<int>();
            var actYear_Minus_3_Years_ColumnSeries = new ColumnSeries<int>();

            int yearOffset = 0;

            actYear_Minus_0_Years_ColumnSeries.Values = new[] {
                (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 1, 1), Period.Year, ref actYearValues),
            };

            yearOffset = -1;

            actYear_Minus_1_Years_ColumnSeries.Values = new[] {
                (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 1, 1), Period.Year, ref actYear_minus_1_Values),
            };

            yearOffset = -2;

            actYear_Minus_2_Years_ColumnSeries.Values = new[] {
                (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 1, 1), Period.Year, ref actYear_minus_2_Values),
            };

            yearOffset = -3;

            actYear_Minus_3_Years_ColumnSeries.Values = new[] {
                (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 1, 1), Period.Year, ref actYear_minus_3_Values),
            };

            var localYearsSeries = new ISeries[]
            {
                new ColumnSeries<int>
                {
                    //Values = new [] { 4, 4, 7, 2, 8, 4, 3 },               
                    Values = actYear_Minus_3_Years_ColumnSeries.Values,
                    //Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 4 }, // mark
                    MaxBarWidth = 100, // mark
                    Padding = 1,
                    Stroke = null,
                    Fill = new SolidColorPaint(SKColors.Yellow) { },
                    //Fill = null,
                },
                new ColumnSeries<int>
                {
                    Values = actYear_Minus_2_Years_ColumnSeries.Values,
                    MaxBarWidth = 100, // mark
                    Padding = 1,
                    Fill = new SolidColorPaint(SKColors.Green),

                },
                new ColumnSeries<int>
                {
                    Values = actYear_Minus_1_Years_ColumnSeries.Values,
                    MaxBarWidth = 100, // mark
                    Padding = 1,
                    Fill = new SolidColorPaint(SKColors.Blue),
                },
                new ColumnSeries<int>
                {
                    Values = actYear_Minus_0_Years_ColumnSeries.Values,
                    MaxBarWidth = 100, // mark
                    Padding = 1,
                    Fill = new SolidColorPaint(SKColors.Red),
                }
            };
            return localYearsSeries;

        }
        #endregion


        #region Region ActualizeQuartersSeries
        private static ISeries[] ActualizeQuartersSeries(DateTime currentDate,
                                            ref float[] actYearValues,
                                            ref float[] actYear_minus_1_Values,
                                            ref float[] actYear_minus_2_Values,
                                            ref float[] actYear_minus_3_Values
                                            )
        {
            var actYear_Minus_0_Quarters_ColumnSeries = new ColumnSeries<int>();
            var actYear_Minus_1_Quarters_ColumnSeries = new ColumnSeries<int>();
            var actYear_Minus_2_Quarters_ColumnSeries = new ColumnSeries<int>();
            var actYear_Minus_3_Quarters_ColumnSeries = new ColumnSeries<int>();

            int yearOffset = 0;

            actYear_Minus_0_Quarters_ColumnSeries.Values = new[] {
                (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 1, 1), Period.Quarter, ref actYearValues),
                (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 4, 1), Period.Quarter, ref actYearValues),
                (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 7, 1), Period.Quarter, ref actYearValues),
                (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 9, 1), Period.Quarter, ref actYearValues),
            };

            yearOffset = -1;

            actYear_Minus_1_Quarters_ColumnSeries.Values = new[] {
                (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 1, 1), Period.Quarter, ref actYear_minus_1_Values),
                (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 4, 1), Period.Quarter, ref actYear_minus_1_Values),
                (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 7, 1), Period.Quarter, ref actYear_minus_1_Values),
                (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 9, 1), Period.Quarter, ref actYear_minus_1_Values),
            };

            yearOffset = -2;

            actYear_Minus_2_Quarters_ColumnSeries.Values = new[] {
                (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 1, 1), Period.Quarter, ref actYear_minus_2_Values),
                (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 4, 1), Period.Quarter, ref actYear_minus_2_Values),
                (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 7, 1), Period.Quarter, ref actYear_minus_2_Values),
                (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 9, 1), Period.Quarter, ref actYear_minus_2_Values),
            };

            yearOffset = -3;

            actYear_Minus_3_Quarters_ColumnSeries.Values = new[] {
                (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 1, 1), Period.Quarter, ref actYear_minus_3_Values),
                (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 4, 1), Period.Quarter, ref actYear_minus_3_Values),
                (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 7, 1), Period.Quarter, ref actYear_minus_3_Values),
                (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 9, 1), Period.Quarter, ref actYear_minus_3_Values),
            };

            var localQuartersSeries = new ISeries[]
            {
                new ColumnSeries<int>
                {
                    //Values = new [] { 4, 4, 7, 2, 8, 4, 3 },               
                    Values = actYear_Minus_3_Quarters_ColumnSeries.Values,
                    //Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 4 }, // mark
                    
                    MaxBarWidth = 40,
                    Padding = 1,
                    Stroke = null,
                    Fill = new SolidColorPaint(SKColors.Yellow) { },
                    //Fill = null,
                },
                new ColumnSeries<int>
                {
                    Values = actYear_Minus_2_Quarters_ColumnSeries.Values,

                    MaxBarWidth = 40,

                    Padding = 1,
                    Fill = new SolidColorPaint(SKColors.Green),

                },
                new ColumnSeries<int>
                {
                    Values = actYear_Minus_1_Quarters_ColumnSeries.Values,
                    MaxBarWidth = 40, // mark
                    Padding = 1,
                    Fill = new SolidColorPaint(SKColors.Blue),
                },
                new ColumnSeries<int>
                {
                    Values = actYear_Minus_0_Quarters_ColumnSeries.Values,
                    MaxBarWidth = 40, // mark
                    Padding = 1,
                    Fill = new SolidColorPaint(SKColors.Red),
                }
            };
            return localQuartersSeries;

        }
        #endregion




        #region Region ActualizeMonthsSeries
        private static ISeries[] ActualizeMonthsSeries(DateTime currentDate,
                                            ref float[] actYearValues,
                                            ref float[] actYear_minus_1_Values,
                                            ref float[] actYear_minus_2_Values,
                                            ref float[] actYear_minus_3_Values
                                            )
        {
            int firstDayOfThisMonth = currentDate.DayOfYear - (currentDate.Day - 1);

           

            var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);


            //  int firstDayOfThisWeek = DateTime.Today.DayOfYear - ((int)DateTime.Today.DayOfWeek - 1);

            var actYear_Minus_0_Months_ColumnSeries = new ColumnSeries<int>();
            var actYear_Minus_1_Months_ColumnSeries = new ColumnSeries<int>();
            var actYear_Minus_2_Months_ColumnSeries = new ColumnSeries<int>();
            var actYear_Minus_3_Months_ColumnSeries = new ColumnSeries<int>();

            int yearOffset = 0;

            actYear_Minus_0_Months_ColumnSeries.Values = new[] {
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 1, 1), Period.Month, ref actYearValues),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 2, 1), Period.Month, ref actYearValues),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 3, 1), Period.Month, ref actYearValues),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 4, 1), Period.Month, ref actYearValues),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 5, 1), Period.Month, ref actYearValues),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 6, 1), Period.Month, ref actYearValues),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 7, 1), Period.Month, ref actYearValues),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 8, 1), Period.Month, ref actYearValues),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 9, 1), Period.Month, ref actYearValues),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 10, 1), Period.Month, ref actYearValues),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 11, 1), Period.Month, ref actYearValues),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 12, 1), Period.Month, ref actYearValues)
            };

            yearOffset = -1;

            actYear_Minus_1_Months_ColumnSeries.Values = new[] { (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 1, 1), Period.Month, ref actYear_minus_1_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 2, 1), Period.Month, ref actYear_minus_1_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 3, 1), Period.Month, ref actYear_minus_1_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 4, 1), Period.Month, ref actYear_minus_1_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 5, 1), Period.Month, ref actYear_minus_1_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 6, 1), Period.Month, ref actYear_minus_1_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 7, 1), Period.Month, ref actYear_minus_1_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 8, 1), Period.Month, ref actYear_minus_1_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 9, 1), Period.Month, ref actYear_minus_1_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 10, 1), Period.Month, ref actYear_minus_1_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 11, 1), Period.Month, ref actYear_minus_1_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 12, 1), Period.Month, ref actYear_minus_1_Values)
            };

            yearOffset = -2;

            actYear_Minus_2_Months_ColumnSeries.Values = new[] { (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 1, 1), Period.Month, ref actYear_minus_2_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 2, 1), Period.Month, ref actYear_minus_2_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 3, 1), Period.Month, ref actYear_minus_2_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 4, 1), Period.Month, ref actYear_minus_2_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 5, 1), Period.Month, ref actYear_minus_2_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 6, 1), Period.Month, ref actYear_minus_2_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 7, 1), Period.Month, ref actYear_minus_2_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 8, 1), Period.Month, ref actYear_minus_2_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 9, 1), Period.Month, ref actYear_minus_2_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 10, 1), Period.Month, ref actYear_minus_2_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 11, 1), Period.Month, ref actYear_minus_2_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 12, 1), Period.Month, ref actYear_minus_2_Values)
            };

            yearOffset = -3;

            actYear_Minus_3_Months_ColumnSeries.Values = new[] { (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 1, 1), Period.Month, ref actYear_minus_3_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 2, 1), Period.Month, ref actYear_minus_3_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 3, 1), Period.Month, ref actYear_minus_3_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 4, 1), Period.Month, ref actYear_minus_3_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 5, 1), Period.Month, ref actYear_minus_3_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 6, 1), Period.Month, ref actYear_minus_3_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 7, 1), Period.Month, ref actYear_minus_3_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 8, 1), Period.Month, ref actYear_minus_3_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 9, 1), Period.Month, ref actYear_minus_3_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 10, 1), Period.Month, ref actYear_minus_3_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 11, 1), Period.Month, ref actYear_minus_3_Values),
                                                         (int)GetPeriodSum(new DateTime(currentDate.AddYears(yearOffset).Year, 12, 1), Period.Month, ref actYear_minus_3_Values)
            };

            var localMonthsSeries = new ISeries[]
            {
                new ColumnSeries<int>
                {
                    //Values = new [] { 4, 4, 7, 2, 8, 4, 3 },               
                    Values = actYear_Minus_3_Months_ColumnSeries.Values,
                    //Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 4 }, // mark
                    MaxBarWidth = 12, // mark
                    Padding = 1,
                    Stroke = null,
                    Fill = new SolidColorPaint(SKColors.Yellow) { },
                    //Fill = null,
                },
                new ColumnSeries<int>
                {
                    Values = actYear_Minus_2_Months_ColumnSeries.Values,
                    MaxBarWidth = 12, // mark
                    Padding = 1,
                    Fill = new SolidColorPaint(SKColors.Green),

                },
                new ColumnSeries<int>
                {
                    Values = actYear_Minus_1_Months_ColumnSeries.Values,
                    MaxBarWidth = 12, // mark
                    Padding = 1,
                    Fill = new SolidColorPaint(SKColors.Blue),
                },
                new ColumnSeries<int>
                {
                    Values = actYear_Minus_0_Months_ColumnSeries.Values,
                    MaxBarWidth = 12, // mark
                    Padding = 1,
                    Fill = new SolidColorPaint(SKColors.Red),
                }
            };
            return localMonthsSeries;
        }
        #endregion

        private static float GetPeriodSum(DateTime date, Period period, ref float[] actYearValues)
        {
            DateTime firstDayOfPeriod = date;
            DateTime lastDayOfPeriod = date;
            int daysInPeriod = lastDayOfPeriod.DayOfYear - firstDayOfPeriod.DayOfYear;

            switch (period)
            {
                case Period.Month:
                        {
                        firstDayOfPeriod = new DateTime(date.Year, date.Month, 1);
                        lastDayOfPeriod = firstDayOfPeriod.AddMonths(1).AddSeconds(-1);
                        daysInPeriod = DateTime.Today < lastDayOfPeriod ? DateTime.Today.DayOfYear - firstDayOfPeriod.DayOfYear : lastDayOfPeriod.DayOfYear - firstDayOfPeriod.DayOfYear;
                        }

                    break;

                    case Period.Quarter:
                        {
                            firstDayOfPeriod = new DateTime(date.Year, date.Month , 1);
                            lastDayOfPeriod = firstDayOfPeriod.AddMonths(3).AddSeconds(-1);
                            daysInPeriod = DateTime.Today < lastDayOfPeriod ? DateTime.Today.DayOfYear - firstDayOfPeriod.DayOfYear : lastDayOfPeriod.DayOfYear - firstDayOfPeriod.DayOfYear;
                        }

                        break;


                    case Period.Year:
                    {
                        firstDayOfPeriod = new DateTime(date.Year, 1, 1);
                        lastDayOfPeriod = firstDayOfPeriod.AddMonths(12).AddSeconds(-1);
                        daysInPeriod = DateTime.Today < lastDayOfPeriod ? DateTime.Today.DayOfYear - firstDayOfPeriod.DayOfYear : lastDayOfPeriod.DayOfYear - firstDayOfPeriod.DayOfYear;
                    }
                    break;

                }

            /*
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);
            int firstIntDayOfMonth = firstDayOfMonth.DayOfYear;
            int lastIntDayOfMonth = lastDayOfMonth.DayOfYear;
            int daysInMonth = lastIntDayOfMonth - firstIntDayOfMonth;
            */



            float returnValue = 0;
            for (int i = 0; i < daysInPeriod; i++)
            {
                returnValue += actYearValues[i + firstDayOfPeriod.DayOfYear];
            }

            return returnValue;
        }


            #region Region ActualizeWeekSeries
            private static ISeries[] ActualizeWeekSeries(DateTime currentDate,
                                            ref float[] actYearValues,
                                            ref float[] actYear_minus_1_Values,
                                            ref float[] actYear_minus_2_Values,
                                            ref float[] actYear_minus_3_Values
                                            )
        {
        
        int firstDayOfWeek = currentDate.DayOfYear - ((int)currentDate.DayOfWeek - 1);

            int firstDayOfThisWeek = DateTime.Today.DayOfYear - ((int)DateTime.Today.DayOfWeek - 1);

            int todaysDayOfYear = DateTime.Today.DayOfYear;

            var actWeekColumnSeries = new ColumnSeries<int>();
            var actWeek_Year_Minus_1_ColumnSeries = new ColumnSeries<int>();
            var actWeek_Year_Minus_2_ColumnSeries = new ColumnSeries<int>();
            var actWeek_Year_Minus_3_ColumnSeries = new ColumnSeries<int>();

            actWeekColumnSeries.Values = new[] { (int)actYearValues[firstDayOfWeek] ,
                                                firstDayOfWeek + 1 <= todaysDayOfYear ? (int)actYearValues[firstDayOfWeek + 1] : 0,
                                                firstDayOfWeek + 2 <= todaysDayOfYear ? (int)actYearValues[firstDayOfWeek + 2] : 0,
                                                firstDayOfWeek + 3 <= todaysDayOfYear ? (int)actYearValues[firstDayOfWeek + 3] : 0,
                                                firstDayOfWeek + 4 <= todaysDayOfYear ? (int)actYearValues[firstDayOfWeek + 4] : 0,
                                                firstDayOfWeek + 5 <= todaysDayOfYear ? (int)actYearValues[firstDayOfWeek + 5] : 0,
                                                firstDayOfWeek + 6 <= todaysDayOfYear ? (int)actYearValues[firstDayOfWeek + 6] : 0
                                                };

            actWeek_Year_Minus_1_ColumnSeries.Values = new[] { (int)actYear_minus_1_Values[firstDayOfWeek],
                                                (int)actYear_minus_1_Values[firstDayOfWeek + 1],
                                                (int)actYear_minus_1_Values[firstDayOfWeek + 2],
                                                (int)actYear_minus_1_Values[firstDayOfWeek + 3],
                                                (int)actYear_minus_1_Values[firstDayOfWeek + 4],
                                                (int)actYear_minus_1_Values[firstDayOfWeek + 5],
                                                (int)actYear_minus_1_Values[firstDayOfWeek + 6]
                                                };

            actWeek_Year_Minus_2_ColumnSeries.Values = new[] { (int)actYear_minus_2_Values[firstDayOfWeek],
                                                (int)actYear_minus_2_Values[firstDayOfWeek + 1],
                                                (int)actYear_minus_2_Values[firstDayOfWeek + 2],
                                                (int)actYear_minus_2_Values[firstDayOfWeek + 3],
                                                (int)actYear_minus_2_Values[firstDayOfWeek + 4],
                                                (int)actYear_minus_2_Values[firstDayOfWeek + 5],
                                                (int)actYear_minus_2_Values[firstDayOfWeek + 6]
                                                };

            actWeek_Year_Minus_3_ColumnSeries.Values = new[] { (int)actYear_minus_3_Values[firstDayOfWeek],
                                                (int)actYear_minus_3_Values[firstDayOfWeek + 1],
                                                (int)actYear_minus_3_Values[firstDayOfWeek + 2],
                                                (int)actYear_minus_3_Values[firstDayOfWeek + 3],
                                                (int)actYear_minus_3_Values[firstDayOfWeek + 4],
                                                (int)actYear_minus_3_Values[firstDayOfWeek + 5],
                                                (int)actYear_minus_3_Values[firstDayOfWeek + 6]
                                                };

            var localWeekSeries = new ISeries[]
            {
                new ColumnSeries<int>
                {
                    //Values = new [] { 4, 4, 7, 2, 8, 4, 3 },               
                    Values = actWeek_Year_Minus_3_ColumnSeries.Values,
                    //Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 4 }, // mark
                    MaxBarWidth = 15, // mark
                    Padding = 1,
                    Stroke = null,
                    Fill = new SolidColorPaint(SKColors.Yellow) { },
                    //Fill = null,
                },
                new ColumnSeries<int>
                {
                    Values = actWeek_Year_Minus_2_ColumnSeries.Values,
                    MaxBarWidth = 15, // mark
                    Padding = 1,
                    Fill = new SolidColorPaint(SKColors.Green),

                },
                new ColumnSeries<int>
                {
                    Values = actWeek_Year_Minus_1_ColumnSeries.Values,
                    MaxBarWidth = 15, // mark
                    Padding = 1,
                    Fill = new SolidColorPaint(SKColors.Blue),
                },
                new ColumnSeries<int>
                {
                    Values = actWeekColumnSeries.Values,
                    MaxBarWidth = 15, // mark
                    Padding = 1,
                    Fill = new SolidColorPaint(SKColors.Red),
                }
            };
            return localWeekSeries;
        }
        #endregion

        #region Region Populate arrays with simulation data
        private static float[] PopulateArray(DateTime date, int displacement, Random random)
        {
            int daysInYear = DateTime.IsLeapYear(date.Year) ? 366 : 365;
            var firstDayOfYear = new DateTime(date.Year, 1, 1);
            float[] currentYearValues = new float[366];

            for (int i = 0; i < daysInYear; i++)
            {
                int actMonth = firstDayOfYear.AddDays(i).Month;
                switch (actMonth)
                {
                    case 1:
                        {
                            currentYearValues[i] = 50 + displacement + (random.Next(100) / 5.0f);
                        }

                        break;
                    case 2:
                        {
                            currentYearValues[i] = 60 + displacement + (random.Next(100) / 5.0f);
                        }

                        break;
                    case 3:
                        {
                            currentYearValues[i] = 40 + displacement + (random.Next(100) / 5.0f);
                        }

                        break;
                    case 4:
                        {
                            currentYearValues[i] = 30 + displacement + (random.Next(100) / 5.0f);
                        }

                        break;
                    case 5:
                        {
                            currentYearValues[i] = 20 + displacement + (random.Next(100) / 5.0f);
                        }

                        break;
                    case 6:
                        {
                            currentYearValues[i] = 20 + displacement + (random.Next(100) / 5.0f);
                        }

                        break;
                    case 7:
                        {
                            currentYearValues[i] = 20 + displacement + (random.Next(100) / 5.0f);
                        }

                        break;
                    case 8:
                        {
                            currentYearValues[i] = 20 + displacement + (random.Next(100) / 5.0f);
                        }

                        break;
                    case 9:
                        {
                            currentYearValues[i] = 30 + displacement + (random.Next(100) / 5.0f);
                        }

                        break;
                    case 10:
                        {
                            currentYearValues[i] = 40 + displacement + (random.Next(100) / 5.0f);
                        }

                        break;
                    case 11:
                        {
                            currentYearValues[i] = 50 + displacement + (random.Next(100) / 5.0f);
                        }

                        break;
                    case 12:
                        {
                            currentYearValues[i] = 50 + displacement + (random.Next(100) / 5.0f);
                        }

                        break;
                }
            }

            return currentYearValues;
        }
        #endregion
    }
}
