# AzureFolderMigrator

[![Built with Nuke](http://nuke.build/rounded)](https://www.nuke.build)

You can find all releases in the _Releases_ section of this GitHub repository.

This small tool is an easy way to mirror a local folder to an Azure Blob Storage container. Call the tool via:

    dotnet AzureFolderMigrator.dll <PathToLocalFolder> <AzureBlobStorageConnectionString> <?ContainerName?>

The `ContainerName` variable is optional, it defaults to `media`. The tool will then copy every file to an Azure Blob Storage account under the specified container.
