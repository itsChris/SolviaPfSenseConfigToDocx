namespace SolviaPfSenseConfigToDocx.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class SecretAttribute : Attribute
    {
        public bool Exclude { get; }

        public SecretAttribute(bool exclude)
        {
            Exclude = exclude;
        }
    }
}
