<!-- GhidraTrace lines 1001-1048 of 1048 -->
```csharp
}

undefined1 FUN_180014880(uint *param_1){
  uint uVar1;
  undefined1 in_AL;
  int iVar2;
  __uint64 *p_Var3;
  ulonglong local_res8;
  
  if (((char)param_1[1] == '\0') || (*(char *)((longlong)param_1 + 5) == '\0')) {
    return 0;
  }
  uVar1 = param_1[6];
  if (uVar1 == 0) {
    uVar1 = *param_1;
    nativeInit(0xc92ac953f0a982ae);
    local_res8 = (ulonglong)uVar1;
    nativePush64(local_res8);
    local_res8 = 0;
  }
  else {
    if (uVar1 == 1) {
      iVar2 = FUN_1800147b0(*param_1);
      return iVar2 != 0;
    }
    if (uVar1 == 2) {
      iVar2 = FUN_1800147b0(*param_1);
      if (iVar2 != 0) {
        if ((char)param_1[7] == '\x01') {
          return 0;
        }
        *(undefined1 *)(param_1 + 7) = 1;
        return 1;
      }
      *(undefined1 *)(param_1 + 7) = 0;
    }
    else if (uVar1 != 3) {
      return in_AL;
    }
    uVar1 = *param_1;
    nativeInit(0x845ce958416dc473);
    local_res8 = (ulonglong)uVar1;
  }
  nativePush64(local_res8);
  p_Var3 = nativeCall();
  return (int)*p_Var3 != 0;
}
```
```
