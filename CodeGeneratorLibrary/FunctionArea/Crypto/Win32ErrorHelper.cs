/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Crypto
{

using System;
using System.Runtime.InteropServices;


    internal static class Win32ErrorHelper
    {
        internal static void ThrowExceptionIfGetLastErrorIsNotZero()
        {
            int win32ErrorCode = Marshal.GetLastWin32Error();
            if (0 != win32ErrorCode)
                Marshal.ThrowExceptionForHR(HResultFromWin32(win32ErrorCode));
        }

        private static int HResultFromWin32(int win32ErrorCode)
        {
            if (win32ErrorCode > 0)
                return (int)((((uint)win32ErrorCode) & 0x0000FFFF) | 0x80070000U);
            else return win32ErrorCode;
        }
    }
}
