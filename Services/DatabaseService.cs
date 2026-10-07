using PetManage.Models;
using PetManage.Models.ModelViewPicker;
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
            _ = await _database.CreateTableAsync<GenderModel>();
            _ = await _database.CreateTableAsync<FoodNames>();
            _ = await _database.CreateTableAsync<FoodSeriesName>();

            await InitializeAnimalTypesAsync();
            await InitializeAppetiteTypesAsync();
            await InitializeBreedsAsync();
            await InitializeGenderAsync();
            await InitializeNameOfFoodAsync();
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

        public async Task<ProfileModel> GetProfileAsync()
        {
            await InitializeAsync();

            var profile = await _database.Table<ProfileModel>().FirstOrDefaultAsync();

            if (profile != null) return profile;

            profile = new ProfileModel();

            _ = await _database.InsertAsync(profile);

            return profile;
        }

        public async Task DeleteProfileAsync(int petId)
        {
            await InitializeAsync();

            var profile = await _database.Table<ProfileModel>().FirstOrDefaultAsync(p => p.PetID == petId);
            
            if (profile != null)
            {
                await _database.DeleteAsync(profile);
            }
        }

        public async Task<ProfileModel?> GetProfileByIdAsync(int id)
        {
            if (id <= 0) return null;
            return await _database.Table<ProfileModel>()
                                  .Where(p => p.Id == id)
                                  .FirstOrDefaultAsync();
        }

        public async Task UpdateProfileAsync(int petId, string field, object value)
        {
            await InitializeAsync();

            var profile = await _database.Table<ProfileModel>()
                .FirstOrDefaultAsync(p => p.PetID == petId);

            if (profile == null)
                return;

            switch (field)
            {
                case "Name":
                    profile.Name = value?.ToString() ?? string.Empty;
                    break;
                case "Breed":
                    profile.Breed = value?.ToString() ?? string.Empty;
                    break;
                case "TypeOfPet":
                    profile.TypeOfPet = value?.ToString() ?? string.Empty;
                    break;
                case "Gender":
                    profile.Gender = value?.ToString() ?? string.Empty;
                    break;
                case "Weight":
                    if (value is double dv)
                        profile.Weight = dv;
                    else if (double.TryParse(value?.ToString(), out var parsedW))
                        profile.Weight = parsedW;
                    break;
                case "Birth":
                    if (value is DateTime dt)
                        profile.Birth = dt;
                    else if (DateTime.TryParse(value?.ToString(), out var parsedD))
                        profile.Birth = parsedD;
                    break;
                case "Notes":
                    profile.Notes = value?.ToString() ?? string.Empty;
                    break;
                default:
                    return;
            }

            await _database.UpdateAsync(profile);
        }

        public async Task<List<FoodModel>> GetFoodAsync()
        {
            await InitializeAsync();

            return await _database.Table<FoodModel>()
                                  .OrderByDescending(x => x.DateOfEat)
                                  .ToListAsync();
        }

        public async Task<List<FoodSeriesName>> GetFoodSeriesAsync(int foodId)
        {
            return await _database
                .Table<FoodSeriesName>()
                .Where(x => x.FoodID == foodId)
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

        public async Task<List<GenderModel>> GetGenderTypesAsync()
        {
            return await _database
                .Table<GenderModel>()
                .ToListAsync();
        }

        public async Task<List<FoodNames>> GetNameFoodsAsync()
        {
            return await _database
                .Table<FoodNames>()
                .ToListAsync();
        }

        #region Initialize Collections

        public async Task InitializeNameOfFoodAsync()
        {
            var count = await _database.Table<FoodNames>().CountAsync();

            if (count > 0)
                return;

            var foodNames = new List<FoodNames>
            {
                new() {FoodID = 1, LocalizationCode = "FoodName_1stChoice"},
                new() {FoodID = 2, LocalizationCode = "FoodName_ACANA"},
                new() {FoodID = 3, LocalizationCode = "FoodName_ARATON"},
                new() {FoodID = 4, LocalizationCode = "FoodName_AlphaSpirit"},
                new() {FoodID = 5, LocalizationCode = "FoodName_Amity"},
                new() {FoodID = 6, LocalizationCode = "FoodName_AnimAll"},
                new() {FoodID = 7, LocalizationCode = "FoodName_Animonda"},
                new() {FoodID = 8, LocalizationCode = "FoodName_BRAVERY"},
                new() {FoodID = 9, LocalizationCode = "FoodName_Bastteto"},
                new() {FoodID = 10, LocalizationCode = "FoodName_Beaphar"},
                new() {FoodID = 11, LocalizationCode = "FoodName_BonaCibo"},
                new() {FoodID = 12, LocalizationCode = "FoodName_BritCare"},
                new() {FoodID = 13, LocalizationCode = "FoodName_BritPremium"},
                new() {FoodID = 14, LocalizationCode = "FoodName_BritVD"},
                new() {FoodID = 15, LocalizationCode = "FoodName_Canina"},
                new() {FoodID = 16, LocalizationCode = "FoodName_Carnie"},
                new() {FoodID = 17, LocalizationCode = "FoodName_Carnilove" },
                new() {FoodID = 18, LocalizationCode = "FoodName_CarpathianPetFood"},
                new() {FoodID = 19, LocalizationCode = "FoodName_Catch!"},
                new() {FoodID = 20, LocalizationCode = "FoodName_Cherie"},
                new() {FoodID = 21, LocalizationCode = "FoodName_Chicopee"},
                new() {FoodID = 22, LocalizationCode = "FoodName_Club4Paws"},
                new() {FoodID = 23, LocalizationCode = "FoodName_Delickcious"},
                new() {FoodID = 24, LocalizationCode = "FoodName_Diamond"},
                new() {FoodID = 25, LocalizationCode = "FoodName_DolinaNoteciPremium"},
                new() {FoodID = 26, LocalizationCode = "FoodName_DolinaNoteciRafi" },
                new() {FoodID = 27, LocalizationCode = "FoodName_Eukanuba" },
                new() {FoodID = 28, LocalizationCode = "FoodName_Exclusion"},
                new() {FoodID = 29, LocalizationCode = "FoodName_Farmina"},
                new() {FoodID = 30, LocalizationCode = "FoodName_Felix"},
                new() {FoodID = 31, LocalizationCode = "FoodName_Gemon"},
                new() {FoodID = 32, LocalizationCode = "FoodName_Gheda"},
                new() {FoodID = 33, LocalizationCode = "FoodName_GimbornGimCat"},
                new() {FoodID = 34, LocalizationCode = "FoodName_GoldenCat"},
                new() {FoodID = 35, LocalizationCode = "FoodName_Gourmet"},
                new() {FoodID = 36, LocalizationCode = "FoodName_Half&Half"},
                new() {FoodID = 37, LocalizationCode = "FoodName_HappyCat"},
                new() {FoodID = 38, LocalizationCode = "FoodName_Hermos"},
                new() {FoodID = 39, LocalizationCode = "FoodName_Hills"},
                new() {FoodID = 40, LocalizationCode = "FoodName_Hiq"},
                new() {FoodID = 41, LocalizationCode = "FoodName_HomeFood"},
                new() {FoodID = 42, LocalizationCode = "FoodName_HowMeow"},
                new() {FoodID = 43, LocalizationCode = "FoodName_Lams"},
                new() {FoodID = 44, LocalizationCode = "FoodName_Josera"},
                new() {FoodID = 45, LocalizationCode = "FoodName_JosiCat"},
                new() {FoodID = 46, LocalizationCode = "FoodName_Kattovit"},
                new() {FoodID = 47, LocalizationCode = "FoodName_Kiani"},
                new() {FoodID = 48, LocalizationCode = "FoodName_Kitecat"},
                new() {FoodID = 49, LocalizationCode = "FoodName_Lara"},
                new() {FoodID = 50, LocalizationCode = "FoodName_LovelyHunter"},
                new() {FoodID = 51, LocalizationCode = "FoodName_Mera"},
                new() {FoodID = 52, LocalizationCode = "FoodName_Miamor"},
                new() {FoodID = 53, LocalizationCode = "FoodName_Monge"},
                new() {FoodID = 54, LocalizationCode = "FoodName_Morando"},
                new() {FoodID = 55, LocalizationCode = "FoodName_MyChampion"},
                new() {FoodID = 56, LocalizationCode = "FoodName_NaturalKitty"},
                new() {FoodID = 57, LocalizationCode = "FoodName_NaturesProtection"},
                new() {FoodID = 58, LocalizationCode = "FoodName_Nuevo"},
                new() {FoodID = 59, LocalizationCode = "FoodName_Oasy"},
                new() {FoodID = 60, LocalizationCode = "FoodName_Optimeal"},
                new() {FoodID = 61, LocalizationCode = "FoodName_Orijen"},
                new() {FoodID = 62, LocalizationCode = "FoodName_OvenBaked"},
                new() {FoodID = 63, LocalizationCode = "FoodName_Ownat"},
                new() {FoodID = 64, LocalizationCode = "FoodName_PureNurture"},
                new() {FoodID = 65, LocalizationCode = "FoodName_PurinaCatChow"},
                new() {FoodID = 66, LocalizationCode = "FoodName_PurinaFriskies"},
                new() {FoodID = 67, LocalizationCode = "FoodName_PurinaOne"},
                new() {FoodID = 68, LocalizationCode = "FoodName_PurinaProPlan"},
                new() {FoodID = 69, LocalizationCode = "FoodName_Quattro"},
                new() {FoodID = 70, LocalizationCode = "FoodName_ReflexPlus"},
                new() {FoodID = 71, LocalizationCode = "FoodName_Regis"},
                new() {FoodID = 72, LocalizationCode = "FoodName_RoyalCanin"},
                new() {FoodID = 73, LocalizationCode = "FoodName_Savory"},
                new() {FoodID = 74, LocalizationCode = "FoodName_Schesir"},
                new() {FoodID = 75, LocalizationCode = "FoodName_Sheba"},
                new() {FoodID = 76, LocalizationCode = "FoodName_TasteOfTheWild"},
                new() {FoodID = 77, LocalizationCode = "FoodName_ThankQ"},
                new() {FoodID = 78, LocalizationCode = "FoodName_Tow"},
                new() {FoodID = 79, LocalizationCode = "FoodName_Trendline"},
                new() {FoodID = 80, LocalizationCode = "FoodName_ThePet+"},
                new() {FoodID = 81, LocalizationCode = "FoodName_Versele-Laga"},
                new() {FoodID = 82, LocalizationCode = "FoodName_Vibrisse"},
                new() {FoodID = 83, LocalizationCode = "FoodName_Wanpy"},
                new() {FoodID = 84, LocalizationCode = "FoodName_Whiskas"},
                new() {FoodID = 85, LocalizationCode = "FoodName_WiseCat"},
                new() {FoodID = 86, LocalizationCode = "FoodName_GudFud"},
                new() {FoodID = 87, LocalizationCode = "FoodName_ForFriend"},
                new() {FoodID = 88, LocalizationCode = "FoodName_EcoGranula"},
                new() {FoodID = 89, LocalizationCode = "FoodName_Leopold"},
                new() {FoodID = 90, LocalizationCode = "FoodName_MyasnaMiska"},
                new() {FoodID = 91, LocalizationCode = "FoodName_Musya"},
                new() {FoodID = 92, LocalizationCode = "FoodName_Meow!"},
                new() {FoodID = 93, LocalizationCode = "FoodName_PanKot"},
                new() {FoodID = 94, LocalizationCode = "FoodName_Alice"},
                new() {FoodID = 95, LocalizationCode = "FoodName_Bavaro"},
                new() {FoodID = 96, LocalizationCode = "FoodName_Bosch"},
                new() {FoodID = 97, LocalizationCode = "FoodName_Brit"},
                new() {FoodID = 98, LocalizationCode = "FoodName_Chappi"},
                new() {FoodID = 99, LocalizationCode = "FoodName_DiamondNaturals"},
                new() {FoodID = 100, LocalizationCode = "FoodName_DibaqDiproteg"},
                new() {FoodID = 101, LocalizationCode = "FoodName_DolinaNoteciSuperfood"},
                new() {FoodID = 102, LocalizationCode = "FoodName_GoldenDog"},
                new() {FoodID = 103, LocalizationCode = "FoodName_GoodFriend"},
                new() {FoodID = 104, LocalizationCode = "FoodName_GreenPetfood"},
                new() {FoodID = 105, LocalizationCode = "FoodName_HappyDog"},
                new() {FoodID = 106, LocalizationCode = "FoodName_HappyLife"},
                new() {FoodID = 107, LocalizationCode = "FoodName_HappyOne"},
                new() {FoodID = 108, LocalizationCode = "FoodName_JuliusK9"},
                new() {FoodID = 109, LocalizationCode = "FoodName_Marpet"},
                new() {FoodID = 110, LocalizationCode = "FoodName_Nutrican"},
                new() {FoodID = 111, LocalizationCode = "FoodName_Orygo"},
                new() {FoodID = 112, LocalizationCode = "FoodName_Pedigree"},
                new() {FoodID = 113, LocalizationCode = "FoodName_PetChef"},
                new() {FoodID = 114, LocalizationCode = "FoodName_PetKind"},
                new() {FoodID = 115, LocalizationCode = "FoodName_Profine"},
                new() {FoodID = 116, LocalizationCode = "FoodName_PurinaDogChow"},
                new() {FoodID = 117, LocalizationCode = "FoodName_Rinti"},
                new() {FoodID = 118, LocalizationCode = "FoodName_Simba"},
                new() {FoodID = 119, LocalizationCode = "FoodName_TurboDog"},
                new() {FoodID = 120, LocalizationCode = "FoodName_Gav!"},
                new() {FoodID = 121, LocalizationCode = "FoodName_PanPes"},
                new() {FoodID = 122, LocalizationCode = "FoodName_Reks"},
            };

            _ = await _database.InsertAllAsync(foodNames);
        }

        public async Task InitializeFoodSeriesAsync()
        {
            var count = await _database.Table<FoodSeriesName>().CountAsync();

            if (count > 0) return;

            var foodseries = new List<FoodSeriesName>
            {
                new() {FoodID = 1, LocalizationCode = "FoodSeries_1stChoice"},
                new() {FoodID = 2, LocalizationCode = "FoodSeries_ACANA"},
                new() {FoodID = 3, LocalizationCode = "FoodSeries_ARATON"},
                new() {FoodID = 4, LocalizationCode = "FoodSeries_AlphaSpirit"},
                new() {FoodID = 5, LocalizationCode = "FoodSeries_Amity"},
                new() {FoodID = 6, LocalizationCode = "FoodSeries_AnimAll"},
                new() {FoodID = 7, LocalizationCode = "FoodSeries_Animonda"},
                new() {FoodID = 8, LocalizationCode = "FoodSeries_BRAVERY"},
                new() {FoodID = 9, LocalizationCode = "FoodSeries_Bastteto"},
                new() {FoodID = 10, LocalizationCode = "FoodSeries_Beaphar"},
            };
        }
        }

        public async Task InitializeGenderAsync()
        {
            var count = await _database.Table<GenderModel>().CountAsync();

            if (count > 0) return;

            var genderTypes = new List<GenderModel>
            {
                new() { LocalizationName = "Gender_Male" },
                new() { LocalizationName = "Gender_Female"},
                new() { LocalizationName = "Gender_Other" }
            };

            _ = await _database.InsertAllAsync(genderTypes);
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

            var breeds = new List<BreedModel>
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

            _ = await _database.InsertAllAsync(breeds);
        } 

        #endregion

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

        public async Task SaveProfileAsync(ProfileModel profile)
        {
            await InitializeAsync();
            _ = await _database.InsertOrReplaceAsync(profile);
        }

        #endregion
    }
}
