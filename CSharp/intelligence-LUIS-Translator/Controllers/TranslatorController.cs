using Microsoft.Bot.Builder.AI.Translation;
using Microsoft.Bot.Builder.AI.Translation.PostProcessor;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace LuisBot.Controllers
{
    public class TranslatorController
    {
        private string configPath = "~\\Dialogs\\HotelReservation\\";
        private ITranslator translator;

        public TranslatorController()
        {
            translator = new Translator(WebConfigurationManager.AppSettings["translatorKey"], GetPatterns(), GetCusomDictionary());
        }

        public async Task TranslateActivityText(Activity activity, string targetLanguage)
        {
            string sourceLanguage = await DetectLanguageAsync(activity.Text).ConfigureAwait(false);
            ITranslatedDocument translatedDocument = await TranslateAsync(activity.Text, sourceLanguage, targetLanguage).ConfigureAwait(false);
            activity.Text = translatedDocument.GetTranslatedMessage();
        }
        
        private async Task<string> DetectLanguageAsync(string textToDetect)
        {
            return await translator.DetectAsync(textToDetect).ConfigureAwait(false);
        }

        private async Task<ITranslatedDocument> TranslateAsync(string textToTrasnlate, string from, string to)
        {
            return await translator.TranslateAsync(textToTrasnlate, from, to).ConfigureAwait(false);
        }

        private async Task<List<ITranslatedDocument>> TranslateArrayAsync(string[] textToTrasnlate, string from, string to)
        {
            return await translator.TranslateArrayAsync(textToTrasnlate, from, to).ConfigureAwait(false);
        }

        private Dictionary<string, List<string>> GetPatterns()
        {
            string path = HttpContext.Current.Server.MapPath(configPath) + "patterns.json";
            var json = File.ReadAllText(path);
            var patterns = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(json);
            return patterns;
        }

        private CustomDictionary GetCusomDictionary()
        {
            string path = HttpContext.Current.Server.MapPath(configPath) + "dictionary.json";
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