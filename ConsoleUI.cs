namespace AddressBookGroupProject
{
    /// <summary>
    /// ConsoleUI is responsible for handling all user input
    /// when creating or editing a Contact.
    /// It acts like a "form" in the console:
    /// - For adding a new contact: ask for all fields.
    /// - For updating an existing contact: show current values in [brackets]
    ///   and allow ENTER to keep the existing value.
    /// </summary>
    public static class ConsoleUI
    {
        /// <summary>
        /// Ask the user for all contact details.
        /// If a template is provided, its values are shown as defaults.
        /// The user can press ENTER to keep the current value (for update).
        /// </summary>
        /// <param name="template">An existing contact (for update), or null (for add).</param>
        /// <returns>A fully constructed Contact object with the entered values.</returns>
        public static Contact ReadContactFromConsole(Contact? template = null)
        {
            // Local helper function for asking a single field
            // Shows the current value in [brackets] if template != null
            string Ask(string label, string? current = null)
            {
                Console.Write($"{label}{(string.IsNullOrEmpty(current) ? "" : $" [{current}]")}: ");
                var s = Console.ReadLine();
                s = s?.Trim() ?? "";

                // If updating and the user just pressed ENTER → keep old value
                return (template != null && s == "" && current != null) ? current : s;
            }

            // Collect all fields (new values or updated ones)
            var first = Ask("First name", template?.FirstName);
            var last = Ask("Last name", template?.LastName);
            var street = Ask("Street address", template?.StreetAddress);
            var postal = Ask("Postal code", template?.PostalCode);
            var city = Ask("City", template?.City);
            var phone = Ask("Phone number", template?.PhoneNumber);
            var email = Ask("Email", template?.Email);

            // Return a new Contact with the gathered values
            return new Contact(first, last, street, postal, city, phone, email);
        }

        /// <summary>
        /// Shortcut for adding a new contact.
        /// Simply calls ReadContactFromConsole without a template.
        /// </summary>
        // Wrapper method for readability – directly calls ReadContactFromConsole(template)
        // Used in the menu when editing an existing contact
        // Can be removed for cleaner code (menu could call ReadContactFromConsole(template) directly)
        public static Contact CreateContact() => ReadContactFromConsole();

        /// <summary>
        /// Shortcut for editing an existing contact.
        /// Calls ReadContactFromConsole with the given template.
        /// </summary>
        // Wrapper method for readability – directly calls ReadContactFromConsole(template)
        // Used in the menu when editing an existing contact
        // Can be removed for cleaner code (menu could call ReadContactFromConsole(template) directly)
        public static Contact EditContact(Contact template) => ReadContactFromConsole(template);
    }
}
