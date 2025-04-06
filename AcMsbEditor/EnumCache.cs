using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace ACMsbEditor
{
    internal static class EnumCache<TEnum> where TEnum : struct, Enum
    {
        static readonly string[] Names = Enum.GetNames<TEnum>();
        static readonly TEnum[] Values = Enum.GetValues<TEnum>();
        static readonly Dictionary<TEnum, int> ValueIndexDictionary = BuildValueIndexDictionary();
        static readonly Dictionary<string, TEnum> NameValueDictionary = BuildNameValueDictionary();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string[] GetEnumNames()
            => Names;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TEnum[] GetEnumValues()
            => Values;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetEnumName(TEnum value)
            => Names[GetEnumIndex(value)];

        public static bool TryGetEnumName(TEnum value, [NotNullWhen(true)] out string? name)
        {
            if (TryGetEnumIndex(value, out int index))
            {
                name = Names[index];
                return true;
            }

            name = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetEnumName(int index)
            => Names[index];

        public static bool TryGetEnumName(int index, [NotNullWhen(true)] out string? name)
        {
            if (index < 0 || index > Values.Length)
            {
                name = null;
                return false;
            }

            name = Names[index];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetEnumIndex(TEnum value)
            => ValueIndexDictionary[value];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEnumIndex(TEnum value, [NotNullWhen(true)] out int index)
            => ValueIndexDictionary.TryGetValue(value, out index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TEnum GetEnumValue(string name)
            => NameValueDictionary[name];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEnumValue(string name, [NotNullWhen(true)] out TEnum value)
            => NameValueDictionary.TryGetValue(name, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TEnum GetEnumValue(int index)
            => Values[index];

        public static bool TryGetEnumValue(int index, [NotNullWhen(true)] out TEnum value)
        {
            if (index < 0 || index > Values.Length)
            {
                value = default;
                return false;
            }

            value = Values[index];
            return true;
        }

        static Dictionary<string, TEnum> BuildNameValueDictionary()
        {
            var dictionary = new Dictionary<string, TEnum>();
            for (int i = 0; i < Values.Length; i++)
            {
                var value = Values[i];
                dictionary.Add(value.ToString(), value);
            }

            return dictionary;
        }

        static Dictionary<TEnum, int> BuildValueIndexDictionary()
        {
            var dictionary = new Dictionary<TEnum, int>(Values.Length);
            for (int i = 0; i < Values.Length; i++)
            {
                dictionary.Add(Values[i], i);
            }

            return dictionary;
        }
    }
}
