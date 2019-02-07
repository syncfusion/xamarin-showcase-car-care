using Syncfusion.SfDataGrid.XForms;
using Xamarin.Forms;

namespace CarCare
{
    /// <summary>
    /// Style for setting alternative column color for Datagrid
    /// </summary>
    public class CustomGridStyle : DataGridStyle
    {
        public override Color GetAlternatingRowBackgroundColor()
        {
            return Color.White;
        }

        public override Color GetRecordBackgroundColor()
        {
            return Color.FromHex("#EDEDED");
        }

        public override Color GetRecordForegroundColor()
        {
            return Color.FromHex("#313131");
        }

        public override GridLinesVisibility GetGridLinesVisibility()
        {
            return GridLinesVisibility.None;
        }

        public override Color GetTableSummaryBackgroundColor()
        {
            return Color.FromHex("#F1F1F1");
        }

        public override Color GetTableSummaryForegroundColor()
        {
            return Color.FromHex("#313131");
        }
    }
}