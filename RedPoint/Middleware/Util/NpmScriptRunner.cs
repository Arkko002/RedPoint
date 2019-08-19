// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNetCore.NodeServices.Npm.NpmScriptRunner
// Assembly: Microsoft.AspNetCore.SpaServices.Extensions, Version=2.1.1.0, Culture=neutral, PublicKeyToken=adb9793829ddae60
// MVID: EEC17521-E7E7-4594-AA4D-D358112C35D1
// Assembly location: C:\Program Files\dotnet\sdk\NuGetFallbackFolder\microsoft.aspnetcore.spaservices.extensions\2.1.1\lib\netstandard2.0\Microsoft.AspNetCore.SpaServices.Extensions.dll

using Microsoft.AspNetCore.NodeServices.Util;
using Microsoft.Extensions.Logging;
using RedPoint.Middleware.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Microsoft.AspNetCore.NodeServices.Npm
{
    /// <summary>
    /// Executes the <c>script</c> entries defined in a <c>package.json</c> file,
    /// capturing any output written to stdio.
    /// </summary>
    internal class NpmScriptRunner
    {
        private static Regex AnsiColorRegex = new Regex("\x001B\\[[0-9;]*m", RegexOptions.None, TimeSpan.FromSeconds(1.0));

        public EventedStreamReader StdOut { get; }

        public EventedStreamReader StdErr { get; }

        public NpmScriptRunner(
          string workingDirectory,
          string scriptName,
          string arguments,
          IDictionary<string, string> envVars)
        {
            if (string.IsNullOrEmpty(workingDirectory))
                throw new ArgumentException("Cannot be null or empty.", nameof(workingDirectory));
            if (string.IsNullOrEmpty(scriptName))
                throw new ArgumentException("Cannot be null or empty.", nameof(scriptName));
            string fileName = "npm";
            string str = string.Format("run {0} -- {1}", (object)scriptName, (object)(arguments ?? string.Empty));
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                fileName = "cmd";
                str = string.Format("/c npm {0}", (object)str);
            }
            ProcessStartInfo startInfo = new ProcessStartInfo(fileName)
            {
                Arguments = str,
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = workingDirectory
            };
            if (envVars != null)
            {
                foreach (KeyValuePair<string, string> envVar in (IEnumerable<KeyValuePair<string, string>>)envVars)
                    startInfo.Environment[envVar.Key] = envVar.Value;
            }
            Process process = NpmScriptRunner.LaunchNodeProcess(startInfo);
            this.StdOut = new EventedStreamReader(process.StandardOutput);
            this.StdErr = new EventedStreamReader(process.StandardError);
        }

        public void AttachToLogger(ILogger logger)
        {
            this.StdOut.OnReceivedLine += (EventedStreamReader.OnReceivedLineHandler)(line =>
            {
                if (string.IsNullOrWhiteSpace(line))
                    return;
                logger.LogInformation(NpmScriptRunner.StripAnsiColors(line));
            });
            this.StdErr.OnReceivedLine += (EventedStreamReader.OnReceivedLineHandler)(line =>
            {
                if (string.IsNullOrWhiteSpace(line))
                    return;
                logger.LogError(NpmScriptRunner.StripAnsiColors(line));
            });
            this.StdErr.OnReceivedChunk += (EventedStreamReader.OnReceivedChunkHandler)(chunk =>
            {
                if (Array.IndexOf<char>(chunk.Array, '\n', chunk.Offset, chunk.Count) >= 0)
                    return;
                Console.Write(chunk.Array, chunk.Offset, chunk.Count);
            });
        }

        private static string StripAnsiColors(string line)
        {
            return NpmScriptRunner.AnsiColorRegex.Replace(line, string.Empty);
        }

        private static Process LaunchNodeProcess(ProcessStartInfo startInfo)
        {
            try
            {
                Process process = Process.Start(startInfo);
                process.EnableRaisingEvents = true;
                return process;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to start 'npm'. To resolve this:.\n\n[1] Ensure that 'npm' is installed and can be found in one of the PATH directories.\n" + string.Format("    Current PATH enviroment variable is: {0}\n", (object)Environment.GetEnvironmentVariable("PATH")) + "    Make sure the executable is in one of those directories, or update your PATH.\n\n[2] See the InnerException for further details of the cause.", ex);
            }
        }
    }
}
