static class Helpers
{
    static public string PromptStringQuestion(string question)
    {
        string? answer = "";
        while (string.IsNullOrEmpty(answer))
        {
            Console.Write(question);
            answer = Console.ReadLine();
            if (string.IsNullOrEmpty(answer)) Console.WriteLine("Empty field not allowed.");
        }
        return answer;
    }

    static public bool PromptYesNoQuestion(string question)
    {
        string? answer = "";
        while (string.IsNullOrEmpty(answer) ||
                answer != "y" && answer != "yes" && answer != "n" && answer != "no")
        {
            Console.Write(question);
            answer = Console.ReadLine()?.ToLower();
            if (string.IsNullOrEmpty(answer) ||
                answer != "y" && answer != "yes" && answer != "n" && answer != "no") Console.WriteLine("You may only enter y/n.");
        }
        bool validatedInput = answer == "y" || answer == "yes";
        return validatedInput;
    }

    static public int PromptIntQuestion(string question)
    {
        int num = 0;
        bool success = false;

        while (!success)
        {
            Console.Write(question);
            success = int.TryParse(Console.ReadLine(), out num);
            if (!success) Console.WriteLine("Invalid number, try again.");
        }
        return num;
    }
}