#if DEBUG
#define CRASH_ON_TEST_THROW
#endif

using SoulsFormats;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ACMsbEditor.Tests
{
    internal static class MsbTest
    {
        internal static void TestAC4(string directory)
        {
            void TestFile(string file)
            {
                byte[] read = File.ReadAllBytes(file);
                var msb = MSBAC4.Read(read);
                byte[] written = msb.Write();

                string? fileDir = Path.GetDirectoryName(file);
                if (string.IsNullOrEmpty(fileDir))
                {
                    throw new DirectoryNotFoundException($"Could not find folder for test file: {file}");
                }

                string basePath = Path.Combine(fileDir, "map_test_mismatches");
                string outPath = Path.Combine(basePath, $"{Path.GetFileNameWithoutExtension(file)}_mismatch.msb");
                if (!read.AsMemory().Span.SequenceEqual(written))
                {
                    Directory.CreateDirectory(basePath);
                    File.WriteAllBytes(outPath, written);
                }
                else if (Directory.Exists(basePath))
                {
                    if (File.Exists(outPath))
                    {
                        File.Delete(outPath);
                    }

                    if (Directory.GetFiles(basePath).Length == 0)
                    {
                        Directory.Delete(basePath);
                    }
                }
            }

            void RunTestFile(string file)
            {
#if CRASH_ON_TEST_THROW
                TestFile(file);
#else
                try
                {
                    TestFile(file);
                }
                catch (Exception ex)
                {
                    string errorMsg = $"Failed to test file: {file}\n{ex}";
                    Console.WriteLine(errorMsg);
                    Debug.WriteLine(errorMsg);
                }
#endif
            }

            if (!Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException($"Could not find folder for test: {directory}");
            }

            var files = Directory.EnumerateFiles(directory, "*.msb", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                RunTestFile(file);
            }
        }
    }
}
