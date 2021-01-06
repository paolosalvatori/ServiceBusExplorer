namespace ServiceBusExplorer.Controls
{
    using System;
    using System.Globalization;
    using System.Windows.Forms;

    public partial class TimeSpanControl : UserControl
    {
        private const string Days = "Days";
        private const string Hours = "Hours";
        private const string Minutes = "Minutes";
        private const string Seconds = "Seconds";
        private const string Milliseconds = "Milliseconds";
        private const string FieldMustBeANumber = "The {0} value of the {1} field must be a number.";

        private TimeSpan? _value;

        public TimeSpanControl()
        {
            InitializeComponent();
        }

        public bool IsFilled => txtDays.IsFilled || txtHours.IsFilled || txtMinutes.IsFilled || txtSeconds.IsFilled || txtMilliseconds.IsFilled;

        public bool IsValidValue => txtDays.IsValidIntegerValue && txtHours.IsValidIntegerValue && txtMinutes.IsValidIntegerValue && txtSeconds.IsValidIntegerValue && txtMilliseconds.IsValidIntegerValue;

        public string GetErrorMessage(string fieldName)
        {
            if (IsValidValue)
            {
                return string.Empty;
            }

            string fieldValue;
            if (!txtDays.IsValidIntegerValue)
            {
                fieldValue = Days;
            }
            else if (!txtHours.IsValidIntegerValue)
            {
                fieldValue = Hours;
            }
            else if (!txtMinutes.IsValidIntegerValue)
            {
                fieldValue = Minutes;
            }
            else if (!txtSeconds.IsValidIntegerValue)
            {
                fieldValue = Seconds;
            }
            else
            {
                fieldValue = Milliseconds;
            }

            return string.Format(FieldMustBeANumber, fieldValue, fieldName);
        }

        public TimeSpan? TimeSpanValue
        {
            get
            {
                if (!IsFilled || !IsValidValue)
                {
                    return null;
                }

                return new TimeSpan(
                    txtDays.IntegerValue,
                    txtHours.IntegerValue,
                    txtMinutes.IntegerValue,
                    txtSeconds.IntegerValue,
                    txtMilliseconds.IntegerValue
                );
            }
            set 
            {
                if (value != _value)
                {
                    _value = value;
                    txtDays.Text = value?.Days.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
                    txtHours.Text = value?.Hours.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
                    txtMinutes.Text = value?.Minutes.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
                    txtSeconds.Text = value?.Seconds.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
                    txtMilliseconds.Text = value?.Milliseconds.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
                }
            }
        }
    }
}
