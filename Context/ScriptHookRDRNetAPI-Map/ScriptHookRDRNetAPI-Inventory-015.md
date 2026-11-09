<!-- ScriptHookRDRNetAPI-Inventory chunk 15 of 26; methods=383; sha256=bfb7607d94b2de7f8530289377f163caad81098afc1fb7ee86191810a6cc6bba -->
## `RDR2.Native.VOICE`

- Kind: class
- Base type: `System.Object`

- `public static System.Void RDR2.Native.VOICE._0x08797A8C03868CB8(System.Single threshold)`
- `public static System.Boolean RDR2.Native.VOICE._0x0DED260A1958A82E(System.Int32 player)`
- `public static System.Void RDR2.Native.VOICE._0x1C38C3577901AF1F()`
- `public static System.Void RDR2.Native.VOICE._0x1FBF7F5BA7E4BE3A(System.Int32 p0)`
- `public static System.Single RDR2.Native.VOICE._0x2F82CAB262C8AE26(System.Int32 player)`
- `public static System.Boolean RDR2.Native.VOICE._0x356135B9B10A2A82(System.UInt64* gamerHandle)`
- `public static System.Void RDR2.Native.VOICE._0x4791899615D70FA2(System.Int32 player, System.Int32 p1, System.Int32 p2)`
- `public static System.Boolean RDR2.Native.VOICE._0x49623BCFC3A3D829(System.Int32 player, System.Boolean muted)`
- `public static System.Void RDR2.Native.VOICE._0x58125B691F6827D5(System.Single proximity)`
- `public static System.Single RDR2.Native.VOICE._0x5CA7FB7D6DE49DCC(System.Int32 player)`
- `public static System.Int32 RDR2.Native.VOICE._0x767931C727DF2ED7(System.Int32 player, System.Int32 p1)`
- `public static System.Void RDR2.Native.VOICE._0x79F478FF5F9F4F05(System.Boolean p0)`
- `public static System.Boolean RDR2.Native.VOICE._0x8E462DB1EAA9C47C(System.Int32 player)`
- `public static System.Boolean RDR2.Native.VOICE._0x919AF2D93E9AA89D(System.Int32 player)`
- `public static System.Boolean RDR2.Native.VOICE._0xAA35FD9ABAB490A3(System.Int32 player)`
- `public static System.Void RDR2.Native.VOICE._0xB3E8841F6BDAF83E()`
- `public static System.Void RDR2.Native.VOICE._0xB6E79850B759A30E(System.Int32 teamId, System.Boolean allow)`
- `public static System.Void RDR2.Native.VOICE._0xB779F4FA19269AEC(System.Int32 p0)`
- `public static System.Boolean RDR2.Native.VOICE._0xCCF71FCFA0070B1A()`
- `public static System.UInt64 RDR2.Native.VOICE._0xDB622ECD3DCBE078(System.Int32 player)`
- `public static System.Void RDR2.Native.VOICE._0xDC9B361CB7776673(System.Int32 player)`
- `public static System.Void RDR2.Native.VOICE._0xEC8703E4536A9952()`
- `public static System.Boolean RDR2.Native.VOICE._0xEF6F2A35FAAF2ED7(System.Int32 player)`
- `public static System.Void RDR2.Native.VOICE._0xF8938CF3984092A5(System.Int32 player)`

## `RDR2.Native.VOLUME`

- Kind: class
- Base type: `System.Object`

- `public static System.Int32 RDR2.Native.VOLUME.CREATE_VOLUME_AGGREGATE()`
- `public static System.Int32 RDR2.Native.VOLUME.CREATE_VOLUME_BOX(RDR2.Math.Vector3 vec, RDR2.Math.Vector3 rotation, RDR2.Math.Vector3 scale)`
- `public static System.Int32 RDR2.Native.VOLUME.CREATE_VOLUME_BOX(System.Single x, System.Single y, System.Single z, System.Single rotX, System.Single rotY, System.Single rotZ, System.Single scaleX, System.Single scaleY, System.Single scaleZ)`
- `public static System.Int32 RDR2.Native.VOLUME.CREATE_VOLUME_CYLINDER(RDR2.Math.Vector3 vec, RDR2.Math.Vector3 rotation, RDR2.Math.Vector3 scale)`
- `public static System.Int32 RDR2.Native.VOLUME.CREATE_VOLUME_CYLINDER(System.Single x, System.Single y, System.Single z, System.Single rotX, System.Single rotY, System.Single rotZ, System.Single scaleX, System.Single scaleY, System.Single scaleZ)`
- `public static System.Int32 RDR2.Native.VOLUME.CREATE_VOLUME_SPHERE(RDR2.Math.Vector3 vec, RDR2.Math.Vector3 rotation, RDR2.Math.Vector3 scale)`
- `public static System.Int32 RDR2.Native.VOLUME.CREATE_VOLUME_SPHERE(System.Single x, System.Single y, System.Single z, System.Single rotX, System.Single rotY, System.Single rotZ, System.Single scaleX, System.Single scaleY, System.Single scaleZ)`
- `public static System.Void RDR2.Native.VOLUME.DELETE_VOLUME(System.Int32 volume)`
- `public static System.Boolean RDR2.Native.VOLUME.DOES_VOLUME_COLLIDE_WITH_ANY_VOLUME_LOCK(RDR2.Math.Vector3 vec, System.Single radius, System.Boolean p4, System.Int32 p5, System.Int32 p6)`
- `public static System.Boolean RDR2.Native.VOLUME.DOES_VOLUME_COLLIDE_WITH_ANY_VOLUME_LOCK(System.Single x, System.Single y, System.Single z, System.Single radius, System.Boolean p4, System.Int32 p5, System.Int32 p6)`
- `public static System.Boolean RDR2.Native.VOLUME.DOES_VOLUME_EXIST(System.Int32 volume)`
- `public static RDR2.Math.Vector3 RDR2.Native.VOLUME.GET_VOLUME_COORDS(System.Int32 volume)`
- `public static System.Int32 RDR2.Native.VOLUME.GET_VOLUME_LOCK_REQUEST_STATUS(System.Int32 volLockRequestId)`
- `public static RDR2.Math.Vector3 RDR2.Native.VOLUME.GET_VOLUME_ROTATION(System.Int32 volume)`
- `public static RDR2.Math.Vector3 RDR2.Native.VOLUME.GET_VOLUME_SCALE(System.Int32 volume)`
- `public static System.Boolean RDR2.Native.VOLUME.IS_POINT_IN_VOLUME(System.Int32 volume, RDR2.Math.Vector3 vec)`
- `public static System.Boolean RDR2.Native.VOLUME.IS_POINT_IN_VOLUME(System.Int32 volume, System.Single x, System.Single y, System.Single z)`
- `public static System.Boolean RDR2.Native.VOLUME.IS_VOLUME_LOCK_REQUEST_VALID(System.Int32 volLockRequestId)`
- `public static System.Int32 RDR2.Native.VOLUME.REQUEST_VOLUME_LOCK(RDR2.Math.Vector3 vec, System.Single radius, System.Int32 p4, System.Int32 p5)`
- `public static System.Int32 RDR2.Native.VOLUME.REQUEST_VOLUME_LOCK(System.Single x, System.Single y, System.Single z, System.Single radius, System.Int32 p4, System.Int32 p5)`
- `public static System.Int32 RDR2.Native.VOLUME.REQUEST_VOLUME_LOCK_WITH_ARGS(System.UInt64* args)`
- `public static System.Boolean RDR2.Native.VOLUME.SET_VOLUME_COORDS(System.Int32 volume, RDR2.Math.Vector3 vec)`
- `public static System.Boolean RDR2.Native.VOLUME.SET_VOLUME_COORDS(System.Int32 volume, System.Single posX, System.Single posY, System.Single posZ)`
- `public static System.Void RDR2.Native.VOLUME.SET_VOLUME_OWNER_PERSISTENT_CHARACTER(System.Int32 volume, System.Int32 persChar, System.Boolean p2)`
- `public static System.Boolean RDR2.Native.VOLUME.SET_VOLUME_ROTATION(System.Int32 volume, RDR2.Math.Vector3 rotation)`
- `public static System.Boolean RDR2.Native.VOLUME.SET_VOLUME_ROTATION(System.Int32 volume, System.Single rotX, System.Single rotY, System.Single rotZ)`
- `public static System.Boolean RDR2.Native.VOLUME.SET_VOLUME_SCALE(System.Int32 volume, RDR2.Math.Vector3 scale)`
- `public static System.Boolean RDR2.Native.VOLUME.SET_VOLUME_SCALE(System.Int32 volume, System.Single scaleX, System.Single scaleY, System.Single scaleZ)`
- `public static System.Void RDR2.Native.VOLUME._0x128FC3A893BF853A(System.UInt64 p0)`
- `public static System.UInt64 RDR2.Native.VOLUME._0x2B32B11520626229(System.UInt64 p0, System.UInt64 p1, System.UInt64 p2, System.UInt64 p3, System.UInt64 p4)`
- `public static System.UInt64 RDR2.Native.VOLUME._0x351D71B8B72B858B(System.UInt64 p0)`
- `public static System.Void RDR2.Native.VOLUME._0x3EFABB21E14A6BD1(System.UInt64 p0, System.UInt64 p1, System.UInt64 p2)`
- `public static System.UInt64 RDR2.Native.VOLUME._0x40F769D31A00D5A0(System.UInt64 p0, System.UInt64 p1)`
- `public static System.Void RDR2.Native.VOLUME._0x4A8FEFC43FD8AC9B(System.UInt64 p0, System.UInt64 p1, System.UInt64 p2)`
- `public static System.UInt64 RDR2.Native.VOLUME._0x51E52C9687FCDEEC(System.UInt64 p0, System.UInt64 p1, System.UInt64 p2, System.UInt64 p3, System.UInt64 p4, System.UInt64 p5, System.UInt64 p6)`
- `public static System.Void RDR2.Native.VOLUME._0x52572B331E693AED(System.UInt64 p0, System.UInt64 p1, System.UInt64 p2)`
- `public static System.Void RDR2.Native.VOLUME._0x53D05D60E5F5B40C(System.UInt64 p0, System.UInt64 p1, System.UInt64 p2, System.UInt64 p3)`
- `public static System.Void RDR2.Native.VOLUME._0x695DAC2DB928F308(System.UInt64 p0, System.UInt64 p1)`
- `public static System.Void RDR2.Native.VOLUME._0x6D5F9E69BA1BE783(System.UInt64 p0)`
- `public static System.Void RDR2.Native.VOLUME._0x748C5F51A18CB8F0(System.UInt64 p0)`
- `public static System.UInt64 RDR2.Native.VOLUME._0x7FD78DFD0C5D7B9B(System.UInt64 p0)`
- `public static System.UInt64 RDR2.Native.VOLUME._0x870E9981ED27C815(System.UInt64 p0, System.UInt64 p1, System.UInt64 p2, System.UInt64 p3, System.UInt64 p4, System.UInt64 p5)`
- `public static System.Void RDR2.Native.VOLUME._0x998202B206872672(System.UInt64 p0)`
- `public static System.UInt64 RDR2.Native.VOLUME._0xAA9EE2AAFC717623(System.UInt64 p0, System.UInt64 p1, System.UInt64 p2, System.UInt64 p3, System.UInt64 p4, System.UInt64 p5)`
- `public static System.Void RDR2.Native.VOLUME._0xAC355980681A7F89(System.UInt64 p0)`
- `public static System.Void RDR2.Native.VOLUME._0xB440F4E35393FC39(System.UInt64 p0, System.UInt64 p1)`
- `public static System.Void RDR2.Native.VOLUME._0xB469CFD9E065EB99(System.UInt64 p0, System.UInt64 p1)`
- `public static RDR2.Math.Vector3 RDR2.Native.VOLUME._0xC4019CF9AE8E931A(System.UInt64 p0)`
- `public static System.Void RDR2.Native.VOLUME._0xC61E2FD926DBB406()`
- `public static System.UInt64 RDR2.Native.VOLUME._0xCA5C90D40665D5CE(System.UInt64 p0, System.UInt64 p1)`
- `public static System.Void RDR2.Native.VOLUME._0xD460135C98940274(System.UInt64 p0, System.UInt64 p1)`
- `public static System.Void RDR2.Native.VOLUME._0xD4FA73FE628FEC63(System.UInt64 p0, System.UInt64 p1)`
- `public static System.Void RDR2.Native.VOLUME._0xD52DF30355EA7C8E(System.UInt64 p0, System.UInt64 p1, System.UInt64 p2)`
- `public static RDR2.Math.Vector3 RDR2.Native.VOLUME._0xD882C5B3991575B7(System.UInt64 p0, System.UInt64 p1, System.UInt64 p2, System.UInt64 p3, System.UInt64 p4)`
- `public static System.Void RDR2.Native.VOLUME._0xEBA87B9273835CF3(System.UInt64 p0, System.UInt64 p1)`
- `public static System.UInt64 RDR2.Native.VOLUME._0xEE1D6FF54CAF7714(System.UInt64 p0, System.UInt64 p1)`
- `public static System.Void RDR2.Native.VOLUME._0xF3A2FBA5985C8CD5(System.UInt64 p0, System.UInt64 p1, System.UInt64 p2, System.UInt64 p3)`
- `public static System.UInt64 RDR2.Native.VOLUME._0xF6CE6F9C3897804E(System.UInt64 p0)`
- `public static System.UInt64 RDR2.Native.VOLUME._0xF6F5447D418DAA82(System.UInt64 p0)`
- `public static System.Void RDR2.Native.VOLUME._0xFA15C9A320E707B0()`
- `public static System.Void RDR2.Native.VOLUME._ADD_BOUNDS_TO_AGGREGATE_VOLUME(System.Int32 volume, System.Int32 aggregate)`
- `public static System.Void RDR2.Native.VOLUME._ADD_BOX_VOLUME_TO_VOLUME_AGGREGATE(System.Int32 aggregate, System.Single p1, System.Single p2, System.Single p3, System.Single p4, System.Single p5, System.Single p6, System.Single p7, System.Single p8, System.Single p9)`
- `public static System.Void RDR2.Native.VOLUME._ADD_CYLINDER_VOLUME_TO_VOLUME_AGGREGATE(System.Int32 aggregate, System.Single p1, System.Single p2, System.Single p3, System.Single p4, System.Single p5, System.Single p6, System.Single p7, System.Single p8, System.Single p9)`
- `public static System.Boolean RDR2.Native.VOLUME._ADD_ENTRY_VOLUME_LOCK(System.UInt64* args)`
- `public static System.Void RDR2.Native.VOLUME._ADD_SPHERE_VOLUME_TO_VOLUME_AGGREGATE(System.Int32 aggregate, System.Single p1, System.Single p2, System.Single p3, System.Single p4, System.Single p5, System.Single p6, System.Single p7, System.Single p8, System.Single p9)`
- `public static System.Void RDR2.Native.VOLUME._ADD_VOLUME_TO_VOLUME_AGGREGATE(System.Int32 aggregate, System.UInt32 typeHash, RDR2.Math.Vector3 vec, RDR2.Math.Vector3 rotation, RDR2.Math.Vector3 scale)`
- `public static System.Void RDR2.Native.VOLUME._ADD_VOLUME_TO_VOLUME_AGGREGATE(System.Int32 aggregate, System.UInt32 typeHash, System.Single x, System.Single y, System.Single z, System.Single rotX, System.Single rotY, System.Single rotZ, System.Single scaleX, System.Single scaleY, System.Single scaleZ)`
- `public static System.Int32 RDR2.Native.VOLUME._CREATE_ANTI_GRIEF_VOLUME(System.UInt32 volumeType, RDR2.Math.Vector3 vec, RDR2.Math.Vector3 rotation, RDR2.Math.Vector3 scale)`
- `public static System.Int32 RDR2.Native.VOLUME._CREATE_ANTI_GRIEF_VOLUME(System.UInt32 volumeType, System.Single x, System.Single y, System.Single z, System.Single rotX, System.Single rotY, System.Single rotZ, System.Single scaleX, System.Single scaleY, System.Single scaleZ)`
- `public static System.Int32 RDR2.Native.VOLUME._CREATE_SPEED_VOLUME(System.UInt64 p0, System.UInt64 p1, System.UInt64 p2, System.UInt64 p3, System.UInt64 p4, System.UInt64 p5, System.UInt64 p6, System.UInt64 p7, System.UInt64 p8, System.UInt64 p9, System.UInt64 p10, System.UInt64 p11, System.UInt64 p12, System.UInt64 p13, System.UInt64 p14)`
- `public static System.Int32 RDR2.Native.VOLUME._CREATE_VOLUME_AGGREGATE_WITH_CUSTOM_NAME(System.String name)`
- `public static System.Int32 RDR2.Native.VOLUME._CREATE_VOLUME_BOX_WITH_CUSTOM_NAME(RDR2.Math.Vector3 vec, RDR2.Math.Vector3 rotation, RDR2.Math.Vector3 scale, System.String name)`
- `public static System.Int32 RDR2.Native.VOLUME._CREATE_VOLUME_BOX_WITH_CUSTOM_NAME(System.Single x, System.Single y, System.Single z, System.Single rotX, System.Single rotY, System.Single rotZ, System.Single scaleX, System.Single scaleY, System.Single scaleZ, System.String name)`
- `public static System.Int32 RDR2.Native.VOLUME._CREATE_VOLUME_BY_HASH(System.UInt32 volumeType, RDR2.Math.Vector3 vec, RDR2.Math.Vector3 rotation, RDR2.Math.Vector3 scale)`
- `public static System.Int32 RDR2.Native.VOLUME._CREATE_VOLUME_BY_HASH(System.UInt32 volumeType, System.Single x, System.Single y, System.Single z, System.Single rotX, System.Single rotY, System.Single rotZ, System.Single scaleX, System.Single scaleY, System.Single scaleZ)`
- `public static System.Int32 RDR2.Native.VOLUME._CREATE_VOLUME_BY_HASH_WITH_CUSTOM_NAME(System.UInt32 volumeType, RDR2.Math.Vector3 vec, RDR2.Math.Vector3 rotation, RDR2.Math.Vector3 scale, System.String name)`
- `public static System.Int32 RDR2.Native.VOLUME._CREATE_VOLUME_BY_HASH_WITH_CUSTOM_NAME(System.UInt32 volumeType, System.Single x, System.Single y, System.Single z, System.Single rotX, System.Single rotY, System.Single rotZ, System.Single scaleX, System.Single scaleY, System.Single scaleZ, System.String name)`
- `public static System.Int32 RDR2.Native.VOLUME._CREATE_VOLUME_CYLINDER_WITH_CUSTOM_NAME(RDR2.Math.Vector3 vec, RDR2.Math.Vector3 rotation, RDR2.Math.Vector3 scale, System.String name)`
- `public static System.Int32 RDR2.Native.VOLUME._CREATE_VOLUME_CYLINDER_WITH_CUSTOM_NAME(System.Single x, System.Single y, System.Single z, System.Single rotX, System.Single rotY, System.Single rotZ, System.Single scaleX, System.Single scaleY, System.Single scaleZ, System.String name)`
- `public static System.Int32 RDR2.Native.VOLUME._CREATE_VOLUME_LOCK(RDR2.Math.Vector3 vec, System.Single radius, System.Int32 flag, System.UInt64 p5)`
- `public static System.Int32 RDR2.Native.VOLUME._CREATE_VOLUME_LOCK(System.Single x, System.Single y, System.Single z, System.Single radius, System.Int32 flag, System.UInt64 p5)`
- `public static System.Int32 RDR2.Native.VOLUME._CREATE_VOLUME_LOCK_ATTACHED_TO_ENTITY(System.Int32 entity, System.Single radius, System.Int32 flag, System.UInt64 p3)`
- `public static System.Int32 RDR2.Native.VOLUME._CREATE_VOLUME_SPHERE_WITH_CUSTOM_NAME(RDR2.Math.Vector3 vec, RDR2.Math.Vector3 rotation, RDR2.Math.Vector3 scale, System.String name)`
- `public static System.Int32 RDR2.Native.VOLUME._CREATE_VOLUME_SPHERE_WITH_CUSTOM_NAME(System.Single x, System.Single y, System.Single z, System.Single rotX, System.Single rotY, System.Single rotZ, System.Single scaleX, System.Single scaleY, System.Single scaleZ, System.String name)`
- `public static System.Int32 RDR2.Native.VOLUME._CREATE_WALK_AND_TALK_VOLUME(System.UInt64 p0, System.UInt64 p1, System.UInt64 p2, System.UInt64 p3, System.UInt64 p4, System.UInt64 p5, System.UInt64 p6, System.UInt64 p7, System.UInt64 p8, System.UInt64 p9, System.UInt64 p10, System.UInt64 p11, System.UInt64 p12)`
- `public static System.Int32 RDR2.Native.VOLUME._FIND_VOLUME_LOCK_REQUEST_ID_WITH_ARGS(System.UInt64* args)`
- `public static System.Void RDR2.Native.VOLUME._GET_VOLUME_BOUNDS(System.Int32 volume, RDR2.Math.Vector3* min, RDR2.Math.Vector3* max)`
- `public static System.UInt32 RDR2.Native.VOLUME._GET_VOLUME_RELATIONSHIP(System.Int32 volume)`
- `public static System.Boolean RDR2.Native.VOLUME._IS_AGGREGATE_VOLUME(System.Int32 volume)`
- `public static System.Boolean RDR2.Native.VOLUME._IS_POINT_NEAR_VOLUME_LOCK_CENTER(RDR2.Math.Vector3 vec, System.Single radius, System.Int32 p4, System.Int32 p5, System.Int32 flags)`
- `public static System.Boolean RDR2.Native.VOLUME._IS_POINT_NEAR_VOLUME_LOCK_CENTER(System.Single x, System.Single y, System.Single z, System.Single radius, System.Int32 p4, System.Int32 p5, System.Int32 flags)`
- `public static System.Boolean RDR2.Native.VOLUME._IS_VOLUME_LOCK_REQUEST_VALID_2(System.Int32 volLockRequestId)`
- `public static System.Void RDR2.Native.VOLUME._MODIFY_VOLUME_LOCK_LOCATION(System.Int32 volLock, RDR2.Math.Vector3 vec)`
- `public static System.Void RDR2.Native.VOLUME._MODIFY_VOLUME_LOCK_LOCATION(System.Int32 volLock, System.Single x, System.Single y, System.Single z)`
- `public static System.Void RDR2.Native.VOLUME._RELEASE_LOCK_VOLUME(System.Int32 volLockRequestId)`
- `public static System.Void RDR2.Native.VOLUME._REMOVE_BOUNDS_FROM_AGGREGATE_VOLUME(System.Int32 volume, System.Int32 aggregate)`
- `public static System.Void RDR2.Native.VOLUME._SET_ANTI_GRIEF_VOLUME_BLOCKS_HORSE(System.Int32 volume, System.Boolean toggle)`
- `public static System.Void RDR2.Native.VOLUME._SET_ANTI_GRIEF_VOLUME_BLOCKS_PLAYER(System.Int32 volume, System.Boolean toggle)`
- `public static System.Void RDR2.Native.VOLUME._SET_VOLUME_RELATIONSHIP(System.Int32 volume, System.UInt32 relationshipGroup)`

## `RDR2.Native.WATER`

- Kind: class
- Base type: `System.Object`

- `public static System.Void RDR2.Native.WATER.DISABLE_WATER_LOOKUP()`
- `public static System.Void RDR2.Native.WATER.ENABLE_WATER_LOOKUP()`
- `public static System.Boolean RDR2.Native.WATER.GET_WATER_HEIGHT(RDR2.Math.Vector3 vec, System.Single* height)`
- `public static System.Boolean RDR2.Native.WATER.GET_WATER_HEIGHT(System.Single x, System.Single y, System.Single z, System.Single* height)`
- `public static System.Boolean RDR2.Native.WATER.GET_WATER_HEIGHT_NO_WAVES(RDR2.Math.Vector3 vec, System.Single* height)`
- `public static System.Boolean RDR2.Native.WATER.GET_WATER_HEIGHT_NO_WAVES(System.Single x, System.Single y, System.Single z, System.Single* height)`
- `public static System.Void RDR2.Native.WATER.REMOVE_EXTRA_CALMING_QUAD(System.Int32 index)`
- `public static System.Int32 RDR2.Native.WATER.TEST_PROBE_AGAINST_ALL_WATER(RDR2.Math.Vector3 vec1, RDR2.Math.Vector3 vec2, System.Int32 flags, System.Single* waterHeight)`
- `public static System.Int32 RDR2.Native.WATER.TEST_PROBE_AGAINST_ALL_WATER(System.Single x1, System.Single y1, System.Single z1, System.Single x2, System.Single y2, System.Single z2, System.Int32 flags, System.Single* waterHeight)`
- `public static System.Int32 RDR2.Native.WATER.TEST_VERTICAL_PROBE_AGAINST_ALL_WATER(RDR2.Math.Vector3 vec, System.Int32 flags, System.Single* waterHeight)`
- `public static System.Int32 RDR2.Native.WATER.TEST_VERTICAL_PROBE_AGAINST_ALL_WATER(System.Single x, System.Single y, System.Single z, System.Int32 flags, System.Single* waterHeight)`
- `public static System.Void RDR2.Native.WATER._0x09A1C7DFDCE54FBC(System.Int32 p0)`
- `public static System.Void RDR2.Native.WATER._0x0DCEC6A92E497E17(System.Int32 entity, System.Int32 p1)`
- `public static System.Void RDR2.Native.WATER._0xA33F5069B0CB89B8()`
- `public static System.Void RDR2.Native.WATER._0xB34A6009A0DB80B8(System.Int32 entity)`
- `public static System.Void RDR2.Native.WATER._0xC63540AEF8384769()`
- `public static System.Void RDR2.Native.WATER._0xE8126623008372AA()`
- `public static System.Void RDR2.Native.WATER._0xF0FBF193F1F5C0EA(System.Int32 ped)`
- `public static System.Int32 RDR2.Native.WATER._GET_WORLD_WATER_TYPE()`
- `public static System.Void RDR2.Native.WATER._SET_OCEAN_GUARMA_WATER_QUADRANT(System.Single wavesHeight, System.Single p1, System.Int32 p2, System.Single p3, System.Single p4, System.Single p5, System.Single p6, System.Single wavesStrength, System.Int32 p8)`
- `public static System.Void RDR2.Native.WATER._SET_WORLD_WATER_TYPE(System.Int32 waterType)`

## `RDR2.Native.WEAPON`

- Kind: class
- Base type: `System.Object`

- `public static System.Boolean RDR2.Native.WEAPON.GET_ALLOW_DUAL_WIELD(System.Int32 ped)`
- `public static System.Boolean RDR2.Native.WEAPON.GET_AMMO_IN_CLIP(System.Int32 ped, System.Int32* ammo, System.UInt32 weaponHash)`
- `public static System.Int32 RDR2.Native.WEAPON.GET_AMMO_IN_PED_WEAPON(System.Int32 ped, System.UInt32 weaponHash)`
- `public static System.Void RDR2.Native.WEAPON.GET_BEST_PED_SHORTARM_GUID(System.Int32 ped, System.UInt64* outGUID, System.Boolean p2, System.Boolean p3)`
- `public static System.UInt32 RDR2.Native.WEAPON.GET_BEST_PED_WEAPON(System.Int32 ped, System.Boolean p1, System.Boolean p2)`
- `public static System.Boolean RDR2.Native.WEAPON.GET_CURRENT_PED_VEHICLE_WEAPON(System.Int32 ped, System.UInt32* weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON.GET_CURRENT_PED_WEAPON(System.Int32 ped, System.UInt32* weaponHash, System.Boolean p2, System.Int32 attachPoint, System.Boolean p4)`
- `public static System.Int32 RDR2.Native.WEAPON.GET_CURRENT_PED_WEAPON_ENTITY_INDEX(System.Int32 ped, System.Int32 attachPoint)`
- `public static System.Boolean RDR2.Native.WEAPON.GET_MAX_AMMO(System.Int32 ped, System.Int32* ammo, System.UInt32 weaponHash)`
- `public static System.Int32 RDR2.Native.WEAPON.GET_MAX_AMMO_IN_CLIP(System.Int32 ped, System.UInt32 weaponHash, System.Boolean p2)`
- `public static System.Int32 RDR2.Native.WEAPON.GET_PED_AMMO_BY_TYPE(System.Int32 ped, System.UInt32 ammoType)`
- `public static System.UInt32 RDR2.Native.WEAPON.GET_PED_AMMO_TYPE_FROM_WEAPON(System.Int32 ped, System.UInt32 weaponHash)`
- `public static System.UInt32 RDR2.Native.WEAPON.GET_PED_BACKUP_WEAPON(System.Int32 ped, System.Boolean p1)`
- `public static System.Boolean RDR2.Native.WEAPON.GET_PED_LAST_WEAPON_IMPACT_COORD(System.Int32 ped, RDR2.Math.Vector3* coords)`
- `public static System.Boolean RDR2.Native.WEAPON.GET_PED_WEAPON_GUID_AT_ATTACH_POINT(System.Int32 ped, System.Int32 attachPoint, System.UInt64* weaponGuid)`
- `public static System.UInt32 RDR2.Native.WEAPON.GET_WEAPONTYPE_GROUP(System.UInt32 weaponHash)`
- `public static System.Int32 RDR2.Native.WEAPON.GET_WEAPON_CLIP_SIZE(System.UInt32 weaponHash)`
- `public static System.Single RDR2.Native.WEAPON.GET_WEAPON_DEGRADATION(System.Int32 weaponObject)`
- `public static System.Single RDR2.Native.WEAPON.GET_WEAPON_PERMANENT_DEGRADATION(System.Int32 weaponObject)`
- `public static System.Void RDR2.Native.WEAPON.GIVE_DELAYED_WEAPON_TO_PED(System.Int32 ped, System.UInt32 weaponHash, System.Int32 ammoCount, System.Boolean p3, System.UInt32 addReason)`
- `public static System.UInt32 RDR2.Native.WEAPON.GIVE_WEAPON_TO_PED(System.Int32 ped, System.UInt32 weaponHash, System.Int32 ammoCount, System.Boolean bForceInHand, System.Boolean bForceInHolster, System.Int32 attachPoint, System.Boolean bAllowMultipleCopies, System.Single p7, System.Single p8, System.UInt32 addReason, System.Boolean bIgnoreUnlocks, System.Single permanentDegradation, System.Boolean p12)`
- `public static System.Boolean RDR2.Native.WEAPON.GIVE_WEAPON_TO_PED_WITH_OPTIONS(System.Int32 ped, System.UInt64* data, System.UInt64* outData)`
- `public static System.Boolean RDR2.Native.WEAPON.HAS_PED_GOT_WEAPON(System.Int32 ped, System.UInt32 weaponHash, System.Int32 p2, System.Boolean p3)`
- `public static System.Boolean RDR2.Native.WEAPON.HAS_WEAPON_GOT_WEAPON_COMPONENT(System.Int32 weapon, System.UInt32 addonHash)`
- `public static System.Void RDR2.Native.WEAPON.HIDE_PED_WEAPON_FOR_SCRIPTED_CUTSCENE(System.Int32 ped, System.Boolean toggle)`
- `public static System.Boolean RDR2.Native.WEAPON.IS_PED_ARMED(System.Int32 ped, System.Int32 flags)`
- `public static System.Boolean RDR2.Native.WEAPON.IS_PED_CARRYING_WEAPON(System.Int32 ped, System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON.IS_PED_WEAPON_READY_TO_SHOOT(System.Int32 ped)`
- `public static System.Boolean RDR2.Native.WEAPON.IS_WEAPON_A_GUN(System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON.IS_WEAPON_BOW(System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON.IS_WEAPON_MELEE_WEAPON(System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON.IS_WEAPON_PISTOL(System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON.IS_WEAPON_REPEATER(System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON.IS_WEAPON_REVOLVER(System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON.IS_WEAPON_RIFLE(System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON.IS_WEAPON_SHOTGUN(System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON.IS_WEAPON_VALID(System.UInt32 weaponHash)`
- `public static System.Int32 RDR2.Native.WEAPON.MAKE_PED_DROP_WEAPON(System.Int32 ped, System.Boolean p1, System.Int32 attachPoint, System.Boolean p3, System.Boolean p4)`
- `public static System.Void RDR2.Native.WEAPON.REMOVE_ALL_PED_WEAPONS(System.Int32 ped, System.Boolean p1, System.Boolean p2)`
- `public static System.Void RDR2.Native.WEAPON.REMOVE_WEAPON_COMPONENT_FROM_WEAPON_OBJECT(System.Int32 weaponObject, System.UInt32 component)`
- `public static System.Void RDR2.Native.WEAPON.REMOVE_WEAPON_FROM_PED(System.Int32 ped, System.UInt32 weaponHash, System.Boolean p2, System.UInt32 removeReason)`
- `public static System.Void RDR2.Native.WEAPON.SET_ALLOW_ANY_WEAPON_DROP(System.Int32 ped, System.Boolean toggle)`
- `public static System.Boolean RDR2.Native.WEAPON.SET_AMMO_IN_CLIP(System.Int32 ped, System.UInt32 weaponHash, System.Int32 ammo)`
- `public static System.Boolean RDR2.Native.WEAPON.SET_CURRENT_PED_VEHICLE_WEAPON(System.Int32 ped, System.UInt32 weaponHash)`
- `public static System.Void RDR2.Native.WEAPON.SET_CURRENT_PED_WEAPON(System.Int32 ped, System.UInt32 weaponHash, System.Boolean equipNow, System.Int32 attachPoint, System.Boolean p4, System.Boolean p5)`
- `public static System.Void RDR2.Native.WEAPON.SET_CURRENT_PED_WEAPON_BY_GUID(System.Int32 ped, System.UInt64* weaponUid, System.Boolean p2, System.Boolean p3, System.Boolean p4, System.Boolean p5)`
- `public static System.Void RDR2.Native.WEAPON.SET_INSTANTLY_EQUIP_WEAPON_PICKUPS(System.Int32 ped, System.Boolean toggle)`
- `public static System.Void RDR2.Native.WEAPON.SET_PED_AMMO(System.Int32 ped, System.UInt32 weaponHash, System.Int32 ammo)`
- `public static System.Void RDR2.Native.WEAPON.SET_PED_AMMO_BY_TYPE(System.Int32 ped, System.UInt32 ammoType, System.Int32 ammo)`
- `public static System.Void RDR2.Native.WEAPON.SET_PED_AMMO_TO_DROP(System.Int32 ped, System.Int32 p1, System.Int32 p2)`
- `public static System.Void RDR2.Native.WEAPON.SET_PED_CURRENT_WEAPON_VISIBLE(System.Int32 ped, System.Boolean visible, System.Boolean deselectWeapon, System.Boolean p3, System.Boolean p4)`
- `public static System.Void RDR2.Native.WEAPON.SET_PED_DROPS_INVENTORY_WEAPON(System.Int32 ped, System.UInt32 weaponHash, RDR2.Math.Vector3 offset, System.Int32 ammoCount)`
- `public static System.Void RDR2.Native.WEAPON.SET_PED_DROPS_INVENTORY_WEAPON(System.Int32 ped, System.UInt32 weaponHash, System.Single xOffset, System.Single yOffset, System.Single zOffset, System.Int32 ammoCount)`
- `public static System.Void RDR2.Native.WEAPON.SET_PED_DROPS_WEAPONS_WHEN_DEAD(System.Int32 ped, System.Boolean toggle)`
- `public static System.Void RDR2.Native.WEAPON.SET_PED_INFINITE_AMMO(System.Int32 ped, System.Boolean toggle, System.UInt32 weaponHash)`
- `public static System.Void RDR2.Native.WEAPON.SET_PLAYER_PED_QUICK_SWAP_WEAPON_BY_GUID(System.Int32 ped, System.UInt64* guidPrimary, System.UInt64* guidSecondary)`
- `public static System.Void RDR2.Native.WEAPON.SET_VEHICLE_WEAPON_HEADING(System.Int32 vehicle, System.Int32 seatIndex, System.Single heading, System.Boolean p3)`
- `public static System.Boolean RDR2.Native.WEAPON.SHOULD_WEAPON_BE_DISCARDED_WHEN_SWAPPED(System.UInt32 weaponHash)`
- `public static System.Void RDR2.Native.WEAPON._0x000FA7A4A8443AF7(System.UInt64 p0)`
- `public static System.UInt64 RDR2.Native.WEAPON._0x07E1C35F0078C3F9(System.UInt64 p0, System.UInt64 p1)`
- `public static System.Boolean RDR2.Native.WEAPON._0x0DE0944ECCB3DF5D(System.UInt64 p0)`
- `public static System.Void RDR2.Native.WEAPON._0x14FF0C2545527F9B(System.Int32 horse, System.UInt32 weaponHash, System.Int32 ped)`
- `public static System.Void RDR2.Native.WEAPON._0x16D9841A85FA627E(System.Int32 ped, System.Boolean toggle)`
- `public static System.Void RDR2.Native.WEAPON._0x183CE355115B6E75(System.UInt64 p0, System.UInt64 p1)`
- `public static System.Boolean RDR2.Native.WEAPON._0x2387D6E9C6B478AA(System.Int32 ped)`
- `public static System.UInt64 RDR2.Native.WEAPON._0x23BF601A42F329A0(System.UInt64 p0)`
- `public static System.Void RDR2.Native.WEAPON._0x2EBF70E1D8C06683(System.Int32 ped, System.UInt32 p1)`
- `public static System.UInt64 RDR2.Native.WEAPON._0x3799EFCC3C8CD5E1(System.UInt64 p0)`
- `public static System.Void RDR2.Native.WEAPON._0x404514D231DB27A0(System.UInt64 p0, System.UInt64 p1)`
- `public static System.Void RDR2.Native.WEAPON._0x431240A58484D5D0(System.Int32 ped, System.Boolean toggle)`
- `public static System.Boolean RDR2.Native.WEAPON._0x44C8F4908F1B2622(System.Int32 ped, System.UInt32 ammoHash)`
- `public static System.Void RDR2.Native.WEAPON._0x457B16951AD77C1B(System.UInt64 p0)`
- `public static System.Void RDR2.Native.WEAPON._0x45E57FDD531C9477(System.Int32 ped, System.Boolean toggle)`
- `public static System.UInt64 RDR2.Native.WEAPON._0x46D42883E873C1D7(System.Int32 ped)`
- `public static System.Void RDR2.Native.WEAPON._0x4820A6939D7CEF28(System.UInt64 p0, System.UInt64 p1)`
- `public static System.UInt64 RDR2.Native.WEAPON._0x4823F13A21F51964(System.UInt64 p0, System.UInt64 p1)`
- `public static System.UInt64 RDR2.Native.WEAPON._0x486C96A0DCD2BC92(System.UInt64 p0, System.UInt64 p1)`
- `public static System.UInt64 RDR2.Native.WEAPON._0x495A04CAEC263AF8(System.UInt64 p0, System.UInt64 p1)`
- `public static System.UInt64 RDR2.Native.WEAPON._0x58425FCA3D3A2D15(System.UInt64 p0)`
- `public static System.UInt64 RDR2.Native.WEAPON._0x5A695BD328586B44(System.UInt64 p0, System.UInt64 p1)`
- `public static System.UInt64 RDR2.Native.WEAPON._0x5B235F24472F2C3B(System.UInt64 p0, System.UInt64 p1)`
- `public static System.Void RDR2.Native.WEAPON._0x63B83A526329AFBC(System.UInt64 p0)`
- `public static System.Void RDR2.Native.WEAPON._0x641351E9AD103890(System.UInt64 p0, System.UInt64 p1)`
- `public static System.Int32 RDR2.Native.WEAPON._0x65DC4AC5B96614CB(System.UInt32 weaponHash)`
- `public static System.Void RDR2.Native.WEAPON._0x74C2365FDD1BB48F(System.UInt64 p0, System.UInt64 p1)`
- `public static System.UInt64 RDR2.Native.WEAPON._0x74C8000FDD1BB111(System.UInt64 p0, System.UInt64 p1)`
- `public static System.UInt64 RDR2.Native.WEAPON._0x74C8000FDD1BB222(System.UInt64 p0, System.UInt64 p1)`
- `public static System.Void RDR2.Native.WEAPON._0x74C9080FDD1BB48E(System.UInt64 p0, System.UInt64 p1)`
- `public static System.Void RDR2.Native.WEAPON._0x74C9080FDD1BB48F(System.UInt64 p0, System.UInt64 p1)`
- `public static System.Void RDR2.Native.WEAPON._0x74C90AAACC1DD48F(System.UInt64 p0)`
- `public static System.UInt64 RDR2.Native.WEAPON._0x80BB243789008A82(System.UInt64 p0, System.UInt64 p1)`
- `public static System.Void RDR2.Native.WEAPON._0x8A779706DA5CA3DD(System.Int32 ped, System.Boolean p1, System.Int32 p2)`
- `public static System.Void RDR2.Native.WEAPON._0x9409C62504A8F9E9(System.Int32 vehicle, System.Boolean p1)`
- `public static System.UInt64 RDR2.Native.WEAPON._0x95CA12E2C68043E5(System.UInt64 p0, System.UInt64 p1)`
- `public static System.UInt64 RDR2.Native.WEAPON._0x9CCA3131E6B53C68(System.UInt64 p0, System.UInt64 p1, System.UInt64 p2)`
- `public static System.UInt32 RDR2.Native.WEAPON._0x9EEFD670F10656D7(System.UInt32 weaponCollection, System.UInt32 weaponGroup)`
- `public static System.UInt64 RDR2.Native.WEAPON._0x9F0E1892C7F228A8(System.UInt64 p0)`
- `public static System.UInt64 RDR2.Native.WEAPON._0xA2091482ED42EF85(System.UInt64 p0, System.UInt64 p1)`
- `public static System.Void RDR2.Native.WEAPON._0xA3716A77DCF17424(System.UInt64 p0, System.UInt64 p1, System.UInt64 p2)`
- `public static System.Void RDR2.Native.WEAPON._0xA769D753922B031B(System.UInt64 p0, System.UInt64 p1, System.UInt64 p2)`
- `public static System.UInt64 RDR2.Native.WEAPON._0xABC18A28BAD4B46F(System.UInt64 p0, System.UInt64 p1)`
- `public static System.UInt64 RDR2.Native.WEAPON._0xAFFD0CCF31F469B8(System.UInt64 p0)`
- `public static System.Void RDR2.Native.WEAPON._0xB0FB9B196A3D13F0(System.UInt64 p0, System.UInt64 p1, System.UInt64 p2)`
- `public static System.Void RDR2.Native.WEAPON._0xB832F1A686B9B810(System.UInt64 p0, System.UInt64 p1, System.UInt64 p2)`
- `public static System.UInt64 RDR2.Native.WEAPON._0xBC9444F2FF94A9C0(System.UInt64 p0)`
- `public static System.UInt64 RDR2.Native.WEAPON._0xBFCA7AFABF9D7967(System.UInt64 p0, System.UInt64 p1)`
- `public static System.Void RDR2.Native.WEAPON._0xC5899C4CD2E2495D(System.UInt64 p0)`
- `public static System.UInt64 RDR2.Native.WEAPON._0xD2209866B0CB72EA(System.UInt64 p0, System.UInt64 p1)`
- `public static System.Void RDR2.Native.WEAPON._0xD4C6E24D955FF061(System.UInt64 p0)`
- `public static System.Void RDR2.Native.WEAPON._0xD53846B9C931C181(System.UInt64 p0, System.UInt64 p1, System.UInt64 p2)`
- `public static System.Void RDR2.Native.WEAPON._0xD63B4BA3A02A99E0(System.UInt64 p0, System.UInt64 p1)`
- `public static System.Void RDR2.Native.WEAPON._0xE9B3FEC825668291(System.UInt64 p0, System.UInt64 p1, System.UInt64 p2)`
- `public static System.UInt64 RDR2.Native.WEAPON._0xEA522F991E120D45(System.UInt64 p0)`
- `public static System.UInt64 RDR2.Native.WEAPON._0xEC97101A8F311282(System.UInt64 p0)`
- `public static System.Void RDR2.Native.WEAPON._0xECBB26529A737EF6(System.UInt64 p0)`
- `public static System.Void RDR2.Native.WEAPON._0xF08D8FEB455F2C8C(System.Int32 ped, System.Boolean toggle)`
- `public static System.Boolean RDR2.Native.WEAPON._0xF252A85B8F3F8C58(System.UInt32 weaponCollection, System.UInt32 dualwieldVariant)`
- `public static System.UInt64 RDR2.Native.WEAPON._0xF2F585411E748B9C(System.UInt64 p0, System.UInt64 p1)`
- `public static System.UInt32 RDR2.Native.WEAPON._0xF8204EF17410BF43(System.UInt32 weaponGroupHash, System.Single p1, System.Single p2, System.UInt64 p3)`
- `public static System.Void RDR2.Native.WEAPON._ADD_AMMO_TO_PED(System.Int32 ped, System.UInt32 weaponHash, System.Int32 amount, System.UInt32 addReason)`
- `public static System.Void RDR2.Native.WEAPON._ADD_AMMO_TO_PED_BY_TYPE(System.Int32 ped, System.UInt32 ammoType, System.Int32 amount, System.UInt32 addReason)`
- `public static System.Void RDR2.Native.WEAPON._CLEAR_PED_LAST_WEAPON_DAMAGE(System.Int32 ped)`
- `public static System.Int32 RDR2.Native.WEAPON._CREATE_WEAPON_OBJECT(System.UInt32 weaponHash, System.Int32 ammoCount, RDR2.Math.Vector3 vec, System.Boolean showWorldModel, System.Single scale)`
- `public static System.Int32 RDR2.Native.WEAPON._CREATE_WEAPON_OBJECT(System.UInt32 weaponHash, System.Int32 ammoCount, System.Single x, System.Single y, System.Single z, System.Boolean showWorldModel, System.Single scale)`
- `public static System.Void RDR2.Native.WEAPON._DISABLE_AMMO_TYPE_FOR_PED(System.Int32 ped, System.UInt32 ammoHash)`
- `public static System.Void RDR2.Native.WEAPON._DISABLE_AMMO_TYPE_FOR_PED_WEAPON(System.Int32 ped, System.UInt32 weaponHash, System.UInt32 ammoHash)`
- `public static System.Void RDR2.Native.WEAPON._ENABLE_AMMO_TYPE_FOR_PED(System.Int32 ped, System.UInt32 weaponHash)`
- `public static System.Void RDR2.Native.WEAPON._ENABLE_AMMO_TYPE_FOR_PED_WEAPON(System.Int32 ped, System.UInt32 weaponHash, System.UInt32 ammoHash)`
- `public static System.Boolean RDR2.Native.WEAPON._ENABLE_WEAPON_RESTORE(System.Int32 ped)`
- `public static System.Boolean RDR2.Native.WEAPON._GET_AMMO_IN_CLIP_BY_INVENTORY_UID(System.Int32 ped, System.Int32* ammo, System.UInt64* inventoryUid)`
- `public static System.UInt32 RDR2.Native.WEAPON._GET_AMMO_TYPE_FOR_WEAPON(System.UInt32 weaponHash)`
- `public static System.UInt32 RDR2.Native.WEAPON._GET_BEST_PED_WEAPON_IN_GROUP(System.Int32 ped, System.UInt32 weaponGroup, System.Boolean p2, System.Boolean p3)`
- `public static System.UInt64 RDR2.Native.WEAPON._GET_BEST_PED_WEAPON_IN_INVENTORY(System.Int32 ped, System.UInt64 p1, System.UInt64* guidPrimary)`
- `public static System.Boolean RDR2.Native.WEAPON._GET_CAN_TWIRL_WEAPON(System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON._GET_CORRECT_KIT_EMOTE_TWIRL_GUN(System.Int32 ped, System.UInt64* weaponGuid)`
- `public static System.UInt32 RDR2.Native.WEAPON._GET_CURRENT_AMMO_TYPE_FROM_GUID(System.Int32 ped, System.UInt64* weaponGuid)`
- `public static System.UInt32 RDR2.Native.WEAPON._GET_CURRENT_PED_WEAPON_AMMO_TYPE(System.Int32 ped, System.Int32 weaponObject)`
- `public static System.UInt32 RDR2.Native.WEAPON._GET_DEFAULT_PED_WEAPON_COLLECTION(System.UInt32 pedModel)`
- `public static System.UInt32 RDR2.Native.WEAPON._GET_DEFAULT_UNARMED_WEAPON_HASH(System.Int32 ped)`
- `public static System.Single RDR2.Native.WEAPON._GET_MAX_LOCKON_DISTANCE_OF_CURRENT_PED_WEAPON(System.Int32 ped)`
- `public static System.UInt32 RDR2.Native.WEAPON._GET_PED_CURRENT_HELD_WEAPON(System.Int32 ped)`
- `public static System.UInt32 RDR2.Native.WEAPON._GET_PED_GUN_SPINNING_EQUIPPED_KIT_EMOTE_TWIRL(System.Int32 ped)`
- `public static System.UInt32 RDR2.Native.WEAPON._GET_PED_GUN_SPINNING_HASH_FROM_WEAPON_EMOTE_VARIATION(System.Int32 ped, System.Int32 weaponEmoteVariation)`
- `public static System.UInt32 RDR2.Native.WEAPON._GET_PED_HOGTIE_WEAPON(System.Int32 ped)`
- `public static System.UInt32 RDR2.Native.WEAPON._GET_PED_WEAPON_IN_SLOT(System.Int32 ped, System.UInt32 slotHash)`
- `public static System.Int32 RDR2.Native.WEAPON._GET_PED_WEAPON_OBJECT(System.Int32 ped, System.Boolean p1)`
- `public static System.UInt32 RDR2.Native.WEAPON._GET_PED_WORST_WEAPON(System.Int32 ped, System.Boolean p1, System.Boolean p2, System.Boolean p3)`
- `public static System.Void RDR2.Native.WEAPON._GET_PLAYER_PED_QUICK_SWAP_WEAPON_BY_GUID(System.Int32 ped, System.UInt64* guidPrimary, System.UInt64* guidSecondary)`
- `public static System.UInt32 RDR2.Native.WEAPON._GET_WEAPONTYPE_MODEL(System.UInt32 weaponHash)`
- `public static System.UInt32 RDR2.Native.WEAPON._GET_WEAPONTYPE_SLOT(System.UInt32 weaponHash)`
- `public static System.Int32 RDR2.Native.WEAPON._GET_WEAPON_ATTACH_POINT(System.Int32 ped, System.Int32 attachPoint)`
- `public static System.UInt32 RDR2.Native.WEAPON._GET_WEAPON_COMPONENT_TYPE_MODEL(System.UInt32 componentHash)`
- `public static System.Single RDR2.Native.WEAPON._GET_WEAPON_DAMAGE(System.Int32 weaponObject)`
- `public static System.Single RDR2.Native.WEAPON._GET_WEAPON_DIRT(System.Int32 weaponObject)`
- `public static System.Int32 RDR2.Native.WEAPON._GET_WEAPON_EMOTE_VARIATION(System.Int32 ped, System.Int32 variation)`
- `public static System.UInt32 RDR2.Native.WEAPON._GET_WEAPON_GUN_SPINNING_WEAPON_EMOTE_TRICK_TYPE_HASH(System.UInt32 emote, System.Int32 weaponEmoteTrickType)`
- `public static System.String RDR2.Native.WEAPON._GET_WEAPON_NAME(System.UInt32 weaponHash)`
- `public static System.String RDR2.Native.WEAPON._GET_WEAPON_NAME_2(System.UInt32 weaponHash)`
- `public static System.String RDR2.Native.WEAPON._GET_WEAPON_NAME_WITH_PERMANENT_DEGRADATION(System.UInt32 weaponHash, System.Single permanentDegradationLevel)`
- `public static System.Int32 RDR2.Native.WEAPON._GET_WEAPON_OBJECT_FROM_PED(System.Int32 ped, System.Boolean p1)`
- `public static System.Single RDR2.Native.WEAPON._GET_WEAPON_SCALE(System.Int32 weaponObject)`
- `public static System.Single RDR2.Native.WEAPON._GET_WEAPON_SOOT(System.Int32 weaponObject)`
- `public static System.UInt32 RDR2.Native.WEAPON._GET_WEAPON_STAT_ID(System.UInt32 weaponHash)`
- `public static System.UInt32 RDR2.Native.WEAPON._GET_WEAPON_TYPE_FROM_AMMO_TYPE(System.UInt32 ammoType)`
- `public static System.UInt32 RDR2.Native.WEAPON._GET_WEAPON_UNLOCK(System.UInt32 weaponHash)`
- `public static System.Void RDR2.Native.WEAPON._GIVE_WEAPON_COLLECTION_TO_PED(System.Int32 ped, System.UInt32 weaponCollection)`
- `public static System.Void RDR2.Native.WEAPON._GIVE_WEAPON_COMPONENT_TO_ENTITY(System.Int32 entity, System.UInt32 componentHash, System.UInt32 weaponHash, System.Boolean p3)`
- `public static System.Void RDR2.Native.WEAPON._GIVE_WEAPON_COMPONENT_TO_WEAPON_OBJECT(System.Int32* weaponObject, System.Int32 ped, System.UInt32 componentHash, System.Boolean p3)`
- `public static System.Boolean RDR2.Native.WEAPON._HAS_ENTITY_BEEN_DAMAGED_BY_WEAPON(System.Int32 entity, System.UInt32 weaponName, System.Int32 weaponType)`
- `public static System.Boolean RDR2.Native.WEAPON._HAS_ENTITY_BEEN_DAMAGED_BY_WEAPON_RECENTLY(System.Int32 entity, System.UInt32 weaponHash, System.Int32 ms)`
- `public static System.Boolean RDR2.Native.WEAPON._HAS_PED_GOT_WEAPON_COMPONENT(System.Int32 ped, System.UInt32 componentHash, System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON._HAS_WEAPON_ASSET_LOADED(System.UInt32 weaponHash)`
- `public static System.Void RDR2.Native.WEAPON._HIDE_PED_WEAPONS(System.Int32 ped, System.Int32 p0, System.Boolean immediately)`
- `public static System.Void RDR2.Native.WEAPON._HOLSTER_PED_WEAPONS(System.Int32 ped, System.Boolean p1, System.Boolean p2, System.Boolean p3, System.Boolean immediately)`
- `public static System.Boolean RDR2.Native.WEAPON._IS_AMMO_SILENT(System.UInt32 ammoHash)`
- `public static System.Boolean RDR2.Native.WEAPON._IS_AMMO_SILENT_2(System.UInt32 ammoHash)`
- `public static System.Boolean RDR2.Native.WEAPON._IS_AMMO_TYPE_VALID_FOR_WEAPON(System.UInt32 weaponHash, System.UInt32 ammoHash)`
- `public static System.Boolean RDR2.Native.WEAPON._IS_AMMO_VALID(System.UInt32 ammoHash)`
- `public static System.Boolean RDR2.Native.WEAPON._IS_PED_CURRENT_WEAPON_HOLSTERED(System.Int32 ped)`
- `public static System.Boolean RDR2.Native.WEAPON._IS_TARGET_PED_CONSTRAINED_BY_PED_USING_BOLAS(System.Int32 ped, System.Int32 targetPed)`
- `public static System.Boolean RDR2.Native.WEAPON._IS_WEAPON_BINOCULARS(System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON._IS_WEAPON_KIT(System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON._IS_WEAPON_KIT_2(System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON._IS_WEAPON_KNIFE(System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON._IS_WEAPON_LANTERN(System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON._IS_WEAPON_LASSO(System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON._IS_WEAPON_ONE_HANDED(System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON._IS_WEAPON_SILENT(System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON._IS_WEAPON_SNIPER(System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON._IS_WEAPON_THROWABLE(System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON._IS_WEAPON_TORCH(System.UInt32 weaponHash)`
- `public static System.Boolean RDR2.Native.WEAPON._IS_WEAPON_TWO_HANDED(System.UInt32 weaponHash)`
- `public static System.Void RDR2.Native.WEAPON._LISTEN_PROJECTILE_HIT_EVENTS(System.Boolean listen)`
- `public static System.UInt64 RDR2.Native.WEAPON._MAKE_PED_RELOAD(System.Int32 ped)`
- `public static System.UInt64 RDR2.Native.WEAPON._REFILL_AMMO_IN_CLIP(System.Int32 ped, System.UInt64* clipInventoryUid, System.Int32 p2)`
- `public static System.UInt64 RDR2.Native.WEAPON._REFILL_AMMO_IN_CURRENT_PED_WEAPON(System.Int32 ped)`
- `public static System.Void RDR2.Native.WEAPON._REMOVE_ALL_PED_AMMO(System.Int32 ped)`
- `public static System.Void RDR2.Native.WEAPON._REMOVE_AMMO_FROM_PED(System.Int32 ped, System.UInt32 weaponHash, System.Int32 amount, System.UInt32 removeReason)`
- `public static System.Void RDR2.Native.WEAPON._REMOVE_AMMO_FROM_PED_BY_TYPE(System.Int32 ped, System.UInt32 ammoHash, System.Int32 amount, System.UInt32 removeReason)`
- `public static System.Void RDR2.Native.WEAPON._REMOVE_WEAPON_ASSET(System.UInt32 weaponHash)`
- `public static System.Void RDR2.Native.WEAPON._REMOVE_WEAPON_COMPONENT_FROM_PED(System.Int32 ped, System.UInt32 componentHash, System.UInt32 weaponHash)`
- `public static System.Void RDR2.Native.WEAPON._REMOVE_WEAPON_FROM_PED_BY_GUID(System.Int32 ped, System.UInt64* weaponGuid, System.UInt32 removeReason)`
- `public static System.Void RDR2.Native.WEAPON._REQUEST_WEAPON_ASSET(System.UInt32 weaponHash, System.Int32 p1, System.Boolean p2)`
- `public static System.Void RDR2.Native.WEAPON._SEND_WEAPON_TO_INVENTORY(System.Int32 ped, System.UInt32 weaponHash)`
- `public static System.Void RDR2.Native.WEAPON._SET_ACTIVE_GUN_SPINNING_EQUIP_KIT_EMOTE_TWIRL(System.Int32 ped, System.UInt32 emote)`
- `public static System.Void RDR2.Native.WEAPON._SET_ACTIVE_GUN_SPINNING_KIT_EMOTE_TWIRL(System.Int32 ped, System.Int32 weaponEmoteTrickType, System.UInt32 spin)`
- `public static System.Void RDR2.Native.WEAPON._SET_ALLOW_DUAL_WIELD(System.Int32 ped, System.Boolean allow)`
- `public static System.Void RDR2.Native.WEAPON._SET_AMMO_IN_TURRET(System.Int32 vehicle, System.UInt32 turretHash, System.Int32 ammo)`
- `public static System.Void RDR2.Native.WEAPON._SET_AMMO_TYPE_FOR_PED_WEAPON(System.Int32 ped, System.UInt32 weaponHash, System.UInt32 ammoHash)`
- `public static System.Void RDR2.Native.WEAPON._SET_AMMO_TYPE_FOR_PED_WEAPON_INVENTORY(System.Int32 ped, System.UInt64* weaponInventoryUid, System.UInt32 ammoHash)`
- `public static System.Void RDR2.Native.WEAPON._SET_FORCE_AUTO_EQUIP(System.Int32 ped, System.Boolean toggle)`
- `public static System.Void RDR2.Native.WEAPON._SET_FORCE_CURRENT_WEAPON_INTO_COCKED_STATE(System.Int32 ped, System.Int32 attachPoint)`
- `public static System.Void RDR2.Native.WEAPON._SET_GUN_SPINNING_INVENTORY_SLOT_ID_ACTIVATE(System.Int32 ped, System.Int32 emoteType)`
- `public static System.Void RDR2.Native.WEAPON._SET_PED_ALL_WEAPONS_VISIBILITY(System.Int32 ped, System.Boolean visible)`
- `public static System.Void RDR2.Native.WEAPON._SET_PED_INFINITE_AMMO_CLIP(System.Int32 ped, System.Boolean toggle)`
- `public static System.Void RDR2.Native.WEAPON._SET_PED_WEAPON_ATTACH_POINT_VISIBILITY(System.Int32 ped, System.Int32 attachPoint, System.Boolean visible)`
- `public static System.Void RDR2.Native.WEAPON._SET_VEHICLE_WEAPON_HEADING_LIMITS(System.Int32 vehicle, System.Int32 p1, System.Single minHeading, System.Single maxHeading)`
- `public static System.UInt64 RDR2.Native.WEAPON._SET_VEHICLE_WEAPON_HEADING_LIMITS_2(System.Int32 vehicle, System.Int32 p1, System.Single minHeading, System.Single maxHeading)`
- `public static System.Void RDR2.Native.WEAPON._SET_WEAPON_DAMAGE(System.Int32 weaponObject, System.Single level, System.Boolean p2)`
- `public static System.Void RDR2.Native.WEAPON._SET_WEAPON_DEGRADATION(System.Int32 weaponObject, System.Single level)`
- `public static System.Void RDR2.Native.WEAPON._SET_WEAPON_DIRT(System.Int32 weaponObject, System.Single level, System.Boolean p2)`
- `public static System.Void RDR2.Native.WEAPON._SET_WEAPON_LEVEL_THRESHOLD(System.Int32 weaponObject, System.Single threshold)`
- `public static System.Void RDR2.Native.WEAPON._SET_WEAPON_SCALE(System.Int32 weaponObject, System.Single scale)`
- `public static System.Void RDR2.Native.WEAPON._SET_WEAPON_SOOT(System.Int32 weaponObject, System.Single level, System.Boolean p2)`

## `RDR2.Native.ZONE`

- Kind: class
- Base type: `System.Object`

- `public static System.UInt32 RDR2.Native.ZONE._GET_MAP_ZONE_AT_COORDS(RDR2.Math.Vector3 vec, System.Int32 type)`
- `public static System.UInt32 RDR2.Native.ZONE._GET_MAP_ZONE_AT_COORDS(System.Single x, System.Single y, System.Single z, System.Int32 type)`
- `public static System.UInt32 RDR2.Native.ZONE._GET_WATER_MAP_ZONE_AT_COORDS(RDR2.Math.Vector3 vec)`
- `public static System.UInt32 RDR2.Native.ZONE._GET_WATER_MAP_ZONE_AT_COORDS(System.Single x, System.Single y, System.Single z)`

## `RDR2.Native._NAMESPACE30`

- Kind: class
- Base type: `System.Object`

- `public static System.Void RDR2.Native._NAMESPACE30.SET_MOUSE_CURSOR_STYLE(System.Int32 spriteId)`
- `public static System.Void RDR2.Native._NAMESPACE30.SET_MOUSE_CURSOR_THIS_FRAME()`
- `public static System.Void RDR2.Native._NAMESPACE30._0x0546B117BB17548B()`
- `public static System.Boolean RDR2.Native._NAMESPACE30._0x2B8B605F2A9E64BF()`
- `public static System.Boolean RDR2.Native._NAMESPACE30._0x61CAE9D1FD055E44()`
- `public static System.Boolean RDR2.Native._NAMESPACE30._0xF7F51A57349739F2()`

## `RDR2.Native._NAMESPACE4`

- Kind: class
- Base type: `System.Object`

- `public static System.Void RDR2.Native._NAMESPACE4._REPORT_PLAYER_BAD_SPORT_BEHAVIOR(System.UInt64* gamerHandle, System.Int32 badSportBehaviorType)`

## `RDR2.NaturalMotion.ActivePoseHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.AdaptiveMode`

- Kind: enum
- Base type: `System.Enum`

*No public methods declared.*

Enum values:
- `DirectionAndSpeed` = `2`
- `DirectionSpeedAndStrength` = `3`
- `NotAdaptive` = `0`
- `OnlyDirection` = `1`

## `RDR2.NaturalMotion.AnimPoseHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.AnimSource`

- Kind: enum
- Base type: `System.Enum`

*No public methods declared.*

Enum values:
- `AnimItems` = `2`
- `CurrentItems` = `0`
- `PreviousItems` = `1`

## `RDR2.NaturalMotion.ApplyBulletImpulseHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.ApplyImpulseHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.ArmDirections`

- Kind: enum
- Base type: `System.Enum`

*No public methods declared.*

Enum values:
- `Adaptive` = `0`
- `Backwards` = `-1`
- `Forwards` = `1`

## `RDR2.NaturalMotion.ArmsWindmillAdaptiveHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.ArmsWindmillHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.BalancerCollisionsReactionHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.BodyBalanceHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.BodyFoetalHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.BodyRelaxHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.BodyRollUpHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.BodyWritheHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.BraceForImpactHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.BuoyancyHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.CarriedHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.CatchFallHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.ConfigureBalanceHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.ConfigureBalanceResetHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.ConfigureBulletsExtraHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.ConfigureBulletsHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.ConfigureConstraintsHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.ConfigureLimitsHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.ConfigureSelfAvoidanceHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.ConfigureShotInjuredArmHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.ConfigureShotInjuredLegHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.ConfigureSoftLimitHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.CustomHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.Message`

- `public System.Void RDR2.NaturalMotion.CustomHelper.Abort()`
- `public System.Void RDR2.NaturalMotion.CustomHelper.Start()`
- `public System.Void RDR2.NaturalMotion.CustomHelper.Start(System.Int32 duration)`
- `public System.Void RDR2.NaturalMotion.CustomHelper.Stop()`

## `RDR2.NaturalMotion.DangleHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.DefineAttachedObjectHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.ElectrocuteHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.Euphoria`

- Kind: class
- Base type: `System.Object`

*No public methods declared.*

## `RDR2.NaturalMotion.FallOverWallHelper`

- Kind: class
- Base type: `RDR2.NaturalMotion.CustomHelper`

*No public methods declared.*

## `RDR2.NaturalMotion.FallType`

- Kind: enum
- Base type: `System.Enum`

*No public methods declared.*

Enum values:
- `DontChangeStep` = `1`
- `ForceBalance` = `2`
- `RampDownStiffness` = `0`
- `Slump` = `3`

