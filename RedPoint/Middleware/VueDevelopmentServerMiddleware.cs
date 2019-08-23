// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.NodeServices.Npm;
using Microsoft.AspNetCore.NodeServices.Util;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.AspNetCore.SpaServices.Util;
using Microsoft.Extensions.Logging;
using RedPoint.Middleware.Util;

namespace RedPoint.Middleware
{
    internal static class VueDevelopmentServerMiddleware
    {
        private const string LogCategoryName = "Microsoft.AspNetCore.SpaServices";
        private static TimeSpan RegexMatchTimeout = TimeSpan.FromSeconds(5); // This is a development-time only feature, so a very long timeout is fine

        public static void Attach(
            IApplicationBuilder appBuilder,
            string sourcePath,
            string npmScriptName)
        {
            if (string.IsNullOrEmpty(sourcePath))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(sourcePath));
            }

            if (string.IsNullOrEmpty(npmScriptName))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(npmScriptName));
            }

            var logger = LoggerFinder.GetOrCreateLogger(appBuilder, LogCategoryName);
            var portTask = StartVueDevServerAsync(appBuilder, sourcePath, npmScriptName, logger);

            // Everything we proxy is hardcoded to target http://localhost because:
            // - the requests are always from the local machine (we're not accepting remote
            //   requests that go directly to the create-react-app server)
            // - given that, there's no reason to use https, and we couldn't even if we
            //   wanted to, because in general the create-react-app server has no certificate
            var targetUriTask = portTask.ContinueWith(
                task => new UriBuilder("http", "localhost", task.Result).Uri);

            appBuilder.Use(async (context, next) =>
            {
                var timeout = TimeSpan.FromSeconds(60);
                await targetUriTask.WithTimeout(timeout,
                    $"The vue development server did not start listening for requests " +
                    $"within the timeout period of {timeout.Seconds} seconds. " +
                    $"Check the log output for error information.");

                await next();
            });

            appBuilder.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    var devServerUri = await targetUriTask;
                    context.Response.Redirect(devServerUri.ToString());
                }
                else
                {
                    await next();
                }
            });
        }

        private static async Task<int> StartVueDevServerAsync(
            IApplicationBuilder appBuilder,
            string sourcePath,
            string npmScriptName,
            ILogger logger)
        {
            var portNumber = TcpPortFinder.FindAvailablePort();
            logger.LogInformation($"Starting create-react-app server on port {portNumber}...");

            var addresses = appBuilder.ServerFeatures.Get<IServerAddressesFeature>().Addresses;
            var envVars = new Dictionary<string, string>
            {
                { "ASPNET_URL", addresses.Count > 0 ? addresses.First() : "" },
            };

            var npmScriptRunner = new NpmScriptRunner(
                sourcePath, npmScriptName, $"--port {portNumber} --host localhost", envVars);
            npmScriptRunner.AttachToLogger(logger);

            using (var stdErrReader = new EventedStreamStringReader(npmScriptRunner.StdErr))
            {
                try
                {
                    // Although the React dev server may eventually tell us the URL it's listening on,
                    // it doesn't do so until it's finished compiling, and even then only if there were
                    // no compiler warnings. So instead of waiting for that, consider it ready as soon
                    // as it starts listening for requests.
                    await npmScriptRunner.StdOut.WaitForMatch(
                        new Regex("DONE", RegexOptions.None, RegexMatchTimeout));
                }
                catch (EndOfStreamException ex)
                {
                    throw new InvalidOperationException(
                        $"The NPM script '{npmScriptName}' exited without indicating that the " +
                        $"create-react-app server was listening for requests. The error output was: " +
                        $"{stdErrReader.ReadAsString()}", ex);
                }
            }

            return portNumber;
        }
    }
}