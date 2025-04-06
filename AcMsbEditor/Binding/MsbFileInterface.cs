using AcMsbEditor.Binding.Nodes;
using AcMsbEditor.IO;
using ACMsbEditor.Binding.Msb;
using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ACMsbEditor.Binding
{
    public class MsbFileInterface
    {
        private readonly Dictionary<string, IMsbFile> MsbFiles;

        public MsbFileInterface()
        {
            MsbFiles = [];
        }

        public bool LoadMsb(string path, MsbGame game, out MsbLoadResult result, [NotNullWhen(true)] out MsbFileInfo? msbFileInfo)
        {
            IMsbFile msbFile;
            switch (game)
            {
                case MsbGame.AC4:
                    if (MsbFiles.ContainsKey(path))
                    {
                        result = MsbLoadResult.Exists;
                        msbFileInfo = null;
                        return false;
                    }

                    var msbac4 = MSBAC4.Read(path);
                    string name = Path.GetFileName(path);
                    msbFile = new Ac4MsbFile(path, name, msbac4);
                    MsbFiles.Add(path, msbFile);
                    result = MsbLoadResult.Success;
                    msbFileInfo = MsbFileInfo.Create(msbFile);
                    return true;
                default:
                    result = MsbLoadResult.Failure;
                    msbFileInfo = null;
                    return false;
            }
        }

        public bool SaveMsb(string path)
        {
            if (!MsbFiles.TryGetValue(path, out IMsbFile? msbFile))
            {
                return false;
            }

            FileHelper.Backup(path);
            msbFile.Save();
            return true;
        }

        public bool CloseMsb(string path)
            => MsbFiles.Remove(path);

        public List<MsbFileInfo> GetFileList()
        {
            var list = new List<MsbFileInfo>();
            var files = MsbFiles.Values;
            foreach (var file in files)
            {
                list.Add(MsbFileInfo.Create(file));
            }

            return list;
        }

        public bool GetMsbNodeBinding(string path, [NotNullWhen(true)] out IMsbNodeBinding? binding)
        {
            if (!MsbFiles.TryGetValue(path, out IMsbFile? msbFile))
            {
                binding = null;
                return false;
            }

            binding = msbFile.MsbNodeBinding;
            return true;
        }
    }
}
