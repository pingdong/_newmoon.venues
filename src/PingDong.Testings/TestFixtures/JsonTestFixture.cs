using System.IO;
using System.Reflection;

namespace PingDong.Testings.TestFixtures
{
    public class JsonTestFixtureBase : TestFixtureBase
    {
        public void Initialize(string defaultNamespace)
        {
            Namespace = defaultNamespace.EnsureNotNullOrWhitespace(nameof(defaultNamespace));
        }

        public string Namespace { get; private set; }

        public string LoadFromResourceStream(string resourceName)
        {
            var assembly = Assembly.GetCallingAssembly();

            using var stream = assembly.GetManifestResourceStream($"{Namespace}.{resourceName}");
            using var reader = new StreamReader(stream);
            
            return reader.ReadToEnd();
        }
    }
}
