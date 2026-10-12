using System;
using System.Data;
using Gizmox.WebGUI.Forms;

namespace StarNet.Forms.UI
{
    public partial class frmComboBoxItems : Form
    {
        private DataTable _dataTable = null;
        private StarNet.Controls.UI.ComboBox _parentComboBox;

        public string SelectedValue { get; private set; }

        public frmComboBoxItems(StarNet.Controls.UI.ComboBox comboBox)
        {
            InitializeComponent();

            _parentComboBox = comboBox;
        }

        #region Methods

        private DataTable CreateComboDataTable()
        {
            var dataTable = new DataTable();

            var dataColumn = new DataColumn();
            dataColumn.DataType = Type.GetType("System.String");
            dataColumn.ColumnName = "id";
            dataTable.Columns.Add(dataColumn);

            dataColumn = new DataColumn();
            dataColumn.DataType = Type.GetType("System.String");
            dataColumn.ColumnName = "text";
            dataTable.Columns.Add(dataColumn);

            return dataTable;
        }

        private void ResizeColumns()
        {
            var totalColumns = grdComboData.Columns.Count;

            foreach (DataGridViewColumn objCol in grdComboData.Columns)
            {
                if (objCol.AutoSizeMode != DataGridViewAutoSizeColumnMode.Fill)
                {
                    objCol.Width = (this.Width / (totalColumns));
                }
            }

            grdComboData.Update();
        }

        private void ResizeTexts()
        {
            txtCol1.Tag = objCol1.DataPropertyName;
            txtCol1.Width = objCol1.Width;
            txtCol1.Left = 0;

            txtCol2.Tag = objCol2.DataPropertyName;
            txtCol2.Width = objCol2.Width - 1;
            txtCol2.Left = txtCol1.Width + 1;
        }

        private void SelectSelected()
        {
            if (grdComboData.SelectedCells.Count == 0)
            {
                return;
            }

            SelectedValue = ((DataRowView)grdComboData
                .SelectedRows[0]
                .DataBoundItem)
                .Row["id"]
                .ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #endregion

        #region Events

        void frmComboBoxItems_Resize(object sender, EventArgs e)
        {
            ResizeColumns();
            ResizeTexts();
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            var dataTable = CreateComboDataTable();

            var rows = _dataTable
                .Select("id like '%"
                + txtCol1.Text.Trim()
                + "%' and text like '%"
                + txtCol2.Text.Trim()
                + "%'");

            foreach (var dataRow in rows)
            {
                var row = dataTable.NewRow();
                row["id"] = dataRow["id"];
                row["text"] = dataRow["text"];
                dataTable.Rows.Add(row);
            }

            grdComboData.DataSource = dataTable;
        }

        void grdComboData_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            ResizeTexts();
        }

        private void grdComboData_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectSelected();
        }

        private void grdComboData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectSelected();
        }

        #endregion

        private void frmComboBoxItems_Load(object sender, EventArgs e)
        {
            _dataTable = CreateComboDataTable();

            foreach (System.Web.UI.WebControls.ListItem item in _parentComboBox.Items)
            {
                var row = _dataTable.NewRow();
                row["id"] = item.Value;
                row["text"] = item.Text;
                _dataTable.Rows.Add(row);
            }

            grdComboData.DataSource = _dataTable;

            frmComboBoxItems_Resize(this, EventArgs.Empty);
        }
    }
}
