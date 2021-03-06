﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Solution.Base.Interfaces.Model;
using Solution.Base.Interfaces.Repository;
using Solution.Base.Interfaces.Persistance;

using System.Data.Entity;
using RefactorThis.GraphDiff;
using System.Linq.Expressions;
using System.Threading;
using System.IO;

namespace Solution.Base.Implementation.Repository.File

// Setting state manually is important in case of detached entities (entities loaded without change tracking or created outside of the current context).
{
    public class BaseJpegMetadataRepository : BaseJpegMetadataReadOnlyRepository, IBaseJpegMetadataRepository
    {
        public BaseJpegMetadataRepository(string physicalPath, Boolean includeSubDirectories, CancellationToken cancellationToken = default(CancellationToken))
            : base(physicalPath, includeSubDirectories, cancellationToken)
        {
        }

        public void Delete(string path)
        {
            var file = GetByPath(path);
            file.Delete();
        }

        public void Save(string path, byte[] bytes)
        {
            string physicalPath = _physicalPath + path;
            System.IO.File.WriteAllBytes(physicalPath, bytes);
        }

        public void Save(string path, string text)
        {
            string physicalPath = _physicalPath + path;
            System.IO.File.WriteAllText(physicalPath, text);
        }

        public void Move(string sourcePath, string destinationPath)
        {
            var file = GetByPath(sourcePath);
            var destination = _physicalPath + destinationPath;
            file.MoveTo(destinationPath);
        }

        public void Rename(string sourcePath, string newName)
        {
            var source = _physicalPath + sourcePath;
            var directory = Path.GetDirectoryName(source);
            var destinationPath = Path.Combine(directory, newName);
            System.IO.File.Move(source, destinationPath);
        }
    }

}
