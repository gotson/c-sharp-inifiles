using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace IniFiles
{
    internal static class IniWriter
    {
        [DllImport("KERNEL32.DLL", EntryPoint = "WritePrivateProfileStringW",
            SetLastError = true,
            CharSet = CharSet.Unicode, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int WritePrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpString,
            string lpFilename);

        /// <summary>
        /// Write a key
        /// </summary>
        /// <param name="iniFile">The ini file.</param>
        /// <param name="category">The category.</param>
        public static void WriteKey(string iniFile, string category, string key, string value)
        {
            WritePrivateProfileString(category, key, value, iniFile);
        }
    }
}
