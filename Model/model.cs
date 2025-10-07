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

        private void EnsureFragmentFolderExists()
        {
            if (!Directory.Exists(FragmentFolder))
                Directory.CreateDirectory(FragmentFolder);
        }

        public void CreateFile(string content)
        {
            File.WriteAllText(InputFile, content);
        }

        public List<string> FragmentFile(int fragmentSize)
        {
            if (!File.Exists(InputFile))
                throw new FileNotFoundException($"File {InputFile} not found!");

            if (!Directory.Exists(FragmentFolder))
                Directory.CreateDirectory(FragmentFolder);

            string content = File.ReadAllText(InputFile);
            List<string> createdFiles = new List<string>();
            int totalFragments = (int)Math.Ceiling((double)content.Length/fragmentSize);

            int padWidth = totalFragments.ToString().Length; 

            for (int i=0;i<totalFragments;i++)
            {
                int size = Math.Min(fragmentSize,content.Length-i*fragmentSize);
                string fragmentContent=content.Substring(i*fragmentSize,size);
                string fileName = Path.Combine(FragmentFolder,$"{(i + 1).ToString().PadLeft(padWidth,'0')}.txt");

                File.WriteAllText(fileName, fragmentContent);
                createdFiles.Add(fileName);
            }

            return createdFiles;
        }

        public string ReadFragment(string fileName)
        {
            string path = Path.Combine(FragmentFolder, fileName);
            if (!File.Exists(path))
                throw new FileNotFoundException($"Fragment {fileName} not found!");

            return File.ReadAllText(path);
        }
        public string DefragmentFiles(List<string> fragmentFiles)
        {
            List<string> combined = new List<string>();
            foreach (var file in fragmentFiles)
            {
                if (File.Exists(file))
                    combined.Add(File.ReadAllText(file));
            }

            string combinedContent = string.Join("",combined);
            File.WriteAllText(OutputFile,combinedContent);
            return combinedContent;
        }
        public bool CompareFiles()
        {
            if (!File.Exists(InputFile)||!File.Exists(OutputFile))
                return false;

            string inputData = File.ReadAllText(InputFile);
            string outputData = File.ReadAllText(OutputFile);
            return inputData.Equals(outputData);
        }
        public void DeleteAllFiles()
        {
            try
            {
                if (Directory.Exists(FragmentFolder))
                {
                    var files = Directory.GetFiles(FragmentFolder);
                    foreach (var file in files)
                    {
                        File.Delete(file);
                    }
                    Directory.Delete(FragmentFolder);
                }

                if (File.Exists(InputFile)) File.Delete(InputFile);
                if (File.Exists(OutputFile)) File.Delete(OutputFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }
        }
    }
}
