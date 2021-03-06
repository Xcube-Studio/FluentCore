using Natsurainko.FluentCore.Class.Model.Download;
using Natsurainko.FluentCore.Class.Model.Parser;
using Natsurainko.Toolkits.Text;
using Natsurainko.Toolkits.Values;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Natsurainko.FluentCore.Module.Parser
{
    public class LibraryParser
    {
        public VersionJsonEntity Entity { get; set; }

        public DirectoryInfo Root { get; set; }

        public LibraryParser(VersionJsonEntity entity, DirectoryInfo root)
        {
            this.Entity = entity;
            this.Root = root;
        }

        public IEnumerable<LibraryResource> GetLibraries()
        {
            foreach (var libraryJsonEntity in this.Entity.Libraries)
            {
                var libraryResource = new LibraryResource
                {
                    CheckSum = (libraryJsonEntity.Downloads?.Artifact?.Sha1) ?? string.Empty,
                    Size = (libraryJsonEntity.Downloads?.Artifact?.Size == null) ? 0 : (int)libraryJsonEntity.Downloads?.Artifact?.Size,
                    Url = ((libraryJsonEntity.Downloads?.Artifact?.Url) ?? string.Empty) + libraryJsonEntity.Url,
                    Name = libraryJsonEntity.Name,
                    Root = this.Root,
                    IsEnable = true
                };

                if (libraryJsonEntity.Rules != null)
                    libraryResource.IsEnable = GetAblility(libraryJsonEntity, EnvironmentInfo.GetPlatformName());

                if (libraryJsonEntity.Natives != null)
                {
                    libraryResource.IsNatives = true;

                    if (!libraryJsonEntity.Natives.ContainsKey(EnvironmentInfo.GetPlatformName()))
                        libraryResource.IsEnable = false;

                    if (libraryResource.IsEnable)
                    {
                        libraryResource.Name += $":{GetNativeName(libraryJsonEntity)}";

                        var file = libraryJsonEntity.Downloads.Classifiers[libraryJsonEntity.Natives[EnvironmentInfo.GetPlatformName()].Replace("${arch}", EnvironmentInfo.Arch)];

                        libraryResource.CheckSum = file.Sha1;
                        libraryResource.Size = file.Size;
                        libraryResource.Url = file.Url;
                    }
                }

                yield return libraryResource;
            }
        }

        private string GetNativeName(LibraryJsonEntity libraryJsonEntity) => libraryJsonEntity.Natives[EnvironmentInfo.GetPlatformName()].Replace("${arch}", EnvironmentInfo.Arch);

        private bool GetAblility(LibraryJsonEntity libraryJsonEntity, string platform)
        {
            bool windows = false;
            bool linux = false;
            bool osx = false;

            foreach (var item in libraryJsonEntity.Rules)
            {
                if (item.Action == "allow")
                {
                    if (item.System == null)
                    {
                        windows = linux = osx = true;
                        continue;
                    }

                    foreach (var (_, os) in item.System)
                        switch (os)
                        {
                            case "windows": 
                                windows = true;
                                break;
                            case "linux":
                                linux = true;
                                break;
                            case "osx":
                                osx = true;
                                break;
                        }
                }
                else if (item.Action == "disallow")
                {
                    if (item.System == null)
                    {
                        windows = linux = osx = false;
                        continue;
                    }

                    foreach (var (_, os) in item.System)
                        switch (os)
                        {
                            case "windows":
                                windows = false;
                                break;
                            case "linux":
                                linux = false;
                                break;
                            case "osx":
                                osx = false;
                                break;
                        }
                }
            }

            switch (platform)
            {
                case "windows":
                    return windows;
                case "linux":
                    return linux;
                case "osx":
                    return osx;
            }

            return false;
        }
    }
}
