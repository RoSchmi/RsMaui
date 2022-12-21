using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using CoreText;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BarChartMauiLiveCharts
{
    public partial class MainPageViewModel : ObservableObject
    {

        // https://github.com/beto-rodriguez/LiveCharts2/blob/master/docs/cartesianChart/columnseries.md

        private float[] actYearValues = new float[366];
        private float[] actYear_minus_1_Values = new float[366];
        private float[] actYear_minus_2_Values = new float[366];
        private float[] actYear_minus_3_Values = new float[366];


        static ColumnSeries<int> actWeekColumnSeries = new ColumnSeries<int>();
        static ColumnSeries<int> actWeek_Year_Minus_1_ColumnSeries = new ColumnSeries<int>();
        static ColumnSeries<int> actWeek_Year_Minus_2_ColumnSeries = new ColumnSeries<int>();
        static ColumnSeries<int> actWeek_Year_Minus_3_ColumnSeries = new ColumnSeries<int>();

        private ISeries[] weekSeries { get; set; } = null;


        #region Constructor
        public MainPageViewModel()
        {    
            Random random = new(1357);  // arbitrary seed

            // populate 4 arrays (each for the 365 or 366 days of a year) with 'arbitrary' example float values, representing the last 4 years 
            actYearValues           = PopulateArray(DateTime.Today, 0, random);       
            actYear_minus_1_Values  = PopulateArray(DateTime.Today.AddYears(-1), - 10, random);
            actYear_minus_2_Values  = PopulateArray(DateTime.Today.AddYears(-2), 10, random);
            actYear_minus_3_Values  = PopulateArray(DateTime.Today.AddYears(-3), -15, random);

            weekSeries = ActualizeWeekSeries(DateTime.Today, ref actYearValues, ref actYear_minus_1_Values, ref actYear_minus_2_Values, ref actYear_minus_3_Values);

            Series = weekSeries;
        }
        #endregion

        #region Region ActualizeWeekSeries
        static ISeries[] ActualizeWeekSeries(DateTime currentDate, 
                                            ref float[] actYearValues, 
                                            ref float[] actYear_minus_1_Values,
                                            ref float[] actYear_minus_2_Values,
                                            ref float[] actYear_minus_3_Values
                                            )
        {
            int dayOfWeek = (int)currentDate.DayOfWeek;

            int dayOfYear = (int)currentDate.DayOfYear;

            int firstDayOfWeek = (int)currentDate.DayOfYear - ((int)currentDate.DayOfWeek - 1);

            ColumnSeries<int> actWeekColumnSeries = new ColumnSeries<int>();
            ColumnSeries<int> actWeek_Year_Minus_1_ColumnSeries = new ColumnSeries<int>();
            ColumnSeries<int> actWeek_Year_Minus_2_ColumnSeries = new ColumnSeries<int>();
            ColumnSeries<int> actWeek_Year_Minus_3_ColumnSeries = new ColumnSeries<int>();


            actWeekColumnSeries.Values = new[] { (int)actYearValues[firstDayOfWeek] ,
                                                dayOfYear >= firstDayOfWeek + 1 ? (int)actYearValues[firstDayOfWeek + 1] : 0,
                                                dayOfYear >= firstDayOfWeek + 2 ? (int)actYearValues[firstDayOfWeek + 2] : 0,
                                                dayOfYear >= firstDayOfWeek + 3 ? (int)actYearValues[firstDayOfWeek + 3] : 0,
                                                dayOfYear >= firstDayOfWeek + 4 ? (int)actYearValues[firstDayOfWeek + 4] : 0,
                                                dayOfYear >= firstDayOfWeek + 5 ? (int)actYearValues[firstDayOfWeek + 5] : 0,
                                                dayOfYear >= firstDayOfWeek + 6 ? (int)actYearValues[firstDayOfWeek + 6] : 0
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

            ISeries[] localWeekSeries = new ISeries[]
            {
                new ColumnSeries<int>
                {
                    //Values = new [] { 4, 4, 7, 2, 8, 4, 3 },               
                    Values = actWeek_Year_Minus_3_ColumnSeries.Values,
                    //Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 4 }, // mark
                    MaxBarWidth = 20, // mark
                    Padding = 2,
                    Stroke = null,
                    Fill = new SolidColorPaint(SKColors.Yellow) { },
                    //Fill = null,
                },
                new ColumnSeries<int>
                {
                    Values = actWeek_Year_Minus_2_ColumnSeries.Values,
                    MaxBarWidth = 20, // mark
                    Padding = 2,
                    Fill = new SolidColorPaint(SKColors.Green),

                },
                new ColumnSeries<int>
                {
                    Values = actWeek_Year_Minus_1_ColumnSeries.Values,
                    MaxBarWidth = 20, // mark
                    Padding = 2,
                    Fill = new SolidColorPaint(SKColors.Blue),
                },
                new ColumnSeries<int>
                {
                    Values = actWeekColumnSeries.Values,
                    MaxBarWidth = 20, // mark
                    Padding = 2,
                    Fill = new SolidColorPaint(SKColors.Red),
                }
            };      
            return localWeekSeries;
        }
        #endregion

        [ObservableProperty]
        private DateTime currentDate = DateTime.Today;


        [RelayCommand]
        private void SwitchToWeekDisplay()
        {
            int dummy78 = 1;
        }

        [RelayCommand]
        private void TimeShiftBack()
        {
            CurrentDate = CurrentDate.DayOfYear > 8 ? CurrentDate.AddDays(-7) : CurrentDate;
            Series = ActualizeWeekSeries(CurrentDate, ref actYearValues, ref actYear_minus_1_Values, ref actYear_minus_2_Values, ref actYear_minus_3_Values);
        }

        [RelayCommand]
        private void TimeShiftFore()
        {
            CurrentDate = CurrentDate.DayOfYear + 7 < DateTime.Today.DayOfYear  ? CurrentDate.AddDays(7) : CurrentDate;
            Series = ActualizeWeekSeries(CurrentDate, ref actYearValues, ref actYear_minus_1_Values, ref actYear_minus_2_Values, ref actYear_minus_3_Values);
        }


        #region Region Populate array
        static float[] PopulateArray(DateTime date, int displacement, Random random)
        { 
            int daysInYear = DateTime.IsLeapYear(date.Year) ? 366 : 365;
            DateTime firstDayofYear = new DateTime(date.Year, 1, 1);
            float[] currentYearValues = new float[366];

            for (int i = 0; i < daysInYear; i++)
            {
                int actMonth = firstDayofYear.AddDays(i).Month;
                switch (actMonth)
                {
                    case 1:
                        {
                            currentYearValues[i] = 50 + displacement + 20 * random.Next(100) / 100.0f;
                        }
                        break;
                    case 2:
                        {
                            currentYearValues[i] = 60 + displacement + 20 * random.Next(100) / 100.0f;
                        }
                        break;
                    case 3:
                        {
                            currentYearValues[i] = 40 + displacement + 20 * random.Next(100) / 100.0f;
                        }
                        break;
                    case 4:
                        {
                            currentYearValues[i] = 30 + displacement + 20 * random.Next(100) / 100.0f;
                        }
                        break;
                    case 5:
                        {
                            currentYearValues[i] = 20 + displacement + 20 * random.Next(100) / 100.0f;
                        }
                        break;
                    case 6:
                        {
                            currentYearValues[i] = 20 + displacement + 20 * random.Next(100) / 100.0f;
                        }
                        break;
                    case 7:
                        {
                            currentYearValues[i] = 20 + displacement + 20 * random.Next(100) / 100.0f;
                        }
                        break;
                    case 8:
                        {
                            currentYearValues[i] = 20 + displacement + 20 * random.Next(100) / 100.0f;
                        }
                        break;
                    case 9:
                        {
                            currentYearValues[i] = 30 + displacement + 20 * random.Next(100) / 100.0f;
                        }
                        break;
                    case 10:
                        {
                            currentYearValues[i] = 40 + displacement + 20 * random.Next(100) / 100.0f;
                        }
                        break;
                    case 11:
                        {
                            currentYearValues[i] = 50 + displacement + 20 * random.Next(100) / 100.0f;
                        }
                        break;
                    case 12:
                        {
                            currentYearValues[i] = 50 + displacement + 20 * random.Next(100) / 100.0f;
                        }
                        break;
                }     
            }   
            return currentYearValues;
        }
        #endregion

        private ISeries[] series = new ISeries[]
        {
            new ColumnSeries<int>
            {
                Values = new [] { 0 },
            }
        };

        public ISeries[] Series { get => series; set { _ = SetProperty(ref series, value); }}
    }
}
