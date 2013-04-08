using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace IniFiles
{
    internal static class IniReader
    {
        [DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringW",
			SetLastError=true,
			CharSet=CharSet.Unicode, ExactSpelling=true,
			CallingConvention=CallingConvention.StdCall)]
		private static extern int GetPrivateProfileString(
			string lpAppName,
			string lpKeyName,
			string lpDefault,
			string lpReturnString,
			int nSize,
			string lpFilename);

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <param name="iniFile">The ini file.</param>
        /// <param name="category">The category.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static string GetValue(string iniFile, string category, string key, string defaultValue)
        {
            string returnString = new string(' ', 65536);
            GetPrivateProfileString(category, key, defaultValue, returnString, 65536, iniFile);
            return returnString.Split('\0')[0];
        }

        /// <summary>
        /// Gets the keys.
        /// </summary>
        /// <param name="iniFile">The ini file.</param>
        /// <param name="category">The category.</param>
        public static List<string> GetKeys(string iniFile, string category)
        {
            string returnString = new string(' ', 65536);
            GetPrivateProfileString(category, null, null, returnString, 65536, iniFile);
            List<string> result = new List<string>(returnString.Split('\0'));
            result.RemoveRange(result.Count-2,2);
            return result;
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <param name="iniFile">The ini file.</param>
        /// <returns></returns>
        public static List<string> GetCategories(string iniFile)
        {
            string returnString = new string(' ', 65536);
            GetPrivateProfileString(null, null, null, returnString, 65536, iniFile);
            List<string> result = new List<string>(returnString.Split('\0'));
            result.RemoveRange(result.Count - 2, 2);
            return result;
        }
    }

    
}
