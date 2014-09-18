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

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    class ScrollBarHelper
    {
        public static ScrollBars GetVisibleScrollbars(Control ctl)
        {
            var wndStyle = Win32.GetWindowLong(ctl.Handle, Win32.GwlStyle);
            var hsVisible = (wndStyle & Win32.WsHscroll) != 0;
            var vsVisible = (wndStyle & Win32.WsVscroll) != 0;

            if (hsVisible)
                return vsVisible ? ScrollBars.Both : ScrollBars.Horizontal;
            return vsVisible ? ScrollBars.Vertical : ScrollBars.None;
        }
    }

    public class Win32
    {
        // offset of window style value
        public const int GwlStyle = -16;

        // window style constants for scrollbars
        public const int WsVscroll = 0x00200000;
        public const int WsHscroll = 0x00100000;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    }
}
