This sample shows how to integrate LUIS and translation to a bot with ASP.Net Core 2.

[![Deploy to Azure](https://azuredeploy.net/deploybutton.png)](https://azuredeploy.net/)

## To try this sample
- Clone the repository
```bash
git clone https://github.com/Microsoft/botbuilder-dotnet.git
```
### [Required] Getting set up with LUIS.ai model
- Navigate to [LUIS Portal](http://luis.ai)
- Click the `Sign in` button
- Click on `My apps` button
- Click on `Import new app`
- Click on the `Choose File` and select [HotelReservation.json](HotelReservation.json) from the `botbuilder-samples\samples\csharp_dotnetcore\25.multilingual-luis-bot\Dialogs\HotelReservation\` folder.
- Get ModelId, SubscriptionKey, and EndPoint. You can find this information under "Publish" tab for your LUIS application. For example, for
	- EndPoint = https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/XXXXXXXXXXXXX?subscription-key=YYYYYYYYYYYY&verbose=true&timezoneOffset=0&q= 
    - AppId = XXXXXXXXXXXXX
    - SubscriptionKey = YYYYYYYYYYYY
- Get translator api key from [here](https://www.microsoft.com/en-us/translator/)
- Update [botconfig.json](botconfig.json) file with your LuisModelId, SubscriptionKey, EndPoint, and TranslatorKey


### (Optional) Install LUDown
- (Optional) Install the LUDown [here](https://github.com/Microsoft/botbuilder-tools/tree/master/packages/LUDown) to help describe language understanding components for your bot.

### (Optional) Install Chatdown
- (Optional) Install the Chatdown [here](https://github.com/Microsoft/botbuilder-tools/tree/master/packages/Chatdown) to generate a .transcript file from a .chat file to generate mock transcripts.

### Visual studio
- Navigate to the `botbuilder-samples\samples\csharp_dotnetcore\25.multilingual-luis-bot\` and open MultilingualLuisBot.csproj in visual studio
- Hit F5

## Visual Studio Code
- Open `botbuilder-samples\samples\csharp_dotnetcore\25.multilingual-luis-bot\` sample folder.
- Bring up a terminal, navigate to BotBuilder-Samples\25.multilingual-luis-bot folder
- type 'dotnet run'

## Testing the bot using Bot Framework Emulator
[Microsoft Bot Framework Emulator](https://github.com/microsoft/botframework-emulator) is a desktop application that allows bot developers to test and debug their bots on localhost or running remotely through a tunnel.

- Install the Bot Framework emulator from [here](https://github.com/Microsoft/BotFramework-Emulator/releases)

### Connect to bot using Bot Framework Emulator **V4**
- Launch the Bot Framework Emulator
- File -> Open bot and navigate to `botbuilder-samples\samples\csharp_dotnetcore\25.multilingual-luis-bot\` folder
- Select [LuisBot.bot](LuisBot.bot) file

# Deploy this bot to Azure
You can use the [MSBot](https://github.com/microsoft/botbuilder-tools) Bot Builder CLI tool to clone and configure any services this sample depends on. 

To install all Bot Builder tools - 

Ensure you have [Node.js](https://nodejs.org/) version 8.5 or higher

```bash
npm i -g msbot chatdown ludown qnamaker luis-apis botdispatch luisgen
```
To clone this bot, run
```
msbot clone services -f deploymentScripts/msbotClone -n <BOT-NAME> -l <Azure-location> --subscriptionId <Azure-subscription-id>
```

# LUIS
Language Understanding service (LUIS) allows your application to understand what a person wants in their own words. LUIS uses machine learning to allow developers to build applications that can receive user input in natural language and extract meaning from it.

# Bot Translator
Bot translator allows your application to support multiple languages without an explicit need to train different language understanding models for each language.

# Further reading

- [Azure Bot Service Introduction](https://docs.microsoft.com/en-us/azure/bot-service/bot-service-overview-introduction?view=azure-bot-service-4.0)
- [Bot State](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-storage-concept?view=azure-bot-service-4.0)
- [LUIS documentation](https://docs.microsoft.com/en-us/azure/cognitive-services/LUIS/)
- [Bot Translator](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-howto-translation?view=azure-bot-service-4.0&tabs=cs)
- [LUDown](https://github.com/Microsoft/botbuilder-tools/tree/master/packages/Ludown)
- [Chatdown](https://github.com/Microsoft/botbuilder-tools/tree/master/packages/Chatdown)