using System.IO;

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
            if (!File.Exists(fileName)) //Creates a new file if the file path can´t be found, Elise
            {
                using (FileStream fs = File.Create(fileName))
                {
                fs.Close();
                }
            }
            using (StreamReader reader = new(fileName))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
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