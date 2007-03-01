using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LoneSeek.FileSharing
{
    /// <summary>
    /// Represents a class used to index directories and files.
    /// </summary>
    public class FileIndex
    {
        private List<String> dirs = new List<String>();
        private Dictionary<String, FileIndexEntry> indexFiles = new Dictionary<String, FileIndexEntry>();
        private List<String> extensionsAllowed = new List<String>();

        private Int32 numFiles = 0;
        private Int32 numDirs = 0;

        /// <summary>
        /// Constructs a new empty file index.
        /// </summary>
        public FileIndex()
        {
        }

        /// <summary>
        /// Recalculates the number of directories.
        /// </summary>
        private void Recalculate()
        {
            numFiles = 0;
            numDirs = indexFiles.Count;
            foreach (FileIndexEntry entry in indexFiles.Values)
            {
                numFiles += entry.Files;
                numDirs += entry.Directories;
            }
        }

        /// <summary>
        /// Adds a new directory to be indexed.
        /// </summary>
        /// <param name="Directory">Directory to add.</param>
        public void Add(String directory)
        {
            String dir = directory;
            if (!Directory.Exists(dir))
            { // Does not exist.
                throw new FileNotFoundException("Directory does not exist.", directory);
            }
#if WIN32 // For portability
          // Windows is case insensitiv so make it lower
            dir = dir.ToLower();
#endif
            if (indexFiles.ContainsKey(dir))
            { // already in list
                throw new LoneSeekException("Directory is already in the list.");
            }
            dirs.Add(dir);
            indexFiles[dir] = new FileIndexEntry(dir);
            indexFiles[dir].FileFound += new FileIndexEntryFound(FileIndex_FileFound);
            indexFiles[dir].Index();
            Recalculate();
        }

        /// <summary>
        /// Remove the given index.
        /// </summary>
        /// <param name="index">Removes the given file.</param>
        public void RemoveAt(Int32 index)
        {
            String dir = dirs[index];
            Remove(dir);
        }

        /// <summary>
        /// Remove the given directory.
        /// </summary>
        /// <param name="directory">Directory to remove</param>
        public void Remove(String directory)
        {
            String dir = directory;
#if WIN32 // For portability
          // Windows is case insensitiv so make it lower
            dir = dir.ToLower();
#endif
            dirs.Remove(dir);
            if (indexFiles.ContainsKey(dir))
            { // Remove the directory
                indexFiles.Remove(dir);
            }
            Recalculate();
        }

        /// <summary>
        /// Removes all directories from the list.
        /// </summary>
        public void Clear()
        {
            while (dirs.Count > 0)
            { // Remove first item
                RemoveAt(0);
            }
            Recalculate();
        }

        /// <summary>
        /// Retrieves a list of allowed file extensions. Only files having this
        /// file extension are indexed.
        /// </summary>
        public List<String> AllowedExtensions
        {
            get { return extensionsAllowed; }
        }

        /// <summary>
        /// Called whenever some indexer finds a file. We use it to apply a filter.
        /// </summary>
        /// <param name="file">Filename found.</param>
        /// <returns>True if he should index it, false otherwise.</returns>
        private Boolean FileIndex_FileFound(String file, String directory)
        {
            String myfull = directory + file;
            String extension = Path.GetExtension(myfull);

            foreach (String allowed in extensionsAllowed)
            {
                String myallowed = allowed;
                if (!myallowed.StartsWith("."))
                { // Do a dot at the beginning.
                    myallowed = "." + myallowed;
                }
                if (String.Compare(myallowed, extension, true) == 0)
                { // Is it an allowed extension?
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Retrieves the number of indexed directories.
        /// </summary>
        public Int32 Count
        {
            get { return dirs.Count; }
        }

        /// <summary>
        /// Returns the number of files.
        /// </summary>
        public Int32 Files
        {
            get { return numFiles; }
        }

        /// <summary>
        /// Returns the number of directories.
        /// </summary>
        public Int32 Directories
        {
            get { return numDirs; }
        }

        /// <summary>
        /// Retrieves the shared directories as array.
        /// </summary>
        /// <returns>Array containing all shared directories.</returns>
        public String[] ToArray()
        {
            return dirs.ToArray();
        }
    }
}
