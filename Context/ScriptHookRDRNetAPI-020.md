<!-- ScriptHookRDRNetAPI lines 4751-5000 of 30005 -->
```csharp
        <member name="M:RDR2.Native.ITEMDATABASE._0x12DF9C58201DD19A(System.UInt64)">
            <summary>
            _ITEMDATABASE_GET_(A)* - _ITEMDATABASE_GET_(B)*
            </summary>
        </member>
        <member name="M:RDR2.Native.ITEMDATABASE._0x1FC25AEB5F76B38D(System.UInt64,System.UInt64,System.UInt64)">
            <summary>
            _ITEMDATABASE_GET_(A)* - _ITEMDATABASE_GET_(B)*
            </summary>
        </member>
        <member name="M:RDR2.Native.ITEMDATABASE._0xF540239F9937033B(System.UInt64,System.UInt64)">
            <summary>
            Returns itemCount (?)
            _ITEMDATABASE_GET_(A)* - _ITEMDATABASE_GET_(B)*
            </summary>
        </member>
        <member name="M:RDR2.Native.ITEMDATABASE._0xF27F01BBF5ACD3F3(System.UInt64,System.UInt64,System.UInt64,System.UInt64)">
            <summary>
            _ITEMDATABASE_FILLOUT_(A)* - _ITEMDATABASE_FILLOUT_(B)*
            </summary>
        </member>
        <member name="M:RDR2.Native.ITEMDATABASE._ITEMDATABASE_CREATE_ITEM_COLLECTION(System.UInt64*,System.Int32*,System.Int32)">
            <summary>
            Returns collectionId to be used with 0x8750F69A720C2E41 (p0) and 0xCBB7B6EDFA933ADE (p0)
            </summary>
        </member>
        <member name="M:RDR2.Native.ITEMDATABASE._0xD389A2549C4EFB30(System.Int32)">
            <summary>
            Returns (collection?) size/index (?)
            _ITEMDATABASE_GET_(A)* - _ITEMDATABASE_GET_(B)*
            </summary>
        </member>
        <member name="M:RDR2.Native.ITEMDATABASE._ITEMDATABASE_GET_COMPONENT_ITEM(System.Int32,System.Int32,System.UInt32*)">
            <summary>
            Params: p2 can be a component item hash
            </summary>
        </member>
        <member name="M:RDR2.Native.ITEMDATABASE._ITEMDATABASE_GET_SHOP_INVENTORIES_ITEM_INFO_BY_KEY(System.UInt64,System.UInt64,System.UInt64)">
            <summary>
            Same Native Function as 0x17721003A66C72BF
            </summary>
        </member>
        <member name="M:RDR2.Native.ITEMDATABASE._0xF8D09EF8CE61D7BF(System.UInt64,System.UInt64)">
            <summary>
            _ITEMDATABASE_FILLOUT_(B)* - ITEMDATABASE_FILLOUT_ITEM*
            </summary>
        </member>
        <member name="M:RDR2.Native.ITEMDATABASE._ITEMDATABASE_GET_MODIFIED_PRICE(System.UInt64,System.UInt64)">
            <summary>
            Returns an alternative cost hash to COST_SHOP_DEFAULT
            </summary>
        </member>
        <member name="M:RDR2.Native.ITEMDATABASE._ITEMDATABASE_GET_PRIORITY_ACCESS_AWARD(System.UInt32)">
            <summary>
            _ITEMDATABASE_GET_* - _ITEMDATABASE_IS_*
            </summary>
        </member>
        <member name="M:RDR2.Native.ITEMDATABASE._ITEMDATABASE_GET_AWARD_ITEM_COUNT(System.UInt32)">
            <summary>
            Returns iAwardItemCount
            </summary>
        </member>
        <member name="M:RDR2.Native.ITEMDATABASE._0x48229CE0C7938237(System.UInt32)">
            <summary>
            _ITEMDATABASE_GET_(A)* - _ITEMDATABASE_GET_(B)*
            </summary>
        </member>
        <member name="M:RDR2.Native.ITEMDATABASE._0x8D029948CA29409B(System.UInt32,System.UInt64,System.UInt64)">
            <summary>
            _ITEMDATABASE_FILLOUT_(A)* - _ITEMDATABASE_FILLOUT_(B)*
            </summary>
        </member>
        <member name="M:RDR2.Native.ITEMDATABASE._ITEMDATABASE_DOES_BUNDLE_HAVE_TAG(System.UInt32,System.UInt32,System.UInt32)">
            <summary>
            Params: tag = TAG_ITEM_PROPERTY (tagType(?))
            </summary>
        </member>
        <member name="M:RDR2.Native.ITEMDATABASE._ITEMDATABASE_LOCALIZATION_GET_NUM_LABEL_TYPES(System.UInt64)">
            <summary>
            Returns docData.iNumTotalLabelTypes
            </summary>
        </member>
        <member name="M:RDR2.Native.ITEMDATABASE._ITEMDATABASE_LOCALIZATION_GET_NUM_VALUES(System.UInt64,System.UInt64)">
            <summary>
            Returns iNumValuesForType
            </summary>
        </member>
        <member name="M:RDR2.Native.ITEMDATABASE._ITEMDATABASE_LOCALIZATION_GET_TYPE(System.UInt64,System.UInt64)">
            <summary>
            Returns LabelType
            </summary>
        </member>
        <member name="M:RDR2.Native.ITEMDATABASE._0xAA29A5F13B2C20B2(System.UInt64,System.UInt32)">
            <summary>
            Returns Pathset Hash
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._REPORT_CRIME(System.Int32,System.UInt32,System.Int32,System.Int32,System.Boolean)">
            <summary>
            crimeType:
            enum eCrimeType : Hash
            {
            	CRIME_ACCOMPLICE = 0xAF074F6D,
            	CRIME_ARSON = 0x68134DC7,
            	CRIME_ASSAULT = 0x0BADC882,
            	CRIME_ASSAULT_ANIMAL = 0x18DA55EE,
            	CRIME_ASSAULT_CORPSE = 0x4E5F23F2,
            	CRIME_ASSAULT_HORSE = 0xC4736181,
            	CRIME_ASSAULT_LAW = 0xD7466D7C,
            	CRIME_ASSAULT_LIVESTOCK = 0xCCE1CCBD,
            	CRIME_BANK_ROBBERY = 0x6A1ADE3D,
            	CRIME_BURGLARY = 0xA54C77E0,
            	CRIME_CHEATING = 0xA2FF1145,
            	CRIME_DISTURBANCE = 0x5011F613,
            	CRIME_EXPLOSION = 0x3EBA7A37,
            	CRIME_EXPLOSION_POISON = 0x91D0A0E1,
            	CRIME_GRAVE_ROBBERY = 0x971EA5AF,
            	CRIME_HASSLE = 0x58488776,
            	CRIME_HIT_AND_RUN = 0xFF0A3CC4,
            	CRIME_HIT_AND_RUN_LAW = 0x064814AF,
            	CRIME_INTIMIDATION = 0x8319FBAB,
            	CRIME_JACK_HORSE = 0x82F7E4A2,
            	CRIME_JACK_VEHICLE = 0x6B981F4C,
            	CRIME_JAIL_BREAK = 0x12C1D589,
            	CRIME_KIDNAPPING = 0x98F908DB,
            	CRIME_KIDNAPPING_LAW = 0xFD72A7EA,
            	CRIME_LASSO_ASSAULT = 0x56EE5D5A,
            	CRIME_LAW_IS_THREATENED = 0x1CB91DF0,
            	CRIME_LOITERING = 0x6629D2F4,
            	CRIME_LOOTING = 0x55AD2BEB,
            	CRIME_MURDER = 0xE28ECE7E,
            	CRIME_MURDER_ANIMAL = 0x48F59A66,
            	CRIME_MURDER_HORSE = 0xC7261D79,
            	CRIME_MURDER_LAW = 0x7797FCE7,
            	CRIME_MURDER_LIVESTOCK = 0x9569C546,
            	CRIME_MURDER_PLAYER = 0xF5ABD6C9,
            	CRIME_MURDER_PLAYER_HORSE = 0xD55C6A79,
            	CRIME_PROPERTY_DESTRUCTION = 0x533B003D,
            	CRIME_RESIST_ARREST = 0xDF577BA5,
            	CRIME_ROBBERY = 0xA3BEDE4C,
            	CRIME_SELF_DEFENCE = 0xBD6A0AA3,
            	CRIME_STAGECOACH_ROBBERY = 0xFC738E61,
            	CRIME_STOLEN_GOODS = 0x9A949C79,
            	CRIME_THEFT = 0x72ADE410,
            	CRIME_THEFT_HORSE = 0xBE3A5838,
            	CRIME_THEFT_LIVESTOCK = 0x85BA08FD,
            	CRIME_THEFT_VEHICLE = 0x43A9ECA1,
            	CRIME_THREATEN = 0x941C985A,
            	CRIME_THREATEN_LAW = 0x7F908566,
            	CRIME_TRAIN_ROBBERY = 0x647D2A5A,
            	CRIME_TRAMPLE = 0x45DB39D8,
            	CRIME_TRAMPLE_LAW = 0xF00F266B,
            	CRIME_TRAMPLE_PLAYER = 0x75970C15,
            	CRIME_TRESPASSING = 0xAEDE8E35,
            	CRIME_UNARMED_ASSAULT = 0x5098CC5A,
            	CRIME_VANDALISM = 0x80FDC759,
            	CRIME_VANDALISM_VEHICLE = 0xF9E7ECE4,
            	CRIME_VEHICLE_DESTRUCTION = 0x54A85DDC,
            	CRIME_WANTED_LEVEL_UP_DEBUG_HIGH = 0x99C52FF5,
            	CRIME_WANTED_LEVEL_UP_DEBUG_LOW = 0xD891890F
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW.SUPPRESS_CRIME_THIS_FRAME(System.Int32,System.UInt32,System.Int32,System.Int32,System.Int32)">
            <summary>
            crimeType: see _REPORT_CRIME
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW.NUM_CRIMES_SUPPRESSED(System.Int32,System.UInt32)">
            <summary>
            Returns amount of suppressed crimes to be used later in the function MPINTRO_CRIME_MONITOR_MAINTAIN
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0x15ABD5004CAD2D99(System.Int32)">
            <summary>
            Params: p0 either 0, 1 or -1 in R* Scripts
            Set to 0 called together with _SUPPRESS_CRIME
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0x292AD61A33A7A485">
            <summary>
            Only used in R* Script fm_mission_controller
            _CLEAR_WANTED_*
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.LAW._0x07E8B8B20570271C(System.Int32)" -->
        <member name="M:RDR2.Native.LAW._0x22741652985C84D0(System.Int32,System.UInt32)">
            <summary>
            Used in SP only
            _REPORT_*
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0x9EF07CFBB19A9733">
            <summary>
            Only used in shoprobberies R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._SET_LAW_REGION(System.Int32,System.UInt32,System.UInt32)">
            <summary>
            enum eLawRegion : Hash
            {
            	LAW_DISPATCH_REGION_NONE = 0,
            	LAW_REGION_AGUASDULCES = 0x2F573EBE,
            	LAW_REGION_ANNESBURG = 0x68CAFD50,
            	LAW_REGION_ARMADILLO = 0xF0B90756,
            	LAW_REGION_BAYOU_NWA = 0x80966B1C,
            	LAW_REGION_BEECHERS_HOPE = 0xE2544977,
            	LAW_REGION_BIG_VALLEY = 0x3DF1559A,
            	LAW_REGION_BLACKWATER = 0x60D4886D,
            	LAW_REGION_BLACKWATER_MAINGAME = 0x66553576,
            	LAW_REGION_BLUEGILL_MARSH = 0x1D6AED8E,
            	LAW_REGION_BRAITHWAITE_MANOR = 0x3D71E7FF,
            	LAW_REGION_BUTCHER_CREEK = 0x2B3E1822,
            	LAW_REGION_CALIGA_HALL = 0xF3FE5080,
            	LAW_REGION_CORNWALL = 0xCC4672FA,
            	LAW_REGION_CUMBERLAND_FOREST = 0x81A78306,
            	LAW_REGION_EMERALD_RANCH = 0x5C069DF3,
            	LAW_REGION_FORT_WALLACE = 0x0AF25192,
            	LAW_REGION_GREAT_PLAINS = 0xB20573FA,
            	LAW_REGION_GREAT_PLAINS_MAINGAME = 0x9862FF7C,
            	LAW_REGION_GRIZZLIES = 0xBB936031,
            	LAW_REGION_GUAMA = 0x200DFF42,
            	LAW_REGION_HEARTLANDS = 0xAD14DA65,
            	LAW_REGION_LAGRAS = 0xC64808D3,
            	LAW_REGION_MACFARLANES_RANCH = 0x396A7D5F,
            	LAW_REGION_MANICATO = 0x039DB6BF,
            	LAW_REGION_MANZANITA_POST = 0x895E580E,
            	LAW_REGION_MANZANITA_POST_MAINGAME = 0x9BDD6A38,
            	LAW_REGION_OCCUPIED_CARAVAN_CAMP = 0x7EBABB01,
            	LAW_REGION_OLD_MAP_WILDERNESS = 0xCBB45950,
            	LAW_REGION_OLD_MAP_WILDERNESS_MAINGAME = 0x9F839BE7,
            	LAW_REGION_OUTLAW3 = 0x97A02FC1,
            	LAW_REGION_PRONGHORN_RANCH = 0x398E4BFC,
            	LAW_REGION_RHODES = 0x89222928,
            	LAW_REGION_RHODES_LOCKDOWN = 0xB1181671,
            	LAW_REGION_RIDGEWOOD_FARM = 0x635C3028,
            	LAW_REGION_ROANOKE_RIDGE = 0x46386A9A,
            	LAW_REGION_SAINT_DENIS = 0x5CF7C268,
            	LAW_REGION_SAINT_DENIS_RURAL = 0x4FD5331A,
            	LAW_REGION_SCARLETT_MEADOWS = 0x5FDD9717,
            	LAW_REGION_SISIKA = 0x2B6BBA52,
            	LAW_REGION_STRAWBERRY = 0xDD932620,
            	LAW_REGION_TALL_TREES = 0xD939B758,
            	LAW_REGION_TALL_TREES_MAINGAME = 0x084B17DF,
            	LAW_REGION_THIEVES_LANDING = 0x3D0C2EB6,
            	LAW_REGION_THIEVES_LANDING_MAINGAME = 0x61C450F3,
            	LAW_REGION_TUMBLEWEED = 0x0EFAF8DC,
            	LAW_REGION_VALENTINE = 0xA7A3F0C3,
            	LAW_REGION_VALENTINE_LOCKDOWN = 0x123582FE,
            	LAW_REGION_VAN_HORN = 0x619B528E,
```
