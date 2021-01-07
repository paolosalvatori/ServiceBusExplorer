#region Copyright
//=======================================================================================
// Microsoft Azure Customer Advisory Team 
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// LICENSED UNDER THE APACHE LICENSE, VERSION 2.0 (THE "LICENSE"); YOU MAY NOT USE THESE 
// FILES EXCEPT IN COMPLIANCE WITH THE LICENSE. YOU MAY OBTAIN A COPY OF THE LICENSE AT 
// http://www.apache.org/licenses/LICENSE-2.0
// UNLESS REQUIRED BY APPLICABLE LAW OR AGREED TO IN WRITING, SOFTWARE DISTRIBUTED UNDER THE 
// LICENSE IS DISTRIBUTED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY 
// KIND, EITHER EXPRESS OR IMPLIED. SEE THE LICENSE FOR THE SPECIFIC LANGUAGE GOVERNING 
// PERMISSIONS AND LIMITATIONS UNDER THE LICENSE.
//=======================================================================================
#endregion

#region Using Directives

using System;
using System.Globalization;
using System.Windows.Forms;

#endregion

namespace ServiceBusExplorer.Controls
{
    public class NumericTextBox : TextBox
    {
        #region Protected Methods
        // Restricts the entry of characters to digits (including hex), the negative sign,
        // the decimal point, and editing keystrokes (backspace).
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            var numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
            var decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            var groupSeparator = numberFormatInfo.NumberGroupSeparator;
            var negativeSign = numberFormatInfo.NegativeSign;

            var keyInput = e.KeyChar.ToString(CultureInfo.InvariantCulture);


            if (char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (keyInput.Equals(groupSeparator))
            {
                // group separator is OK
            }
            else if (AllowDecimal && keyInput.Equals(decimalSeparator))
            {
                // Decimal separator is OK if configured
            }
            else if (AllowNegative && keyInput.Equals(negativeSign))
            {
                // Negative is OK if configured
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else if (AllowSpace && e.KeyChar == ' ')
            {

            }
            else
            {
                // Swallow this invalid key and beep
                e.Handled = true;
            }
        } 
        #endregion

        #region Public Properties

        public bool IsFilled => !string.IsNullOrWhiteSpace(Text);

        public bool IsValidIntegerValue => int.TryParse(Text, out _);

        public int IntegerValue => int.Parse(Text);

        public bool IsValidDecimalValue => decimal.TryParse(Text, out _);

        public decimal DecimalValue => decimal.Parse(Text);

        public bool AllowSpace { set; get; }

        public bool AllowDecimal { set; get; }

        public bool AllowNegative { set; get; }

        #endregion
    }
}
