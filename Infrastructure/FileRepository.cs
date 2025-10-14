static class FileRepository
{
    static private string fileName = ""; //Changed from pulblic to private because encapsulation (Martin 2025-10-08)
    static public void SetFilePath(string path)
    {
        fileName = path;//removed @ fronm @path (Martin, 2025-10-08)
    }

    static public (bool success, List<Contact> contacts) ReadContacts() //Returns a tuple with a boolean and a list of contacts.
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

    static List<Contact> ConvertToListItem(List<Contact> list, string listItem)
    {
        var parts = listItem.Split(',');
        Contact person = new(
            long.Parse(parts[0]), parts[1], parts[2], parts[3], parts[4], (parts[5]), parts[6], parts[7]
        );
        list.Add(person);
        return list;
    }


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