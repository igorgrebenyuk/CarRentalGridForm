namespace CarRentalGridForm.Constants
{
    /// <summary>
    /// Константы для пользовательского интерфейса и отрисовки элементов.
    /// </summary>
    public static class UiConstants
    {
        /// <summary>
        /// Максимальное значение цветового канала RGB.
        /// </summary>
        public const int MaxColorValue = 255;

        /// <summary>
        /// Базовое значение зеленого канала для градиента.
        /// </summary>
        public const int BaseGreenValue = 200;

        /// <summary>
        /// Базовое значение синего канала для градиента.
        /// </summary>
        public const int BaseBlueValue = 150;

        /// <summary>
        /// Прозрачность цвета (Alpha канал).
        /// </summary>
        public const int AlphaTransparency = 150;

        /// <summary>
        /// Горизонтальный отступ для цветной полоски в ячейке.
        /// </summary>
        public const int CellPaddingX = 4;

        /// <summary>
        /// Вертикальный отступ для цветной полоски в ячейке.
        /// </summary>
        public const int CellPaddingY = 2;

        /// <summary>
        /// Уменьшение высоты цветной полоски относительно ячейки.
        /// </summary>
        public const int CellHeightReduction = 5;

        /// <summary>
        /// Минимальная ширина цветной полоски в пикселях.
        /// </summary>
        public const int MinBarWidth = 2;

        /// <summary>
        /// Имя колонки с суммой аренды в DataGridView.
        /// </summary>
        public const string TotalSumColumnName = "colTotalSum";

        /// <summary>
        /// Критический уровень топлива для подсветки (литров).
        /// </summary>
        public const double CriticalFuelLevel = 7.0;
    }
}