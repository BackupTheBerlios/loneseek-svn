using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LoneSeek.FileSharing
{
    public delegate Boolean FileIndexEntryFound(String file, String directory);

    /// <summary>
    /// An entry of the FileIndex class.
    /// </summary>
    public class FileIndexEntry
    {
        private String directory;
        private List<String> files = new List<String>();
        private Int32 numFiles = 0;
        private Int32 numDirs = 0;

        public event FileIndexEntryFound FileFound;

        /// <summary>
        /// Creates a new empty FileIndexEntry() object.
        /// </summary>
        public FileIndexEntry()
        {
        }

        /// <summary>
        /// Creates a new FileIndexEntry object constructed with
        /// the given directory.
        /// </summary>
        /// <param name="directory">Directory to index.</param>
        public FileIndexEntry(string directory)
        {
            this.directory = directory;
        }

        /// <summary>
        /// Retrieves the directory the object was constructed with.
        /// </summary>
        public String Directory
        {
            get { return directory; }
        }

        /// <summary>
        /// Retrieves the number of found files.
        /// </summary>
        public Int32 Files
        {
            get { return numFiles; }
        }

        /// <summary>
        /// Retrieves the number of found directories.
        /// </summary>
        public Int32 Directories
        {
            get { return numDirs; }
        }

        /// <summary>
        /// Index the files and directories of the given directory.
        /// </summary>
        /// <param name="parent"></param>
        private void Index(String parent)
        {
            string[] files = null;
            string[] dirs = null;
            string mydir = parent;

            try
            {
                if (!mydir.EndsWith("\\"))
                {
                    mydir += "\\";
                }
                files = System.IO.Directory.GetFiles(mydir);
                dirs = System.IO.Directory.GetDirectories(mydir);

                // Sum up what we have found
                numDirs += dirs.Length;
                // Now check if we should add the files.
                foreach (string file in files)
                {
                    string fullfile = mydir + file;
                    bool add = true;
                    if (FileFound != null)
                    { // Let the caller decide if he wants the file or not.
                        add = FileFound(file, mydir);
                    }
                    if (add)
                    { // Add this file to our list
                        this.files.Add(fullfile);
                    }
                }
                numFiles += this.files.Count;
                foreach (string dir in dirs)
                { // Index the subdirectory
                    Index(mydir + dir);
                }
            }
            catch (Exception)
            { // AccessViolation maybe?
            }
        }

        /// <summary>
        /// Indexes the files.
        /// </summary>
        public void Index()
        {
            Index(directory);
        }
    }
}
