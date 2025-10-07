using System;
using System.Collections.Generic;
using System.IO;

namespace FileFragmentationProject.Model
{
    public class FileModel
    {
        public string InputFile { get; set; } = "input.txt";
        public string OutputFile { get; set; } = "output.txt";
        public string FragmentFolder { get; set; } = "Fragments";

        public FileModel()
        {
            EnsureFragmentFolderExists();
        }

        // 🔹 Utility: Ensures the fragment folder is always available
        private void EnsureFragmentFolderExists()
        {
            if (!Directory.Exists(FragmentFolder))
                Directory.CreateDirectory(FragmentFolder);
        }

        // Step 1: Create main input file
        public void CreateFile(string content)
        {
            File.WriteAllText(InputFile, content);
        }

        // Step 2: Fragmentation logic
        public List<string> FragmentFile(int fragmentSize)
        {
            if (!File.Exists(InputFile))
                throw new FileNotFoundException($"File {InputFile} not found!");

            // ✅ Ensure folder exists (important if previous cleanup deleted it)
            EnsureFragmentFolderExists();

            string content = File.ReadAllText(InputFile);
            List<string> createdFiles = new List<string>();
            int totalFragments = (int)Math.Ceiling((double)content.Length / fragmentSize);

            for (int i = 0; i < totalFragments; i++)
            {
                int size = Math.Min(fragmentSize, content.Length - i * fragmentSize);
                string fragmentContent = content.Substring(i * fragmentSize, size);
                string fileName = Path.Combine(FragmentFolder, $"{(i + 1).ToString("D3")}.txt");

                File.WriteAllText(fileName, fragmentContent);
                createdFiles.Add(fileName);
            }

            return createdFiles;
        }

        // Step 3: Read a specific fragment
        public string ReadFragment(string fileName)
        {
            string path = Path.Combine(FragmentFolder, fileName);
            if (!File.Exists(path))
                throw new FileNotFoundException($"Fragment {fileName} not found!");

            return File.ReadAllText(path);
        }

        // Step 4: Defragmentation (combine all fragments)
        public string DefragmentFiles(List<string> fragmentFiles)
        {
            List<string> combined = new List<string>();
            foreach (var file in fragmentFiles)
            {
                if (File.Exists(file))
                    combined.Add(File.ReadAllText(file));
            }

            string combinedContent = string.Join("", combined);
            File.WriteAllText(OutputFile, combinedContent);
            return combinedContent;
        }

        // Step 5: Compare input.txt and output.txt
        public bool CompareFiles()
        {
            if (!File.Exists(InputFile) || !File.Exists(OutputFile))
                return false;

            string inputData = File.ReadAllText(InputFile);
            string outputData = File.ReadAllText(OutputFile);

            return inputData.Equals(outputData);
        }

        // Step 6: Delete all files from previous or current run
        public void DeleteAllFiles()
        {
            try
            {
                // Delete fragments folder if it exists
                if (Directory.Exists(FragmentFolder))
                {
                    var files = Directory.GetFiles(FragmentFolder);
                    foreach (var file in files)
                    {
                        File.Delete(file);
                    }
                    Directory.Delete(FragmentFolder);
                }

                // Delete input/output files
                if (File.Exists(InputFile)) File.Delete(InputFile);
                if (File.Exists(OutputFile)) File.Delete(OutputFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while deleting files: {ex.Message}");
            }
        }
    }
}
