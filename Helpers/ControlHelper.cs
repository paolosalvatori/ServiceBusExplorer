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
using System.Windows.Forms;
using System.Runtime.InteropServices;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public static class ControlHelper 
    {     
        #region Redraw Suspend/Resume
        [DllImport("user32.dll", EntryPoint = "SendMessageA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]     
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);     
        private const int WmSetredraw = 0xB;      
        public static void SuspendDrawing(this Control target)     
        {         
            SendMessage(target.Handle, WmSetredraw, 0, 0);     
        }      
        
        public static void ResumeDrawing(this Control target) 
        { 
            ResumeDrawing(target, true); 
        }     

        public static void ResumeDrawing(this Control target, bool redraw)     
        {         
            SendMessage(target.Handle, WmSetredraw, 1, 0);          
            if (redraw)         
            {             
                target.Refresh();         
            }     
        }     
        #endregion 
    } 
}
