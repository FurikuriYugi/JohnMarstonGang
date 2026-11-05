# Native Lock-On Interaction Guide

## REGISTER_INTERACTION_LOCKON_PROMPT

### Signature
```csharp
RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(
	Int32 p0,           // Entity handle (Ped/NPC/Horse)
	String p1,          // Label/text displayed for the interaction
	Single p2,          // Radius (interaction range)
	Single p3,          // Usually same as p2 (radius)
	Int32 p4,           // Unknown (likely flags or type)
	Single p5,          // Unknown (possibly related to positioning)
	Single p6,          // Unknown (possibly related to animation or behavior)
	Int32 p7,           // Unknown (possibly timeout or priority)
	Boolean p8,         // Determines if ILO prompt is lock-on prompt with RMB (Right Mouse Button)
	Int32 p9            // Unknown (possibly group or category)
)
```

### Known Parameters

| Parameter | Type | Known Value | Purpose |
|-----------|------|-------------|---------|
| p0 | Int32 | Entity handle | The target NPC/Ped/Horse |
| p1 | String | "Recruit", "Follow", etc. | UI label text |
| p2 | Single | 2.0f - 5.0f | Interaction radius in game units |
| p3 | Single | Same as p2 | Usually mirrors p2 value |
| p4 | Int32 | 0 or 1 (?) | Type flag or behavior mode |
| p5 | Single | 0.0f (?) | Unknown offset or parameter |
| p6 | Single | 0.0f (?) | Unknown offset or parameter |
| p7 | Int32 | 0 (?) | Unknown timeout or priority |
| p8 | Boolean | **true** | Enable lock-on with RMB interaction |
| p9 | Int32 | 0 (?) | Unknown category or group |

### Key Behavior

- **p8 = true**: Prompt appears as interactive lock-on prompt with RMB support (Rockstar-style native UI)
- **p3 = p2**: Standard practice; usually set to the same radius value
- The native automatically handles UI rendering including button glyphs and state changes
- Button presses are likely detected via native like `_LAST_INPUT_COMMAND_WAS_ACCEPTED` or similar, not `_UI_PROMPT_IS_JUST_PRESSED`

### Usage Pattern (Hypothesis)

```csharp
// In OnTick or frame update loop:
int entityHandle = <target entity>;
string actionLabel = "Recruit";
float radius = 2.5f;
bool isLockOnPrompt = true; // p8 = true for native-style UI

RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(
	entityHandle,           // p0
	actionLabel,            // p1
	radius,                 // p2
	radius,                 // p3 (same as p2)
	1,                      // p4
	0.0f,                   // p5
	0.0f,                   // p6
	0,                      // p7
	isLockOnPrompt,         // p8
	0                       // p9
);

// Call every frame while target is valid
// Button press detection TBD - try LAST_INPUT_COMMAND_WAS_ACCEPTED or similar
```

### References
- Installed at: `C:\Games\Red Dead Redemption 2\ScriptHookRDRNetAPI.xml` (lines 5884-5888)
- XML Doc: "p8 determines whether the ILO prompt is a lock on prompt with RMB"
- "p3 is usually the same value of radius"
