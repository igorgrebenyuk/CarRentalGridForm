using CarRentalGridForm.UI;
using CarRentalGridForm.DAL;
using CarRentalGridForm.Models;

namespace CarRentalGridForm.UI
{
    partial class CarRentalGridForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CarRentalGridForm));
            dgvCars = new DataGridView();
            colBrand = new DataGridViewTextBoxColumn();
            colPlate = new DataGridViewTextBoxColumn();
            colMileage = new DataGridViewTextBoxColumn();
            colConsumption = new DataGridViewTextBoxColumn();
            colFuel = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            colRange = new DataGridViewTextBoxColumn();
            colTotalSum = new DataGridViewTextBoxColumn();
            toolStrip1 = new ToolStrip();
            toolStripButton1 = new ToolStripButton();
            toolStripButton2 = new ToolStripButton();
            toolStripButton3 = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            lblStatusInfo = new ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)dgvCars).BeginInit();
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvCars
            // 
            dgvCars.AllowDrop = true;
            dgvCars.AllowUserToAddRows = false;
            dgvCars.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCars.Columns.AddRange(new DataGridViewColumn[] { colBrand, colPlate, colMileage, colConsumption, colFuel, colPrice, colRange, colTotalSum });
            dgvCars.Location = new Point(0, 28);
            dgvCars.Name = "dgvCars";
            dgvCars.ReadOnly = true;
            dgvCars.Size = new Size(878, 454);
            dgvCars.TabIndex = 0;
            dgvCars.CellPainting += dgvCars_CellPainting;
            // 
            // colBrand
            // 
            colBrand.DataPropertyName = "Brand";
            colBrand.HeaderText = "Марка";
            colBrand.Name = "colBrand";
            colBrand.ReadOnly = true;
            // 
            // colPlate
            // 
            colPlate.DataPropertyName = "LicensePlate";
            colPlate.HeaderText = "Гос номер";
            colPlate.Name = "colPlate";
            colPlate.ReadOnly = true;
            // 
            // colMileage
            // 
            colMileage.DataPropertyName = "Mileage";
            colMileage.HeaderText = "Пробег (км)";
            colMileage.Name = "colMileage";
            colMileage.ReadOnly = true;
            // 
            // colConsumption
            // 
            colConsumption.DataPropertyName = "AverageConsumption";
            colConsumption.HeaderText = "Расход (л/100км)";
            colConsumption.Name = "colConsumption";
            colConsumption.ReadOnly = true;
            // 
            // colFuel
            // 
            colFuel.DataPropertyName = "CurrentFuel";
            colFuel.HeaderText = "Топливо (л)";
            colFuel.Name = "colFuel";
            colFuel.ReadOnly = true;
            // 
            // colPrice
            // 
            colPrice.DataPropertyName = "RentCostPerMinute";
            colPrice.HeaderText = "Цена (мин)";
            colPrice.Name = "colPrice";
            colPrice.ReadOnly = true;
            // 
            // colRange
            // 
            colRange.DataPropertyName = "Range";
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            colRange.DefaultCellStyle = dataGridViewCellStyle1;
            colRange.HeaderText = "Запас хода (ч)";
            colRange.Name = "colRange";
            colRange.ReadOnly = true;
            // 
            // colTotalSum
            // 
            colTotalSum.DataPropertyName = "TotalRentSum";
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            colTotalSum.DefaultCellStyle = dataGridViewCellStyle2;
            colTotalSum.HeaderText = "Сумма аренды";
            colTotalSum.Name = "colTotalSum";
            colTotalSum.ReadOnly = true;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton1, toolStripButton2, toolStripButton3 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(878, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(23, 22);
            toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton2.Image = (Image)resources.GetObject("toolStripButton2.Image");
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(23, 22);
            toolStripButton2.Text = "Изменить ";
            toolStripButton2.Click += toolStripButton2_Click;
            // 
            // toolStripButton3
            // 
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton3.Image = (Image)resources.GetObject("toolStripButton3.Image");
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(23, 22);
            toolStripButton3.Text = "toolStripButton3";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatusInfo });
            statusStrip1.Location = new Point(0, 485);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(878, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatusInfo
            // 
            lblStatusInfo.Name = "lblStatusInfo";
            lblStatusInfo.Size = new Size(234, 17);
            lblStatusInfo.Text = "Всего машин: 0 | Критическое топливо: 0";
            // 
            // CarRentalGridForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(878, 507);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Controls.Add(dgvCars);
            Name = "CarRentalGridForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvCars).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvCars;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatusInfo;
        private DataGridViewTextBoxColumn colBrand;
        private DataGridViewTextBoxColumn colPlate;
        private DataGridViewTextBoxColumn colMileage;
        private DataGridViewTextBoxColumn colConsumption;
        private DataGridViewTextBoxColumn colFuel;
        private DataGridViewTextBoxColumn colPrice;
        private DataGridViewTextBoxColumn colRange;
        private DataGridViewTextBoxColumn colTotalSum;
    }
}
