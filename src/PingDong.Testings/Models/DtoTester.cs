using System;
using System.Collections.Generic;
using System.Linq;

namespace PingDong.Testings
{
    public class DtoClassTester<T> where T : class
    {
        public bool VerifyPropertiesAssignedFromConstructor(IList<string> exclusion = null, Dictionary<string, Func<object>> valueGenerator = null)
        {
            var type = typeof(T);
            var constructor = type.GetConstructors()[0];

            // Create parameter list
            var parameters = new Dictionary<string, object>();
            foreach (var parameter in constructor.GetParameters())
            {
                if (exclusion != null &&
                    exclusion.Contains(parameter.Name, StringComparer.InvariantCultureIgnoreCase))
                    continue;

                var key = parameter.Name.FirstCharToUpper();
                object value;
                if (valueGenerator != null && valueGenerator.ContainsKey(parameter.Name))
                    value = valueGenerator[parameter.Name]();
                else
                    value = ModelGenerator.Generate(parameter.ParameterType);

                parameters.Add(key, value);
            }

            // Initial Object
            var instance = (T)Activator.CreateInstance(typeof(T), parameters.Values.ToArray());

            // Verify value
            foreach (var key in parameters.Keys)
            {
                var pi = type.GetProperty(key);
                if (pi == null)
                    throw new NullReferenceException();

                var value = pi.GetValue(instance);
                if (value == null) // Missing assign value to property
                    return false;

                if (!value.Equals(parameters[key]))
                    return false;
            }

            return true;
        }
    }
}
