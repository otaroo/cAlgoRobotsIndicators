using cAlgo.API;
using cAlgo.API.Indicators;
  
namespace cAlgo.Indicators
{
    [Indicator(AccessRights = AccessRights.None)]
    public class TrendFX : Indicator
    {
        private ExponentialMovingAverage ma;
  
        [Parameter]
        public DataSeries Source { get; set; }
  
        [Parameter("Period", DefaultValue = 50)]
        public int Period { get; set; }
  
        [Output("Trend", PlotType = PlotType.Histogram, Color = Colors.Gray)]
        public IndicatorDataSeries Result { get; set; }
  
        [Output("TrendMA", PlotType = PlotType.Line, Color = Colors.Orange)]
        public IndicatorDataSeries ResultMA { get; set; }
         
        [Output("TrendMABars", PlotType = PlotType.Line, Color = Colors.YellowGreen)]
        public IndicatorDataSeries ResultMABars { get; set; }
         
        [Parameter("Open", DefaultValue = true)]
        public bool Open { get; set; }
 
        [Parameter("Close", DefaultValue = false)]
        public bool Close { get; set; }
         
        [Parameter("Low", DefaultValue = false)]
        public bool Low { get; set; }
        
        [Parameter("High", DefaultValue = false)]
        public bool High { get; set; }
         
        protected override void Initialize()
        {
            ma = Indicators.ExponentialMovingAverage(Source, Period);
        }
  
        public override void Calculate(int index)
        {
 
            Result[index] = MarketSeries.Open[index] - ma.Result[index];
            ResultMA[index] = (MarketSeries.Open[index] - ma.Result[index]) * -1;
            ResultMABars[index] = (MarketSeries.Open[index] - ma.Result[index]);
 
            if(Close){
            Result[index] = MarketSeries.Close[index] - ma.Result[index];
            ResultMA[index] = (MarketSeries.Close[index] - ma.Result[index]) * -1;
            ResultMABars[index] = (MarketSeries.Open[index] - ma.Result[index]);
            }
             
            if(Low){
            Result[index] = MarketSeries.Low[index] - ma.Result[index];
            ResultMA[index] = (MarketSeries.Low[index] - ma.Result[index]) * -1;
            ResultMABars[index] = (MarketSeries.Open[index] - ma.Result[index]);
            }
             
            if(High){
            Result[index] = MarketSeries.High[index] - ma.Result[index];
            ResultMA[index] = (MarketSeries.High[index] - ma.Result[index]) * -1;
            ResultMABars[index] = (MarketSeries.Open[index] - ma.Result[index]);
            }
             
            if(Result[index] > 0){
            var name = "Trend";
            string text = "UP Trend Side: BUY";
            var staticPos = StaticPosition.BottomRight;
            var color = Colors.DodgerBlue;
            ChartObjects.DrawText(name, text, staticPos, color);
            }
 
            if(Result[index] < 0){
            var name = "Trend";
            string text = "DOWN Trend Side: SELL";
            var staticPos = StaticPosition.BottomRight;
            var color = Colors.Red;
            ChartObjects.DrawText(name, text, staticPos, color);
            }
             
        }
    }
}
