// <copyright file="FileService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Common.Infrastructure.Files
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BaseProject.Common.Infrastructure.Configuration;
    using BaseProject.Common.Infrastructure.Exceptions;
    using BaseProject.Common.Infrastructure.Files.Models;
    using Microsoft.AspNetCore.StaticFiles;

    public class FileService
    {
        private readonly FileConfiguration _config;

        public FileService(AppConfiguration appConfig)
        {
            _config = appConfig.File;
        }

        public async Task WriteAsync(UploadFileModel model)
        {
            string fullPath = GetFullPath(model.FileName);
            var uploadStream = model.FileStream;

            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            try
            {
                using var fileStream = File.OpenWrite(fullPath);
                uploadStream.Position = 0;
                await uploadStream.CopyToAsync(fileStream);
            }
            catch (Exception ex)
            {
                throw new FileUploadException(model, ex);
            }
            finally
            {
                uploadStream.Dispose();
            }
        }

        public ReadFileModel Read(string fileName)
        {
            string fullPath = GetFullPath(fileName);

            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"path: \"{fullPath}\"", Path.GetFileName(fullPath));
            }

            try
            {
                var provider = new FileExtensionContentTypeProvider();
                provider.TryGetContentType(fullPath, out string mimeType);

                return new ()
                {
                    FileStream = File.OpenRead(fullPath),
                    FileName = fileName,
                    MimeType = mimeType
                };
            }
            catch
            {
                throw new FileReadException($"path: \"{fullPath}\"", Path.GetFileName(fullPath));
            }
        }

        private string GetFullPath(string fileName) =>
            Path.Combine(_config.UploadedFilesDirectory, fileName);
    }
}
