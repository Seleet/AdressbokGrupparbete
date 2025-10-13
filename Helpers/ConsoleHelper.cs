static class ConsoleHelper
{
    static public string PromptStringQuestion(string question) //Handles string input and verifies that it is not empty.
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

    static public bool PromptYesNoQuestion(string question) //Handles yes/no questions and verifies that the input is either y/yes or n/no.
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

    static public int PromptIntQuestion(string question) //Handles integer input and verifies that it is a valid integer.
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

    static public void WriteSlow(string txt, int time) //Writes text slowly to the console, character by character, with a specified delay time in milliseconds.
    {
        foreach (char c in txt)
        {
            Console.Write($"{c}");
            Thread.Sleep(time);
        }
    }
}