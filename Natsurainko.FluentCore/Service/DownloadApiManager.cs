using System;
using System.Collections.Generic;
using System.Text;

namespace Natsurainko.FluentCore.Service
{
    public static class DownloadApiManager
    {
        public static DownloadApi Current { get; set; } = new DownloadApi
        {
            Host = "https://download.mcbbs.net",
            VersionManifest = "https://download.mcbbs.net/mc/game/version_manifest.json",
            Assets = "https://download.mcbbs.net/assets",
            Libraries = "https://download.mcbbs.net/maven"
        };

        public static readonly DownloadApi Mojang = new DownloadApi 
        {
            Host = "https://launcher.mojang.com",
            VersionManifest = "http://launchermeta.mojang.com/mc/game/version_manifest.json",
            Assets = "http://resources.download.minecraft.net",
            Libraries = "https://libraries.minecraft.net"
        };

        public static readonly DownloadApi Bmcl = new DownloadApi
        {
            Host = "https://bmclapi2.bangbang93.com",
            VersionManifest = "https://bmclapi2.bangbang93.com/mc/game/version_manifest.json",
            Assets = "https://bmclapi2.bangbang93.com/assets",
            Libraries = "https://bmclapi2.bangbang93.com/maven"
        };

        public static readonly DownloadApi Mcbbs = new DownloadApi
        {
            Host = "https://download.mcbbs.net",
            VersionManifest = "https://download.mcbbs.net/mc/game/version_manifest.json",
            Assets = "https://download.mcbbs.net/assets",
            Libraries = "https://download.mcbbs.net/maven"
        };

        public static readonly string ForgeLibrary = "https://files.minecraftforge.net/maven";
    }
}
