#region Copyright
//=======================================================================================
// Windows Azure Customer Advisory Team 
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright © 2011 Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER 
// EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF 
// MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. YOU BEAR THE RISK OF USING IT.
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
