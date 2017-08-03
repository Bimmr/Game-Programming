/*
	FileHelper.cs
	Assignment 3
	Revision History:
		Randy Bimm - 10/26/16 - Created
*/
using System.IO;

namespace RBAssignment3
{
    /// <summary>
    /// FileHelper Class
    /// </summary>
    class FileHelper
    {
        private string path;

        /// <summary>
        /// Create new instance of FileHelper
        /// </summary>
        /// <param name="path"></param>
        public FileHelper(string path)
        {
            this.path = path;
        }

        /// <summary>
        /// Create the file
        /// </summary>
        public void createFile()
        {
            File.WriteAllText(path, "");
        }
        /// <summary>
        /// Add a value to the file
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value associated with the key</param>
        public void add(string key, string value)
        {
            TextWriter tw = new StreamWriter(path, true);
            tw.WriteLine(key + ":" + value);
            tw.Close();
        }

        /// <summary>
        /// Get a value from the filed
        /// </summary>
        /// <param name="key">The Key</param>
        /// <returns>The value associated with the key</returns>
        public string get(string key)
        {
            TextReader tr = new StreamReader(path, true);
            string line;
            while (!(line = tr.ReadLine()).StartsWith(key)) ;
            tr.Close();
            return line.Split(':')[1];
        }
    }
}
