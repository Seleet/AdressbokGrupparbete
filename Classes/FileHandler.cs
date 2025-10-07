static class FileHandler
{
    static public string fileName = "";
    static public void SetFilePath(string path)
    {
        fileName = @path;
    }

    static public (bool success, List<Contact> contacts) ReadContacts()
    {
        List<Contact> list = new();
        try
        {
            // ======================================
            // FIX (Martin, 2025-10-07):
            // Issue: Missing file caused FileNotFoundException.
            // Solution: Create directory and empty file if missing, return empty list.
            // ======================================
            if (!File.Exists(fileName))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName)!);
                using var _ = File.Create(fileName);
                return (true, list);
            }

            using (StreamReader reader = new(fileName))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    // Protection against broken files
                    var parts = line.Split(',');
                    if (parts.Length < 7) continue;
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
            long.Parse(parts[0]), parts[1], parts[2], parts[3], parts[4], int.Parse(parts[5]), parts[6]
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
                    writer.WriteLine($"{item.ID},{item.Name},{item.Street},{item.ZipCode},{item.City},{item.Phone},{item.Email}");
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