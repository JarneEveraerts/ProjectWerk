namespace MVVM
{
    [AttributeUsageAttribute(AttributeTargets.Property)]
	public class PropertySourceAttribute : Attribute
	{
		public IEnumerable<string> Sources { get; private set; }

		public PropertySourceAttribute(params string[] sources)
		{
			Sources = sources;
		}
	}
}