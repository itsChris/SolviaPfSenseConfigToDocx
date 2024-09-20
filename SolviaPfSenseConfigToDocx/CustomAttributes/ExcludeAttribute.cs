namespace SolviaPfSenseConfigToDocx.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class ExcludeAttribute : Attribute
    {
        public bool Exclude { get; }

        public ExcludeAttribute(bool exclude)
        {
            Exclude = exclude;
        }
    }
}
