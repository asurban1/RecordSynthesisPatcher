using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Mutagen.Bethesda.Synthesis.Settings;

namespace RecordSynthesisPatcher.Settings;

// One alphabetized record list for the Synthesis settings UI.
public sealed class PatcherSettings
{
    public GeneralSettings General = new();
    public AactForwardingSettings AACT = new();
    public AchrForwardingSettings ACHR = new();
    public ActiForwardingSettings ACTI = new();
    public AddnForwardingSettings ADDN = new();
    public AlchForwardingSettings ALCH = new();
    public AmmoForwardingSettings AMMO = new();
    public AnioForwardingSettings ANIO = new();
    public AppaForwardingSettings APPA = new();
    public ArmaForwardingSettings ARMA = new();
    public ArmoForwardingSettings ARMO = new();
    public ArtoForwardingSettings ARTO = new();
    public AspcForwardingSettings ASPC = new();
    public AstpForwardingSettings ASTP = new();
    public AvifForwardingSettings AVIF = new();
    public BookForwardingSettings BOOK = new();
    public BptdForwardingSettings BPTD = new();
    public CamsForwardingSettings CAMS = new();
    public CellForwardingSettings CELL = new();
    public ClasForwardingSettings CLAS = new();
    public ClfmForwardingSettings CLFM = new();
    public ClmtForwardingSettings CLMT = new();
    public CobjForwardingSettings COBJ = new();
    public CollForwardingSettings COLL = new();
    public ContForwardingSettings CONT = new();
    public CpthForwardingSettings CPTH = new();
    public CstyForwardingSettings CSTY = new();
    public DebrForwardingSettings DEBR = new();
    public DialForwardingSettings DIAL = new();
    public DlbrForwardingSettings DLBR = new();
    public DlvwForwardingSettings DLVW = new();
    public DobjForwardingSettings DOBJ = new();
    public DoorForwardingSettings DOOR = new();
    public DualForwardingSettings DUAL = new();
    public EcznForwardingSettings ECZN = new();
    public EfshForwardingSettings EFSH = new();
    public EnchForwardingSettings ENCH = new();
    public EqupForwardingSettings EQUP = new();
    public ExplForwardingSettings EXPL = new();
    public EyesForwardingSettings EYES = new();
    public FactForwardingSettings FACT = new();
    public FlorForwardingSettings FLOR = new();
    public FlstForwardingSettings FLST = new();
    public FstpForwardingSettings FSTP = new();
    public FstsForwardingSettings FSTS = new();
    public FurnForwardingSettings FURN = new();
    public GlobForwardingSettings GLOB = new();
    public GmstForwardingSettings GMST = new();
    public GrasForwardingSettings GRAS = new();
    public HairForwardingSettings HAIR = new();
    public HazdForwardingSettings HAZD = new();
    public HdptForwardingSettings HDPT = new();
    public IdleForwardingSettings IDLE = new();
    public IdlmForwardingSettings IDLM = new();
    public ImadForwardingSettings IMAD = new();
    public ImgsForwardingSettings IMGS = new();
    public InfoForwardingSettings INFO = new();
    public IngrForwardingSettings INGR = new();
    public IpctForwardingSettings IPCT = new();
    public IpdsForwardingSettings IPDS = new();
    public KeymForwardingSettings KEYM = new();
    public KywdForwardingSettings KYWD = new();
    public LandForwardingSettings LAND = new();
    public LcrtForwardingSettings LCRT = new();
    public LctnForwardingSettings LCTN = new();
    public LensForwardingSettings LENS = new();
    public LgtmForwardingSettings LGTM = new();
    public LighForwardingSettings LIGH = new();
    public LscrForwardingSettings LSCR = new();
    public LtexForwardingSettings LTEX = new();
    public LvliForwardingSettings LVLI = new();
    public LvlnForwardingSettings LVLN = new();
    public LvspForwardingSettings LVSP = new();
    public MatoForwardingSettings MATO = new();
    public MattForwardingSettings MATT = new();
    public MesgForwardingSettings MESG = new();
    public MgefForwardingSettings MGEF = new();
    public MiscForwardingSettings MISC = new();
    public MovtForwardingSettings MOVT = new();
    public MsttForwardingSettings MSTT = new();
    public MuscForwardingSettings MUSC = new();
    public MustForwardingSettings MUST = new();
    public NaviForwardingSettings NAVI = new();
    public NavmForwardingSettings NAVM = new();
    public NpcForwardingSettings NPC_ = new();
    public OtftForwardingSettings OTFT = new();
    public PackForwardingSettings PACK = new();
    public ParwForwardingSettings PARW = new();
    public PbarForwardingSettings PBAR = new();
    public PbeaForwardingSettings PBEA = new();
    public PconForwardingSettings PCON = new();
    public PerkForwardingSettings PERK = new();
    public PflaForwardingSettings PFLA = new();
    public PgreForwardingSettings PGRE = new();
    public PhzdForwardingSettings PHZD = new();
    public PmisForwardingSettings PMIS = new();
    public ProjForwardingSettings PROJ = new();
    public QustForwardingSettings QUST = new();
    public RaceForwardingSettings RACE = new();
    public RefrForwardingSettings REFR = new();
    public RegnForwardingSettings REGN = new();
    public RelaForwardingSettings RELA = new();
    public RevbForwardingSettings REVB = new();
    public RfctForwardingSettings RFCT = new();
    public ScenForwardingSettings SCEN = new();
    public ScrlForwardingSettings SCRL = new();
    public ShouForwardingSettings SHOU = new();
    public SlgmForwardingSettings SLGM = new();
    public SmbnForwardingSettings SMBN = new();
    public SmenForwardingSettings SMEN = new();
    public SmqnForwardingSettings SMQN = new();
    public SnctForwardingSettings SNCT = new();
    public SndrForwardingSettings SNDR = new();
    public SopmForwardingSettings SOPM = new();
    public SounForwardingSettings SOUN = new();
    public SpelForwardingSettings SPEL = new();
    public SpgdForwardingSettings SPGD = new();
    public StatForwardingSettings STAT = new();
    public TactForwardingSettings TACT = new();
    public TreeForwardingSettings TREE = new();
    public TxstForwardingSettings TXST = new();
    public VoliForwardingSettings VOLI = new();
    public VtypForwardingSettings VTYP = new();
    public WatrForwardingSettings WATR = new();
    public WeapForwardingSettings WEAP = new();
    public WoopForwardingSettings WOOP = new();
    public WrldForwardingSettings WRLD = new();
    public WthrForwardingSettings WTHR = new();

    // Reads the pre-organization Forwarding/Merging layout once, then
    // Synthesis saves only the new record-first layout.
    [JsonExtensionData(ReadData = true, WriteData = false)]
    private IDictionary<string, JToken>? _legacySettings;

    [OnDeserialized]
    private void MigrateLegacySettings(StreamingContext context)
    {
        if (_legacySettings is null)
            return;

        if (_legacySettings.TryGetValue("Forwarding", out JToken? forwarding))
            ApplyLegacySection(forwarding, merge: false);
        if (_legacySettings.TryGetValue("Merging", out JToken? merging))
            ApplyLegacySection(merging, merge: true);

        _legacySettings = null;
    }

    private void ApplyLegacySection(JToken section, bool merge)
    {
        if (section is not JObject records)
            return;

        foreach (var recordProperty in records.Properties())
        {
            FieldInfo? recordField = GetType().GetField(recordProperty.Name);
            object? record = recordField?.GetValue(this);
            if (record is null || recordProperty.Value is not JObject fields)
                continue;

            foreach (var fieldProperty in fields.Properties())
            {
                string fieldName = fieldProperty.Name + (merge ? "Merge" : string.Empty);
                FieldInfo? field = record.GetType().GetField(fieldName);
                if (field?.FieldType == typeof(bool) &&
                    fieldProperty.Value.Type == JTokenType.Boolean)
                    field.SetValue(record, fieldProperty.Value.Value<bool>());
            }
        }
    }
}

public sealed class GeneralSettings
{
    public int ProgressInterval = 250_000;
    public bool VerboseRecordLogging = false;
}

public sealed partial class AactForwardingSettings
{
    [SynthesisOrder]
    public bool Color = false;
    [SynthesisOrder]
    public bool EditorID = false;
}

public sealed partial class AchrForwardingSettings
{
    [SynthesisOrder]
    public bool Base = false;
    [SynthesisOrder]
    public bool Count = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Emittance = false;
    [SynthesisOrder]
    public bool EnableParent = false;
    [SynthesisOrder]
    public bool EncounterZone = false;
    [SynthesisOrder]
    public bool FactionRank = false;
    [SynthesisOrder]
    public bool FavorCost = false;
    [SynthesisOrder]
    public bool HeadTrackingWeight = false;
    [SynthesisOrder]
    public bool Health = false;
    [SynthesisOrder]
    public bool Horse = false;
    [SynthesisOrder]
    public bool IsIgnoredBySandbox = false;
    [SynthesisOrder]
    public bool IsIgnoredBySandbox2 = false;
    [SynthesisOrder]
    public bool LevelModifier = false;
    [SynthesisOrder]
    public bool LinkedReferencesMerge = false;
    [SynthesisOrder]
    public bool LocationReference = false;
    [SynthesisOrder]
    public bool LocationRefTypesMerge = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool MerchantContainer = false;
    [SynthesisOrder]
    public bool MultiBoundReference = false;
    [SynthesisOrder]
    public bool Owner = false;
    [SynthesisOrder]
    public bool PersistentLocation = false;
    [SynthesisOrder]
    public bool Position = false;
    [SynthesisOrder]
    public bool Radius = false;
    [SynthesisOrder]
    public bool RagdollBipedData = false;
    [SynthesisOrder]
    public bool RagdollData = false;
    [SynthesisOrder]
    public bool Rotation = false;
    [SynthesisOrder]
    public bool Scale = false;
}

public sealed partial class ActiForwardingSettings
{
    [SynthesisOrder]
    public bool ActivateTextOverride = false;
    [SynthesisOrder]
    public bool ActivationSound = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool InteractionKeyword = false;
    [SynthesisOrder]
    public bool KeywordsMerge = false;
    [SynthesisOrder]
    public bool LoopingSound = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool MarkerColor = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool WaterType = false;
}

public sealed partial class AddnForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool MasterParticleSystemCap = false;
    [SynthesisOrder]
    public bool NodeIndex = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool Sound = false;
}

public sealed partial class AlchForwardingSettings
{
    [SynthesisOrder]
    public bool Addiction = false;
    [SynthesisOrder]
    public bool AddictionChance = false;
    [SynthesisOrder]
    public bool ConsumeSound = false;
    [SynthesisOrder]
    public bool Description = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool EquipmentType = false;
    [SynthesisOrder]
    public bool KeywordsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool PickUpSound = false;
    [SynthesisOrder]
    public bool PutDownSound = false;
    [SynthesisOrder]
    public bool Value = false;
    [SynthesisOrder]
    public bool Weight = false;
}

public sealed partial class AmmoForwardingSettings
{
    [SynthesisOrder]
    public bool Damage = false;
    [SynthesisOrder]
    public bool DATADataTypeState = false;
    [SynthesisOrder]
    public bool Description = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool KeywordsMerge = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool PickUpSound = false;
    [SynthesisOrder]
    public bool Projectile = false;
    [SynthesisOrder]
    public bool PutDownSound = false;
    [SynthesisOrder]
    public bool ShortName = false;
    [SynthesisOrder]
    public bool Value = false;
    [SynthesisOrder]
    public bool Weight = false;
}

public sealed partial class AnioForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool UnloadEvent = false;
}

public sealed partial class AppaForwardingSettings
{
    [SynthesisOrder]
    public bool Description = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool PickUpSound = false;
    [SynthesisOrder]
    public bool PutDownSound = false;
    [SynthesisOrder]
    public bool Quality = false;
    [SynthesisOrder]
    public bool Value = false;
    [SynthesisOrder]
    public bool Weight = false;
}

public sealed partial class ArmaForwardingSettings
{
    [SynthesisOrder]
    public bool AdditionalRacesMerge = false;
    [SynthesisOrder]
    public bool ArtObject = false;
    [SynthesisOrder]
    public bool BipedBodyTemplateArmorType = false;
    [SynthesisOrder]
    public bool BipedBodyTemplateFirstPersonFlagsMerge = false;
    [SynthesisOrder]
    public bool DetectionSoundValue = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FirstPersonModel = false;
    [SynthesisOrder]
    public bool FootstepSound = false;
    [SynthesisOrder]
    public bool Race = false;
    [SynthesisOrder]
    public bool Unknown = false;
    [SynthesisOrder]
    public bool Unknown2 = false;
    [SynthesisOrder]
    public bool WeaponAdjust = false;
}

public sealed partial class ArmoForwardingSettings
{
    [SynthesisOrder]
    public bool AlternateBlockMaterial = false;
    [SynthesisOrder]
    public bool ArmatureMerge = false;
    [SynthesisOrder]
    public bool ArmorRating = false;
    [SynthesisOrder]
    public bool BashImpactDataSet = false;
    [SynthesisOrder]
    public bool BipedBodyTemplateArmorType = false;
    [SynthesisOrder]
    public bool BipedBodyTemplateFirstPersonFlagsMerge = false;
    [SynthesisOrder]
    public bool Description = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool EnchantmentAmount = false;
    [SynthesisOrder]
    public bool EquipmentType = false;
    [SynthesisOrder]
    public bool KeywordsMerge = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool ObjectEffect = false;
    [SynthesisOrder]
    public bool PickUpSound = false;
    [SynthesisOrder]
    public bool PutDownSound = false;
    [SynthesisOrder]
    public bool Race = false;
    [SynthesisOrder]
    public bool RagdollConstraintTemplate = false;
    [SynthesisOrder]
    public bool TemplateArmor = false;
    [SynthesisOrder]
    public bool Value = false;
    [SynthesisOrder]
    public bool Weight = false;
}

public sealed partial class ArtoForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool Type = false;
}

public sealed partial class AspcForwardingSettings
{
    [SynthesisOrder]
    public bool AmbientSound = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool EnvironmentType = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool UseSoundFromRegion = false;
}

public sealed partial class AstpForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool IsFamily = false;
}

public sealed partial class AvifForwardingSettings
{
    [SynthesisOrder]
    public bool Abbreviation = false;
    [SynthesisOrder]
    public bool CNAM = false;
    [SynthesisOrder]
    public bool Description = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Name = false;
}

public sealed partial class BookForwardingSettings
{
    [SynthesisOrder]
    public bool BookText = false;
    [SynthesisOrder]
    public bool Description = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool InventoryArt = false;
    [SynthesisOrder]
    public bool KeywordsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool PickUpSound = false;
    [SynthesisOrder]
    public bool PutDownSound = false;
    [SynthesisOrder]
    public bool Type = false;
    [SynthesisOrder]
    public bool Unused = false;
    [SynthesisOrder]
    public bool Value = false;
    [SynthesisOrder]
    public bool Weight = false;
}

public sealed partial class BptdForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
}

public sealed partial class CamsForwardingSettings
{
    [SynthesisOrder]
    public bool Action = false;
    [SynthesisOrder]
    public bool DATADataTypeState = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool ImageSpaceModifier = false;
    [SynthesisOrder]
    public bool Location = false;
    [SynthesisOrder]
    public bool MaxTime = false;
    [SynthesisOrder]
    public bool MinTime = false;
    [SynthesisOrder]
    public bool NearTargetDistance = false;
    [SynthesisOrder]
    public bool Target = false;
    [SynthesisOrder]
    public bool TargetPercentBetweenActors = false;
    [SynthesisOrder]
    public bool TimeMultiplierGlobal = false;
    [SynthesisOrder]
    public bool TimeMultiplierPlayer = false;
    [SynthesisOrder]
    public bool TimeMultiplierTarget = false;
}

public sealed partial class CellForwardingSettings
{
    [SynthesisOrder]
    public bool AcousticSpace = false;
    [SynthesisOrder]
    public bool AmbientColors = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool EncounterZone = false;
    [SynthesisOrder]
    public bool FactionRank = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool ImageSpace = false;
    [SynthesisOrder]
    public bool LightingTemplate = false;
    [SynthesisOrder]
    public bool LNAM = false;
    [SynthesisOrder]
    public bool Location = false;
    [SynthesisOrder]
    public bool LockList = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool MaxHeightData = false;
    [SynthesisOrder]
    public bool Music = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool OcclusionData = false;
    [SynthesisOrder]
    public bool Owner = false;
    [SynthesisOrder]
    public bool RegionsMerge = false;
    [SynthesisOrder]
    public bool SkyAndWeatherFromRegion = false;
    [SynthesisOrder]
    public bool Water = false;
    [SynthesisOrder]
    public bool WaterEnvironmentMap = false;
    [SynthesisOrder]
    public bool WaterHeight = false;
    [SynthesisOrder]
    public bool WaterNoiseTexture = false;
    [SynthesisOrder]
    public bool WaterVelocity = false;
    [SynthesisOrder]
    public bool XWCN = false;
    [SynthesisOrder]
    public bool XWCS = false;
}

public sealed partial class ClasForwardingSettings
{
    [SynthesisOrder]
    public bool BleedoutDefault = false;
    [SynthesisOrder]
    public bool Description = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Icon = false;
    [SynthesisOrder]
    public bool MaxTrainingLevel = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool Teaches = false;
    [SynthesisOrder]
    public bool Unknown2 = false;
    [SynthesisOrder]
    public bool VoicePoints = false;
}

public sealed partial class ClfmForwardingSettings
{
    [SynthesisOrder]
    public bool Color = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool Playable = false;
}

public sealed partial class ClmtForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Moons = false;
    [SynthesisOrder]
    public bool PhaseLength = false;
    [SynthesisOrder]
    public bool SunriseBegin = false;
    [SynthesisOrder]
    public bool SunriseEnd = false;
    [SynthesisOrder]
    public bool SunsetBegin = false;
    [SynthesisOrder]
    public bool SunsetEnd = false;
    [SynthesisOrder]
    public bool Volatility = false;
}

public sealed partial class CobjForwardingSettings
{
    [SynthesisOrder]
    public bool CreatedObject = false;
    [SynthesisOrder]
    public bool CreatedObjectCount = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool ItemsMerge = false;
    [SynthesisOrder]
    public bool WorkbenchKeyword = false;
}

public sealed partial class CollForwardingSettings
{
    [SynthesisOrder]
    public bool DebugColor = false;
    [SynthesisOrder]
    public bool Description = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool Index = false;
    [SynthesisOrder]
    public bool Name = false;
}

public sealed partial class ContForwardingSettings
{
    [SynthesisOrder]
    public bool CloseSound = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool ItemsMerge = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool OpenSound = false;
    [SynthesisOrder]
    public bool Weight = false;
}

public sealed partial class CpthForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Zoom = false;
    [SynthesisOrder]
    public bool ZoomMustHaveCameraShots = false;
}

public sealed partial class CstyForwardingSettings
{
    [SynthesisOrder]
    public bool AvoidThreatChance = false;
    [SynthesisOrder]
    public bool CSGDDataTypeState = false;
    [SynthesisOrder]
    public bool CSMD = false;
    [SynthesisOrder]
    public bool DefensiveMult = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool EquipmentScoreMultMagic = false;
    [SynthesisOrder]
    public bool EquipmentScoreMultMelee = false;
    [SynthesisOrder]
    public bool EquipmentScoreMultRanged = false;
    [SynthesisOrder]
    public bool EquipmentScoreMultShout = false;
    [SynthesisOrder]
    public bool EquipmentScoreMultStaff = false;
    [SynthesisOrder]
    public bool EquipmentScoreMultUnarmed = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool GroupOffensiveMult = false;
    [SynthesisOrder]
    public bool LongRangeStrafeMult = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool OffensiveMult = false;
}

public sealed partial class DebrForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
}

public sealed partial class DialForwardingSettings
{
    [SynthesisOrder]
    public bool Branch = false;
    [SynthesisOrder]
    public bool Category = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool Priority = false;
    [SynthesisOrder]
    public bool Quest = false;
    [SynthesisOrder]
    public bool Subtype = false;
    [SynthesisOrder]
    public bool TopicFlagsMerge = false;
}

public sealed partial class DlbrForwardingSettings
{
    [SynthesisOrder]
    public bool Category = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool Quest = false;
    [SynthesisOrder]
    public bool StartingTopic = false;
}

public sealed partial class DlvwForwardingSettings
{
    [SynthesisOrder]
    public bool DNAM = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool ENAM = false;
    [SynthesisOrder]
    public bool Quest = false;
}

public sealed partial class DobjForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
}

public sealed partial class DoorForwardingSettings
{
    [SynthesisOrder]
    public bool CloseSound = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool LoopSound = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool OpenSound = false;
}

public sealed partial class DualForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool EffectShader = false;
    [SynthesisOrder]
    public bool Explosion = false;
    [SynthesisOrder]
    public bool HitEffectArt = false;
    [SynthesisOrder]
    public bool ImpactDataSet = false;
    [SynthesisOrder]
    public bool InheritScale = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool Projectile = false;
}

public sealed partial class EcznForwardingSettings
{
    [SynthesisOrder]
    public bool DATADataTypeState = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool Location = false;
    [SynthesisOrder]
    public bool MaxLevel = false;
    [SynthesisOrder]
    public bool MinLevel = false;
    [SynthesisOrder]
    public bool Owner = false;
    [SynthesisOrder]
    public bool Rank = false;
}

public sealed partial class EfshForwardingSettings
{
    [SynthesisOrder]
    public bool AddonModels = false;
    [SynthesisOrder]
    public bool AddonModelsFadeInTime = false;
    [SynthesisOrder]
    public bool AddonModelsFadeOutTime = false;
    [SynthesisOrder]
    public bool AddonModelsScaleEnd = false;
    [SynthesisOrder]
    public bool AddonModelsScaleInTime = false;
    [SynthesisOrder]
    public bool AddonModelsScaleOutTime = false;
    [SynthesisOrder]
    public bool AddonModelsScaleStart = false;
    [SynthesisOrder]
    public bool AmbientSound = false;
    [SynthesisOrder]
    public bool BirthPositionOffset = false;
    [SynthesisOrder]
    public bool BirthPositionOffsetRangePlusMinus = false;
    [SynthesisOrder]
    public bool ColorKey1 = false;
    [SynthesisOrder]
    public bool ColorKey1Alpha = false;
    [SynthesisOrder]
    public bool ColorKey1Time = false;
    [SynthesisOrder]
    public bool ColorKey2 = false;
    [SynthesisOrder]
    public bool ColorKey2Alpha = false;
    [SynthesisOrder]
    public bool ColorKey2Time = false;
    [SynthesisOrder]
    public bool ColorKey3 = false;
    [SynthesisOrder]
    public bool ColorKey3Alpha = false;
    [SynthesisOrder]
    public bool ColorKey3Time = false;
    [SynthesisOrder]
    public bool ColorScale = false;
    [SynthesisOrder]
    public bool DATADataTypeState = false;
    [SynthesisOrder]
    public bool EdgeColor = false;
    [SynthesisOrder]
    public bool EdgeEffectAlphaFadeInTime = false;
    [SynthesisOrder]
    public bool EdgeEffectAlphaFadeOutTime = false;
    [SynthesisOrder]
    public bool EdgeEffectAlphaPulseAmplitude = false;
    [SynthesisOrder]
    public bool EdgeEffectAlphaPulseFrequency = false;
    [SynthesisOrder]
    public bool EdgeEffectColor = false;
    [SynthesisOrder]
    public bool EdgeEffectFallOff = false;
    [SynthesisOrder]
    public bool EdgeEffectFullAlphaRatio = false;
    [SynthesisOrder]
    public bool EdgeEffectFullAlphaTime = false;
    [SynthesisOrder]
    public bool EdgeEffectPersistentAlphaRatio = false;
    [SynthesisOrder]
    public bool EdgeWidth = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool ExplosionWindSpeed = false;
    [SynthesisOrder]
    public bool FillAlphaFadeInTime = false;
    [SynthesisOrder]
    public bool FillAlphaPulseAmplitude = false;
    [SynthesisOrder]
    public bool FillAlphaPulseFrequency = false;
    [SynthesisOrder]
    public bool FillColorKey1 = false;
    [SynthesisOrder]
    public bool FillColorKey1Scale = false;
    [SynthesisOrder]
    public bool FillColorKey1Time = false;
    [SynthesisOrder]
    public bool FillColorKey2 = false;
    [SynthesisOrder]
    public bool FillColorKey2Scale = false;
    [SynthesisOrder]
    public bool FillColorKey2Time = false;
    [SynthesisOrder]
    public bool FillColorKey3 = false;
    [SynthesisOrder]
    public bool FillColorKey3Scale = false;
    [SynthesisOrder]
    public bool FillColorKey3Time = false;
    [SynthesisOrder]
    public bool FillFadeOutTime = false;
    [SynthesisOrder]
    public bool FillFullAlphaRatio = false;
    [SynthesisOrder]
    public bool FillFullAlphaTime = false;
    [SynthesisOrder]
    public bool FillPersistentAlphaRatio = false;
    [SynthesisOrder]
    public bool FillTextureAnimationSpeedU = false;
    [SynthesisOrder]
    public bool FillTextureAnimationSpeedV = false;
    [SynthesisOrder]
    public bool FillTextureScaleU = false;
    [SynthesisOrder]
    public bool FillTextureScaleV = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool HolesEndTime = false;
    [SynthesisOrder]
    public bool HolesEndValue = false;
    [SynthesisOrder]
    public bool HolesStartTime = false;
    [SynthesisOrder]
    public bool HolesStartValue = false;
    [SynthesisOrder]
    public bool MembraneBlendOperation = false;
    [SynthesisOrder]
    public bool MembraneDestBlendMode = false;
    [SynthesisOrder]
    public bool MembraneSourceBlendMode = false;
    [SynthesisOrder]
    public bool MembraneZTest = false;
    [SynthesisOrder]
    public bool ParticleAcceleration1 = false;
    [SynthesisOrder]
    public bool ParticleAcceleration2 = false;
    [SynthesisOrder]
    public bool ParticleAcceleration3 = false;
    [SynthesisOrder]
    public bool ParticleAccelerationAlongNormal = false;
    [SynthesisOrder]
    public bool ParticleAnimatedEndFrame = false;
    [SynthesisOrder]
    public bool ParticleAnimatedFrameCount = false;
    [SynthesisOrder]
    public bool ParticleAnimatedFrameCountVariation = false;
    [SynthesisOrder]
    public bool ParticleAnimatedLoopStartFrame = false;
    [SynthesisOrder]
    public bool ParticleAnimatedLoopStartVariation = false;
    [SynthesisOrder]
    public bool ParticleAnimatedStartFrame = false;
    [SynthesisOrder]
    public bool ParticleAnimatedStartFrameVariation = false;
    [SynthesisOrder]
    public bool ParticleBirthRampDownTime = false;
    [SynthesisOrder]
    public bool ParticleBirthRampUpTime = false;
    [SynthesisOrder]
    public bool ParticleBlendOperation = false;
    [SynthesisOrder]
    public bool ParticleDestBlendMode = false;
    [SynthesisOrder]
    public bool ParticleFullBirthRatio = false;
    [SynthesisOrder]
    public bool ParticleFullBirthTime = false;
    [SynthesisOrder]
    public bool ParticleInitialRotationDegree = false;
    [SynthesisOrder]
    public bool ParticleInitialRotationDegreePlusMinus = false;
    [SynthesisOrder]
    public bool ParticleInitialSpeedAlongNormal = false;
    [SynthesisOrder]
    public bool ParticleInitialSpeedAlongNormalPlusMinus = false;
    [SynthesisOrder]
    public bool ParticleInitialVelocity1 = false;
    [SynthesisOrder]
    public bool ParticleInitialVelocity2 = false;
    [SynthesisOrder]
    public bool ParticleInitialVelocity3 = false;
    [SynthesisOrder]
    public bool ParticleLifetime = false;
    [SynthesisOrder]
    public bool ParticleLifetimePlusMinus = false;
    [SynthesisOrder]
    public bool ParticlePeristentCount = false;
    [SynthesisOrder]
    public bool ParticleRotationSpeedDegreePerSec = false;
    [SynthesisOrder]
    public bool ParticleRotationSpeedDegreePerSecPlusMinus = false;
    [SynthesisOrder]
    public bool ParticleScaleKey1 = false;
    [SynthesisOrder]
    public bool ParticleScaleKey1Time = false;
    [SynthesisOrder]
    public bool ParticleScaleKey2 = false;
    [SynthesisOrder]
    public bool ParticleScaleKey2Time = false;
    [SynthesisOrder]
    public bool ParticleSourceBlendMode = false;
    [SynthesisOrder]
    public bool ParticleZTest = false;
    [SynthesisOrder]
    public bool SceneGraphEmitDepthLimit = false;
    [SynthesisOrder]
    public bool TextureCountU = false;
    [SynthesisOrder]
    public bool TextureCountV = false;
}

public sealed partial class EnchForwardingSettings
{
    [SynthesisOrder]
    public bool BaseEnchantment = false;
    [SynthesisOrder]
    public bool CastType = false;
    [SynthesisOrder]
    public bool ChargeTime = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool EnchantmentCost = false;
    [SynthesisOrder]
    public bool EnchantType = false;
    [SynthesisOrder]
    public bool ENITDataTypeState = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool TargetType = false;
    [SynthesisOrder]
    public bool WornRestrictions = false;
}

public sealed partial class EqupForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool UseAllParents = false;
}

public sealed partial class ExplForwardingSettings
{
    [SynthesisOrder]
    public bool Damage = false;
    [SynthesisOrder]
    public bool DATADataTypeState = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool Force = false;
    [SynthesisOrder]
    public bool ImageSpaceModifier = false;
    [SynthesisOrder]
    public bool ImpactDataSet = false;
    [SynthesisOrder]
    public bool ISRadius = false;
    [SynthesisOrder]
    public bool Light = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool ObjectEffect = false;
    [SynthesisOrder]
    public bool PlacedObject = false;
    [SynthesisOrder]
    public bool Radius = false;
    [SynthesisOrder]
    public bool Sound1 = false;
    [SynthesisOrder]
    public bool Sound2 = false;
    [SynthesisOrder]
    public bool SoundLevel = false;
    [SynthesisOrder]
    public bool SpawnProjectile = false;
    [SynthesisOrder]
    public bool VerticalOffsetMult = false;
}

public sealed partial class EyesForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
}

public sealed partial class FactForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool ExteriorJailMarker = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool FollowerWaitMarker = false;
    [SynthesisOrder]
    public bool JailOutfit = false;
    [SynthesisOrder]
    public bool MerchantContainer = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool PlayerInventoryContainer = false;
    [SynthesisOrder]
    public bool RelationsMerge = false;
    [SynthesisOrder]
    public bool SharedCrimeFactionList = false;
    [SynthesisOrder]
    public bool StolenGoodsContainer = false;
    [SynthesisOrder]
    public bool VendorBuySellList = false;
}

public sealed partial class FlorForwardingSettings
{
    [SynthesisOrder]
    public bool ActivateTextOverride = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FNAM = false;
    [SynthesisOrder]
    public bool HarvestSound = false;
    [SynthesisOrder]
    public bool Ingredient = false;
    [SynthesisOrder]
    public bool KeywordsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool PNAM = false;
}

public sealed partial class FlstForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool ItemsMerge = false;
}

public sealed partial class FstpForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool ImpactDataSet = false;
    [SynthesisOrder]
    public bool Tag = false;
}

public sealed partial class FstsForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
}

public sealed partial class FurnForwardingSettings
{
    [SynthesisOrder]
    public bool AssociatedSpell = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool InteractionKeyword = false;
    [SynthesisOrder]
    public bool KeywordsMerge = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool PNAM = false;
}

public sealed partial class GlobForwardingSettings
{
    [SynthesisOrder]
    public bool Data = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
}

public sealed partial class GmstForwardingSettings
{
    [SynthesisOrder]
    public bool Data = false;
    [SynthesisOrder]
    public bool EditorID = false;
}

public sealed partial class GrasForwardingSettings
{
    [SynthesisOrder]
    public bool ColorRange = false;
    [SynthesisOrder]
    public bool Density = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool HeightRange = false;
    [SynthesisOrder]
    public bool MaxSlope = false;
    [SynthesisOrder]
    public bool MinSlope = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool PositionRange = false;
    [SynthesisOrder]
    public bool UnitsFromWater = false;
    [SynthesisOrder]
    public bool UnitsFromWaterType = false;
    [SynthesisOrder]
    public bool Unknown = false;
    [SynthesisOrder]
    public bool Unknown2 = false;
    [SynthesisOrder]
    public bool Unknown3 = false;
    [SynthesisOrder]
    public bool WavePeriod = false;
}

public sealed partial class HairForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
}

public sealed partial class HazdForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool ImageSpaceModifier = false;
    [SynthesisOrder]
    public bool ImageSpaceRadius = false;
    [SynthesisOrder]
    public bool ImpactDataSet = false;
    [SynthesisOrder]
    public bool Lifetime = false;
    [SynthesisOrder]
    public bool Light = false;
    [SynthesisOrder]
    public bool Limit = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool Radius = false;
    [SynthesisOrder]
    public bool Sound = false;
    [SynthesisOrder]
    public bool Spell = false;
    [SynthesisOrder]
    public bool TargetInterval = false;
}

public sealed partial class HdptForwardingSettings
{
    [SynthesisOrder]
    public bool Color = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool TextureSet = false;
    [SynthesisOrder]
    public bool Type = false;
    [SynthesisOrder]
    public bool ValidRaces = false;
}

public sealed partial class IdleForwardingSettings
{
    [SynthesisOrder]
    public bool AnimationEvent = false;
    [SynthesisOrder]
    public bool AnimationGroupSection = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool LoopingSecondsMax = false;
    [SynthesisOrder]
    public bool LoopingSecondsMin = false;
    [SynthesisOrder]
    public bool ReplayDelay = false;
}

public sealed partial class IdlmForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool IdleTimer = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
}

public sealed partial class ImadForwardingSettings
{
    [SynthesisOrder]
    public bool Animatable = false;
    [SynthesisOrder]
    public bool Duration = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool RadialBlurCenter = false;
    [SynthesisOrder]
    public bool RadialBlurUseTarget = false;
}

public sealed partial class ImgsForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool ENAM = false;
}

public sealed partial class InfoForwardingSettings
{
    [SynthesisOrder]
    public bool AudioOutputOverride = false;
    [SynthesisOrder]
    public bool DATA = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FavorLevel = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool PreviousDialog = false;
    [SynthesisOrder]
    public bool Prompt = false;
    [SynthesisOrder]
    public bool ResponseData = false;
    [SynthesisOrder]
    public bool Speaker = false;
    [SynthesisOrder]
    public bool Topic = false;
    [SynthesisOrder]
    public bool WalkAwayTopic = false;
}

public sealed partial class IngrForwardingSettings
{
    [SynthesisOrder]
    public bool Description = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool EquipType = false;
    [SynthesisOrder]
    public bool IngredientValue = false;
    [SynthesisOrder]
    public bool KeywordsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool PickUpSound = false;
    [SynthesisOrder]
    public bool PutDownSound = false;
    [SynthesisOrder]
    public bool Value = false;
    [SynthesisOrder]
    public bool Weight = false;
}

public sealed partial class IpctForwardingSettings
{
    [SynthesisOrder]
    public bool AngleThreshold = false;
    [SynthesisOrder]
    public bool Duration = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Hazard = false;
    [SynthesisOrder]
    public bool NoDecalData = false;
    [SynthesisOrder]
    public bool Orientation = false;
    [SynthesisOrder]
    public bool PlacementRadius = false;
    [SynthesisOrder]
    public bool Result = false;
    [SynthesisOrder]
    public bool SecondaryTextureSet = false;
    [SynthesisOrder]
    public bool Sound1 = false;
    [SynthesisOrder]
    public bool Sound2 = false;
    [SynthesisOrder]
    public bool SoundLevel = false;
    [SynthesisOrder]
    public bool TextureSet = false;
    [SynthesisOrder]
    public bool Unknown = false;
}

public sealed partial class IpdsForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
}

public sealed partial class KeymForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool KeywordsMerge = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool PickUpSound = false;
    [SynthesisOrder]
    public bool PutDownSound = false;
    [SynthesisOrder]
    public bool Value = false;
    [SynthesisOrder]
    public bool Weight = false;
}

public sealed partial class KywdForwardingSettings
{
    [SynthesisOrder]
    public bool Color = false;
    [SynthesisOrder]
    public bool EditorID = false;
}

public sealed partial class LandForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
}

public sealed partial class LcrtForwardingSettings
{
    [SynthesisOrder]
    public bool Color = false;
    [SynthesisOrder]
    public bool EditorID = false;
}

public sealed partial class LctnForwardingSettings
{
    [SynthesisOrder]
    public bool Color = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool HorseMarkerRef = false;
    [SynthesisOrder]
    public bool KeywordsMerge = false;
    [SynthesisOrder]
    public bool Music = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ParentLocation = false;
    [SynthesisOrder]
    public bool UnreportedCrimeFaction = false;
    [SynthesisOrder]
    public bool WorldLocationMarkerRef = false;
    [SynthesisOrder]
    public bool WorldLocationRadius = false;
}

public sealed partial class LensForwardingSettings
{
    [SynthesisOrder]
    public bool ColorInfluence = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FadeDistanceRadiusScale = false;
}

public sealed partial class LgtmForwardingSettings
{
    [SynthesisOrder]
    public bool AmbientColor = false;
    [SynthesisOrder]
    public bool AmbientColors = false;
    [SynthesisOrder]
    public bool DATADataTypeState = false;
    [SynthesisOrder]
    public bool DirectionalAmbientColors = false;
    [SynthesisOrder]
    public bool DirectionalColor = false;
    [SynthesisOrder]
    public bool DirectionalFade = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FogClipDistance = false;
    [SynthesisOrder]
    public bool FogFar = false;
    [SynthesisOrder]
    public bool FogFarColor = false;
    [SynthesisOrder]
    public bool FogMax = false;
    [SynthesisOrder]
    public bool FogNear = false;
    [SynthesisOrder]
    public bool FogNearColor = false;
    [SynthesisOrder]
    public bool FogPower = false;
    [SynthesisOrder]
    public bool LightFadeEndDistance = false;
    [SynthesisOrder]
    public bool LightFadeStartDistance = false;
}

public sealed partial class LighForwardingSettings
{
    [SynthesisOrder]
    public bool Color = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FadeValue = false;
    [SynthesisOrder]
    public bool FalloffExponent = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool FlickerIntensityAmplitude = false;
    [SynthesisOrder]
    public bool FlickerMovementAmplitude = false;
    [SynthesisOrder]
    public bool FlickerPeriod = false;
    [SynthesisOrder]
    public bool FOV = false;
    [SynthesisOrder]
    public bool Lens = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool NearClip = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool Radius = false;
    [SynthesisOrder]
    public bool Sound = false;
    [SynthesisOrder]
    public bool Value = false;
    [SynthesisOrder]
    public bool Weight = false;
}

public sealed partial class LscrForwardingSettings
{
    [SynthesisOrder]
    public bool Description = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool InitialRotation = false;
    [SynthesisOrder]
    public bool InitialScale = false;
    [SynthesisOrder]
    public bool InitialTranslationOffset = false;
    [SynthesisOrder]
    public bool LoadingScreenNif = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
}

public sealed partial class LtexForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool HavokFriction = false;
    [SynthesisOrder]
    public bool HavokRestitution = false;
    [SynthesisOrder]
    public bool MaterialType = false;
    [SynthesisOrder]
    public bool TextureSet = false;
    [SynthesisOrder]
    public bool TextureSpecularExponent = false;
}

public sealed partial class LvliForwardingSettings
{
    [SynthesisOrder]
    public bool ChanceNone = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool EntriesMerge = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool Global = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
}

public sealed partial class LvlnForwardingSettings
{
    [SynthesisOrder]
    public bool ChanceNone = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool EntriesMerge = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool Global = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
}

public sealed partial class LvspForwardingSettings
{
    [SynthesisOrder]
    public bool ChanceNone = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool EntriesMerge = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
}

public sealed partial class MatoForwardingSettings
{
    [SynthesisOrder]
    public bool DATADataTypeState = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FalloffBias = false;
    [SynthesisOrder]
    public bool FalloffScale = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool HasSnow = false;
    [SynthesisOrder]
    public bool MaterialUvScale = false;
    [SynthesisOrder]
    public bool NoiseUvScale = false;
    [SynthesisOrder]
    public bool NormalDampener = false;
    [SynthesisOrder]
    public bool ProjectionVector = false;
    [SynthesisOrder]
    public bool SinglePassColor = false;
}

public sealed partial class MattForwardingSettings
{
    [SynthesisOrder]
    public bool Buoyancy = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool HavokDisplayColor = false;
    [SynthesisOrder]
    public bool HavokImpactDataSet = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool Parent = false;
}

public sealed partial class MesgForwardingSettings
{
    [SynthesisOrder]
    public bool Description = false;
    [SynthesisOrder]
    public bool DisplayTime = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool INAM = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool Quest = false;
}

public sealed partial class MgefForwardingSettings
{
    [SynthesisOrder]
    public bool BaseCost = false;
    [SynthesisOrder]
    public bool CastingArt = false;
    [SynthesisOrder]
    public bool CastingLight = false;
    [SynthesisOrder]
    public bool CastingSoundLevel = false;
    [SynthesisOrder]
    public bool CastType = false;
    [SynthesisOrder]
    public bool CounterEffectsMerge = false;
    [SynthesisOrder]
    public bool Description = false;
    [SynthesisOrder]
    public bool DualCastArt = false;
    [SynthesisOrder]
    public bool DualCastScale = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool EnchantArt = false;
    [SynthesisOrder]
    public bool EnchantShader = false;
    [SynthesisOrder]
    public bool EnchantVisuals = false;
    [SynthesisOrder]
    public bool EquipAbility = false;
    [SynthesisOrder]
    public bool Explosion = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool HitEffectArt = false;
    [SynthesisOrder]
    public bool HitShader = false;
    [SynthesisOrder]
    public bool HitVisuals = false;
    [SynthesisOrder]
    public bool ImageSpaceModifier = false;
    [SynthesisOrder]
    public bool ImpactData = false;
    [SynthesisOrder]
    public bool KeywordsMerge = false;
    [SynthesisOrder]
    public bool MagicSkill = false;
    [SynthesisOrder]
    public bool MenuDisplayObject = false;
    [SynthesisOrder]
    public bool MinimumSkillLevel = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool PerkToApply = false;
    [SynthesisOrder]
    public bool Projectile = false;
    [SynthesisOrder]
    public bool ResistValue = false;
    [SynthesisOrder]
    public bool ScriptEffectAIDelayTime = false;
    [SynthesisOrder]
    public bool ScriptEffectAIScore = false;
    [SynthesisOrder]
    public bool SecondActorValue = false;
    [SynthesisOrder]
    public bool SecondActorValueWeight = false;
    [SynthesisOrder]
    public bool SkillUsageMultiplier = false;
    [SynthesisOrder]
    public bool SpellmakingArea = false;
    [SynthesisOrder]
    public bool SpellmakingCastingTime = false;
    [SynthesisOrder]
    public bool TaperCurve = false;
    [SynthesisOrder]
    public bool TaperDuration = false;
    [SynthesisOrder]
    public bool TaperWeight = false;
    [SynthesisOrder]
    public bool TargetType = false;
    [SynthesisOrder]
    public bool Unknown1 = false;
}

public sealed partial class MiscForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool KeywordsMerge = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool PickUpSound = false;
    [SynthesisOrder]
    public bool PutDownSound = false;
    [SynthesisOrder]
    public bool Value = false;
    [SynthesisOrder]
    public bool Weight = false;
}

public sealed partial class MovtForwardingSettings
{
    [SynthesisOrder]
    public bool BackRun = false;
    [SynthesisOrder]
    public bool BackWalk = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool ForwardRun = false;
    [SynthesisOrder]
    public bool ForwardWalk = false;
    [SynthesisOrder]
    public bool LeftRun = false;
    [SynthesisOrder]
    public bool LeftWalk = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool RightRun = false;
    [SynthesisOrder]
    public bool RightWalk = false;
    [SynthesisOrder]
    public bool RotateInPlaceRun = false;
    [SynthesisOrder]
    public bool RotateInPlaceWalk = false;
    [SynthesisOrder]
    public bool RotateWhileMovingRun = false;
    [SynthesisOrder]
    public bool SPEDDataTypeState = false;
}

public sealed partial class MsttForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool LoopingSound = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
}

public sealed partial class MuscForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FadeDuration = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
}

public sealed partial class MustForwardingSettings
{
    [SynthesisOrder]
    public bool Duration = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FadeOut = false;
    [SynthesisOrder]
    public bool Type = false;
}

public sealed partial class NaviForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool NavMeshVersion = false;
    [SynthesisOrder]
    public bool NVSI = false;
}

public sealed partial class NavmForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool NNAM = false;
    [SynthesisOrder]
    public bool ONAM = false;
    [SynthesisOrder]
    public bool PNAM = false;
}

public sealed partial class NpcForwardingSettings
{
    [SynthesisOrder]
    public bool ActorEffectsMerge = false;
    [SynthesisOrder]
    public bool AttackRace = false;
    [SynthesisOrder]
    public bool Class = false;
    [SynthesisOrder]
    public bool CombatOverridePackageList = false;
    [SynthesisOrder]
    public bool CombatStyle = false;
    [SynthesisOrder]
    public bool ConfigurationFlagsMerge = false;
    [SynthesisOrder]
    public bool CrimeFaction = false;
    [SynthesisOrder]
    public bool DeathItem = false;
    [SynthesisOrder]
    public bool DefaultOutfit = false;
    [SynthesisOrder]
    public bool DefaultPackageList = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FactionsMerge = false;
    [SynthesisOrder]
    public bool FarAwayModel = false;
    [SynthesisOrder]
    public bool GiftFilter = false;
    [SynthesisOrder]
    public bool GuardWarnOverridePackageList = false;
    [SynthesisOrder]
    public bool HairColor = false;
    [SynthesisOrder]
    public bool HeadPartsMerge = false;
    [SynthesisOrder]
    public bool HeadTexture = false;
    [SynthesisOrder]
    public bool Height = false;
    [SynthesisOrder]
    public bool ItemsMerge = false;
    [SynthesisOrder]
    public bool KeywordsMerge = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool NAM5 = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool ObserveDeadBodyOverridePackageList = false;
    [SynthesisOrder]
    public bool PackagesMerge = false;
    [SynthesisOrder]
    public bool PerksMerge = false;
    [SynthesisOrder]
    public bool Race = false;
    [SynthesisOrder]
    public bool ShortName = false;
    [SynthesisOrder]
    public bool SleepingOutfit = false;
    [SynthesisOrder]
    public bool SoundLevel = false;
    [SynthesisOrder]
    public bool SpectatorOverridePackageList = false;
    [SynthesisOrder]
    public bool Template = false;
    [SynthesisOrder]
    public bool TextureLighting = false;
    [SynthesisOrder]
    public bool Voice = false;
    [SynthesisOrder]
    public bool Weight = false;
    [SynthesisOrder]
    public bool WornArmor = false;
}

public sealed partial class OtftForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool ItemsMerge = false;
}

public sealed partial class PackForwardingSettings
{
    [SynthesisOrder]
    public bool CombatStyle = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool InterruptFlagsMerge = false;
    [SynthesisOrder]
    public bool InterruptOverride = false;
    [SynthesisOrder]
    public bool OwnerQuest = false;
    [SynthesisOrder]
    public bool PackageTemplate = false;
    [SynthesisOrder]
    public bool PreferredSpeed = false;
    [SynthesisOrder]
    public bool ScheduleDate = false;
    [SynthesisOrder]
    public bool ScheduleDayOfWeek = false;
    [SynthesisOrder]
    public bool ScheduleHour = false;
    [SynthesisOrder]
    public bool ScheduleMinute = false;
    [SynthesisOrder]
    public bool ScheduleMonth = false;
    [SynthesisOrder]
    public bool Type = false;
    [SynthesisOrder]
    public bool Unknown = false;
    [SynthesisOrder]
    public bool Unknown2 = false;
    [SynthesisOrder]
    public bool Unknown3 = false;
    [SynthesisOrder]
    public bool XnamMarker = false;
}

public sealed partial class ParwForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Emittance = false;
    [SynthesisOrder]
    public bool EnableParent = false;
    [SynthesisOrder]
    public bool EncounterZone = false;
    [SynthesisOrder]
    public bool FavorCost = false;
    [SynthesisOrder]
    public bool HeadTrackingWeight = false;
    [SynthesisOrder]
    public bool IgnoredBySandbox = false;
    [SynthesisOrder]
    public bool LinkedReferencesMerge = false;
    [SynthesisOrder]
    public bool LocationReference = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool MultiBoundReference = false;
    [SynthesisOrder]
    public bool Owner = false;
    [SynthesisOrder]
    public bool Projectile = false;
    [SynthesisOrder]
    public bool Scale = false;
}

public sealed partial class PbarForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Emittance = false;
    [SynthesisOrder]
    public bool EnableParent = false;
    [SynthesisOrder]
    public bool EncounterZone = false;
    [SynthesisOrder]
    public bool FavorCost = false;
    [SynthesisOrder]
    public bool HeadTrackingWeight = false;
    [SynthesisOrder]
    public bool IgnoredBySandbox = false;
    [SynthesisOrder]
    public bool LinkedReferencesMerge = false;
    [SynthesisOrder]
    public bool LocationReference = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool MultiBoundReference = false;
    [SynthesisOrder]
    public bool Owner = false;
    [SynthesisOrder]
    public bool Projectile = false;
    [SynthesisOrder]
    public bool Scale = false;
}

public sealed partial class PbeaForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Emittance = false;
    [SynthesisOrder]
    public bool EnableParent = false;
    [SynthesisOrder]
    public bool EncounterZone = false;
    [SynthesisOrder]
    public bool FavorCost = false;
    [SynthesisOrder]
    public bool HeadTrackingWeight = false;
    [SynthesisOrder]
    public bool IgnoredBySandbox = false;
    [SynthesisOrder]
    public bool LinkedReferencesMerge = false;
    [SynthesisOrder]
    public bool LocationReference = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool MultiBoundReference = false;
    [SynthesisOrder]
    public bool Owner = false;
    [SynthesisOrder]
    public bool Projectile = false;
    [SynthesisOrder]
    public bool Scale = false;
}

public sealed partial class PconForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Emittance = false;
    [SynthesisOrder]
    public bool EnableParent = false;
    [SynthesisOrder]
    public bool EncounterZone = false;
    [SynthesisOrder]
    public bool FavorCost = false;
    [SynthesisOrder]
    public bool HeadTrackingWeight = false;
    [SynthesisOrder]
    public bool IgnoredBySandbox = false;
    [SynthesisOrder]
    public bool LinkedReferencesMerge = false;
    [SynthesisOrder]
    public bool LocationReference = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool MultiBoundReference = false;
    [SynthesisOrder]
    public bool Owner = false;
    [SynthesisOrder]
    public bool Projectile = false;
    [SynthesisOrder]
    public bool Scale = false;
}

public sealed partial class PerkForwardingSettings
{
    [SynthesisOrder]
    public bool Description = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Hidden = false;
    [SynthesisOrder]
    public bool Level = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool NextPerk = false;
    [SynthesisOrder]
    public bool NumRanks = false;
    [SynthesisOrder]
    public bool Playable = false;
    [SynthesisOrder]
    public bool Trait = false;
}

public sealed partial class PflaForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Emittance = false;
    [SynthesisOrder]
    public bool EnableParent = false;
    [SynthesisOrder]
    public bool EncounterZone = false;
    [SynthesisOrder]
    public bool FavorCost = false;
    [SynthesisOrder]
    public bool HeadTrackingWeight = false;
    [SynthesisOrder]
    public bool IgnoredBySandbox = false;
    [SynthesisOrder]
    public bool LinkedReferencesMerge = false;
    [SynthesisOrder]
    public bool LocationReference = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool MultiBoundReference = false;
    [SynthesisOrder]
    public bool Owner = false;
    [SynthesisOrder]
    public bool Projectile = false;
    [SynthesisOrder]
    public bool Scale = false;
}

public sealed partial class PgreForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Emittance = false;
    [SynthesisOrder]
    public bool EnableParent = false;
    [SynthesisOrder]
    public bool EncounterZone = false;
    [SynthesisOrder]
    public bool FavorCost = false;
    [SynthesisOrder]
    public bool HeadTrackingWeight = false;
    [SynthesisOrder]
    public bool IgnoredBySandbox = false;
    [SynthesisOrder]
    public bool LinkedReferencesMerge = false;
    [SynthesisOrder]
    public bool LocationReference = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool MultiBoundReference = false;
    [SynthesisOrder]
    public bool Owner = false;
    [SynthesisOrder]
    public bool Projectile = false;
    [SynthesisOrder]
    public bool Scale = false;
}

public sealed partial class PhzdForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Emittance = false;
    [SynthesisOrder]
    public bool EnableParent = false;
    [SynthesisOrder]
    public bool EncounterZone = false;
    [SynthesisOrder]
    public bool FavorCost = false;
    [SynthesisOrder]
    public bool Hazard = false;
    [SynthesisOrder]
    public bool HeadTrackingWeight = false;
    [SynthesisOrder]
    public bool IgnoredBySandbox = false;
    [SynthesisOrder]
    public bool LinkedReferencesMerge = false;
    [SynthesisOrder]
    public bool LocationReference = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool MultiBoundReference = false;
    [SynthesisOrder]
    public bool Owner = false;
    [SynthesisOrder]
    public bool Scale = false;
}

public sealed partial class PmisForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Emittance = false;
    [SynthesisOrder]
    public bool EnableParent = false;
    [SynthesisOrder]
    public bool EncounterZone = false;
    [SynthesisOrder]
    public bool FavorCost = false;
    [SynthesisOrder]
    public bool HeadTrackingWeight = false;
    [SynthesisOrder]
    public bool IgnoredBySandbox = false;
    [SynthesisOrder]
    public bool LinkedReferencesMerge = false;
    [SynthesisOrder]
    public bool LocationReference = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool MultiBoundReference = false;
    [SynthesisOrder]
    public bool Owner = false;
    [SynthesisOrder]
    public bool Projectile = false;
    [SynthesisOrder]
    public bool Scale = false;
}

public sealed partial class ProjForwardingSettings
{
    [SynthesisOrder]
    public bool CollisionLayer = false;
    [SynthesisOrder]
    public bool CollisionRadius = false;
    [SynthesisOrder]
    public bool ConeSpread = false;
    [SynthesisOrder]
    public bool CountdownSound = false;
    [SynthesisOrder]
    public bool DATADataTypeState = false;
    [SynthesisOrder]
    public bool DecalData = false;
    [SynthesisOrder]
    public bool DefaultWeaponSource = false;
    [SynthesisOrder]
    public bool DisaleSound = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Explosion = false;
    [SynthesisOrder]
    public bool ExplosionAltTriggerProximity = false;
    [SynthesisOrder]
    public bool ExplosionAltTriggerTimer = false;
    [SynthesisOrder]
    public bool FadeDuration = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool Gravity = false;
    [SynthesisOrder]
    public bool ImpactForce = false;
    [SynthesisOrder]
    public bool Lifetime = false;
    [SynthesisOrder]
    public bool Light = false;
    [SynthesisOrder]
    public bool MuzzleFlash = false;
    [SynthesisOrder]
    public bool MuzzleFlashDuration = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool Range = false;
    [SynthesisOrder]
    public bool RelaunchInterval = false;
    [SynthesisOrder]
    public bool Sound = false;
    [SynthesisOrder]
    public bool SoundLevel = false;
    [SynthesisOrder]
    public bool Speed = false;
    [SynthesisOrder]
    public bool TextureFilesHashes = false;
    [SynthesisOrder]
    public bool TracerChance = false;
    [SynthesisOrder]
    public bool Type = false;
}

public sealed partial class QustForwardingSettings
{
    [SynthesisOrder]
    public bool Description = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Filter = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool NextAliasID = false;
    [SynthesisOrder]
    public bool Priority = false;
    [SynthesisOrder]
    public bool QuestFormVersion = false;
    [SynthesisOrder]
    public bool Type = false;
}

public sealed partial class RaceForwardingSettings
{
    [SynthesisOrder]
    public bool AccelerationRate = false;
    [SynthesisOrder]
    public bool AimAngleTolerance = false;
    [SynthesisOrder]
    public bool AngularAccelerationRate = false;
    [SynthesisOrder]
    public bool AngularTolerance = false;
    [SynthesisOrder]
    public bool ArmorRace = false;
    [SynthesisOrder]
    public bool AttackRace = false;
    [SynthesisOrder]
    public bool BaseCarryWeight = false;
    [SynthesisOrder]
    public bool BaseMass = false;
    [SynthesisOrder]
    public bool BaseMovementDefaultFly = false;
    [SynthesisOrder]
    public bool BaseMovementDefaultRun = false;
    [SynthesisOrder]
    public bool BaseMovementDefaultSneak = false;
    [SynthesisOrder]
    public bool BaseMovementDefaultSprint = false;
    [SynthesisOrder]
    public bool BaseMovementDefaultSwim = false;
    [SynthesisOrder]
    public bool BaseMovementDefaultWalk = false;
    [SynthesisOrder]
    public bool BipedBodyTemplateArmorType = false;
    [SynthesisOrder]
    public bool BipedBodyTemplateFirstPersonFlagsMerge = false;
    [SynthesisOrder]
    public bool BodyBipedObject = false;
    [SynthesisOrder]
    public bool BodyPartData = false;
    [SynthesisOrder]
    public bool CloseLootSound = false;
    [SynthesisOrder]
    public bool DATADataTypeState = false;
    [SynthesisOrder]
    public bool DecapitationFX = false;
    [SynthesisOrder]
    public bool DecelerationRate = false;
    [SynthesisOrder]
    public bool Description = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool EquipmentFlagsMerge = false;
    [SynthesisOrder]
    public bool ExportingExtraNam2 = false;
    [SynthesisOrder]
    public bool FacegenFaceClamp = false;
    [SynthesisOrder]
    public bool FacegenMainClamp = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool FlightRadius = false;
    [SynthesisOrder]
    public bool HairBipedObject = false;
    [SynthesisOrder]
    public bool HeadBipedObject = false;
    [SynthesisOrder]
    public bool ImpactDataSet = false;
    [SynthesisOrder]
    public bool InjuredHealthPercent = false;
    [SynthesisOrder]
    public bool KeywordsMerge = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool MaterialType = false;
    [SynthesisOrder]
    public bool MorphRace = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool NumberOfTintsInList = false;
    [SynthesisOrder]
    public bool OpenLootSound = false;
    [SynthesisOrder]
    public bool ShieldBipedObject = false;
    [SynthesisOrder]
    public bool Size = false;
    [SynthesisOrder]
    public bool Skin = false;
    [SynthesisOrder]
    public bool UnarmedDamage = false;
    [SynthesisOrder]
    public bool UnarmedEquipSlot = false;
    [SynthesisOrder]
    public bool UnarmedReach = false;
    [SynthesisOrder]
    public bool Unknown = false;
}

public sealed partial class RefrForwardingSettings
{
    [SynthesisOrder]
    public bool Action = false;
    [SynthesisOrder]
    public bool AttachRef = false;
    [SynthesisOrder]
    public bool Base = false;
    [SynthesisOrder]
    public bool BoundData = false;
    [SynthesisOrder]
    public bool Charge = false;
    [SynthesisOrder]
    public bool CollisionLayer = false;
    [SynthesisOrder]
    public bool DistantLodData = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Emittance = false;
    [SynthesisOrder]
    public bool EnableParent = false;
    [SynthesisOrder]
    public bool EncounterZone = false;
    [SynthesisOrder]
    public bool FactionRank = false;
    [SynthesisOrder]
    public bool FavorCost = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool HeadTrackingWeight = false;
    [SynthesisOrder]
    public bool ImageSpace = false;
    [SynthesisOrder]
    public bool IsIgnoredBySandbox = false;
    [SynthesisOrder]
    public bool IsMultiBoundPrimitive = false;
    [SynthesisOrder]
    public bool IsOpenByDefault = false;
    [SynthesisOrder]
    public bool ItemCount = false;
    [SynthesisOrder]
    public bool LeveledItemBaseObject = false;
    [SynthesisOrder]
    public bool LevelModifier = false;
    [SynthesisOrder]
    public bool LightingTemplate = false;
    [SynthesisOrder]
    public bool LinkedReferencesMerge = false;
    [SynthesisOrder]
    public bool LinkedRoomsMerge = false;
    [SynthesisOrder]
    public bool LitWaterMerge = false;
    [SynthesisOrder]
    public bool LocationReference = false;
    [SynthesisOrder]
    public bool LocationRefTypesMerge = false;
    [SynthesisOrder]
    public bool Lock = false;
    [SynthesisOrder]
    public bool MultiBoundReference = false;
    [SynthesisOrder]
    public bool Owner = false;
    [SynthesisOrder]
    public bool PersistentLocation = false;
    [SynthesisOrder]
    public bool Position = false;
    [SynthesisOrder]
    public bool PrimitiveBounds = false;
    [SynthesisOrder]
    public bool PrimitiveColor = false;
    [SynthesisOrder]
    public bool PrimitiveType = false;
    [SynthesisOrder]
    public bool PrimitiveUnknown = false;
    [SynthesisOrder]
    public bool Radius = false;
    [SynthesisOrder]
    public bool RagdollBipedData = false;
    [SynthesisOrder]
    public bool RagdollData = false;
    [SynthesisOrder]
    public bool Rotation = false;
    [SynthesisOrder]
    public bool Scale = false;
    [SynthesisOrder]
    public bool SpawnContainer = false;
    [SynthesisOrder]
    public bool TeleportDestination = false;
    [SynthesisOrder]
    public bool TeleportMessageBox = false;
    [SynthesisOrder]
    public bool Unknown = false;
    [SynthesisOrder]
    public bool XCVL = false;
    [SynthesisOrder]
    public bool XCZA = false;
    [SynthesisOrder]
    public bool XCZC = false;
    [SynthesisOrder]
    public bool XCZR = false;
    [SynthesisOrder]
    public bool XORD = false;
    [SynthesisOrder]
    public bool XWCN = false;
    [SynthesisOrder]
    public bool XWCS = false;
}

public sealed partial class RegnForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Grasses = false;
    [SynthesisOrder]
    public bool Land = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool Map = false;
    [SynthesisOrder]
    public bool MapColor = false;
    [SynthesisOrder]
    public bool Objects = false;
    [SynthesisOrder]
    public bool Sounds = false;
    [SynthesisOrder]
    public bool Weather = false;
    [SynthesisOrder]
    public bool Worldspace = false;
}

public sealed partial class RelaForwardingSettings
{
    [SynthesisOrder]
    public bool AssociationType = false;
    [SynthesisOrder]
    public bool Child = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool Parent = false;
    [SynthesisOrder]
    public bool Rank = false;
    [SynthesisOrder]
    public bool Unknown = false;
}

public sealed partial class RevbForwardingSettings
{
    [SynthesisOrder]
    public bool DecayHfRatio = false;
    [SynthesisOrder]
    public bool DecayMilliseconds = false;
    [SynthesisOrder]
    public bool DensityPercent = false;
    [SynthesisOrder]
    public bool DiffusionPercent = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool HfReferenceHertz = false;
    [SynthesisOrder]
    public bool ReflectDelayMS = false;
    [SynthesisOrder]
    public bool Reflections = false;
    [SynthesisOrder]
    public bool ReverbAmp = false;
    [SynthesisOrder]
    public bool ReverbDelayMS = false;
    [SynthesisOrder]
    public bool RoomFilter = false;
    [SynthesisOrder]
    public bool RoomHfFilter = false;
    [SynthesisOrder]
    public bool Unknown = false;
}

public sealed partial class RfctForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool EffectArt = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool Shader = false;
}

public sealed partial class ScenForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool LastActionIndex = false;
    [SynthesisOrder]
    public bool Quest = false;
    [SynthesisOrder]
    public bool VNAM = false;
}

public sealed partial class ScrlForwardingSettings
{
    [SynthesisOrder]
    public bool BaseCost = false;
    [SynthesisOrder]
    public bool CastDuration = false;
    [SynthesisOrder]
    public bool CastType = false;
    [SynthesisOrder]
    public bool ChargeTime = false;
    [SynthesisOrder]
    public bool Description = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool EquipmentType = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool HalfCostPerk = false;
    [SynthesisOrder]
    public bool KeywordsMerge = false;
    [SynthesisOrder]
    public bool MenuDisplayObject = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool PickUpSound = false;
    [SynthesisOrder]
    public bool PutDownSound = false;
    [SynthesisOrder]
    public bool Range = false;
    [SynthesisOrder]
    public bool TargetType = false;
    [SynthesisOrder]
    public bool Type = false;
    [SynthesisOrder]
    public bool Value = false;
    [SynthesisOrder]
    public bool Weight = false;
}

public sealed partial class ShouForwardingSettings
{
    [SynthesisOrder]
    public bool Description = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool MenuDisplayObject = false;
    [SynthesisOrder]
    public bool Name = false;
}

public sealed partial class SlgmForwardingSettings
{
    [SynthesisOrder]
    public bool ContainedSoul = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool KeywordsMerge = false;
    [SynthesisOrder]
    public bool LinkedTo = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool MaximumCapacity = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool PickUpSound = false;
    [SynthesisOrder]
    public bool PutDownSound = false;
    [SynthesisOrder]
    public bool Value = false;
    [SynthesisOrder]
    public bool Weight = false;
}

public sealed partial class SmbnForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool MaxConcurrentQuests = false;
    [SynthesisOrder]
    public bool Parent = false;
    [SynthesisOrder]
    public bool PreviousSibling = false;
}

public sealed partial class SmenForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool MaxConcurrentQuests = false;
    [SynthesisOrder]
    public bool Parent = false;
    [SynthesisOrder]
    public bool PreviousSibling = false;
    [SynthesisOrder]
    public bool Type = false;
}

public sealed partial class SmqnForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool MaxConcurrentQuests = false;
    [SynthesisOrder]
    public bool MaxNumQuestsToRun = false;
    [SynthesisOrder]
    public bool Parent = false;
    [SynthesisOrder]
    public bool PreviousSibling = false;
    [SynthesisOrder]
    public bool QuestFlagsMerge = false;
}

public sealed partial class SnctForwardingSettings
{
    [SynthesisOrder]
    public bool DefaultMenuVolume = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool Parent = false;
    [SynthesisOrder]
    public bool StaticVolumeMultiplier = false;
}

public sealed partial class SndrForwardingSettings
{
    [SynthesisOrder]
    public bool AlternateSoundFor = false;
    [SynthesisOrder]
    public bool Category = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool OutputModel = false;
    [SynthesisOrder]
    public bool PercentFrequencyShift = false;
    [SynthesisOrder]
    public bool PercentFrequencyVariance = false;
    [SynthesisOrder]
    public bool Priority = false;
    [SynthesisOrder]
    public bool StaticAttenuation = false;
    [SynthesisOrder]
    public bool String = false;
    [SynthesisOrder]
    public bool Type = false;
    [SynthesisOrder]
    public bool Variance = false;
}

public sealed partial class SopmForwardingSettings
{
    [SynthesisOrder]
    public bool CNAM = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FNAM = false;
    [SynthesisOrder]
    public bool SNAM = false;
    [SynthesisOrder]
    public bool Type = false;
}

public sealed partial class SounForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FNAM = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool SNDD = false;
    [SynthesisOrder]
    public bool SoundDescriptor = false;
}

public sealed partial class SpelForwardingSettings
{
    [SynthesisOrder]
    public bool BaseCost = false;
    [SynthesisOrder]
    public bool CastDuration = false;
    [SynthesisOrder]
    public bool CastType = false;
    [SynthesisOrder]
    public bool ChargeTime = false;
    [SynthesisOrder]
    public bool Description = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool EquipmentType = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool HalfCostPerk = false;
    [SynthesisOrder]
    public bool KeywordsMerge = false;
    [SynthesisOrder]
    public bool MenuDisplayObject = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool Range = false;
    [SynthesisOrder]
    public bool TargetType = false;
    [SynthesisOrder]
    public bool Type = false;
}

public sealed partial class SpgdForwardingSettings
{
    [SynthesisOrder]
    public bool BoxSize = false;
    [SynthesisOrder]
    public bool CenterOffsetMax = false;
    [SynthesisOrder]
    public bool CenterOffsetMin = false;
    [SynthesisOrder]
    public bool DATADataTypeState = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool GravityVelocity = false;
    [SynthesisOrder]
    public bool InitialRotationRange = false;
    [SynthesisOrder]
    public bool NumSubtexturesX = false;
    [SynthesisOrder]
    public bool NumSubtexturesY = false;
    [SynthesisOrder]
    public bool ParticleDensity = false;
    [SynthesisOrder]
    public bool ParticleSizeX = false;
    [SynthesisOrder]
    public bool ParticleSizeY = false;
    [SynthesisOrder]
    public bool RotationVelocity = false;
    [SynthesisOrder]
    public bool Type = false;
}

public sealed partial class StatForwardingSettings
{
    [SynthesisOrder]
    public bool DNAMDataTypeState = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool Material = false;
    [SynthesisOrder]
    public bool MaxAngle = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool Unused = false;
}

public sealed partial class TactForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FNAM = false;
    [SynthesisOrder]
    public bool KeywordsMerge = false;
    [SynthesisOrder]
    public bool LoopingSound = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool Voice = false;
}

public sealed partial class TreeForwardingSettings
{
    [SynthesisOrder]
    public bool BranchFlexibility = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool HarvestSound = false;
    [SynthesisOrder]
    public bool Ingredient = false;
    [SynthesisOrder]
    public bool LeafAmplitude = false;
    [SynthesisOrder]
    public bool LeafFrequency = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool TrunkFlexibility = false;
    [SynthesisOrder]
    public bool Unknown = false;
}

public sealed partial class TxstForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
}

public sealed partial class VoliForwardingSettings
{
    [SynthesisOrder]
    public bool ColorB = false;
    [SynthesisOrder]
    public bool ColorG = false;
    [SynthesisOrder]
    public bool ColorR = false;
    [SynthesisOrder]
    public bool CustomColorContribution = false;
    [SynthesisOrder]
    public bool DensityContribution = false;
    [SynthesisOrder]
    public bool DensityFallingSpeed = false;
    [SynthesisOrder]
    public bool DensitySize = false;
    [SynthesisOrder]
    public bool DensityWindSpeed = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Intensity = false;
    [SynthesisOrder]
    public bool PhaseFunctionContribution = false;
    [SynthesisOrder]
    public bool PhaseFunctionScattering = false;
    [SynthesisOrder]
    public bool SamplingRepartitionRangeFactor = false;
}

public sealed partial class VtypForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
}

public sealed partial class WatrForwardingSettings
{
    [SynthesisOrder]
    public bool AngularVelocity = false;
    [SynthesisOrder]
    public bool DamagePerSecond = false;
    [SynthesisOrder]
    public bool DeepColor = false;
    [SynthesisOrder]
    public bool DepthNormals = false;
    [SynthesisOrder]
    public bool DepthReflections = false;
    [SynthesisOrder]
    public bool DepthRefraction = false;
    [SynthesisOrder]
    public bool DepthSpecularLighting = false;
    [SynthesisOrder]
    public bool DisplacementDampner = false;
    [SynthesisOrder]
    public bool DisplacementFalloff = false;
    [SynthesisOrder]
    public bool DisplacementFoce = false;
    [SynthesisOrder]
    public bool DisplacementStartingSize = false;
    [SynthesisOrder]
    public bool DisplacementVelocity = false;
    [SynthesisOrder]
    public bool DNAMDataTypeState = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool FogAboveWaterAmount = false;
    [SynthesisOrder]
    public bool FogAboveWaterDistanceFarPlane = false;
    [SynthesisOrder]
    public bool FogAboveWaterDistanceNearPlane = false;
    [SynthesisOrder]
    public bool FogUnderWaterAmount = false;
    [SynthesisOrder]
    public bool FogUnderWaterDistanceFarPlane = false;
    [SynthesisOrder]
    public bool FogUnderWaterDistanceNearPlane = false;
    [SynthesisOrder]
    public bool GNAM = false;
    [SynthesisOrder]
    public bool ImageSpace = false;
    [SynthesisOrder]
    public bool LinearVelocity = false;
    [SynthesisOrder]
    public bool Material = false;
    [SynthesisOrder]
    public bool MNAM = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool NoiseFalloff = false;
    [SynthesisOrder]
    public bool NoiseFlowmapScale = false;
    [SynthesisOrder]
    public bool NoiseLayerOneAmplitudeScale = false;
    [SynthesisOrder]
    public bool NoiseLayerOneUvScale = false;
    [SynthesisOrder]
    public bool NoiseLayerOneWindDirection = false;
    [SynthesisOrder]
    public bool NoiseLayerOneWindSpeed = false;
    [SynthesisOrder]
    public bool NoiseLayerThreeAmplitudeScale = false;
    [SynthesisOrder]
    public bool NoiseLayerThreeUvScale = false;
    [SynthesisOrder]
    public bool NoiseLayerThreeWindDirection = false;
    [SynthesisOrder]
    public bool NoiseLayerThreeWindSpeed = false;
    [SynthesisOrder]
    public bool NoiseLayerTwoAmplitudeScale = false;
    [SynthesisOrder]
    public bool NoiseLayerTwoUvScale = false;
    [SynthesisOrder]
    public bool NoiseLayerTwoWindDirection = false;
    [SynthesisOrder]
    public bool NoiseLayerTwoWindSpeed = false;
    [SynthesisOrder]
    public bool Opacity = false;
    [SynthesisOrder]
    public bool OpenSound = false;
    [SynthesisOrder]
    public bool ReflectionColor = false;
    [SynthesisOrder]
    public bool ShallowColor = false;
    [SynthesisOrder]
    public bool SpecularBrightness = false;
    [SynthesisOrder]
    public bool SpecularPower = false;
    [SynthesisOrder]
    public bool SpecularRadius = false;
    [SynthesisOrder]
    public bool SpecularSunPower = false;
    [SynthesisOrder]
    public bool SpecularSunSparkleMagnitude = false;
    [SynthesisOrder]
    public bool SpecularSunSparklePower = false;
    [SynthesisOrder]
    public bool SpecularSunSpecularMagnitude = false;
    [SynthesisOrder]
    public bool Spell = false;
    [SynthesisOrder]
    public bool Unknown = false;
    [SynthesisOrder]
    public bool Unknown3 = false;
    [SynthesisOrder]
    public bool Unknown5 = false;
    [SynthesisOrder]
    public bool WaterFresnel = false;
    [SynthesisOrder]
    public bool WaterReflectionMagnitude = false;
    [SynthesisOrder]
    public bool WaterReflectivity = false;
    [SynthesisOrder]
    public bool WaterRefractionMagnitude = false;
}

public sealed partial class WeapForwardingSettings
{
    [SynthesisOrder]
    public bool AlternateBlockMaterial = false;
    [SynthesisOrder]
    public bool AttackFailSound = false;
    [SynthesisOrder]
    public bool AttackLoopSound = false;
    [SynthesisOrder]
    public bool AttackSound = false;
    [SynthesisOrder]
    public bool AttackSound2D = false;
    [SynthesisOrder]
    public bool BlockBashImpact = false;
    [SynthesisOrder]
    public bool Description = false;
    [SynthesisOrder]
    public bool DetectionSoundLevel = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool EnchantmentAmount = false;
    [SynthesisOrder]
    public bool EquipmentType = false;
    [SynthesisOrder]
    public bool EquipSound = false;
    [SynthesisOrder]
    public bool FirstPersonModel = false;
    [SynthesisOrder]
    public bool IdleSound = false;
    [SynthesisOrder]
    public bool ImpactDataSet = false;
    [SynthesisOrder]
    public bool KeywordsMerge = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBounds = false;
    [SynthesisOrder]
    public bool ObjectEffect = false;
    [SynthesisOrder]
    public bool PickUpSound = false;
    [SynthesisOrder]
    public bool PutDownSound = false;
    [SynthesisOrder]
    public bool Template = false;
    [SynthesisOrder]
    public bool UnequipSound = false;
    [SynthesisOrder]
    public bool Unused = false;
}

public sealed partial class WoopForwardingSettings
{
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool Translation = false;
}

public sealed partial class WrldForwardingSettings
{
    [SynthesisOrder]
    public bool Climate = false;
    [SynthesisOrder]
    public bool DistantLodMultiplier = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool EncounterZone = false;
    [SynthesisOrder]
    public bool FixedDimensionsCenterCell = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool InteriorLighting = false;
    [SynthesisOrder]
    public bool Location = false;
    [SynthesisOrder]
    public bool LodWater = false;
    [SynthesisOrder]
    public bool LodWaterHeight = false;
    [SynthesisOrder]
    public bool MajorFlagsMerge = false;
    [SynthesisOrder]
    public bool Music = false;
    [SynthesisOrder]
    public bool Name = false;
    [SynthesisOrder]
    public bool ObjectBoundsMax = false;
    [SynthesisOrder]
    public bool ObjectBoundsMin = false;
    [SynthesisOrder]
    public bool OffsetData = false;
    [SynthesisOrder]
    public bool Water = false;
    [SynthesisOrder]
    public bool WorldMapCellOffset = false;
    [SynthesisOrder]
    public bool WorldMapOffsetScale = false;
}

public sealed partial class WthrForwardingSettings
{
    [SynthesisOrder]
    public bool ANAM = false;
    [SynthesisOrder]
    public bool BNAM = false;
    [SynthesisOrder]
    public bool CNAM = false;
    [SynthesisOrder]
    public bool DirectionalAmbientLightingColorsDay = false;
    [SynthesisOrder]
    public bool DirectionalAmbientLightingColorsNight = false;
    [SynthesisOrder]
    public bool DirectionalAmbientLightingColorsSunrise = false;
    [SynthesisOrder]
    public bool DirectionalAmbientLightingColorsSunset = false;
    [SynthesisOrder]
    public bool DNAM = false;
    [SynthesisOrder]
    public bool EditorID = false;
    [SynthesisOrder]
    public bool FlagsMerge = false;
    [SynthesisOrder]
    public bool FogDistanceDayFar = false;
    [SynthesisOrder]
    public bool FogDistanceDayMax = false;
    [SynthesisOrder]
    public bool FogDistanceDayNear = false;
    [SynthesisOrder]
    public bool FogDistanceDayPower = false;
    [SynthesisOrder]
    public bool FogDistanceNightFar = false;
    [SynthesisOrder]
    public bool FogDistanceNightMax = false;
    [SynthesisOrder]
    public bool FogDistanceNightNear = false;
    [SynthesisOrder]
    public bool FogDistanceNightPower = false;
    [SynthesisOrder]
    public bool LightningColor = false;
    [SynthesisOrder]
    public bool LNAM = false;
    [SynthesisOrder]
    public bool NAM0DataTypeState = false;
    [SynthesisOrder]
    public bool NAM2 = false;
    [SynthesisOrder]
    public bool NAM3 = false;
    [SynthesisOrder]
    public bool ONAM = false;
    [SynthesisOrder]
    public bool Precipitation = false;
    [SynthesisOrder]
    public bool PrecipitationBeginFadeIn = false;
    [SynthesisOrder]
    public bool PrecipitationEndFadeOut = false;
    [SynthesisOrder]
    public bool SunDamage = false;
    [SynthesisOrder]
    public bool SunGlare = false;
    [SynthesisOrder]
    public bool SunGlareLensFlare = false;
    [SynthesisOrder]
    public bool ThunderLightningBeginFadeIn = false;
    [SynthesisOrder]
    public bool ThunderLightningEndFadeOut = false;
    [SynthesisOrder]
    public bool ThunderLightningFrequency = false;
    [SynthesisOrder]
    public bool TransDelta = false;
    [SynthesisOrder]
    public bool Unknown = false;
    [SynthesisOrder]
    public bool VisualEffect = false;
    [SynthesisOrder]
    public bool VisualEffectBegin = false;
    [SynthesisOrder]
    public bool VisualEffectEnd = false;
    [SynthesisOrder]
    public bool WindDirection = false;
    [SynthesisOrder]
    public bool WindDirectionRange = false;
    [SynthesisOrder]
    public bool WindSpeed = false;
}


