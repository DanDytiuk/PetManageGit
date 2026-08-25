using PetManage.Models;
using SQLite;

namespace PetManage.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public async Task InitializeAsync()
        {
            if (_database != null)
                return;

            string path = Path.Combine(
                FileSystem.AppDataDirectory,
                "PetManage.db");

            _database = new SQLiteAsyncConnection(path);

            _ = await _database.CreateTableAsync<SettingsModel>();
            _ = await _database.CreateTableAsync<ProfileModel>();
            _ = await _database.CreateTableAsync<FoodModel>();
            _ = await _database.CreateTableAsync<AnimalTypeModel>();
            _ = await _database.CreateTableAsync<BreedModel>();
            _ = await _database.CreateTableAsync<AppetiteModel>();

            await InitializeAnimalTypesAsync();
            await InitializeAppetiteTypesAsync();
        }

        #region GetDataAsync

        public async Task<SettingsModel> GetSettingsAsync()
        {
            await InitializeAsync();

            var settings = await _database.Table<SettingsModel>()
                    .FirstOrDefaultAsync();

            if (settings != null) return settings;

            settings = new SettingsModel();

            _ = await _database.InsertAsync(settings);

            return settings;
        }

        public async Task<List<FoodModel>> GetFoodAsync()
        {
            await InitializeAsync();

            return await _database.Table<FoodModel>()
                                  .OrderByDescending(x => x.DateOfEat)
                                  .ToListAsync();
        }

        public async Task<List<AnimalTypeModel>> GetAnimalTypesAsync()
        {
            return await _database
                .Table<AnimalTypeModel>()
                .ToListAsync();
        }

        public async Task<List<BreedModel>> GetBreedsByAnimalTypeAsync(int animalTypeId)
        {
            return await _database
                .Table<BreedModel>()
                .Where(x => x.AnimalID == animalTypeId)
                .ToListAsync();
        }

        public async Task InitializeAnimalTypesAsync()
        {
            var count = await _database.Table<AnimalTypeModel>().CountAsync();

            if (count > 0)
                return;

            var AnimalTypes = new List<AnimalTypeModel>
            {
                new() {LocalizationCode = "AnimalType_Cat"},
                new() {LocalizationCode = "AnimalType_Dog"},
                new() {LocalizationCode = "AnimalType_Hamster"},
                new() {LocalizationCode = "AnimalType_Rabbit"},
                new() {LocalizationCode = "AnimalType_Parrot"},
                new() {LocalizationCode = "AnimalType_Turtle"},
                new() {LocalizationCode = "AnimalType_Fox"},
                new() {LocalizationCode = "AnimalType_Lizard" },
                new() {LocalizationCode = "AnimalType_Spyder"},
                new() {LocalizationCode = "AnimalType_Snake" },
                new() {LocalizationCode = "AnimalType_Other" }
            };

            _ = await _database.InsertAllAsync(AnimalTypes);
        }

        public async Task InitializeAppetiteTypesAsync()
        {

            var count = await _database.Table<AppetiteModel>().CountAsync();

            if (count > 0)
                return;

            var appetiteTypes = new List<AppetiteModel>
            {
                new() {LocalizationCode = "Appetite_Beautiful"},
                new() {LocalizationCode = "Appetite_Normal"},
                new() {LocalizationCode = "Appetite_Good"},
                new() {LocalizationCode = "Appetite_Bad"},
                new() {LocalizationCode = "Appetite_VeryBad"}
            };

            _ = await _database.InsertAllAsync(appetiteTypes);
        }

        public async Task InitializeBreedsAsync()
        {
            var count = await _database.Table<BreedModel>().CountAsync();

            if (count > 0) return;

            _ = new List<BreedModel>
            {
                new() {AnimalID = 1, LocalizationCode = "Breed_Abyssinan"},
                new() {AnimalID = 1, LocalizationCode = "Breed_AustralianSmoke"},
                new() {AnimalID = 1, LocalizationCode = "Breed_AsianTabby"},
                new() {AnimalID = 1, LocalizationCode = "Breed_AmericanLongHair"},
                new() {AnimalID = 1, LocalizationCode = "Breed_AmericanWireHair"},
                new() {AnimalID = 1, LocalizationCode = "Breed_AmericanShortHair"},
                new() {AnimalID = 1, LocalizationCode = "Breed_AmericanBobtail"},
                new() {AnimalID = 1, LocalizationCode = "Breed_AmericanCurl"},
                new() {AnimalID = 1, LocalizationCode = "Breed_AnatolianCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_ArabianMau"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Asherah"},
                new() {AnimalID = 1, LocalizationCode = "Breed_BalineseCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Bambino"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Bengal"},
                new() {AnimalID = 1, LocalizationCode = "Breed_BurmeseCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Bombay"},
                new() {AnimalID = 1, LocalizationCode = "Breed_BrasilianShortHair"},
                new() {AnimalID = 1, LocalizationCode = "Breed_BritainLongHair"},
                new() {AnimalID = 1, LocalizationCode = "Breed_BritainShortHair"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Burma"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Burmilla"},
                new() {AnimalID = 1, LocalizationCode = "Breed_HavanaBraun"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Himalai"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Dwelf"},
                new() {AnimalID = 1, LocalizationCode = "Breed_DevonRex"},
                new() {AnimalID = 1, LocalizationCode = "Breed_DonSphynx"},
                new() {AnimalID = 1, LocalizationCode = "Breed_EuropeanShortHair"},
                new() {AnimalID = 1, LocalizationCode = "Breed_EgyptianMau"},
                new() {AnimalID = 1, LocalizationCode = "Breed_YorkieChocCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_CaliforniaShiningCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Canaan"},
                new() {AnimalID = 1, LocalizationCode = "Breed_CanadianSphynx"},
                new() {AnimalID = 1, LocalizationCode = "Breed_KaoMani"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Caracal"},
                new() {AnimalID = 1, LocalizationCode = "Breed_KarelianBobtail"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Cymric"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Korat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_CornishRex"},
                new() {AnimalID = 1, LocalizationCode = "Breed_KurilianBobtail"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Laperm"},
                new() {AnimalID = 1, LocalizationCode = "Breed_LeeHua"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Likoy"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Mandalay"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Munchkin"},
                new() {AnimalID = 1, LocalizationCode = "Breed_MaineCoon"},
                new() {AnimalID = 1, LocalizationCode = "Breed_MekongBobtail"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Minskin"},
                new() {AnimalID = 1, LocalizationCode = "Breed_ManxCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Napoleon"},
                new() {AnimalID = 1, LocalizationCode = "Breed_NevaMasqueradeCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_GermanRex"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Nibelung"},
                new() {AnimalID = 1, LocalizationCode = "Breed_NorwegianForestCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_OregonRex"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Oriental"},
                new() {AnimalID = 1, LocalizationCode = "Breed_OjosAzules"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Ocicat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_PersianCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_PetersburgSphinx"},
                new() {AnimalID = 1, LocalizationCode = "Breed_PixieBob"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Ragamuffin"},
                new() {AnimalID = 1, LocalizationCode = "Breed_RussianBlueCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Ragdoll"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Savannah"},
                new() {AnimalID = 1, LocalizationCode = "Breed_SeychellsCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_SelkirkRex"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Serengeti"},
                new() {AnimalID = 1, LocalizationCode = "Breed_SiameseCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_SiberianCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_SingapuraCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_SnowShoe"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Sokoke"},
                new() {AnimalID = 1, LocalizationCode = "Breed_SomaliCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_ThaiCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Toybob"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Toyger"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Tonkinese"},
                new() {AnimalID = 1, LocalizationCode = "Breed_TurkishAngora"},
                new() {AnimalID = 1, LocalizationCode = "Breed_TurkishVan"},
                new() {AnimalID = 1, LocalizationCode = "Breed_UkrainianLevkoy"},
                new() {AnimalID = 1, LocalizationCode = "Breed_UralRex"},
                new() {AnimalID = 1, LocalizationCode = "Breed_ForeignWhite"},
                new() {AnimalID = 1, LocalizationCode = "Breed_HighlandFold"},
                new() {AnimalID = 1, LocalizationCode = "Breed_CeylonCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Chauzi"},
                new() {AnimalID = 1, LocalizationCode = "Breed_ChantillyTiffany"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Shartrez"},
                new() {AnimalID = 1, LocalizationCode = "Breed_ScottishFoldCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_ScottishStraightCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_AegeanCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_ExoticShorthairCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Elf"},
                new() {AnimalID = 1, LocalizationCode = "Breed_JavaneseCat"},
                new() {AnimalID = 1, LocalizationCode = "Breed_JapaneseBobtail"},
                new() {AnimalID = 1, LocalizationCode = "Breed_Dvornyaga"},

                new() {AnimalID = 2, LocalizationCode = "Breed_AustralianShepherd"},
                new() {AnimalID = 2, LocalizationCode = "Breed_AustralianKelpie"},
                new() {AnimalID = 2, LocalizationCode = "Breed_AustralianTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_AustralianHeeler"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Azawakh"},
                new() {AnimalID = 2, LocalizationCode = "Breed_AkitaInu"},
                new() {AnimalID = 2, LocalizationCode = "Breed_AlapahaBulldog"},
                new() {AnimalID = 2, LocalizationCode = "Breed_AlaskanMalamute"},
                new() {AnimalID = 2, LocalizationCode = "Breed_AmericanAkita"},
                new() {AnimalID = 2, LocalizationCode = "Breed_AmericanBandog"},
                new() {AnimalID = 2, LocalizationCode = "Breed_AmericanBulldog"},
                new() {AnimalID = 2, LocalizationCode = "Breed_AmericanWaterSpaniel"},
                new() {AnimalID = 2, LocalizationCode = "Breed_AmericanHairlessTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_AmericanCockerSpaniel"},
                new() {AnimalID = 2, LocalizationCode = "Breed_AmericanPitBullTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_AmericanStaffordshireTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_AmericanFoxhound"},
                new() {AnimalID = 2, LocalizationCode = "Breed_AmericanEskimoSpitz"},
                new() {AnimalID = 2, LocalizationCode = "Breed_AnatolianShepherd"},
                new() {AnimalID = 2, LocalizationCode = "Breed_EnglishBulldog"},
                new() {AnimalID = 2, LocalizationCode = "Breed_EnglishCockerSpaniel"},
                new() {AnimalID = 2, LocalizationCode = "Breed_EnglishPointer"},
                new() {AnimalID = 2, LocalizationCode = "Breed_EnglishSetter"},
                new() {AnimalID = 2, LocalizationCode = "Breed_EnglishSpringerSpaniel"},
                new() {AnimalID = 2, LocalizationCode = "Breed_EnglishToyTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_EnglishFoxhound"},
                new() {AnimalID = 2, LocalizationCode = "Breed_AppenzellerSennenhund"},
                new() {AnimalID = 2, LocalizationCode = "Breed_DogoArgentino"},
                new() {AnimalID = 2, LocalizationCode = "Breed_AfghanHound"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Affenpinscher"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Basenji"},
                new() {AnimalID = 2, LocalizationCode = "Breed_BassetHound"},
                new() {AnimalID = 2, LocalizationCode = "Breed_BedlingtonTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_WhiteSwissShepherd"},
                new() {AnimalID = 2, LocalizationCode = "Breed_BelgianShepherdGroenendael"},
                new() {AnimalID = 2, LocalizationCode = "Breed_BelgianShepherdLaekenois"},
                new() {AnimalID = 2, LocalizationCode = "Breed_BelgianMalinois"},
                new() {AnimalID = 2, LocalizationCode = "Breed_BelgianShepherdTervuren"},
                new() {AnimalID = 2, LocalizationCode = "Breed_BerneseMountainDog"},
                new() {AnimalID = 2, LocalizationCode = "Breed_BeaverYorkshireTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Beagle"},
                new() {AnimalID = 2, LocalizationCode = "Breed_BichonFrize"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Bloodhound"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Bobtail"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Boxer"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Bolognese"},
                new() {AnimalID = 2, LocalizationCode = "Breed_GreaterSwissMountainDog"},
                new() {AnimalID = 2, LocalizationCode = "Breed_BorderCollie"},
                new() {AnimalID = 2, LocalizationCode = "Breed_BorderrTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_DogueDeBordeaux"},
                new() {AnimalID = 2, LocalizationCode = "Breed_BeardedCollie"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Beauceron"},
                new() {AnimalID = 2, LocalizationCode = "Breed_BostonTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_BretonEpagnole"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Briard"},
                new() {AnimalID = 2, LocalizationCode = "Breed_BrusselsGriffon"},
                new() {AnimalID = 2, LocalizationCode = "Breed_BullyKutta"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Bullmastiff"},
                new() {AnimalID = 2, LocalizationCode = "Breed_BullTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_MiniatureBullTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_BuryatMongolianWolfhound"},
                new() {AnimalID = 2, LocalizationCode = "Breed_VendeenBassetGriffon"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Weimaraner"},
                new() {AnimalID = 2, LocalizationCode = "Breed_WelshCorgiCardigan"},
                new() {AnimalID = 2, LocalizationCode = "Breed_PembrokeWelshCorgi"},
                new() {AnimalID = 2, LocalizationCode = "Breed_WelshSpringerSpaniel"},
                new() {AnimalID = 2, LocalizationCode = "Breed_WelshTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_HungarianGreyhound"},
                new() {AnimalID = 2, LocalizationCode = "Breed_HungarianVizsla"},
                new() {AnimalID = 2, LocalizationCode = "Breed_WestGighlandTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_WolfDog"},
                new() {AnimalID = 2, LocalizationCode = "Breed_EastEuropeanShepherd"},
                new() {AnimalID = 2, LocalizationCode = "Breed_EastSiberianLaika"},
                new() {AnimalID = 2, LocalizationCode = "Breed_HavaneseBichon"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Gampr"},
                new() {AnimalID = 2, LocalizationCode = "Breed_SmoothFoxTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Greyhound"},
                new() {AnimalID = 2, LocalizationCode = "Breed_GriffonKortalsa"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Dalmatian"},
                new() {AnimalID = 2, LocalizationCode = "Breed_DandieDinmontTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_JackRussellTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Doberman"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Drathaar"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Euraiser"},
                new() {AnimalID = 2, LocalizationCode = "Breed_WestSiberianLaika"},
                new() {AnimalID = 2, LocalizationCode = "Breed_GoldenRetriever"},
                new() {AnimalID = 2, LocalizationCode = "Breed_IrishWaterSpaniel"},
                new() {AnimalID = 2, LocalizationCode = "Breed_IrishWolfhound"},
                new() {AnimalID = 2, LocalizationCode = "Breed_IrishRedSetter"},
                new() {AnimalID = 2, LocalizationCode = "Breed_IrishSoftCoatedWheatenTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_IrishTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_IrishGlenOfImaalTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_IcelandicDog"},
                new() {AnimalID = 2, LocalizationCode = "Breed_SpanishMastiff"},
                new() {AnimalID = 2, LocalizationCode = "Breed_YorkshireTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Cadebo"},
                new() {AnimalID = 2, LocalizationCode = "Breed_CavalierKingCharlesSpaniel"},
                new() {AnimalID = 2, LocalizationCode = "Breed_CaucasianShepherd"},
                new() {AnimalID = 2, LocalizationCode = "Breed_DogoCanario"},
                new() {AnimalID = 2, LocalizationCode = "Breed_CaneCorso"},
                new() {AnimalID = 2, LocalizationCode = "Breed_KarelianBearDog"},
                new() {AnimalID = 2, LocalizationCode = "Breed_MiniaturePinscher"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Keeshond"},
                new() {AnimalID = 2, LocalizationCode = "Breed_CairnTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_KerryBlueTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_ChineseCrestedDog"},
                new() {AnimalID = 2, LocalizationCode = "Breed_ClumberSpaniel"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Collie"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Commons"},
                new() {AnimalID = 2, LocalizationCode = "Breed_CotonDeTulear"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Kuvasz"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Kurzhaar"},
                new() {AnimalID = 2, LocalizationCode = "Breed_CurlyCoatedRetriever"},
                new() {AnimalID = 2, LocalizationCode = "Breed_LabradorRetriever"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Labradoodle"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Langhaar"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Landseer"},
                new() {AnimalID = 2, LocalizationCode = "Breed_ItalianGreyhound"},
                new() {AnimalID = 2, LocalizationCode = "Breed_LakelandTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Leonberger"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Louchen"},
                new() {AnimalID = 2, LocalizationCode = "Breed_LhasaApso"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Maltese"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Maltipu"},
                new() {AnimalID = 2, LocalizationCode = "Breed_ManchesterTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Mastiff"},
                new() {AnimalID = 2, LocalizationCode = "Breed_MexicanHairlessDog"},
                new() {AnimalID = 2, LocalizationCode = "Breed_MittelSchnauzer"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Pug"},
                new() {AnimalID = 2, LocalizationCode = "Breed_MoscowWatchDog"},
                new() {AnimalID = 2, LocalizationCode = "Breed_NeapolitainMastiff"},
                new() {AnimalID = 2, LocalizationCode = "Breed_GermanShepherd"},
                new() {AnimalID = 2, LocalizationCode = "Breed_GreatDane"},
                new() {AnimalID = 2, LocalizationCode = "Breed_GermanPinscher"},
                new() {AnimalID = 2, LocalizationCode = "Breed_GriffonDeNivernaise"},
                new() {AnimalID = 2, LocalizationCode = "Breed_NovaScotiaDuckRetriever"},
                new() {AnimalID = 2, LocalizationCode = "Breed_NorwegianBuhund"},
                new() {AnimalID = 2, LocalizationCode = "Breed_NorwegianElkhound"},
                new() {AnimalID = 2, LocalizationCode = "Breed_NorwichTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_NorfolkTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Newfoundland"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Otterhound"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Pig"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Papillon"},
                new() {AnimalID = 2, LocalizationCode = "Breed_ParsonRussellTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Pekingese"},
                new() {AnimalID = 2, LocalizationCode = "Breed_PeruvianHairlessDog"},
                new() {AnimalID = 2, LocalizationCode = "Breed_PyreneanShepherd"},
                new() {AnimalID = 2, LocalizationCode = "Breed_PyreneanMastiff"},
                new() {AnimalID = 2, LocalizationCode = "Breed_IbizanHound"},
                new() {AnimalID = 2, LocalizationCode = "Breed_PolishLowlandSheepDog"},
                new() {AnimalID = 2, LocalizationCode = "Breed_PolishPodgalianShepherdDog"},
                new() {AnimalID = 2, LocalizationCode = "Breed_PomeranianSpitz"},
                new() {AnimalID = 2, LocalizationCode = "Breed_PortugueseWaterDog"},
                new() {AnimalID = 2, LocalizationCode = "Breed_PragueKnight"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Poodle"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Puli"},
                new() {AnimalID = 2, LocalizationCode = "Breed_RafeiraDoAlentejo"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Riesenschnauzer"},
                new() {AnimalID = 2, LocalizationCode = "Breed_RhodesianRidgeback"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Rottweiler"},
                new() {AnimalID = 2, LocalizationCode = "Breed_RussianToy"},
                new() {AnimalID = 2, LocalizationCode = "Breed_BlackRussianTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_RussianEuropeanLaika"},
                new() {AnimalID = 2, LocalizationCode = "Breed_RatTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Saluki"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Samoyed"},
                new() {AnimalID = 2, LocalizationCode = "Breed_SussexSpaniel"},
                new() {AnimalID = 2, LocalizationCode = "Breed_SaintBernard"},
                new() {AnimalID = 2, LocalizationCode = "Breed_ShibaInu"},
                new() {AnimalID = 2, LocalizationCode = "Breed_SealyhamTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_SkyeTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_ScotchTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_SlovakWatchman"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Slugi"},
                new() {AnimalID = 2, LocalizationCode = "Breed_CentralAsianShepherdDog"},
                new() {AnimalID = 2, LocalizationCode = "Breed_StaffordshireBullTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Taigan"},
                new() {AnimalID = 2, LocalizationCode = "Breed_ThaiRidgeback"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Fee"},
                new() {AnimalID = 2, LocalizationCode = "Breed_TibetanMastiff"},
                new() {AnimalID = 2, LocalizationCode = "Breed_TibetanSpaniel"},
                new() {AnimalID = 2, LocalizationCode = "Breed_TibetanTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_TosaInu"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Whippet"},
                new() {AnimalID = 2, LocalizationCode = "Breed_PharaohHound"},
                new() {AnimalID = 2, LocalizationCode = "Breed_FilaBrasileiro"},
                new() {AnimalID = 2, LocalizationCode = "Breed_FinnishLaika"},
                new() {AnimalID = 2, LocalizationCode = "Breed_FlatRetriever"},
                new() {AnimalID = 2, LocalizationCode = "Breed_FrenchBulldog"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Harrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Husky"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Hovawart"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Zvergschnauzer"},
                new() {AnimalID = 2, LocalizationCode = "Breed_ChowChow"},
                new() {AnimalID = 2, LocalizationCode = "Breed_CzechoslovakianWolfdog"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Chinook"},
                new() {AnimalID = 2, LocalizationCode = "Breed_CirnecoDelEtna"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Chihuahua"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Chongqing"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Sharpey"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Sheltie"},
                new() {AnimalID = 2, LocalizationCode = "Breed_ShihTzu"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Schipperke"},
                new() {AnimalID = 2, LocalizationCode = "Breed_ScottishGreyhound"},
                new() {AnimalID = 2, LocalizationCode = "Breed_ScottishSetter"},
                new() {AnimalID = 2, LocalizationCode = "Breed_EntlebucherZennenhund"},
                new() {AnimalID = 2, LocalizationCode = "Breed_AiredaleTerrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_EstonianHound"},
                new() {AnimalID = 2, LocalizationCode = "Breed_SouthAfricanBoerboel"},
                new() {AnimalID = 2, LocalizationCode = "Breed_SouthRussianShepherd"},
                new() {AnimalID = 2, LocalizationCode = "Breed_Jagdterrier"},
                new() {AnimalID = 2, LocalizationCode = "Breed_JapaneseChin"},
                new() {AnimalID = 2, LocalizationCode = "Breed_JapaneseSpitz"},

                new() {AnimalID = 3, LocalizationCode = "Breed_DjungarianHamster"},
                new() {AnimalID = 3, LocalizationCode = "Breed_SyrianHamster"},
                new() {AnimalID = 3, LocalizationCode = "Breed_RoborovskiHamster"},
                new() {AnimalID = 3, LocalizationCode = "Breed_CampbellHamster"},
                new() {AnimalID = 3, LocalizationCode = "Breed_TaylorHamster"},
                new() {AnimalID = 3, LocalizationCode = "Breed_BarabinskiyHamster"},
                new() {AnimalID = 3, LocalizationCode = "Breed_Albinos"},
                new() {AnimalID = 3, LocalizationCode = "Breed_ChineseHamster"},
                new() {AnimalID = 3, LocalizationCode = "Breed_CommonsHamster"},

                new() {AnimalID = 4, LocalizationCode = "Breed_NetherlandDwarfRabbit"},
                new() {AnimalID = 4, LocalizationCode = "Breed_LionheadRabbit"},
                new() {AnimalID = 4, LocalizationCode = "Breed_LopearedDwarfRam"},
                new() {AnimalID = 4, LocalizationCode = "Breed_Germelin"},
                new() {AnimalID = 4, LocalizationCode = "Breed_ColoredDwarf"},
                new() {AnimalID = 4, LocalizationCode = "Breed_DwarfRex"},
                new() {AnimalID = 4, LocalizationCode = "Breed_SatinDwarfRabbit"},
                new() {AnimalID = 4, LocalizationCode = "Breed_DwarfAngora"},
                new() {AnimalID = 4, LocalizationCode = "Breed_Minilop"},
                new() {AnimalID = 4, LocalizationCode = "Breed_Flandre"},
                new() {AnimalID = 4, LocalizationCode = "Breed_ViennaBlue"},
                new() {AnimalID = 4, LocalizationCode = "Breed_Baran"},
                new() {AnimalID = 4, LocalizationCode = "Breed_Rex"},
                new() {AnimalID = 4, LocalizationCode = "Breed_Satin"},

                new() {AnimalID = 5, LocalizationCode = "Breed_AlexandrianParrot"},
                new() {AnimalID = 5, LocalizationCode = "Breed_AmazonParrot"},
                new() {AnimalID = 5, LocalizationCode = "Breed_Aratinga"},
                new() {AnimalID = 5, LocalizationCode = "Breed_LorisParrots"},
                new() {AnimalID = 5, LocalizationCode = "Breed_Macaws"},
                new() {AnimalID = 5, LocalizationCode = "Breed_Budgerigars"},
                new() {AnimalID = 5, LocalizationCode = "Breed_CockatooParrots"},
                new() {AnimalID = 5, LocalizationCode = "Breed_CorellaParrots"},
                new() {AnimalID = 5, LocalizationCode = "Breed_Lovebirds"},
                new() {AnimalID = 5, LocalizationCode = "Breed_Parakeets"},
                new() {AnimalID = 5, LocalizationCode = "Breed_RosellaParrots"},
                new() {AnimalID = 5, LocalizationCode = "Breed_GrayParrot"},
                new() {AnimalID = 5, LocalizationCode = "Breed_CockatielParrot"},
                new() {AnimalID = 5, LocalizationCode = "Breed_KalitaParrot"},
                
                new() {AnimalID = 6, LocalizationCode = "Breed_RedEaredTurtle"},
                new() {AnimalID = 6, LocalizationCode = "Breed_TrionicsChineseTurtle"},
                new() {AnimalID = 6, LocalizationCode = "Breed_EuropeanMarshTurtle"},
                new() {AnimalID = 6, LocalizationCode = "Breed_MuskTurtle"},
                new() {AnimalID = 6, LocalizationCode = "Breed_CentralAsianTurtle"},
                new() {AnimalID = 6, LocalizationCode = "Breed_SnappingTurtle"},
                new() {AnimalID = 6, LocalizationCode = "Breed_FringedTurtle"},
                new() {AnimalID = 6, LocalizationCode = "Breed_LoggerheadTurtle"},
                new() {AnimalID = 6, LocalizationCode = "Breed_StarTurtle"},
                new() {AnimalID = 6, LocalizationCode = "Breed_CaspianTurtle"},
                new() {AnimalID = 6, LocalizationCode = "Breed_YellowEaredTurtle"},
                new() {AnimalID = 6, LocalizationCode = "Breed_OrnamentedTurtle"},
                new() {AnimalID = 6, LocalizationCode = "Breed_PigNosedTurtle"},

                new() {AnimalID = 7, LocalizationCode = "Breed_HomelessFox"},
                new() {AnimalID = 7, LocalizationCode = "Breed_Fenech"},

                new() {AnimalID = 8, LocalizationCode = "Breed_LeopardGecko"},
                new() {AnimalID = 8, LocalizationCode = "Breed_BananaEater"},
                new() {AnimalID = 8, LocalizationCode = "Breed_BeardedDragon"},

                new() {AnimalID = 9, LocalizationCode = "Breed_WhiteHairedTarantula"},
                new() {AnimalID = 9, LocalizationCode = "Breed_GiantTarantula"},
                new() {AnimalID = 9, LocalizationCode = "Breed_BicolorTarantula"},
                new() {AnimalID = 9, LocalizationCode = "Breed_MexicanRedLeggedTarantula"},
                new() {AnimalID = 9, LocalizationCode = "Breed_StripedTarantula"},
                new() {AnimalID = 9, LocalizationCode = "Breed_HornedTarantula"},
                new() {AnimalID = 9, LocalizationCode = "Breed_BlueTarantula"},
                new() {AnimalID = 9, LocalizationCode = "Breed_ChromeTarantula"},


                new() {AnimalID = 10, LocalizationCode = "Breed_CornSnake"},
                new() {AnimalID = 10, LocalizationCode = "Breed_MilkSnake"},
            };
        }

        #endregion

        #region SaveAsync

        public async Task SaveSettingsAsync(SettingsModel settings)
        {
            await InitializeAsync();

            _ = await _database.InsertOrReplaceAsync(settings);
        }

        public async Task<int> SaveFoodAsync(FoodModel food)
        {
            await InitializeAsync();
            return await _database.InsertAsync(food);
        }

        #endregion
    }
}
