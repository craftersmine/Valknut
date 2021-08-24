using craftersmine.Valknut.Server.Models;

using Newtonsoft.Json;

using Swan.Logging;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Server
{
    public sealed class ClientsHelper
    {
        public static void CreateDefaultMetadata()
        {
            var clientsPath = Path.Combine(Program.Config.PathsConfig.ContentPath, "clients");
            var clientsMeta = Path.Combine(clientsPath, "clients-metadata.json");
            MinecraftClients clients = new MinecraftClients();
            MinecraftClient client = new MinecraftClient();
            client.ClientId = "testClient";
            client.MinecraftVersion = "1.12.2";
            client.ClientName = "Test Client";
            client.ClientEnabled = false;
            client.LaunchString =
                "-Xmx{memory}M " +
                "-Dfile.encoding=UTF-8 " +
                "-Dfml.ignoreInvalidMinecraftCertificates=true " +
                "-Dfml.ignorePatchDiscrepancies=true " +
                "-Djava.net.useSystemProxies=true " +
                "-XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump " +
                "\"-Dos.name={os}\" " +
                "-Dos.version=10.0 " +
                "\"-Djava.library.path={natives}\" " +
                "-Dminecraft.launcher.brand=java-minecraft-launcher " +
                "-Dminecraft.launcher.version=1.6.84-j " +
                "\"-Dminecraft.client.jar={minecraftJar}\" " +
                "-cp \"{classpath}\" " +
                "net.minecraft.launchwrapper.Launch " +
                "--accessToken \"{accessToken}\" " +
                "--username {username} " +
                "--version \"{clientName}\" " +
                "--gameDir {gameDir} " +
                "--assetsDir {assetDir} " +
                "--assetIndex {assetIndex} " +
                "--uuid {userUuid} " +
                "--tweakClass net.minecraftforge.fml.common.launcher.FMLTweaker " +
                "--versionType Forge " +
                "--width 925 --height 530";
            client.AssetsIndex = "1.12";
            clients.Clients = new[] { client };

            var data = JsonConvert.SerializeObject(clients, Formatting.Indented);
            File.WriteAllText(clientsMeta, data);
        }

        public static MinecraftClients GetMinecraftClientsWithoutFiles()
        {
            var clientsPath = Path.Combine(Program.Config.PathsConfig.ContentPath, "clients");
            var clientsMeta = Path.Combine(clientsPath, "clients-metadata.json");

            var metadataValue = File.ReadAllText(clientsMeta);

            var metadata = JsonConvert.DeserializeObject<MinecraftClients>(metadataValue);

            return metadata;
        }

        public static MinecraftClient GetMinecraftClient(string clientId)
        {
            var clientsPath = Path.Combine(Program.Config.PathsConfig.ContentPath, "clients");
            var clients = GetMinecraftClientsWithoutFiles();
            MinecraftClient client = clients.Clients.Where(x => x.ClientId == clientId && x.ClientEnabled == true).FirstOrDefault();

            if (client is null)
            {
                Logger.Warn("No client with ID " + clientId + " found!");
                return null;
            }

            var assetsPath = Path.Combine(clientsPath, "assets", client.AssetsIndex + ".zip");
            var minecraftPath = Path.Combine(clientsPath, client.ClientId, "bin/minecraft.jar");
            var libraries = Path.Combine(clientsPath, client.ClientId, "bin/libraries");
            var nativesPath = Path.Combine(clientsPath, client.ClientId, "bin", "natives.zip");
            var modsPath = Path.Combine(clientsPath, client.ClientId, "mods");
            var configPath = Path.Combine(clientsPath, client.ClientId, "config.zip");

            if (!File.Exists(assetsPath))
            {
                Logger.Warn("Unable to find assets for Minecraft with index \"" + client.AssetsIndex + "\" for client \"" + client.ClientId + "\"");
                return null;
            }
            if (!File.Exists(minecraftPath))
            {
                Logger.Warn("Unable to find Minecraft jar file for client \"" + client.ClientId + "\"");
                return null;
            }
            if (!Directory.Exists(libraries))
            {
                Logger.Warn("Unable to find Minecraft libraries folder for client \"" + client.ClientId + "\"");
                return null;
            }
            if (!File.Exists(nativesPath))
            {
                Logger.Warn("Unable to find natives archive for client \"" + client.ClientId + "\"");
                return null;
            }
            if (!File.Exists(configPath))
            {
                Logger.Warn("Unable to find config archive for client \"" + client.ClientId + "\"");
                return null;
            }


            MinecraftFile assetsArchive = MinecraftFile.GetMinecraftFile(assetsPath);

            MinecraftFile minecraftJar = MinecraftFile.GetMinecraftFile(minecraftPath);

            List<MinecraftFile> librariesList = new List<MinecraftFile>();
            DirectoryInfo libDir = new DirectoryInfo(libraries);
            foreach (var libFile in libDir.EnumerateFiles())
            {
                librariesList.Add(MinecraftFile.GetMinecraftFile(libFile.FullName));
            }

            MinecraftFile nativesArchive = MinecraftFile.GetMinecraftFile(nativesPath);

            List<MinecraftFile> modsList = null;
            if (Directory.Exists(modsPath))
            {
                modsList = new List<MinecraftFile>();
                DirectoryInfo modsDir = new DirectoryInfo(modsPath);
                foreach (var modFile in modsDir.EnumerateFiles("*.*", SearchOption.AllDirectories))
                {
                    modsList.Add(MinecraftFile.GetMinecraftFile(modFile.FullName));
                }
            }

            MinecraftFile configArchive = MinecraftFile.GetMinecraftFile(configPath);

            client.Files = new MinecraftFiles();

            client.Files.AssetsArchive = assetsArchive;
            client.Files.Minecraft = minecraftJar;
            client.Files.Libraries = librariesList.ToArray();
            client.Files.NativesArchive = nativesArchive;
            if (modsList is not null)
                client.Files.Mods = modsList.ToArray();
            client.Files.ConfigArchive = configArchive;

            return client;
        }
    }
}
