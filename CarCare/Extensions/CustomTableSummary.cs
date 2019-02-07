using Syncfusion.SfDataGrid.XForms;

namespace CarCare
{
    /// <summary>
    /// Summary to show custom text in Column of Datagrid
    /// </summary>
    public class CustomTableSummary : GridTableSummaryCellRenderer
    {
        protected override void OnUpdateCellValue(DataColumnBase dataColumn)
        {
            base.OnUpdateCellValue(dataColumn);
        }

        public override void OnInitializeDisplayView(DataColumnBase dataColumn, SfLabel view)
        {
            base.OnInitializeDisplayView(dataColumn, view);
            if (dataColumn.GridColumn.MappingName == "Hours")
            {
                view.Text = "Total";
            }
        }
    }
}