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
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion
namespace ServiceBusExplorer.UIHelpers
{
    public static class CheckistBoxExtensions
    {
        public static bool GetItemChecked(this CheckedListBox listBox, string itemText)
        {
            int index = GetIndex(listBox, itemText);

            return listBox.GetItemChecked(index);
        }

        public static bool GetItemChecked(this CheckedListBox listBox, string itemText, bool defaultValue)
        {
            int index = listBox.Items.IndexOf(itemText);

            if (index < 0 || index > listBox.Items.Count - 1) return defaultValue;

            return listBox.GetItemChecked(index);
        }

        public static void SetItemChecked(this CheckedListBox listBox, string itemText, bool value)
        {
            int index = GetIndex(listBox, itemText);

            listBox.SetItemChecked(index, value);
        }

        private static int GetIndex(CheckedListBox listBox, string itemText)
        {
            int index = listBox.Items.IndexOf(itemText);

            if (index < 0 || index > listBox.Items.Count - 1)
            {
                throw new ArgumentException($"{itemText} does not exist the listbox");
            }

            return index;
        }
    }
}
