namespace CarRentalGridForm.UI
{
    partial class CarEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            numMileage = new NumericUpDown();
            label1 = new Label();
            txtBrand = new TextBox();
            label2 = new Label();
            txtLicensePlate = new TextBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            numConsumption = new NumericUpDown();
            numFuel = new NumericUpDown();
            numPrice = new NumericUpDown();
            btnOk = new Button();
            btnCancel = new Button();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)numMileage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numConsumption).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numFuel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
            SuspendLayout();
            // 
            // numMileage
            // 
            numMileage.Location = new Point(215, 206);
            numMileage.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numMileage.Name = "numMileage";
            numMileage.Size = new Size(99, 23);
            numMileage.TabIndex = 0;
            // 
            // label1
            // 
            label1.AccessibleRole = AccessibleRole.MenuBar;
            label1.AutoSize = true;
            label1.Location = new Point(120, 97);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 1;
            label1.Text = "Марка";
            // 
            // txtBrand
            // 
            txtBrand.Location = new Point(215, 94);
            txtBrand.Name = "txtBrand";
            txtBrand.Size = new Size(99, 23);
            txtBrand.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(98, 150);
            label2.Name = "label2";
            label2.Size = new Size(65, 15);
            label2.TabIndex = 3;
            label2.Text = "Гос номер";
            // 
            // txtLicensePlate
            // 
            txtLicensePlate.Location = new Point(214, 150);
            txtLicensePlate.Name = "txtLicensePlate";
            txtLicensePlate.Size = new Size(100, 23);
            txtLicensePlate.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(115, 206);
            label3.Name = "label3";
            label3.Size = new Size(48, 15);
            label3.TabIndex = 5;
            label3.Text = "Пробег";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(116, 265);
            label4.Name = "label4";
            label4.Size = new Size(44, 15);
            label4.TabIndex = 6;
            label4.Text = "Расход";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(117, 304);
            label5.Name = "label5";
            label5.Size = new Size(55, 15);
            label5.TabIndex = 7;
            label5.Text = "Топливо";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(122, 354);
            label6.Name = "label6";
            label6.Size = new Size(35, 15);
            label6.TabIndex = 8;
            label6.Text = "Цена";
            // 
            // numConsumption
            // 
            numConsumption.DecimalPlaces = 1;
            numConsumption.Location = new Point(214, 257);
            numConsumption.Name = "numConsumption";
            numConsumption.Size = new Size(100, 23);
            numConsumption.TabIndex = 9;
            // 
            // numFuel
            // 
            numFuel.DecimalPlaces = 1;
            numFuel.Location = new Point(214, 304);
            numFuel.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numFuel.Name = "numFuel";
            numFuel.Size = new Size(100, 23);
            numFuel.TabIndex = 10;
            // 
            // numPrice
            // 
            numPrice.DecimalPlaces = 2;
            numPrice.Location = new Point(214, 352);
            numPrice.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numPrice.Name = "numPrice";
            numPrice.Size = new Size(100, 23);
            numPrice.TabIndex = 11;
            // 
            // btnOk
            // 
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(85, 403);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 23);
            btnOk.TabIndex = 12;
            btnOk.Text = "ОК";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(261, 403);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 13;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BorderStyle = BorderStyle.FixedSingle;
            label7.Location = new Point(132, 43);
            label7.Name = "label7";
            label7.Size = new Size(168, 17);
            label7.TabIndex = 11;
            label7.Text = "Редактирование автомобиля";
            // 
            // CarEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(457, 450);
            Controls.Add(label7);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(numPrice);
            Controls.Add(numFuel);
            Controls.Add(numConsumption);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(txtLicensePlate);
            Controls.Add(label2);
            Controls.Add(txtBrand);
            Controls.Add(label1);
            Controls.Add(numMileage);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "CarEditForm";
            Text = "Редактирование автомобиля";
            Load += CarEditForm_Load;
            Click += CarEditForm_Load;
            ((System.ComponentModel.ISupportInitialize)numMileage).EndInit();
            ((System.ComponentModel.ISupportInitialize)numConsumption).EndInit();
            ((System.ComponentModel.ISupportInitialize)numFuel).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown numMileage;
        private Label label1;
        private TextBox txtBrand;
        private Label label2;
        private TextBox txtLicensePlate;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private NumericUpDown numConsumption;
        private NumericUpDown numFuel;
        private NumericUpDown numPrice;
        private Button btnOk;
        private Button btnCancel;
        private Label label7;
    }
}