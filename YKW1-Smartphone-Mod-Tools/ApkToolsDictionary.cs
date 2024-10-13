using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YKW1_Smartphone_Mod_Tools
{
    //APKTOOL STATES
    public static class ApkToolStates
    {
        public const string APKTOOL_START = "Using Apktool 2.9.3";
        public const string LOADING_RESOURCE_TABLE = "Loading resource table...";
        public const string DECODING_FILE_RESOURCES = "Decoding file-resources";
        public const string LOADING_RESOURCE_TABLE_FILE = "Loading resource table from file:";
        public const string DECODING_VALUES = "Decoding values */*";
        public const string DECODING_MANIFEST = "Decoding AndroidManifest.xml";
        public const string REGULAR_MANIFEST = "Regular manifest package";
        public const string BAKSMALING_CLASSES = "Baksmaling classes.dex";
        public const string COPYING_ASSETS_LIBS = "Copying assets and libs";
        public const string COPYING_UNKNOWN_FILES = "Copying unknown files";
        public const string COPYING_ORIGINAL_FILES = "Copying original files";
        public const string CHECKING_SOURCES_CHANGED = "Checking whether sources has changed";
        public const string SMALING_SMALI_FOLDER = "Smaling smali folder into classes.dex";
        public const string CHECKING_RESOURCES_CHANGED = "Checking whether resources has changed";
        public const string BUILDING_RESOURCES = "Building resources";
        public const string COPYING_LIBS_KOTLIN = "Copying libs... (/kotlin)";
        public const string COPYING_LIBS_META = "Copying libs... (/META-INF/services)";
        public const string BUILDING_APK_FILE = "Building apk file...";
        public const string BUILT_APK = "Built apk into:";
    }

    public static class ApkToolsDictionary
    {
        public static readonly Dictionary<string, int> _progressMapType1 = new Dictionary<string, int>
        {
            { ApkToolStates.APKTOOL_START, 9 },
            { ApkToolStates.LOADING_RESOURCE_TABLE, 13 },
            { ApkToolStates.DECODING_FILE_RESOURCES, 17 },
            { ApkToolStates.LOADING_RESOURCE_TABLE_FILE, 19 },
            { ApkToolStates.DECODING_VALUES, 22 },
            { ApkToolStates.DECODING_MANIFEST, 25 },
            { ApkToolStates.REGULAR_MANIFEST, 30 },
            { ApkToolStates.BAKSMALING_CLASSES, 35 },
            { ApkToolStates.COPYING_ASSETS_LIBS, 39 },
            { ApkToolStates.COPYING_UNKNOWN_FILES, 45 },
            { ApkToolStates.COPYING_ORIGINAL_FILES, 51 }
        };

        public static readonly Dictionary<string, int> _progressMapType1Split = new Dictionary<string, int>
        {
            { ApkToolStates.APKTOOL_START, 51 },
            { ApkToolStates.LOADING_RESOURCE_TABLE, 55 },
            { ApkToolStates.DECODING_FILE_RESOURCES, 61 },
            { ApkToolStates.LOADING_RESOURCE_TABLE_FILE, 64 },
            { ApkToolStates.DECODING_VALUES, 67 },
            { ApkToolStates.DECODING_MANIFEST, 70 },
            { ApkToolStates.REGULAR_MANIFEST, 74 },
            { ApkToolStates.BAKSMALING_CLASSES, 79 },
            { ApkToolStates.COPYING_ASSETS_LIBS, 81 },
            { ApkToolStates.COPYING_UNKNOWN_FILES, 91 },
            { ApkToolStates.COPYING_ORIGINAL_FILES, 100 }
        };

        public static readonly Dictionary<string, int> _progressMapType2 = new Dictionary<string, int>
        {
            { ApkToolStates.APKTOOL_START, 9 },
            { ApkToolStates.CHECKING_SOURCES_CHANGED, 13 },
            { ApkToolStates.SMALING_SMALI_FOLDER, 17 },
            { ApkToolStates.CHECKING_RESOURCES_CHANGED, 19 },
            { ApkToolStates.BUILDING_RESOURCES, 22 },
            { ApkToolStates.COPYING_LIBS_KOTLIN, 25 },
            { ApkToolStates.COPYING_LIBS_META, 30 },
            { ApkToolStates.BUILDING_APK_FILE, 35 },
            { ApkToolStates.COPYING_UNKNOWN_FILES, 39 },
            { ApkToolStates.BUILT_APK, 51 }
        };

        public static readonly Dictionary<string, int> _progressMapType2Split = new Dictionary<string, int>
        {
            { ApkToolStates.APKTOOL_START, 51 },
            { ApkToolStates.CHECKING_SOURCES_CHANGED, 55 },
            { ApkToolStates.SMALING_SMALI_FOLDER, 61 },
            { ApkToolStates.CHECKING_RESOURCES_CHANGED, 64 },
            { ApkToolStates.BUILDING_RESOURCES, 67 },
            { ApkToolStates.COPYING_LIBS_KOTLIN, 78 },
            { ApkToolStates.COPYING_LIBS_META, 84 },
            { ApkToolStates.BUILDING_APK_FILE, 87 },
            { ApkToolStates.COPYING_UNKNOWN_FILES, 91 },
            { ApkToolStates.BUILT_APK, 100 }
        };
    }

    public static class APKEditorStates
    {
        public const string MERGING_BASE = "Merging: base";
        public const string SPLIT_CONFIG_JA = "Merging: split_config.ja";
        public const string SPLIT_CONFIG_XXHDPI = "Merging: split_config.xxhdpi";
        public const string SPLIT_CONIG_ARM64 = "Merging: split_config.arm64_v8a";
        public const string SPLIT_CONFIG_EN = "Merging: split_config.en";
        public const string SPLIT_ASSET_PACK = "Merging: split_asset_pack_install_time";
        public const string SIGNATURE_BLOCK = "Writing signature block";
        public const string SAVED_TO = "Saved to:";
    }

    public static class APKEditorDictionary
    {
        public static readonly Dictionary<string, int> _progressMapType5 = new Dictionary<string, int>
        {
            { APKEditorStates.MERGING_BASE, 13 },
            { APKEditorStates.SPLIT_CONFIG_JA, 25 },
            { APKEditorStates.SPLIT_CONFIG_XXHDPI, 37 },
            { APKEditorStates.SPLIT_CONIG_ARM64, 49 },
            { APKEditorStates.SPLIT_CONFIG_EN, 62 },
            { APKEditorStates.SPLIT_ASSET_PACK, 76 },
            { APKEditorStates.SIGNATURE_BLOCK, 88 },
            { APKEditorStates.SAVED_TO, 100 }
        };
    }

    public static class UberSignerStates
    {
        public const string KEYSTORE = "keystore:";
        public const string BUILED_MERGED = "01. Build_merged.apk";
        public const string SIGN = "SIGN";
        public const string FILE = "Build_merged.apk";
        public const string ZIP_ALIGNED = "zipalign success";
        public const string SIGN_SUCCESFUL = "sign success";
        public const string VERIFY = "VERIFY";
        public const string FILE_SIG = "Build_merged-aligned-debugSigned.apk";
        public const string ZIP_ALIGNED_VER = "zipalign verified";
        public const string SIGN_VER = "sign verified";
        public const string SUCCESFUL = "Successfully processed 1 APKs";
    }

    public static class UberSignerDictionary
    {
        public static readonly Dictionary<string, int> _progressMapType6 = new Dictionary<string, int>
        {
            { UberSignerStates.KEYSTORE, 9 },
            { UberSignerStates.BUILED_MERGED, 13 },
            { UberSignerStates.SIGN, 24 },
            { UberSignerStates.FILE, 31 },
            { UberSignerStates.ZIP_ALIGNED, 38 },
            { UberSignerStates.SIGN_SUCCESFUL, 44 },
            { UberSignerStates.VERIFY, 50 },
            { UberSignerStates.FILE_SIG, 61 },
            { UberSignerStates.ZIP_ALIGNED_VER, 69},
            { UberSignerStates.SIGN_VER, 78},
            { UberSignerStates.SUCCESFUL, 100}
        };
    }
}
