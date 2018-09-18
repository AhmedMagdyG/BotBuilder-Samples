using Microsoft.Bot.Builder.AI.Translation;
using Microsoft.Bot.Builder.AI.Translation.PostProcessor;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace LuisBot.Controllers
{
    public class TranslatorController
    {
        private ITranslator translator;
        public TranslatorController()
        {
            translator = new Translator(WebConfigurationManager.AppSettings["translatorKey"]);
        }

        public async Task TranslateActivityText(Activity activity, string targetLanguage)
        {
            string sourceLanguage = await DetectLanguageAsync(activity.Text);
            ITranslatedDocument translatedDocument = await TranslateAsync(activity.Text, sourceLanguage, targetLanguage);
            activity.Text = translatedDocument.GetTranslatedMessage();
        }
        
        private async Task<string> DetectLanguageAsync(string textToDetect)
        {
            return await translator.DetectAsync(textToDetect);
        }

        private async Task<ITranslatedDocument> TranslateAsync(string textToTrasnlate, string from, string to)
        {
            return await translator.TranslateAsync(textToTrasnlate, from, to);
        }

        private async Task<List<ITranslatedDocument>> TranslateArrayAsync(string[] textToTrasnlate, string from, string to)
        {
            return await translator.TranslateArrayAsync(textToTrasnlate, from, to);
        }

    }
}