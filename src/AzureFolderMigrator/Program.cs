using Dangl.AspNetCore.FileHandling.Azure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AzureFolderMigrator
{
    class Program
    {
        private static AzureBlobFileManager _azureBlobFileManager;
        private const string CONTAINER = "media";

        private static async Task Main(string[] args)
        {
            if (args.Length != 2 && args.Length != 3)
            {
                Console.WriteLine("Please provide two arguments in the following order:");
                Console.WriteLine("1. Absolute path to the local folder as the migration source");
                Console.WriteLine("2. Azure Blob Storage connection string");
                Console.WriteLine("You can optionally specify a third parameter that will be used to as the target container name, it defaults to \"media\"");
                return;
            }

            var localFolder = args[0];
            var storageConnectionString = args[1];

            var containerName = CONTAINER;
            if (args.Length >= 3)
            {
                containerName = args[2];
            }

            _azureBlobFileManager = new AzureBlobFileManager(storageConnectionString);
            await _azureBlobFileManager.EnsureContainerCreated(containerName);

            await MigrateFolder(localFolder, containerName);

            Console.WriteLine("Migration finished");
        }

        private static async Task MigrateFolder(string localFolder, string containerName)
        {
            var files = GetFiles(localFolder);
            foreach (var file in files)
            {
                var relativePath = file.Replace(localFolder, string.Empty).TrimStart('/').TrimStart('\\');
                using (var fs = File.Open(file, FileMode.Open))
                {
                    await _azureBlobFileManager.SaveFileAsync(containerName, relativePath, fs);
                }
            }
        }

        private static List<string> GetFiles(string localFolder)
        {
            return Directory.GetFiles(localFolder, "*", SearchOption.AllDirectories).ToList();
        }
    }
}
