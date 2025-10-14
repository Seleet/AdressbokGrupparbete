/// <summary>
/// Provides static methods for reading from and writing to the AddressBook data file.
/// </summary>
/// <remarks>
/// The FileRepository class manages file-level operations such as creating the file or directory,
/// parsing CSV lines into Contact objects, and writing contact lists back to disk.
/// </remarks>
static class FileRepository
{
    /// <summary>
    /// Holds the path to the AddressBook data file.
    /// </summary>
    static private string fileName = "";
    /// <summary>
    /// Sets the full file path that will be used for subsequent read/write operations.
    /// </summary>
    /// <param name="path">The full path to the contact data file.</param>
    static public void SetFilePath(string path)
    {
        fileName = path;
    }

    /// <summary>
    /// Reads contacts from the file defined in <see cref="fileName"/>.
    /// </summary>
    /// <returns>
    /// A tuple containing:
    /// <list type="bullet">
    /// <item><term>success</term><description>True if the file was read successfully.</description></item>
    /// <item><term>contacts</term><description>A list of <see cref="Contact"/> objects, or an empty list if no contacts were found.</description></item>
    /// </list>
    /// </returns>
    /// <remarks>
    ///  Creates the file and its directory if they do not exist.
    /// Skips invalid or empty lines to prevent data corruption.
    /// </remarks>
    static public (bool success, List<Contact> contacts) ReadContacts()
    {
        List<Contact> list = new(); // Initialize empty list to return in case of failure
        try // Try-catch to handle file read errors
        {
            if (!File.Exists(fileName))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName)!);
                using var _ = File.Create(fileName);
                return (true, list);
            }

            using (StreamReader reader = new(fileName)) // Using statement for closing, creates SR object, reads file line by line, converts to list item and adds to list.
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    // Protection against broken files
                    var parts = line.Split(','); // Split line into parts by comma CSV  
                    if (parts.Length < 8) continue; // Skip lines that don't have enough parts
                    ConvertToListItem(list, line);
                }
                return (true, list);
            }
        }
        catch (Exception exp)
        {
            Console.Write(exp.Message);
            return (false, list);
        }
    }
    /// <summary>
    /// Converts a CSV-formatted line into a <see cref="Contact"/> object and adds it to the provided list.
    /// </summary>
    /// <param name="list">The list to which the new Contact object will be added.</param>
    /// <param name="listItem">A comma-separated string representing one contact record.</param>
    /// <returns>The same list instance, with the new Contact appended.</returns>
    static List<Contact> ConvertToListItem(List<Contact> list, string listItem)
    {
        var parts = listItem.Split(',');
        Contact person = new(
            long.Parse(parts[0]), parts[1], parts[2], parts[3], parts[4], (parts[5]), parts[6], parts[7]
        );
        list.Add(person);
        return list;
    }

    /// <summary>
    /// Writes all contacts to the file specified in <see cref="fileName"/>.
    /// </summary>
    /// <param name="list">The list of <see cref="Contact"/> objects to write.</param>
    /// <returns>True if the write operation succeeds; otherwise, false.</returns>
    /// <remarks>
    /// Ensures that the directory exists before attempting to write.  
    /// Overwrites the file if it already exists.
    /// </remarks>
    static public bool Write(List<Contact> list)
    {
        try
        {
            // ======================================
            // FIX (Martin, 2025-10-07):
            // Issue: App crashed when directory didn't exist.
            // Solution: Ensure directory exists before writing.
            // ======================================
            Directory.CreateDirectory(Path.GetDirectoryName(fileName)!);
            using (StreamWriter writer = new(fileName))
            {
                foreach (var item in list)
                {
                    writer.WriteLine($"{item.ID},{item.FirstName},{item.LastName},{item.Street},{item.ZipCode},{item.City},{item.Phone},{item.Email}");

                }
                return true;
            }
        }
        catch (Exception exp)
        {
            Console.Write(exp.Message);
            return false;
        }
    }
}