﻿#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;

namespace FMODUnity
{
    public static class CodeGeneration {
        public static void GenerateStaticPluginRegistration(string filePath, Platform platform,
            Action<string> reportError)
        {
            List<string> validatedPlugins = ValidateStaticPlugins(platform.StaticPlugins, reportError);

            using (StreamWriter file = new StreamWriter(filePath))
            {
                WriteStaticPluginRegistration(file, platform.IsFMODStaticallyLinked, validatedPlugins);
            }
        }

        private static void WriteStaticPluginRegistration(StreamWriter file, bool isFMODStaticallyLinked,
            IEnumerable<string> pluginFunctions)
        {
            file.WriteLine("// This file was generated by FMOD for Unity from the Static Plugins list in the FMOD settings.");
            file.WriteLine();

            file.WriteLine("// These macros control the behaviour of {0}.", Settings.StaticPluginsSupportHeader);

            file.WriteLine("#define FMOD_LINKAGE_STATIC {0}", isFMODStaticallyLinked ? 1 : 0);

#if UNITY_2020_1_OR_NEWER
            file.WriteLine("#define UNITY_2020_1_OR_NEWER 1");
#else
            file.WriteLine("#define UNITY_2020_1_OR_NEWER 0");
#endif

#if UNITY_2019_1_OR_NEWER
            file.WriteLine("#define UNITY_2019_1_OR_NEWER 1");
#else
            file.WriteLine("#define UNITY_2019_1_OR_NEWER 0");
#endif

            file.WriteLine();
            file.WriteLine("#include \"{0}\"", Settings.StaticPluginsSupportHeader);
            file.WriteLine();

            // Declare the extern functions
            foreach (string pluginFunction in pluginFunctions)
            {
                file.WriteLine("extern \"C\" FMOD_DSP_DESCRIPTION* DEFAULT_CALL {0}();", pluginFunction);
            }

            file.WriteLine();

            file.WriteLine("extern \"C\" unsigned int DEFAULT_CALL {0}(const char *coreLibraryName, FMOD_SYSTEM *system)",
                Platform.RegisterStaticPluginsFunctionName);
            file.WriteLine("{");
            file.WriteLine("    InitializeRegisterDSP(coreLibraryName);");
            file.WriteLine();
            file.WriteLine("    unsigned int result = 0;");

            foreach (string pluginFunction in pluginFunctions)
            {
                file.WriteLine("    result = sRegisterDSP(system, {0}(), nullptr);", pluginFunction);
                file.WriteLine("    if (result != 0)");
                file.WriteLine("    {");
                file.WriteLine("        return result;");
                file.WriteLine("    }");
                file.WriteLine();
            }

            file.WriteLine("    return result;");
            file.WriteLine("}");
        }

        private static List<string> ValidateStaticPlugins(List<string> staticPlugins, Action<string> reportError)
        {
            List<string> result = new List<string>();

            for (int i = 0; i < staticPlugins.Count; ++i)
            {
                string functionName = staticPlugins[i];

                string trimmedName = (functionName != null) ? functionName.Trim() : null;

                if (string.IsNullOrEmpty(trimmedName))
                {
                    reportError(string.Format("Static plugin {0} has no name and will be ignored.", i + 1));
                }
                else if (IsValidFunctionName(trimmedName, reportError))
                {
                    result.Add(trimmedName);
                }
            }

            return result;
        }

        private static bool IsValidFunctionName(string name, Action<string> reportError)
        {
            if (!(char.IsLetter(name[0]) || name[0] == '_'))
            {
                reportError(string.Format(
                    "Plugin name '{0}' is not valid. Names must start with a letter or an underscore ('_').", name));
                return false;
            }

            for (int i = 1; i < name.Length; ++i)
            {
                if (!(char.IsLetterOrDigit(name[i]) || name[i] == '_'))
                {
                    reportError(string.Format(
                        "Plugin name '{0}' is not valid. " +
                        "Character '{1}' at position {2} is invalid - it must be a letter, a number, or an underscore ('_').",
                        name, name[i], i));
                    return false;
                }
            }

            return true;
        }
    }
}
#endif
