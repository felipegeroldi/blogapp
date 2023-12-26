namespace BlogApp.WebApp.Events
{
    public class RegistryNotifyEventArgs : EventArgs
    {
        public RegistryNotifyEventArgs(string contact, string name)
        {
            Contact = contact;
            Name = name;
        }

        public string Contact { get; set; }
        public string Name { get; set; }
    }
}
