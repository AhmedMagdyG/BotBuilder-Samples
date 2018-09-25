// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder.Ai.LUIS;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Bot.Builder.AI.Translation;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Microsoft.Bot.Samples.Ai.Luis.Translator
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBot<LuisTranslatorBot>(options =>
            {
                options.CredentialProvider = new ConfigurationCredentialProvider(Configuration);
                ConfigureBot(out var luisModelId, out var luisSubscriptionKey, out var luisEndPoint, out var translatorKey);
                var luisApplication = new LuisApplication(luisModelId, luisSubscriptionKey, luisEndPoint);
                var luisRecognizer = new LuisRecognizer(luisApplication);
                var middleware = options.Middleware;
                middleware.Add(new TranslationMiddleware(new string[] { "en" }, translatorKey, GetPatterns(), GetCusomDictionary()));
                middleware.Add(new LuisRecognizerMiddleware(luisRecognizer));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles()
                .UseStaticFiles()
                .UseBotFramework();
        }

        private void ConfigureBot(out string luisModelId, out string luisSubscriptionKey,out string luisEndPoint, out string translatorKey)
        {
            string path = Configuration.GetValue<string>("botConfigPath");
            var json = File.ReadAllText(path);
            dynamic content = JsonConvert.DeserializeObject(json);
            luisModelId = content.luisModelId;
            luisSubscriptionKey = content.luisSubscriptionKey;
            luisEndPoint = content.luisEndPoint;
            translatorKey = content.translatorKey;
        }

        private Dictionary<string, List<string>> GetPatterns()
        {
            string path = Configuration.GetValue<string>("patternsPath");
            var json = File.ReadAllText(path);
            var patterns = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(json);
            return patterns;
        }

        private CustomDictionary GetCusomDictionary()
        {
            string path = Configuration.GetValue<string>("dictionaryPath");
            var json = File.ReadAllText(path);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);
            CustomDictionary customDictionary = new CustomDictionary();
            foreach (KeyValuePair<string, Dictionary<string, string>> lang in dictionary)
            {
                customDictionary.AddNewLanguageDictionary(lang.Key, lang.Value);
            }

            return customDictionary;
        }
    }
}
