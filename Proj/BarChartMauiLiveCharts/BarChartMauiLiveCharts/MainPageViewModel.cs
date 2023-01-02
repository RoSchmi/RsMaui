
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




        private ISeries[] WeekSeries { get; set; }

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
            int dummy33 = 1;
        }
        #endregion

        private readonly double widthFactor = 1.3;

        //private double tableWidth;

        //public double TableWidth { get => tableWidth; set { _ = SetProperty(ref tableWidth, value * widthFactor); } }

        public double TableWidth => widthFactor * DisplayWidth;
    

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TableWidth))]
        private double displayWidth = DeviceDisplay.Current.MainDisplayInfo.Width;

        [ObservableProperty]
        private static DateTime currentDate = DateTime.Today;

        [ObservableProperty]
        private string displayTimeSpan = FormattableString.Invariant($"{currentDate.AddDays(-(int)currentDate.DayOfWeek + 1).ToString("dd.MM.yyyy", formatProviderInvariant)} - {currentDate.AddDays(-(int)currentDate.DayOfWeek + 1).AddDays(6).ToString("dd.MM.yyyy", formatProviderInvariant)}");

        #region [RelayCommand] SwitchToWeekDisplay()
        [RelayCommand]
        private static void SwitchToWeekDisplay()
        {
            int dummy78 = 1;
            //CurrentDate = CurrentDate.DayOfYear > 8 ? CurrentDate.AddDays(-7) : CurrentDate;
            //Series = ActualizeWeekSeries(CurrentDate, ref actYearValues, ref actYear_minus_1_Values, ref actYear_minus_2_Values, ref actYear_minus_3_Values);
        }
        #endregion

        #region Region [RelayCommand] TimeShiftBack()
        [RelayCommand]
        private void TimeShiftBack()
        {
            CurrentDate = CurrentDate.DayOfYear > 8 ? CurrentDate.AddDays(-7) : CurrentDate;
            DisplayTimeSpan = FormattableString.Invariant($"{currentDate.AddDays(-(int)currentDate.DayOfWeek + 1).ToString("dd.MM.yyyy", formatProviderInvariant)} - {currentDate.AddDays(-(int)currentDate.DayOfWeek + 1).AddDays(6).ToString("dd.MM.yyyy", formatProviderInvariant)}");
            Series = ActualizeWeekSeries(CurrentDate, ref actYearValues, ref actYear_minus_1_Values, ref actYear_minus_2_Values, ref actYear_minus_3_Values);
        }
        #endregion

        #region Region [RelayCommand] TimeShiftFore()
        [RelayCommand]
        private void TimeShiftFore()

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
             {
                   // Name = "Y Axis",
                   // NamePaint = new SolidColorPaint(SKColors.Red),
                   // LabelsPaint = new SolidColorPaint(SKColors.Green),
                   // TextSize = 20,
                   // LabelsAlignment = LiveChartsCore.Drawing.Align.Start,
                   // Labels = new string[] { "Anne"},

                    /*
                    SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray)
                    { 
                        StrokeThickness = 2,
                        PathEffect = new DashEffect(new float[] { 3, 3 }) 
                    }
                    */
             }
        };
        #endregion
        public Axis[] YAxes { get => yAxes; set { _ = SetProperty(ref yAxes, value); } }

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
                    MaxBarWidth = 10, // mark
                    Padding = 1,
                    Stroke = null,
                    Fill = new SolidColorPaint(SKColors.Yellow) { },
                    //Fill = null,
                },
                new ColumnSeries<int>
                {
                    Values = actWeek_Year_Minus_2_ColumnSeries.Values,
                    MaxBarWidth = 10, // mark
                    Padding = 1,
                    Fill = new SolidColorPaint(SKColors.Green),

                },
                new ColumnSeries<int>
                {
                    Values = actWeek_Year_Minus_1_ColumnSeries.Values,
                    MaxBarWidth = 10, // mark
                    Padding = 1,
                    Fill = new SolidColorPaint(SKColors.Blue),
                },
                new ColumnSeries<int>
                {
                    Values = actWeekColumnSeries.Values,
                    MaxBarWidth = 10, // mark
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
