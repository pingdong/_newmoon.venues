using System;

namespace PingDong.Testings
{
    public class ModelGenerator
    {
        private static readonly Random Random = new Random();

        public static object Generate(Type type)
        {
            if (type == typeof(Guid))
                return Guid.NewGuid();

            if (type == typeof(string))
                return NextString();

            if (type == typeof(decimal))
                return NextDecimal();

            if (type == typeof(byte))
                return NextByte();

            if (type == typeof(byte[]))
                return NextBytes(25);

            if (type == typeof(char))
                return NextChar();

            if (type == typeof(int))
                return NextInt();

            if (type == typeof(short))
                return NextShort();

            if (type == typeof(long))
                return NextLong();

            if (type == typeof(float))
                return NextFloat();

            if (type == typeof(double))
                return NextDouble();

            if (type.IsEnum)
                return NextEnum(type);

            throw new NotImplementedException();
        }

        public static string NextString()
        {
            return Guid.NewGuid().ToString();
        }

        public static decimal NextDecimal()
        {
            return (decimal)Random.NextDouble();
        }

        public static byte NextByte()
        {
            var buffer = new byte[1];
            Random.NextBytes(buffer);
            return buffer[0];
        }

        public static byte[] NextBytes(int length)
        {
            var newValue = new byte[length];
            Random.NextBytes(newValue);
            return newValue;
        }

        public static char NextChar()
        {
            return (char)Random.Next(0, 256);
        }

        public static int NextInt()
        {
            return NextInt(int.MinValue, int.MaxValue);
        }

        public static int NextInt(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue);
        }

        public static short NextShort()
        {
            return Convert.ToInt16(Random.Next(short.MinValue, short.MaxValue));
        }

        public static long NextLong()
        {
            return Random.Next() * Random.Next();
        }

        public static object NextEnum(Type enumType)
        {
            var values = Enum.GetValues(enumType);

            var nextInt = NextInt(0, values.Length);

            return values.GetValue(nextInt);
        }

        public static float NextFloat()
        {
            return Convert.ToSingle(Random.NextDouble());
        }

        public static Guid NextGuid()
        {
            return Guid.NewGuid();
        }

        public static double NextDouble()
        {
            return Random.NextDouble();
        }
    }
}
